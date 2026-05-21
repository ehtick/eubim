using System.Globalization;
using System.Text;
using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Structure;
using Autodesk.Revit.UI;

namespace Demo02.Commands;

[Transaction(TransactionMode.Manual)]
public class CmdLittleHouse3Storey : IExternalCommand
{
    private const double MillimetersToFeet = 1.0 / 304.8;
    private const double HouseWidthMm = 3000.0;
    private const double HouseLengthMm = 4000.0;
    private const double StoreyHeightMm = 3000.0;
    private const double WindowSillHeightMm = 900.0;

    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var document = commandData.Application.ActiveUIDocument?.Document;
        if (document is null)
        {
            message = "No active document was available.";
            ExecutionLog.Write("LittleHouse3Storey.Execute", message);
            return Result.Failed;
        }

        return Execute2(document);
    }

    public static Result Execute2(Document document)
    {
        var report = new LittleHouseReport(document);

        try
        {
            using var transaction = new Transaction(document, "Create little house 3 storey");
            transaction.Start();

            var level0 = EnsureLevel(document, report, "Little House 3S Level 0", 0.0);
            var level1 = EnsureLevel(document, report, "Little House 3S Level 3000", StoreyHeightMm * MillimetersToFeet);
            var level2 = EnsureLevel(document, report, "Little House 3S Level 6000", 2.0 * StoreyHeightMm * MillimetersToFeet);

            var boundary = CreateBoundary();
            var walls = CreateWalls(document, report, level0, level2, boundary);
            _ = CreateFloor(document, report, level0, walls, boundary);
            _ = CreateFloor(document, report, level1, walls, boundary);
            var roof = CreateRoof(document, report, level2, boundary);
            AttachWallsToRoof(walls, roof);

            PlaceDoorAndWindows(document, report, level0, walls);

            transaction.Commit();

            var mdPath = report.WriteMarkdown();
            ExecutionLog.Write("LittleHouse3Storey.Execute2", $"Report written to '{mdPath}'.");
            return Result.Succeeded;
        }
        catch (Exception ex)
        {
            ExecutionLog.Write("LittleHouse3Storey.Execute2", ex.ToString());
            return Result.Failed;
        }
    }

    private static Level EnsureLevel(Document document, LittleHouseReport report, string name, double elevationFeet)
    {
        var level = new FilteredElementCollector(document)
            .OfClass(typeof(Level))
            .Cast<Level>()
            .FirstOrDefault(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));

        if (level is null)
        {
            level = Level.Create(document, elevationFeet);
            level.Name = name;
        }

        report.Levels.Add(level);
        return level;
    }

    private static CurveLoop CreateBoundary()
    {
        var width = HouseWidthMm * MillimetersToFeet;
        var length = HouseLengthMm * MillimetersToFeet;

        var p0 = new XYZ(0, 0, 0);
        var p1 = new XYZ(length, 0, 0);
        var p2 = new XYZ(length, width, 0);
        var p3 = new XYZ(0, width, 0);

        return CurveLoop.Create(new[]
        {
            Line.CreateBound(p0, p1),
            Line.CreateBound(p1, p2),
            Line.CreateBound(p2, p3),
            Line.CreateBound(p3, p0)
        });
    }

    private static IReadOnlyList<Wall> CreateWalls(Document document, LittleHouseReport report, Level baseLevel, Level upperLevel, CurveLoop boundary)
    {
        var walls = new List<Wall>();
        foreach (var curve in boundary)
        {
            var wall = Wall.Create(document, curve, baseLevel.Id, false);
            wall.get_Parameter(BuiltInParameter.WALL_HEIGHT_TYPE)?.Set(upperLevel.Id);
            wall.get_Parameter(BuiltInParameter.WALL_TOP_OFFSET)?.Set(0.0);
            walls.Add(wall);
        }

        report.Walls.AddRange(walls);
        return walls;
    }

    private static Floor CreateFloor(Document document, LittleHouseReport report, Level level, IReadOnlyList<Wall> walls, CurveLoop boundary)
    {
        var floorType = new FilteredElementCollector(document)
            .OfClass(typeof(FloorType))
            .Cast<FloorType>()
            .FirstOrDefault();

        if (floorType is null)
        {
            throw new InvalidOperationException("No floor type was found in the document.");
        }

        var floorBoundary = ExpandBoundary(boundary, GetWallHalfWidth(walls));
        var floor = Floor.Create(document, new List<CurveLoop> { floorBoundary }, floorType.Id, level.Id);
        report.Floors.Add(floor);
        return floor;
    }

    private static FootPrintRoof CreateRoof(Document document, LittleHouseReport report, Level level, CurveLoop boundary)
    {
        var roofType = new FilteredElementCollector(document)
            .OfClass(typeof(RoofType))
            .Cast<RoofType>()
            .FirstOrDefault();

        if (roofType is null)
        {
            throw new InvalidOperationException("No roof type was found in the document.");
        }

        var roofBoundary = ExpandBoundary(boundary, 300.0 * MillimetersToFeet);
        var footprint = new ModelCurveArray();
        var roof = document.Create.NewFootPrintRoof(ToCurveArray(roofBoundary), level, roofType, out footprint);
        SetRoofSlopeAllEdges(roof, footprint);
        report.Roofs.Add(roof);
        return roof;
    }

    private static CurveLoop ExpandBoundary(CurveLoop boundary, double offset)
    {
        var width = HouseWidthMm * MillimetersToFeet;
        var length = HouseLengthMm * MillimetersToFeet;
        var offsetX = -offset;
        var offsetY = -offset;
        return CurveLoop.Create(new[]
        {
            Line.CreateBound(new XYZ(offsetX, offsetY, 0), new XYZ(length + offset, offsetY, 0)),
            Line.CreateBound(new XYZ(length + offset, offsetY, 0), new XYZ(length + offset, width + offset, 0)),
            Line.CreateBound(new XYZ(length + offset, width + offset, 0), new XYZ(offsetX, width + offset, 0)),
            Line.CreateBound(new XYZ(offsetX, width + offset, 0), new XYZ(offsetX, offsetY, 0))
        });
    }

    private static double GetWallHalfWidth(IReadOnlyList<Wall> walls)
    {
        var width = walls.FirstOrDefault()?.Width ?? 0.0;
        return Math.Max(width / 2.0, 0.0);
    }

    private static void SetRoofSlopeAllEdges(FootPrintRoof roof, ModelCurveArray footprint)
    {
        foreach (ModelCurve modelCurve in footprint)
        {
            roof.set_DefinesSlope(modelCurve, true);
            roof.set_SlopeAngle(modelCurve, 0.5);
        }
    }

    private static void AttachWallsToRoof(IReadOnlyList<Wall> walls, Element roof)
    {
        foreach (var wall in walls)
        {
            wall.AddAttachment(roof.Id, AttachmentLocation.Top);
        }
    }

    private static void PlaceDoorAndWindows(Document document, LittleHouseReport report, Level level, IReadOnlyList<Wall> walls)
    {
        var doorSymbol = FindSymbol(document, "Doors");
        var windowSymbol = FindSymbol(document, "Windows");

        if (doorSymbol is null || windowSymbol is null)
        {
            throw new InvalidOperationException("Door or window family symbol could not be loaded.");
        }

        if (!doorSymbol.IsActive) doorSymbol.Activate();
        if (!windowSymbol.IsActive) windowSymbol.Activate();
        document.Regenerate();

        var longWalls = walls.OrderByDescending(w => w.get_Parameter(BuiltInParameter.CURVE_ELEM_LENGTH)?.AsDouble() ?? 0.0).ToList();
        var doorWall = longWalls[0];
        var doorPoint = GetWallMidpointAtHeight(doorWall, 0.0);
        report.Instances.Add(CreateHostedInstance(document, doorWall, doorSymbol, doorPoint, level, "Door", true));

        var windowTargets = walls.Where(w => !ReferenceEquals(w, doorWall)).ToList();
        foreach (var wall in windowTargets)
        {
            var point = GetWallMidpointAtHeight(wall, WindowSillHeightMm * MillimetersToFeet);
            report.Instances.Add(CreateHostedInstance(document, wall, windowSymbol, point, level, "Window", false));
        }
    }

    private static FamilyInstance CreateHostedInstance(Document document, Wall hostWall, FamilySymbol symbol, XYZ location, Level level, string label, bool flipFacing)
    {
        var instance = document.Create.NewFamilyInstance(location, symbol, hostWall, level, StructuralType.NonStructural);
        document.Regenerate();
        if (flipFacing)
        {
            instance.flipFacing();
        }

        ExecutionLog.Write("LittleHouse3Storey.Execute2", $"Placed {label} '{symbol.FamilyName}:{symbol.Name}' on wall '{hostWall.Id}'.");
        return instance;
    }

    private static XYZ GetWallMidpointAtHeight(Wall wall, double heightFeet)
    {
        var location = (LocationCurve)wall.Location;
        var midpoint = location.Curve.Evaluate(0.5, true);
        return new XYZ(midpoint.X, midpoint.Y, midpoint.Z + heightFeet);
    }

    private static FamilySymbol? FindSymbol(Document document, string categoryName)
    {
        return new FilteredElementCollector(document)
            .OfClass(typeof(FamilySymbol))
            .Cast<FamilySymbol>()
            .FirstOrDefault(symbol =>
                symbol.Family is not null &&
                symbol.Family.FamilyCategory is not null &&
                !symbol.FamilyName.Contains("Tag", StringComparison.OrdinalIgnoreCase) &&
                string.Equals(symbol.Category?.Name, categoryName, StringComparison.OrdinalIgnoreCase));
    }

    private sealed class LittleHouseReport
    {
        private readonly Document _document;
        private readonly string _stamp;

        public List<Level> Levels { get; } = [];
        public List<Wall> Walls { get; } = [];
        public List<Floor> Floors { get; } = [];
        public List<FootPrintRoof> Roofs { get; } = [];
        public List<FamilyInstance> Instances { get; } = [];

        public LittleHouseReport(Document document)
        {
            _document = document;
            _stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff", CultureInfo.InvariantCulture);
        }

        public string WriteMarkdown()
        {
            var logDirectory = Path.Combine(@"C:\Users\j\w\src\eubim\Codex\Demo02", "log");
            Directory.CreateDirectory(logDirectory);
            var path = Path.Combine(logDirectory, $"{_stamp}-LittleHouse3Storey-report.md");
            var sb = new StringBuilder();
            sb.AppendLine("# Little House 3 Storey Report");
            sb.AppendLine();
            sb.AppendLine($"Document: `{_document.Title}`");
            sb.AppendLine($"Path: `{_document.PathName}`");
            sb.AppendLine();
            sb.AppendLine("## Elements");
            sb.AppendLine($"- Levels: {Levels.Count}");
            sb.AppendLine($"- Walls: {Walls.Count}");
            sb.AppendLine($"- Floors: {Floors.Count}");
            sb.AppendLine($"- Roofs: {Roofs.Count}");
            sb.AppendLine($"- Family instances: {Instances.Count}");
            sb.AppendLine();
            sb.AppendLine("## Level Names");
            foreach (var level in Levels)
            {
                sb.AppendLine($"- {level.Name} @ {level.Elevation.ToString("F3", CultureInfo.InvariantCulture)} ft");
            }

            sb.AppendLine();
            sb.AppendLine("## Instances");
            foreach (var instance in Instances)
            {
                sb.AppendLine($"- {instance.Symbol?.FamilyName}:{instance.Symbol?.Name} ({instance.Category?.Name})");
            }

            File.WriteAllText(path, sb.ToString());
            ExecutionLog.Write("LittleHouse3Storey.Report", $"Markdown report written to '{path}'.");
            return path;
        }
    }

    private static CurveArray ToCurveArray(CurveLoop loop)
    {
        var array = new CurveArray();
        foreach (var curve in loop)
        {
            array.Append(curve);
        }

        return array;
    }
}

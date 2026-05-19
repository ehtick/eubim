using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace Demo02.Commands;

[Transaction(TransactionMode.Manual)]
public class CmdDemo02 : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        var document = commandData.Application.ActiveUIDocument?.Document;

        if (document is null)
        {
            message = "No active document was available.";
            ExecutionLog.Write("Execute", message);
            return Result.Failed;
        }

        ExecutionLog.Write("Execute", $"Dispatching Execute2 for '{document.Title}'.");
        return Execute2(document);
    }

    public static Result Execute2(Document document)
    {
        ExecutionLog.Write("Execute2", $"Document: '{document.Title}' | Path: '{document.PathName}'.");
        return Result.Succeeded;
    }
}

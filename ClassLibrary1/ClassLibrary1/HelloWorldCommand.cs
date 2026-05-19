using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;

namespace ClassLibrary1;

[Transaction(TransactionMode.Manual)]
public class HelloWorldCommand : IExternalCommand
{
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
        TaskDialog.Show("Hello World", "Hello from a minimal Revit external command.");
        return Result.Succeeded;
    }
}

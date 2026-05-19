using Autodesk.Revit.Attributes;
using Nice3point.Revit.Toolkit.External;
using Demo01.ViewModels;
using Demo01.Views;

namespace Demo01.Commands;

/// <summary>
///     External command entry point.
/// </summary>
[UsedImplicitly]
[Transaction(TransactionMode.Manual)]
public class StartupCommand : ExternalCommand
{
    public override void Execute()
    {
        var viewModel = new Demo01ViewModel();
        var view = new Demo01View(viewModel);
        view.ShowDialog();
    }
}
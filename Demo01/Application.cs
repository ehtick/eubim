using Nice3point.Revit.Toolkit.External;
using Demo01.Commands;

namespace Demo01;

/// <summary>
///     Application entry point
/// </summary>
[UsedImplicitly]
public class Application : ExternalApplication
{
    public override void OnStartup()
    {
        CreateRibbon();
    }


    private void CreateRibbon()
    {
        var panel = Application.CreatePanel("Commands", "Demo01");

        panel.AddPushButton<StartupCommand>("Execute")
            .SetImage("/Demo01;component/Resources/Icons/RibbonIcon16.png")
            .SetLargeImage("/Demo01;component/Resources/Icons/RibbonIcon32.png");
    }
}
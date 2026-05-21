using Autodesk.Revit.ApplicationServices;
using Autodesk.Revit.DB;
using Autodesk.Revit.DB.Events;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using System.Globalization;
using System.IO;
using System.Reflection;

namespace Demo02;

public class Application : IExternalApplication
{
    private UIControlledApplication? _uiControlledApplication;

    public Result OnStartup(UIControlledApplication application)
    {
        _uiControlledApplication = application;
        application.ControlledApplication.ApplicationInitialized += OnApplicationInitialized;
        RegisterCommand(application);

        ExecutionLog.Write(
            "OnStartup",
            $"Subscribed to ApplicationInitialized. LateAddinLoading={application.IsLateAddinLoading}.");
        return Result.Succeeded;
    }

    public Result OnShutdown(UIControlledApplication application)
    {
        if (_uiControlledApplication is not null)
        {
            _uiControlledApplication.ControlledApplication.ApplicationInitialized -= OnApplicationInitialized;
            _uiControlledApplication.Idling -= OnIdling;
        }

        ExecutionLog.Write("OnShutdown", "Unsubscribed from ApplicationInitialized and Idling.");
        return Result.Succeeded;
    }

    private void OnApplicationInitialized(object? sender, ApplicationInitializedEventArgs e)
    {
        if (_uiControlledApplication is null)
        {
            ExecutionLog.Write("OnApplicationInitialized", "UIControlledApplication was not available.");
            return;
        }

        _uiControlledApplication.Idling += OnIdling;
        ExecutionLog.Write("OnApplicationInitialized", "Subscribed to Idling.");
    }

    private void OnIdling(object? sender, IdlingEventArgs e)
    {
        if (_uiControlledApplication is null)
        {
            ExecutionLog.Write("OnIdling", "UIControlledApplication was not available.");
            return;
        }

        var uiApplication = sender as UIApplication;
        if (uiApplication is null)
        {
            ExecutionLog.Write("OnIdling", "UIApplication sender was not available.");
            return;
        }

        var document = uiApplication.ActiveUIDocument?.Document;

        if (document is null)
        {
            ExecutionLog.Write("OnIdling", "No active document was available.");
            return;
        }

        _uiControlledApplication.Idling -= OnIdling;
        ExecutionLog.Write("OnIdling", $"Invoking CmdLittleHouse3Storey.Execute2 for '{document.Title}'.");
        Commands.CmdLittleHouse3Storey.Execute2(document);
    }

    private void RegisterCommand(UIControlledApplication application)
    {
        var panel = application.CreateRibbonPanel("Demo02");
        var assemblyPath = Assembly.GetExecutingAssembly().Location;
        var buttonData = new PushButtonData(
            "CmdDemo02",
            "CmdDemo02",
            assemblyPath,
            "Demo02.Commands.CmdDemo02");

        panel.AddItem(buttonData);
        ExecutionLog.Write("RegisterCommand", "Registered CmdDemo02 ribbon button.");

        var littleHouseButton = new PushButtonData(
            "CmdLittleHouse",
            "CmdLittleHouse",
            assemblyPath,
            "Demo02.Commands.CmdLittleHouse");

        panel.AddItem(littleHouseButton);
        ExecutionLog.Write("RegisterCommand", "Registered CmdLittleHouse ribbon button.");

        var littleHouse3StoreyButton = new PushButtonData(
            "CmdLittleHouse3Storey",
            "CmdLittleHouse3Storey",
            assemblyPath,
            "Demo02.Commands.CmdLittleHouse3Storey");

        panel.AddItem(littleHouse3StoreyButton);
        ExecutionLog.Write("RegisterCommand", "Registered CmdLittleHouse3Storey ribbon button.");
    }
}

internal static class ExecutionLog
{
    private const string DevDirectory = @"C:\Users\j\w\src\eubim\Codex\Demo02";

    public static string Write(string phase, string details)
    {
        var logDirectory = Path.Combine(DevDirectory, "log");
        Directory.CreateDirectory(logDirectory);

        var stamp = DateTime.Now.ToString("yyyyMMdd-HHmmss-fff", CultureInfo.InvariantCulture);
        var filePath = Path.Combine(logDirectory, $"{stamp}-{phase}.log");
        var lines = new[]
        {
            $"Timestamp: {DateTime.Now:O}",
            $"Phase: {phase}",
            $"Details: {details}"
        };

        File.WriteAllLines(filePath, lines);
        return filePath;
    }
}

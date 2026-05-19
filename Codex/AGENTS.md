# Codex / Demo02 Guidance

## External Command Pattern

- Keep `CmdDemo02.Execute` as the Revit entry point that receives `ExternalCommandData`.
- Move actual document work into `CmdDemo02.Execute2(Document document)`.
- `Execute` should resolve the active document and delegate to `Execute2`.
- `Execute2` should remain document-focused and side-effect limited to the command's work.

## Startup Automation Pattern

- Register the external command in `OnStartup`.
- Subscribe to `ApplicationInitialized` in `OnStartup`.
- Subscribe to `Idling` only from `OnApplicationInitialized`.
- In `OnIdling`, wait for an active document before unsubscribing and invoking `Execute2`.
- If no document is available yet, stay subscribed and retry on the next `Idling` event.

## Revit Startup Test Model

- Use the empty RVT startup model at `C:\Users\j\w\rvt\rvt2026_1_empty.rvt` when verifying automatic startup execution.
- The automatic path is expected to run only after Revit has opened the model and has an active document.

## Local Signing for Unattended Runs

- Generate and use a locally signed .NET assembly for the add-in so Revit does not prompt the user to `Load Always`, `Load Once`, or `Do Not Load`.
- Keep the signing material local to the workspace or developer machine and wire the build so the installed add-in DLL is signed before Revit loads it.
- The goal is zero user interaction during startup so the add-in can be launched and exercised automatically in unit or integration tests.

## Logging

- Write execution logs under `C:\Users\j\w\src\eubim\Codex\Demo02\log`.
- Log the phases `OnStartup`, `OnApplicationInitialized`, `OnIdling`, `Execute`, and `Execute2` when relevant.

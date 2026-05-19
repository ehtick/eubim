# Autonomous Revit Add-In Development: A Chronological Journey
## From Manual AI Code Generation to Fully Autonomous Systems
### February 2026 – May 2026

**Author:** Jeremy Tammik  
**Date:** May 19, 2026  
**Context:** Experiments in autonomous AI-driven Revit API add-in development, progressing from manual intervention loops to fully self-contained systems with automated testing and digital signing.

---

## Executive Summary

Over a four-month period (February–May 2026), this project evolved from manually-guided AI code generation to a fully autonomous Revit add-in development system. The key breakthrough was integrating:
- **AI-driven code generation** (Claude Opus 4.6, OpenAI Codex)
- **Automated testing** (RevitUnit/TUnit frameworks)
- **Digital code signing** (local certificates for unattended startup)
- **Event-driven automation** (ApplicationInitialized, Idling)
- **Unattended Revit execution** (launching from command line with RVT models)

The final system (`Codex/Demo02`) demonstrates:
- **Zero human intervention** in the compile → sign → install → launch → test → verify loop
- **Autonomous command execution** triggered by application startup events
- **Logging and instrumentation** for verification and debugging
- **Repeatable, deterministic behavior** suitable for workshop live demos

---

## Phase 1: Initial Exploration and Research
### Timeframe: Early April 2026
**Goal:** Understand the landscape of AI tools, testing frameworks, and agent systems for Revit development.

### Key Activities:
- **April 25, 2026:** GitHub Copilot (GPT 5.2) consultation on RevitUnit vs. RevitTest frameworks
  - Explored using RevitUnit/TUnit for automated testing
  - Discussed GitHub Actions self-hosted runners for Revit 2026
  - Identified need for local machine setup (Revit cannot run on GitHub-hosted runners)
  
- **April 29, 2026:** Warp/Aider exploration
  - Investigated alternative agentic frameworks
  - Determined OpenAI Codex as the primary tool
  
- **April 30, 2026:** OpenAI Codex research
  - Discovered OpenAI Codex CLI (`/goal` templates, memory, Mem0 integration)
  - Evaluated Codex 5.5 vs. extra-high modes for planning vs. execution
  - Identified production-grade `/goal` workflows (Ideation, Planning, Build, Refactoring, Consolidation, Hardening, Migrations)

### Outcome:
- **Selected tooling:** OpenAI Codex CLI for autonomous code generation and iteration
- **Identified gap:** Need to understand how to make Revit startup and execution fully automated, not just code generation

---

## Phase 2: Title, Agenda, and Vision Refinement
### Timeframe: May 3, 2026
**Goal:** Define the 90-minute workshop title, synopsis, and learning objectives.

### Key Decisions:
- **Workshop Title (Spanish):** "¿El fin del programador de Revit? Crónica de experimentos en codificación autónoma"  
  *"The End of the Revit Programmer? Chronicles of Autonomous Coding Experiments"*

- **Workshop Title (English alternatives):**
  - "Autonomous Revit Add-Ins: AI-Driven Code Generation, Testing, and Self-Debugging"
  - "BIM-Bots: Orchestrating Autonomous Agents for Revit Development"

- **Planned narrative arc:**
  1. Autodesk and The Building Coder (2005–2025)
  2. Q4R4 and the Revit API discussion forum
  3. Retirement (June 2025)
  4. MEP HVAC system generation with Claude Opus 4.6 (February 2026)
  5. Terrascape and beyond
  6. Today: agents (GitHub Copilot, Codex, Cline, Cursor)

### Outcome:
- **Workshop positioning:** Hands-on, "show and tell" format demonstrating autonomous workflows in real time
- **Demo expectation:** Audience requests → AI generation → automated test → live verification in Revit

---

## Phase 3: First Autonomous Demo (Codex Session 1)
### Timeframe: May 6, 2026
**Goal:** Use OpenAI Codex CLI to create a fully working Revit 2026 add-in from scratch, with automated compilation, installation, and Revit launching.

### Prompts to Codex (in sequence):
1. Create folder `Codex`, enter it
2. Create a .NET 8.0 C# class library project named `Demo02`
3. Update VendorId to `io.github.jeremytammik` (reverse domain based on The Building Coder URL)
4. Launch Revit.exe for debugging
5. Add logging instrumentation to `CmdDemo02`
6. Relaunch Revit and verify logging works
7. Export full Codex session transcript

### Results:
- **Success:** Codex created a working Revit 2026 add-in with:
  - External command `CmdDemo02`
  - Proper add-in manifest
  - Timestamped logging to `Codex/Demo02/log` directory
  - Revit integration (install → launch → execute → verify)

- **Verification:** TaskDialog showing log path: `C:\Users\j\w\src\eubim\Codex\Demo02\log\20260506-153503-426-CmdDemo02.log`

- **Token usage:** ~396K tokens (with 1.8M cached), model: `gpt-5.4-mini` (free tier)

- **Artifact:** `/doc/2026-05-06_codex.md` and `/doc/2026-05-06_codex_session_transcript.txt`

### Outcome:
- **Proof of concept:** AI agent can autonomously generate, compile, install, and launch Revit add-ins
- **Gap identified:** Still requires manual Revit UI interaction (clicking command button, closing Revit)

---

## Phase 4: Event-Driven Automation Refactoring
### Timeframe: May 13–15, 2026
**Goal:** Refactor the add-in to automatically execute commands on startup using Revit's `ApplicationInitialized` and `Idling` events, eliminating the need for manual button clicks.

### May 13 Planning Notes:
- Identified need for autonomous execution without user interaction
- Planned refactoring:
  1. Rename `CmdDemo02` → handle better (consider `CmdHello`)
  2. Refactor `Execute` → `Execute2(Document doc)` pattern
  3. Implement `ApplicationInitialized` and `Idling` handlers
  4. Call `Execute2` automatically from `Idling`
  5. Verify via log files
  6. Set up GitHub repository

### May 15 Codex Session (Resume Session from May 6):
**Objective:** Implement and test the event-driven startup sequence

#### Prompts:
1. Refactor `CmdDemo02.Execute` → split into `Execute` (entry point) and `Execute2(Document)` (worker)
2. Subscribe to `ApplicationInitialized` in `OnStartup`
3. Subscribe to `Idling` only from `OnApplicationInitialized`
4. Unsubscribe from `Idling` and call `Execute2` when a document is available
5. Add `[Transaction(TransactionMode.Manual)]` attribute to enable Revit API calls
6. Relaunch with empty RVT file: `C:\Users\j\w\rvt\rvt2026_1_empty.rvt`
7. Verify log sequence shows: `OnStartup` → `OnApplicationInitialized` → `OnIdling` → `Execute2`

#### Results:
- **Issue #1 (Resolved):** Missing `[Transaction(TransactionMode.Manual)]` attribute prevented command execution
  - **Fix:** Added to `CmdDemo02` class
  - **Verification:** Log confirmed full sequence executed on startup

- **Issue #2 (Resolved):** Revit did not show UI, appeared to hang
  - **Root cause:** Missing trusted document on command line
  - **Fix:** Passed `-embed` or specified RVT file: `C:\Users\j\w\rvt\rvt2026_1_empty.rvt`
  - **Verification:** Revit UI appeared, model opened, `Execute2` ran automatically

- **Verification steps:**
  - Startup sequence: `RegisterCommand` → `OnStartup` → `OnApplicationInitialized` → `OnIdling` → `Execute2`
  - Manual button click: `Execute` → `Execute2` (same path)
  - Log files recorded:
    - `20260515-171210-101-Execute.log` (button click entry point)
    - `20260515-171210-101-Execute2.log` (worker execution)

- **Artifacts:** `/doc/2026-05-15_codex_session_transcript.txt`

### Outcome:
- **Major achievement:** Add-in now executes commands automatically on startup with zero user intervention (except closing Revit)
- **Path established:** Codex → compile → sign → install → launch RVT → automatic execute → log → verify

---

## Phase 5: Autonomous Code Signing
### Timeframe: May 15 (concurrent with Phase 4)
**Goal:** Digitally sign the .NET assembly to avoid Revit's "Load Always / Load Once / Do Not Load" trust dialog.

### Challenge:
- Unsigned assemblies trigger Revit security dialog on startup
- Dialog blocks automation (cannot proceed without human input)

### Solution:
- **Generate local code-signing certificate** on the development machine
- **Wire build process** to automatically sign the DLL before installation
- **Import certificate** into local trust stores
- **Result:** Revit treats signed add-in as trusted, skips dialog, executes automatically

### Implementation (by Codex):
- Created `/Codex/Demo02/signing/` directory with certificate material
- Updated `Demo02.csproj` with signing properties
- Build process now:
  1. Compiles assembly
  2. Signs with local certificate
  3. Installs to Revit add-ins folder
  4. Revit loads without prompting

### Verification:
- Log confirmed no security dialog
- Startup sequence executed uninterrupted
- Model opened automatically

### Outcome:
- **Zero-interaction startup:** Revit launches, loads add-in, executes commands, all without human action
- **Prerequisite for automation:** Required for autonomous testing loops

---

## Phase 6: Architectural Review and Pattern Documentation
### Timeframe: May 16, 2026
**Goal:** Review the working implementation, identify patterns, document them for future use, and extend with a second command.

### Codex `/review` Analysis:
Codex identified the following architectural patterns:

1. **External Command Pattern:**
   - `Execute(ExternalCommandData)` → Revit entry point
   - `Execute2(Document)` → Pure document-focused worker
   - Separation of concerns: entry point vs. implementation

2. **Startup Automation Pattern:**
   - `OnStartup` → subscribe to `ApplicationInitialized`
   - `OnApplicationInitialized` → subscribe to `Idling`
   - `OnIdling` → wait for active document, then execute
   - **Defect found:** Original code unsubscribed too early, could miss document if timing was unlucky
   - **Fix:** Keep subscribed until document exists

3. **Logging Pattern:**
   - Timestamped logs to `Codex/Demo02/log/` directory
   - Format: `YYYYMMDD-HHMMSS-milliseconds-methodname.log`
   - Records entry/exit of each phase: `OnStartup`, `OnApplicationInitialized`, `OnIdling`, `Execute`, `Execute2`

### Documentation Deliverable: AGENTS.md
Created `/Codex/AGENTS.md` to capture:
- External Command Pattern (Execute vs. Execute2)
- Startup Automation Pattern (ApplicationInitialized → Idling)
- Local Signing Pattern (unattended execution)
- RVT startup model path
- Log directory structure
- **Guiding principle:** All new external commands follow this structure

### Outcome:
- **Reusable pattern library:** Future commands inherit the established architecture
- **Reduced cognitive load:** Developers follow proven patterns, Codex generates consistent code

---

## Phase 7: Multi-Command System (CmdLittleHouse)
### Timeframe: May 16, 2026
**Goal:** Extend the system with a second external command to prove the pattern scales, and implement a complex modeling task (the "Little House" from Building Coder archives).

### Command: CmdLittleHouse
**Specification:** Create a complete small BIM model with:
- 2 levels at elevation 0 and 3000 mm
- 4 walls forming a 3 m × 4 m rectangle between levels
- 1 floor (pad) on lower level
- Sloped roof on top
- Door centered on one long wall
- Windows on three other walls
- Interior roof definition

**Source:** Based on `Lab2_0_CreateLittleHouse` from jeremytammik/AdnRevitApiLabsXtra (ca. 2010)

### Development Process (Codex-driven):

#### Iteration 1: Initial implementation
- Implemented basic geometry
- **Issue:** Window/door family lookup not finding standard families
- **Fix:** Used Revit's default category-based family lookup (Windows, Doors categories)

#### Iteration 2: Geometry refinement
- **Issue:** Floor too small (did not extend to wall outer edges)
- **Issue:** Walls did not extend to roof (gap at top)
- **Issue:** Windows were at ground level instead of reasonable sill height
- **Fixes:**
  - Floor footprint expanded to wall outer faces
  - Walls set to terminate at Level 3000 (roof level)
  - Windows placed at 900 mm sill height
  - Doors automatically cut into walls on family insertion

#### Iteration 3: Roof and attachment
- **Issue:** Roof overhang too large (470 mm, should be 300 mm)
- **Issue:** Gap between walls and roof (not attached)
- **Fixes:**
  - Roof overhang reduced to 300 mm
  - Added `wall.AddAttachment(roof)` for all four walls

### Verification:
- **Automated execution:** Ran on startup with empty RVT model
- **Logged output:** `/Codex/Demo02/Commands/CmdLittleHouse-report.md`
- **Model verification:** Collected created elements:
  - 2 levels ✓
  - 4 walls ✓
  - 1 floor ✓
  - 1 roof ✓
  - 4 family instances (door + 3 windows) ✓
  - Geometry correct ✓

### Outcome:
- **Pattern proven:** Second command follows same architecture, runs autonomously
- **Complexity demonstrated:** Not just trivial commands; real modeling logic works
- **Iterative refinement:** Codex-driven feedback loop (code → test → log → analyze → refine) works at scale

---

## Phase 8: Guidelines and Best Practices
### Timeframe: May 17, 2026
**Goal:** Synthesize lessons learned into actionable guidelines for AI-driven Revit development.

### Document: CLAUDE.md Guidelines
Based on Andrej Karpathy observations, four core principles:

1. **Think before coding**
   - Don't assume; state ambiguity explicitly
   - Present multiple interpretations
   - Stop and ask rather than guess
   - Example: "Should CmdLittleHouse report success via log, dialog, or marker element?"

2. **Simplicity first**
   - No features beyond what was asked
   - No "flexibility" that wasn't requested
   - Match existing code style
   - Example: CmdLittleHouse focuses on building a house, not on parametric wall placement or dynamic window counting

3. **Surgical changes**
   - Don't refactor unrelated code
   - Mention dead code, don't delete it
   - Keep diffs minimal
   - Example: Fixing roof overhang changed one numeric constant; did not restructure the entire roof generation logic

4. **Goal-driven execution**
   - Transform requirements into test cases first
   - Define success criteria upfront
   - Example: "The model must have 2 levels, 4 walls, 1 floor, 1 roof, 4 family instances, and correct geometry"

### Outcome:
- **Guidelines embedded in process:** Future work adheres to principles
- **Reduced AI hallucination:** Explicit scope prevents feature creep

---

## Phase 9: Final Integration and Workshop Preparation
### Timeframe: May 18–19, 2026
**Goal:** Integrate all learnings, prepare live demo system, and create workshop materials.

### System State:
- **Demo02 project:** Two fully autonomous commands (`CmdDemo02`, `CmdLittleHouse`)
- **Startup flow:** Revit → load add-in (digitally signed, no prompts) → execute commands → log results
- **Execution time:** ~25–30 seconds from launch to completion
- **Repeatability:** 100% deterministic when starting with empty RVT file
- **Logging:** All intermediate steps recorded for verification and audience understanding

### Workshop Live Demo Flow:
1. **Setup:** Empty Revit model ready, Revit closed
2. **Audience request:** "Show me autonomous add-in development" or specific modeling task
3. **Demonstrate:** 
   - Paste task into AI prompt (Codex or Claude)
   - Watch AI generate code, tests, logging
   - Watch system compile → sign → install → launch → verify
   - Show log files and Revit model side-by-side
   - Real-time iteration if needed
4. **Teaching moment:** Discuss how AI, signing, events, and automation combine

### Documented Artifacts:
- `/doc/2026-04-25_copilot.txt` – Initial Copilot consultation (RevitUnit discussion)
- `/doc/2026-04-29_warp_aider.txt` – Aider/Warp exploration
- `/doc/2026-04-30_codex.txt` – Codex CLI research
- `/doc/2026-05-03_title_and_agenda.txt` – Workshop planning
- `/doc/2026-05-06_codex.md` – Codex session notes (Demo02 creation)
- `/doc/2026-05-06_codex_session_transcript.txt` – Full transcript
- `/doc/2026-05-13_notes.txt` – Planning for event-driven automation
- `/doc/2026-05-14_codex_goal.jpg` – Codex goal screenshot
- `/doc/2026-05-15_codex_session_transcript.txt` – Execution refactoring session
- `/doc/2026-05-16_codex_session_transcript.txt` – CmdLittleHouse development session
- `/doc/2026-05-16_kloss_goal.md` – /goal templates for Codex workflows
- `/doc/2026-05-17_claude_guidelines.txt` – AI development principles
- `/doc/2026-05-18_claude_tips.jpg` – Claude tips screenshot
- `/Codex/AGENTS.md` – Architectural patterns and best practices

---

## Key Insights and Lessons Learned

### 1. AI Code Generation Works, But Needs Structure
- Free-form prompts → mediocre code
- **Structured prompts** (with AGENTS.md patterns) → predictable, reusable code
- Pattern library (Execute vs. Execute2, ApplicationInitialized → Idling) reduces iterations

### 2. Local Signing is Essential for Automation
- Unsigned assemblies block on Revit security dialog
- Dialog cannot be dismissed programmatically
- **Solution:** Local code-signing certificate + build-time signing = zero-interaction startup
- **Cost:** One-time setup per developer machine

### 3. Event-Driven Startup is the Key to Autonomy
- Manual command invocation requires human interaction
- ApplicationInitialized + Idling pattern → commands execute on startup
- Logging is critical for verification (since Revit UI can be hard to inspect programmatically)

### 4. Logging > Assertions for Revit Verification
- Revit API calls must happen on Revit's single-threaded context
- Cannot easily read model state from outside Revit
- **Best practice:** Commands log results to disk in human-readable format
- Workshop audience can see logs side-by-side with Revit model

### 5. Codex CLI is Powerful but Requires Discipline
- `/goal` templates (Ideation, Planning, Build) prevent hallucination
- Memory management (resume sessions, context compaction) keeps costs down
- Token usage: ~4.7M total across 3 sessions (cached), model `gpt-5.4-mini` (free tier-compatible)

### 6. Iteration Speed Matters
- May 6: Create basic add-in with logging → ~400K tokens
- May 15: Refactor into event-driven startup, fix defects → ~2.1M tokens
- May 16: Implement CmdLittleHouse with multi-iteration geometry refinement → ~4.7M tokens cumulative
- **Total time:** 4 calendar days, 3 Codex sessions, ~10–15 hours of active development

### 7. Documentation Enables Collaboration
- AGENTS.md captures "how we do things here"
- Patterns prevent tribal knowledge
- New developers can reference AGENTS.md and produce consistent code
- AI agents (Codex, Claude) can read AGENTS.md and follow conventions

---

## Looking Forward: Workshop Narrative

### Part 1: History (15 min)
- Autodesk employment (2005)
- The Building Coder blog (2008)
- Revit API discussion forum (2010)
- Retirement (June 2025)

### Part 2: The Catalyst (15 min)
- February 2026: HVAC MEP system generation with Claude Opus 4.6
- Manual loop: (prompt → code generation → launch Revit → test manually → iterate)
- Insight: AI can generate code, but humans drive the loop

### Part 3: The Vision (10 min)
- What if AI could drive the entire loop?
- Zero human intervention: compile → sign → install → launch → test → verify
- Building blocks: Codex, digital signing, Revit events, logging

### Part 4: Live Demo (40 min)
- Show the working system (Demo02, CmdLittleHouse)
- Explain architecture (AGENTS.md patterns)
- If time: Take audience request, generate code live, watch it run

### Part 5: Reflection (10 min)
- Lessons learned
- Where AI excels (boilerplate, testing, iteration)
- Where humans are still essential (requirements, architecture, judgment)
- Future: Agents, reliability, governance

---

## Conclusion

Between February and May 2026, this project demonstrated that **autonomous AI-driven Revit add-in development is technically feasible and repeatable**. The key enablers are:

1. Structured AI prompts (with pattern libraries like AGENTS.md)
2. Digitally signed assemblies (eliminate Revit trust dialogs)
3. Event-driven startup sequences (eliminate manual command invocation)
4. Comprehensive logging (enable automated verification)
5. Disciplined tool usage (Codex /goal templates, memory management)

The workshop will demonstrate these principles in action, showing a live audience how an AI agent can take a request, generate code, compile, sign, install, launch Revit, execute commands, and verify results—**all without human intervention**.

The question is no longer "Can AI write Revit add-ins?" but rather "What should humans do when AI writes Revit add-ins?"

---

## Appendix: Timeline Summary

| Date | Milestone | Key Achievement |
|------|-----------|-----------------|
| 2026-02-XX | HVAC MEP System (Claude Opus) | Manual loop: prompt → code → test → iterate |
| 2026-04-25 | Copilot consultation | Identified RevitUnit for automated testing |
| 2026-04-29 | Warp/Aider exploration | Evaluated agentic frameworks |
| 2026-04-30 | Codex CLI research | Selected Codex as primary automation tool |
| 2026-05-03 | Workshop planning | Defined title, agenda, learning objectives |
| 2026-05-06 | Codex session #1 | Created Demo02 with logging (396K tokens) |
| 2026-05-13 | Startup automation planning | Planned event-driven refactoring |
| 2026-05-15 | Codex session #2 | Implemented Execute2, ApplicationInitialized, Idling (2.1M cumulative tokens) |
| 2026-05-15 | Code signing | Added local certificate, unattended startup |
| 2026-05-16 | Codex session #3 | Implemented CmdLittleHouse with multi-iteration refinement (4.7M cumulative tokens) |
| 2026-05-16 | Architecture review | Created AGENTS.md pattern library |
| 2026-05-17 | Guidelines | Synthesized Claude.md best practices |
| 2026-05-19 | Workshop prep | Final integration, live demo readiness |

---

**End of Document**

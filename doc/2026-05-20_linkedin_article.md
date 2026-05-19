# From Programmer to AI Orchestrator: Chronicles of Autonomous Revit Development

For 20 years, I answered the same Revit API questions over and over. Then, in February 2026, everything changed.

## The Catalyst

A client needed HVAC system generation from JSON. I procrastinated for a month. Then Claude Opus 4.6 arrived, and I had access through GitHub Copilot.

The result? I generated working code in 3-4 days. But every iteration required me to manually launch Revit, click buttons, inspect the model, and tell Claude what to fix.

**The insight hit me:**

What if the AI could verify itself? What if the entire loop—from code generation to compilation to verification—ran without human intervention?

## The Four Building Blocks

Over 4 months (Feb-May 2026), I experimented with autonomous Revit add-in development. It turns out, closing the loop requires just four things:

1. **AI Code Generation** – Claude, Codex, or similar models can write solid Revit API code
2. **Digital Signing** – A local certificate eliminates Revit's trust dialog (the single biggest blocker)
3. **Revit Events** – `ApplicationInitialized` + `Idling` allow commands to execute automatically on startup
4. **Comprehensive Logging** – Timestamped files are the "eyes" an autonomous system uses to verify results

## Proof: CmdLittleHouse

I built a real test case: an AI-generated command that models a complete house (2 levels, 4 walls, floor, roof, doors, windows) entirely unattended.

**The flow:**
- Codex generates code → Compile → Sign → Install → Launch Revit (command-line) → Execute automatically → Write logs → Verify success

**Time: ~30 seconds. Zero human button-clicks.**

Three iterations. Multiple refinements. All driven by AI reading the logs and responding to failures.

## The Real Question

We spent decades asking: *"Can AI write code?"*

The answer is yes. But that's not the interesting question anymore.

The real question is: **"What do humans do when AI writes Revit add-ins?"**

The answer: We define problems. We create patterns. We interpret edge cases. We make judgment calls. We ensure governance.

AI does the rest.

## What's Next?

This isn't theory. The working system—Demo02, CmdLittleHouse, architecture patterns, and documentation—is on GitHub.

**Full details, source code, slides, and chronological timeline:**
→ https://github.com/jeremytammik/eubim

I've documented the journey, the patterns (AGENTS.md), and the lessons learned. Everything is reproducible.

The future of BIM development isn't "AI replaces programmers." It's "humans + AI collaborate to build smarter."

**Try it. Experiment. Share what you build.**

---

#AI #BIM #Revit #AutomatedDevelopment #ArchTech #RevitAPI #AGI #BuildingCoder

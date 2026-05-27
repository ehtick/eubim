/home/jeremy/w/src/eubim/doc/2026-05-27_linkedin_article.md

https://www.linkedin.com/pulse/end-manual-revit-programming-autonomous-ai-loop-rewrites-tammik-iepzf

# The End of Manual Revit Programming? Autonomous AI Loop Rewrites the Code

From coding by hand to orchestrating AI agents

## Manual Loop

In February 2026, I needed to build an MEP HVAC system generator for Revit from external data. Normally, that would have meant days of manual Revit API work. Instead, I tried something different: I handed the task to an LLM.

Luckily, Claude Opus 4.6 had just been released. I never wrote a line of the production code myself.

Claude generated the code. I compiled it, launched Revit, tested the add-in, reviewed results and suggested improvements. My role shifted from programmer to supervisor.

This experience prompted a question that haunts (or ought to) every Revit programmer today: 
*What if AI could close the entire development loop without human intervention?*

That first week in February, I spent 3–4 days in what I'll call a "manually driven AI loop." Here's how it worked:

1. Describe what I want in natural language
2. Claude generates C# Revit API code
3. Manually compile, launch Revit, launch add-in, observe visual results
4. Describe problems, suggest improvements, make debugging, logging, diagnosis suggestions to Claude
5. Claude regenerates code based on my feedback and its own diagnosis of logs
6. Repeat

After exhausting my February $12 GitHub Copilot quota (293 requests), I had a fully working HVAC system import/export system. But the friction was unmistakable: *every iteration required me to be the human verification layer*. I had to see what the code did, interpret failures, and guide the AI forward.

How can I eliminate myself from this loop entirely?

## Vision: Zero Human Intervention

Since then, I ran some experiments on a fully autonomous Revit add-in execution loop. Not "AI helps humans code faster." Not "AI as a copilot." I mean *full autonomy: code generation → compilation → signing → installation → launch → execution → verification, without a single human button press*.

Initially, I explored existing Revit add-in unit testing frameworks, including GitHub projects by ricaun and Nice3point. However, I am a hopeless DIY fanatic, so I did not try that tack hard enough. Instead, I rolled my own from first principles.

The architecture rests on four pillars:

### 1. AI Code Generation (Codex CLI)

OpenAI's Codex generates code from prompts. The trick is consistency — not free-form instructions, but adherence to patterns documented in AGENTS.md. Reliability comes from structure, not creativity.

### 2. Digital Code Signing

One blocker is the digital signature: unsigned Revit add-ins trigger a user dialog asking "Load Always / Load Once / Do Not Load?" You can't automate that away. The solution: sign the assembly at build time using a local certificate. Zero prompts. Revit loads the add-in silently.

### 3. Revit Events (ApplicationInitialized, Idling)

Autonomous systems cannot click UI buttons. Instead of relying on external commands triggered by the user, the add-in subscribes to ApplicationInitialized and Idling events to execute automatically once a document is ready.

### 4. Logging as the AI's Eyes

An autonomous system has no eyes. It can't see the Revit window or inspect the BIM model visually. Logs are its vision. Every phase — OnStartup, ApplicationInitialized, Idling, Execute2 — writes timestamped, human-readable logs. The logs provide the AI’s sensory system: execution trace, diagnosis channel, and feedback loop.

## Complete Loop

Here's what a full autonomous cycle looks like:

Prompt → Code Generation → Compilation → Signing → Installation → Revit Launch → Autonomous Execution → Log Analysis → Iteration

No human required.

## CmdLittleHouse: A Case Study in Autonomous Modeling

To prove this works I let the AI build the CmdLittleHouse sample command, very similar to my own first baby steps building the little house in 2008, cf. 

https://jeremytammik.github.io/tbc/a/0014_wall_dimensions.htm

The command constructs a complete little house with walls, floors, roof, doors, windows, a room defined, and proper geometric relationships, entirely from AI-generated code running in a fully autonomous loop.

The development unfolded in iterations:

- Iteration 1: Generated code with basic geometry. Family lookups failed. Logs showed the error.
- Iteration 2: Codex refined the family loading logic. Walls, floor, and windows materialized.
- Iteration 3: Roof geometry needed fixing. Codex adjusted overhang values based on log feedback.

The iterations were not entirely frictionless, but this initial experimental loop operates with little human intervention.

## What This Means

For Revit developers: The question is no longer "Can AI write Revit add-ins?" We proved it can, in a weekend. The real question is: what becomes valuable when code generation is effectively automated?

The value of developers shifts upward. Humans define problems, establish architecture, evaluate tradeoffs, and handle ambiguity. AI handles iteration, boilerplate, and refinement.

For BIM workflows: Autonomous coding loops enable rapid, hands-off prototyping and testing of Revit extensions. Need a custom command? Describe it. Get a working add-in in minutes.

## What's Next?

I documented my experiments, prompts, logs, and architectural patterns in AGENTS.md and published the code on GitHub. The experiment is repeatable.

Interestingly, all of this work was done using free tooling tiers and older models rather than premium subscriptions. Claude Opus 4.6 work in February was via free GitHub Copilot, Codex experiments used `gpt-5.4-mini low`. The capability ceiling is already surprisingly high.

The invitation is simple: *¡Pruébalo!*, try it!

---

Jeremy Tammik is a former Autodesk technology evangelist and the author of The Building Coder:

https://jeremytammik.github.io/tbc/a/  

He presented these experiments at EUBIM 2026 in Valencia:

https://www.eubim.com/

The code and more details are open-source on GitHub:

https://github.com/jeremytammik/eubim



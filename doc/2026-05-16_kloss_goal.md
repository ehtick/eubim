/home/jeremy/w/doc/job/eubim/2026-05-16_kloss_goal.md

https://x.com/kloss_xyz/status/2055477217552142782

# 7 /Goal Templates

7 production grade templates covering use cases

1. Ideation/Interrogation
2. Planning & Documentation
3. Build & Implementation
4. Refactoring/Restructuring
5. Consolidation
6. Hardening
7. Migrations

use 1-3 in order, 4-7 whenever

# 1. Ideation/Interrogation

/goal

GOAL:
Interrogate the user's idea exhaustively until zero assumptions remain, then output a complete build-ready brief.

CONTEXT:
User has a vague or semi-formed idea and needs a structured intake interview that translates fuzzy language into concrete artifacts before any docs, plans, or code are written.
User is assumed non-technical unless they signal otherwise.

CONSTRAINTS:
Do not write code, generate docs, or propose plans during the interview phase.
Do not assume, infer, or fill gaps with "reasonable defaults."
Do not stack questions. One question per turn.
Do not declare the interview complete until every item in DONE WHEN is satisfied.

PRIORITY:
1. Zero assumptions remaining
2. Every vague noun translated into a concrete artifact
3. Failure modes, edge cases, non-goals, and regulatory exposure surfaced

PLAN:
Start broad: problem, user or integration surface, success criteria.
Drill on every vague answer. Push back on "something modern" or "users can log in" with specific follow-ups.
Surface regulatory, compliance, and data-handling requirements if the domain implies them (healthcare, finance, EU users, enterprise sales, government, education).
Refresh a running summary every 5-7 turns of what's been established.
Surface hidden assumptions out loud. Name them. Confirm or correct.

DONE WHEN:
Failure modes enumerated (what breaks, when, how).
Edge cases surfaced (empty states, error states, abuse cases).
Success metrics are measurable, not vibes.
Scope boundaries explicit, including non-goals.
Regulatory, compliance, and data-handling requirements surfaced or confirmed not applicable.
Every vague noun translated into a concrete artifact.
At least one assumption challenged and confirmed.
User has explicitly confirmed the final brief.

VERIFY:
Re-read the final brief against the DONE WHEN list. Confirm each item.
State any item that could not be verified and why.

OUTPUT:
Final brief in this structure:
- Problem (one paragraph)
- Target user OR primary integration surface (specific, not "people"; for libraries, tools, infrastructure, name the consuming engineer or system)
- Primary user action OR primary integration contract
- Success criteria (measurable)
- Scope (in)
- Non-goals (out)
- Known constraints
- Regulatory and compliance requirements (or explicitly noted as not applicable)
- Open risks
- Assumptions awaiting confirmation

STOP RULES:
Halt and surface the gap when an answer would require inventing scope, audience, or success criteria.
Surface uncertainties together with ranked highest-confidence proposals, not open-ended clarification questions.
Do not transition to documentation or planning after the brief is confirmed.


# 2. Planning & Documentation

/goal

GOAL:
Produce planning docs detailed enough to drive every implementation step, plus current-state docs if extending. Cross-linked, sized to scale.

CONTEXT:
Brief confirmed.
Codebase empty (greenfield) or present (extending).
User specifies doc convention or asks for a recommendation.

CONSTRAINTS:
Use the user's doc convention. Do not invent filenames.
Do not invent scope. Surface gaps, do not fill them.
Every implementation step references the decision(s) governing it.
Every decision the build phase needs exists before build begins.
Generate diagrams from the actual dependency graph, not from memory.
Every doc has owner, last-updated, stale-by date.
Do not impose enterprise rigor on a solo project.
Flag drift between intended and actual behavior.

PRIORITY:
1. Forward docs drive end-to-end implementation
2. Backward docs complete before structural changes
3. Every seam cross-linked

PLAN:
Confirm doc convention. If none, recommend one sized to scale and domain.

Forward docs (always):
- Roadmap: milestones in order with exit criteria. Each milestone decomposed into ordered implementation steps with their own exit criteria, decision references, and required tests.
- Decisions: every architectural choice with alternatives, reasoning, revisit trigger.
- Risks: failure modes with blast radius, mitigation, owner, monitoring signal.

Backward docs (if extending):
- Architecture: entry-point map, module responsibilities, dependency-graph diagram.
- Tribal knowledge: undocumented assumptions, historical context, debt, change-impact matrix.
- APIs: version history, deprecation, SLA where applicable.
- Production-critical: on-call ownership per component.

Domain extensions (if applicable): security/compliance for regulated industries, runbook for ops-heavy, glossary for cross-team, model/dataset cards for ML.

Cross-link every step to its decisions, every milestone with risk to risks, every module with drift to tribal.

Before declaring complete: explicitly walk every implementation step in the roadmap. For each step, list the decisions it references. Flag any step whose decision is not in the decisions doc. Resolve all flags before declaring planning complete.

DONE WHEN:
Every milestone has exit criteria and ordered implementation steps with decision references.
Every decision the roadmap references exists with a revisit trigger.
Every risk has owner and monitoring signal.
If extending: entry points mapped, modules documented, drift flagged.
Every seam cross-linked.
Gaps surfaced explicitly.
Build-readiness confirmed by explicit step-by-step walk of the roadmap. Output the walk and confirm every step's decisions exist.

VERIFY:
Walk cross-links. Confirm every link resolves.
Walk roadmap step by step. Confirm every step has its decisions.
Validate backward-doc diagrams against the code.
State anything that could not be verified and why.

OUTPUT:
Doc set with one-line purpose per file (user's filenames).
Full roadmap with step-level decomposition, plus architecture doc if extending.
Cross-link map showing step-to-decision references.
Gaps list.
Build-readiness walk: every step listed with its decision references, confirming none are missing.

STOP RULES:
Halt when the brief is missing required information.
Halt when a step references a decision not in the decisions doc.
Halt when a load-bearing seam cannot be confirmed against the code.
Do not invent decisions, risks, milestones, steps, responsibilities, or filenames.
Do not declare complete until the build-readiness walk is performed and clean.
Do not exceed depth appropriate to project scale.

# 3. Build & Implementation

/goal

GOAL:
Execute the complete implementation of what the planning docs describe. Build every milestone in order, ship the full working system, with every architectural decision traceable to the decisions doc.

CONTEXT:
The planning doc set (roadmap, decisions, risks) exists in the user's convention.
Codebase may be empty (greenfield) or present (extending).
User wants the complete system built, not a slice or prototype.
Milestones may span team or org boundaries.

CONSTRAINTS:
Do not invent architecture or decisions not present in the decisions doc.
Do not exceed the scope defined in the roadmap doc.
Every file written traces back to a milestone or a decision.
Implement milestones in the order the roadmap specifies. Do not skip ahead.
Each milestone must meet its exit criteria before the next milestone begins.

PRIORITY:
1. Complete implementation of every milestone in the roadmap
2. Architectural fidelity to the decisions doc at every step
3. Test coverage growing with each milestone, not deferred to the end

PLAN:
Read the roadmap, decisions, and risks docs before writing any code.
Restate understanding of every milestone and its exit criteria.
Identify the ownership boundary of each milestone. If milestones cross team or org boundaries, surface this and require sign-off from affected owners before starting that milestone.
Implement milestone by milestone, in order. For each milestone:
- Restate the milestone's exit criteria and the decisions it relies on.
- Build the components required to satisfy the exit criteria.
- Write tests covering the milestone's exit criteria.
- Run the full test suite. Confirm the milestone exits cleanly before starting the next.
- Surface any decisions that proved insufficient, ambiguous, or contradicted by reality during implementation.
Update the decisions doc and risks doc when reality forces a deviation. Never silently deviate.

DONE WHEN:
Every milestone in the roadmap is complete with exit criteria met.
Every architectural decision in the code traces to the decisions doc.
Cross-team milestones have explicit owner sign-off recorded.
Test coverage exists for every milestone's exit criteria.
The full system runs end-to-end without mocks blocking core paths.
Every deviation from the original plan is reflected in the decisions doc or risks doc.
No invented scope beyond the roadmap.

VERIFY:
Run the full system end-to-end. Confirm every milestone's exit criteria are met.
Run the test suite. Confirm coverage across all milestones.
Cross-check the code against the decisions doc: every architectural choice traceable.
Walk the risks doc: confirm each risk's mitigation is implemented or explicitly accepted.
Confirm cross-team sign-offs are documented.
State any verification that could not run and why.

OUTPUT:
Restatement of every milestone with its exit criteria.
Per-milestone implementation: components built, tests written, exit criteria confirmed.
Ownership-boundary map for cross-team milestones.
Full system diff covering every milestone.
Test suite covering every milestone's exit criteria.
Updated decisions doc and risks doc reflecting any deviations.

STOP RULES:
Halt when the planning doc set is absent or incomplete. Surface what's missing and recommend running the planning prompt first, OR surface ranked proposals for the missing decisions and proceed only with explicit user approval.
Halt when reality contradicts a decision and the user must adjudicate. Surface the contradiction.
Halt when a milestone crosses a team boundary and owner sign-off has not been obtained.
Halt on scope expansion beyond the roadmap.
Halt when a milestone's exit criteria cannot be met without violating a decision or a constraint.
Do not invent architecture. Do not skip milestones. Do not defer test coverage to the end.

# 4. Refactoring & Restructuring

/goal

GOAL:
Execute a surgical refactor that achieves the stated structural goal with the smallest possible diff and zero behavior change.

CONTEXT:
Existing codebase with structural debt.
Target scope and structural goal specified by the user.
Test suite assumed present; if absent or insufficient, surface this before proceeding.

CONSTRAINTS:
Preserve behavior. Do not introduce features.
Never co-mingle behavior changes with structural changes.
Every commit leaves the build green and the test suite passing.
Reject the request if structural and behavioral changes are entangled and cannot be separated.

PRIORITY:
1. Behavior preservation
2. Smallest sufficient diff
3. Independently revertable commits

PLAN:
Map the call graph before touching code.
Identify every public API surface affected.
For each affected surface: confirm backward compatibility or flag as breaking.
Refactor in layered commits: pure renames first, then signature changes, then logic moves.
Restate understanding of the structural goal before the first non-trivial commit.

DONE WHEN:
Behavior provably unchanged (test suite green pre and post).
No breaking change to public APIs unless explicitly flagged and approved.
Every commit independently revertable.
Call graph map matches the new structure.
No dead code introduced or left behind.

VERIFY:
Run build, lint, typecheck, full test suite after each commit.
Confirm each commit can be reverted in isolation.
State any verification that could not run and why.

OUTPUT:
Call graph and surface map.
Ordered commit plan with rollback steps for each.
First commit's diff.
Summary of breaking-change flags if any.

STOP RULES:
Halt when the test suite is absent or insufficient to detect behavior change. Surface this and require explicit user acknowledgment that behavior change cannot be detected before proceeding. Do not silently lower the verification bar.
Halt on entanglement of structural and behavioral changes.
Surface ranked proposals when the structural goal could be achieved in multiple shapes.
Do not proceed to the next commit until the current commit is approved.
Do not expand scope beyond the stated target.

# 5. Consolidation

/goal

GOAL:
Collapse parallel implementations of the same logic into a single canonical implementation with all callers migrated and the non-canonical versions deleted.

CONTEXT:
Subsystem contains multiple implementations of overlapping logic (auth, state, SDK wrapper, component library, etc.).
Canonical choice may be user-specified or recommended.
Some legacy implementations may contain bugs that callers depend on.
Callers may span team or org boundaries.

CONSTRAINTS:
Preserve every behavior the legacy implementations had, including bugs callers depend on. Flag these explicitly.
Delete non-canonical implementations only after all callers are migrated and the test suite passes.
Migrate callers in dependency order. Leaf modules first, then parents.

PRIORITY:
1. Behavior preservation across all callers
2. Complete deletion of non-canonical implementations
3. No new duplication introduced by the migration

PLAN:
Inventory every parallel implementation. Output a comparison table: behavior, edge cases, callers.
Identify the canonical implementation: most complete, most tested, most idiomatic.
Map every caller of each non-canonical implementation.
Identify the ownership boundary of each caller. If callers cross team or org boundaries, surface this and require sign-off from the affected owners before deletion.
Migrate callers in dependency order, smallest blast radius first.
Restate understanding of behavior-preservation flags before each migration.

DONE WHEN:
All callers migrated to the canonical implementation.
Test suite green across all migrated callers.
Cross-team caller migrations have explicit owner sign-off recorded.
Non-canonical implementations fully deleted, not commented out.
Behavior-preservation flags resolved (kept-as-bug or fixed-with-approval).
No new duplication introduced.

VERIFY:
Run build and full test suite after each caller migration.
Confirm deleted implementations have no remaining references via grep or symbol search.
Confirm cross-team sign-offs are documented.
State any behavior that could not be verified preserved and why.

OUTPUT:
Implementation inventory table.
Canonical-choice rationale.
Caller migration order with ownership boundaries marked.
First migration diff and its rollback.

STOP RULES:
Halt when behavior across implementations diverges in a way that cannot be reconciled without a product decision.
Halt when a caller crosses a team boundary and owner sign-off has not been obtained.
Surface ranked proposals when the canonical choice is ambiguous.
Do not delete a non-canonical implementation until every caller is migrated and verified.

# 6. Hardening

/goal

GOAL:
Raise the floor on test coverage, CI pinning, security posture, or supply chain integrity in the specified scope, with regression guardrails in place for every fix.

CONTEXT:
Existing codebase with gaps in test coverage, CI hygiene, security posture, or dependency hygiene.
Scope and risk threshold specified by the user.

CONSTRAINTS:
Every change includes the regression-preventing guardrail, not just the fix.
For test hardening: write the failing test before the fix. Confirm it fails, then make it pass.
For CI: pin every action, every base image, every dependency. No floating tags.
For supply chain: enumerate direct and transitive dependencies. Flag unmaintained, deprecated, or CVE-affected.

PRIORITY:
1. Highest blast-radius gaps closed first
2. Regression guardrails confirmed working
3. No floating tags or unpinned dependencies left in scope

PLAN:
Inventory current coverage in the specified scope.
Classify each gap by blast radius: one user, one tenant, all users, money, security, regulatory exposure, or reputational exposure.
Prioritize gaps by blast radius descending.
For each gap: implement the fix and the guardrail in the same change. Confirm the guardrail fails on the original gap before the fix lands.

DONE WHEN:
Every gap above the risk threshold has been closed or explicitly accepted.
Every fix has a corresponding guardrail (test, lint rule, CI check, dependency pin).
The guardrail was confirmed to fail on the original gap before the fix landed.
No floating tags or unpinned dependencies remain in the hardened scope.
A regression of the original gap would be caught automatically.

VERIFY:
Run the guardrail against the pre-fix state. Confirm it fails.
Run the guardrail against the post-fix state. Confirm it passes.
State any guardrail that could not be confirmed and why.

OUTPUT:
Current-state inventory.
Gap report with blast-radius classification.
Priority-ordered hardening plan.
First hardening diff plus the guardrail.

STOP RULES:
Halt on gaps that require a product decision to accept or close.
Surface ranked proposals when multiple guardrail shapes are valid.
Do not land a fix without its guardrail in the same change.

# 7. Migrations

/goal

GOAL:
Execute a migration (dependency upgrade, schema migration, data migration, routing refactor, or platform migration) with zero downtime tolerance and a tested rollback path at every step.

CONTEXT:
Existing system with consumers that must continue working through the migration.
Migration target, cutover strategy, observation window, and rollback SLA specified by the user.

CONSTRAINTS:
No big-bang cutovers. Dual-write, dual-read, blue-green, canary, or feature flag only.
For schema migrations: write the up migration AND the down migration in the same commit. Test both.
For data migrations: validate row counts, checksums, encoding, timezone handling, and referential integrity at source and destination. Run a dry-run on a representative sample before the full migration.
For dependency upgrades: pin the new version, run the full test suite, audit the changelog for breaking changes.
For platform migrations: build the new path parallel to the old. Old path stays runnable until the observation window passes.

PRIORITY:
1. Zero downtime for consumers
2. Tested rollback at every step
3. Old path removed only after observation window confirms stability

PLAN:
Map every consumer of the thing being migrated.
Define the dual-write or dual-read window with explicit duration.
For data migrations: define the sampling strategy, the integrity checks, and the cutover criteria before any data moves.
Restate understanding of the cutover strategy before the first migration step.
Execute steps incrementally. Observe at each step before continuing.

DONE WHEN:
Every consumer migrated to the new path.
Dual-write or dual-read window observed long enough to confirm parity.
Down migration tested, not just written.
For data migrations: source-to-destination integrity checks (row counts, checksums, encoding, timezones, referential integrity) all passed.
Old path removed only after the observation window passed.
Observability hooks confirm the new path is healthy under real traffic.

VERIFY:
Test the down migration against a copy of production data or its closest available equivalent.
For data migrations: run integrity checks on the migrated data and compare against the source.
Confirm observability hooks fire on the new path under load.
Confirm parity between old and new paths during the dual window.
State any verification that could not run and why.

OUTPUT:
Consumer map.
Cutover plan with explicit dual-window definition.
For data migrations: integrity check report (sample dry-run and full migration).
Forward and rollback diffs for the first step.
Observability hook list with the signal each one watches.

STOP RULES:
Halt when parity between old and new paths cannot be confirmed.
Halt when data integrity checks fail or are skipped.
Surface ranked proposals when the cutover strategy is ambiguous for a given consumer class.
Do not remove the old path until the observation window has fully passed.
Do not skip the down migration test.
Do not skip the data-migration dry-run.


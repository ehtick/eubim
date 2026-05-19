# AI and BIM Programming at EUBIM 2026 Valencia

Experiments in BIM Programming with AI.

## ¿El fin del programador de Revit? <br/>Crónica de experimentos en codificación autónoma 

Sinopsis: En esta sesión de 90 minutos, Jeremy Tammik narra su transición de programador a orquestador de IA. La crónica comienza en febrero de 2026, tras desarrollar un add-in complejo de MEP sin escribir una sola línea de código personalmente. El núcleo de la charla explora experimentos con frameworks de codificación autónoma y unit testing que utilizan agentes de IA para generar, testear y corregir código en tiempo real. Analizaremos cómo la instrumentación adaptativa permite a la IA observar el entorno transaccional de Revit, convirtiendo un ecosistema cerrado en una plataforma de desarrollo autodidacta y determinista.

## Rough Agenda Draft

- interest in 2017 --> Q4R4
- show revitqc.com --> governance
- bimrock procrastination --> cost $12
- fortuitous timing
- human interaction
- agents --> cost of AI versus cost of human intervention
- reliability? governance! --> cost
- terrascape
- little house: create with 2 storeys?
- pruebalo! gather own experience

![Rough agenda draft](img/agenda.jpg)

## Agenda

1. Autodesk and The Building Coder
2. Q4R4 and the Revit API discussion forum
3. Retirement
4. HVAC MEP system generation with Claude Opus 4.6
5. Terrascape
6. Today EUBIM demo
7. Tomorrow up to you and your agents

## 1. Autodesk and The Building Coder

2005 Autodesk
2008 The Building Coder
2010 Revit API discussion forum

Employed as technology evangelist, conferences, Autodesk developer support, cf. details in bio.

## 2. Q4R4 and the Revit API discussion forum

Repetitive questions, thoughts on automation, Google translate getting better, but natural language comprehension still tricky.

Intelligent flexible search engine, versus
Machine learning, versus
Deep learning

Out of scope for me, still decdicated to providing support and answering questions, but some posts on Q4R4:

- <a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/1536_q4r4.md">1536</a> The Revit API Question Answering system Q4R4
- <a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/1539_q4r4_lookup.md">1539</a> Q4R4 question sources, GitHub repo, <code>tbcimport.py</code> script, result presentation
- <a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/1688_that_bim_girl.md#3">1688</a> Notes to Self on AskNow for Q4R4
- <a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/2047_aps_accel_vacat.md#3">2047</a> Q4R4 Chunking with Claude, using LLM and RAG
- <a href="https://github.com/jeremytammik/tbc/blob/gh-pages/a/2060_modeless_tutor.md#4">2060</a> ChatGPT for Q4R4

add links and dates

## 3. Retirement

Retired in June 2025. 

Last event: 

DevCon Amsterdam 2025, thank you and bye bye!

Photo

## 4. MEP HVAC system generation, Claude Opus 4.6

February 2026, after month-long procrastination, with very fortuitous timing:

MEP HVAC system generation using Claude Opus 4.6

Demo:

- MEP HVAC system export to JSON
- MEP HVAC system import from JSON

## 5. Terrascape

- [Terrascape](https://terrascape.ai) technical advisor on Revit API
- [Sentinel QC](https://www.revitqc.com/) automates QA/QC controls and implements governed write access to Revit with preview, atomic rollback, and audit receipts on every change. No AI touches the model. Deterministic tools do the work.

## 6. Today EUBIM demo

I asked GitHub Copilot using Haiku 4.5 (old, cheap) to read this repo to generate a chronological outline and a slide deck for my EUBIM workshop.

- [My prompt and session notes](/doc/2026-05-19_copilot.txt)
- [Chronological outline](/doc/2026-05-19_autonomous_revit_addins_chronology.md)
- [Slide deck](/doc/2026-05-19_eubim_workshop_slides.html)
  &ndash; [render](https://htmlpreview.github.io/?https://github.com/jeremytammik/eubim/blob/main/doc/2026-05-19_eubim_workshop_slides.html)

## 7. Tomorrow is up to you and your agents

What I have tested and used:

- GitHub Copilot
- Codex

Colleagues recommended looking at:

- Cline
- Cursor

Larger more powerful autonomous agent frameworks:

- Hermes
- OpenClaw

Current research emphasises:

- Harness


## License

## Author

https://jeremytammik.github.io/tbc/a/


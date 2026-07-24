# ContractProcessor — New Session Context

## Quick Start for New Agent

**Read these files first (in order):**
1. `AGENTS.md` — Current project state, what's done, what's next
2. `CHANGELOG.md` — Full change history
3. `README.md` — Project overview with extraction evolution table
4. Source code in `ContractProcessor/` — C# WinForms + SQLite + Ollama/OpenRouter

---

## Project Summary

**ContractProcessor** — Desktop app (C# .NET 8 WinForms + SQLite) for extracting data from Moroccan insurance PDF contracts (Allianz/Sanlam). Offline-first, single user, no auth.

**Target org:** Small Moroccan insurance company processing AT (Accident Travail), AUTO, MRH (Multirisque Habitation) contracts.

---

## Tech Stack

| Component | Technology |
|-----------|------------|
| Framework | .NET 8 WinForms |
| UI | Guna UI 2.0 |
| Database | SQLite (Microsoft.Data.Sqlite) |
| PDF | PdfPig |
| Export | ClosedXML (Excel), CsvHelper (CSV) |
| Local AI | Ollama + llama3.2 (working, ~70% accuracy) |
| Cloud AI | **OpenRouter (implemented v0.8.0)** — `meta-llama/llama-3.1-8b-instruct`, `mistralai/mistral-7b-instruct`, etc. |
| Repo | https://github.com/YounesJY/ContractProcessor |

---

## Current State (v0.8.0)

### ✅ Completed
- PDF text extraction (PdfPig) with cleaning
- SQLite database (contracts, categories, settings)
- Auto category detection (AT/AUTO/MRH)
- Duplicate detection (SHA256)
- Export to CSV/Excel with field selection
- Guna UI 2.0 on all forms
- Manual extraction form (split-view fallback)
- Settings: root folder, dynamic categories
- **Local AI (Ollama + llama3.2)** — working end-to-end
  - `OllamaService.cs` — HTTP client for localhost:11434
  - `ExtractionService.cs` — AI first, regex fallback
  - Settings: AI toggle, model dropdown (llama3.2/mistral/phi3/gemma2)
  - ~70% accuracy, ~2 min/PDF, 2.5 GB RAM
  - Regex fallback when Ollama unavailable
- **Cloud AI (OpenRouter)** — implemented as fallback
  - `OpenRouterService.cs` — HTTP client for openrouter.ai/api/v1
  - Settings: Cloud AI toggle, API key input, model dropdown
  - Fallback chain: Ollama → OpenRouter → Regex
  - ~80-90% accuracy, ~5s/PDF
  - Settings apply immediately (no restart)
- Debug logging (`debug.log`)
- `AGENTS.md`, `CHANGELOG.md`, `test_ollama.ps1`

### ⚠️ Known Issues
- **Regex (FieldExtractor)** — failed, jumbled PDF text breaks patterns
- **Local LLM** — 70% accuracy ceiling (2B params too small), 2 min/PDF, 2.5 GB RAM
- **Cloud AI** — requires internet + API key + credits (~$0.002/PDF for GPT-4o-mini)
- **Missing fields** — Police Num often null, dates sometimes swapped
- **Encoding** — fixed UTF-8 corruption in LLM responses

---

## Extraction Evolution

| Phase | Approach | Accuracy | Status |
|-------|----------|----------|--------|
| 1 | Regex | ~30% | ❌ Failed |
| 2 | Local Ollama (llama3.2) | ~70% | ✅ Working but slow |
| 3 | **OpenRouter (cloud)** | **80-90%+** | ✅ **Implemented** |

---

## Key Files to Know

```
ContractProcessor/
├── Services/
│   ├── OllamaService.cs          # Local AI (working)
│   ├── OpenRouterService.cs      # Cloud AI (implemented v0.8.0)
│   ├── ExtractionService.cs      # Orchestrator: Ollama → OpenRouter → Regex
│   ├── FieldExtractor.cs         # Regex fallback (inaccurate)
│   ├── PdfProcessor.cs           # PDF text extraction
│   └── DebugLogger.cs            # debug.log output
├── Models/AppSettings.cs         # UseAI, AIModel, UseCloudAI, OpenRouterApiKey, OpenRouterModel
├── Forms/SettingsForm.cs         # AI toggles, model dropdowns, API key input
├── Forms/MainForm.cs             # Recreates ExtractionService on settings change
└── test_ollama.ps1               # Direct Ollama testing
```

---

## Environment Setup (for new session)

1. **Ollama must be running:** `ollama serve` (or auto-started)
2. **Model pulled:** `ollama pull llama3.2` (2 GB)
3. **OpenRouter account:** https://openrouter.ai (free tier available)
4. **Build:** `dotnet build ContractProcessor/ContractProcessor.csproj`
5. **Run:** F5 in Visual Studio or `dotnet run`

---

## Testing Commands

```powershell
# Test Ollama directly
.\test_ollama.ps1

# Check debug log
Get-Content "bin\Debug\net8.0-windows\debug.log"

# Build
dotnet build ContractProcessor/ContractProcessor.csproj
```

---

## Decision Needed After Testing

| Option | Accuracy | Cost | Effort |
|--------|----------|------|--------|
| Keep local only | 70% | Free | Done |
| OpenRouter free-tier models | 80-90% | Free tier | Done |
| OpenRouter paid (GPT-4o-mini) | 95%+ | ~$0.002/PDF | Low |
| Azure Document Intelligence | 98% | ~$0.05/PDF | Medium |

---

## Prompt for New Agent

> "Continue from AGENTS.md. Project: ContractProcessor (C# WinForms + SQLite).
> 
> Current state: Local Ollama (~70%, 2 min/PDF) + OpenRouter cloud fallback (~85%, 5s/PDF) both working. Fallback chain: Ollama → OpenRouter → Regex.
> 
> Task: Evaluate if accuracy is sufficient for production, or test GPT-4o-mini for 95%+. Consider deployment.
> 
> Read AGENTS.md, CHANGELOG.md, then check ExtractionService.cs for fallback logic."

---

## Contact / Resources

- **Repo:** https://github.com/YounesJY/ContractProcessor
- **Ollama:** https://ollama.com
- **OpenRouter:** https://openrouter.ai
- **Working models:** `meta-llama/llama-3.1-8b-instruct`, `mistralai/mistral-7b-instruct`, `microsoft/phi-3-mini-128k-instruct`, `qwen/qwen-2-7b-instruct`
- **Paid models:** `openai/gpt-4o-mini`, `deepseek/deepseek-chat`, `anthropic/claude-3.5-sonnet`

---

**Last session:** 2026-07-24 — Committed v0.8.0, OpenRouter cloud fallback fully integrated, settings apply without restart.
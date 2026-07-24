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
| Cloud AI | **OpenRouter (to implement)** — free models first, then paid (GPT-4o-mini, DeepSeek) |
| Repo | https://github.com/YounesJY/ContractProcessor |

---

## Current State (v0.7.0)

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
- Debug logging (`debug.log`)
- `AGENTS.md`, `CHANGELOG.md`, `test_ollama.ps1`

### ⚠️ Known Issues
- **Regex (FieldExtractor)** — failed, jumbled PDF text breaks patterns
- **Local LLM** — 70% accuracy ceiling (2B params too small), 2 min/PDF, 2.5 GB RAM
- **Missing fields** — Police Num often null, dates sometimes confused
- **Encoding** — fixed UTF-8 corruption in LLM responses

---

## Immediate Next Task (In Progress)

### **Add OpenRouter as Cloud Fallback**
Created `OpenRouterService.cs` — needs integration:

1. **Update `AppSettings.cs`** — add `OpenRouterApiKey`, `OpenRouterModel`, `UseCloudAI`
2. **Update `SettingsForm`** — API key input, model dropdown (free + paid models), cloud toggle
3. **Update `ExtractionService`** — try Ollama → if fails/low confidence → try OpenRouter → fallback to regex
4. **Test with free OpenRouter models** — `meta-llama/llama-3.1-8b-instruct:free`, `google/gemma-2-9b-it:free`, `mistralai/mistral-7b-instruct:free`
5. **Then evaluate paid** — GPT-4o-mini (~$0.002/PDF), DeepSeek, etc.

---

## Extraction Evolution

| Phase | Approach | Accuracy | Status |
|-------|----------|----------|--------|
| 1 | Regex | ~30% | ❌ Failed |
| 2 | Local Ollama (llama3.2) | ~70% | ⚠️ Working but slow |
| 3 | **OpenRouter (free → paid)** | **Target 90%+** | 🔜 **Next** |

---

## Key Files to Know

```
ContractProcessor/
├── Services/
│   ├── OllamaService.cs          # Local AI (working)
│   ├── OpenRouterService.cs      # Cloud AI (created, needs integration)
│   ├── ExtractionService.cs      # Orchestrator (needs update)
│   ├── FieldExtractor.cs         # Regex fallback (inaccurate)
│   ├── PdfProcessor.cs           # PDF text extraction
│   └── DebugLogger.cs            # debug.log output
├── Models/AppSettings.cs         # Needs OpenRouter props
├── Forms/SettingsForm.cs         # Needs OpenRouter UI
├── Forms/MainForm.cs             # Uses ExtractionService
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
| OpenRouter free models | ~80% | Free | In progress |
| OpenRouter paid (GPT-4o-mini) | 95%+ | ~$0.002/PDF | Low |
| Azure Document Intelligence | 98% | ~$0.05/PDF | Medium |

---

## Prompt for New Agent

> "Continue from AGENTS.md. Project: ContractProcessor (C# WinForms + SQLite).
> 
> Current state: Local Ollama extraction working (~70%, 2 min/PDF). OpenRouterService created but not yet integrated into ExtractionService/Settings.
> 
> Task: Integrate OpenRouter as cloud fallback (Settings: API key, model dropdown, toggle). Test free models first, then evaluate paid.
> 
> Read AGENTS.md, CHANGELOG.md, then check OpenRouterService.cs and ExtractionService.cs."

---

## Contact / Resources

- **Repo:** https://github.com/YounesJY/ContractProcessor
- **Ollama:** https://ollama.com
- **OpenRouter:** https://openrouter.ai
- **Models to test free:** `meta-llama/llama-3.1-8b-instruct:free`, `google/gemma-2-9b-it:free`, `mistralai/mistral-7b-instruct:free`
- **Paid models:** `openai/gpt-4o-mini`, `deepseek/deepseek-chat`, `anthropic/claude-3.5-sonnet`

---

**Last session:** 2026-07-24 — Committed v0.7.0, created OpenRouterService, ready for integration.
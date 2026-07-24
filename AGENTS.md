# ContractProcessor — Project State

## Overview
Desktop app (C# WinForms + SQLite) for extracting data from Moroccan insurance PDF contracts.
Offline-first, single user, no auth. Uses Ollama (llama3.2) for local AI + OpenRouter for cloud fallback.

## Tech Stack
- **Framework:** .NET 8 WinForms
- **UI:** Guna UI 2.0
- **PDF:** PdfPig
- **Database:** SQLite (Microsoft.Data.Sqlite)
- **Export:** ClosedXML (Excel), CsvHelper (CSV)
- **AI:** Ollama + llama3.2 (local, offline) + **OpenRouter (cloud fallback)**
- **Repo:** https://github.com/YounesJY/ContractProcessor

## Directory Structure
```
ContractProcessor/
├── Constants/AppConstants.cs
├── Models/Contract.cs, AppSettings.cs
├── Services/
│   ├── PdfProcessor.cs          (PDF text extraction via PdfPig)
│   ├── FieldExtractor.cs        (regex-based extraction — inaccurate, fallback)
│   ├── SettingsService.cs       (JSON-based settings load/save)
│   ├── ExportServiceFactory.cs  (factory pattern for export)
│   ├── CsvExportService.cs      (CsvHelper-based CSV export)
│   ├── ExcelExportService.cs    (ClosedXML-based Excel export)
│   ├── OllamaService.cs         (AI extraction via local Ollama)
│   ├── OpenRouterService.cs     (AI extraction via OpenRouter cloud)
│   └── ExtractionService.cs     (Orchestrates AI + regex fallback)
├── Data/
│   ├── DatabaseInitializer.cs   (SQLite schema creation)
│   └── DatabaseHelper.cs        (CRUD for contracts, categories, settings)
├── Helpers/
│   ├── FileHashHelper.cs        (SHA-256 file hashing)
│   ├── CategoryDetector.cs      (keyword-based category detection)
│   └── NotificationHelper.cs    (MessageBox wrappers)
└── Forms/
    ├── MainForm.cs / .Designer.cs          (upload, grid, export, view text, manual extract)
    ├── SettingsForm.cs / .Designer.cs      (root folder + category management + AI settings)
    ├── FieldSelectionForm.cs / .Designer.cs (checkbox dialog for export fields)
    └── ManualExtractForm.cs / .Designer.cs (split-view: PDF text + input fields)
```

## Database Schema
- **contracts:** Id, FileName, FilePath, Category, FileHash (unique), UploadDate, ExtractedData (JSON), SelectedFields (JSON), ProcessingStatus
- **categories:** Id, Name (unique) — seeded: AT, AUTO, MRH
- **settings:** Key, Value (key-value store)

## What's Done
- [x] PDF text extraction via PdfPig (multi-page, text cleaning)
- [x] SQLite database with CRUD operations
- [x] Auto-detection of categories (AT/AUTO/MRH) via keyword matching
- [x] Duplicate detection via SHA256 file hash
- [x] Export to CSV (CsvHelper) and Excel (ClosedXML)
- [x] Guna UI 2.0 on all forms
- [x] DataGridView dark headers (#32323C, white text)
- [x] Manual Extraction Mode (split-view form with PDF text + editable fields)
- [x] "View Text" button (purple) showing extracted JSON
- [x] Settings form (root folder + dynamic category management)
- [x] README.md with full docs and Mermaid flowchart
- [x] LaTeX Compte Rendu (user's AlexNet template style)
- [x] Git repo with remote on GitHub, multiple commits pushed
- [x] **Local AI (Ollama + llama3.2) — working end-to-end**
  - ~70% accuracy, ~2 min/PDF, 2.5GB RAM
  - Regex fallback when Ollama unavailable
- [x] **Cloud AI (OpenRouter) — integrated as fallback**
  - Settings: API key input, model dropdown (free + paid), cloud toggle
  - Extraction chain: Ollama → OpenRouter → Regex
  - Tested with meta-llama/llama-3.1-8b-instruct (~80-90% accuracy, ~5s/PDF)

## What's In Progress
- [x] Ollama + llama3.2 installed on user's PC
- [x] OllamaService.cs — created and working
- [x] ExtractionService.cs — created, AI + regex fallback
- [x] SettingsForm AI toggle — added
- [x] MainForm upload flow — updated to use ExtractionService
- [x] Test script (test_ollama.ps1) — Ollama works correctly via terminal
- [x] AI extraction working — ~70% accuracy, ~2 min/PDF, pre-fill + manual correction
- [x] **OpenRouterService.cs — created and integrated**
- [x] **SettingsForm — OpenRouter UI added (API key, model dropdown, toggle)**
- [x] **ExtractionService — OpenRouter fallback chain implemented**
- [x] **MainForm — recreates ExtractionService on settings change**

## What's Next (Priority Order)
1. Test free OpenRouter models (mistral, phi3, qwen, gemma)
2. Evaluate paid models (GPT-4o-mini ~$0.002/PDF, DeepSeek) for 95%+ accuracy
3. Tune LLM prompt for better date/phone/Police Num extraction
4. Consider Azure Document Intelligence for 98% accuracy (~$0.05/PDF)
5. Deploy (ClickOnce)

## Known Issues
- **FieldExtractor regex is inaccurate** — Allianz PDF text is jumbled, regex matches wrong chunks. This is the regex fallback.
- **Local AI extraction takes ~2-3 min + 2.5GB RAM per PDF** — expected for llama3.2 on CPU. Timeout increased to 5 min.
- **Cloud AI (OpenRouter) ~5s/PDF** — requires internet + API key + credits
- **First build after install** — Ollama must be running (`ollama serve` or auto-started) before uploading PDFs
- **Debug logs** — check `debug.log` in bin/Debug folder for troubleshooting

## Key Notes for Next Session
- The project uses **Guna UI 2.0** — all buttons are Guna2Button, not standard WinForms
- FieldExtractor.cs is NOT deleted — kept as fallback for when Ollama/OpenRouter unavailable
- Categories are dynamic (user can add/remove via Settings), never hardcode category list
- User prefers French UI labels where appropriate (this is a Moroccan insurance company)
- Settings changes now apply immediately (no restart needed) — ExtractionService recreated on settings save

## LLM Prompt Strategy
Send extracted PDF text to Ollama with a structured prompt:
```
Extract the following fields from this insurance contract text.
Return ONLY valid JSON with these keys: "Police Num", "Souscripteur", "Adresse",
"Date d'effet", "Date d'echeance", "Telephone", etc.
If a field is not found, use null.
Text: {pdfText}
```

## Deployment
- ClickOnce (deferred to last phase)

## Key Notes for Next Session
- The project uses **Guna UI 2.0** — all buttons are Guna2Button, not standard WinForms
- FieldExtractor.cs is NOT deleted — kept as fallback for when Ollama is unavailable
- Categories are dynamic (user can add/remove via Settings), never hardcode category list
- The app is fully offline — no cloud APIs, no internet required after Ollama model download
- User prefers French UI labels where appropriate (this is a Moroccan insurance company)

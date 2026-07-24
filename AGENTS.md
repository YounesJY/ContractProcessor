# ContractProcessor — Project State

## Overview
Desktop app (C# WinForms + SQLite) for extracting data from Moroccan insurance PDF contracts.
Offline-first, single user, no auth. Uses Ollama (llama3.2) for AI-powered field extraction.

## Tech Stack
- **Framework:** .NET 8 WinForms
- **UI:** Guna UI 2.0
- **PDF:** PdfPig
- **Database:** SQLite (Microsoft.Data.Sqlite)
- **Export:** ClosedXML (Excel), CsvHelper (CSV)
- **AI:** Ollama + llama3.2 (local, offline)
- **Repo:** https://github.com/YounesJY/ContractProcessor

## Directory Structure
```
ContractProcessor/
├── Constants/AppConstants.cs
├── Models/Contract.cs, AppSettings.cs
├── Services/
│   ├── PdfProcessor.cs          (PDF text extraction via PdfPig)
│   ├── FieldExtractor.cs        (regex-based extraction — inaccurate, to be replaced)
│   ├── SettingsService.cs       (JSON-based settings load/save)
│   ├── ExportServiceFactory.cs  (factory pattern for export)
│   ├── CsvExportService.cs      (CsvHelper-based CSV export)
│   ├── ExcelExportService.cs    (ClosedXML-based Excel export)
│   ├── OllamaService.cs         [TODO] AI extraction via local Ollama
│   └── ExtractionService.cs     [TODO] Orchestrates AI + regex fallback
├── Data/
│   ├── DatabaseInitializer.cs   (SQLite schema creation)
│   └── DatabaseHelper.cs        (CRUD for contracts, categories, settings)
├── Helpers/
│   ├── FileHashHelper.cs        (SHA-256 file hashing)
│   ├── CategoryDetector.cs      (keyword-based category detection)
│   └── NotificationHelper.cs    (MessageBox wrappers)
└── Forms/
    ├── MainForm.cs / .Designer.cs          (upload, grid, export, view text, manual extract)
    ├── SettingsForm.cs / .Designer.cs      (root folder + category management)
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

## What's In Progress
- [x] Ollama + llama3.2 installed on user's PC
- [x] OllamaService.cs — created and working
- [x] ExtractionService.cs — created, AI + regex fallback
- [x] SettingsForm AI toggle — added
- [x] MainForm upload flow — updated to use ExtractionService
- [x] Test script (test_ollama.ps1) — Ollama works correctly via terminal
- [x] AI extraction working — ~70% accuracy, ~2 min/PDF, pre-fill + manual correction

## What's Next (Priority Order)
1. ~~Install Ollama + pull llama3.2~~ ✅ DONE
2. ~~Create `Services/OllamaService.cs`~~ ✅ DONE
3. ~~Create `Services/ExtractionService.cs`~~ ✅ DONE
4. ~~Update `Models/AppSettings.cs`~~ ✅ DONE
5. ~~Update `Forms/SettingsForm.cs`~~ ✅ DONE
6. ~~Update `Forms/MainForm.cs`~~ ✅ DONE
7. ~~Fix AI extraction field name mismatch~~ ✅ DONE
8. ~~Fix AI timeout (5 min)~~ ✅ DONE
9. ~~Fix UTF-8 encoding corruption~~ ✅ DONE
10. ~~Simplify prompt for better accuracy~~ ✅ DONE
11. Test with real Allianz PDFs in the app ✅ DONE
12. Commit and push
13. **Decide next step:** OpenAI fallback? Keep local only? Deploy?

## Known Issues
- **FieldExtractor regex is inaccurate** — Allianz PDF text is jumbled, regex matches wrong chunks. This is the regex fallback.
- **AI extraction takes ~2-3 min + 2.5GB RAM per PDF** — expected for llama3.2 on CPU. Timeout increased to 5 min.
- **App restart required** after changing AI settings (UseAI/AIModel toggle)
- **First build after install** — Ollama must be running (`ollama serve` or auto-started) before uploading PDFs
- **Debug logs** — check `debug.log` in bin/Debug folder for troubleshooting

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

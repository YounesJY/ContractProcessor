# Changelog

All notable changes to ContractProcessor are documented here.
Format follows [Keep a Changelog](https://keepachangelog.com/).

---

## [0.7.0] — 2026-07-24

### Completed
- **AI-powered extraction fully integrated** — local Ollama (llama3.2) working end-to-end
  - Upload → extract text → AI → store → export pipeline functional
  - ~70% field accuracy, ~2 min/PDF, 2.5GB RAM
  - Regex fallback when Ollama unavailable
- **Settings** — AI toggle, model selector (llama3.2/mistral/phi3/gemma2)
- **Debug logging** — `debug.log` for troubleshooting
- **Method indicator** — "imported via AI" vs "imported via Regex" in notifications

---

## [Unreleased]

### To Do
- Decide next step: OpenAI fallback? Keep local only? Deploy?
- Tune LLM prompt for better accuracy if needed

---

## [0.6.4] — 2026-07-24

### Fixed
- **UTF-8 encoding corruption** — accented chars like `Assuré` became `AssurÃ©`, field names didn't match
  - Fix: Read HTTP response as string first with proper encoding, then parse JSON
- **LLM returning paragraphs instead of dates** — simplified prompt, explicit DD/MM/YYYY format
- **Too many fields confusing LLM** — reduced to 6 core fields (Police Num, Souscripteur, Adresse, Date d'effet, Date d'échéance, Téléphone)

### Changed
- ExtractionService now requests fewer fields for better accuracy
- Removed unused fields from AI prompt (Assuré, Type de contrat, Code Intermédiaire, N° Client)
- Updated test_ollama.ps1 with simplified prompt

---

## [0.6.3] — 2026-07-24

### Fixed
- **AI extraction timing out after 120s** — increased HttpClient timeout to 5 minutes
- **LLM prompt too large** — reduced text limit from 8000 to 4000 chars (still enough context)

### Added
- **DebugLogger.cs** — writes diagnostic logs to `debug.log` for troubleshooting
- ExtractionService now logs each step (availability check, API call, field count)

---

## [0.6.2] — 2026-07-24

### Fixed
- **Critical: AI extraction returning empty, falling back to regex**
  - Root cause: LLM field names didn't match expected keys (e.g. "Date d effets" vs "Date d'effet")
  - Root cause: LLM wrapped JSON in markdown code blocks (` ```json ... ``` `)
  - Fix: Stricter prompt with exact JSON structure shown to LLM
  - Fix: Strip markdown code blocks before JSON parsing
  - Fix: Fuzzy field name matching (case-insensitive, accent-insensitive)

### Changed
- **OllamaService prompt rewritten** — shows exact JSON structure LLM must return
  - Explicit rules: no translation, no code blocks, raw JSON only
  - Increases reliability of field name matching
- **ParseJsonResponse improved**
  - Strips ```json code blocks before parsing
  - Fuzzy matching via NormalizeForMatch (removes accents, spaces, special chars)
  - FormatJsonValue helper for clean extraction
- Added `test_ollama.ps1` for testing Ollama directly via terminal

---

## [0.6.1] — 2026-07-24

### Changed
- **Improved LLM prompt** — more specific about Moroccan insurance contracts (Allianz/Sanlam format)
  - Added examples of what to find (Police N°, Nom et prénom, Adresse, dates)
  - Better rules (names uppercase, dates DD/MM/YYYY, no label text as values)
  - Increased text limit from 6000 to 8000 chars for better context
- **ExtractionResult class** — tracks which method was used (AI vs Regex)
- **MainForm** — notification now shows "imported via AI" or "imported via Regex"
- Add status indicator showing AI vs regex extraction method

---

## [0.6.0] — 2026-07-24

### Added
- **OllamaService.cs** — HTTP client for local Ollama API (localhost:11434)
  - `IsAvailableAsync()` — checks if Ollama is running
  - `GetModelsAsync()` — lists installed models
  - `ExtractFieldsAsync()` — sends PDF text to LLM, returns extracted fields
  - JSON response parsing with malformed output handling
- **ExtractionService.cs** — unified extraction orchestrator
  - AI first (via Ollama), regex fallback if Ollama unavailable
  - Category-aware field name lists
- **AI Settings in SettingsForm**
  - Toggle switch for enabling/disabling AI extraction
  - Model selector dropdown (llama3.2, mistral, phi3, gemma2)
- **AppSettings** — new properties: `UseAI` (bool), `AIModel` (string)
- **MainForm** — upload flow now uses `ExtractionService` instead of `FieldExtractor` directly

---

## [0.5.0] — 2026-07-24

### Added
- **Manual Extraction Mode** (`ManualExtractForm`)
  - Split-view UI: extracted PDF text on left, editable input fields on right
  - Dynamic field generation from existing extracted data
  - Confirm button saves edited fields back to database
  - Cancel button returns to main form
- Purple "View Text" button on MainForm — shows raw extracted JSON
- Yellow "Manual Extract" button on MainForm

### Fixed
- `SplitContainer.SplitterDistance` crash — removed hardcoded distance/minsize values, let auto-size handle layout

---

## [0.4.0] — 2026-07-24

### Changed
- DataGridView header style: dark background (#32323C) with white text
- Reverted rounded corners feature (user didn't like the Guna2BorderlessForm approach)

---

## [0.3.0] — 2026-07-24

### Added
- **SettingsForm** for root folder and category management
- Dynamic category system — users can add/remove categories at runtime
- Settings persisted via JSON file (`appsettings.json`)

---

## [0.2.0] — 2026-07-24

### Added
- **Export to CSV** via CsvHelper
- **Export to Excel** via ClosedXML
- **FieldSelectionForm** — checkbox dialog for selecting which fields to export
- Duplicate detection via SHA-256 file hash (DatabaseHelper.DuplicateExists)
- Contract copied to `Contracts/{category}/` folder on upload

---

## [0.1.0] — 2026-07-24

### Added
- Initial project structure (Forms/, Models/, Data/, Services/, Helpers/, Constants/)
- **PdfProcessor** — text extraction via PdfPig with text cleaning (broken word fix, whitespace normalization)
- **FieldExtractor** — regex-based extraction for common, AUTO, AT, MRH fields
- **SQLite database** — contracts, categories, settings tables
- **CategoryDetector** — keyword-based auto-detection (AT, AUTO, MRH)
- **Guna UI 2.0** integration on all forms
- **MainForm** — PDF upload, grid display, delete workflow
- **DatabaseHelper** — full CRUD operations
- **FileHashHelper** — SHA-256 hashing
- **NotificationHelper** — Success/Error/Warning/Confirm message boxes

---

## Version History Summary

| Version | Date | Milestone |
|---------|------|-----------|
| 0.1.0 | 2026-07-24 | Core: PDF extraction + SQLite + basic UI |
| 0.2.0 | 2026-07-24 | Export: CSV + Excel + field selection |
| 0.3.0 | 2026-07-24 | Settings: root folder + categories |
| 0.4.0 | 2026-07-24 | UI polish: dark headers |
| 0.5.0 | 2026-07-24 | Manual extraction fallback |
| 0.6.0 | 2026-07-24 | AI extraction via Ollama (llama3.2) |
| 0.6.1 | 2026-07-24 | Improved AI prompt + method tracking |
| 0.6.2 | 2026-07-24 | Fix AI field name mismatch + markdown code block parsing |
| 0.6.3 | 2026-07-24 | Fix AI timeout (5min) + reduce prompt text (4000 chars) + debug logging |
| 0.6.4 | 2026-07-24 | Fix UTF-8 encoding + simplify prompt + reduce fields for accuracy |
| 0.7.0 | 2026-07-24 | AI extraction complete — local Ollama end-to-end |

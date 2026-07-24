# ContractProcessor

Desktop application for extracting and managing data from insurance PDF contracts. Built for a small assurance organization - fully offline, single user, no authentication required.

---

## MVP Overview

**Goal:** Extract data from multiple PDF contracts (AT, AUTO, MRH, etc.), let users choose which fields to display in a final table, and export to Excel/CSV.

### Workflow

1. User uploads PDF contracts (drag-and-drop or file picker)
2. App auto-detects contract type (AT, AUTO, MRH) from PDF text
3. PDF text is extracted and stored
4. User selects which fields to keep (e.g., contract number, amount, dates)
5. Results displayed in a filterable table
6. User exports selected data to Excel or CSV

### Flowchart

```mermaid
flowchart TD
    A([Start]) --> B[User selects root folder]
    B --> C[Load existing contracts]
    C --> D{User action}

    D -->|Upload PDFs| E[Select PDF files]
    E --> F[Compute SHA256 hash]
    F --> G{Duplicate?}
    G -->|Yes| H[Skip - show warning]
    G -->|No| I[Extract text from PDF]
    I --> J[Auto-detect category]
    J --> K[Copy PDF to category folder]
    K --> L[Insert into database]
    L --> M[Refresh table]
    M --> D

    D -->|Select Fields| N[Show field selection dialog]
    N --> O[User checks desired fields]
    O --> P[Save selection to database]
    P --> D

    D -->|Filter by category| Q[Filter table by category]
    Q --> D

    D -->|Export| R{Fields selected?}
    R -->|No| S[Show warning]
    S --> D
    R -->|Yes| T[Build export data]
    T --> U{Export format?}
    U -->|Excel| V[Export to .xlsx]
    U -->|CSV| W[Export to .csv]
    V --> X[Save file + open]
    W --> X
    X --> D

    D -->|Settings| Y[Change root folder / manage categories]
    Y --> D

    D -->|Delete contract| Z[Confirm + delete from DB]
    Z --> D

    D -->|Close app| AA([End])
```

### Contract Types

| Type                                              | Description                                     |
| ------------------------------------------------- | ----------------------------------------------- |
| AT                                                | Assurance Temporaire (temporary life insurance) |
| AUTO                                              | Automobile/vehicle insurance                    |
| MRH                                               | Multirisque Habitation (home insurance)         |
| *New types can be added dynamically via Settings* |                                                 |

---

## Extraction Evolution

| Phase | Approach | Accuracy | Resources | Status |
|-------|----------|----------|-----------|--------|
| 1 | Regex-based (FieldExtractor) | ~30% | Negligible | ❌ Failed — jumbled PDF text breaks patterns |
| 2 | Local LLM (Ollama + llama3.2) | ~70% | ~2.5 GB RAM, ~2 min/PDF | ⚠️ Works but slow & resource-heavy |
| 3 | **Online LLM (OpenAI / Azure)** | **Target 90%+** | Negligible local | 🔜 **Next** |

### Why Regex Failed
Allianz/Sanlam PDFs produce jumbled text where labels and values run together:
```
Police N°IntermédiaireAdresseintermédiaireCode Intermédiaire01883
```
Regex patterns match wrong chunks because there's no reliable delimiter.

### Why Local LLM (Ollama) Isn't Enough
- **Speed**: ~2 minutes per PDF on CPU
- **RAM**: ~2.5 GB for llama3.2 (2B params)
- **Accuracy**: ~70% — still misses Police Num, confuses dates, hallucinates
- **Root cause**: 2B parameter model too small for complex document parsing; no vision/layout awareness

---

## Current Status

### Completed

- [x] Project structure (Forms, Models, Data, Services, Helpers, Constants)
- [x] SQLite database with contracts, categories, and settings tables
- [x] PDF text extraction using PdfPig
- [x] Auto-detection of contract categories from PDF text
- [x] Duplicate detection via SHA256 file hash
- [x] Export to CSV (CsvHelper) and Excel (ClosedXML)
- [x] Guna UI 2.0 controls for modern look
- [x] WinForms UI with upload, table view, field selection, export
- [x] **Local AI extraction (Ollama + llama3.2) — working, ~70% accuracy**
- [x] Manual extraction fallback (split-view: PDF text + editable fields)
- [x] Settings: root folder, dynamic categories, AI toggle + model selector
- [x] Debug logging (`debug.log`) for troubleshooting AI extraction

### In Progress

- [ ] Testing with real PDF samples
- [ ] Evaluating online LLM options (OpenAI / Azure Document Intelligence)

### Next Steps

- [ ] **Integrate OpenAI GPT-4o-mini or Azure Document Intelligence** for production accuracy
- [ ] OCR support for scanned PDFs (Tesseract)
- [ ] Manual category override during upload
- [ ] Remember field selection preferences
- [ ] Search/filter by date range
- [ ] ClickOnce deployment setup

---

## Tech Stack

| Component    | Technology                       |
| ------------ | -------------------------------- |
| Language     | C# (.NET 8)                      |
| UI           | WinForms + Guna UI 2.0           |
| Database     | SQLite (file-based, zero config) |
| PDF Parsing  | PdfPig                           |
| Excel Export | ClosedXML                        |
| CSV Export   | CsvHelper                        |
| **Local AI** | **Ollama + llama3.2** (optional) |
| **Cloud AI** | **OpenAI GPT-4o-mini / Azure Document Intelligence (planned)** |
| Deployment   | ClickOnce (planned)              |

---

## Project Structure

```
ContractProcessor/
├── Forms/
│   ├── MainForm.cs              # Main window - upload, table, filter, export
│   ├── MainForm.Designer.cs
│   ├── FieldSelectionForm.cs    # Dialog to select fields
│   ├── FieldSelectionForm.Designer.cs
│   ├── SettingsForm.cs          # Root folder & category management
│   └── SettingsForm.Designer.cs
├── Models/
│   ├── Contract.cs              # Contract record model
│   └── AppSettings.cs           # User preferences model
├── Data/
│   ├── DatabaseInitializer.cs   # Create DB/tables on first run
│   └── DatabaseHelper.cs        # CRUD operations
├── Services/
│   ├── IPdfProcessor.cs         # PDF processing interface
│   ├── PdfProcessor.cs          # PdfPig text extraction
│   ├── IExportService.cs        # Export interface
│   ├── CsvExportService.cs      # CSV export
│   ├── ExcelExportService.cs    # Excel export
│   ├── ExportServiceFactory.cs  # Create correct exporter
│   └── SettingsService.cs       # Load/save app settings
├── Helpers/
│   ├── FileHashHelper.cs        # SHA256 duplicate detection
│   ├── CategoryDetector.cs      # Auto-detect AT/AUTO/MRH
│   └── NotificationHelper.cs    # Message dialogs
├── Constants/
│   └── AppConstants.cs          # Categories, status values, paths
└── Program.cs                   # Entry point
```

---

## Setup

1. Open `ContractProcessor.sln` in Visual Studio 2022
2. Build and run (F5)
3. Select a root folder when prompted (where contracts and data will be stored)

---

## Root Folder Structure

```
YourRootFolder/
├── Contracts/           # Uploaded PDFs organized by category
│   ├── AT/
│   ├── AUTO/
│   └── MRH/
├── Exports/             # Exported CSV/Excel files
└── AppData/
    └── contract_history.db   # SQLite database
```

---

## Database Schema

```sql
contracts (
    Id              INTEGER PRIMARY KEY AUTOINCREMENT,
    FileName        TEXT NOT NULL,
    FilePath        TEXT NOT NULL,
    Category        TEXT NOT NULL DEFAULT 'Unknown',
    FileHash        TEXT UNIQUE NOT NULL,
    UploadDate      DATETIME DEFAULT CURRENT_TIMESTAMP,
    ExtractedData   TEXT DEFAULT '{}',        -- JSON
    SelectedFields  TEXT DEFAULT '[]',        -- JSON
    ProcessingStatus TEXT DEFAULT 'Pending'
)

categories (
    Id   INTEGER PRIMARY KEY AUTOINCREMENT,
    Name TEXT UNIQUE NOT NULL
)

settings (
    Key   TEXT PRIMARY KEY,
    Value TEXT NOT NULL
)
```

---

## License

Internal use only.

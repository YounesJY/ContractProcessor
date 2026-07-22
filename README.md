# ContractProcessor

Desktop application for extracting and managing data from insurance PDF contracts.

## Features

- **PDF Upload** - Drag-and-drop or file picker, with duplicate detection (SHA256)
- **Auto-categorization** - Automatically detects contract type (AT, AUTO, MRH) from PDF text
- **Data Extraction** - Extracts text from PDFs using PdfPig
- **Field Selection** - Choose which fields to include in exports
- **Table View** - View all contracts with category filtering
- **Export** - Export to CSV or Excel (xlsx)
- **Offline** - Fully local, no internet required

## Tech Stack

- C# WinForms (.NET 8)
- SQLite (file-based, zero config)
- PdfPig (PDF text extraction)
- ClosedXML (Excel export)
- CsvHelper (CSV export)

## Project Structure

```
ContractProcessor/
├── Forms/          # WinForms UI
├── Models/         # Data models (Contract, AppSettings)
├── Data/           # SQLite database layer
├── Services/       # PDF processing, export, settings
├── Helpers/        # File hashing, category detection, notifications
└── Constants/      # App-wide constants
```

## Setup

1. Open `ContractProcessor.sln` in Visual Studio 2022
2. Build and run (F5)
3. Select a root folder when prompted (where contracts and data will be stored)

## Root Folder Structure

```
YourRootFolder/
├── Contracts/      # Uploaded PDFs organized by category
│   ├── AT/
│   ├── AUTO/
│   └── MRH/
├── Exports/        # Exported CSV/Excel files
└── AppData/        # SQLite database
```

## License

Internal use only.

Write-Host "Testing Ollama with simplified prompt..."
try {
    $tags = Invoke-RestMethod -Uri "http://localhost:11434/api/tags" -Method Get -ErrorAction Stop
    Write-Host "Ollama OK. Model: $($tags.models.name -join ', ')" -ForegroundColor Green
} catch {
    Write-Host "FAIL: Ollama not running!" -ForegroundColor Red
    exit 1
}

$contractText = "AKRAM EL KASSAD ASSURANCES 9936084470 AVENUE MOHAMED EL FASSI ET RUE AGLOU HAY SALAM 1ER ETAGE AGADIR Conditions Particulieres Police N intermediaire Adresse intermediaire Code Intermediaire 01883 Affaire nouvelle Remplacement 4123750 N Client Par N police X Allianz Habitation Nom et prenom ou raison sociale M. EL HADRI OMAR SOUSCRIPTEUR ASSURE EL HADRI OMAR Rue IMM 106 APPT 16 HAY MOHAMMADI Adresse AGADIR Date d effets 16/07/2027 Date expiration 17/07/2026 Telephone 0600000000"

$prompt = @"
Extract data from a Moroccan MRH insurance contract (Allianz/Sanlam).

Return EXACTLY this JSON with these EXACT keys:
{
  "Police Num": "value or null",
  "Souscripteur": "value or null",
  "Adresse": "value or null",
  "Date d'effet": "DD/MM/YYYY or null",
  "Date d'échéance": "DD/MM/YYYY or null",
  "Téléphone": "digits only or null"
}

Rules:
- Use EXACT key names, do NOT rename
- Dates MUST be short: DD/MM/YYYY only, NOT paragraphs of text
- Names in UPPERCASE
- Phone: digits only, no spaces
- Return ONLY the raw JSON, no explanation, no code blocks

Text:
$contractText
"@

$body = @{
    model = "llama3.2"
    prompt = $prompt
    stream = $false
    options = @{ temperature = 0.1; top_p = 0.9 }
} | ConvertTo-Json -Depth 5

Write-Host "`nSending to Ollama..."
$response = Invoke-RestMethod -Uri "http://localhost:11434/api/generate" -Method Post -Body $body -ContentType "application/json" -TimeoutSec 300

Write-Host "`nLLM Response:" -ForegroundColor Yellow
Write-Host $response.response

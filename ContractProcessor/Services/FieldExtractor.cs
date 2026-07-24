using System.Text.RegularExpressions;

namespace ContractProcessor.Services;

public static class FieldExtractor
{
    public static Dictionary<string, string> Extract(string text, string category)
    {
        var fields = new Dictionary<string, string>();

        if (string.IsNullOrWhiteSpace(text))
            return fields;

        var lines = text.Split('\n').Select(l => l.Trim()).Where(l => l.Length > 0).ToList();
        var fullText = string.Join(" ", lines);

        // ---- POLICE N° (most important) ----
        // Try multiple patterns
        ExtractFirstMatch(fields, fullText, "Police N°", @"Police\s*N[°o]\s*:?\s*([A-Z0-9\-/]{3,25})");
        ExtractFirstMatch(fields, fullText, "Police N°", @"N[°o]\s*Police\s*:?\s*([A-Z0-9\-/]{3,25})");

        // ---- CODE INTERMÉDIAIRE ----
        ExtractFirstMatch(fields, fullText, "Code Intermédiaire", @"Code\s*Int[eé]rm[eé]diaire\s*:?\s*(\d{4,10})");

        // ---- N° CLIENT ----
        ExtractFirstMatch(fields, fullText, "N° Client", @"N[°o]\s*Client\s*:?\s*(\d{5,15})");

        // ---- NOM / SOUSCRIPTEUR ----
        // Pattern: "Nom et prénom ou raison sociale : VALUE"
        ExtractFirstMatch(fields, fullText, "Souscripteur",
            @"Nom\s+et\s+pr[eé]nom(?:\s+ou\s+raison\s+sociale)?\s*:\s*(.{3,70}?)(?:\s*(?:Adresse|Rue|Avenue|Tel|N[°o]|Code|$))");

        // Pattern: look for name after "SOUSCRIPTEUR" keyword
        ExtractFirstMatch(fields, fullText, "Souscripteur",
            @"SOUSCRIPTEUR\s*(.{3,70}?)(?:\s*(?:ASSUR[ÉE]|Rue|Adresse))");

        // ---- ASSURÉ (if different from souscripteur) ----
        ExtractFirstMatch(fields, fullText, "Assuré",
            @"(?:II\s*[-–]\s*ASSUR[ÉE]|ASSUR[ÉE]\s*[:\-])\s*(.{3,70}?)(?:\s*(?:Rue|Adresse|N[°o]|Code|$))");

        // ---- ADRESSE ----
        // Look for address patterns: Rue/Avenue/Boulevard + number + city
        ExtractFirstMatch(fields, fullText, "Adresse",
            @"Adresse\s*:\s*(.{10,120}?)(?:\s*(?:T[eé]l|Tel|Code|Police|Nom|Situation|$))");

        // Pattern: "Rue XXX N° YYY VILLE"
        ExtractFirstMatch(fields, fullText, "Adresse",
            @"((?:Rue|Avenue|Boulevard|BP|Cité|Hay|Quartier)\s+.{10,100}?AGADIR)");

        // ---- TYPE DE CONTRAT ----
        ExtractFirstMatch(fields, fullText, "Type de contrat", @"ALLIANZ\s+([A-Z\s\-]{5,50}?)(?:\s*\d{5,})");
        ExtractFirstMatch(fields, fullText, "Type de contrat", @"Allianz\s+([A-ZÀ-Ü][a-zà-ü]+(?:\s+[A-ZÀ-Ü][a-zà-ü]+){0,3})");

        // ---- DATES ----
        ExtractFirstMatch(fields, fullText, "Date d'effet",
            @"(?:date\s*d['']effet|effet\s*le|du\s*contrat)\s*:?\s*(\d{1,2}[/\-.]\d{1,2}[/\-.]\d{2,4})");

        ExtractFirstMatch(fields, fullText, "Date d'échéance",
            @"(?:[eé]ch[eé]ance|date\s+de\s+fin)\s*:?\s*(\d{1,2}[/\-.]\d{1,2}[/\-.]\d{2,4})");

        // ---- TÉLÉPHONE ----
        ExtractFirstMatch(fields, fullText, "Téléphone",
            @"(?:T[eé]l[eé]?phone|Tel|Mobile)\s*:?\s*([\d\s\-+]{8,15})");

        // ---- CATEGORY-SPECIFIC ----
        if (category == "AUTO")
        {
            ExtractFirstMatch(fields, fullText, "Immatriculation",
                @"(?:immatriculation|matricule|plaque)\s*:?\s*([A-Z0-9\-]{5,15})");
            ExtractFirstMatch(fields, fullText, "Marque véhicule",
                @"(?:marque|vehicule)\s*:?\s*([A-ZÀ-Ü][a-zà-ü]{2,15})");
            ExtractFirstMatch(fields, fullText, "Puissance fiscale",
                @"(?:puissance\s*fiscale|chevaux|cv)\s*:?\s*(\d{1,3})");
            ExtractFirstMatch(fields, fullText, "N° Châssis",
                @"(?:chassis|châssis|VIN)\s*:?\s*([A-Z0-9]{10,20})");
        }
        else if (category == "AT")
        {
            ExtractFirstMatch(fields, fullText, "Capital décès",
                @"(?:capital\s+d[eé]c[eè]s)\s*:?\s*([\d\s,.]+)\s*(?:DH|MAD|EUR|€)?");
            ExtractFirstMatch(fields, fullText, "Salaire journalier",
                @"(?:salaire\s+journalier)\s*:?\s*([\d\s,.]+)");
            ExtractFirstMatch(fields, fullText, "Durée arrêt",
                @"(?:dur[eé]e\s+d['']arr[eê]t)\s*:?\s*(\d+)");
        }
        else if (category == "MRH")
        {
            ExtractFirstMatch(fields, fullText, "Type habitation",
                @"(?:habitation|logement|maison|appartement)\s*:?\s*(\w[\w\s]{2,30})");
            ExtractFirstMatch(fields, fullText, "Surface",
                @"(?:surface|superficie)\s*:?\s*(\d+[\d,.]*)\s*(?:m[²2])?");
            ExtractFirstMatch(fields, fullText, "Valeur locative",
                @"(?:valeur\s*locative|loyer)\s*:?\s*([\d\s,.]+)\s*(?:DH|MAD|EUR|€)?");
        }

        return fields;
    }

    private static void ExtractFirstMatch(Dictionary<string, string> fields, string text, string fieldName, string pattern)
    {
        if (fields.ContainsKey(fieldName))
            return;

        var match = Regex.Match(text, pattern, RegexOptions.IgnoreCase);
        if (match.Success && match.Groups.Count > 1)
        {
            var value = match.Groups[1].Value.Trim();
            value = Regex.Replace(value, @"\s{2,}", " ").Trim();
            value = value.TrimEnd('.', ':', ',', ';');

            if (!string.IsNullOrEmpty(value) && value.Length >= 2 && value.Length <= 150)
                fields[fieldName] = value;
        }
    }
}

using System.Globalization;
using System.Text;

namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _paysPopulationFilePath = Path.Combine(AppApprendreDataFolderPath, "PaysPopulation.json");
        private List<global::Apprendre.Models.PaysPopulation> _paysPopulationList { get; set; } = new List<global::Apprendre.Models.PaysPopulation>();

        #endregion Properties

        #region Private

        private void LoadPaysPopulation()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_paysPopulationFilePath))
            {
                ShowEmptyPaysPopulationState();
                return;
            }

            string json = File.ReadAllText(_paysPopulationFilePath);
            List<global::Apprendre.Models.PaysPopulation> paysPopulationList = JsonSerializer.Deserialize<List<global::Apprendre.Models.PaysPopulation>>(json) ?? [];
            _paysPopulationList = NormalizePaysPopulationList(paysPopulationList);

            if (_paysPopulationList.Count == 0)
            {
                ShowEmptyPaysPopulationState();
                return;
            }

            SavePaysPopulationListIfChanged(json);

            ShowPaysPopulationOnPanelWorking();
        }

        private void SavePaysPopulationListIfChanged(string currentJson)
        {
            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            string updatedJson = JsonSerializer.Serialize(_paysPopulationList, options);
            if (!string.Equals(currentJson, updatedJson, StringComparison.Ordinal))
            {
                File.WriteAllText(_paysPopulationFilePath, updatedJson);
            }
        }

        private static List<global::Apprendre.Models.PaysPopulation> NormalizePaysPopulationList(List<global::Apprendre.Models.PaysPopulation> paysPopulationList)
        {
            Dictionary<string, long>? latestPopulations = TryGetLatestPopulationByCountryNameEn();

            return paysPopulationList
                .Select(item =>
                {
                    string countryNameEn = NormalizeCountryText(item.CountryNameEn);

                    return new global::Apprendre.Models.PaysPopulation
                    {
                        CountryNameFr = NormalizeCountryText(item.CountryNameFr),
                        CountryNameEn = countryNameEn,
                        Population = RoundPopulationToNearestThousand(latestPopulations is not null && latestPopulations.TryGetValue(countryNameEn, out long latestPopulation) ? latestPopulation : item.Population),
                        CapitalFr = NormalizeCountryText(item.CapitalFr),
                        CapitalEn = NormalizeCountryText(item.CapitalEn)
                    };
                })
                .OrderByDescending(item => item.Population)
                .ThenBy(item => item.CountryNameFr, StringComparer.OrdinalIgnoreCase)
                .ToList();
        }

        private static Dictionary<string, long>? TryGetLatestPopulationByCountryNameEn()
        {
            try
            {
                using var httpClient = new System.Net.Http.HttpClient
                {
                    Timeout = TimeSpan.FromSeconds(15)
                };

                string json = httpClient.GetStringAsync("https://restcountries.com/v3.1/all?fields=name,population").GetAwaiter().GetResult();
                using JsonDocument document = JsonDocument.Parse(json);

                Dictionary<string, long> populations = new(StringComparer.OrdinalIgnoreCase);

                foreach (JsonElement country in document.RootElement.EnumerateArray())
                {
                    if (!country.TryGetProperty("name", out JsonElement nameElement)
                        || !nameElement.TryGetProperty("common", out JsonElement commonElement)
                        || commonElement.ValueKind != JsonValueKind.String
                        || !country.TryGetProperty("population", out JsonElement populationElement)
                        || !populationElement.TryGetInt64(out long population))
                    {
                        continue;
                    }

                    string? countryName = commonElement.GetString();
                    if (!string.IsNullOrWhiteSpace(countryName))
                    {
                        populations[countryName] = population;
                    }
                }

                return populations;
            }
            catch
            {
                return null;
            }
        }

        private static long RoundPopulationToNearestThousand(long population)
        {
            if (population <= 0)
            {
                return 0;
            }

            return Math.Max(1_000, ((population + 500) / 1_000) * 1_000);
        }

        private static string NormalizeCountryText(string value)
        {
            if (string.IsNullOrWhiteSpace(value) || value.IndexOfAny(['Ã', 'Â', 'Å', 'â']) < 0)
            {
                return value;
            }

            string normalizedValue = Encoding.UTF8.GetString(Encoding.Latin1.GetBytes(value));

            return normalizedValue.Contains('�') ? value : normalizedValue;
        }

        private void ShowPaysPopulationOnPanelWorking()
        {
            SuspendLayout();

            BackColor = Color.FromArgb(249, 250, 251);

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "La liste complète des pays classés par population décroissante, avec leur population actuelle et leur capitale en français et en anglais.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 88),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "The complete list of countries sorted by descending population, with their current population and capital in French and English.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 128),
                AutoSize = true
            });

            int yPosition = 186;
            int cardWidth = Math.Max(860, ClientSize.Width - 100);
            int englishXPosition = Math.Max(470, (cardWidth / 2) + 60);
            CultureInfo frenchCulture = CultureInfo.GetCultureInfo("fr-FR");

            for (int i = 0; i < _paysPopulationList.Count; i++)
            {
                string populationText = _paysPopulationList[i].Population.ToString("N0", frenchCulture);

                Panel card = new Panel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, 146),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Label labelCountryFrHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Pays",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(24, 16),
                    AutoSize = true
                };

                Label labelCountryFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _paysPopulationList[i].CountryNameFr,
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Location = new Point(24, 40),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4)
                };

                Label labelCapitalFrHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Capitale",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(24, 84),
                    AutoSize = true
                };

                Label labelCapitalFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _paysPopulationList[i].CapitalFr,
                    Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(24, 108),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4)
                };

                Label labelCountryEnHeader = new Label
                {
                    Tag = $"en|{i}",
                    Text = "Country",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(englishXPosition, 16),
                    AutoSize = true
                };

                Label labelCountryEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _paysPopulationList[i].CountryNameEn,
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Location = new Point(englishXPosition, 40),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4)
                };

                Label labelCapitalEnHeader = new Label
                {
                    Tag = $"en|{i}",
                    Text = "Capital",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(englishXPosition, 84),
                    AutoSize = true
                };

                Label labelCapitalEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _paysPopulationList[i].CapitalEn,
                    Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(englishXPosition, 108),
                    AutoSize = true,
                    Padding = new Padding(8, 4, 8, 4)
                };

                Label labelPopulationHeader = new Label
                {
                    Tag = "fr|-1",
                    Text = "Population",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point((cardWidth / 2) - 154, 18),
                    AutoSize = true
                };

                Label labelPopulation = new Label
                {
                    Tag = "fr|-1",
                    Text = $"{populationText.Replace(" ", "", StringComparison.CurrentCultureIgnoreCase)}",
                    Font = new Font("Segoe UI", 11F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point((cardWidth / 2) - 174, 48),
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6)
                };

                card.Controls.Add(labelCountryFrHeader);
                card.Controls.Add(labelCountryFr);
                card.Controls.Add(labelCapitalFrHeader);
                card.Controls.Add(labelCapitalFr);
                card.Controls.Add(labelCountryEnHeader);
                card.Controls.Add(labelCountryEn);
                card.Controls.Add(labelCapitalEnHeader);
                card.Controls.Add(labelCapitalEn);
                card.Controls.Add(labelPopulationHeader);
                card.Controls.Add(labelPopulation);

                Controls.Add(card);

                yPosition += 164;
            }

            AutoScrollMinSize = new Size(0, yPosition + 32);
            ResumeLayout();
        }

        private void ShowEmptyPaysPopulationState()
        {
            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "Aucune donnée de pays et population n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "No country and population data is available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 145),
                AutoSize = true
            });
        }

        #endregion Private
    }
}

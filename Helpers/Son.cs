namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _sonFrancaisFilePath = Path.Combine(AppApprendreDataFolderPath, "SonFrancais.json");
        private List<SonFrancais> _sonFrancaisList { get; set; } = new List<SonFrancais>();

        #endregion Properties

        #region Private

        private void LoadSonFrancaisList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_sonFrancaisFilePath))
            {
                ShowEmptySonFrancaisState();
                return;
            }

            string json = File.ReadAllText(_sonFrancaisFilePath);
            _sonFrancaisList = JsonSerializer.Deserialize<List<SonFrancais>>(json) ?? new List<SonFrancais>();

            if (_sonFrancaisList.Count == 0)
            {
                ShowEmptySonFrancaisState();
                return;
            }

            ShowSonFrancaisOnPanelWorking();
        }

        private void ShowSonFrancaisOnPanelWorking()
        {
            SuspendLayout();

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = $"Des cartes visuelles pour apprendre à écrire et à prononcer les mots en français et en anglais.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 88),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = $"Visual flashcards to learn how to write and pronounce words in French and English.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 128),
                AutoSize = true
            });

            int yPosition = 185;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);

            for (int i = 0; i < _sonFrancaisList.Count; i++)
            {
                int exampleCount = _sonFrancaisList[i].Exemples?.Count ?? 0;
                int cardHeight = 126 + (exampleCount * 42);

                Panel card = new Panel
                {
                    Tag = "son-dynamic",
                    BackColor = Color.FromArgb(248, 250, 252),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, cardHeight),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Panel accentBar = new Panel
                {
                    Tag = "son-dynamic",
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point(0, 0),
                    Size = new Size(12, cardHeight),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
                };

                Label header = new Label
                {
                    Tag = "",
                    Text = string.Join("   •   ", _sonFrancaisList[i].Epellation),
                    Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point(18, 16),
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6)
                };

                Label exampleCountLabel = new Label
                {
                    Tag = "fr",
                    Text = exampleCount <= 1 ? $"{exampleCount} exemple" : $"{exampleCount} exemples",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(30, 64, 175),
                    BackColor = Color.FromArgb(219, 234, 254),
                    AutoSize = true,
                    Padding = new Padding(10, 4, 10, 4),
                    Location = new Point(Math.Max(220, cardWidth - 150), 18)
                };

                card.Controls.Add(accentBar);
                card.Controls.Add(header);
                card.Controls.Add(exampleCountLabel);

                int exampleYPosition = 98;
                int englishXPosition = Math.Max(380, cardWidth / 2);

                for (int j = 0; j < _sonFrancaisList[i].Exemples!.Count; j++)
                {
                    Label frenchLabel = new Label
                    {
                        Tag = $"fr|{i}|{j}",
                        Text = string.Join("   •   ", _sonFrancaisList[i].Exemples[j].Fr),
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(17, 24, 39),
                        BackColor = Color.FromArgb(224, 231, 255),
                        Location = new Point(24, exampleYPosition),
                        AutoSize = true,
                        Padding = new Padding(10, 4, 10, 4)
                    };

                    Label englishLabel = new Label
                    {
                        Tag = $"en|{i}|{j}",
                        Text = _sonFrancaisList[i].Exemples[j].En,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(55, 65, 81),
                        Location = new Point(englishXPosition, exampleYPosition + 5),
                        AutoSize = true
                    };

                    card.Controls.Add(frenchLabel);
                    card.Controls.Add(englishLabel);

                    exampleYPosition += 42;
                }

                Controls.Add(card);
                yPosition += cardHeight + 18;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private void ClearDynamicLearningControls()
        {
            Control?[] staticControls = [lblApprendre, lblLearn, 
                comboBoxApprendre, panelImageSearch, 
                WebView2ImageSearch, checkBoxGetDataImage, WebView21, checkBoxAfficherImage,
                btnOptions, btnLanguage,panelOptions

            ];

            foreach (Control control in Controls.OfType<Control>().ToList())
            {
                if (staticControls.Contains(control))
                {
                    continue;
                }

                Controls.Remove(control);
                control.Dispose();
            }
        }

        private void ShowEmptySonFrancaisState()
        {
            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "Aucune phonétique français n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "No French phonetic is available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 145),
                AutoSize = true
            });
        }

        #endregion
    }
}

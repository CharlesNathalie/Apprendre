namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _soundEnglishFilePath = Path.Combine(AppApprendreDataFolderPath, "SoundEnglish.json");
        private List<SoundEnglish> _soundEnglishList { get; set; } = new List<SoundEnglish>();

        #endregion Properties

        #region Private

        private void LoadSoundEnglishList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_soundEnglishFilePath))
            {
                ShowEmptySoundEnglishState();
                return;
            }

            string json = File.ReadAllText(_soundEnglishFilePath);
            _soundEnglishList = JsonSerializer.Deserialize<List<SoundEnglish>>(json) ?? new List<SoundEnglish>();

            if (_soundEnglishList.Count == 0)
            {
                ShowEmptySoundEnglishState();
                return;
            }

            ShowSoundEnglishOnPanelWorking();
        }

        private void ShowSoundEnglishOnPanelWorking()
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

            for (int i = 0; i < _soundEnglishList.Count; i++)
            {
                int exampleCount = _soundEnglishList[i].Examples?.Count ?? 0;
                int cardHeight = 126 + (exampleCount * 42);

                Panel card = new Panel
                {
                    Tag = "sound-dynamic",
                    BackColor = Color.FromArgb(248, 250, 252),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, cardHeight),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Panel accentBar = new Panel
                {
                    Tag = "sound-dynamic",
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point(0, 0),
                    Size = new Size(12, cardHeight),
                    Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
                };

                Label header = new Label
                {
                    Tag = "",
                    Text = string.Join("   •   ", _soundEnglishList[i].Spelling),
                    Font = new Font("Segoe UI", 15F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point(18, 16),
                    AutoSize = true,
                    Padding = new Padding(12, 6, 12, 6)
                };

                Label exampleCountLabel = new Label
                {
                    Tag = "en",
                    Text = exampleCount <= 1 ? $"{exampleCount} example" : $"{exampleCount} examples",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(30, 64, 175),
                    BackColor = Color.FromArgb(219, 234, 254),
                    AutoSize = true,
                    Padding = new Padding(10, 4, 10, 4),
                    Location = new Point(Math.Max(220, cardWidth - 150), 18)
                };

                Label englishHeader = new Label
                {
                    Tag = "en",
                    Text = "English",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(30, 72),
                    AutoSize = true
                };

                Label frenchHeader = new Label
                {
                    Tag = "fr",
                    Text = "Français",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(Math.Max(380, cardWidth / 2), 72),
                    AutoSize = true
                };

                card.Controls.Add(accentBar);
                card.Controls.Add(header);
                card.Controls.Add(exampleCountLabel);
                card.Controls.Add(englishHeader);
                card.Controls.Add(frenchHeader);

                int exampleYPosition = 98;
                int frenchXPosition = Math.Max(380, cardWidth / 2);

                for (int j = 0; j < _soundEnglishList[i].Examples!.Count; j++)
                {
                    Label englishLabel = new Label
                    {
                        Tag = $"en|{i}|{j}",
                        Text = _soundEnglishList[i].Examples[j].En,
                        Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(17, 24, 39),
                        BackColor = Color.FromArgb(224, 231, 255),
                        Location = new Point(24, exampleYPosition),
                        AutoSize = true,
                        Padding = new Padding(10, 4, 10, 4)
                    };

                    Label frenchLabel = new Label
                    {
                        Tag = $"fr|{i}|{j}",
                        Text = _soundEnglishList[i].Examples[j].Fr,
                        Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(55, 65, 81),
                        Location = new Point(frenchXPosition, exampleYPosition + 5),
                        AutoSize = true
                    };

                    card.Controls.Add(englishLabel);
                    card.Controls.Add(frenchLabel);

                    exampleYPosition += 42;
                }

                Controls.Add(card);
                yPosition += cardHeight + 18;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private void ShowEmptySoundEnglishState()
        {
            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "No English phonetic is available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "Aucune phonétique anglaise n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 145),
                AutoSize = true
            });
        }

        #endregion Private

    }
}

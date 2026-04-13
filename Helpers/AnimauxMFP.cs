namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _animauxMFPFilePath = Path.Combine(AppApprendreDataFolderPath, "AnimauxMFP.json");

        #endregion Properties

        #region Private

        private void LoadAnimauxMFPList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_animauxMFPFilePath))
            {
                ShowEmptyAnimauxMFPState();
                return;
            }

            string json = File.ReadAllText(_animauxMFPFilePath);
            _animauxMFPList = JsonSerializer.Deserialize<List<AnimauxMFP>>(json) ?? new List<AnimauxMFP>();

            if (_animauxMFPList.Count == 0)
            {
                ShowEmptyAnimauxMFPState();
                return;
            }

            ShowAnimauxMFPOnPanelWorking();
        }

        private void ShowAnimauxMFPOnPanelWorking()
        {
            SuspendLayout();

            BackColor = Color.FromArgb(249, 250, 251);

            Controls.Add(new Label
            {
                Tag = $"fr|{-1}",
                Text = "Découvrir les animaux",
                Font = new Font("Segoe UI", 22F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(56, 72),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"fr|{-1}",
                Text = "Des cartes visuelles pour apprendre le mâle, la femelle, le petit et les mots anglais associés.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(58, 118),
                AutoSize = true
            });

            int yPosition = 182;
            int cardWidth = Math.Max(780, ClientSize.Width - 112);
            int englishXPosition = Math.Max(470, cardWidth / 2 + 36);

            for (int i = 0; i < _animauxMFPList.Count; i++)
            {
                Panel card = new Panel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(56, yPosition),
                    Size = new Size(cardWidth, 186),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Label labelMaleHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Mâle",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(28, 58),
                    AutoSize = true
                };

                Label labelMale = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Male,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(28, 82),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelFemelleHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Femelle",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(28, 128),
                    AutoSize = true
                };

                Label labelFemelle = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Female,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(28, 152),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelPetitHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Petit",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(206, 128),
                    AutoSize = true
                };

                Label labelPetit = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Petit,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(206, 152),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelAnimalHeader = new Label
                {
                    Tag = $"en|{i}",
                    Text = "Animal",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(englishXPosition, 58),
                    AutoSize = true
                };

                Label labelEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _animauxMFPList[i].En,
                    Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    BackColor = Color.FromArgb(243, 244, 246),
                    Location = new Point(englishXPosition, 82),
                    AutoSize = true
                    ,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelYoungHeader = new Label
                {
                    Tag = $"en|{i}",
                    Text = "Young",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(englishXPosition, 128),
                    AutoSize = true
                };

                Label labelYoung = new Label
                {
                    Tag = $"en|{i}",
                    Text = _animauxMFPList[i].Young,
                    Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    BackColor = Color.FromArgb(243, 244, 246),
                    Location = new Point(englishXPosition, 152),
                    AutoSize = true
                    ,
                    Padding = new Padding(14, 6, 14, 6)
                };

                if (!string.IsNullOrEmpty(_animauxMFPList[i].Url))
                {
                    Label labelImageFr = new Label
                    {
                        Tag = $"fr|{i}",
                        Text = "Image",
                        Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(75, 200, 99),
                        Location = new Point(30, 18),
                        AutoSize = true
                    };

                    card.Controls.Add(labelImageFr);
                }

                card.Controls.Add(labelMaleHeader);
                card.Controls.Add(labelMale);
                card.Controls.Add(labelFemelleHeader);
                card.Controls.Add(labelFemelle);
                card.Controls.Add(labelPetitHeader);
                card.Controls.Add(labelPetit);
                card.Controls.Add(labelAnimalHeader);
                card.Controls.Add(labelEn);
                card.Controls.Add(labelYoungHeader);
                card.Controls.Add(labelYoung);

                Controls.Add(card);

                yPosition += 206;
            }

            AutoScrollMinSize = new Size(0, yPosition + 32);
            ResumeLayout();
        }

        private void ShowEmptyAnimauxMFPState()
        {
            BackColor = Color.FromArgb(249, 250, 251);

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "Aucun animal n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(56, 112),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "No animals are available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(56, 142),
                AutoSize = true
            });

        }

        #endregion Private
    }
}

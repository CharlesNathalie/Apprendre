namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _animauxMFPFilePath = Path.Combine(AppApprendreDataFolderPath, "AnimauxMFP.json");

        #endregion Properties

        #region Private

        private void LoadAnimauxMFPList(
            string _fileName,
            string _groupeFr,
            string _groupeEn)
        {
            _dataList = new List<FrEnURL>();
            _animauxMFPList = new List<AnimauxMFP>();

            string _dataFilePath = Path.Combine(AppApprendreDataFolderPath, _fileName);

            ClearDynamicLearningControls();

            if (!File.Exists(_dataFilePath))
            {
                ShowEmptyAnimauxMFPState(_groupeFr, _groupeEn);
                return;
            }

            string json = File.ReadAllText(_animauxMFPFilePath);
            _animauxMFPList = JsonSerializer.Deserialize<List<AnimauxMFP>>(json) ?? new List<AnimauxMFP>();

            if (_animauxMFPList.Count == 0)
            {
                ShowEmptyAnimauxMFPState(_groupeFr, _groupeEn);
                return;
            }

            ShowAnimauxMFPOnPanelWorking(_animauxMFPList, "animaux", "animals");
        }

        private void ShowAnimauxMFPOnPanelWorking(List<AnimauxMFP> _animauxMFPList, string _groupeFr, string _groupeEn)
        {
            SuspendLayout();

            BackColor = Color.FromArgb(249, 250, 251);

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

            int yPosition = 188;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(440, cardWidth / 2 + 120);

            for (int i = 0; i < _animauxMFPList.Count; i++)
            {
                bool hasImage = !string.IsNullOrEmpty(_animauxMFPList[i].Url);

                Panel card = new Panel
                {
                    BackColor = Color.White,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(56, yPosition),
                    Size = new Size(cardWidth, 186),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                if (hasImage)
                {
                    Label labelImageFr = new Label
                    {
                        Tag = $"",
                        Text = "📷",
                        Font = new Font("Segoe UI Emoji", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(75, 200, 99),
                        Location = new Point(16, 18),
                        AutoSize = true
                    };

                    card.Controls.Add(labelImageFr);
                }

                Label labelMaleHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Mâle",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(46, 18),
                    AutoSize = true
                };


                Label labelMale = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Male,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Location = new Point(28, 42),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelFemelleHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Femelle",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(28, 88),
                    AutoSize = true
                };

                Label labelFemelle = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Female,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Location = new Point(28, 120),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelPetitHeader = new Label
                {
                    Tag = $"fr|{i}",
                    Text = "Petit",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(206, 88),
                    AutoSize = true
                };

                Label labelPetit = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _animauxMFPList[i].Petit,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    Location = new Point(206, 120),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

                Label labelAnimalHeader = new Label
                {
                    Tag = $"en|{i}",
                    Text = "Animal",
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(75, 85, 99),
                    Location = new Point(englishXPosition, 18),
                    AutoSize = true
                };

                Label labelEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _animauxMFPList[i].En,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(englishXPosition, 42),
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
                    Location = new Point(englishXPosition, 88),
                    AutoSize = true
                };

                Label labelYoung = new Label
                {
                    Tag = $"en|{i}",
                    Text = _animauxMFPList[i].Young,
                    Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(englishXPosition, 120),
                    AutoSize = true,
                    Padding = new Padding(14, 6, 14, 6)
                };

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

        private void ShowEmptyAnimauxMFPState(string _groupeFr, string _groupeEn)
        {
            BackColor = Color.FromArgb(249, 250, 251);

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = $"Aucun mot pour la catégorie {_groupeFr} n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(56, 112),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = $"No words from {_groupeEn} category are available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(56, 142),
                AutoSize = true
            });

        }

        #endregion Private
    }
}

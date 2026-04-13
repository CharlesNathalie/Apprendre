namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        #endregion Properties

        #region Private

        private void LoadDataList(
            string _fileName, 
            string _groupeFr,
            string _groupeEn) 
        {
            _dataList = new List<FrEnURL>();

            string _dataFilePath = Path.Combine(AppApprendreDataFolderPath, _fileName);

            ClearDynamicLearningControls();

            if (!File.Exists(_dataFilePath))
            {
                ShowEmptyFruitState(_groupeFr, _groupeEn);
                return;
            }

            string json = File.ReadAllText(_dataFilePath);
            _dataList = JsonSerializer.Deserialize<List<FrEnURL>>(json) ?? new List<FrEnURL>();

            if (_dataList.Count == 0)
            {
                ShowEmptyFruitState(_groupeFr, _groupeEn);
                return;
            }

            ShowFruitOnPanelWorking(_dataList, _groupeFr, _groupeEn);
        }

        private void ShowFruitOnPanelWorking(List<FrEnURL> _dataList, string _groupeFr, string _groupeEn)
        {
            SuspendLayout();

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = $"Découvrir les {_groupeFr}",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 68),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = $"Des cartes visuelles pour apprendre facilement les mots des {_groupeFr} en français et en anglais.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 108),
                AutoSize = true
            });


            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = $"Discovering {_groupeEn}",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 128),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = $"Visual cards to easily learn the words of {_groupeEn} in French and English.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 138),
                Location = new Point(52, 168),
                AutoSize = true
            });

            int yPosition = 188;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(440, (cardWidth / 2) + 120);

            for (int i = 0; i < _dataList.Count; i++)
            {
                bool hasImage = !string.IsNullOrEmpty(_dataList[i].Url);

                Panel card = new Panel
                {
                    BackColor = Color.FromArgb(248, 250, 252),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, 98),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Label labelFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _dataList[i].Fr,
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(hasImage ? 48 : 24, 34),
                    AutoSize = true,
                    Padding = new Padding(10, 4, 10, 4)
                };

                Label labelEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _dataList[i].En,
                    Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    Location = new Point(englishXPosition, 48),
                    AutoSize = true
                };

                if (hasImage)
                {
                    Label labelImageFr = new Label
                    {
                        Tag = $"",
                        Text = "📷",
                        Font = new Font("Segoe UI Emoji", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                        ForeColor = Color.FromArgb(75, 200, 99),
                        Location = new Point(16, 36),
                        AutoSize = true
                    };

                    card.Controls.Add(labelImageFr);
                }

                card.Controls.Add(labelFr);


                card.Controls.Add(labelEn);

                Controls.Add(card);

                yPosition += 116;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private void ShowEmptyFruitState(string _groupeFr, string _groupeEn)
        {
            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = $"Aucun mot pour la catégorie {_groupeFr} n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = $"No word from {_groupeEn} category is available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 145),
                AutoSize = true
            });
        }

        #endregion Private
    }
}

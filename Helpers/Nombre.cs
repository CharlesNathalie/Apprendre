namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _nombreFilePath = Path.Combine(AppApprendreDataFolderPath, "Nombre.json");
        private List<Nombre> _nombreList { get; set; } = new List<Nombre>();

        #endregion Properties

        #region Private

        private void LoadNombreList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_nombreFilePath))
            {
                ShowEmptyNombresState();
                return;
            }

            string json = File.ReadAllText(_nombreFilePath);
            _nombreList = JsonSerializer.Deserialize<List<Nombre>>(json) ?? new List<Nombre>();

            if (_nombreList.Count == 0)
            {
                ShowEmptyNombresState();
                return;
            }

            ShowNombresOnPanelWorking();
        }

        private void ShowNombresOnPanelWorking()
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

            int yPosition = 165;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(430, (cardWidth / 2) + 70);

            for (int i = 0; i < _nombreList.Count; i++)
            {
                Font badgeFont = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
                int badgeHorizontalPadding = 18;
                int badgeWidth = Math.Max(
                    132,
                    TextRenderer.MeasureText(
                        _nombreList[i].NombreValue,
                        badgeFont,
                        Size.Empty,
                        TextFormatFlags.SingleLine | TextFormatFlags.NoPadding).Width + (badgeHorizontalPadding * 2));
                int contentXPosition = 24 + badgeWidth + 24;

                Panel card = new Panel
                {
                    BackColor = Color.FromArgb(248, 250, 252),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, 106),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Panel badge = new Panel
                {
                    BackColor = Color.FromArgb(37, 99, 235),
                    Location = new Point(24, 22),
                    Size = new Size(badgeWidth, 56),
                    Padding = new Padding(badgeHorizontalPadding, 0, badgeHorizontalPadding, 0)
                };

                Label labelNombre = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _nombreList[i].NombreValue,
                    Font = badgeFont,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(37, 99, 235),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label labelFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _nombreList[i].Fr,
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(contentXPosition, 46),
                    AutoSize = true,
                    Padding = new Padding(10, 4, 10, 4)
                };

                Label labelEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _nombreList[i].En,
                    Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    Location = new Point(englishXPosition, 50),
                    AutoSize = true
                };

                badge.Controls.Add(labelNombre);

                card.Controls.Add(badge);
                card.Controls.Add(labelFr);
                card.Controls.Add(labelEn);

                Controls.Add(card);

                yPosition += 124;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private void ShowEmptyNombresState()
        {
            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Aucun nombre n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Ajoutez des données dans le fichier JSON pour afficher les cartes d'apprentissage.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 142),
                AutoSize = true
            });
        }

        #endregion Private
    }
}

namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _nombreRomainFilePath = Path.Combine(AppApprendreDataFolderPath, "NombreRomain.json");
        private List<NombreRomain> _nombresRomainList { get; set; } = new List<NombreRomain>();

        #endregion Properties

        #region Private

        private void LoadNombresRomainList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_nombreRomainFilePath))
            {
                ShowEmptyNombresRomainState();
                return;
            }

            string json = File.ReadAllText(_nombreRomainFilePath);
            _nombresRomainList = JsonSerializer.Deserialize<List<NombreRomain>>(json) ?? new List<NombreRomain>();

            if (_nombresRomainList.Count == 0)
            {
                ShowEmptyNombresRomainState();
                return;
            }

            ShowNombresRomainOnPanelWorking();
        }

        private void ShowNombresRomainOnPanelWorking()
        {
            SuspendLayout();

            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Découvrir les nombres romains",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 78),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Des cartes visuelles pour associer chaque nombre romain à sa lecture en français et en anglais.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 118),
                AutoSize = true
            });

            int yPosition = 165;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(430, (cardWidth / 2) + 70);

            for (int i = 0; i < _nombresRomainList.Count; i++)
            {
                Font badgeFont = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0);
                int badgeHorizontalPadding = 28;
                int badgeWidth = Math.Max(
                    170,
                    TextRenderer.MeasureText(
                        _nombresRomainList[i].NombreValue,
                        badgeFont,
                        Size.Empty,
                        TextFormatFlags.SingleLine | TextFormatFlags.NoPadding).Width + (badgeHorizontalPadding * 2) + 24);
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
                    Text = _nombresRomainList[i].NombreValue,
                    Font = badgeFont,
                    ForeColor = Color.White,
                    BackColor = Color.FromArgb(37, 99, 235),
                    Dock = DockStyle.Fill,
                    TextAlign = ContentAlignment.MiddleCenter
                };

                Label labelFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _nombresRomainList[i].Fr,
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
                    Text = _nombresRomainList[i].En,
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

        private void ShowEmptyNombresRomainState()
        {
            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Aucun nombre romain n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "en",
                Text = "No Roman numbers are available at the moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });
        }

        #endregion Private
    }
}

using Apprendre.Models;

namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private readonly string _colorNameFilePath = Path.Combine(AppApprendreDataFolderPath, "ColorName.json");
        private List<ColorName> _colorNameList { get; set; } = new List<ColorName>();

        #endregion Properties

        #region Private

        private void LoadColorNameList()
        {
            ClearDynamicLearningControls();

            if (!File.Exists(_colorNameFilePath))
            {
                ShowEmptyColorNamesState();
                return;
            }

            string json = File.ReadAllText(_colorNameFilePath);
            _colorNameList = JsonSerializer.Deserialize<List<ColorName>>(json) ?? new List<ColorName>();

            if (_colorNameList.Count == 0)
            {
                ShowEmptyColorNamesState();
                return;
            }

            ShowColorNamesOnPanelWorking();
        }

        private void ShowColorNamesOnPanelWorking()
        {
            SuspendLayout();

            Controls.Add(new Label
            {
                Tag = $"fr|{-1}",
                Text = "Découvrir les couleurs",
                Font = new Font("Segoe UI", 20F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 78),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"fr|{-1}",
                Text = "Une présentation visuelle pour associer rapidement la couleur, le mot français et le mot anglais.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 118),
                AutoSize = true
            });

            int yPosition = 165;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);

            for (int i = 0; i < _colorNameList.Count; i++)
            {
                Color displayColor = GetColorFromValue(_colorNameList[i].ColorNameValue);
                int englishXPosition = Math.Max(390, cardWidth / 2);

                Panel card = new Panel
                {
                    Tag = $"fr|{i}",
                    BackColor = Color.FromArgb(248, 250, 252),
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(50, yPosition),
                    Size = new Size(cardWidth, 110),
                    Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
                };

                Panel swatch = new Panel
                {
                    Tag = $"fr|{i}",
                    BackColor = displayColor,
                    BorderStyle = BorderStyle.FixedSingle,
                    Location = new Point(24, 24),
                    Size = new Size(72, 72)
                };

                Label hexLabel = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _colorNameList[i].ColorNameValue,
                    Font = new Font("Segoe UI", 9.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = GetReadableTextColor(displayColor),
                    BackColor = displayColor,
                    Location = new Point(30, 49),
                    AutoSize = true
                };

                Label labelFr = new Label
                {
                    Tag = $"fr|{i}",
                    Text = _colorNameList[i].Fr,
                    Font = new Font("Segoe UI", 13F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(17, 24, 39),
                    BackColor = Color.FromArgb(224, 231, 255),
                    Location = new Point(125, 49),
                    AutoSize = true,
                    Padding = new Padding(10, 4, 10, 4)
                };

                Label labelEn = new Label
                {
                    Tag = $"en|{i}",
                    Text = _colorNameList[i].En,
                    Font = new Font("Segoe UI", 13F, FontStyle.Regular, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(55, 65, 81),
                    Location = new Point(englishXPosition, 53),
                    AutoSize = true
                };

                card.Controls.Add(swatch);
                card.Controls.Add(hexLabel);
                card.Controls.Add(labelFr);
                card.Controls.Add(labelEn);

                Controls.Add(card);

                yPosition += 128;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private static Color GetColorFromValue(string colorValue)
        {
            try
            {
                return ColorTranslator.FromHtml(colorValue);
            }
            catch
            {
                return SystemColors.Control;
            }
        }

        private static Color GetReadableTextColor(Color backgroundColor)
        {
            double brightness = (backgroundColor.R * 0.299) + (backgroundColor.G * 0.587) + (backgroundColor.B * 0.114);

            return brightness > 186 ? Color.FromArgb(17, 24, 39) : Color.White;
        }

        private void ShowEmptyColorNamesState()
        {
            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Aucune couleur n'est disponible pour le moment.",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 105),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "fr",
                Text = "Ajoutez des données dans le fichier JSON pour afficher les cartes de couleurs.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(107, 114, 128),
                Location = new Point(52, 142),
                AutoSize = true
            });
        }

        #endregion Private
    }
}

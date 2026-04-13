namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        #endregion Properties

        #region Constructors
        public class ABC
        {

        }

        #endregion Constructors

        #region Private

        private void LoadABC()
        {
            ClearDynamicLearningControls();

            ShowABCOnPanelWorking();
        }

        private void ShowABCOnPanelWorking()
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

            Controls.Add(new Label
            {
                Tag = $"fr",
                Text = "Français",
                Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 232),
                AutoSize = true
            });

            for (int i = 0; i < 26; i++)
            {
                char letter = (char)('A' + i);
                Controls.Add(new Label
                {
                    Tag = $"fr",
                    Text = $" {letter} ",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(50 + (i * 34), 262),
                    AutoSize = true
                });
            }

            for (int i = 0; i < 26; i++)
            {
                char letter = (char)('a' + i);
                Controls.Add(new Label
                {
                    Tag = $"fr",
                    Text = $" {letter} ",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(50 + (i * 34), 294),
                    AutoSize = true
                });
            }

            string allLetters = string.Join(" ", Enumerable.Range(0, 26).Select(i => $"{(char)('A' + i)} "));

            Controls.Add(new Label
            {
                Tag = $"fr",
                Text = $" {allLetters} ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 332),
                AutoSize = true
            });

            Controls.Add(new Button
            {
                Name = "btnPlayAudio",
                Text = $" Multimedia ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 382),
                AutoSize = true,
                
            });

            Controls.Add(new Label
            {
                Tag = $"en",
                Text = "English",
                Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 442),
                AutoSize = true
            });

            for (int i = 0; i < 26; i++)
            {
                char letter = (char)('A' + i);
                Controls.Add(new Label
                {
                    Tag = $"en",
                    Text = $" {letter} ",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(50 + (i * 34), 472),
                    AutoSize = true
                });
            }

            for (int i = 0; i < 26; i++)
            {
                char letter = (char)('a' + i);
                Controls.Add(new Label
                {
                    Tag = $"en",
                    Text = $" {letter} ",
                    Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    ForeColor = Color.FromArgb(31, 41, 55),
                    Location = new Point(50 + (i * 34), 514),
                    AutoSize = true
                });
            }

            Controls.Add(new Label
            {
                Tag = $"en",
                Text = $" {allLetters} ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 552),
                AutoSize = true
            });


            int yPosition = 165;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(430, (cardWidth / 2) + 70);

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        #endregion Private

    }
}

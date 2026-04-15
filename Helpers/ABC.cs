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
                Location = new Point(50, 192),
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
                    Location = new Point(50 + (i * 34), 222),
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
                    Location = new Point(50 + (i * 34), 254),
                    AutoSize = true
                });
            }

            string allLetters = string.Join(" ", Enumerable.Range(0, 26).Select(i => $"{(char)('A' + i)}, "));

            Controls.Add(new Label
            {
                Tag = $"fr",
                Text = $" {allLetters} ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 292),
                AutoSize = true
            });

            LinkLabel abc_frAudioLink = new LinkLabel
            {
                Name = "lnkPlayAudio_FR",
                Text = "ABC_FR.mp3",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                LinkColor = Color.RoyalBlue,
                ActiveLinkColor = Color.FromArgb(30, 64, 175),
                VisitedLinkColor = Color.RoyalBlue,
                Location = new Point(50, 342),
                AutoSize = true,
                Tag = AbcFrenchAudioFilePath
            };
            abc_frAudioLink.LinkClicked += Abc_frAudioLink_LinkClicked;
            Controls.Add(abc_frAudioLink);

            Controls.Add(new Label
            {
                Tag = $"en",
                Text = "English",
                Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 412),
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
                    Location = new Point(50 + (i * 34), 442),
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
                    Location = new Point(50 + (i * 34), 484),
                    AutoSize = true
                });
            }

            Controls.Add(new Label
            {
                Tag = $"en",
                Text = $" {allLetters} ",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(31, 41, 55),
                Location = new Point(50, 522),
                AutoSize = true
            });


            LinkLabel abc_enAudioLink = new LinkLabel
            {
                Name = "lnkPlayAudio_EN",
                Text = "ABC_EN.mp3",
                Font = new Font("Segoe UI", 16F, FontStyle.Bold, GraphicsUnit.Point, 0),
                LinkColor = Color.RoyalBlue,
                ActiveLinkColor = Color.FromArgb(30, 64, 175),
                VisitedLinkColor = Color.RoyalBlue,
                Location = new Point(50, 582),
                AutoSize = true,
                Tag = AbcEnglishAudioFilePath
            };
            abc_enAudioLink.LinkClicked += Abc_enAudioLink_LinkClicked;
            Controls.Add(abc_enAudioLink);


            int yPosition = 165;
            int cardWidth = Math.Max(760, ClientSize.Width - 100);
            int englishXPosition = Math.Max(430, (cardWidth / 2) + 70);

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private static void Abc_frAudioLink_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is not LinkLabel linkLabel || linkLabel.Tag is not string audioFilePath || !File.Exists(audioFilePath))
            {
                return;
            }

            linkLabel.LinkVisited = true;

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = audioFilePath,
                    UseShellExecute = true
                });
            }
            catch
            {
            }
        }

        private static void Abc_enAudioLink_LinkClicked(object? sender, LinkLabelLinkClickedEventArgs e)
        {
            if (sender is not LinkLabel linkLabel || linkLabel.Tag is not string audioFilePath || !File.Exists(audioFilePath))
            {
                return;
            }

            linkLabel.LinkVisited = true;

            try
            {
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = audioFilePath,
                    UseShellExecute = true
                });
            }
            catch
            {
            }
        }

        #endregion Private

    }
}

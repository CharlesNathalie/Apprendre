namespace Apprendre
{
    public partial class Apprendre
    {
        #region Properties

        private sealed record ConjugationRow(string PronounFr, string PronounEn, string FrenchText, string EnglishText);
        private sealed record TenseSection(string TenseFr, string TenseEn, IReadOnlyList<ConjugationRow> Rows);
        private sealed record VerbSection(string VerbFr, string VerbEn, IReadOnlyList<TenseSection> Tenses);

        #endregion Properties

        #region Constructors

        #endregion Constructors

        #region Private

        private void LoadVerbeConjuger()
        {
            ClearDynamicLearningControls();

            ShowVerbeConjugerOnPanelWorking();
        }

        private void ShowVerbeConjugerOnPanelWorking()
        {
            SuspendLayout();

            int contentLeft = 50;
            int availableWidth = Math.Max(320, ClientSize.Width - 100);

            Controls.Add(new Label
            {
                Tag = $"fr|-1",
                Text = "Des fiches simples pour apprendre les pronoms et la conjugaison en français et en anglais.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 88),
                MaximumSize = new Size(availableWidth, 0),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = $"en|-1",
                Text = "Simple cards to learn pronouns and verb conjugation in French and English.",
                Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(52, 128),
                MaximumSize = new Size(availableWidth, 0),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "fr|-1",
                Text = "Choisissez un verbe pour aller directement à sa conjugaison.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(52, 166),
                MaximumSize = new Size(availableWidth, 0),
                AutoSize = true
            });

            Controls.Add(new Label
            {
                Tag = "en|-1",
                Text = "Choose a verb to jump directly to its conjugation.",
                Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(52, 194),
                MaximumSize = new Size(availableWidth, 0),
                AutoSize = true
            });

            IReadOnlyList<VerbSection> sections = GetVerbSections();
            List<Control> sectionAnchors = [];
            int navigationButtonWidth = Math.Min(190, Math.Max(150, (availableWidth - 24) / 3));
            int navigationButtonHeight = 38;
            int navigationButtonSpacing = 12;
            int navigationButtonX = contentLeft;
            int navigationButtonY = 236;

            for (int i = 0; i < sections.Count; i++)
            {
                if (navigationButtonX + navigationButtonWidth > contentLeft + availableWidth)
                {
                    navigationButtonX = contentLeft;
                    navigationButtonY += navigationButtonHeight + navigationButtonSpacing;
                }

                int sectionIndex = i;
                Button verbButton = new Button
                {
                    Text = $"{sections[i].VerbFr} / {sections[i].VerbEn}",
                    Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
                    BackColor = Color.FromArgb(219, 234, 254),
                    ForeColor = Color.FromArgb(30, 64, 175),
                    FlatStyle = FlatStyle.Flat,
                    Location = new Point(navigationButtonX, navigationButtonY),
                    Size = new Size(navigationButtonWidth, navigationButtonHeight),
                    Cursor = Cursors.Hand,
                    UseVisualStyleBackColor = false
                };

                verbButton.FlatAppearance.BorderColor = Color.FromArgb(147, 197, 253);
                verbButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(191, 219, 254);
                verbButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(191, 219, 254);
                verbButton.Click += (_, _) =>
                {
                    if (sectionAnchors.Count > sectionIndex)
                    {
                        ScrollControlIntoView(sectionAnchors[sectionIndex]);
                    }
                };

                Controls.Add(verbButton);
                navigationButtonX += navigationButtonWidth + navigationButtonSpacing;
            }

            int yPosition = navigationButtonY + navigationButtonHeight + 24;
            int sectionWidth = availableWidth;

            for (int sectionIndex = 0; sectionIndex < sections.Count; sectionIndex++)
            {
                Panel sectionPanel = CreateVerbSectionPanel(sections[sectionIndex], sectionIndex, yPosition, sectionWidth);
                sectionAnchors.Add(sectionPanel);
                Controls.Add(sectionPanel);
                yPosition += sectionPanel.Height + 22;
            }

            AutoScrollMinSize = new Size(0, yPosition + 40);
            ResumeLayout();
        }

        private Panel CreateVerbSectionPanel(VerbSection section, int sectionIndex, int yPosition, int sectionWidth)
        {
            int tenseCardHeight = 288;
            int sectionHeight = 88 + (section.Tenses.Count * tenseCardHeight) + ((section.Tenses.Count - 1) * 16) + 18;

            Panel sectionPanel = new Panel
            {
                BackColor = Color.FromArgb(248, 250, 252),
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(50, yPosition),
                Size = new Size(sectionWidth, sectionHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Panel accentBar = new Panel
            {
                BackColor = Color.FromArgb(37, 99, 235),
                Location = new Point(0, 0),
                Size = new Size(12, sectionHeight),
                Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left
            };

            Label sectionTitle = new Label
            {
                Tag = $"fr|{sectionIndex}",
                Text = section.VerbFr,
                Font = new Font("Segoe UI", 17F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(17, 24, 39),
                Location = new Point(26, 22),
                AutoSize = true
            };

            Label sectionSubtitle = new Label
            {
                Tag = $"en|{sectionIndex}",
                Text = section.VerbEn,
                Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(55, 65, 81),
                Location = new Point(28, 54),
                AutoSize = true
            };

            sectionPanel.Controls.Add(accentBar);
            sectionPanel.Controls.Add(sectionTitle);
            sectionPanel.Controls.Add(sectionSubtitle);

            int tenseYPosition = 88;
            int tenseWidth = sectionWidth - 36;

            for (int tenseIndex = 0; tenseIndex < section.Tenses.Count; tenseIndex++)
            {
                Panel tensePanel = CreateTensePanel(section.Tenses[tenseIndex], sectionIndex, tenseIndex, tenseYPosition, tenseWidth);
                sectionPanel.Controls.Add(tensePanel);
                tenseYPosition += tensePanel.Height + 16;
            }

            return sectionPanel;
        }

        private Panel CreateTensePanel(TenseSection tense, int sectionIndex, int tenseIndex, int yPosition, int panelWidth)
        {
            Panel tensePanel = new Panel
            {
                BackColor = Color.White,
                BorderStyle = BorderStyle.FixedSingle,
                Location = new Point(18, yPosition),
                Size = new Size(panelWidth, 288),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
            };

            Label tenseFrenchLabel = new Label
            {
                Tag = $"fr|{sectionIndex}|{tenseIndex}",
                Text = tense.TenseFr,
                Font = new Font("Segoe UI", 12.5F, FontStyle.Bold, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(30, 64, 175),
                Location = new Point(20, 18),
                AutoSize = true
            };

            Label tenseEnglishLabel = new Label
            {
                Tag = $"en|{sectionIndex}|{tenseIndex}",
                Text = tense.TenseEn,
                Font = new Font("Segoe UI", 10.5F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = Color.FromArgb(75, 85, 99),
                Location = new Point(22, 46),
                AutoSize = true
            };

            tensePanel.Controls.Add(tenseFrenchLabel);
            tensePanel.Controls.Add(tenseEnglishLabel);

            TableLayoutPanel conjugationTable = new TableLayoutPanel
            {
                Location = new Point(18, 84),
                Size = new Size(panelWidth - 36, 182),
                Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
                ColumnCount = 4,
                RowCount = tense.Rows.Count + 1,
                CellBorderStyle = TableLayoutPanelCellBorderStyle.Single,
                BackColor = Color.FromArgb(226, 232, 240),
                Padding = new Padding(0),
                Margin = new Padding(0)
            };

            conjugationTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            conjugationTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            conjugationTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 34F));
            conjugationTable.ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 46F));
            conjugationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 28F));

            for (int rowIndex = 0; rowIndex < tense.Rows.Count; rowIndex++)
            {
                conjugationTable.RowStyles.Add(new RowStyle(SizeType.Absolute, 25F));
            }

            conjugationTable.Controls.Add(CreateConjugationCellLabel("fr", "Pronoms", true), 0, 0);
            conjugationTable.Controls.Add(CreateConjugationCellLabel("en", "Pronouns", true), 1, 0);
            conjugationTable.Controls.Add(CreateConjugationCellLabel("fr", "Français", true), 2, 0);
            conjugationTable.Controls.Add(CreateConjugationCellLabel("en", "English", true), 3, 0);

            for (int rowIndex = 0; rowIndex < tense.Rows.Count; rowIndex++)
            {
                ConjugationRow row = tense.Rows[rowIndex];

                conjugationTable.Controls.Add(CreateConjugationCellLabel($"fr|{sectionIndex}|{tenseIndex}|{rowIndex}", row.PronounFr, true), 0, rowIndex + 1);
                conjugationTable.Controls.Add(CreateConjugationCellLabel($"en|{sectionIndex}|{tenseIndex}|{rowIndex}", row.PronounEn, false), 1, rowIndex + 1);
                conjugationTable.Controls.Add(CreateConjugationCellLabel($"fr|{sectionIndex}|{tenseIndex}|{rowIndex}|verb", row.FrenchText, true), 2, rowIndex + 1);
                conjugationTable.Controls.Add(CreateConjugationCellLabel($"en|{sectionIndex}|{tenseIndex}|{rowIndex}|verb", row.EnglishText, false), 3, rowIndex + 1);
            }

            tensePanel.Controls.Add(conjugationTable);

            return tensePanel;
        }

        private static Label CreateConjugationCellLabel(string tag, string text, bool isFrench)
        {
            return new Label
            {
                Tag = tag,
                Text = text,
                Dock = DockStyle.Fill,
                Margin = new Padding(0),
                Padding = new Padding(6, 4, 6, 4),
                TextAlign = ContentAlignment.MiddleLeft,
                Font = isFrench
                    ? new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0)
                    : new Font("Segoe UI", 9.5F, FontStyle.Regular, GraphicsUnit.Point, 0),
                ForeColor = isFrench ? Color.FromArgb(17, 24, 39) : Color.FromArgb(55, 65, 81),
                BackColor = Color.White
            };
        }

        private static IReadOnlyList<VerbSection> GetVerbSections()
        {
            return
            [
                new VerbSection(
                    "Avoir",
                    "To have",
                    [
                        new TenseSection(
                            "Présent",
                            "Present",
                            [
                                new ConjugationRow("Je", "I", "j'ai", "I have"),
                                new ConjugationRow("Tu", "You", "tu as", "you have"),
                                new ConjugationRow("Il / Elle", "He / She", "il a / elle a", "he has / she has"),
                                new ConjugationRow("Nous", "We", "nous avons", "we have"),
                                new ConjugationRow("Vous", "You", "vous avez", "you have"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont / elles ont", "they have")
                            ]),
                        new TenseSection(
                            "Imparfait",
                            "Imperfect",
                            [
                                new ConjugationRow("Je", "I", "j'avais", "I had"),
                                new ConjugationRow("Tu", "You", "tu avais", "you had"),
                                new ConjugationRow("Il / Elle", "He / She", "il avait / elle avait", "he had / she had"),
                                new ConjugationRow("Nous", "We", "nous avions", "we had"),
                                new ConjugationRow("Vous", "You", "vous aviez", "you had"),
                                new ConjugationRow("Ils / Elles", "They", "ils avaient / elles avaient", "they had")
                            ]),
                        new TenseSection(
                            "Passé composé",
                            "Present perfect",
                            [
                                new ConjugationRow("Je", "I", "j'ai eu", "I have had"),
                                new ConjugationRow("Tu", "You", "tu as eu", "you have had"),
                                new ConjugationRow("Il / Elle", "He / She", "il a eu / elle a eu", "he has had / she has had"),
                                new ConjugationRow("Nous", "We", "nous avons eu", "we have had"),
                                new ConjugationRow("Vous", "You", "vous avez eu", "you have had"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont eu / elles ont eu", "they have had")
                            ])
                    ]),
                new VerbSection(
                    "Être",
                    "To be",
                    [
                        new TenseSection(
                            "Présent",
                            "Present",
                            [
                                new ConjugationRow("Je", "I", "je suis", "I am"),
                                new ConjugationRow("Tu", "You", "tu es", "you are"),
                                new ConjugationRow("Il / Elle", "He / She", "il est / elle est", "he is / she is"),
                                new ConjugationRow("Nous", "We", "nous sommes", "we are"),
                                new ConjugationRow("Vous", "You", "vous êtes", "you are"),
                                new ConjugationRow("Ils / Elles", "They", "ils sont / elles sont", "they are")
                            ]),
                        new TenseSection(
                            "Imparfait",
                            "Imperfect",
                            [
                                new ConjugationRow("Je", "I", "j'étais", "I was"),
                                new ConjugationRow("Tu", "You", "tu étais", "you were"),
                                new ConjugationRow("Il / Elle", "He / She", "il était / elle était", "he was / she was"),
                                new ConjugationRow("Nous", "We", "nous étions", "we were"),
                                new ConjugationRow("Vous", "You", "vous étiez", "you were"),
                                new ConjugationRow("Ils / Elles", "They", "ils étaient / elles étaient", "they were")
                            ]),
                        new TenseSection(
                            "Passé composé",
                            "Present perfect",
                            [
                                new ConjugationRow("Je", "I", "j'ai été", "I have been"),
                                new ConjugationRow("Tu", "You", "tu as été", "you have been"),
                                new ConjugationRow("Il / Elle", "He / She", "il a été / elle a été", "he has been / she has been"),
                                new ConjugationRow("Nous", "We", "nous avons été", "we have been"),
                                new ConjugationRow("Vous", "You", "vous avez été", "you have been"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont été / elles ont été", "they have been")
                            ])
                    ]),
                new VerbSection(
                    "Être aimé",
                    "To be loved",
                    [
                        new TenseSection(
                            "Présent",
                            "Present",
                            [
                                new ConjugationRow("Je", "I", "je suis aimé", "I am loved"),
                                new ConjugationRow("Tu", "You", "tu es aimé", "you are loved"),
                                new ConjugationRow("Il / Elle", "He / She", "il est aimé / elle est aimée", "he is loved / she is loved"),
                                new ConjugationRow("Nous", "We", "nous sommes aimés", "we are loved"),
                                new ConjugationRow("Vous", "You", "vous êtes aimés", "you are loved"),
                                new ConjugationRow("Ils / Elles", "They", "ils sont aimés / elles sont aimées", "they are loved")
                            ]),
                        new TenseSection(
                            "Imparfait",
                            "Imperfect",
                            [
                                new ConjugationRow("Je", "I", "j'étais aimé", "I was loved"),
                                new ConjugationRow("Tu", "You", "tu étais aimé", "you were loved"),
                                new ConjugationRow("Il / Elle", "He / She", "il était aimé / elle était aimée", "he was loved / she was loved"),
                                new ConjugationRow("Nous", "We", "nous étions aimés", "we were loved"),
                                new ConjugationRow("Vous", "You", "vous étiez aimés", "you were loved"),
                                new ConjugationRow("Ils / Elles", "They", "ils étaient aimés / elles étaient aimées", "they were loved")
                            ]),
                        new TenseSection(
                            "Passé composé",
                            "Present perfect",
                            [
                                new ConjugationRow("Je", "I", "j'ai été aimé", "I have been loved"),
                                new ConjugationRow("Tu", "You", "tu as été aimé", "you have been loved"),
                                new ConjugationRow("Il / Elle", "He / She", "il a été aimé / elle a été aimée", "he has been loved / she has been loved"),
                                new ConjugationRow("Nous", "We", "nous avons été aimés", "we have been loved"),
                                new ConjugationRow("Vous", "You", "vous avez été aimés", "you have been loved"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont été aimés / elles ont été aimées", "they have been loved")
                            ])
                    ]),
                new VerbSection(
                    "Aimer",
                    "To love",
                    [
                        new TenseSection(
                            "Présent",
                            "Present",
                            [
                                new ConjugationRow("Je", "I", "j'aime", "I love"),
                                new ConjugationRow("Tu", "You", "tu aimes", "you love"),
                                new ConjugationRow("Il / Elle", "He / She", "il aime / elle aime", "he loves / she loves"),
                                new ConjugationRow("Nous", "We", "nous aimons", "we love"),
                                new ConjugationRow("Vous", "You", "vous aimez", "you love"),
                                new ConjugationRow("Ils / Elles", "They", "ils aiment / elles aiment", "they love")
                            ]),
                        new TenseSection(
                            "Imparfait",
                            "Imperfect",
                            [
                                new ConjugationRow("Je", "I", "j'aimais", "I loved"),
                                new ConjugationRow("Tu", "You", "tu aimais", "you loved"),
                                new ConjugationRow("Il / Elle", "He / She", "il aimait / elle aimait", "he loved / she loved"),
                                new ConjugationRow("Nous", "We", "nous aimions", "we loved"),
                                new ConjugationRow("Vous", "You", "vous aimiez", "you loved"),
                                new ConjugationRow("Ils / Elles", "They", "ils aimaient / elles aimaient", "they loved")
                            ]),
                        new TenseSection(
                            "Passé composé",
                            "Present perfect",
                            [
                                new ConjugationRow("Je", "I", "j'ai aimé", "I have loved"),
                                new ConjugationRow("Tu", "You", "tu as aimé", "you have loved"),
                                new ConjugationRow("Il / Elle", "He / She", "il a aimé / elle a aimé", "he has loved / she has loved"),
                                new ConjugationRow("Nous", "We", "nous avons aimé", "we have loved"),
                                new ConjugationRow("Vous", "You", "vous avez aimé", "you have loved"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont aimé / elles ont aimé", "they have loved")
                            ])
                    ]),
                new VerbSection(
                    "Manger",
                    "To eat",
                    [
                        new TenseSection(
                            "Présent",
                            "Present",
                            [
                                new ConjugationRow("Je", "I", "je mange", "I eat"),
                                new ConjugationRow("Tu", "You", "tu manges", "you eat"),
                                new ConjugationRow("Il / Elle", "He / She", "il mange / elle mange", "he eats / she eats"),
                                new ConjugationRow("Nous", "We", "nous mangeons", "we eat"),
                                new ConjugationRow("Vous", "You", "vous mangez", "you eat"),
                                new ConjugationRow("Ils / Elles", "They", "ils mangent / elles mangent", "they eat")
                            ]),
                        new TenseSection(
                            "Imparfait",
                            "Imperfect",
                            [
                                new ConjugationRow("Je", "I", "je mangeais", "I was eating"),
                                new ConjugationRow("Tu", "You", "tu mangeais", "you were eating"),
                                new ConjugationRow("Il / Elle", "He / She", "il mangeait / elle mangeait", "he was eating / she was eating"),
                                new ConjugationRow("Nous", "We", "nous mangions", "we were eating"),
                                new ConjugationRow("Vous", "You", "vous mangiez", "you were eating"),
                                new ConjugationRow("Ils / Elles", "They", "ils mangeaient / elles mangeaient", "they were eating")
                            ]),
                        new TenseSection(
                            "Passé composé",
                            "Present perfect",
                            [
                                new ConjugationRow("Je", "I", "j'ai mangé", "I have eaten"),
                                new ConjugationRow("Tu", "You", "tu as mangé", "you have eaten"),
                                new ConjugationRow("Il / Elle", "He / She", "il a mangé / elle a mangé", "he has eaten / she has eaten"),
                                new ConjugationRow("Nous", "We", "nous avons mangé", "we have eaten"),
                                new ConjugationRow("Vous", "You", "vous avez mangé", "you have eaten"),
                                new ConjugationRow("Ils / Elles", "They", "ils ont mangé / elles ont mangé", "they have eaten")
                            ])
                    ])
            ];
        }

        #endregion Private

    }
}

namespace Apprendre;

public partial class Apprendre
{
    #region Properties


    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Private

    private void LoadWriteAnything()
    {
        ClearDynamicLearningControls();

        ShowWriteAnythingOnPanelWorking();
    }

    private void ShowWriteAnythingOnPanelWorking()
    {
        SuspendLayout();

        int contentWidth = Math.Max(760, ClientSize.Width - 100);
        int sectionTop = 70;
        int inputHeight = 32;
        int resultHeight = 52;

        Controls.Add(new Label
        {
            Tag = "fr",
            Text = "Français",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(17, 24, 39),
            Location = new Point(50, sectionTop),
            AutoSize = true
        });

        TextBox textBoxFrench = new TextBox
        {
            Name = "txtWriteAnythingFrInput",
            Tag = "writeanything-input|fr",
            Font = new Font("Segoe UI", 14F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(31, 41, 55),
            BackColor = Color.White,
            Location = new Point(50, sectionTop + 44),
            Size = new Size(contentWidth, inputHeight),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Text = "Bonjourt tout le mondee! Comme exemple."
        };
        textBoxFrench.KeyDown += WriteAnythingInput_KeyDown;
        Controls.Add(textBoxFrench);

        Controls.Add(new Label
        {
            Tag = "fr",
            Text = "Texte corrigé",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, sectionTop + 86),
            AutoSize = true
        });

        Controls.Add(new Label
        {
            Name = "lblWriteAnythingFrCorrectionResult",
            Tag = "fr",
            Text = string.Empty,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(50, sectionTop + 114),
            Size = new Size(contentWidth, resultHeight),
            Padding = new Padding(8)
        });

        Controls.Add(new Label
        {
            Tag = "en",
            Text = "English Translation",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, sectionTop + 170),
            AutoSize = true
        });

        Controls.Add(new Label
        {
            Name = "lblWriteAnythingFrTranslationResult",
            Tag = "en",
            Text = string.Empty,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(50, sectionTop + 198),
            Size = new Size(contentWidth, resultHeight),
            Padding = new Padding(8)
        });

        int englishSectionTop = sectionTop + 284;

        Controls.Add(new Label
        {
            Tag = "en",
            Text = "English",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            Location = new Point(50, englishSectionTop),
            AutoSize = true
        });

        TextBox textBoxEnglish = new TextBox
        {
            Name = "txtWriteAnythingEnInput",
            Tag = "writeanything-input|en",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(31, 41, 55),
            BackColor = Color.White,
            BorderStyle = BorderStyle.FixedSingle,
            Location = new Point(50, englishSectionTop + 34),
            Size = new Size(contentWidth, inputHeight),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right,
            Text = "Helllo everyboddy! As an example."
        };
        textBoxEnglish.KeyDown += WriteAnythingInput_KeyDown;
        Controls.Add(textBoxEnglish);

        Controls.Add(new Label
        {
            Tag = "en",
            Text = "Corrected text",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, englishSectionTop + 76),
            AutoSize = true
        });

        Controls.Add(new Label
        {
            Name = "lblWriteAnythingEnCorrectionResult",
            Tag = "en",
            Text = string.Empty,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(31, 41, 55),
            BackColor = Color.White,
            Location = new Point(50, englishSectionTop + 104),
            Size = new Size(contentWidth, resultHeight),
            Padding = new Padding(8)
        });

        Controls.Add(new Label
        {
            Tag = "fr",
            Text = "Traduction française",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, englishSectionTop + 170),
            AutoSize = true
        });

        Controls.Add(new Label
        {
            Name = "lblWriteAnythingEnTranslationResult",
            Tag = "fr",
            Text = string.Empty,
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(31, 41, 55),
            BackColor = Color.White,
            Location = new Point(50, englishSectionTop + 198),
            Size = new Size(contentWidth, resultHeight),
            Padding = new Padding(8)
        });

        AutoScrollMinSize = new Size(0, englishSectionTop + 300);
        ResumeLayout();
    }

    private async void WriteAnythingInput_KeyDown(object? sender, KeyEventArgs e)
    {
        if (e.KeyCode != Keys.Enter)
        {
            return;
        }

        e.Handled = true;
        e.SuppressKeyPress = true;

        if (sender is not TextBox inputBox || inputBox.Tag is not string inputTag)
        {
            return;
        }

        string sourceLanguage = inputTag.EndsWith("|en", StringComparison.OrdinalIgnoreCase) ? "en" : "fr";
        await ProcessWriteAnythingAsync(inputBox, sourceLanguage);
    }

    private async Task ProcessWriteAnythingAsync(TextBox inputBox, string sourceLanguage)
    {
        if (textBoxCode.Text.Length != 4)
        {
            MessageBox.Show("SVP veuillez entrer un code à 4 chiffres valide.");
            return;
        }

        if (string.IsNullOrWhiteSpace(inputBox.Text))
        {
            MessageBox.Show("SVP veuillez entrer un texte.");
            return;
        }

        Label? correctionLabel = GetWriteAnythingResultLabel(sourceLanguage, isTranslation: false);
        Label? translationLabel = GetWriteAnythingResultLabel(sourceLanguage, isTranslation: true);

        if (correctionLabel is null || translationLabel is null)
        {
            MessageBox.Show("Impossible de trouver les étiquettes de résultat.");
            return;
        }

        inputBox.Enabled = false;
        UseWaitCursor = true;
        correctionLabel.Text = string.Empty;
        translationLabel.Text = string.Empty;

        try
        {
            string correctedText = await SendOpenAiPromptAsync(BuildCorrectionPrompt(inputBox.Text, sourceLanguage));
            correctionLabel.Text = correctedText;

            string translatedText = await SendOpenAiPromptAsync(BuildTranslationPrompt(correctedText, sourceLanguage));
            translationLabel.Text = translatedText;
        }
        catch (Exception ex)
        {
            MessageBox.Show($"Erreur OpenAI API: {ex.Message}");
        }
        finally
        {
            UseWaitCursor = false;
            inputBox.Enabled = true;
            inputBox.Focus();
            inputBox.SelectionStart = inputBox.TextLength;
        }
    }

    private Label? GetWriteAnythingResultLabel(string sourceLanguage, bool isTranslation)
    {
        string labelName = sourceLanguage switch
        {
            "en" when isTranslation => "lblWriteAnythingEnTranslationResult",
            "en" => "lblWriteAnythingEnCorrectionResult",
            _ when isTranslation => "lblWriteAnythingFrTranslationResult",
            _ => "lblWriteAnythingFrCorrectionResult"
        };

        return Controls.Find(labelName, true).OfType<Label>().FirstOrDefault();
    }

    private async Task<string> SendOpenAiPromptAsync(string prompt)
    {
        GetApprendreOpenAIAPIKey(ApprendreOpenAIAPIKey, textBoxCode.Text);

        var apiKey = ApprendreOpenAIAPIKey;

        using var client = new HttpClient();
        client.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", apiKey);

        var requestBody = new
        {
            model = "gpt-4.1-mini",
            messages = new[]
            {
                new { role = "system", content = "Retourne seulement le texte demandé, sans explication, sans titre et sans guillemets." },
                new { role = "user", content = prompt }
            }
        };

        var content = new StringContent(
            JsonSerializer.Serialize(requestBody),
            Encoding.UTF8,
            "application/json"
        );

        var response = await client.PostAsync(
            "https://api.openai.com/v1/chat/completions",
            content
        );

        var responseString = await response.Content.ReadAsStringAsync();

        if (!response.IsSuccessStatusCode)
        {
            throw new InvalidOperationException($"{(int)response.StatusCode} - {responseString}");
        }

        using var json = JsonDocument.Parse(responseString);
        return json.RootElement
            .GetProperty("choices")[0]
            .GetProperty("message")
            .GetProperty("content")
            .GetString()?
            .Trim() ?? string.Empty;
    }

    private static string BuildCorrectionPrompt(string text, string sourceLanguage)
    {
        return sourceLanguage == "en"
            ? $"Correct the following English text. Return only the corrected text.{Environment.NewLine}{text}"
            : $"Corrige le texte français suivant. Retourne seulement le texte corrigé.{Environment.NewLine}{text}";
    }

    private static string BuildTranslationPrompt(string correctedText, string sourceLanguage)
    {
        return sourceLanguage == "en"
            ? $"Traduis le texte anglais corrigé suivant en français. Retourne seulement la traduction.{Environment.NewLine}{correctedText}"
            : $"Translate the following corrected French text into English. Return only the translation.{Environment.NewLine}{correctedText}";
    }


    #endregion Private
}

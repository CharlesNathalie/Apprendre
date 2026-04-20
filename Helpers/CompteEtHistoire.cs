using System.Globalization;
using System.Speech.Synthesis;

namespace Apprendre;

public partial class Apprendre
{
    #region Properties

    private SpeechSynthesizer? _compteEtHistoireSpeechSynthesizer;

    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Private

    private void LoadCompteEtHistoire()
    {
        ClearDynamicLearningControls();

        ShowCompteEtHistoireOnPanelWorking();
    }

    private void ShowCompteEtHistoireOnPanelWorking()
    {
        SuspendLayout();

        int contentWidth = Math.Max(760, ClientSize.Width - 100);
        int sectionTop = 70;

        Controls.Add(new Label
        {
            Tag = "fr",
            Text = "Âge, Langue, Lecture, Écoute, Nombre de mot, etc. Maximum de 1000 mots pour le moment.",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, sectionTop + 10),
            AutoSize = true
        });

        Controls.Add(new Label
        {
            Tag = "en",
            Text = "Age, Language, Reading, Listening, Word Count, etc. Maximum of 1000 words for now.",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(55, 65, 81),
            Location = new Point(50, sectionTop + 40),
            AutoSize = true
        });

        RichTextBox promptRichTextBox = new()
        {
            Name = "rtbCompteEtHistoirePrompt",
            Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(31, 41, 55),
            BackColor = Color.White,
            Location = new Point(50, sectionTop + 72),
            Size = new Size(contentWidth, 140),
            Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right
        };
        Controls.Add(promptRichTextBox);

        Button createButton = new()
        {
            Name = "btnCompteEtHistoireCreate",
            Text = "Créer / Create",
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
            BackColor = Color.FromArgb(219, 234, 254),
            ForeColor = Color.FromArgb(30, 64, 175),
            FlatStyle = FlatStyle.Flat,
            Location = new Point(50, sectionTop + 224),
            Size = new Size(120, 34),
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        Controls.Add(createButton);

        createButton.FlatAppearance.BorderColor = Color.FromArgb(147, 197, 253);
        createButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(191, 219, 254);
        createButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(191, 219, 254);

        Button stopButton = new()
        {
            Name = "btnCompteEtHistoireStop",
            Text = "Arrêter / Stop",
            Font = new Font("Segoe UI", 10F, FontStyle.Bold, GraphicsUnit.Point, 0),
            BackColor = Color.FromArgb(254, 226, 226),
            ForeColor = Color.FromArgb(185, 28, 28),
            FlatStyle = FlatStyle.Flat,
            Location = new Point(182, sectionTop + 224),
            Size = new Size(140, 34),
            Cursor = Cursors.Hand,
            UseVisualStyleBackColor = false
        };
        Controls.Add(stopButton);

        stopButton.FlatAppearance.BorderColor = Color.FromArgb(252, 165, 165);
        stopButton.FlatAppearance.MouseDownBackColor = Color.FromArgb(254, 202, 202);
        stopButton.FlatAppearance.MouseOverBackColor = Color.FromArgb(254, 202, 202);
        stopButton.Click += (_, _) => StopCompteEtHistoirePrononciation();

        Label resultLabelFr = new()
        {
            Name = "lblCompteEtHistoireResultFr",
            Tag = "fr",
            Text = string.Empty,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(17, 24, 39),
            Location = new Point(50, sectionTop + 264),
            MaximumSize = new Size(contentWidth, 0),
            AutoSize = true,
            Visible = false
        };

        Label resultLabelEn = new()
        {
            Name = "lblCompteEtHistoireResultEn",
            Tag = "en",
            Text = string.Empty,
            Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(17, 24, 39),
            Location = new Point(50, sectionTop + 264),
            MaximumSize = new Size(contentWidth, 0),
            AutoSize = true,
            Visible = false
        };

        createButton.Click += async (_, _) => await ProcessCompteEtHistoirePromptAsync(promptRichTextBox, resultLabelFr, resultLabelEn, createButton);

        Controls.Add(resultLabelFr);
        Controls.Add(resultLabelEn);
        ConfigurerPrononciationCompteEtHistoire(resultLabelFr);
        ConfigurerPrononciationCompteEtHistoire(resultLabelEn);

        AutoScrollMinSize = new Size(0, sectionTop + 500);

        ResumeLayout();
    }

    private async Task ProcessCompteEtHistoirePromptAsync(RichTextBox promptRichTextBox, Label resultLabelFr, Label resultLabelEn, Button createButton)
    {
        if (textBoxCode.Text.Length != 4)
        {
            if (_isFr)
            {
                MessageBox.Show("SVP veuillez entrer un code à 4 chiffres valide.");
            }
            else
            {
                MessageBox.Show("Please enter a valid 4-digit code.");
            }
            return;
        }

        if (string.IsNullOrWhiteSpace(promptRichTextBox.Text))
        {
            if (_isFr)
            {
                MessageBox.Show("SVP veuillez entrer un prompt.");
            }
            else
            {
                MessageBox.Show("Please enter a prompt.");
            }
            return;
        }

        promptRichTextBox.Enabled = false;
        createButton.Enabled = false;
        UseWaitCursor = true;
        resultLabelFr.Text = string.Empty;
        resultLabelEn.Text = string.Empty;
        resultLabelFr.Visible = false;
        resultLabelEn.Visible = false;

        try
        {
            string responseText = await SendOpenAiPromptAsync(BuildCompteEtHistoirePrompt(promptRichTextBox.Text.Trim()));
            bool useFrenchLabel = ShouldUseFrenchResultLabel(promptRichTextBox.Text);
            Label resultLabel = useFrenchLabel ? resultLabelFr : resultLabelEn;
            Label hiddenLabel = useFrenchLabel ? resultLabelEn : resultLabelFr;

            hiddenLabel.Visible = false;
            resultLabel.Text = responseText;
            resultLabel.Visible = true;
        }
        catch (Exception ex)
        {
            if (_isFr)
            {
                MessageBox.Show($"Erreur OpenAI API: {ex.Message}");
            }
            else
            {
                MessageBox.Show($"OpenAI API Error: {ex.Message}");
            }
        }
        finally
        {
            UseWaitCursor = false;
            promptRichTextBox.Enabled = true;
            createButton.Enabled = true;
            BeginInvoke(() =>
            {
                ActiveControl = promptRichTextBox;
                promptRichTextBox.Focus();
            });
        }
    }

    private bool ShouldUseFrenchResultLabel(string prompt)
    {
        string normalizedPrompt = prompt.ToLowerInvariant();

        if (normalizedPrompt.Contains("français")
            || normalizedPrompt.Contains("francais")
            || normalizedPrompt.Contains("french")
            || normalizedPrompt.Contains(" en fr ")
            || normalizedPrompt.Contains(" en français")
            || normalizedPrompt.Contains(" in french"))
        {
            return true;
        }

        if (normalizedPrompt.Contains("anglais")
            || normalizedPrompt.Contains("english")
            || normalizedPrompt.Contains(" en anglais")
            || normalizedPrompt.Contains(" in english"))
        {
            return false;
        }

        return _isFr;
    }

    private static string BuildCompteEtHistoirePrompt(string prompt)
    {
        return $"{prompt}{Environment.NewLine}{Environment.NewLine}Important: écris seulement une histoire de moins de 1000 mots. Return only the story, with fewer than 1000 words.";
    }

    private void ConfigurerPrononciationCompteEtHistoire(Label label)
    {
        label.Click -= LabelPrononciation_Click;
        label.Click -= CompteEtHistoireResultLabel_Click;
        label.Click += CompteEtHistoireResultLabel_Click;
        label.Disposed -= LabelPrononciation_Disposed;
        label.Disposed -= CompteHistoireResultLabel_Disposed;
        label.Disposed += CompteHistoireResultLabel_Disposed;
        label.MouseEnter -= LabelPrononciation_MouseEnter;
        label.MouseEnter += LabelPrononciation_MouseEnter;
        label.MouseLeave -= LabelPrononciation_MouseLeave;
        label.MouseLeave += LabelPrononciation_MouseLeave;
        label.Cursor = Cursors.Hand;

        _apparencesInitialesDesLabels.TryAdd(label, (label.ForeColor, label.BackColor));
    }

    private void CompteHistoireResultLabel_Disposed(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        label.Click -= LabelPrononciation_Click;
        label.Click -= CompteEtHistoireResultLabel_Click;
        label.Disposed -= LabelPrononciation_Disposed;
        label.Disposed -= CompteHistoireResultLabel_Disposed;
        _apparencesInitialesDesLabels.Remove(label);

        if (Controls.Find("lblCompteEtHistoireResultFr", true).Length == 0
            && Controls.Find("lblCompteEtHistoireResultEn", true).Length == 0)
        {
            StopCompteEtHistoirePrononciation();
            _compteEtHistoireSpeechSynthesizer?.Dispose();
            _compteEtHistoireSpeechSynthesizer = null;
        }
    }

    private void CompteEtHistoireResultLabel_Click(object? sender, EventArgs e)
    {
        if (sender is not Label label || !label.Visible || string.IsNullOrWhiteSpace(label.Text))
        {
            return;
        }

        PrononcerResultatCompteEtHistoire(label);
    }

    private void PrononcerResultatCompteEtHistoire(Label label)
    {
        _compteEtHistoireSpeechSynthesizer ??= new SpeechSynthesizer();
        StopCompteEtHistoirePrononciation();

        CultureInfo culture = (label.Tag?.ToString() ?? string.Empty).StartsWith("fr", StringComparison.OrdinalIgnoreCase)
            ? CultureInfo.GetCultureInfo("fr-CA")
            : CultureInfo.GetCultureInfo("en-US");

        SelectionnerVoixCompteEtHistoire(culture);
        _compteEtHistoireSpeechSynthesizer.SpeakAsync(label.Text);
    }

    private void SelectionnerVoixCompteEtHistoire(CultureInfo culture)
    {
        if (_compteEtHistoireSpeechSynthesizer is null)
        {
            return;
        }

        VoiceInfo? installedVoice = _compteEtHistoireSpeechSynthesizer
            .GetInstalledVoices()
            .Select(voice => voice.VoiceInfo)
            .FirstOrDefault(voice => string.Equals(voice.Culture.Name, culture.Name, StringComparison.OrdinalIgnoreCase));

        installedVoice ??= _compteEtHistoireSpeechSynthesizer
            .GetInstalledVoices()
            .Select(voice => voice.VoiceInfo)
            .FirstOrDefault(voice => string.Equals(voice.Culture.TwoLetterISOLanguageName, culture.TwoLetterISOLanguageName, StringComparison.OrdinalIgnoreCase));

        if (installedVoice is not null)
        {
            _compteEtHistoireSpeechSynthesizer.SelectVoice(installedVoice.Name);
            return;
        }

        _compteEtHistoireSpeechSynthesizer.SelectVoiceByHints(VoiceGender.NotSet, VoiceAge.NotSet, 0, culture);
    }

    private void StopCompteEtHistoirePrononciation()
    {
        _compteEtHistoireSpeechSynthesizer?.SpeakAsyncCancelAll();
    }

    #endregion Private
}

namespace Apprendre;

public partial class Apprendre
{
    #region Properties
    
    private readonly Dictionary<Label, (Color CouleurTexte, Color CouleurFond)> _apparencesInitialesDesLabels = [];
    private bool _prononciationEnCours;
    private bool _prononciationEnAttente;
    private Point _positionDefilementAvantPrononciation = Point.Empty;
    private System.Windows.Media.MediaPlayer? _lecteurPrononciation;

    #endregion Properties

    #region Private

    private void ActiverPrononciationDesLabels(Control controle)
    {
        if (controle.IsDisposed)
        {
            return;
        }

        controle.ControlAdded -= ControlePrononciation_ControlAdded;
        controle.ControlAdded += ControlePrononciation_ControlAdded;

        if (controle is Label label)
        {
            ConfigurerPrononciationDuLabel(label);
        }

        foreach (Control enfant in controle.Controls)
        {
            ActiverPrononciationDesLabels(enfant);
        }
    }

    private async Task ChargerMediaEtPrononcerAsync(Label label)
    {
        string url = CreerUrlPrononciation(label);

        _positionDefilementAvantPrononciation = new Point(Math.Abs(AutoScrollPosition.X), Math.Abs(AutoScrollPosition.Y));
        _prononciationEnAttente = true;

        LirePrononciationAvecMedia(url);

        if (!IsDisposed && !Disposing && IsHandleCreated)
        {
            try
            {
                BeginInvoke(() => AutoScrollPosition = _positionDefilementAvantPrononciation);
            }
            catch (ObjectDisposedException)
            {
                return;
            }
            catch (InvalidOperationException)
            {
                return;
            }
        }

        if (checkBoxGetDataImage!.Checked)
        {
            panelImageSearch!.Location = new Point(400, 100);
            panelImageSearch!.Size = new Size(600, 500);

            ResumeLayout(true);
            PerformLayout();

            await webView2ImageSearchRun(label);
            return;
        }

        ResumeLayout(true);
        PerformLayout();
    }

    private void ConfigurerPrononciationDuLabel(Label label)
    {
        if (EstLabelImage(label))
        {
            label.Click -= LabelImage_Click;
            label.Click += LabelImage_Click;
            label.Disposed -= LabelImage_Disposed;
            label.Disposed += LabelImage_Disposed;
            label.MouseEnter -= LabelImage_MouseEnter;
            label.MouseEnter += LabelImage_MouseEnter;
            label.MouseLeave -= LabelImage_MouseLeave;
            label.MouseLeave += LabelImage_MouseLeave;
            label.Cursor = Cursors.Hand;
        }
        else
        {

            label.Click -= LabelPrononciation_Click;
            label.Click += LabelPrononciation_Click;
            label.Disposed -= LabelPrononciation_Disposed;
            label.Disposed += LabelPrononciation_Disposed;
            label.MouseEnter -= LabelPrononciation_MouseEnter;
            label.MouseEnter += LabelPrononciation_MouseEnter;
            label.MouseLeave -= LabelPrononciation_MouseLeave;
            label.MouseLeave += LabelPrononciation_MouseLeave;
            label.Cursor = Cursors.Hand;

            _apparencesInitialesDesLabels.TryAdd(label, (label.ForeColor, label.BackColor));
        }
    }

    private static bool CoreWebView2EstDisponible(Microsoft.Web.WebView2.WinForms.WebView2? webView2)
    {
        if (!ControleWebView2EstValide(webView2))
        {
            return false;
        }

        try
        {
            return webView2!.CoreWebView2 is not null;
        }
        catch (ObjectDisposedException)
        {
            return false;
        }
        catch (InvalidOperationException)
        {
            return false;
        }
    }

    private void ControlePrononciation_ControlAdded(object? sender, ControlEventArgs e)
    {
        if (e.Control is null || e.Control.IsDisposed)
        {
            return;
        }

        ActiverPrononciationDesLabels(e.Control);

    }

    private static bool ControleWebView2EstValide(Microsoft.Web.WebView2.WinForms.WebView2? webView2)
    {
        return webView2 is not null
            && !webView2.IsDisposed
            && !webView2.Disposing;
    }

    private void LecteurPrononciation_MediaEnded(object? sender, EventArgs e)
    {
        if (sender is System.Windows.Media.MediaPlayer mediaPlayer)
        {
            try
            {
                mediaPlayer.Stop();
            }
            catch (InvalidOperationException)
            {
            }
        }

        NettoyerLecteurPrononciation(sender as System.Windows.Media.MediaPlayer);
    }

    private static string CreerUrlGoogleTranslatePourPrononciation(Label label)
    {
        string langue = label.Tag?.ToString()?.Split('|')[0] ?? "";

        return $"https://translate.google.com/translate_tts?ie=UTF-8&client=tw-ob&tl={langue}&q={Uri.EscapeDataString(label.Text)}";
    }

    private string CreerUrlPrononciation(Label label)
    {
        if (UtiliserAudioAbcFrancais(label))
        {
            return new Uri(AbcFrenchAudioFilePath, UriKind.Absolute).AbsoluteUri;
        }

        return CreerUrlGoogleTranslatePourPrononciation(label);
    }

    private void LecteurPrononciation_MediaFailed(object? sender, System.Windows.Media.ExceptionEventArgs e)
    {
        NettoyerLecteurPrononciation(sender as System.Windows.Media.MediaPlayer);
    }

    private void LecteurPrononciation_MediaOpened(object? sender, EventArgs e)
    {
        if (sender is not System.Windows.Media.MediaPlayer mediaPlayer)
        {
            return;
        }

        _prononciationEnAttente = false;

        try
        {
            mediaPlayer.Position = TimeSpan.Zero;
            mediaPlayer.Play();
        }
        catch (InvalidOperationException)
        {
            NettoyerLecteurPrononciation(mediaPlayer);
        }
    }

    private void LirePrononciationAvecMedia(string url)
    {
        ArreterPrononciationMedia();

        System.Windows.Media.MediaPlayer mediaPlayer = new();
        mediaPlayer.MediaOpened += LecteurPrononciation_MediaOpened;
        mediaPlayer.MediaEnded += LecteurPrononciation_MediaEnded;
        mediaPlayer.MediaFailed += LecteurPrononciation_MediaFailed;

        _lecteurPrononciation = mediaPlayer;

        try
        {
            mediaPlayer.Open(new Uri(url, UriKind.Absolute));
        }
        catch (InvalidOperationException)
        {
            NettoyerLecteurPrononciation(mediaPlayer);
        }
        catch (UriFormatException)
        {
            NettoyerLecteurPrononciation(mediaPlayer);
        }
    }

    private void NettoyerLecteurPrononciation(System.Windows.Media.MediaPlayer? mediaPlayer)
    {
        if (mediaPlayer is null)
        {
            _prononciationEnAttente = false;
            return;
        }

        mediaPlayer.MediaOpened -= LecteurPrononciation_MediaOpened;
        mediaPlayer.MediaEnded -= LecteurPrononciation_MediaEnded;
        mediaPlayer.MediaFailed -= LecteurPrononciation_MediaFailed;

        try
        {
            mediaPlayer.Close();
        }
        catch (InvalidOperationException)
        {
        }

        if (ReferenceEquals(_lecteurPrononciation, mediaPlayer))
        {
            _lecteurPrononciation = null;
        }

        _prononciationEnAttente = false;
    }

    private async void LabelPrononciation_Click(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        await PrononcerTexteAsync(label);
    }

    private void LabelPrononciation_Disposed(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        label.Disposed -= LabelPrononciation_Disposed;
        _apparencesInitialesDesLabels.Remove(label);
    }

    private void LabelPrononciation_MouseEnter(object? sender, EventArgs e)
    {
        if (sender is not Label label)
        {
            return;
        }

        _apparencesInitialesDesLabels.TryAdd(label, (label.ForeColor, label.BackColor));
        label.ForeColor = Color.RoyalBlue;
        label.BackColor = Color.FromArgb(230, 240, 255);
    }

    private void LabelPrononciation_MouseLeave(object? sender, EventArgs e)
    {
        if (sender is not Label label || !_apparencesInitialesDesLabels.TryGetValue(label, out (Color CouleurTexte, Color CouleurFond) apparenceInitiale))
        {
            return;
        }

        label.ForeColor = apparenceInitiale.CouleurTexte;
        label.BackColor = apparenceInitiale.CouleurFond;
    }

    private async Task PrononcerTexteAsync(Label label)
    {
        if (_prononciationEnCours || string.IsNullOrWhiteSpace(label.Text))
        {
            return;
        }

        _prononciationEnCours = true;

        try
        {
            if (string.IsNullOrEmpty(label.Tag?.ToString()))
            {
                await ChargerMediaEtPrononcerAsync(label);
                return;
            }

            string[] tagParts = label.Tag?.ToString()?.Split('|') ?? [];

            _currentItemIndex = tagParts.Length > 1 && int.TryParse(tagParts[1], out int itemIndex)
                ? itemIndex
                : -1;

            _currentChildItemIndex = tagParts.Length > 2 && int.TryParse(tagParts[2], out int childItemIndex)
                ? childItemIndex
                : -1;

            await ChargerMediaEtPrononcerAsync(label);

            if (checkBoxAfficherImage.Checked)
            {
                await ShowSelectedImageForLabelAsync(label);
            }

        }
        finally
        {
            _prononciationEnCours = false;
        }

    }

    private bool UtiliserAudioAbcFrancais(Label label)
    {
        return _selectionFromCombobox == ABCSelection
            && label.Tag?.ToString()?.StartsWith("fr|", StringComparison.Ordinal) == true
            && File.Exists(AbcFrenchAudioFilePath);
    }

    private void ArreterPrononciationMedia()
    {
        if (_lecteurPrononciation is null)
        {
            _prononciationEnAttente = false;
            return;
        }

        try
        {
            _lecteurPrononciation.Stop();
        }
        catch (InvalidOperationException)
        {
        }

        NettoyerLecteurPrononciation(_lecteurPrononciation);
    }

    private async void webView2ImageSearch_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
    {
        if (sender is not Microsoft.Web.WebView2.WinForms.WebView2 webView2
            || !e.IsSuccess
            || !CoreWebView2EstDisponible(webView2))
        {
            return;
        }
        string query = "voiture";

        try
        {
            await webView2.ExecuteScriptAsync($@"
                    (function(){{
                        const ta = document.querySelector('textarea#APjFqb.gLFyf');
                        if (!ta) return;
                        ta.focus();
                        ta.value = {query};
                        ta.dispatchEvent(new Event('input', {{bubbles:true}}));
                        ta.dispatchEvent(new Event('change', {{bubbles:true}}));
                        const form = ta.closest('form');
                        if (form) {{
                            form.submit();
                        }} else {{
                            ta.dispatchEvent(new KeyboardEvent('keydown', {{key:'Enter',code:'Enter',keyCode:13,which:13,bubbles:true}}));
                        }}
                    }})();
                ");
        }
        catch (ObjectDisposedException)
        {
        }
        catch (InvalidOperationException)
        {
        }
    }

    private async Task webView2ImageSearchRun(Label label)
    {
        string query = label.Text;

        if (!ControleWebView2EstValide(WebView2ImageSearch))
        {
            return;
        }

        try
        {
            WebView2ImageSearch!.Source = new Uri($"https://www.google.com/search?q={Uri.EscapeDataString(query)}&sei=09bKaf6JKr-pptQPsoj-qAI", UriKind.Absolute);
        }
        catch (ObjectDisposedException)
        {
        }
        catch (InvalidOperationException)
        {
        }
    }

    #endregion Private
}

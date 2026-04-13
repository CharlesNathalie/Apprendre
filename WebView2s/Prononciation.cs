namespace Apprendre;

public partial class Apprendre
{
    #region Properties
    
    private readonly Dictionary<Label, (Color CouleurTexte, Color CouleurFond)> _apparencesInitialesDesLabels = [];
    private bool _prononciationEnCours;
    private bool _prononciationEnAttente;
    private Point _positionDefilementAvantPrononciation = Point.Empty;

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

    private async Task ChargerGoogleTranslateEtPrononcerAsync(Label label)
    {
        if (!ControleWebView2EstValide(WebView2WebGoogleTranslate))
        {
            return;
        }

        string url = CreerUrlPrononciation(label);

        _positionDefilementAvantPrononciation = new Point(Math.Abs(AutoScrollPosition.X), Math.Abs(AutoScrollPosition.Y));
        _sourceWebView2 = url;
        _prononciationEnAttente = true;

        try
        {
            await WebView2WebGoogleTranslate!.EnsureCoreWebView2Async();
        }
        catch (ObjectDisposedException)
        {
            _prononciationEnAttente = false;
            return;
        }
        catch (InvalidOperationException)
        {
            _prononciationEnAttente = false;
            return;
        }

        if (!CoreWebView2EstDisponible(WebView2WebGoogleTranslate))
        {
            _prononciationEnAttente = false;
            return;
        }

        try
        {
            WebView2WebGoogleTranslate!.NavigateToString(CreerDocumentHtmlPourPrononciation(url));
        }
        catch (ObjectDisposedException)
        {
            _prononciationEnAttente = false;
            return;
        }
        catch (InvalidOperationException)
        {
            _prononciationEnAttente = false;
            return;
        }

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
        }

        ResumeLayout(true);
        PerformLayout();

        await webView2ImageSearchRun(label);
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

    private static string CreerDocumentHtmlPourPrononciation(string url)
    {
        return $$"""
        <!DOCTYPE html>
        <html lang="en">
        <head>
            <meta charset="utf-8">
            <title>Prononciation</title>
        </head>
        <body style="margin:0;background:transparent;overflow:hidden;">
            <audio id="tts" autoplay src="{{url}}"></audio>
            <script>
                const audio = document.getElementById('tts');

                function lancerLecture() {
                    if (!audio) {
                        return;
                    }

                    audio.currentTime = 0;
                    const lecture = audio.play();
                    if (lecture) {
                        lecture.catch(() => window.setTimeout(() => audio.play().catch(() => { }), 150));
                    }
                }

                audio?.addEventListener('canplaythrough', lancerLecture, { once: true });
                window.addEventListener('load', lancerLecture, { once: true });
            </script>
        </body>
        </html>
        """;

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
                await ChargerGoogleTranslateEtPrononcerAsync(label);
                return;
            }

            string[] tagParts = label.Tag?.ToString()?.Split('|') ?? [];

            _currentItemIndex = tagParts.Length > 1 && int.TryParse(tagParts[1], out int itemIndex)
                ? itemIndex
                : -1;

            _currentChildItemIndex = tagParts.Length > 2 && int.TryParse(tagParts[2], out int childItemIndex)
                ? childItemIndex
                : -1;

            await ChargerGoogleTranslateEtPrononcerAsync(label);

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

    private async void WebView2WebGoogleTranslate_NavigationCompleted(object? sender, Microsoft.Web.WebView2.Core.CoreWebView2NavigationCompletedEventArgs e)
    {
        if (sender is not Microsoft.Web.WebView2.WinForms.WebView2 webView2
            || !e.IsSuccess
            || !CoreWebView2EstDisponible(webView2)
            || !_prononciationEnAttente)
        {
            return;
        }

        _prononciationEnAttente = false;
        AutoScrollPosition = _positionDefilementAvantPrononciation;

        try
        {
            await webView2.ExecuteScriptAsync("""
                const audio = document.getElementById('tts');
                if (audio) {
                    audio.currentTime = 0;
                    audio.play().catch(() => { });
                }
                """);
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

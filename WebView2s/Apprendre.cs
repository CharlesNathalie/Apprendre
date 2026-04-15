namespace Apprendre
{
    [System.ComponentModel.Localizable(false)]
    public partial class Apprendre : Form
    {
        #region Properties

        private List<FrEnURL> _dataList { get; set; } = new List<FrEnURL>();
        private List<AnimauxMFP> _animauxMFPList { get; set; } = new List<AnimauxMFP>();
        private List<ABC> _abcList { get; set; } = new List<ABC>();

        private string _sourceWebView2 = string.Empty;

        private const string ChooseSelection = "Choisir / Choose";
        private const string ABCSelection = "ABC / ABC";
        private const string SonSelection = "Son / Sound";
        private const string SoundSelection = "Sound / Son";
        private const string CouleurSelection = "Couleur / Color";
        private const string FemininSelection = "Féminin / Feminine";
        private const string MasculinSelection = "Masculin / Masculine";
        private const string NombreSelection = "Nombre / Number";
        private const string NombreRomainSelection = "Nombre Romain / Roman Number";
        private const string MaisonSelection = "Maison / House";
        private const string CuisineSelection = "Cuisine / Kitchen";
        private const string SalleDeBainSelection = "Salle de Bain / Bathroom";
        private const string VoitureSelection = "Voiture / Car";
        private const string ChambreACoucherSelection = "Chambre à Coucher / Bedroom";
        private const string CorpsHumainSelection = "Corps Humain / Human Body";
        private const string AnimalMfpSelection = "Animal Mâle, Femelle, Petit / Male, Female, Young Animal";
        private const string MachinerieSelection = "Machinerie / Machinery";
        private const string MoyenDeTransportSelection = "Moyen de Transport / Means of Transport";
        private const string FruitSelection = "Fruit / Fruit";
        private const string NourritureSelection = "Nourriture / Food";
        private const string TerreSelection = "Terre / Earth";
        private const string CommunicationSelection = "Communication / Communication";
        private const string AdjectiveSelection = "Adjectif / Adjective";
        private const string LegumeSelection = "Légume / Vegetable";
        private const string SentimentSelection = "Sentiment / Feeling";
        private const string VerbeSelection = "Verbe / Verb";
        private const string DefaultTranslateSourceLanguage = "fr";
        private const string DefaultTranslateTargetLanguage = "en";
        private const string DefaultImageSearchQuery = "image";
        private const string GitHubRawDataBaseUrl = "https://raw.githubusercontent.com/CharlesNathalie/Apprendre/refs/heads/master/Data/";

        private static readonly string AppApprendreDataFolderPath = GetAppApprendreDataFolderPath();
        private static readonly string AbcFrenchAudioFilePath = Path.Combine(AppApprendreDataFolderPath, "ABC_FR.mp3");
        private static readonly string AbcEnglishAudioFilePath = Path.Combine(AppApprendreDataFolderPath, "ABC_EN.mp3");
        private static readonly string[] RequiredAppDataFileNames =
        [
            "ABC_FR.mp3",
            "ABC_EN.mp3",
            "Adjective.json",
            "AnimauxMFP.json",
            "ChambreACoucher.json",
            "ColorName.json",
            "Communication.json",
            "CorpsHumain.json",
            "Cuisine.json",
            "Feminin.json",
            "Fruit.json",
            "Legume.json",
            "Machinerie.json",
            "Maison.json",
            "Masculin.json",
            "MoyenDeTransport.json",
            "Nombre.json",
            "NombreRomain.json",
            "Nourriture.json",
            "SalleDeBain.json",
            "Sentiment.json",
            "SonFrancais.json",
            "SoundEnglish.json",
            "Terre.json",
            "Verbe.json",
            "Voiture.json"
        ];
        private static readonly string[] AvailableSelections =
        {
            ChooseSelection,
            ABCSelection,
            SonSelection,
            SoundSelection,
            CouleurSelection,
            NombreSelection,
            NombreRomainSelection,
            AnimalMfpSelection,
            FruitSelection,
            LegumeSelection,
            NourritureSelection,
            MaisonSelection,
            CuisineSelection,
            SalleDeBainSelection,
            ChambreACoucherSelection,
            CorpsHumainSelection,
            MoyenDeTransportSelection,
            TerreSelection,
            AdjectiveSelection,
            SentimentSelection,
            VerbeSelection,
            MachinerieSelection,
            VoitureSelection,
            CommunicationSelection,
            FemininSelection,
            MasculinSelection,
        };

        private string _selectionFromCombobox = string.Empty;
        private int _currentItemIndex = -1;
        private int _currentChildItemIndex = -1;
        private Dictionary<string, SelectionConfiguration>? _selectionConfigurations;

        private bool _isFr = true;
        private bool _optionsOpen = false;

        #endregion Properties

        #region Constructors

        public Apprendre()
        {
            InitializeComponent();

            // When opened in the WinForms designer avoid executing runtime-only initialization.
            if (IsDesignTime())
            {
                return;
            }

            ApplyApplicationIcon();
            EnsureApplicationDataFiles();
            InitializeDocking();
            InitializeHideImageOnNonImageClicks();
            ActiverPrononciationDesLabels(this);

            InitialiserChoixListe();

            lblShowError.Text = string.Empty;
        }

        #endregion Constructors

        private void ApplyApplicationIcon()
        {
            try
            {
                System.Drawing.Icon? icon = System.Drawing.Icon.ExtractAssociatedIcon(Application.ExecutablePath);
                if (icon is not null)
                {
                    Icon = icon;
                }
            }
            catch
            {
            }
        }

        #region Events

        private void btnLanguage_Click(object sender, EventArgs e)
        {
            if (_isFr)
            {
                btnLanguage.Text = "Fr";
                _isFr = false;
                checkBoxAfficherImage.Text = "Show image";
                checkBoxGetDataImage.Text = "Get image";
            }
            else
            {
                btnLanguage.Text = "En";
                _isFr = true;
                checkBoxAfficherImage.Text = "Afficher l'image";
                checkBoxGetDataImage.Text = "Importer image";
            }
        }

        private void btnOptions_Click(object sender, EventArgs e)
        {
            if (_optionsOpen)
            {
                panelOptions.Visible = false;
                _optionsOpen = false;
            }
            else
            {
                panelOptions.Visible = true;
                panelOptions.BringToFront();
                _optionsOpen = true;
            }
        }

        private void btnSaveJsonFile_Click(object sender, EventArgs e)
        {
            SaveJSONFile();
        }

        private void checkBoxGetDataImage_Click(object sender, EventArgs e)
        {
            if (!checkBoxGetDataImage.Checked)
            {
                richTextBoxDataImage.BackColor = SystemColors.Window;
                panelImageSearch.Visible = false;
            }
            else
            {
                panelImageSearch.Visible = true;
            }
        }

        private void comboBoxApprendre_SelectedIndexChanged(object? sender, EventArgs e)
        {
            //SaveJSONFile();

            string selection = comboBoxApprendre!.SelectedItem?.ToString() ?? string.Empty;

            _selectionFromCombobox = selection;
            _currentItemIndex = -1;
            _currentChildItemIndex = -1;

            if (selection == ChooseSelection)
            {
                return;
            }

            SelectionConfiguration? configuration = GetSelectionConfiguration(selection);
            if (configuration is null)
            {
                ShowUnsupportedSelectionError(selection);
                return;
            }

            configuration.Load();
        }

        private void richTextBoxDataImage_TextChanged(object? sender, EventArgs e)
        {
            if (richTextBoxDataImage.Text.Length > 0)
            {
                if (IsImageDataUri(richTextBoxDataImage.Text) || IsImageUrl(richTextBoxDataImage.Text))
                {
                    if (_selectionFromCombobox == ChooseSelection)
                    {
                        return;
                    }

                    SelectionConfiguration? configuration = GetSelectionConfiguration(_selectionFromCombobox);
                    if (configuration is null)
                    {
                        ShowUnsupportedSelectionError(_selectionFromCombobox);
                    }
                    else if (configuration.AssignImage?.Invoke(richTextBoxDataImage.Text) == true)
                    {
                        richTextBoxDataImage.Text = string.Empty;
                    }

                    richTextBoxDataImage.BackColor = Color.LightGreen;
                }
                else
                {
                    richTextBoxDataImage.BackColor = Color.LightPink;
                }

                ScrollToLabelForCurrentSelection();
            }
        }

        #endregion Events

        #region Private

        private void InitializeDocking()
        {
            panelGoogleTranslate!.Dock = DockStyle.None;
            panelGoogleTranslate!.Location = new Point(-10000, -10000);
            panelGoogleTranslate!.Size = new Size(1, 1);
            panelGoogleTranslate!.Visible = true;
            panelGoogleTranslate!.TabStop = false;
            WebView2WebGoogleTranslate!.Dock = DockStyle.Fill;
            WebView2WebGoogleTranslate!.Visible = true;
            WebView2WebGoogleTranslate!.TabStop = false;
            WebView2WebGoogleTranslate!.Source = CreateGoogleTranslateUri(DefaultTranslateSourceLanguage, DefaultTranslateTargetLanguage);
            WebView2ImageSearch!.Source = CreateImageSearchUri(DefaultImageSearchQuery);

            AutoScroll = true;

        }

        private static Uri CreateGoogleTranslateUri(string sourceLanguage, string targetLanguage)
        {
            string query = $"sl={Uri.EscapeDataString(sourceLanguage)}&tl={Uri.EscapeDataString(targetLanguage)}&op=translate";
            return new Uri($"https://translate.google.com/?{query}", UriKind.Absolute);
        }

        private static Uri CreateImageSearchUri(string searchQuery)
        {
            string query = Uri.EscapeDataString(searchQuery);
            return new Uri($"https://www.google.com/search?tbm=isch&q={query}", UriKind.Absolute);
        }

        private static string GetAppApprendreDataFolderPath()
        {
            string folderPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                "Apprendre",
                "Data");

            Directory.CreateDirectory(folderPath);

            return folderPath;
        }

        private static void EnsureApplicationDataFiles()
        {
            List<string> failedFileNames = [];

            using var httpClient = new System.Net.Http.HttpClient
            {
                Timeout = TimeSpan.FromSeconds(30)
            };

            foreach (string fileName in RequiredAppDataFileNames)
            {
                string destinationFilePath = Path.Combine(AppApprendreDataFolderPath, fileName);
                if (File.Exists(destinationFilePath))
                {
                    continue;
                }

                try
                {
                    if (TryCopyLocalDataFile(fileName, destinationFilePath))
                    {
                        continue;
                    }

                    byte[] fileContent = httpClient.GetByteArrayAsync(CreateGitHubDataFileUri(fileName)).GetAwaiter().GetResult();
                    File.WriteAllBytes(destinationFilePath, fileContent);
                }
                catch
                {
                    failedFileNames.Add(fileName);
                }
            }

            if (failedFileNames.Count > 0)
            {
                MessageBox.Show(
                    $"Impossible d'initialiser certains fichiers de données dans '{AppApprendreDataFolderPath}'.{Environment.NewLine}{string.Join(Environment.NewLine, failedFileNames)}",
                    "Erreur",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Warning);
            }
        }

        private static bool TryCopyLocalDataFile(string fileName, string destinationFilePath)
        {
            string[] candidatePaths =
            [
                Path.Combine(AppContext.BaseDirectory, "Data", fileName),
                Path.GetFullPath(Path.Combine(AppContext.BaseDirectory, "..", "..", "..", "Data", fileName))
            ];

            foreach (string candidatePath in candidatePaths)
            {
                if (!File.Exists(candidatePath))
                {
                    continue;
                }

                File.Copy(candidatePath, destinationFilePath, overwrite: false);
                return true;
            }

            return false;
        }

        private static Uri CreateGitHubDataFileUri(string fileName)
        {
            return new Uri($"{GitHubRawDataBaseUrl}{Uri.EscapeDataString(fileName)}", UriKind.Absolute);
        }

        private void InitialiserChoixListe()
        {
            comboBoxApprendre!.Items.Clear();
            comboBoxApprendre!.Items.AddRange(AvailableSelections);
            comboBoxApprendre.SelectedIndex = 0;
        }

        private void SaveJSONFile()
        {
            string selection = comboBoxApprendre!.SelectedItem?.ToString() ?? string.Empty;

            JsonSerializerOptions options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = System.Text.Encodings.Web.JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };

            if (selection == ChooseSelection)
            {
                return;
            }

            SelectionConfiguration? configuration = GetSelectionConfiguration(selection);
            if (configuration is null)
            {
                ShowUnsupportedSelectionError(selection);
                return;
            }

            configuration.Save(options);

        }

        private SelectionConfiguration? GetSelectionConfiguration(string selection)
        {
            _selectionConfigurations ??= CreateSelectionConfigurations();

            return _selectionConfigurations.TryGetValue(selection, out SelectionConfiguration? configuration)
                ? configuration
                : null;
        }

        private Dictionary<string, SelectionConfiguration> CreateSelectionConfigurations()
        {
            return new Dictionary<string, SelectionConfiguration>(StringComparer.Ordinal)
            {
                [ABCSelection] = new(
                    LoadABC,
                    options => { /* No need to save static information */ },
                    null),
                [SonSelection] = new(
                    LoadSonFrancaisList,
                    options => SaveListToJson(_sonFrancaisList, "SonFrancais.json", options),
                    imageUrl => TryAssignImageToNestedListItem(_sonFrancaisList, imageUrl, item => item.Exemples, (example, url) => example.Url = url)),
                [SoundSelection] = new(
                    LoadSoundEnglishList,
                    options => SaveListToJson(_soundEnglishList, "SoundEnglish.json", options),
                    imageUrl => TryAssignImageToNestedListItem(_soundEnglishList, imageUrl, item => item.Examples, (example, url) => example.Url = url)),
                [CouleurSelection] = new(
                    LoadColorNameList,
                    options => SaveListToJson(_colorNameList, "Color.json", options),
                    null),
                [NombreSelection] = new(
                    LoadNombreList,
                    options => { /* No need to save static information */ },
                    null),
                [NombreRomainSelection] = new(
                    LoadNombresRomainList,
                    options => { /* No need to save static information */ },
                    null),
                [AnimalMfpSelection] = new(
                    () => LoadAnimauxMFPList("AnimauxMFP.json", "animaux", "animals"),
                    options => SaveListToJson(_animauxMFPList, "AnimauxMFP.json", options),
                    imageUrl => TryAssignImageToAnimauxMFPListItem(imageUrl, (item, url) => item.Url = url)),
                [FruitSelection] = new(
                    () => LoadDataList("Fruit.json", "Fruit", "Fruit"),
                    options => SaveDataListToJson("Fruit.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [LegumeSelection] = new(
                    () => LoadDataList("Legume.json", "Légume", "Vegetable"),
                    options => SaveDataListToJson("Legume.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [NourritureSelection] = new(
                    () => LoadDataList("Nourriture.json", "Nourriture", "Food"),
                    options => SaveDataListToJson("Nourriture.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [MaisonSelection] = new(
                    () => LoadDataList("Maison.json", "Maison", "House"),
                    options => SaveDataListToJson("Maison.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [CuisineSelection] = new(
                    () => LoadDataList("Cuisine.json", "Cuisine", "Kitchen"),
                    options => SaveDataListToJson("Cuisine.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [SalleDeBainSelection] = new(
                    () => LoadDataList("SalleDeBain.json", "Salle de Bain", "Bathroom"),
                    options => SaveDataListToJson("SalleDeBain.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [ChambreACoucherSelection] = new(
                    () => LoadDataList("ChambreACoucher.json", "Chambre à Coucher", "Bedroom"),
                    options => SaveDataListToJson("ChambreACoucher.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [CorpsHumainSelection] = new(
                    () => LoadDataList("CorpsHumain.json", "Corps Humain", "Human Body"),
                    options => SaveDataListToJson("CorpsHumain.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [MoyenDeTransportSelection] = new(
                    () => LoadDataList("MoyenDeTransport.json", "Moyen de Transport", "Means of Transport"),
                    options => SaveDataListToJson("MoyenDeTransport.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [TerreSelection] = new(
                    () => LoadDataList("Terre.json", "Terre", "Earth"),
                    options => SaveDataListToJson("Terre.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [AdjectiveSelection] = new(
                    () => LoadDataList("Adjective.json", "Adjective", "Adjective"),
                    options => SaveDataListToJson("Adjective.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [SentimentSelection] = new(
                    () => LoadDataList("Sentiment.json", "Sentiment", "Sentiment"),
                    options => SaveDataListToJson("Sentiment.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [VerbeSelection] = new(
                    () => LoadDataList("Verbe.json", "Verbe", "Verb"),
                    options => SaveDataListToJson("Verbe.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [MachinerieSelection] = new(
                    () => LoadDataList("Machinerie.json", "Machinerie", "Machinery"),
                    options => SaveDataListToJson("Machinerie.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [VoitureSelection] = new(
                    () => LoadDataList("Voiture.json", "Voiture", "Car"),
                    options => SaveDataListToJson("Voiture.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [CommunicationSelection] = new(
                    () => LoadDataList("Communication.json", "Communication", "Communication"),
                    options => SaveDataListToJson("Communication.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [FemininSelection] = new(
                    () => LoadDataList("Feminin.json", "Feminin", "Feminine"),
                    options => SaveDataListToJson("Feminin.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
                [MasculinSelection] = new(
                    () => LoadDataList("Masculin.json", "Masculin", "Masculine"),
                    options => SaveDataListToJson("Masculin.json", options),
                    imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
            };
        }

        private void SaveListToJson<T>(IList<T> list, string fileName, JsonSerializerOptions options)
        {
            string filePath = Path.Combine(AppApprendreDataFolderPath, fileName);

            try
            {
                string json = JsonSerializer.Serialize(list, options);
                File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement du fichier '{filePath}': {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SaveDataListToJson(string fileName, JsonSerializerOptions options)
        {
            string filePath = Path.Combine(AppApprendreDataFolderPath, fileName);

            try
            {
                string json = JsonSerializer.Serialize(_dataList, options);
                File.WriteAllText(filePath, json, System.Text.Encoding.UTF8);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erreur lors de l'enregistrement du fichier '{filePath}': {ex.Message}", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool TryAssignImageToDataListItem(string imageUrl, Action<FrEnURL, string> assignImage)
        {
            if (_currentItemIndex < 0 || _dataList.Count <= _currentItemIndex)
            {
                return false;
            }

            assignImage(_dataList[_currentItemIndex], imageUrl);
            return true;
        }

        private bool TryAssignImageToAnimauxMFPListItem(string imageUrl, Action<AnimauxMFP, string> assignImage)
        {
            if (_currentItemIndex < 0 || _animauxMFPList.Count <= _currentItemIndex)
            {
                return false;
            }

            assignImage(_animauxMFPList[_currentItemIndex], imageUrl);
            return true;
        }

        private bool TryAssignImageToNestedListItem<TItem, TChild>(IList<TItem> list, string imageUrl, Func<TItem, IList<TChild>> childSelector, Action<TChild, string> assignImage)
        {
            if (_currentItemIndex < 0 || list.Count <= _currentItemIndex)
            {
                return false;
            }

            IList<TChild> childList = childSelector(list[_currentItemIndex]);
            if (_currentChildItemIndex < 0 || childList.Count <= _currentChildItemIndex)
            {
                return false;
            }

            assignImage(childList[_currentChildItemIndex], imageUrl);
            return true;
        }

        private static void ShowUnsupportedSelectionError(string selection)
        {
            MessageBox.Show($"Erreur: La sélection '{selection}' n'est pas gérée pour l'enregistrement.", "Erreur", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private sealed record SelectionConfiguration(
            Action Load,
            Action<JsonSerializerOptions> Save,
            Func<string, bool>? AssignImage);

        private void ScrollToLabelForCurrentSelection()
        {
            if (_currentItemIndex < 0)
            {
                return;
            }

            for (int i = 0; i < Controls.Count; i++)
            {
                Control c = Controls[i];
                if (c is Label lbl && lbl.Tag != null && lbl.Tag.ToString() == $"fr|{_currentItemIndex}")
                {
                    Control? rowContainer = c.Parent;
                    Control? scrollTarget = rowContainer?.Parent ?? rowContainer;

                    if (scrollTarget != null)
                    {
                        ScrollControlIntoView(scrollTarget);
                    }

                    if (rowContainer != null)
                    {
                        panelImageSearch.Location = new Point(panelImageSearch.Location.X, rowContainer.Location.Y);
                    }

                    ScrollControlIntoView(panelImageSearch);

                    break;
                }
            }
        }

        #endregion Private

        #region WebView2 Clipboard Handling

        // Clipboard listening to auto-paste image addresses when copied from WebView2
        private const int WM_CLIPBOARDUPDATE = 0x031D;

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool AddClipboardFormatListener(IntPtr hwnd);

        [System.Runtime.InteropServices.DllImport("user32.dll", SetLastError = true)]
        private static extern bool RemoveClipboardFormatListener(IntPtr hwnd);

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            if (IsDesignTime())
            {
                return;
            }
            try
            {
                AddClipboardFormatListener(this.Handle);
            }
            catch
            {
                // ignore failures to register
            }
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            try
            {
                RemoveClipboardFormatListener(this.Handle);
            }
            catch
            {
                // ignore failures to unregister
            }
            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            if (m.Msg == WM_CLIPBOARDUPDATE)
            {
                try
                {
                    if (Clipboard.ContainsText())
                    {
                        string clip = Clipboard.GetText();
                        if (checkBoxGetDataImage != null && checkBoxGetDataImage.Checked)
                        {
                            if (!string.IsNullOrWhiteSpace(clip) &&
                                (IsImageDataUri(clip) || IsImageUrl(clip)))
                            {
                                if (richTextBoxDataImage.InvokeRequired)
                                {
                                    richTextBoxDataImage.Invoke((Action)(() => richTextBoxDataImage.Text = clip));
                                }
                                else
                                {
                                    richTextBoxDataImage.Text = clip;
                                }
                            }
                        }
                    }
                }
                catch
                {
                    // ignore clipboard access exceptions
                }
            }

            base.WndProc(ref m);
        }

        private static bool IsImageDataUri(string s)
        {
            return s.StartsWith("data:image/", StringComparison.InvariantCultureIgnoreCase);
        }

        private static bool IsImageUrl(string s)
        {
            if (Uri.TryCreate(s, UriKind.Absolute, out Uri? uri) &&
                (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps))
            {
                string path = uri.AbsolutePath.ToLowerInvariant();
                if (path.EndsWith(".jpg") || path.EndsWith(".jpeg") || path.EndsWith(".png")
                    || path.EndsWith(".gif") || path.EndsWith(".webp")
                    || path.EndsWith(".bmp") || path.EndsWith(".svg") || path.EndsWith(".avif"))
                {
                    return true;
                }

                return QueryContainsImageIndicator(uri.Query);
            }

            return false;
        }

        private static bool QueryContainsImageIndicator(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
            {
                return false;
            }

            string trimmedQuery = query.TrimStart('?');
            if (string.IsNullOrWhiteSpace(trimmedQuery))
            {
                return false;
            }

            string[] imageKeys = ["format", "fm", "ext", "extension", "mime", "type", "content-type", "response-content-type"];
            string[] imageValues = ["jpg", "jpeg", "png", "gif", "webp", "bmp", "svg", "avif", "image/jpeg", "image/png", "image/gif", "image/webp", "image/bmp", "image/svg+xml", "image/avif"];

            foreach (string parameter in trimmedQuery.Split('&', StringSplitOptions.RemoveEmptyEntries | StringSplitOptions.TrimEntries))
            {
                string[] parts = parameter.Split('=', 2);
                string key = Uri.UnescapeDataString(parts[0]).ToLowerInvariant();
                string value = parts.Length > 1 ? Uri.UnescapeDataString(parts[1]).ToLowerInvariant() : string.Empty;

                if (Array.IndexOf(imageKeys, key) >= 0)
                {
                    if (value.StartsWith("image/", StringComparison.Ordinal) || Array.IndexOf(imageValues, value) >= 0)
                    {
                        return true;
                    }
                }
            }

            return false;
        }

        private bool IsDesignTime()
        {
            return System.ComponentModel.LicenseManager.UsageMode == System.ComponentModel.LicenseUsageMode.Designtime
                || DesignMode
                || Site?.DesignMode == true;
        }

        #endregion WebView2 Clipboard Handling
   
    }
}

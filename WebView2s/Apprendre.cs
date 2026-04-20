namespace Apprendre;

[System.ComponentModel.Localizable(false)]
public partial class Apprendre : Form
{
    #region Properties

    private List<FrEnURL> _dataList { get; set; } = new List<FrEnURL>();
    private List<AnimauxMFP> _animauxMFPList { get; set; } = new List<AnimauxMFP>();
    private List<ABC> _abcList { get; set; } = new List<ABC>();

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
    private const string PaysPopulationSelection = "Pays et Population / Countries and Population";
    private const string OiseauxDuCanadaSelection = "Oiseaux du Canada / Birds of Canada";
    private const string MarquesDeVoitureSelection = "Marques de Voiture / Car Brands";
    private const string PoissonsEtCrustacesDuCanadaSelection = "Poissons et Crustacés du Canada / Fish and Shellfish of Canada";
    private const string SentimentSelection = "Sentiment / Feeling";
    private const string VerbeSelection = "Verbe / Verb";
    private const string VerbeConjugerSelection = "Verbe Conjugaison / Conjugated Verb";
    private const string WriteAnythingSelection = "Écrire n'importe quoi / Write Anything";
    private const string CompteEtHistoireSelection = "Compte et Histoire / Stories";
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
        "MarquesDeVoiture.json",
        "Masculin.json",
        "MoyenDeTransport.json",
        "Nombre.json",
        "NombreRomain.json",
        "Nourriture.json",
        "OiseauxDuCanada.json",
        "PoissonsEtCrustacesDuCanada.json",
        "PaysPopulation.json",
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
        PaysPopulationSelection,
        OiseauxDuCanadaSelection,
        MarquesDeVoitureSelection,
        PoissonsEtCrustacesDuCanadaSelection,
        CommunicationSelection,
        FemininSelection,
        MasculinSelection,
        VerbeConjugerSelection,
        WriteAnythingSelection,
        CompteEtHistoireSelection,
    };

    private string _selectionFromCombobox = string.Empty;
    private int _currentItemIndex = -1;
    private int _currentChildItemIndex = -1;
    private Dictionary<string, SelectionConfiguration>? _selectionConfigurations;

    private bool _isFr = true;
    private bool _optionsOpen = false;

    private List<string> NotCorrectOpenAIKeyList { get; set; } = new List<string>()
    {
        "sk-proj-SrteetzUf-YjH6XMwidDaqMiR8Evue6h",
        "A4jP-OTk4eT11DlWv9BnL0v_qH7X2bvKmXFzdWZt",
        "b-T3BlbkFJrbg9iHKjONfxN1VQtRY7XEbo7a2xZt",
        "uverQxfVpNtFMw8DfVmR_pEuSO61FfndLfhYzLyXDI8A"
    };

    private int Code0 = 0;
    private int Code1 = 0;
    private int Code2 = 0;
    private int Code3 = 0;

    private int Offset = 20;

    private string switch0Char = string.Empty;
    private string switch1Char = string.Empty;

    private string ApprendreOpenAIAPIKey { get; set; } = string.Empty;


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
        UpdateLocalizedCheckboxTexts();

        InitialiserChoixListe();

        lblShowError.Text = string.Empty;

        ApprendreOpenAIAPIKey = $"{NotCorrectOpenAIKeyList[0]}{NotCorrectOpenAIKeyList[1]}{NotCorrectOpenAIKeyList[2]}{NotCorrectOpenAIKeyList[3]}";


        NotCorrectOpenAIKeyList = new List<string>()
        {
            "sk-proj-SrteetzUf-YjH6XMwidDaqMiR8Evue6h",
            "A4jP-OTk4eT11DlWv9BnL0v_qH7X2bvKmXFzdWZt",
            "b-T3BlbkFJrbg9iHKjONfxN1VQtRY7XEbo7a2xZt",
            "uverQxfVpNtFMw8DfVmR_pEuSO61FfndLfhYzLyXDI8A"
        };
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

    private void UpdateLocalizedCheckboxTexts()
    {
        checkBoxAfficherImage.Text = GetLocalizedText("Afficher l'image", "Show image");
        checkBoxGetDataImage.Text = GetLocalizedText("Importer image", "Get image");
        labelNathalieTelLast4Digit.Text = GetLocalizedText("Tel Nathalie dernier 4 chiffres", "Nathalie phone last 4 digits");
    }

    private string GetLocalizedText(string frenchText, string englishText)
    {
        return _isFr ? frenchText : englishText;
    }

    private string GetLocalizedMessageBoxTitle(MessageBoxIcon icon)
    {
        return icon == MessageBoxIcon.Warning
            ? GetLocalizedText("Avertissement", "Warning")
            : GetLocalizedText("Erreur", "Error");
    }

    private void ShowLocalizedMessage(string message, MessageBoxIcon icon)
    {
        MessageBox.Show(message, GetLocalizedMessageBoxTitle(icon), MessageBoxButtons.OK, icon);
    }

    private string GetDataInitializationErrorMessage(IEnumerable<string> failedFileNames)
    {
        return GetLocalizedText(
            $"Impossible d'initialiser certains fichiers de données dans '{AppApprendreDataFolderPath}'.{Environment.NewLine}{string.Join(Environment.NewLine, failedFileNames)}",
            $"Unable to initialize some data files in '{AppApprendreDataFolderPath}'.{Environment.NewLine}{string.Join(Environment.NewLine, failedFileNames)}");
    }

    private string GetSaveFileErrorMessage(string filePath, string errorMessage)
    {
        return GetLocalizedText(
            $"Erreur lors de l'enregistrement du fichier '{filePath}' : {errorMessage}",
            $"Error while saving file '{filePath}': {errorMessage}");
    }

    private string GetUnsupportedSelectionErrorMessage(string selection)
    {
        return GetLocalizedText(
            $"La sélection '{selection}' n'est pas gérée pour l'enregistrement.",
            $"The selection '{selection}' is not supported for saving.");
    }

    #region Events

    private void btnLanguage_Click(object sender, EventArgs e)
    {
        if (_isFr)
        {
            btnLanguage.Text = "Fr";
            _isFr = false;
        }
        else
        {
            btnLanguage.Text = "En";
            _isFr = true;
        }

        UpdateLocalizedCheckboxTexts();
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

    private void GetApprendreOpenAIAPIKey(string key, string code)
    {

        // doing correct 0

        string tempNotCorrectOpenAIKeyFirstPart0 = NotCorrectOpenAIKeyList[0].Substring(0, Code0 + Offset);
        string tempNotCorrectOpenAIKeyLastPart0 = NotCorrectOpenAIKeyList[0].Substring(Code0 + Offset + 2);

        switch0Char = NotCorrectOpenAIKeyList[0].Substring(Code0 + Offset, 1);
        switch1Char = NotCorrectOpenAIKeyList[0].Substring(Code0 + Offset + 1, 1);
        tempNotCorrectOpenAIKeyFirstPart0 = $"{tempNotCorrectOpenAIKeyFirstPart0}{switch0Char}{switch1Char}{tempNotCorrectOpenAIKeyLastPart0}";
        NotCorrectOpenAIKeyList[0] = tempNotCorrectOpenAIKeyFirstPart0;

        // doing correct 1

        string tempNotCorrectOpenAIKeyFirstPart1 = NotCorrectOpenAIKeyList[1].Substring(0, Code1 + Offset);
        string tempNotCorrectOpenAIKeyLastPart1 = NotCorrectOpenAIKeyList[1].Substring(Code1 + Offset + 2);
        switch0Char = NotCorrectOpenAIKeyList[1].Substring(Code1 + Offset, 1);
        switch1Char = NotCorrectOpenAIKeyList[1].Substring(Code1 + Offset + 1, 1);
        tempNotCorrectOpenAIKeyFirstPart1 = $"{tempNotCorrectOpenAIKeyFirstPart1}{switch0Char}{switch1Char}{tempNotCorrectOpenAIKeyLastPart1}";
        NotCorrectOpenAIKeyList[1] = tempNotCorrectOpenAIKeyFirstPart1;

        // doing correct 2

        string tempNotCorrectOpenAIKeyFirstPart2 = NotCorrectOpenAIKeyList[2].Substring(0, Code2 + Offset);
        string tempNotCorrectOpenAIKeyLastPart2 = NotCorrectOpenAIKeyList[2].Substring(Code2 + Offset + 2);
        switch0Char = NotCorrectOpenAIKeyList[2].Substring(Code2 + Offset, 1);
        switch1Char = NotCorrectOpenAIKeyList[2].Substring(Code2 + Offset + 1, 1);
        tempNotCorrectOpenAIKeyFirstPart2 = $"{tempNotCorrectOpenAIKeyFirstPart2}{switch0Char}{switch1Char}{tempNotCorrectOpenAIKeyLastPart2}";
        NotCorrectOpenAIKeyList[2] = tempNotCorrectOpenAIKeyFirstPart2;

        // doing correct 3

        string tempNotCorrectOpenAIKeyFirstPart3 = NotCorrectOpenAIKeyList[3].Substring(0, Code3 + Offset);
        string tempNotCorrectOpenAIKeyLastPart3 = NotCorrectOpenAIKeyList[3].Substring(Code3 + Offset + 2);
        switch0Char = NotCorrectOpenAIKeyList[3].Substring(Code3 + Offset, 1);
        switch1Char = NotCorrectOpenAIKeyList[3].Substring(Code3 + Offset + 1, 1);
        tempNotCorrectOpenAIKeyFirstPart3 = $"{tempNotCorrectOpenAIKeyFirstPart3}{switch0Char}{switch1Char}{tempNotCorrectOpenAIKeyLastPart3}";
        NotCorrectOpenAIKeyList[3] = tempNotCorrectOpenAIKeyFirstPart3;

        ApprendreOpenAIAPIKey = $"{NotCorrectOpenAIKeyList[0]}{NotCorrectOpenAIKeyList[1]}{NotCorrectOpenAIKeyList[2]}{NotCorrectOpenAIKeyList[3]}";
    }

    private void InitializeDocking()
    {
        WebView2ImageSearch!.Source = CreateImageSearchUri(DefaultImageSearchQuery);

        AutoScroll = true;

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

    private void EnsureApplicationDataFiles()
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
            ShowLocalizedMessage(GetDataInitializationErrorMessage(failedFileNames), MessageBoxIcon.Warning);
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
            [PaysPopulationSelection] = new(
                LoadPaysPopulation,
                options => { /* No need to save static information */ },
                null),
            [OiseauxDuCanadaSelection] = new(
                () => LoadDataList("OiseauxDuCanada.json", "Oiseaux du Canada", "Birds of Canada"),
                options => SaveDataListToJson("OiseauxDuCanada.json", options),
                imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
            [PoissonsEtCrustacesDuCanadaSelection] = new(
                () => LoadDataList("PoissonsEtCrustacesDuCanada.json", "Poissons et Crustacés du Canada", "Fish and Shellfish of Canada"),
                options => SaveDataListToJson("PoissonsEtCrustacesDuCanada.json", options),
                imageUrl => TryAssignImageToDataListItem(imageUrl, (item, url) => item.Url = url)),
            [MarquesDeVoitureSelection] = new(
                () => LoadDataList("MarquesDeVoiture.json", "Marques de Voiture", "Car Brands"),
                options => SaveDataListToJson("MarquesDeVoiture.json", options),
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
            [VerbeConjugerSelection] = new(
                LoadVerbeConjuger,
                options => { /* No need to save static information */ },
                null),
            [WriteAnythingSelection] = new(
                LoadWriteAnything,
                options => { /* No need to save static information */ },
                null),
            [CompteEtHistoireSelection] = new(
                LoadCompteEtHistoire,
                options => { /* No need to save static information */ },
                null),
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
            ShowLocalizedMessage(GetSaveFileErrorMessage(filePath, ex.Message), MessageBoxIcon.Error);
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
            ShowLocalizedMessage(GetSaveFileErrorMessage(filePath, ex.Message), MessageBoxIcon.Error);
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

    private void ShowUnsupportedSelectionError(string selection)
    {
        ShowLocalizedMessage(GetUnsupportedSelectionErrorMessage(selection), MessageBoxIcon.Error);
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

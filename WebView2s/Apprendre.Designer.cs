namespace Apprendre
{
    public partial class Apprendre
    {
        /// <summary>
        ///  Variable du concepteur requise.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Nettoie les ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le concepteur Windows Form

        /// <summary>
        ///  Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        ///  le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            lblLearn = new Label();
            lblApprendre = new Label();
            comboBoxApprendre = new ComboBox();
            panelImageSearch = new Panel();
            WebView2ImageSearch = new Microsoft.Web.WebView2.WinForms.WebView2();
            panelImageSearchTop = new Panel();
            panelTopLeft = new Panel();
            richTextBoxDataImage = new RichTextBox();
            panelSaveJsonFile = new Panel();
            btnSaveJsonFile = new Button();
            checkBoxGetDataImage = new CheckBox();
            checkBoxAfficherImage = new CheckBox();
            lblShowError = new Label();
            WebView21 = new Microsoft.Web.WebView2.WinForms.WebView2();
            btnOptions = new Button();
            btnLanguage = new Button();
            panelOptions = new Panel();
            labelNathalieTelLast4Digit = new Label();
            textBoxCode = new TextBox();
            panelImageSearch.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WebView2ImageSearch).BeginInit();
            panelImageSearchTop.SuspendLayout();
            panelTopLeft.SuspendLayout();
            panelSaveJsonFile.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)WebView21).BeginInit();
            panelOptions.SuspendLayout();
            SuspendLayout();
            // 
            // lblLearn
            // 
            lblLearn.AutoSize = true;
            lblLearn.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblLearn.Location = new Point(785, 20);
            lblLearn.Name = "lblLearn";
            lblLearn.Size = new Size(49, 21);
            lblLearn.TabIndex = 6;
            lblLearn.Tag = "en";
            lblLearn.Text = "Learn";
            // 
            // lblApprendre
            // 
            lblApprendre.AutoSize = true;
            lblApprendre.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblApprendre.Location = new Point(51, 17);
            lblApprendre.Name = "lblApprendre";
            lblApprendre.Size = new Size(84, 21);
            lblApprendre.TabIndex = 5;
            lblApprendre.Tag = "fr";
            lblApprendre.Text = "Apprendre";
            // 
            // comboBoxApprendre
            // 
            comboBoxApprendre.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxApprendre.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            comboBoxApprendre.FormattingEnabled = true;
            comboBoxApprendre.ItemHeight = 21;
            comboBoxApprendre.Location = new Point(160, 17);
            comboBoxApprendre.MaxDropDownItems = 5;
            comboBoxApprendre.Name = "comboBoxApprendre";
            comboBoxApprendre.Size = new Size(601, 29);
            comboBoxApprendre.TabIndex = 4;
            comboBoxApprendre.SelectedIndexChanged += comboBoxApprendre_SelectedIndexChanged;
            // 
            // panelImageSearch
            // 
            panelImageSearch.BackColor = Color.Red;
            panelImageSearch.BorderStyle = BorderStyle.Fixed3D;
            panelImageSearch.Controls.Add(WebView2ImageSearch);
            panelImageSearch.Controls.Add(panelImageSearchTop);
            panelImageSearch.Location = new Point(391, 102);
            panelImageSearch.Name = "panelImageSearch";
            panelImageSearch.Padding = new Padding(5);
            panelImageSearch.Size = new Size(102, 68);
            panelImageSearch.TabIndex = 7;
            panelImageSearch.Visible = false;
            // 
            // WebView2ImageSearch
            // 
            WebView2ImageSearch.AllowExternalDrop = true;
            WebView2ImageSearch.CreationProperties = null;
            WebView2ImageSearch.DefaultBackgroundColor = Color.White;
            WebView2ImageSearch.Dock = DockStyle.Fill;
            WebView2ImageSearch.Location = new Point(5, 45);
            WebView2ImageSearch.Name = "WebView2ImageSearch";
            WebView2ImageSearch.Size = new Size(88, 14);
            WebView2ImageSearch.TabIndex = 5;
            WebView2ImageSearch.ZoomFactor = 1D;
            WebView2ImageSearch.NavigationCompleted += webView2ImageSearch_NavigationCompleted;
            // 
            // panelImageSearchTop
            // 
            panelImageSearchTop.BackColor = Color.WhiteSmoke;
            panelImageSearchTop.Controls.Add(panelTopLeft);
            panelImageSearchTop.Controls.Add(panelSaveJsonFile);
            panelImageSearchTop.Dock = DockStyle.Top;
            panelImageSearchTop.Location = new Point(5, 5);
            panelImageSearchTop.Name = "panelImageSearchTop";
            panelImageSearchTop.Size = new Size(88, 40);
            panelImageSearchTop.TabIndex = 6;
            // 
            // panelTopLeft
            // 
            panelTopLeft.Controls.Add(richTextBoxDataImage);
            panelTopLeft.Dock = DockStyle.Fill;
            panelTopLeft.Location = new Point(0, 0);
            panelTopLeft.Name = "panelTopLeft";
            panelTopLeft.Size = new Size(0, 40);
            panelTopLeft.TabIndex = 11;
            // 
            // richTextBoxDataImage
            // 
            richTextBoxDataImage.Dock = DockStyle.Fill;
            richTextBoxDataImage.Location = new Point(0, 0);
            richTextBoxDataImage.Name = "richTextBoxDataImage";
            richTextBoxDataImage.Size = new Size(0, 40);
            richTextBoxDataImage.TabIndex = 0;
            richTextBoxDataImage.Text = "";
            richTextBoxDataImage.TextChanged += richTextBoxDataImage_TextChanged;
            // 
            // panelSaveJsonFile
            // 
            panelSaveJsonFile.Controls.Add(btnSaveJsonFile);
            panelSaveJsonFile.Dock = DockStyle.Right;
            panelSaveJsonFile.Location = new Point(-50, 0);
            panelSaveJsonFile.Name = "panelSaveJsonFile";
            panelSaveJsonFile.Size = new Size(138, 40);
            panelSaveJsonFile.TabIndex = 10;
            // 
            // btnSaveJsonFile
            // 
            btnSaveJsonFile.BackColor = Color.GreenYellow;
            btnSaveJsonFile.Dock = DockStyle.Right;
            btnSaveJsonFile.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnSaveJsonFile.ForeColor = SystemColors.ControlText;
            btnSaveJsonFile.Location = new Point(2, 0);
            btnSaveJsonFile.Name = "btnSaveJsonFile";
            btnSaveJsonFile.Size = new Size(136, 40);
            btnSaveJsonFile.TabIndex = 9;
            btnSaveJsonFile.Text = "Save Json File";
            btnSaveJsonFile.UseVisualStyleBackColor = false;
            btnSaveJsonFile.Click += btnSaveJsonFile_Click;
            // 
            // checkBoxGetDataImage
            // 
            checkBoxGetDataImage.AutoSize = true;
            checkBoxGetDataImage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxGetDataImage.Location = new Point(12, 158);
            checkBoxGetDataImage.Name = "checkBoxGetDataImage";
            checkBoxGetDataImage.Size = new Size(137, 25);
            checkBoxGetDataImage.TabIndex = 8;
            checkBoxGetDataImage.Text = "Importer image";
            checkBoxGetDataImage.UseVisualStyleBackColor = true;
            checkBoxGetDataImage.Click += checkBoxGetDataImage_Click;
            // 
            // checkBoxAfficherImage
            // 
            checkBoxAfficherImage.AutoSize = true;
            checkBoxAfficherImage.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            checkBoxAfficherImage.Location = new Point(12, 17);
            checkBoxAfficherImage.Name = "checkBoxAfficherImage";
            checkBoxAfficherImage.Size = new Size(138, 25);
            checkBoxAfficherImage.TabIndex = 13;
            checkBoxAfficherImage.Text = "Afficher l'image";
            checkBoxAfficherImage.UseVisualStyleBackColor = true;
            // 
            // lblShowError
            // 
            lblShowError.AutoSize = true;
            lblShowError.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            lblShowError.ForeColor = Color.Red;
            lblShowError.Location = new Point(63, 61);
            lblShowError.Name = "lblShowError";
            lblShowError.Size = new Size(131, 21);
            lblShowError.TabIndex = 9;
            lblShowError.Text = "Message d'erreur";
            // 
            // WebView21
            // 
            WebView21.AllowExternalDrop = true;
            WebView21.CreationProperties = null;
            WebView21.DefaultBackgroundColor = Color.White;
            WebView21.Location = new Point(63, 102);
            WebView21.Name = "WebView21";
            WebView21.Size = new Size(86, 31);
            WebView21.Source = new Uri("C:\\Users\\charl\\AppData\\Local\\Apprendre\\Data\\Empty.html", UriKind.Absolute);
            WebView21.TabIndex = 12;
            WebView21.Visible = false;
            WebView21.ZoomFactor = 1D;
            WebView21.NavigationCompleted += WebView21_NavigationCompleted;
            // 
            // btnOptions
            // 
            btnOptions.BackColor = Color.Lime;
            btnOptions.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnOptions.Location = new Point(850, 17);
            btnOptions.Name = "btnOptions";
            btnOptions.Size = new Size(93, 35);
            btnOptions.TabIndex = 14;
            btnOptions.Text = "Options";
            btnOptions.UseVisualStyleBackColor = false;
            btnOptions.Click += btnOptions_Click;
            // 
            // btnLanguage
            // 
            btnLanguage.BackColor = Color.Lime;
            btnLanguage.Font = new Font("Segoe UI", 14.25F, FontStyle.Regular, GraphicsUnit.Point, 0);
            btnLanguage.Location = new Point(959, 17);
            btnLanguage.Name = "btnLanguage";
            btnLanguage.Size = new Size(62, 35);
            btnLanguage.TabIndex = 15;
            btnLanguage.Text = "En";
            btnLanguage.UseVisualStyleBackColor = false;
            btnLanguage.Click += btnLanguage_Click;
            // 
            // panelOptions
            // 
            panelOptions.BackColor = Color.LightGreen;
            panelOptions.BorderStyle = BorderStyle.Fixed3D;
            panelOptions.Controls.Add(labelNathalieTelLast4Digit);
            panelOptions.Controls.Add(textBoxCode);
            panelOptions.Controls.Add(checkBoxAfficherImage);
            panelOptions.Controls.Add(checkBoxGetDataImage);
            panelOptions.Location = new Point(850, 61);
            panelOptions.Name = "panelOptions";
            panelOptions.Size = new Size(193, 203);
            panelOptions.TabIndex = 16;
            panelOptions.Visible = false;
            // 
            // labelNathalieTelLast4Digit
            // 
            labelNathalieTelLast4Digit.AutoSize = true;
            labelNathalieTelLast4Digit.Location = new Point(13, 75);
            labelNathalieTelLast4Digit.Name = "labelNathalieTelLast4Digit";
            labelNathalieTelLast4Digit.Size = new Size(159, 15);
            labelNathalieTelLast4Digit.TabIndex = 15;
            labelNathalieTelLast4Digit.Text = "Tel Nathalie dernier 4 chiffres";
            // 
            // textBoxCode
            // 
            textBoxCode.Font = new Font("Segoe UI", 12F, FontStyle.Regular, GraphicsUnit.Point, 0);
            textBoxCode.Location = new Point(12, 104);
            textBoxCode.Name = "textBoxCode";
            textBoxCode.Size = new Size(157, 29);
            textBoxCode.TabIndex = 14;
            // 
            // Apprendre
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(1080, 687);
            Controls.Add(panelOptions);
            Controls.Add(btnLanguage);
            Controls.Add(btnOptions);
            Controls.Add(WebView21);
            Controls.Add(lblShowError);
            Controls.Add(panelImageSearch);
            Controls.Add(lblLearn);
            Controls.Add(lblApprendre);
            Controls.Add(comboBoxApprendre);
            Name = "Apprendre";
            Text = "Apprendre";
            panelImageSearch.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WebView2ImageSearch).EndInit();
            panelImageSearchTop.ResumeLayout(false);
            panelTopLeft.ResumeLayout(false);
            panelSaveJsonFile.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)WebView21).EndInit();
            panelOptions.ResumeLayout(false);
            panelOptions.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion Code généré par le concepteur Windows Form
        private ComboBox comboBoxApprendre;
        private Label lblLearn;
        private Label lblApprendre;
        private Panel panelImageSearch;
        private Panel panelImageSearchTop;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView2ImageSearch;
        private CheckBox checkBoxGetDataImage;
        private CheckBox checkBoxAfficherImage;
        private RichTextBox richTextBoxDataImage;
        private Button btnSaveJsonFile;
        private Panel panelTopLeft;
        private Panel panelSaveJsonFile;
        private Label lblShowError;
        private Microsoft.Web.WebView2.WinForms.WebView2 WebView21;
        private Button btnOptions;
        private Button btnLanguage;
        private Panel panelOptions;
        private TextBox textBoxCode;
        private Label labelNathalieTelLast4Digit;
    }
}

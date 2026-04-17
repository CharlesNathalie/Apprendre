namespace Apprendre;

public partial class Apprendre
{
    #region Properties


    #endregion Properties

    #region Constructors

    #endregion Constructors

    #region Private

    private void LoadCompteHistoire()
    {
        ClearDynamicLearningControls();

        ShowCompteHistoireOnPanelWorking();
    }

    private void ShowCompteHistoireOnPanelWorking()
    {
        SuspendLayout();

        int contentWidth = Math.Max(760, ClientSize.Width - 100);
        int sectionTop = 70;

        Controls.Add(new Label
        {
            Tag = "fr",
            Text = "Français",
            Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point, 0),
            ForeColor = Color.FromArgb(17, 24, 39),
            Location = new Point(50, sectionTop),
            AutoSize = true
        });

        ResumeLayout();
    }

    #endregion Private
}

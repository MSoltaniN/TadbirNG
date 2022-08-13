using System.Windows.Forms;

namespace SPPC.Tadbir.Setup
{
    interface ISetupProgressPage
    {
        Label ElapsedLabel { get; }

        Label ProgressLabel { get; }

        Label StatusLabel { get; }

        ProgressBar ProgressBar { get; }

        TextBox ConsoleTextBox { get; }
    }
}

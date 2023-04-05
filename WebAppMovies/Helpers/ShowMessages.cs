using Microsoft.JSInterop;
using MudBlazor;

namespace WebAppMovies.Helpers
{
    public class ShowMessages : IShowMessages
    {
        private readonly ISnackbar _snackbar;
        public ShowMessages(ISnackbar snackbar)
        {
            _snackbar = snackbar;
        }
        public async Task ShowErrorMessage(string message)
        {
            await ShowMessage(Severity.Error, message);
        }

        public async Task ShowInfoMessage(string message)
        {
            await ShowMessage(Severity.Info, message);
        }

        public async Task ShowSuccessMessage(string message)
        {
            await ShowMessage(Severity.Success, message);
        }

        public async Task ShowWarningMessage(string message)
        {
            await ShowMessage(Severity.Warning, message);
        }

        private async Task ShowMessage(Severity severityMessage, string message)
        { 
            Task.Run(() => _snackbar.Add(message,severityMessage)).GetAwaiter().GetResult();
        }
    }
}

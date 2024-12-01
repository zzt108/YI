namespace YiChing.Services
{
    public class AlertService : IAlertService
    {
        public async Task DisplayAlert(string title, string message, string cancel)
        {
            if (Application.Current?.MainPage != null)
            {
                await Application.Current.MainPage.DisplayAlert(title, message, cancel);
            }
            else
            {
                // Optional: Log the error or handle the case when MainPage is not available
                System.Diagnostics.Debug.WriteLine("Cannot display alert: MainPage is null");
            }
        }
    }
}

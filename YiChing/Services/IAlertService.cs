namespace YiChing.Services
{
    public interface IAlertService
    {
        Task DisplayAlert(string title, string message, string cancel);
    }
}

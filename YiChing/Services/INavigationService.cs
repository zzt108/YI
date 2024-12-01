namespace YiChing.Services;

public interface INavigationService
{
    Task NavigateToAsync(string route);
}

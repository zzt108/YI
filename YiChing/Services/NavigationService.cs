namespace YiChing.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        if (Shell.Current != null)
        {
            await Shell.Current.GoToAsync(route);
        }
        else
        {
            throw new InvalidOperationException("Navigation failed: Shell.Current is not initialized");
        }
    }
}

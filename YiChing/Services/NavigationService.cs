namespace YiChing.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        if (Shell.Current != null)
        {
            if (!route.StartsWith("/"))
                route = $"///{route}";
                
            await Shell.Current.GoToAsync(route);
        }
        else
        {
            throw new InvalidOperationException("Navigation failed: Shell.Current is not initialized");
        }
    }
}

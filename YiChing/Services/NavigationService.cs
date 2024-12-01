namespace YiChing.Services;

public class NavigationService : INavigationService
{
    public async Task NavigateToAsync(string route)
    {
        System.Diagnostics.Debug.WriteLine($"Attempting to navigate to route: {route}");

        if (Shell.Current != null)
        {
            try 
            {
                // Use the route exactly as suggested
                await Shell.Current.GoToAsync(route);
                
                System.Diagnostics.Debug.WriteLine($"Navigation to {route} successful");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Navigation error: {ex.Message}");
                throw; // Re-throw to allow caller to handle
            }
        }
        else
        {
            System.Diagnostics.Debug.WriteLine("Navigation failed: Shell.Current is not initialized");
            throw new InvalidOperationException("Navigation failed: Shell.Current is not initialized");
        }
    }
}

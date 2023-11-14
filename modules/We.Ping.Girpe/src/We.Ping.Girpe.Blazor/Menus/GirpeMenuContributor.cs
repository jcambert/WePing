using System.Threading.Tasks;
using Volo.Abp.UI.Navigation;

namespace We.Ping.Girpe.Blazor.Menus;

public class GirpeMenuContributor : IMenuContributor
{
    public async Task ConfigureMenuAsync(MenuConfigurationContext context)
    {
        if (context.Menu.Name == StandardMenus.Main)
        {
            await ConfigureMainMenuAsync(context);
        }
    }

    private Task ConfigureMainMenuAsync(MenuConfigurationContext context)
    {
        //Add main menu items.
        context.Menu.AddItem(new ApplicationMenuItem(GirpeMenus.Prefix, displayName: "Girpe", "/Girpe", icon: "fa fa-globe"));

        return Task.CompletedTask;
    }
}

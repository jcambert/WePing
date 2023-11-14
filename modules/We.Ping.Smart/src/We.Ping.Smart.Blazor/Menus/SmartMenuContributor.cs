using Volo.Abp.UI.Navigation;
using We.Ping.Smart.Blazor.Pages.Smart;

namespace We.Ping.Smart.Blazor.Menus;

public class SmartMenuContributor : IMenuContributor
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
        var appMenu = new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Smart", "/Smart", icon: "fa fa-globe");
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Clubs", "/recherche_clubs", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Detail Clubs", "/detail_clubs", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Equipes", "/recherche_equipes", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Organismes", "/recherche_organismes", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Joueur Classement", "/recherche_joueur_cla", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Joueurs Classement", "/recherche_joueurs_cla", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Joueur Licence Spid", "/recherche_joueur_spid", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Joueurs Licence Spid", "/recherche_joueurs_spid", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Joueur Parties", "/recherche_parties", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Epreuves par Organisme", "/recherche_epreuves", icon: "fa fa-globe"));
        appMenu.AddItem(new ApplicationMenuItem(SmartMenus.Prefix, displayName: "Divisions", "/recherche_divisions", icon: "fa fa-globe"));


        
        context.Menu.AddItem(appMenu);

        return Task.CompletedTask;
    }
}

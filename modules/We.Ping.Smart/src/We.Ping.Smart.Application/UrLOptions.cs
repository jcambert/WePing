namespace We.Ping.Smart;

public sealed class UrlOptions
{
    public string ListeClubs { get; set; } = string.Empty;
    public string DetailClub { get; set; }=string.Empty;
    public string ListeEquipePourClub { get;  set; }=string.Empty;
    public string ListeOrganisme { get;  set; } = string.Empty;
    public string ListeJoueurClassement { get;  set; }= string.Empty;
    public string ListeJoueurLicenceSpid { get; set; } = string.Empty;
    public string JoueurClassement { get; set; } = string.Empty;
    public string JoueurLicenceSpid { get; set; } = string.Empty;
    public string JoueurParties { get; set; } = string.Empty;
    public string EpreuveOrganisme { get;  set; } = string.Empty;
    public string Division { get;  set; } = string.Empty;
}

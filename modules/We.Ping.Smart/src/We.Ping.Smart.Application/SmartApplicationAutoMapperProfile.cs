using AutoMapper;
using System.Text.RegularExpressions;
using Volo.Abp.AutoMapper;
using We.Ping.Smart.Entities;
using We.Ping.Smart.Entities.Clubs;
using We.Ping.Smart.Entities.Organismes;
using We.Ping.Smart.Joueurs;

namespace We.Ping.Smart;

public class SmartApplicationAutoMapperProfile : Profile
{
    public SmartApplicationAutoMapperProfile()
    {
        /* You can configure your AutoMapper mapping configuration here.
         * Alternatively, you can split your mapping configurations
         * into multiple profile classes for a better organization. */
        this.CreateMap<ClubPourDepartement, ClubPourDepartementDto>()
            .Ignore(x => x.Details)
            .Ignore(x => x.IsLoadingDetail)
            .Ignore(x => x.Equipes)
            .Ignore(x => x.NombreEquipe)
            .ReverseMap();
        this.CreateMap<ClubDetail, ClubDetailDto>().ReverseMap();
        this.CreateMap<EquipePourClub, EquipePourClubDto>()
            .Ignore(x => x.Phase)
            .Ignore(x => x.CodeEpreuve)
            .Ignore(x => x.CodeOrganisme)

            .Ignore(x => x.Poule)
            .AfterMap((x, y) =>
            {
                var tmp = y.Equipe.ToPhase();
                y.Phase = tmp.Item2;
                y.Equipe = tmp.Item1;

                var tmp0 = x.Epreuve.Split('_');
                y.Epreuve = tmp0?[1] ?? x.Epreuve;
                y.CodeEpreuve = tmp0?[0] ?? string.Empty;

                var tmp1 = x.Division.ToDivision();
                y.CodeOrganisme = tmp1.Item1;
                y.Division = tmp1.Item2;
                y.Poule = tmp1.Item3;
            })

            ;

        this.CreateMap<EquipePourClubDto, EquipePourClub>()

            .AfterMap((x, y) =>
            {
                y.Equipe = $"{x.Equipe} - Phase {x.Phase}";
                y.Epreuve = $"{x.CodeEpreuve}_{x.Epreuve}";
                y.Division = $"{x.CodeOrganisme}_{x.Division} PH {x.Phase} Poule {x.Poule}";
            })
            ;

        this.CreateMap<NombreEquipePourClub, NombreEquipePourClubDto>().ReverseMap();

        this.CreateMap<Organisme, OrganismeDto>().ReverseMap();

        this.CreateMap<JoueurClassement, JoueurClassementDto>().ReverseMap();

        this.CreateMap<JoueurLicenceSpid, JoueurLicenceSpidDto>()
            .Ignore(x => x.IdLicence)
            .AfterMap((x, y) =>
            {
                y.Sexe = x.Sexe.ToUpper() switch
                {
                    "M" => "Homme",
                    "F" => "Femme",
                    _ => "Indefini"
                };
            });

        this.CreateMap<JoueurLicenceSpidDto, JoueurLicenceSpid>()
            .AfterMap((x, y) =>
            {
                y.Sexe = x.Sexe.ToUpper() switch
                {
                    "HOMME" => "M",
                    "FEMME" => "F",
                    _ => "Indefini"
                };
            });

        this.CreateMap<JoueurLicenceSpid_, JoueurLicenceSpidDto>();

        this.CreateMap<JoueurLicenceSpidDto, JoueurLicenceSpid_>();

        this.CreateMap<JoueurPartie, JoueurPartieDto>()
            .Ignore(x=>x.Date)
            .AfterMap((x, y) =>
            {
                y.Victoire = x.Victoire.ToUpper() switch
                {
                    "V" => "Vicoire",
                    "D" => "Defaite",
                    _ => "Indefini"
                };
                if (DateOnly.TryParse(x.Date, out var dd))
                    y.Date = dd;
            });

        this.CreateMap<JoueurPartieDto, JoueurPartie>()
            .Ignore(x => x.Date)
            .AfterMap((x, y) =>
            {
                y.Victoire = x.Victoire.ToUpper() switch
                {
                    "VICTOIRE" => "V",
                    "DEFAITE" => "D",
                    _ => "Indefini"
                };
                y.Date = x.Date?.ToString() ??string.Empty;
            });

        this.CreateMap<EpreuveOrganisme, EpreuveOrganismeDto>().ReverseMap();

        this.CreateMap<Division,DivisionDto>().ReverseMap();
    }


}
static class TransformExtensions
{
    const string EQUIPE_PATTERN = @"(?<Equipe>(\w*\s+)*\(*\d{1,2})+\)*.*Phase\s*(?<Phase>\d+)+";//voir equipe n° 11340010
    const string DIVISION_PATTERN = @"(?<Organisme>[A-Za-z]{0,3}\d{0,2})_(?<Niveau>[a-zA-z\s]*[0-9]{0,2}).*Poule\s(?<Poule>\d{1})";

    public static (string, string) ToPhase(this string value)
    {
        var match = Regex.Match(value, EQUIPE_PATTERN);
        if (match.Success)
            return (match.Groups["Equipe"].Value.Replace("(", string.Empty).Replace(")", string.Empty), match.Groups["Phase"].Value);
        else
            return (string.Empty, string.Empty);
    }

    public static (string, string, string) ToDivision(this string value)
    {
        var match = Regex.Match(value, DIVISION_PATTERN);
        if (match.Success)
            return (match.Groups["Organisme"].Value, match.Groups["Niveau"].Value, match.Groups["Poule"].Value);
        else
            return (string.Empty, string.Empty, string.Empty);
    }
}

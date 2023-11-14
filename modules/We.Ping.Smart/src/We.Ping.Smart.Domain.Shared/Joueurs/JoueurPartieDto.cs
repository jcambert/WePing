using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Volo.Abp.Application.Dtos;

namespace We.Ping.Smart.Joueurs;

public class JoueurPartieDto:EntityDto
{
    public DateOnly? Date { get; set; } 

    public string NomAdversaire { get; set; } = string.Empty;

    public string ClassementAdversaire { get; set; } = string.Empty;

    public string Epreuve { get; set; } = string.Empty;

    public string Victoire { get; set; } = string.Empty;

    public string Forfait { get; set; } = string.Empty;

    public string Coeficient { get; set; } = string.Empty;
    public string Id { get; set; } = string.Empty;
}

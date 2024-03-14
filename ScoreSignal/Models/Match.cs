using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScoreSignal.Models;
public class Match
{
public int MatchId { get; set; }
[DisplayName("Équipe à Domicile")]
public string? Equipe1 { get; set; }
[DisplayName("Équipe à l'exterieur")]
public string? Equipe2 { get; set; }

public string? Ligue { get; set; }
[DisplayName("Nombre de buts de l'équipe à Domicile")]
public int ScoreEquipe1 { get; set; }
[DisplayName("Nombre de buts de l'équipe à l'Exterieur")]
public int ScoreEquipe2 { get; set; }
[DisplayName("Date du coup d'envoi")]
[DataType(DataType.Date)]
public DateTime Date { get; set; }
[DisplayName("Temps du jeu")]
public string? Temps { get; set; }

public ICollection<Evenement>? Evenements { get; set; }
}
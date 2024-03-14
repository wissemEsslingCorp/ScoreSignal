using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScoreSignal.Models;
public class Evenement
{
public int Id { get; set; }
public int Score { get; set; } 
public string? Buteur { get; set; }

public string? Etat_match { get; set; }
public string? Description { get; set; }
[DataType(DataType.Date)]
[DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:HH:mm}")]
public DateTime Temps { get; set; }
[ForeignKey("Match")]
public int MatchId { get; set; }
public Match? Match { get; set; } 
public ICollection<Evenement>? Evenements { get; set; }
}
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace ScoreSignal.Models;
public class Equipe
{
public int EquipeId { get; set; }
public string? Nom { get; set; }
public string? Abreviation { get; set; }
}
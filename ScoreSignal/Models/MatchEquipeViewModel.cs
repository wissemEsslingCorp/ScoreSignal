namespace ScoreSignal.Models;
public class MatchEquipeViewModel
{
    public Match? Match { get; set; }
    public  ICollection<Equipe>? Equipes { get; set; }
}
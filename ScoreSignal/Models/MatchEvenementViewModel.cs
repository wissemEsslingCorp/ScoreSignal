namespace ScoreSignal.Models;
public class MatchEvenementViewModel
{
    public Match? Match { get; set; }
    public  ICollection<Evenement>? Evenement { get; set; }
    public Evenement? NouvelEvenement { get; set; }
}
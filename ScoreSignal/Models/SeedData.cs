using Microsoft.EntityFrameworkCore;
using ScoreSignal.Data;

namespace ScoreSignal.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MGContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MGContext>>()))
        {
            if (context == null || context.Match == null)
            {
                throw new ArgumentNullException("Null MGContext");
            }

            // Look for any movies.
            if (context.Match.Any())
            {
                return;   // DB has been seeded
            }

            context.Match.AddRange(
               new Match
                    {
                        Equipe1 = "PSG",
                        Equipe2 = "FCB",
                        Ligue = "LDC",
                        ScoreEquipe1 = 0,
                        ScoreEquipe2 = 0,
                        Temps = null,
                        Evenements = []
                    }
            );
            context.Equipe.AddRange(
                new Equipe 
                {
                    Nom = "Paris Saint Germain",
                    Abreviation = "PSG"
                }, 
                new Equipe
                {
                     Nom = "FC Barcelone",
                    Abreviation = "BAR"
                }, 
                  new Equipe
                {
                     Nom = "FC Bayern",
                    Abreviation = "FCB"
                },
                   new Equipe
                {
                     Nom = "Réal Madrid",
                    Abreviation = "RMA"
                }



            );
            context.SaveChanges();
        }
    }
}
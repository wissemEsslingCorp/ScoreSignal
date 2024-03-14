using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ScoreSignal.Models;

namespace ScoreSignal.Data
{
    public class MGContext : DbContext
    {
        public MGContext (DbContextOptions<MGContext> options)
            : base(options)
        {
        }

        public DbSet<ScoreSignal.Models.Match> Match { get; set; } = default!;
        public DbSet<ScoreSignal.Models.Evenement> Evenement { get; set; } = default!;
        public DbSet<ScoreSignal.Models.Equipe> Equipe { get; set; } = default!;
    }
}

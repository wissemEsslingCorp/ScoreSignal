using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ScoreSignal.Data;
using ScoreSignal.Models;

namespace ScoreSignal.Controllers
{
    public class MatchsController : Controller
    {
        private readonly MGContext _context;

        public MatchsController(MGContext context)
        {
            _context = context;
        }

        // GET: Matchs
        public async Task<IActionResult> Index(string? ligue)
        {
            IQueryable<Match> query = _context.Set<Match>();

            if (!string.IsNullOrEmpty(ligue))
            {
                query = query.Where(match => match.Ligue == ligue);
            }

            var matches = await query.ToListAsync();
            return View(matches);
          
        }

        // GET: Matchs/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .Include(m => m.Evenements) // Inclure les événements associés au match
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // GET: Matchs/Create
        public IActionResult Create()
        {
             IQueryable<Equipe> equipeQuery = _context.Set<Equipe>();

        // Récupérer toutes les équipes
        var equipes = equipeQuery.ToList();

        // Créer une instance de MatchEquipeViewModel pour transmettre à la vue
        var matchEquipeViewModel = new MatchEquipeViewModel
        {
           
            Match = null, 
            Equipes = equipes 

        }; 
           return View(matchEquipeViewModel); 
        }

      
        // GET: Matchs/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
           var match = await _context.Match.FindAsync(id);
            var evenements = await _context.Evenement
                                    .Where(e => e.MatchId == id)
                                    .ToListAsync();

            var matchEvenementViewModel = new MatchEvenementViewModel
            {
                Match = match,
                Evenement = evenements
            };

           

            return View(matchEvenementViewModel);
           // return View(match);
        }

       

        // GET: Matchs/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var match = await _context.Match
                .FirstOrDefaultAsync(m => m.MatchId == id);
            if (match == null)
            {
                return NotFound();
            }

            return View(match);
        }

        // POST: Matchs/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match != null)
            {
                _context.Match.Remove(match);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchId == id);
        }
    }
}

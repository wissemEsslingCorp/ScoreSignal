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
    public class EvenementController : Controller
    {
        private readonly MGContext _context;

        public EvenementController(MGContext context)
        {
            _context = context;
        }

        // GET: Evenement
        public async Task<IActionResult> Index()
        {
            return View(await _context.Evenement.ToListAsync());
        }

        // GET: Evenement/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenement == null)
            {
                return NotFound();
            }

        

            return View(evenement);
        }

        // GET: Evenement/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Evenement/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MatchEvenementViewModel viewModel)
    {
        if (ModelState.IsValid)
        {
            // Mapping the data from the view model to the Evenement model
            var evenement = new Evenement
            {
                Description = viewModel.NouvelEvenement.Description,
                Buteur = viewModel.NouvelEvenement.Buteur,
                Temps = viewModel.NouvelEvenement.Temps,
                MatchId = viewModel.NouvelEvenement.MatchId
            };

            _context.Add(evenement);

            try
            {
                await _context.SaveChangesAsync();
                 return RedirectToAction("Edit", "Matchs", new { id = viewModel.NouvelEvenement.MatchId });;
            }
            catch (DbUpdateException ex)
            {
                // Log or handle the exception
                ModelState.AddModelError("", "Unable to save changes. Try again.");
            }
        }

        // If ModelState is not valid, return to the view with the current viewModel
        return View(viewModel);
    }

        // GET: Evenement/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }
            return View(evenement);
        }

        // POST: Evenement/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Score,Temps,EvenementId")] Evenement evenement)
        {
            if (id != evenement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(evenement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EvenementExists(evenement.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(evenement);
        }

        // GET: Evenement/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var evenement = await _context.Evenement
                .FirstOrDefaultAsync(m => m.Id == id);
            if (evenement == null)
            {
                return NotFound();
            }

            return View(evenement);
        }

        // POST: Evenement/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement != null)
            {
                _context.Evenement.Remove(evenement);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EvenementExists(int id)
        {
            return _context.Evenement.Any(e => e.Id == id);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ScoreSignal.Data;
using ScoreSignal.Models;

namespace ScoreSignal.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EvenementsAPIController : ControllerBase
    {
        private readonly MGContext _context;

        public EvenementsAPIController(MGContext context)
        {
            _context = context;
        }

        // GET: api/EvenementsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Evenement>>> GetEvenement()
        {
            return await _context.Evenement.ToListAsync();
        }

        // GET: api/EvenementsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<IEnumerable<Evenement>>> GetEvenement(int id)
        {
             var evenements = await _context.Evenement
        .Where(e => e.MatchId == id)
        .ToListAsync();

        return evenements;
        }

        // PUT: api/EvenementsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutEvenement(int id, Evenement evenement)
        {
            if (id != evenement.Id)
            {
                return BadRequest();
            }

            _context.Entry(evenement).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!EvenementExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/EvenementsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Evenement>> PostEvenement(Evenement evenement)
        {
            _context.Evenement.Add(evenement);
            await _context.SaveChangesAsync();
            

            return CreatedAtAction("GetEvenement", new { id = evenement.Id }, evenement);
        }

        // DELETE: api/EvenementsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteEvenement(int id)
        {
            var evenement = await _context.Evenement.FindAsync(id);
            if (evenement == null)
            {
                return NotFound();
            }

            _context.Evenement.Remove(evenement);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool EvenementExists(int id)
        {
            return _context.Evenement.Any(e => e.Id == id);
        }
    }
}

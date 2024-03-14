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
    public class MatchsAPIController : ControllerBase
    {
        private readonly MGContext _context;

        public MatchsAPIController(MGContext context)
        {
            _context = context;
        }

        // GET: api/MatchsAPI
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Match>>> GetMatch(string? ligue)
        {
             IQueryable<Match> query = _context.Set<Match>();

            if (!string.IsNullOrEmpty(ligue))
            {
                query = query.Where(match => match.Ligue == ligue);
            }

            var matches = await query.ToListAsync();
            return matches;
        }

        // GET: api/MatchsAPI/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Match>> GetMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);

            if (match == null)
            {
                return NotFound();
            }

            return match;
        }

        // PUT: api/MatchsAPI/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutMatch(int id, Match match)
        {
            Console.WriteLine($"ID dans l'URL : {id}");
            Console.WriteLine($"ID dans le corps de la requête : {match.MatchId}");
            if (id != match.MatchId)
            {
                return BadRequest("ID mismatch");
            }

            _context.Entry(match).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MatchExists(id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest("Concurrency exception");
                }
            }
          
            return CreatedAtAction("PutMatch",id,match);
        }

        // POST: api/MatchsAPI
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Match>> PostMatch(Match match)
        {
            _context.Match.Add(match);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetMatch", new { id = match.MatchId }, match);
        }

        // DELETE: api/MatchsAPI/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteMatch(int id)
        {
            var match = await _context.Match.FindAsync(id);
            if (match == null)
            {
                return NotFound();
            }

            _context.Match.Remove(match);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool MatchExists(int id)
        {
            return _context.Match.Any(e => e.MatchId == id);
        }
    }
}

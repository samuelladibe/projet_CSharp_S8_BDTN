using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PopulationMondiale.Data;
using PopulationMondiale.Models;

namespace PopulationMondiale.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaysController : ControllerBase
    {
        private readonly PopulationMondialeContext _context;

        public PaysController(PopulationMondialeContext context)
        {
            _context = context;
        }

        // GET: api/Pays
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pays>>> GetPays()
        {
            return await _context.Pays.Include("Population_").ToListAsync();
        }

        // GET: api/Pays/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Pays>> GetPays(int id)
        {
            var pays = await _context.Pays.Include(p => p.Population_).FirstOrDefaultAsync(p => p.Id == id);

            if (pays == null)
            {
                return NotFound();
            }

            return pays;
        }

    
        // PUT syntax: api/Pays/{id}
        // PUT: api/Pays/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPays(int id, Pays pays)
        {
            if (id != pays.Id)
            {
                return BadRequest();
            }

            var existingPays = await _context.Pays.FindAsync(id);

            if (existingPays == null)
            {
                return NotFound();
            }

            existingPays.NomPays = pays.NomPays;
            existingPays.ContinentId = pays.ContinentId;
            existingPays.Population_ = pays.Population_;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PaysExists(id))
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
        
        //Population of a country for a given year: api/pays/{countryName}/{year}
        [HttpGet("{countryName}/{year}")]
        public ActionResult<Population> GetPopulationData(string countryName, int year)
        {
            var country = _context.Pays.SingleOrDefault(c => c.NomPays == countryName);

            if (country == null)
            {
                return NotFound();
            }

            var population = _context.Population.SingleOrDefault(p => p.Annee == year && p.PaysId == country.Id);

            if (population == null)
            {
                return NotFound();
            }

            return population;
        }
        
        // POST: api/Pays
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Pays>> PostPays(Pays pays)
        {
            _context.Pays.Add(pays);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPays", new { id = pays.Id }, pays);
        }

        // DELETE: api/Pays/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePays(int id)
        {
            var pays = await _context.Pays.FindAsync(id);
            if (pays == null)
            {
                return NotFound();
            }

            _context.Pays.Remove(pays);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PaysExists(int id)
        {
            return _context.Pays.Any(e => e.Id == id);
        }
    }
}

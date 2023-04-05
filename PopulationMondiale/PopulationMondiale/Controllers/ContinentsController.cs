﻿using System;
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
    public class ContinentsController : ControllerBase
    {
        private readonly PopulationMondialeContext _context;

        public ContinentsController(PopulationMondialeContext context)
        {
            _context = context;
        }

        // GET: api/Continents
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Continent>>> GetContinent()
        {
            return await _context.Continent.Include("Pays_.Population_").ToListAsync();
        }

        // GET: api/Continents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Continent>> GetContinent(int id)
        {
            var continent = await _context.Continent
                .Include(c => c.Pays_)
                .ThenInclude(p => p.Population_)
                .SingleOrDefaultAsync(c => c.Id == id);

            if (continent == null)
            {
                return NotFound();
            }

            return continent;
        }


        // PUT: api/Continents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutContinent(int id, Continent continent)
        {
            if (id != continent.Id)
            {
                return BadRequest();
            }

            var existingContinent = await _context.Continent.FindAsync(id);

            if (existingContinent == null)
            {
                return NotFound();
            }
            
            existingContinent.NomContinent = continent.NomContinent;
            existingContinent.Pays_ = continent.Pays_;
            
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ContinentExists(id))
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

        // POST: api/Continents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Continent>> PostContinent(Continent continent)
        {
            _context.Continent.Add(continent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetContinent", new { id = continent.Id }, continent);
        }

        // DELETE: api/Continents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteContinent(int id)
        {
            var continent = await _context.Continent.FindAsync(id);
            if (continent == null)
            {
                return NotFound();
            }

            _context.Continent.Remove(continent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        // Get the population of a continent for a given year: api/Continents/{continentName}/{year}
        [HttpGet("{continentName}/{year}")]
        public ActionResult<long> GetPopulationData(string continentName, int year)
        {
            var continent = _context.Continent.Include(c => c.Pays_).ThenInclude(country => country.Population_).SingleOrDefault(c => c.NomContinent == continentName);
            
            if (continent == null)
            {
                return NotFound();
            }
            var population = continent.Pays_.SelectMany(p => p.Population_).Where(p => p.Annee == year).Sum(p => p.Valeur);

            return Ok($"La population totale du continent {continentName} pour l'annee {year} est: {population}");
        }

        private bool ContinentExists(int id)
        {
            return _context.Continent.Any(e => e.Id == id);
        }
    }
}

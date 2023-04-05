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
    public class PopulationsController : ControllerBase
    {
        private readonly PopulationMondialeContext _context;

        public PopulationsController(PopulationMondialeContext context)
        {
            _context = context;
        }

        // GET: api/Populations
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Population>>> GetPopulation()
        {
            return await _context.Population.ToListAsync();
        }

        // GET: api/Populations/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Population>> GetPopulation(int id)
        {
            var population = await _context.Population.FindAsync(id);

            if (population == null)
            {
                return NotFound();
            }

            return population;
        }

        // PUT: api/Populations/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPopulation(int id, Population population)
        {
            if (id != population.Id)
            {
                return BadRequest();
            }

            var existingPopulation = await _context.Population.FindAsync(id);

            if (existingPopulation == null)
            {
                return NotFound();
            }

            existingPopulation.Annee = population.Annee;
            existingPopulation.Valeur = population.Valeur;
            existingPopulation.PaysId = population.PaysId;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PopulationExists(id))
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

        // POST: api/Populations
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Population>> PostPopulation(Population population)
        {
            _context.Population.Add(population);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPopulation", new { id = population.Id }, population);
        }

        // DELETE: api/Populations/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePopulation(int id)
        {
            var population = await _context.Population.FindAsync(id);
            if (population == null)
            {
                return NotFound();
            }

            _context.Population.Remove(population);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        private bool PopulationExists(int id)
        {
            return _context.Population.Any(e => e.Id == id);
        }
    }
}

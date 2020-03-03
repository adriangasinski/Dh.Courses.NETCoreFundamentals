using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NoweM.Core;
using NoweM.Data;

namespace NoweM.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class HousesController : ControllerBase
    {
        private readonly NoweMDbContext _context;

        public HousesController(NoweMDbContext context)
        {
            _context = context;
        }

        // GET: api/Houses
        [HttpGet]
        public IEnumerable<House> GetHouses()
        {
            return _context.Houses;
        }

        // GET: api/Houses/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetHouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var house = await _context.Houses.FindAsync(id);

            if (house == null)
            {
                return NotFound();
            }

            return Ok(house);
        }

        // PUT: api/Houses/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutHouse([FromRoute] int id, [FromBody] House house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != house.Id)
            {
                return BadRequest();
            }

            _context.Entry(house).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!HouseExists(id))
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

        // POST: api/Houses
        [HttpPost]
        public async Task<IActionResult> PostHouse([FromBody] House house)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Houses.Add(house);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetHouse", new { id = house.Id }, house);
        }

        // DELETE: api/Houses/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteHouse([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var house = await _context.Houses.FindAsync(id);
            if (house == null)
            {
                return NotFound();
            }

            _context.Houses.Remove(house);
            await _context.SaveChangesAsync();

            return Ok(house);
        }

        private bool HouseExists(int id)
        {
            return _context.Houses.Any(e => e.Id == id);
        }
    }
}
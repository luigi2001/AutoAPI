using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoAPI.Model;
using Nancy.Json;
using System.Text.Json;
using Nancy;

namespace AutoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AcquirentesController : ControllerBase
    {
        private readonly DbEngineContext _context;

        public AcquirentesController(DbEngineContext context)
        {
            _context = context;
        }

        // GET: api/Acquirentes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Acquirente>>> Getacquirente()
        {
            return await _context.acquirente.ToListAsync();
        }

        [HttpGet("&{name}")]
        // GET: Star Wars
        public async Task<string> GetStarWars(string name)
        {
            using HttpClient client = new();
            var array = await client.GetStringAsync("https://swapi.dev/api/people/?search=" + name);
            var index1 = array.IndexOf("birth_year\":\"");
            var substring = array.Substring(index1);
            var split = substring.Split(",")[0].Split(":\"")[1];
            var birtday = split.Substring(0, 5);

            var JsonFinal = System.Text.Json.JsonSerializer.Serialize(birtday, new JsonSerializerOptions() { WriteIndented = true });

            return JsonFinal;
        }

        // GET: api/Acquirentes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Acquirente>> GetAcquirente(int id)
        {
            var acquirente = await _context.acquirente.FindAsync(id);

            if (acquirente == null)
            {
                return NotFound();
            }

            return acquirente;
        }

        // PUT: api/Acquirentes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAcquirente(int id, Acquirente acquirente)
        {
            if (id != acquirente.ID)
            {
                return BadRequest();
            }

            _context.Entry(acquirente).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AcquirenteExists(id))
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

        // POST: api/Acquirentes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Acquirente>> PostAcquirente(Acquirente acquirente)
        {
            _context.acquirente.Add(acquirente);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetAcquirente", new { id = acquirente.ID }, acquirente);
        }

        // DELETE: api/Acquirentes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAcquirente(int id)
        {
            var acquirente = await _context.acquirente.FindAsync(id);
            if (acquirente == null)
            {
                return NotFound();
            }

            _context.acquirente.Remove(acquirente);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AcquirenteExists(int id)
        {
            return _context.acquirente.Any(e => e.ID == id);
        }
    }
}

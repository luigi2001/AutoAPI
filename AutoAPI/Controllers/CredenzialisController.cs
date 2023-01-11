using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AutoAPI.Model;
using System.Security.Cryptography;
using System.Text;

namespace AutoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CredenzialisController : ControllerBase
    {
        private readonly DbEngineContext _context;

        public CredenzialisController(DbEngineContext context)
        {
            _context = context;
        }

        // GET: api/Credenzialis
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Credenziali>>> Getcredenziali()
        {
            return await _context.credenziali.ToListAsync();
        }

        // GET: api/Credenzialis/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Credenziali>> GetCredenziali(int id)
        {
            var credenziali = await _context.credenziali.FindAsync(id);

            if (credenziali == null)
            {
                return NotFound();
            }

            return credenziali;
        }

        // PUT: api/Credenzialis/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCredenziali(int id, Credenziali credenziali)
        {
            if (id != credenziali.ID)
            {
                return BadRequest();
            }

            _context.Entry(credenziali).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CredenzialiExists(id))
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

        // POST: api/Credenzialis
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Credenziali>> PostCredenziali(Credenziali credenziali)
        {
            using (Aes myAes = Aes.Create())
            {
                byte[] encrypted = Cryptography.Cryptography.EncryptStringToBytes_Aes(credenziali.password, myAes.Key, myAes.IV);
                credenziali.password = Convert.ToBase64String(encrypted);
                credenziali.key = Convert.ToBase64String(myAes.Key);
                credenziali.IV = Convert.ToBase64String(myAes.IV);
            }

            _context.credenziali.Add(credenziali);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCredenziali", new { id = credenziali.ID }, credenziali);
        }

        // DELETE: api/Credenzialis/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCredenziali(int id)
        {
            var credenziali = await _context.credenziali.FindAsync(id);
            if (credenziali == null)
            {
                return NotFound();
            }

            _context.credenziali.Remove(credenziali);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool CredenzialiExists(int id)
        {
            return _context.credenziali.Any(e => e.ID == id);
        }
    }
}

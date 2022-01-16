using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZadanieApi.Models;
using Swashbuckle.AspNetCore.Annotations;

namespace ZadanieApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PrezentsController : ControllerBase
    {
        private readonly PrezentContext _context;

        public PrezentsController(PrezentContext context)
        {
            _context = context;
        }




        // GET: api/Prezents
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [SwaggerResponse(200, "Success")]
        public async Task<ActionResult<IEnumerable<Prezent>>> GetPrezents()
        {
            return await _context.Prezents.ToListAsync();
        }

        // GET: api/Prezents/5
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(404, "Nie znaleziono Prezentu o podanym {id}")]
        [SwaggerResponse(200, "Success")]


        public async Task<ActionResult<Prezent>> GetPrezent(int id)
        {
            var prezent = await _context.Prezents.FindAsync(id);

            if (prezent == null)
            {
                return NotFound();
            }

            return prezent;
        }

        // PUT: api/Prezents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [SwaggerResponse(404, "Nie znaleziono Prezentu o podanym {id}")]
        [SwaggerResponse(400, "Zle zapytanie")]
       
        public async Task<IActionResult> PutPrezent(int id, Prezent prezent)
        {
            if (id != prezent.Id)
            {
                return BadRequest();
            }

            _context.Entry(prezent).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PrezentExists(id))
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

        // POST: api/Prezents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [SwaggerResponse(201, "Utworzono")]
        public async Task<ActionResult<Prezent>> PostPrezent(Prezent prezent)
        {
            _context.Prezents.Add(prezent);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPrezent", new { id = prezent.Id }, prezent);
        }

        // DELETE: api/Prezents/5
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [SwaggerResponse(204, "Brak zawartosci {id}")]
        [SwaggerResponse(404, "Nie znaleziono Prezentu o podanym {id}")]
        public async Task<IActionResult> DeletePrezent(int id)
        {
            var prezent = await _context.Prezents.FindAsync(id);
            if (prezent == null)
            {
                return NotFound();
            }

            _context.Prezents.Remove(prezent);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool PrezentExists(int id)
        {
            return _context.Prezents.Any(e => e.Id == id);
        }
    }
}

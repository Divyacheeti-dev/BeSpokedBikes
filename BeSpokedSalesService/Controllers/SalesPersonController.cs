using BeSpokedSalesModel.DBModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BeSpokedSalesService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SalespeopleController : ControllerBase
    {
        private readonly AppDBContext _context;

        public SalespeopleController(AppDBContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Salesperson>>> GetSalespeoples()

        {
            return await _context.Salespeople.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Salesperson>> GetSalespeople(int id)
        {
            var Salespeople = await _context.Salespeople.FindAsync(id);

            if (Salespeople == null)
            {
                return NotFound();
            }

            return Salespeople;
        }

        [HttpPost]
        public async Task<ActionResult<Salesperson>> PostSalespeople(Salesperson Salespeople)
        {
            _context.Salespeople.Add(Salespeople);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSalespeople", new
            {
                id = Salespeople.SalespersonId
            }, Salespeople);

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutSalespeople(int id, Salesperson Salespeople)
        {
            if (id != Salespeople.SalespersonId)
            {
                return BadRequest();
            }

            _context.Entry(Salespeople).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SalespeopleExists(id))
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

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteSalespeople(int id)
        {
            var Salespeople = await _context.Salespeople.FindAsync(id);

            if (Salespeople == null)
            {
                return NotFound();
            }

            _context.Salespeople.Remove(Salespeople);
            await _context.SaveChangesAsync();


            return NoContent();
        }

        private bool SalespeopleExists(int id)
        {
            return _context.Salespeople.Any(e => e.SalespersonId == id);
        }
    }
}

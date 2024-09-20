using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaMercado.Data;
using SistemaMercado.Models;

namespace SistemaMercado.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountController : Controller
    {
        private readonly SistemaMercadoDbContext _context;

        public AccountController(SistemaMercadoDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetAccount()
        {
            try
            {
                var result = await _context.Account.ToListAsync();
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in account listing. Exception  {e.Message}");
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAccount([FromBody] Account account)
        {
            try
            {
                await _context.Account.AddAsync(account);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok("Inserted account");
                }
                else
                {
                    return BadRequest("Account not included");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error in account listing. Exception  {e.Message}");
            }
        }

        [HttpPut]
        public async Task<IActionResult> PutAccount([FromBody] Account account)
        {
            try
            {
                _context.Account.Update(account);
                var result = await _context.SaveChangesAsync();

                if (result == 1)
                {
                    return Ok("Update account");
                }
                else
                {
                    return BadRequest("Account not update");
                }
                return Ok(result);
            }
            catch (Exception e)
            {
                return BadRequest($"Error updating account. Exception  {e.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAccount([FromRoute] Guid id)
        {
            try
            {
                Account account = await _context.Account.FindAsync(id);
                
                if (account != null)
                {
                    _context.Account.Remove(account);
                    var result = await _context.SaveChangesAsync();

                    if (result == 1)
                    {
                        return Ok("Account deleted.");
                    }
                    else
                    {
                        return BadRequest("Account not deleted");
                    }
                }
                else
                {
                    return NotFound("Account not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error deleting account. Exception  {e.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> AccountSearch([FromRoute] Guid id)
        {
            try
            {
                Account account = await _context.Account.FindAsync(id);
                if (account != null)
                {
                    return Ok(account);
                }
                else
                {
                    return NotFound("Account not found");
                }
            }
            catch (Exception e)
            {
                return BadRequest($"Error in search account. Exception  {e.Message}");
            }
        }
    }
}

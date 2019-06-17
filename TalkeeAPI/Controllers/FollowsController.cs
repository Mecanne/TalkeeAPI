using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalkeeAPI.Models;

namespace TalkeeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FollowsController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Follows
        [HttpGet]
        public IEnumerable<Follows> GetFollows()
        {
            return _context.Follows;
        }

        // GET: api/Follows/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollows([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var follows = from user in _context.Users
                            join follow in _context.Follows
                            on user.UserID equals follow.FollowedID
                            where follow.UserID == id
                            select user;

            if (follows == null)
            {
                return NotFound();
            }

            return Ok(follows);
        }

        // PUT: api/Follows/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollows([FromRoute] int id, [FromBody] Follows follows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != follows.UserID)
            {
                return BadRequest();
            }

            _context.Entry(follows).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowsExists(id))
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

        // POST: api/Follows
        [HttpPost]
        public async Task<IActionResult> PostFollows([FromBody] Follows follows)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Follows.Add(follows);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollows", new { id = follows.UserID }, follows);
        }

        // DELETE: api/Follows/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollows([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var follows = await _context.Follows.FindAsync(id);
            if (follows == null)
            {
                return NotFound();
            }

            _context.Follows.Remove(follows);
            await _context.SaveChangesAsync();

            return Ok(follows);
        }

        private bool FollowsExists(int id)
        {
            return _context.Follows.Any(e => e.UserID == id);
        }
    }
}
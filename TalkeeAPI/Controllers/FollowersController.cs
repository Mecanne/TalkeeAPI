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
    public class FollowersController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowersController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Followers
        [HttpGet]
        public IEnumerable<Followers> GetFollowers()
        {
            return _context.Followers;
        }

        // GET: api/Followers/5
        [HttpGet("{id}")]
        public IActionResult GetFollowers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followers = from user in _context.Users
                            join follower in _context.Followers
                            on user.UserID equals follower.UserID
                            where follower.UserID == id
                            select user;

            if (followers == null)
            {
                return NotFound();
            }

            return Ok(followers);
        }

        // PUT: api/Followers/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowers([FromRoute] int id, [FromBody] Followers followers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followers.UserID)
            {
                return BadRequest();
            }

            _context.Entry(followers).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowersExists(id))
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

        // POST: api/Followers
        [HttpPost]
        public async Task<IActionResult> PostFollowers([FromBody] Followers followers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Followers.Add(followers);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowers", new { id = followers.UserID }, followers);
        }

        // DELETE: api/Followers/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteFollowers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followers = await _context.Followers.FindAsync(id);
            if (followers == null)
            {
                return NotFound();
            }

            _context.Followers.Remove(followers);
            await _context.SaveChangesAsync();

            return Ok(followers);
        }

        private bool FollowersExists(int id)
        {
            return _context.Followers.Any(e => e.UserID == id);
        }
    }
}
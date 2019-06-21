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
    public class FollowController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public FollowController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Follow
        [HttpGet]
        public IEnumerable<FollowModel> GetFollow()
        {
            return _context.Follow;
        }

        // GET: api/Follow/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetFollowModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followModel = await _context.Follow.FindAsync(id);

            if (followModel == null)
            {
                return NotFound();
            }

            return Ok(followModel);
        }

        // GET: api/Follow/5
        [HttpGet("followers/{id}")]
        public IActionResult GetFollowers([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followModel = from follow in _context.Follow
                              join user in _context.Users
                              on follow.UserID equals user.UserID
                              where follow.FollowID == id
                              select user;


            if (followModel == null)
            {
                return NotFound();
            }

            return Ok(followModel);
        }

        [HttpGet]
        [Route("test")]
        public IActionResult IsFollowing(int userid, int followid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followModel = (from follow in _context.Follow
                               where follow.UserID == userid && follow.FollowID == followid
                               select follow).FirstOrDefault();

            if (followModel == null)
            {
                return Ok(false);
            }

            return Ok(true);
        }

        [HttpGet("follows/{id}")]
        public IActionResult GetFollows([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followModel = from follow in _context.Follow
                              join user in _context.Users
                              on follow.FollowID equals user.UserID
                              where follow.UserID == id
                              select user;


            if (followModel == null)
            {
                return NotFound();
            }

            return Ok(followModel);
        }

        // PUT: api/Follow/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutFollowModel([FromRoute] int id, [FromBody] FollowModel followModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != followModel.ID)
            {
                return BadRequest();
            }

            _context.Entry(followModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!FollowModelExists(id))
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

        // POST: api/Follow
        [HttpPost]
        public async Task<IActionResult> PostFollowModel([FromBody] FollowModel followModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Follow.Add(followModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetFollowModel", new { id = followModel.ID }, followModel);
        }

        // DELETE: api/Follow/5
        [HttpDelete]
        public async Task<IActionResult> DeleteFollowModel(int userid,int followid)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var followModel = (from follow in _context.Follow
                              where follow.UserID == userid && follow.FollowID == followid
                              select follow).First();

            if (followModel == null)
            {
                return NotFound();
            }

            _context.Follow.Remove(followModel);
            await _context.SaveChangesAsync();

            return Ok(followModel);
        }

        private bool FollowModelExists(int id)
        {
            return _context.Follow.Any(e => e.ID == id);
        }
    }
}
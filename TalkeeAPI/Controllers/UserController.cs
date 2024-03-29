﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TalkeeAPI.Models;

namespace WebApplication.api.Controllers
{
    
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public UserController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/User
        [HttpGet]
        public IEnumerable<UserModel> GetUserModel()
        {
            return _context.Users;
        }

        // GET: api/User/5
        [HttpGet]
        [Route("byid")]
        public IActionResult GetUserByIDModel(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = from user in _context.Users
                            where user.UserID.Equals(id)
                            select user;

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        [HttpGet]
        [Route("followers")]
        public IActionResult GetFollowers(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = from user in _context.Followers
                            where user.UserID.Equals(id)
                            select user;

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        [HttpGet]
        [Route("follows")]
        public IActionResult GetFollows(int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = from user in _context.Followers
                            where user.UserID.Equals(id)
                            select user;

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // GET: api/User/{email}
        [HttpGet("{email}")]
        public IActionResult GetUserModel([FromRoute] string email)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = _context.Users.Where(u => u.Email == email).ToList().FirstOrDefault();

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // GET: api/User/{}
        [HttpGet]
        [Route("search")]
        public IActionResult SearchUsers(string q)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = from user in _context.Users
                            where user.UserName.Contains(q)
                            select user;

            if (userModel == null)
            {
                return NotFound();
            }

            return Ok(userModel);
        }

        // PUT: api/User/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUserModel([FromRoute] int id, [FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != userModel.UserID)
            {
                return BadRequest();
            }

            _context.Entry(userModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserModelExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            var userjson = Newtonsoft.Json.JsonConvert.SerializeObject(userModel);
            return Ok(new { userlog = userjson });
        }

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> PostUserModel([FromBody] UserModel userModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Users.Add(userModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetUserModel", new { id = userModel.UserID }, userModel);
        }

        // DELETE: api/User/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUserModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userModel = await _context.Users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }

            _context.Users.Remove(userModel);
            await _context.SaveChangesAsync();

            return Ok(userModel);
        }

        private bool UserModelExists(int id)
        {
            return _context.Users.Any(e => e.UserID == id);
        }
    }
}
﻿using System;
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
    public class PostController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PostController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Post
        [HttpGet]
        public IEnumerable<PostModel> GetPosts()
        {
            return _context.Posts;
        }

        // GET: api/Post/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPostModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = await _context.Posts.FindAsync(id);

            if (postModel == null)
            {
                return NotFound();
            }

            return Ok(postModel);
        }

        // PUT: api/Post/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutPostModel([FromRoute] int id, [FromBody] PostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != postModel.PostID)
            {
                return BadRequest();
            }

            _context.Entry(postModel).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PostModelExists(id))
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

        // POST: api/Post
        [HttpPost]
        public async Task<IActionResult> PostPostModel([FromBody] PostModel postModel)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            _context.Posts.Add(postModel);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPostModel", new { id = postModel.PostID }, postModel);
        }

        // POST: api/Post
        [HttpGet]
        [Route("getall")]
        public IActionResult GetAllPost(int UserID)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var posts = _context.Posts.Where(post => post.UserID == UserID).ToList();

            return Ok(posts);
        }

        // DELETE: api/Post/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePostModel([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var postModel = await _context.Posts.FindAsync(id);
            if (postModel == null)
            {
                return NotFound();
            }

            _context.Posts.Remove(postModel);
            await _context.SaveChangesAsync();

            return Ok(postModel);
        }

        private bool PostModelExists(int id)
        {
            return _context.Posts.Any(e => e.PostID == id);
        }
    }
}
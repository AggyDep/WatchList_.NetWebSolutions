using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebAPI.Data;
using WebAPI.Models;

namespace WebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatchListsController : ControllerBase
    {
        private readonly WatchListContext _context;

        public WatchListsController(WatchListContext context)
        {
            _context = context;
        }

        // GET: api/WatchLists
        [HttpGet]
        public async Task<ActionResult<IEnumerable<WatchList>>> GetWatchLists()
        {
            return await _context.WatchLists.ToListAsync();
        }

        // GET: api/WatchLists/5
        [HttpGet("{id}")]
        public async Task<ActionResult<WatchList>> GetWatchList(int id)
        {
            var watchList = await _context.WatchLists.FindAsync(id);

            if (watchList == null)
            {
                return NotFound();
            }

            return watchList;
        }

        // PUT: api/WatchLists/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutWatchList(int id, WatchList watchList)
        {
            if (id != watchList.UserId)
            {
                return BadRequest();
            }

            _context.Entry(watchList).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!WatchListExists(id))
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

        // POST: api/WatchLists
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for
        // more details see https://aka.ms/RazorPagesCRUD.
        [HttpPost]
        public async Task<ActionResult<WatchList>> PostWatchList(WatchList watchList)
        {
            _context.WatchLists.Add(watchList);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (WatchListExists(watchList.UserId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetWatchList", new { id = watchList.UserId }, watchList);
        }

        // DELETE: api/WatchLists/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<WatchList>> DeleteWatchList(int id)
        {
            var watchList = await _context.WatchLists.FindAsync(id);
            if (watchList == null)
            {
                return NotFound();
            }

            _context.WatchLists.Remove(watchList);
            await _context.SaveChangesAsync();

            return watchList;
        }

        private bool WatchListExists(int id)
        {
            return _context.WatchLists.Any(e => e.UserId == id);
        }
    }
}

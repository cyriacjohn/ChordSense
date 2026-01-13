using Microsoft.AspNetCore.Mvc;
using ChordSense.Api.Data;
using ChordSense.Api.Models;
using Microsoft.EntityFrameworkCore;

namespace ChordSense.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SongsController : ControllerBase
    {
        private readonly ChordSenseDbContext _context;
        public SongsController(ChordSenseDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> GetSongs()
        {
            var songs = await _context.Songs.ToListAsync();
            return Ok(songs);
        }

        [HttpPost]
        public async Task<IActionResult> AddSong(Song song)
        {
            _context.Songs.Add(song);
            await _context.SaveChangesAsync();
            return Ok(song);
        }

    }
}

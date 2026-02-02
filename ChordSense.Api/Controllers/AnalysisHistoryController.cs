using Microsoft.AspNetCore.Mvc;
using ChordSense.Api.Data;
using Microsoft.EntityFrameworkCore;
using ChordSense.Api.Models;

namespace ChordSense.Api.Controllers
{
    [ApiController]
    [Route("api/history")]
    public class AnalysisHistoryController : ControllerBase
    {
        private readonly ChordSenseDbContext _db;
        public AnalysisHistoryController(ChordSenseDbContext db)
        {
            _db = db;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(
            [FromQuery] string? type,
            [FromQuery] int page = 1,
            [FromQuery] int pageSize = 10)
        {
            var query = _db.AnalysisResults.AsQueryable();

            if (!string.IsNullOrWhiteSpace(type))
            {
                query = query.Where(x => x.AnalysisType == type);
            }

            var totalCount = await query.CountAsync();

            var results = await query.OrderByDescending(x => x.CreatedAt).Skip((page - 1) * pageSize).Take(pageSize).ToListAsync();

            return Ok(new
            {
                totalCount,
                page,
                pageSize,
                results
            });
        }
    }
}

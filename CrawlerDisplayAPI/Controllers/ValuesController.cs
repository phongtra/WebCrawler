using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Content.Data;
using Crawler.Data.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CrawlerDisplayAPI.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        private readonly ContentContext _context;

        public ValuesController(ContentContext context)
        {
            _context = context;
        }
        // GET: api/<controller>
        [HttpGet]
        public async Task<ActionResult<List<WebToon>>> Get()
        {
            return await _context.WebToons.ToListAsync();
        }

        // GET api/<controller>/5
        [HttpGet("{titleNo}")]
        public async Task<ActionResult<List<Episode>>> Get(string titleNo)
        {
            var page = await _context.Episodes.Where(p => p.TitleNo == titleNo).ToListAsync();
            return Ok(page);
        }
        // GET api/<controller>/5
        [HttpGet("{titleNo}/{ep}")]
        public async Task<ActionResult<Page>> Get(string titleNo, string ep)
        {
            var page = await _context.Pages.Where(p => p.EpisodeLinkHash == ep).ToListAsync();
            return Ok(page[0]);
        }

        // POST api/<controller>
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

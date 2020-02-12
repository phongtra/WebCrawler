using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AspNetCore.Proxy;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ComicProxyServer.Controllers
{
    [Route("api/comic")]
    public class ProxyController : Controller
    {
        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        [HttpGet("{id}/{hash}/{something}")]
        public async Task<string> Get(string id, string hash, string something, string type)
        {
            var options = ProxyOptions.Instance.WithBeforeSend((c, hrm) =>
            {
                hrm.Headers.Referrer = new Uri("https://webtoon-phinf.pstatic.net/");
                return Task.CompletedTask;
            });
            await this.ProxyAsync($"https://webtoon-phinf.pstatic.net/{id}/{hash}/{something}?type={type}", options);
            var uri =  await Task.FromResult($"https://webtoon-phinf.pstatic.net/{id}/{hash}/{something}?type={type}");
            return uri;
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

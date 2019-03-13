using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace locationms.Controllers
{
    [Route("api/[controller]")]
    public class ValuesController : Controller
    {
        // GET api/values
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{ip}")]
        public async Task<string> Get(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            string url = "http://api.ipstack.com/" + "{ipAddress}" + "?access_key=" + Environment.GetEnvironmentVariable("ACCESS_KEY") + "&output=json";
            Console.WriteLine(url);
            GeoLocation model = await GeoLocation.QueryGeographicalLocationAsync(ip);

            return JsonConvert.SerializeObject(model);
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody]string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace locationms.Controllers
{
    [Route("/")]
    public class ValuesController : Controller
    {
        // GET /
        [HttpGet]
        public string Get()
        {
            return "Version 1.0";
        }

        // GET {/ip}
        [HttpGet("{ip}")]
        public async Task<string> Get(string ip)
        {
            if (string.IsNullOrEmpty(ip))
            {
                ip = Request.HttpContext.Connection.RemoteIpAddress.MapToIPv4().ToString();
            }

            GeoLocation model = await GeoLocation.QueryGeographicalLocationAsync(ip);

            return JsonConvert.SerializeObject(model);
        }
    }
}

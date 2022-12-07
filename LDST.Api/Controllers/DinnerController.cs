using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LDST.Api.Controllers
{
    [Route("[controller]")]
    public class DinnerController : Controller
    {

        [HttpGet]
        public IActionResult Index()
        {
            return Ok(Array.Empty<string>());
        }
    }
}
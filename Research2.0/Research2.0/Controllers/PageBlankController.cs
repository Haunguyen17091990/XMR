using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Research2._0.Controllers
{
    public class PageBlankController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BilletAPI.Controllers
{
    public class Koebscontroller : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult Get()
        //{
        //    return new Billet;
        //}

        public IActionResult Post(int userID, int eventID)
        {
            return Ok("Yes");
        }
    }
}

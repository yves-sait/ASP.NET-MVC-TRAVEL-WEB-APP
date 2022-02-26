

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    public class PackageController : Controller
    {
        /// <summary>
        /// Blueprint for displaying available package
        /// </summary>
        /// <returns>package objects</returns>

        // GET: PackageController
        public ActionResult Package()
        {
            List<Package> packages = null;

            try
            {
                packages = PackageManager.GetPackages(); // get the list of all packages
            }
            catch
            {
                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
            }

            return View(packages); //pass the package object to view
        }

       
    }
}

/*
 * 
 * Project Workshop 5
 * Author: Sung Jai Kim & Hugo Schrupp Suarez
 * Date: February 2022
 * 
 */

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public static class PackageManager
    {

        /// <summary>
        /// get all packages
        /// </summary>
        /// <returns>list of packages</returns>
        /// 

        public static List<Package> GetPackages()
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();

            return db.Packages.ToList();

        }

    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExpertsMVC.Models
{
    /// <summary>
    /// Blueprint for validating uniqueness of customer username
    /// </summary>
    public static class FormFieldValidation
    {

        public static string UsernameExist(string custUsername)
        {
            string msg = "";
            if (!String.IsNullOrEmpty(custUsername)) 
            {
                TravelExpertsASPContext db = new TravelExpertsASPContext();
                var cust = db.Customers.FirstOrDefault(
                                        c => c.CustUsername.ToLower() == custUsername.ToLower()); //check if customer exist
                if(cust != null)
                {
                    msg = $"Username {custUsername} is already taken. Please try again.";// return error to the user.
                }
            }
            return msg;
        }
    }
}

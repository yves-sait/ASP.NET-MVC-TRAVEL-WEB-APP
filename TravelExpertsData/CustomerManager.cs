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
    /// <summary>
    /// Blueprint for interacting with customer entity
    /// For customer registration, login and update data.
    /// </summary>
    public static class CustomerManager
    {

        /// <summary>
        /// Add customer data to customer table from registration page
        /// </summary>
        /// <param name="cust"> customer object</param>
        /// <returns>true if added, otherwise false</returns>
        public static bool AddCustomer(Customer cust)
        {
            bool isAddSuccess = true;

            try
            {
                TravelExpertsASPContext db = new TravelExpertsASPContext();
                db.Customers.Add(cust);
                db.SaveChanges();

            }
            catch
            {

                return false;
            }


            return isAddSuccess;

        }

        /// <summary>
        /// User is authenticated based on credentials and a user returned if exists or null if not.
        /// </summary>
        /// <param name="username">Username as string</param>
        /// <param name="password">Password as string</param>
        /// <returns>A user object or null.</returns>
        /// <remarks>
        /// </remarks>
        public static Customer Authenticate(string username, string password)
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();

            // get the customer object using the entered username and password
            var cust = db.Customers.SingleOrDefault(cust => cust.CustUsername.ToLower() == username.ToLower() &&
                                                            cust.CustPassword == password);
            return cust; // will return null or object of the customer
        }


        /// <summary>
        /// Get Customer Data using username
        /// </summary>
        /// <param name="name"></param>
        /// <returns>return the current customer object</returns>

        public static Customer GetCustomerByUser(string username)
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();
            Customer cust = db.Customers.SingleOrDefault(c => c.CustUsername == username);

            return cust;
        }

        /// <summary>
        /// Update customer data
        /// </summary>
        /// <param name="CustId">id of the movie to update</param>
        /// <param name="newCust">new customer data (from the form)</param>
        public static Customer UpdateCustomer(int CustId, Customer newCust)
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();
            Customer oldCust = db.Customers.Find(CustId);
            oldCust.CustFirstName = newCust.CustFirstName;
            oldCust.CustLastName = newCust.CustLastName;
            oldCust.CustAddress = newCust.CustAddress;
            oldCust.CustCity = newCust.CustCity;
            oldCust.CustProv = newCust.CustProv;
            oldCust.CustPostal = newCust.CustPostal;
            oldCust.CustCountry = newCust.CustCountry;
            oldCust.CustHomePhone = newCust.CustHomePhone;
            oldCust.CustBusPhone = newCust.CustBusPhone;
            oldCust.CustEmail = newCust.CustEmail;
            oldCust.CustPassword = newCust.CustPassword;
            oldCust.CustUsername = newCust.CustUsername;

            db.SaveChanges();

            return db.Customers.Find(CustId);

        }


    }
}

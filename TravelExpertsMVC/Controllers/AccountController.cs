/*
 * Project Workshop 5
 * Author: Sung Jai Kim & Hugo Schrupp Suarez
 * Date: February 2022
 */

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelExpertsData;
using TravelExpertsMVC.Models;

namespace TravelExpertsMVC.Controllers
{
    public class AccountController : Controller
    {

        /// <summary>
        /// Blueprint for customer account creation, registration and profile update
        /// </summary>
        /// <param name="ReturnUrl"></param>
        /// <returns></returns>
        /// 
        // GET: AccountController
        public IActionResult Login( string ReturnUrl ="")
        {
            if (ReturnUrl != null)
                TempData["ReturnUrl"] = ReturnUrl;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginAsync(Customer customer)
        {
            try
            {
                Customer cust = CustomerManager.Authenticate(customer.CustUsername, customer.CustPassword);

                if(cust == null) // authentication failed. No customer in the Db with the provided username and password.
                {
                    ViewBag.LoginFailed = "Authentication Failed. Please enter the correct username and password.";
                    return View(); //return to the login page view with auth failed error.
                }

                //Authentication pass on this point. Customer object found in db.\
                List<Claim> claims = new List<Claim> // instatiate claim object{
                {
                    new Claim(ClaimTypes.Name,  cust.CustUsername),
                    new Claim("FullName", cust.CustFirstName +" "+ cust.CustLastName)
                };

                ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies"); // user cookies as authentication
                ClaimsPrincipal claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync("Cookies", claimsPrincipal); // sign in using cookies as auth type and claim

                ViewBag.FullName = ((ClaimsIdentity)User.Identity).FindFirst("FullName");

                if (String.IsNullOrEmpty(TempData["ReturnUrl"].ToString())) // if no return URL
                    return RedirectToAction("Package", "Package"); // redirect to package page
                else
                    return Redirect(TempData["ReturnUrl"].ToString()); // redirect to return URL if it exist.
                
            }
            catch(Exception ex)
            {
                TempData["Message"] = "Database connection problem. Try again later." + ex.Message;
                TempData["IsError"] = true;
            }
            return RedirectToAction("Login");
        }


        //Logout and redirect to Home page
        public async Task<IActionResult> LogOutAsync()
        {
            await HttpContext.SignOutAsync("Cookies");
            return RedirectToAction("Index", "Home");
        }

        public IActionResult AccessDenied()
        {
            return View();
        }



        //Registration

        // GET: RegisterController
        public IActionResult Register()
        {
            return View();
        }



        [HttpPost]
        public IActionResult Register(Customer customer)
        {
            try
            {
                //validate unique username
                if (TempData["validUsername"] == null)
                {
                    // Validate the entered username if exist in the db.
                    string msg = FormFieldValidation.UsernameExist(customer.CustUsername);
                    if (!String.IsNullOrEmpty(msg))
                    {
                        ModelState.AddModelError(nameof(Customer.CustUsername), msg);//return error if username already exist.
                    }
                }

                //validate if no error on the form
                if (ModelState.IsValid)
                {
                    if (CustomerManager.AddCustomer(customer))
                    {
                        TempData["Message"] = "You have successfully registered. You may now login.";
                        TempData["IsError"] = false;
                        return RedirectToAction("Register", "Account");
                        //return View();

                    }
                }
                return View(customer);

            }
            catch (Exception)
            {
                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
                return RedirectToAction("Register");
            }
        }


        //customer data update

        //get
        public IActionResult Update()
        {
            try
            {
                string user = HttpContext.User.Identity.Name;//get the current login username
                Customer cust = CustomerManager.GetCustomerByUser(user); //query customer object if exist
                return View(cust); //return customer obj to view
            }
            catch (Exception)
            {

                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
                //return RedirectToAction("Index");
                return View();
            }
        }


        //post update
        [HttpPost]
        [ValidateAntiForgeryToken]
   
        public IActionResult Update(int CustomerId, Customer customer)
        {
            try
            {
                var newcust = CustomerManager.UpdateCustomer(CustomerId, customer);
                TempData["Message"] = "Your profile has been successfully updated";
                TempData["IsError"] = false;
                return View(newcust);

            }
            catch
            {
                TempData["Message"] = "Database connection problem. Try again later.";
                TempData["IsError"] = true;
                return View();
            }
        }




    }
}

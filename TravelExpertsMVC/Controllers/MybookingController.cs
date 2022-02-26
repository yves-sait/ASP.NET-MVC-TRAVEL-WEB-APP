
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TravelExpertsData;

namespace TravelExpertsMVC.Controllers
{
    [Authorize]
    public class MyBookingController : Controller
    {
        /// <summary>
        /// Blueprint for presenting customer booking history and booking details
        /// </summary>
        /// <returns>booking and booking details data</returns>

        // GET: MyBooking
        public ActionResult Mybooking()
        {
            List<Booking> myBookings; // declare the list of bookings

            try
            {
                string user = HttpContext.User.Identity.Name;//get the current login username

                Customer cust = CustomerManager.GetCustomerByUser(user); // get the customer object

                myBookings = MyBookingManager.GetMyBooking(cust.CustomerId); //get the booking transaction of customer using customer id

                decimal totalPrice = 0m; //declare the totalprice of the booking

                //iterate to all the bookings
                for( int i = 0; i < myBookings.Count(); i++) 
                {
                    totalPrice += myBookings[i].Package.PkgBasePrice; //summation of all the booking price
                }

                ViewBag.TotalPrice = totalPrice.ToString("C0"); // assign the totalprice to viewbag and format to currency

                return View(myBookings); // return the booking objects to view
            }
            catch (Exception ex) //handle the error
            {
                TempData["Message"] = "Database connection problem. Try again later." + ex.Message;
                TempData["IsError"] = true;
            }

            return View();
        }

        // GET: MyBooking/Details/5
        public ActionResult Details(int id)
        {
            List<BookingDetail> bookingDetails;
            try
            {

                bookingDetails = MyBookingManager.GetMyBookingDetails(id); //get the bookings details using the booking id

                //ViewBag.BookingIndex = myBookings;

                return View(bookingDetails); //pass the booking details object to view.
            }
            catch (Exception ex) //handle exception
            {
                TempData["Message"] = "Database connection problem. Try again later." + ex.Message;
                TempData["IsError"] = true;
            }

            return View();

        }

       
    }
}

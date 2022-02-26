

using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelExpertsData
{
    public class MyBookingManager
    {
        /// <summary>
        /// Blueprint for interacting with booking and booking details entities
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Booking and booking details object</returns>

        public static List<Booking> GetMyBooking(int id)
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();

            var myBookings = db.Bookings.Where(b => b.CustomerId == id).Include(b => b.Package).ToList(); // return booking object and filtered by customerid

            return myBookings;
        }

        public static List<BookingDetail> GetMyBookingDetails(int id)
        {
            TravelExpertsASPContext db = new TravelExpertsASPContext();
            var myBookingDetails = db.BookingDetails.Where(bd => bd.BookingId == id)
                                                    .Include(bd => bd.Booking) // join booking entity to get package desc
                                                    .Include(bd => bd.ProductSupplier).ThenInclude(bd => bd.Product) // join product entity to get product name
                                                    .Include(bd => bd.ProductSupplier).ThenInclude(bd => bd.Supplier) // join supplier entity to get supplier
                                                    .OrderBy(p => p.ProductSupplier.Product.ProdName).ToList(); // order by prodname

            return myBookingDetails; // return bookingdetails objects
        }
    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Index(nameof(AgentId), Name = "EmployeesCustomers")]
    public partial class Customer
    {
        public Customer()
        {
            Bookings = new HashSet<Booking>();
            CreditCards = new HashSet<CreditCard>();
            CustomersRewards = new HashSet<CustomersReward>();
        }

        [Key]
        public int CustomerId { get; set; }

        [Required(ErrorMessage = "Please enter your first name.")]
        [StringLength(25, ErrorMessage = "First Name must be 25 characters or less.")]
        public string CustFirstName { get; set; }

        [Required(ErrorMessage = "Please enter your last name.")]
        [StringLength(25, ErrorMessage = "Last Name must be 25 characters or less.")]
        public string CustLastName { get; set; }


        [Required(ErrorMessage = "Please enter your Address.")]
        [StringLength(75)]
        public string CustAddress { get; set; }

        [Required(ErrorMessage = "Please enter your City.")]
        [StringLength(50)]
        public string CustCity { get; set; }

        [Required(ErrorMessage = "Please enter your Province code.")]
        [StringLength(2)]
        public string CustProv { get; set; }


        [Required(ErrorMessage = "Please enter your Postal code.")]
        [StringLength(6, ErrorMessage = "Postal Code must be 6 characters.")]
        [RegularExpression(@"^(?:[A-Za-z]\d[A-Za-z]\d[A-Za-z]\d)$", ErrorMessage = " Please enter a valid postal format. Ex.(T2T0R9)")]
        public string CustPostal { get; set; }
        [Required(ErrorMessage = "Please enter your country.")]
        [StringLength(20, ErrorMessage = "Country must be 20 characters or less.")]
        public string CustCountry { get; set; }

        [Required(ErrorMessage = "Please provide your home phone number.")]
        [StringLength(15)]
        [RegularExpression(@"^\d{3}\-\d{3}-\d{4}$", ErrorMessage = " Please enter a valid phone format. Ex.(123-456-7890)")]
        public string CustHomePhone { get; set; }
        [StringLength(20)]
        public string CustBusPhone { get; set; }
        [StringLength(50)]
        public string CustEmail { get; set; }
        public int? AgentId { get; set; }

        [Required(ErrorMessage = "Please enter your password.")]
        [StringLength(30, ErrorMessage = "Password must be 30 characters or less.")]
        [Display(Name = "Password")]
        public string CustPassword { get; set; }

        [Required(ErrorMessage = "Please confirm your password.")]
        [Display(Name ="Confirm Password")]
        [Compare("CustPassword")]
        [NotMapped]

        public string CustConfirmPassword { get; set; }

        [Required(ErrorMessage = "Please enter your username.")]
        [StringLength(30, ErrorMessage = "Username must be 30 characters or less.")]
        [Display(Name = "Username")]
        public string CustUsername { get; set; }

        [ForeignKey(nameof(AgentId))]
        [InverseProperty("Customers")]
        public virtual Agent Agent { get; set; }
        [InverseProperty(nameof(Booking.Customer))]
        public virtual ICollection<Booking> Bookings { get; set; }
        [InverseProperty(nameof(CreditCard.Customer))]
        public virtual ICollection<CreditCard> CreditCards { get; set; }
        [InverseProperty(nameof(CustomersReward.Customer))]
        public virtual ICollection<CustomersReward> CustomersRewards { get; set; }
    }
}

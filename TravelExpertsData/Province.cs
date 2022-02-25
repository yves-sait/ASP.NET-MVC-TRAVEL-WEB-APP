using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace TravelExpertsData
{
    [Keyless]
    public partial class Province
    {
        [Column("provname")]
        [StringLength(100)]
        public string Provname { get; set; }
        [Column("code")]
        [StringLength(2)]
        public string Code { get; set; }
    }
}

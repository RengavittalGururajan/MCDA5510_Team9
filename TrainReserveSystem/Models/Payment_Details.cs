using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TrainReserveSystem.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;
    public class Payment_Details
    {
        [Required]
        [Display(Name = "Credit Card")]
        public string cardtype { get; set; }

        [Required]
        [Display(Name = "Name on Credit Card")]
        public string name { get; set; }

        [Required]
        [Display(Name = "Card Number")]
        public long creditcardnumber { get; set; }

        [Required]
        [Display(Name = "Expiry Month-MM")]
        public int expirymonth { get; set; }

        [Required]
        [Display(Name = "Expiry Year YYYY")]
        public int expiry_year { get; set; }

       
        public enum CardType
        {
            visa,
            mastercard,
            americanexpress
        }

    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        [Required]
        [DisplayFormat(DataFormatString = "{0:dddd, MMMM dd, yyyy}")]
        public DateTime DateTime { get; set; }

        [Required]
        public int GuestCount { get; set; }

        [Required]
        public DateTime DateCreated { get; set; }

        public DateTime DateCompleted { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        public ApplicationUser AppicationUser { get; set; }

        [Required]
        public int ChefMenuId { get; set; }
        [Required]
        public ChefMenu ChefMenu { get; set; }
    }
}

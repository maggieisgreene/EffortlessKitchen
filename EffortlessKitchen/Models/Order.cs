using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        public DateTime DateTime { get; set; }

        [Required]
        public int GuestCount { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime DateCompleted { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser AppicationUser { get; set; }

        public string ChefMenuId { get; set; }
        public ChefMenu ChefMenu { get; set; }
    }
}

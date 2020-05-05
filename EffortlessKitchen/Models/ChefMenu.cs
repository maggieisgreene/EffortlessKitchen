using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class ChefMenu
    {
        [Key]
        public int ChefMenuId { get; set; }

        [Required]
        public int ChefId { get; set; }

        [Required]
        public Chef Chef { get; set; }

        [Required]
        public int MenuOptionId { get; set; }

        [Required]
        public MenuOption MenuOption { get; set; }

        public virtual List<Order> Orders { get; set; }
    }
}

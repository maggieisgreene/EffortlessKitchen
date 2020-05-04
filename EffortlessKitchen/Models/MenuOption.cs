using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class MenuOption
    {
        [Required]
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public string Ingredients { get; set; }
        public double Price { get; set; }

        public virtual List<ChefMenu> ChefMenus { get; set; }
    }
}

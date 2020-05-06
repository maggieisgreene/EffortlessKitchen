using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class MenuOption
    {
        [Key]
        public int MenuOptionId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Ingredients { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual List<ChefMenu> ChefMenus { get; set; }
    }
}

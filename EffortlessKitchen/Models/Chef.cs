using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class Chef
    {
        [Key]
        public int ChefId { get; set; }

        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Specialties { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public double Price { get; set; }

        public virtual List<ChefMenu> ChefMenus { get; set; }
    }
}

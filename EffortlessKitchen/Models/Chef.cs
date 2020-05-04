using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models
{
    public class Chef
    {
        [Required]
        public int Id { get; set; }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Description { get; set; }
        public string Specialties { get; set; }
        public double Price { get; set; }

        public virtual List<ChefMenu> ChefMenus { get; set; }
    }
}

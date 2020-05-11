using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models.ViewModels
{
    public class ChefFormViewModel
    {
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

        public List<SelectListItem> MenuOptions { get; set; }
        public List<int> SelectedMenuOptionIds { get; set; }
    }
}

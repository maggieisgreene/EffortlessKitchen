using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace EffortlessKitchen.Models.ViewModels
{
    public class OrderFormViewModel
    {
        public int OrderId { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        [Display(Name = "Choose Your Date")]
        public DateTime DateTime { get; set; }

        [Required]
        [Display(Name = "Guest Count")]
        public int GuestCount { get; set; }

        [Required]
        public string ApplicationUserId { get; set; }

        [Required]
        [Display(Name = "Select Your Menu")]
        public int ChefMenuId { get; set; }
        public List<SelectListItem> ChefMenus { get; set; }
    }
}

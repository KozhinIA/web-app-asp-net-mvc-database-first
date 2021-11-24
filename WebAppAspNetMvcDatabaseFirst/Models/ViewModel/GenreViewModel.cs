using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace WebAppAspNetMvcDatabaseFirst.Models
{
    public class GenreViewModel
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>    
        [Required]
        [Display(Name = "Название", Order = 5)]
        public string Name { get; set; }
    }
}
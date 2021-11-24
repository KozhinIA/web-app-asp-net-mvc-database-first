using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Web.Mvc;
using WebAppAspNetMvcDatabaseFirst.Attributes;
using WebAppAspNetMvcDatabaseFirst.Extensions;

namespace WebAppAspNetMvcDatabaseFirst.Models
{
    public class AuthorViewModel
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Имя
        /// </summary>    
        [Required]
        [Display(Name = "Имя", Order = 5)]
        public string FirestName { get; set; }

        /// <summary>
        /// Фамилия
        /// </summary>    
        [Required]
        [Display(Name = "Фамилия", Order = 10)]
        public string LastName { get; set; }

        /// <summary>
        /// День рождения
        /// </summary> 
        [Display(Name = "День рождения", Order = 20)]
        public DateTime? Birthday { get; set; }

        /// <summary>
        /// Пол
        /// </summary> 
        [ScaffoldColumn(false)]
        public Gender Gender { get; set; }

        [Display(Name = "Пол", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("Gender")]
        public IEnumerable<SelectListItem> GenderDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Gender type in Enum.GetValues(typeof(Gender)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Gender
                    });
                }

                return dictionary;
            }
        }

    }
}
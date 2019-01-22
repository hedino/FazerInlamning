using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Fazzer.ViewModels
{
    public class CategoryEditViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Wrong amount of letters 2-50")]
        public string Name { get; set; }
    }

    public class CategoryCreateViewModel
    {
        [Required]
        [StringLength(50, MinimumLength = 2, ErrorMessage = "Wrong amount of letters 2-50")]
        public string Name { get; set; }

    }

}
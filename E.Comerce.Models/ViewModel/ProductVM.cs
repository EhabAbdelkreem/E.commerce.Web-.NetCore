﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace E.Comerce.Models.ViewModel
{
    public class ProductVM
    {
        public Product Product { get; set; } = new Product();
        [ValidateNever]
        public IEnumerable<Product> products { get; set; } = new List<Product>();
        [ValidateNever]
        public IEnumerable<SelectListItem> categories { get; set; } 
    }
}

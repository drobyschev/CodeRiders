
//-----------------------------------------------------------------------
// <copyright file="Product.cs" company="Code Riders Ltd 2016">
//     Copyright (c) Code Riders. All rights reserved.
// </copyright>
// <author>Andrey Drobyshev</author>
//-----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebStore.Domain.EF.Entities
{
    public class Product
    {
        public int ProductId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }
        public decimal Price { get; set; }
    }
}

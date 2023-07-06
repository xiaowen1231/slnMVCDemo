﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace prjMVCDemo.Models
{
    public class CShoppingCartItem
    {
        public int productId { get; set; }
        public int count { get; set; }
        public decimal price { get; set; }
        public decimal 小計
        {
            get
            {
                return this.price * this.count;
            }
        }
        public tProduct product { get; set; }
    }
}
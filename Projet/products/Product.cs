using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    public class Product
    {
        public long Id { get; set; }
        public string Designation { get; set; }
        public double Price { get; set; }
        public string Photo { get; set; }

        public long CategoryId { get; set; }
        public Category Category { get; set; }

        public int Quantity { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Projet
{
    public class Category
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string Photo { get; set; }

        public List<Product> Products { get; set; } = new List<Product>();
    }

}

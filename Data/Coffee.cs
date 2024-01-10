using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class Coffee
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CoffeeName { get; set; }
        public float Price { get; set; }

        public int Quantity { get; set; } = 0;
    }
}

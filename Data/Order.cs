using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class Order
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public  List<Coffee>? coffee { get; set; }

        public List<AddIns>? addIns { get; set; }
        public float Price { get; set; }

       

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public string Description { get; set; }
    }
}

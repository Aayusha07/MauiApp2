using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class AddIns
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string AddInsName { get; set; }
        public float Price { get; set; }
    }
}

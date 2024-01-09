using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiApp2.Data
{
    public class Member
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }

        public string Name { get; set; }

        public string PhoneNumber { get; set; }


    }
}

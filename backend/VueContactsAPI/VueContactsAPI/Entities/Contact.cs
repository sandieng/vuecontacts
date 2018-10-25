using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueContactsAPI.Entities
{
    public class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Company { get; set; }
        public string JobTitle { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Notes { get; set; }
    }
}

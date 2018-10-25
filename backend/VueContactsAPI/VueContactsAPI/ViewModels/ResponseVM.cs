using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VueContactsAPI.ViewModels
{
    public class ResponseVM
    {
        public string Payload { get; set; }
        public ErrorVM Error { get; set; }
    }
}

using System.Collections.Generic;

namespace VueContactsAPI.ViewModels
{
    public class ResponseVM<T> where T: class
    {
        public List<T> Payload { get; set; }
        public ErrorVM Error { get; set; }
    }
}

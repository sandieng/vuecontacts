using System.Collections.Generic;

namespace VueContactsAPI.ViewModels
{
    public class ResponseSingleVM<T> where T: class
    {
        public T Payload { get; set; }
        public ErrorVM Error { get; set; }
    }
}

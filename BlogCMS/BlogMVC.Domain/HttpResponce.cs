using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace BlogMVC.Domain
{
    [DataContract]
    public class HttpResponse<T> where T: class
    {
        [DataMember]
        public bool Success { get; set; }
        [DataMember]
        public string ErrorMessage { get; set; }
        [DataMember]
        public T Data { get; set; }
    }
}
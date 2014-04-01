using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;


namespace BlogMVC.Domain
{
    [DataContract]
    public class Post
    {
        [DataMember]
        public int PostId { get; set; }
        [DataMember]
        [MaxLength(40)]
        public string Title { get; set; }
        [DataMember]
        public string Content { get; set; }
        [DataMember]
        public DateTime CreateDate { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
        [DataMember]
        [Required]
        public string UserId { get; set; }
    }
}
namespace Blog.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Message
    {
       
        public int Id { get; set; }

       
        [StringLength(50)]
        public string Title { get; set; }

        [Required]
        public string MessageBody { get; set; }

    
        public DateTime MessageDate { get; set; }

        public int? UserId { get; set; }

       
        public bool IsDeleted { get; set; }

        public bool IsRead { get; set; }

        [Required]
        [StringLength(50)]
        public string MessageFrom { get; set; }

        [Required]
        public string Email { get; set; }

        public virtual User User { get; set; }
    }
}

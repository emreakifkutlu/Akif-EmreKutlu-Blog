namespace Blog.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class MediaUpload
    {
       
        public int Id { get; set; }

        [StringLength(50)]
        public string Name { get; set; }

        public string Path { get; set; }
    }
}

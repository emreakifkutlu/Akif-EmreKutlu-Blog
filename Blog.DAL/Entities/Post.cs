namespace Blog.DAL.Entities
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Post
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Post()
        {
            Comments = new HashSet<Comment>();
        }

        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string PostBody { get; set; }

        public DateTime PostDate { get; set; }

        public string Description { get; set; }

        public int? UserId { get; set; }

        public int CategoryId { get; set; }

        [Required]
        [StringLength(200)]
        public string Summary { get; set; }

        public bool IsDeleted { get; set; }

        public int? LikeCount { get; set; }

        public int? DislikeCount { get; set; }

        public string PostPic { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Comment> Comments { get; set; }
    }
}

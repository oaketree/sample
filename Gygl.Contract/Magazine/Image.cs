using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.Magazine
{
    [Table("Tbl_GyglImage")]
    public class Image
    {
        [Key]
        public int ID { get; set; }

        public int ArticleID { get; set; }

        [StringLength(50)]
        public string ImageID { get; set; }

        public int? SortID { get; set; }

        [StringLength(50)]
        public string Guid { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        //[ForeignKey("ArticleID")]
        //public virtual Article Article { get; set; }

    }
}

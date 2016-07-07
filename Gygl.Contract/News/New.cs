using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.News
{
    [Table("Tbl_News")]
    public partial class New
    {
        [Key]
        public int newsid { get; set; }

        public int ColumnID { get; set; }

        [StringLength(100)]
        public string Title { get; set; }

        [Column(TypeName = "text")]
        public string Content { get; set; }

        public int? Hits { get; set; }

        public DateTime? RegDate { get; set; }

        [StringLength(200)]
        public string InfoFrom { get; set; }
    }
}

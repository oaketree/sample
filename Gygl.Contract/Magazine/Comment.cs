using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Gygl.Contract.Magazine
{
    [Table("Tbl_GyglComment")]
    public class Comment
    {
        [Key]
        public int ID { get; set; }

        [StringLength(50)]
        public string IP { get; set; }

        public int? ArticleID { get; set; }

        [StringLength(100)]
        public string Advice { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

    }
}

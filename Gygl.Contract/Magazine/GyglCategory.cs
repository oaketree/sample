using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.Magazine
{
    [Table("Tbl_GyglCategoryRelation")]
    public class GyglCategory
    {
        [Key]
        public int ID { get; set; }

        public int? GyglID { get; set; }

        public int? CategoryID { get; set; }

        [ForeignKey("GyglID")]
        public virtual Gygl Gygl { get; set; }

        [ForeignKey("CategoryID")]
        public virtual Category Category { get; set; }
    }
}

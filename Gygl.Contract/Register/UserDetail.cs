using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.Register
{
    [Table("Tbl_UserDetail")]
    public class UserDetail
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ID { get; set; }

        [Key]
        public int UserID { get; set; }

        [StringLength(100)]
        public string Tel { get; set; }

        [StringLength(150)]
        public string Address { get; set; }


        [StringLength(50)]
        public string QQ { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string DisplayName { get; set; }


        [StringLength(100)]
        public string CompanyName { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public DateTime? RegDate { get; set; }

        public virtual Users Users { get; set; }
    }
}

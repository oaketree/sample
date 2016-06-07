using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Gygl.Contract.Register
{
    [Table("Tbl_UserAuthorise")]
    public class Authorise
    {
        [Key]
        public int AuthoriseID { get; set; }

        /// <summary>
        /// 名称
        /// </summary>
        [StringLength(20, MinimumLength = 2, ErrorMessage = "{1}到{0}个字")]
        [Display(Name = "名称")]
        public string Name { get; set; }


        /// <summary>
        /// 说明
        /// </summary>
        [StringLength(50, ErrorMessage = "应少于{0}个字")]
        [Display(Name = "说明")]
        public string Description { get; set; }
    }
}

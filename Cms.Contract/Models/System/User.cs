using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cms.Contract.Models.System
{
    [Table("SYS_USER")]
    public class User
    {
        [Key]
        public int ID { get; set; }



    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CreateNotbookSystem.Service.Context
{
    public class BaseEntity
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]  //设置自增
        [Column("id")]
        public int Id { get; set; }

        [Column("createdDate", TypeName = "datetime2")]
        public DateTime CreatedDate { get; set; }

        [Column("updatedDate", TypeName = "datetime2")]
        public DateTime UpdatedDate { get; set; }
    }
}

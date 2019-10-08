using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTestMVC
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        [Column(TypeName = "datetime")]
        public DateTime DateBirth { get;set; } 
        public bool Gender { get; set; } 
        public int ParentId { get; set; }

    }
}

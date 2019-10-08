using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopTestMVC
{
    public class Relatives
    {
        [Key]
        public int ChildId { get; set; }
        public int ParentId { get; set;}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ShopTestMVC.Model
{
    public class Relatives
    {
        [Key]
        public int ChildId { get; set; }
        public int ParentId { get; set; }
    }
}

using System.ComponentModel.DataAnnotations;

namespace ShopTestMVC.Model {
    public class Relatives {
        [Key]
        public int ChildId { get; set; }
        public int ParentId { get; set; }
    }
}

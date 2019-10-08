using System;
using System.ComponentModel.DataAnnotations;
namespace ShopTestMVC.Model
{
    public class Users
    {
        [Key]
        public int Id { get; set; }
        [StringLength(100)]
        public string Name { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        public string  Gender { get; set; }
        public int ParentId { get; set; }
    }
}

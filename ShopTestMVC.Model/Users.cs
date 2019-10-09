using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ShopTestMVC.Model {
    public class Users {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [StringLength(100)]
        [Required]
        public string Name { get; set; }
        [DataType(DataType.Date)]
        public DateTime DateBirth { get; set; }
        public bool Gender { get; set; }
        public int ParentId { get; set; }
    }
}

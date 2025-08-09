using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AOWebApp.Models.CodeFirst
{
    public class ExampleItem
    {
        [Key]
        public int itemID { get; set; }

        [Required]
        [Display(Name = "Item Name")]
        [StringLength(100, ErrorMessage = "The item name musst be less than 100 characters")]
        public string itemName { get; set; } = string.Empty;

        [Required]
        [Range(1, 1000)]
        [Display(Name ="Price")]
        [DataType(DataType.Currency)]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal itemPrice {  get; set; }
    }
}

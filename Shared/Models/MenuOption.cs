using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSMenuOption")]
    public class MenuOption : IAuditable
    {
        [Key]
        public int OptionId { get; set; }

        [Required]
        [MaxLength(255)]
        public string OptionName { get; set; }

        public decimal? OptionPrice { get; set; }

        public string OptionImage { get; set; }

        public int? MOCalories { get; set; }

        public decimal? MOProtein { get; set; }

        public decimal? MOFat { get; set; }

        public decimal? MOCarbohydrates { get; set; }

        public decimal? MOSodium { get; set; }

        [Required]
        public int ItemId { get; set; }

        [Required]
        public bool IsActive { get; set; }

        // Auditable fields (handled by IAuditable)
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("ItemId")]
        public virtual MenuItem MenuItem { get; set; }
    }
}
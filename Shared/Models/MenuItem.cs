using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSMenuItem")]
    public class MenuItem : IAuditable
    {
        [Key]
        public int ItemId { get; set; }
        public int ModuleId { get; set; }
        public int RestaurantMenuId { get; set; }
        public int CategoryId { get; set; }
        public string ItemName { get; set; }
        public string ItemDescription { get; set; }
        public string ItemImageURL { get; set; }
        public decimal ItemPrice { get; set; }
        public string PriceNote { get; set; }
        public decimal Calories { get; set; }
        public decimal Protein { get; set; }
        public decimal Fat { get; set; }
        public decimal Carbohydrates { get; set; }
        public decimal Sodium { get; set; }
        public int SortOrder { get; set; }
        public bool IsNewItem { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual RestaurantMenu RestaurantMenu { get; set; }
        public virtual MenuCategory MenuCategory { get; set; }

        [InverseProperty("MenuItem")]
        public virtual ICollection<MenuItemAttribute> MenuItemAttributes { get; set; }

        [InverseProperty("MenuItem")]
        public virtual ICollection<MenuOption> MenuOptions { get; set; }

    }
}
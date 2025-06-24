using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSMenuCategory")]
    public class MenuCategory: IAuditable
    {
        [Key]
        public int CategoryId { get; set; }
        public int ModuleId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryDescription { get; set; }
        public string CategoryIcon { get; set; }
        public int SortOrder { get; set; } 
        public bool IsActive { get; set; } 
        public int RestaurantMenuId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual RestaurantMenu RestaurantMenu { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }

    }
}


//GIBSMenuCategoryEntityBuilder.cs
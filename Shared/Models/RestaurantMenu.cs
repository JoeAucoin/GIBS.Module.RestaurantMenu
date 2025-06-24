using Oqtane.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSRestaurantMenu")]
    public class RestaurantMenu : IAuditable
    {
        [Key]
        public int RestaurantMenuId { get; set; }
        public int ModuleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageURL { get; set; }
        public string HoursServed { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        public virtual ICollection<MenuCategory> MenuCategories { get; set; }
        public virtual ICollection<MenuItem> MenuItems { get; set; }
    }
}

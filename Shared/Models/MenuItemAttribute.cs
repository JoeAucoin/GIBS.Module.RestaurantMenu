using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSMenuItemAttribute")]
    public class MenuItemAttribute : IAuditable
    {
        [Key]
        public int MenuItemAttributeId { get; set; }
        public int ItemId { get; set; }
        public int AttributeId { get; set; }

        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

        [ForeignKey("ItemId")]
        public virtual MenuItem MenuItem { get; set; }
        [ForeignKey("AttributeId")]
        public virtual MenuAttribute MenuAttribute { get; set; }
    }
}
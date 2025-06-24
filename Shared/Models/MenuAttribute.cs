using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Oqtane.Models;

namespace GIBS.Module.RestaurantMenu.Models
{
    [Table("GIBSMenuAttribute")]
    public class MenuAttribute : IAuditable
    {
        [Key]
        public int AttributeId { get; set; }
        public int ModuleId { get; set; }
        public string AttributeName { get; set; }
        public string AttributeDescription { get; set; }
        public string AttributeIcon { get; set; }
        public string AttributeCode { get; set; }
        public string AttributeColor { get; set; }
        public int SortOrder { get; set; }
        public bool IsActive { get; set; }
      //  public int RestaurantMenuId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public string ModifiedBy { get; set; }
        public DateTime ModifiedOn { get; set; }

    }
}

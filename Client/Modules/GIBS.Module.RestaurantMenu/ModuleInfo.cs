using Oqtane.Models;
using Oqtane.Modules;
using System.Collections.Generic;
using System.Security.AccessControl;

namespace GIBS.Module.RestaurantMenu
{
    public class ModuleInfo : IModule
    {
        public ModuleDefinition ModuleDefinition => new ModuleDefinition
        {
            Name = "RestaurantMenu",
            Description = "Restaurant Menu",
            Version = "1.0.11",
            ServerManagerType = "GIBS.Module.RestaurantMenu.Manager.RestaurantMenuManager, GIBS.Module.RestaurantMenu.Server.Oqtane",
            ReleaseVersions = "1.0.0,1.0.4,1.0.8,1.0.9,1.0.10,1.0.11",
            Dependencies = "GIBS.Module.RestaurantMenu.Shared.Oqtane",
            PackageName = "GIBS.Module.RestaurantMenu" ,
            Owner = "Global Internet Business Solutions LLC",
            Url = "www.gibs.com",
            Contact = "Joseph Aucoin",
            License = "MIT"};
    }
}

# RestaurantMenu Oqtane Module

## Overview
The RestaurantMenu module provides a flexible way to display menus, categories, and menu items within an Oqtane-based site. It integrates seamlessly with Blazor and leverages standard Oqtane API endpoints for easy administration and customization.

## Key Features
1. **Menus**  
   • Define separate menus, each with its own name, description, images, and availability times.  
   • Organize multiple menus for different locations or occasions.

2. **Categories**  
   • Group related menu items under specific categories.  
   • Provide descriptions and sort categories for neat display.

3. **Items**  
   • List each individual menu item with name, description, price, and images.  
   • Mark items as new or featured, and sort them by priority.  
   • Link items to optional add-ons or upsells.

4. **Attributes**  
   • Store extra attributes as needed (for example, gluten-free, spicy, or other labels).  
   • Use them to categorize items or add item-specific notes.

5. **Nutrition Information**  
   • Display calories, protein, carbohydrates, fat, and sodium data.  
   • Apply dynamic formatting for a clean user experience.

6. **Customization Options**  
   • Modify menu designs with user-defined images, colors, and item arrangement.  
   • Include item options and additional pricing to create custom variations.

7. **Administration**  
   • Manage menus, categories, and items in an integrated dashboard.  
   • Control module settings such as the default menu, active attributes, and other parameters.  
   • Restrict viewing or editing through Oqtane’s built-in permission system.

## Architecture
• Built using Blazor and integrates directly with Oqtane’s modular framework.  
• Separates data models (menus, items, categories) in distinct services and repositories following Oqtane patterns.  
• Incorporates multi-layer data access (server manager, repository, migrations) to handle module installation and updates.

## Implementation
• Utilizes standard ASP.NET Core and Blazor patterns.  
• Implements portable module interfaces (IInstallable, IPortable, ISearchable) for seamless Oqtane compatibility.  
• Consumes Oqtane’s structured API endpoints for retrieving data and updating menu records.  
• Offers local storage of images and references for easy theming.

For more information, including setup and configuration details, refer to the official Oqtane documentation.

/* Module Script */
var GIBS = GIBS || {};

GIBS.RestaurantMenu = {
};

// wwwroot/Modules/GIBS.Module.RestaurantMenu/Module.js
window.interopFormValid = function (form) {
    if (form && form instanceof HTMLFormElement) {
        return form.checkValidity();
    }
    return false;
};
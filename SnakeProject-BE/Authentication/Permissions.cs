using System.Reflection;

namespace SnakeProject.API.Authentication
{
    public static class Permissions
    {
        public const string Type = "permissions";

        public const string ProductsRead = "products:read";
        public const string ProductsAdd = "products:add";
        public const string ProductsUpdate = "products:update";
        public const string ProductsDelete = "products:remove";

        public const string CategoriesRead = "categories:read";
        public const string CategoriesAdd = "categories:add";
        public const string CategoriesUpdate = "categories:update";
        public const string CategoriesDelete = "categories:remove";

        public const string PsnCodesRead = "psncodes:read";
        public const string PsnCodesAdd = "psncodes:add";
        public const string PsnCodesUpdate = "psncodes:update";
        public const string PsnCodesDelete = "psncodes:remove";
        public const string PsnCodesReserve = "psncodes:reserve";
        public const string PsnCodesRelease = "psncodes:release";
        public const string PsnCodesSell = "psncodes:sell";
        public const string PsnCodesInventorySummary = "psncodes:summary";

        public const string CartRead = "cart:read";
        public const string CartAddItem = "cart:add-item";
        public const string CartRemoveItem = "cart:remove-item";
        public const string CartClear = "cart:clear";
        public const string CartCheckout = "cart:checkout";

        public const string OrdersRead = "orders:read";
        public const string OrdersCreate = "orders:add";
        public const string OrdersUpdateStatus = "orders:update-status";
        public const string OrdersCancel = "orders:cancel";

        public const string UsersRead = "users:read";
        public const string UsersAdd = "users:add";
        public const string UsersUpdate = "users:update";

        public const string RolesRead = "roles:read";
        public const string RolesAdd = "roles:add";
        public const string RolesUpdate = "roles:update";

        public static IList<string> GetAllPermissions() =>
            typeof(Permissions)
                .GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy)
                .Where(field => field.IsLiteral && !field.IsInitOnly && field.FieldType == typeof(string) && field.Name != nameof(Type))
                .Select(field => (string)field.GetRawConstantValue()!)
                .ToList();
    }
}

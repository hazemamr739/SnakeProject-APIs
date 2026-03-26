namespace SnakeProject.Domain.Enums
{
    public enum InventoryStatus
    {
        Available = 1, // The item is available for purchase

        Reserved = 2, // The item is reserved for a customer ( added to cart but not yet purchased)

        Sold = 3     // The item has been sold and is no longer available
    }
}

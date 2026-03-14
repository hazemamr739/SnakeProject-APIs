namespace SnakeProject.Domain.Enums
{
    public enum ProductType : byte
    {
        Account = 1,
        Subscription = 2,
        PsnCode = 3
    }

    public enum AccessType : byte
    {
        FullAccount = 1,
        Primary = 2,
        Secondary = 3
    }
    public enum Currency
    {
        USD = 1,
        EUR = 2,
        GBP = 3
    }
    public enum PlusPlan : byte
    {
        essintial = 1,
        extra = 2,
        deluxe = 3
    }
    public enum SubscriptionDuration
    {
        OneMonth = 1,
        ThreeMonths = 3,
        TwelveMonths = 12
    }
    public enum ConsoleType
    {
        PS5 = 1,
        PS4 = 2
    }
    public enum InventoryStatus
    {
        Available = 1,
        Reserved = 2,
        Sold = 3
    }

    public enum OrderStatus
    {
        Pending = 1,
        Paid = 2,
        Processing = 3,
        Completed = 4,
        Cancelled = 5,
        Failed = 6
    }
    public enum CartStatus
    {
        Active = 1,
        Converted = 2,
        Abandoned = 3
    }

}

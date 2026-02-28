namespace SnakeProject_BE.Entities.Enums
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
    public enum Region
    {
        USA = 1,
        UAE = 2,
        UK = 3,
        KSA = 4,
        TRY = 5
    }

    public enum Denomination
    { 
        Ten = 5,
        USD20 = 20,
        USD50 = 50
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

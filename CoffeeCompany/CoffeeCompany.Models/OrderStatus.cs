namespace CoffeeCompany.Models
{
    public enum OrderStatus
    {
        // Not yet paid for
        Pending,
        // Ready to be shipped
        Processed,
        // Are in the process of being delivered
        Shipping,
        // Finished
        Shipped,
        // Returned from client
        Returned,
        // Not to be send
        Canceled
    }
}

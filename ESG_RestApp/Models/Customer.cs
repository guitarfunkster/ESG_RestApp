namespace ESG_RestApp.Models
{
    public class Customer
    {

        public static string Display(Customer customer)
        {
            return $"Customer: ref: {customer.CustomerRef} name : {customer.CustomerName} Address Line 1:{customer.AddressLine1} ";
        }

        public int Id { get; set; }

        public int CustomerRef { get; set; } 
        public string? CustomerName { get; set; } 
        public string? AddressLine1 { get; set; } 
        public string? AddressLine2 { get; set; } 
        public string? Town { get; set; } 
        public string? County { get; set; }
        public string? Country { get; set; } 
        public string? Postcode { get; set; } 



    }
}

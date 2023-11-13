namespace ESG_ConsoleApp
{
    public class Customer
    {
 
        public static string Display(Customer customer)
        {
            return $"Customer: ref: {customer.CustomerRef} name : {customer.CustomerName} Address Line 1:{customer.AddressLine1} ";
        }

        public Customer()
        {
            //Default
        }

        public Customer(int id, string customerRef, string customerName, string addressLine1, string addressLine2, string town, string county, string country, string postcode)
        {
            Id= id ;
            CustomerRef = customerRef;
            CustomerName = customerName;
            AddressLine1 = addressLine1 ;
            AddressLine2 = addressLine2 ;
            Town = town ;
            County = county ;
            Country = country ;
            Postcode = postcode ;
        }
        
        public int Id { get; set; }

        public string? CustomerRef { get; set; } 
        public string? CustomerName { get; set; }
        public string? AddressLine1 { get; set; } 
        public string? AddressLine2 { get; set; }
        public string? Town { get; set; } 
        public string? County { get; set; } 
        public string? Country { get; set; } 
        public string? Postcode { get; set; }


    }
        
}
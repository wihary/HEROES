namespace AlmaIt.Dotnet.Heroes.Shared.Business
{
    using System;
    
    public class TokenInfo
    {
        public string UserName { get; set; }
        
        public string Token { get; set; }

        public DateTime Expired { get; set; }
    }
}
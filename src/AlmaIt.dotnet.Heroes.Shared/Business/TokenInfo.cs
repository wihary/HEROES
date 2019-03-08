namespace AlmaIt.Dotnet.Heroes.Shared.Business
{
    using System;

    /// <summary>Class represents token info.</summary>
    public class TokenInfo
    {
        /// <summary>Gets or sets the expired date.</summary>
        public DateTime Expired { get; set; }

        /// <summary>Gets or sets the token.</summary>
        public string Token { get; set; }

        /// <summary>Gets or sets the user name.</summary>
        public string UserName { get; set; }
    }
}

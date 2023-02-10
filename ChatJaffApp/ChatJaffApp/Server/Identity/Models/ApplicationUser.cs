using Microsoft.AspNetCore.Identity;

namespace ChatJaffApp.Server.Identity.Models
{
    public class ApplicationUser : IdentityUser
    {
        public bool IsBanned { get; set; }
        public bool AgreedToUserTerms { get; set; }
    }
}

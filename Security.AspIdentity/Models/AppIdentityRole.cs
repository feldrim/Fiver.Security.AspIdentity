using Microsoft.AspNetCore.Identity;

namespace Security.AspIdentity.Models
{
    public class AppIdentityRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
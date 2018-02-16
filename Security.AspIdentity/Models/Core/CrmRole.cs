using Microsoft.AspNetCore.Identity;

namespace Security.AspIdentity.Models.Core
{
    public class CrmRole : IdentityRole
    {
        public string Description { get; set; }
    }
}
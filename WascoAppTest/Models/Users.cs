using Microsoft.AspNetCore.Identity;

namespace WascoAppTest.Models
{
    public class Users : IdentityUser
    {
        public string FullName { get; set; }
    }
}

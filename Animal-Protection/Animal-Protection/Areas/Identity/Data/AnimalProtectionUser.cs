using Microsoft.AspNet.Identity;
using Microsoft.AspNetCore.Identity;

namespace Animal_Protection.Areas.Identity.Data;

// Add profile data for application users by adding properties to the AnimalProtectionUser class
public class AnimalProtectionUser : IdentityUser
{
    public string FullName { get; set; }
}


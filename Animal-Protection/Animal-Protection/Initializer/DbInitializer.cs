using Animal_Protection.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Protection_Animal.Utility;

namespace Animal_Protection.Initializer
{
    public class DbInitializer : IDbInitializer
    {
        private readonly IdentityContext _context;
        private readonly UserManager<AnimalProtectionUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(IdentityContext context, UserManager<AnimalProtectionUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }

        public void Initialize()
        {
            try
            {
                if (_context.Database.GetPendingMigrations().Count() > 0)
                {
                    _context.Database.Migrate();
                }
            }
            catch (Exception ex)
            {

            }


            if (!_roleManager.RoleExistsAsync(WebConstants.Admin).GetAwaiter().GetResult())
            {
                _roleManager.CreateAsync(new IdentityRole(WebConstants.Admin)).GetAwaiter().GetResult();
                _roleManager.CreateAsync(new IdentityRole(WebConstants.User)).GetAwaiter().GetResult();
            }
            else
            {
                return;
            }

            _userManager.CreateAsync(new AnimalProtectionUser
            {
                UserName = "admin@admin.com",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                FullName = "Admin",
                PhoneNumber = "777-777"
            }, "Admin777*").GetAwaiter().GetResult();

            AnimalProtectionUser user = _context.AnimalProtectionUsers.FirstOrDefault(u => u.Email == "admin@admin.com");
            _userManager.AddToRoleAsync(user, WebConstants.Admin).GetAwaiter().GetResult();
        }
    }
}
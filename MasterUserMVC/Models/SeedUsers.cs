using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace MasterUserMVC.Models {

  public class SeedUsers {

      public static void Initialize(IServiceProvider serviceProvider)
      {
        var context = HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();

      string[] roles = new string[] { "Owner", "Administrator", "Manager", "Editor", "Buyer", "Business", "Seller", "Subscriber" };

        foreach (string role in roles) {
          var roleStore = new RoleStore<IdentityRole>(context);

          if (!context.Roles.Any(r => r.Name == role)) {
            roleStore.CreateAsync(new IdentityRole(role));
          }
        }


        var user = new ApplicationUser {
          FirstName = "Muhammad",
          LastName = "Abdullah",
          Email = "abdullahnaseer999@gmail.com",
          NormalizedEmail = "ABDULLAHNASEER999@GMAIL.COM",
          UserName = "Owner",
          NormalizedUserName = "OWNER",
          PhoneNumber = "+923366633352",
          EmailConfirmed = true,
          PhoneNumberConfirmed = true,
          SecurityStamp = Guid.NewGuid().ToString("D")
        };


        if (!context.Users.Any(u => u.UserName == user.UserName)) {
          var password = new PasswordHasher<ApplicationUser>();
          var hashed = password.HashPassword(user, "secret");
          user.PasswordHash = hashed;

          var userStore = new UserStore<ApplicationUser>(context);
          var result = userStore.CreateAsync(user);

        }

        AssignRoles(serviceProvider, user.Email, roles);

        context.SaveChangesAsync();
      }

      public static async Task<IdentityResult> AssignRoles(IServiceProvider services, string email, string[] roles) {
        UserManager<ApplicationUser> _userManager = services.GetService<UserManager<ApplicationUser>>();
        ApplicationUser user = await _userManager.FindByEmailAsync(email);
        var result = await _userManager.AddToRolesAsync(user, roles);

        return result;
      }

    
  }

}
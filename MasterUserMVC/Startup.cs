using MasterUserMVC.Models;
using Microsoft.AspNet.Identity;
using Microsoft.Owin;
using Owin;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using MasterUserMVC.Controllers;
using Microsoft.AspNet.Identity.EntityFramework;
 
[assembly: OwinStartupAttribute(typeof(MasterUserMVC.Startup))]
namespace MasterUserMVC
{
    public partial class Startup
    {
    public void Configuration(IAppBuilder app)
        {
              ConfigureAuth(app);
            app.MapSignalR();

      //     truncateDB();
            seedDB();
    }




      // In this method we will create default User roles and Admin user for login   
    private void seedDB() {
      ApplicationDbContext context = new ApplicationDbContext();
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
      var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

      // In Startup iam creating first Admin Role and creating a default Admin User    
      if (!roleManager.RoleExists("Admin")) {

        // first we create Admin rool   
        var role = new IdentityRole { Name = "Admin"};
        roleManager.Create(role);

        //Here we create a Admin super user who will maintain the website                  

        var adminUser = new ApplicationUser { UserName = "admin@test.com", Email = "admin@test.com" };
        var result =  UserManager.Create(adminUser, "Admin123");
        //Add default User to Role Admin   
        if (result.Succeeded) 
           UserManager.AddToRole(adminUser.Id, "Admin");

        UserManager.Create(new ApplicationUser {UserName = "user1@test.com", Email = "user1@test.com" }, "User123");
        UserManager.Create(new ApplicationUser {UserName = "user2@test.com", Email = "user2@test.com" }, "User123");
      }


      //// creating Creating Manager role    
      //if (!roleManager.RoleExists("Manager")) {
      //  var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
      //  role.Name = "Manager";
      //  roleManager.Create(role);

      //}

      //// creating Creating Employee role    
      //if (!roleManager.RoleExists("Employee")) {
      //  var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
      //  role.Name = "Employee";
      //  roleManager.Create(role);
      //}

    }
 
      //truncate DB
    private void truncateDB() {
      ApplicationDbContext context = new ApplicationDbContext();
      var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
      var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));

      foreach (var u in UserManager.Users.ToList())
        UserManager.Delete(u);

      context.SaveChanges();

      foreach (var r in roleManager.Roles.ToList())
        roleManager.Delete(r);
    }
  }
   
}

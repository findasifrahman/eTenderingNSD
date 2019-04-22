using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace BSDKhulna.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
        public string roleName { get; set; }


    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        static ApplicationDbContext()
        {

            //Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseIfModelChanges<ApplicationDbContext>());
            Database.SetInitializer<ApplicationDbContext>(new DropCreateDatabaseAlways<ApplicationDbContext>());
            Database.SetInitializer(new MyDbInitializer());
        }
        public ApplicationDbContext()
            : base("BSDKhulnaUser", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
        public class MyDbInitializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                InitializeIdentityForEF(context);
                base.Seed(context);
            }

            private void InitializeIdentityForEF(ApplicationDbContext context)
            {
                var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));
                var RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
                //var myinfo = new MyUserInfo() { FirstName = "Pranav", LastName = "Rastogi" };
                string name = "admin@gmail.com";
                string password = "admin12";
                string test = "Admin";

                //Create Role Test and User Test

                //RoleManager.Create(new IdentityRole(test));
                //UserManager.Create(new ApplicationUser() { UserName = name });

                //Create Role Admin if it does not exist
                if (!RoleManager.RoleExists(test))
                {
                    var roleresult = RoleManager.Create(new IdentityRole(test));
                }
                if (!RoleManager.RoleExists("Admin")) { var roleresult = RoleManager.Create(new IdentityRole("Admin")); }
                if (!RoleManager.RoleExists("IT")) { var roleresult = RoleManager.Create(new IdentityRole("IT")); }
                if (!RoleManager.RoleExists("CST")) { var roleresult = RoleManager.Create(new IdentityRole("CST")); }
                if (!RoleManager.RoleExists("LP")) { var roleresult = RoleManager.Create(new IdentityRole("LP")); }
                if (!RoleManager.RoleExists("CR")) { var roleresult = RoleManager.Create(new IdentityRole("CR")); }
                if (!RoleManager.RoleExists("CINS")) { var roleresult = RoleManager.Create(new IdentityRole("CINS")); }
                if (!RoleManager.RoleExists("BILL")) { var roleresult = RoleManager.Create(new IdentityRole("BILL")); }
                if (!RoleManager.RoleExists("CASH")) { var roleresult = RoleManager.Create(new IdentityRole("CASH")); }
                if (!RoleManager.RoleExists("PPC")) { var roleresult = RoleManager.Create(new IdentityRole("PPC")); }

                /////////////////////////////////////////////////////////////////
                //Create User=Admin with password=123456
                var user = new ApplicationUser();
                user.Email = name;
                user.UserName = "admin";
                user.roleName = "Admin";
                var adminresult = UserManager.Create(user, password);

                //Add User Admin to Role Admin
                if (adminresult.Succeeded)
                {
                    var result = UserManager.AddToRole(user.Id, test);
                }
            }
        }
    }
}
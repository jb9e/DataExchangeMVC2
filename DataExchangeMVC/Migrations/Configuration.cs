using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using WebMatrix.WebData;
using System.Web.Security;

namespace DataExchangeMVC.Migrations
{
    internal sealed class Configuration : DbMigrationsConfiguration<DataExchangeMVC.Models.DataExchangeDBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(DataExchangeMVC.Models.DataExchangeDBContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data. E.g.
            //
            //    context.People.AddOrUpdate(
            //      p => p.FullName,
            //      new Person { FullName = "Andrew Peters" },
            //      new Person { FullName = "Brice Lambson" },
            //      new Person { FullName = "Rowan Miller" }
            //    );
            //

            WebSecurity.InitializeDatabaseConnection(
                "DataExchangeDBContext",
                "UserProfile",
                "UserId",
                "UserName", autoCreateTables: true);

            if (!Roles.RoleExists("Admin"))
                Roles.CreateRole("Admin");

            if (!Roles.RoleExists("User"))
                Roles.CreateRole("User");

            if (!WebSecurity.UserExists("Administrator"))
                WebSecurity.CreateUserAndAccount(
                    "Administrator",
                    "Administrator");

            if (!WebSecurity.UserExists("User"))
                WebSecurity.CreateUserAndAccount(
                    "User",
                    "password");

            if (!Roles.GetRolesForUser("Administrator").Contains("Admin"))
                Roles.AddUsersToRoles(new[] { "Administrator" }, new[] { "Admin" });

            if (!Roles.GetRolesForUser("User").Contains("User"))
                Roles.AddUsersToRoles(new[] { "User" }, new[] { "User" });
        }
    }
}

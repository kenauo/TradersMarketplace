namespace TradersMarketplace.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using TradersMarketplace.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<TradersMarketplace.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(TradersMarketplace.Models.ApplicationDbContext context)
        {
            this.AddUserAndRoles();
        }

        bool AddUserAndRoles()
        {
            bool success = false;

            var idManager = new IdentityManager();
            success = idManager.CreateRole("Admin");
            if (!success == true) return success;

            success = idManager.CreateRole("CanEdit");
            if (!success == true) return success;

            success = idManager.CreateRole("User");

            if (!success) return success;

            var newUser = new ApplicationUser()
            {
                UserName = "Blaze",
                FirstName = "Kenneth",
                LastName = "Cauchi",
                Email = "kenneth.cauchi9@gmail.com"
            };

            success = idManager.CreateUser(newUser, "Derp123!");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "Admin");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "CanEdit");
            if (!success) return success;

            success = idManager.AddUserToRole(newUser.Id, "User");
            if (!success) return success;

            return success;
        }

    }
}

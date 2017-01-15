namespace Vidly.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class SeedUsers : DbMigration
    {
        public override void Up()
        {
            Sql(@"INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'a6f36de7-2e97-4b35-a808-9607b88c91f0', N'guest@vidly.com', 0, N'AGaQTjwPd7kstEqk4eeo7/sV6OEnysUSYkkEbrR8ugpIPpGi2B8UcXGzt65yXTVqnw==', N'2f0bd0f3-389b-4406-89a3-92d2177caa3d', NULL, 0, 0, NULL, 1, 0, N'guest@vidly.com')
INSERT INTO [dbo].[AspNetUsers] ([Id], [Email], [EmailConfirmed], [PasswordHash], [SecurityStamp], [PhoneNumber], [PhoneNumberConfirmed], [TwoFactorEnabled], [LockoutEndDateUtc], [LockoutEnabled], [AccessFailedCount], [UserName]) VALUES (N'b58fb638-895c-4ca3-833b-e5323d1a65cc', N'admin@vidly.com', 0, N'ABLfPvWHNsEJcm59kouy3A3sDKkgTf8RHctL853Bf4RWmhprP2MFLlRSPNHh0NxK+A==', N'f2edb448-119e-4c01-a3d0-7dd8e86818b4', NULL, 0, 0, NULL, 1, 0, N'admin@vidly.com')

INSERT INTO [dbo].[AspNetRoles] ([Id], [Name]) VALUES (N'bbc0190c-afa7-43af-9d95-746f54506bbd', N'CanManageMovies')

INSERT INTO [dbo].[AspNetUserRoles] ([UserId], [RoleId]) VALUES (N'b58fb638-895c-4ca3-833b-e5323d1a65cc', N'bbc0190c-afa7-43af-9d95-746f54506bbd')
");
        }
        
        public override void Down()
        {
        }
    }
}

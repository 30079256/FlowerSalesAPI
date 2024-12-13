using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SMTAFE_Web_App.Areas.Identity.Data;

namespace SMTAFE_Web_App.Data;

public class SMTAFE_Web_AppContext : IdentityDbContext<SMTAFE_Web_AppUser>
{
    public SMTAFE_Web_AppContext(DbContextOptions<SMTAFE_Web_AppContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Siimple.Models;

namespace Siimple.DataContext
{
    public class SiimpleDbContext :IdentityDbContext<AppUser>
    {
        public SiimpleDbContext(DbContextOptions<SiimpleDbContext> opt) :base(opt)
        {
            
        }
        public DbSet<Team> Team { get; set; }
        public DbSet<Title> Title { get; set; }
        public DbSet<Setting> Setting { get; set; }
    }
}

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MessagesStore.ViewModels;

namespace MessagesStore.Models
{
    public class AppContext : IdentityDbContext<User> /*DbContext*/
    {
        public DbSet<Message> Messages { get; set; }

        public DbSet<RegistrationViewModel> RegistrationViewModel { get; set; }
        public DbSet<LoginViewModel> LoginViewModel { get; set; }

        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
            //Database.EnsureCreated();
        }
    }
}

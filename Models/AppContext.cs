using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MessagesStore.ViewModels;

namespace MessagesStore.Models
{
    public class AppContext : IdentityDbContext<User>
    {
        public AppContext(DbContextOptions<AppContext> options) : base(options)
        {
           // Database.EnsureCreated();
        }
        public DbSet<MessagesStore.ViewModels.RegistrationViewModel> RegistrationViewModel { get; set; }
        public DbSet<MessagesStore.ViewModels.LoginViewModel> LoginViewModel { get; set; }
    }
}

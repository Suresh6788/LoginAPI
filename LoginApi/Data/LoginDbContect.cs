using LoginApi.Models.UserLogin;
using Microsoft.EntityFrameworkCore;

namespace LoginApi.Data
{
    public class LoginDbContect :DbContext
    {
        public LoginDbContect(DbContextOptions<LoginDbContect> options) : base(options)
        {
                
        }
        public DbSet<User> Users { get; set; } 
    }
}

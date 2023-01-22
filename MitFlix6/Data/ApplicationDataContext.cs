using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using MitFlix6.Models;

namespace MitFlix6.Data
{
    public class ApplicationDataContext: IdentityDbContext<ApplicationUser, IdentityRole<Guid>,Guid>
    {
        public ApplicationDataContext(DbContextOptions<ApplicationDataContext> options):base(options)
        {

        }
    }
}

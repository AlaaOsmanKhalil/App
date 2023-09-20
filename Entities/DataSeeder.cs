/*using DatingApp.Data;
using System.Linq;

namespace DatingApp.Entities
{
    public class DataSeeder
    {
        private readonly DataContext _context;

        public DataSeeder(DataContext context)
        {
            _context = context;
        }

        public void SeedData()
        {
            if (!_context.Users.Any())
            {
                // Add mock user data
                var users = new[]
                {
                    new AppUser { UserName = "Alaa" },
                    new AppUser { UserName = "Omar" },
                    new AppUser { UserName = "Kevin"}
                    // Add more mock users as needed
                };

                _context.Users.AddRange(users);
                _context.SaveChanges();
            }
        }
    }
}
*/
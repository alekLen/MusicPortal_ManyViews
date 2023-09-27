using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.EF
{
    public class MusicPortalContext : DbContext
    {
        public MusicPortalContext(DbContextOptions<MusicPortalContext> options)
         : base(options)
        {
            if (Database.EnsureCreated())
            {
                string pass = "qwerty";
                byte[] saltbuf = new byte[16];
                RandomNumberGenerator randomNumberGenerator = RandomNumberGenerator.Create();
                randomNumberGenerator.GetBytes(saltbuf);
                StringBuilder sb = new StringBuilder(16);
                for (int i = 0; i < 16; i++)
                    sb.Append(string.Format("{0:X2}", saltbuf[i]));
                string salt = sb.ToString();
                Salt s = new();
                s.salt = salt;
                string password = salt + pass;
                string hashedPassword = BCrypt.Net.BCrypt.HashPassword(password);
                User u = new User { Name = "admin", Password = hashedPassword, Level = 2, Age = "27", email = "asd@gmail.com" };
                Users.Add(u);
                SaveChanges();
                s.user = u;
                Salts.Add(s);
                SaveChanges();
                SaveChanges();
            }
        }
        public DbSet<User> Users { get; set; }
        public DbSet<Salt> Salts { get; set; }
        public DbSet<Style> Styles { get; set; }
        public DbSet<Artist> Artists { get; set; }
        public DbSet<Song> Songs { get; set; }
    }
}

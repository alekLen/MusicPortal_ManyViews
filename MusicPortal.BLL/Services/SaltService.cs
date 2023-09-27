using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using System.Security.Cryptography;
using System.Text;

namespace MusicPortal.BLL.Services
{
    public class SaltService: ISaltService
    {
        IUnitOfWork Database { get; set; }

        public SaltService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task<SaltDTO> GetSalt(UserDTO u)
        {
            User user = new User
            {
                Id = u.Id,
                Name = u.Name,
                Password = u.Password,
                Level = u.Level,
                email = u.email,
                Age = u.Age,
            };
            Salt s= await Database.Salts.Get(user);
            return new SaltDTO()
            {
                Id=s.Id,
                salt=s.salt,
                userId=user.Id
            };
        }
        public async Task AddSalt(SaltDTO s)
        {
            User u = await Database.Users.Get(s.userId);
            Salt salt = new() { 
                Id = s.Id,
                salt=s.salt,
                user=u
            };

            await Database.Salts.AddItem(salt);
            await Database.Save();
        }
       
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IArtistRepository Artists { get; }
        IStyleRepository Styles { get; }
        ISongRepository Songs { get; }
        ISaltRepository Salts { get; }
        IUserRepository Users { get; }
        Task Save();
    }
}

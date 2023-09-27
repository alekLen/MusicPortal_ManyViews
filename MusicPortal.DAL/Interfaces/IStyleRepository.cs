using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface IStyleRepository : ISetGetRepository<Style>, IRepository<Style>, IGetIdRepository
    {
        Task Update(int id, string s);
    }
}

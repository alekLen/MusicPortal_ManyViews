using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace MusicPortal.DAL.Repositories
{
    public class SaltRepository : ISaltRepository
    {
        private MusicPortalContext db;

        public SaltRepository(MusicPortalContext context)
        {
            this.db = context;
        }
        public async Task<Salt> Get(User u)
        {
            return await db.Salts.FirstOrDefaultAsync(m => m.user == u);
        }
        public async Task AddItem(Salt s)
        {
            await db.AddAsync(s);
        }
    }
}

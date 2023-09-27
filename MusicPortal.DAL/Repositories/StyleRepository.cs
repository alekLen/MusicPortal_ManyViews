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
    public class StyleRepository : IStyleRepository
    {
        private MusicPortalContext db;

        public StyleRepository(MusicPortalContext context)
        {
            this.db = context;
        }
        public async Task<Style> Get(int id)
        {
            return await db.Styles.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<int> GetId(Song s)
        {
            Style a = s.style;
            return a.Id;
        }
        public async Task<List<Style>> GetList()
        {
            return await db.Styles.ToListAsync();
        }
        public async Task AddItem(Style s)
        {
            await db.AddAsync(s);
        }
        public async Task Delete(int id)
        {
            var f = await db.Styles.FindAsync(id);
            if (f != null)
            {
                db.Styles.Remove(f);

            }
        }
        public async Task Update(int id, string s)
        {
            var f = await db.Styles.FindAsync(id);
            if (f != null)
            {
                f.Name = s;
                db.Styles.Update(f);

            }
        }
    }
}

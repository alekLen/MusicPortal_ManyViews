using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MusicPortal.DAL.EF;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Repositories
{
    public class ArtistRepository :  IArtistRepository
    {
        private MusicPortalContext db;

        public ArtistRepository(MusicPortalContext context)
        {
            this.db = context;
        }
        public async Task<Artist> Get(int id)
        {
            return await db.Artists.FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Artist>> GetList()
        {
            return await db.Artists.ToListAsync();
        }
        public async Task AddItem(Artist s)
        {
            await db.AddAsync(s);
        }
        public async Task Delete(int id)
        {
            var f = await db.Artists.FindAsync(id);
            if (f != null)
            {
                db.Artists.Remove(f);

            }
        }
        public async Task Update(int id, string s, string p)
        {
            var f = await db.Artists.FindAsync(id);
            if (f != null)
            {
                f.Name = s;
                f.photo = p;
                db.Artists.Update(f);

            }
        }
        public async Task<int> GetId(Song s)
        {       
            Artist a = await db.Artists.FirstOrDefaultAsync(m => m == s.artist);
            return a.Id;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Interfaces;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.EF;
using Microsoft.EntityFrameworkCore;
using System.Numerics;


namespace MusicPortal.DAL.Repositories
{
    public class SongRepository :  ISongRepository
    {
        private MusicPortalContext db;

        public SongRepository(MusicPortalContext context)
        {
            this.db = context;
        }
        public async Task<Song> Get(int id)
        {
            return await db.Songs.Include((p) => p.artist).Include((p) => p.style).FirstOrDefaultAsync(m => m.Id == id);
        }
        public async Task<List<Song>> GetList()
        {
            return await db.Songs.Include((p) => p.artist).Include((p) => p.style).ToListAsync();
        }
        public async Task<List<Song>> FindSongs(string str)
        {
            Artist a = await db.Artists.FirstOrDefaultAsync(m => m.Name == str);
            if (a == null)
                return await db.Songs.Where(son => son.Name == str).Include((p) => p.artist).Include((p) => p.style).ToListAsync();
            else
                return await db.Songs.Where(son => son.artist == a).Include((p) => p.artist).Include((p) => p.style).ToListAsync();
        }
        public async Task AddItem(Song s)
        {
            await db.AddAsync(s);
        }
        public async Task AddSongToArtist(int id, Song s)
        {

            var f = await db.Artists.FindAsync(id);
            Song s1 = await db.Songs.Where(son => son.artist == f).FirstOrDefaultAsync(m => m.Name == s.Name);
            if (f != null && s1 != null)
            {
                f.Songs.Add(s1);
                db.Artists.Update(f);
            }
        }
        public async Task Delete(int id)
        {
            var f = await db.Songs.FindAsync(id);
            if (f != null)
            {
                db.Songs.Remove(f);
            }
        }
        public void Update(Song s)
        {
            try
            {
                db.Songs.Update(s);
                //db.Entry(s).State = EntityState.Modified;
               
            }
            catch { }
        }
    }
}

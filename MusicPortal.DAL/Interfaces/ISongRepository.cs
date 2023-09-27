using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicPortal.DAL.Entities;

namespace MusicPortal.DAL.Interfaces
{
    public interface ISongRepository : IRepository<Song>, ISetGetRepository<Song>
    {  
        Task<List<Song>> FindSongs(string str);
        Task AddSongToArtist(int id, Song s);
        void Update(Song s);

    }
}

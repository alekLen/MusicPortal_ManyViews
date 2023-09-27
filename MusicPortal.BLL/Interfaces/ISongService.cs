using MusicPortal.BLL.DTO;

namespace MusicPortal.BLL.Interfaces
{
    public interface ISongService
    {
         Task AddSong(SongDTO songDto);
        Task<SongDTO> GetSong(int id);
        Task<IEnumerable<SongDTO>> GetAllSongs();
        Task<IEnumerable<SongDTO>> FindSongs(string str);
        Task AddSongToArtist(int id, SongDTO songDto);
        Task DeleteSong(int id);
        Task UpdateSong(SongDTO songDto);
    }
}

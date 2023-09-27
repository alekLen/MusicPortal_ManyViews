using MusicPortal.BLL.DTO;


namespace MusicPortal.BLL.Interfaces
{
    public interface IArtistService
    {
        Task AddArtist(ArtistDTO artistDto);
        Task<ArtistDTO> GetArtist(int id);
        Task<IEnumerable<ArtistDTO>> GetAllArtists();
        Task<int> GetArtistId(SongDTO song);
        Task DeleteArtist(int id);
        Task UpdateArtist(int id, string n, string p);
    }
}

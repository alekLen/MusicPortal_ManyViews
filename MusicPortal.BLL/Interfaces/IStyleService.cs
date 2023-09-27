using MusicPortal.BLL.DTO;


namespace MusicPortal.BLL.Interfaces
{
    public interface IStyleService
    {
        Task AddStyle(StyleDTO styleDto);
        Task<StyleDTO> GetStyle(int id);
        Task<IEnumerable<StyleDTO>> GetAllStyles();
        Task<int> GetStyleId(SongDTO song);
        Task DeleteStyle(int id);
        Task UpdateStyle(int id, string n);
    }
}

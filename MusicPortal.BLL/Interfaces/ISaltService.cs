using MusicPortal.BLL.DTO;


namespace MusicPortal.BLL.Interfaces
{
    public interface ISaltService
    {
        Task<SaltDTO> GetSalt(UserDTO u);
        Task AddSalt(SaltDTO s);
    }
}

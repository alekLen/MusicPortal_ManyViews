using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class SongService: ISongService
    {
        IUnitOfWork Database { get; set; }

        public SongService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddSong(SongDTO songDto)
        {
            Song s = await SongDTOToSong(songDto);
            await Database.Songs.AddItem(s);
            await Database.Save();
        }
        public async Task<SongDTO> GetSong(int id)
        {
            var s = await Database.Songs.Get(id);
            if (s == null)
                throw new ValidationException("Wrong song!", "");
            return new SongDTO
            {
                Id = s.Id,
                Name = s.Name,
                file = s.file,
                Year = s.Year,
                Album = s.Album,
                text = s.text,
                artist = s.artist.Name,
                artistId = s.artist.Id,
                style = s.style.Name,
                styleId = s.style.Id,
                artistPhoto = s.artist.photo
            };
        }
        public async Task<IEnumerable<SongDTO>> GetAllSongs()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
            .ForMember("artist", opt => opt.MapFrom(c => c.artist.Name)).ForMember("style", opt => opt.MapFrom(c => c.style.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.GetList());
        }
        public async Task<IEnumerable<SongDTO>> FindSongs( string str)
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Song, SongDTO>()
             .ForMember("artist", opt => opt.MapFrom(c => c.artist.Name)).ForMember("style", opt => opt.MapFrom(c => c.style.Name)));
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Song>, IEnumerable<SongDTO>>(await Database.Songs.FindSongs(str));
        }
        public async Task AddSongToArtist(int id,SongDTO songDto)
        {
            Song s =await SongDTOToSong(songDto);
            await Database.Songs.AddSongToArtist(id, s);
            await Database.Save();
        }
        public async Task<Song> SongDTOToSong(SongDTO songDto)
        {
            Artist a = await Database.Artists.Get(songDto.artistId.Value);
            Style st = await Database.Styles.Get(songDto.styleId.Value);
            var s = new Song
            {
                Id = songDto.Id,
                Name = songDto.Name,
                file = songDto.file,
                Year = songDto.Year,
                Album = songDto.Album,
                text = songDto.text,
                artist = a,
                style = st
            };
            return s;
        }
        public async Task DeleteSong(int id)
        {
            await Database.Songs.Delete(id);
            await Database.Save();
        }
        public async Task UpdateSong(SongDTO songDto)
        {
            Song s = await SongDTOToSong(songDto);          
              Database.Songs.Update(s);
             await Database.Save();
        }
    }
}

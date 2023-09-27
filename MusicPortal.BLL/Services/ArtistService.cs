using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;
using System.Numerics;
using System;

namespace MusicPortal.BLL.Services
{
    public class ArtistService: IArtistService
    {
        IUnitOfWork Database { get; set; }

        public ArtistService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddArtist(ArtistDTO artistDto)
        {
            var a = new Artist
            {
                Id = artistDto.Id,
                Name = artistDto.Name,
                photo = artistDto.photo              
            };
            await Database.Artists.AddItem(a);
            await Database.Save();
        }
        public async Task<ArtistDTO> GetArtist(int id)
        {
            var a = await Database.Artists.Get(id);
            if (a == null)
                throw new ValidationException("Wrong artist!", "");
            return new ArtistDTO
            {
                Id = a.Id,
                Name = a.Name,
                photo = a.photo               
            };
        }
        public async Task<IEnumerable<ArtistDTO>> GetAllArtists()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Artist, ArtistDTO>() );
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Artist>, IEnumerable<ArtistDTO>>(await Database.Artists.GetList());
        }
        public async Task<int> GetArtistId(SongDTO song)
        {
            Song s= await Database.Songs.Get(song.Id);
            return await Database.Artists.GetId(s);
        }
        public async Task DeleteArtist(int id)
        {
            await Database.Artists.Delete(id);
            await Database.Save();
        }
        public async Task UpdateArtist(int id,string n,string p)
        {        
           await Database.Artists.Update(id, n, p);
            await Database.Save();
        }
    }
}

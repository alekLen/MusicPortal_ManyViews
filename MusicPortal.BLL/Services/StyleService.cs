using MusicPortal.DAL.Interfaces;
using MusicPortal.BLL.DTO;
using MusicPortal.DAL.Entities;
using MusicPortal.BLL.Infrastructure;
using MusicPortal.BLL.Interfaces;
using AutoMapper;

namespace MusicPortal.BLL.Services
{
    public class StyleService: IStyleService
    {
        IUnitOfWork Database { get; set; }

        public StyleService(IUnitOfWork uow)
        {
            Database = uow;
        }
        public async Task AddStyle(StyleDTO styleDto)
        {
            var st = new Style
            {
                Id = styleDto.Id,
                Name = styleDto.Name,
            };
            await Database.Styles.AddItem(st);
            await Database.Save();
        }
        public async Task<StyleDTO> GetStyle(int id)
        {
            var st = await Database.Styles.Get(id);
            if (st == null)
                throw new ValidationException("Wrong artist!", "");
            return new StyleDTO
            {
                Id = st.Id,
                Name = st.Name,
            };
        }
        public async Task<IEnumerable<StyleDTO>> GetAllStyles()
        {
            var config = new MapperConfiguration(cfg => cfg.CreateMap<Style, StyleDTO>());
            var mapper = new Mapper(config);
            return mapper.Map<IEnumerable<Style>, IEnumerable<StyleDTO>>(await Database.Styles.GetList());
        }
        public async Task<int> GetStyleId(SongDTO song)
        {
            Song s = await Database.Songs.Get(song.Id);
            return await Database.Styles.GetId(s);
        }
        public async Task DeleteStyle(int id)
        {
            await Database.Styles.Delete(id);
            await Database.Save();
        }
        public async Task UpdateStyle(int id,string n)
        {
            await Database.Styles.Update(id,n);
            await Database.Save();
        }
    }
}

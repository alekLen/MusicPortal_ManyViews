using Microsoft.AspNetCore.Mvc.Rendering;
using MusicPortal.BLL.DTO;
namespace MusikPortal.Models
{
    public class FilterViewModel
    {
        public FilterViewModel(List<ArtistDTO> artists, int art, List<StyleDTO> styles, int st)
        {
            // устанавливаем начальный элемент, который позволит выбрать всех
            artists.Insert(0, new ArtistDTO { Name = "All", Id = 0 });
            styles.Insert(0, new StyleDTO { Name = "All", Id = 0 });
            Artists = new SelectList(artists, "Id", "Name", art);
            Styles = new SelectList(styles, "Id", "Name", st);
            SelectedArtist = art;
            SelectedStyle = st;
        }
        public SelectList Artists { get; } // список аристов
        public SelectList Styles { get; } // список жанров
        public int SelectedArtist { get; } // выбранный артист
        public int SelectedStyle { get; } // выбранный жанр      
    }
}

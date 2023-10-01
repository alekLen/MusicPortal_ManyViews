using MusicPortal.BLL.DTO;
namespace MusikPortal.Models
{
    public class IndexViewModel
    {
        public IEnumerable<SongDTO> Songs { get; set; }
        public PageViewModel PageViewModel { get; }
        public FilterViewModel FilterViewModel { get; }
        public SortViewModel SortViewModel { get; }

        public string find { get; set; }
        public IndexViewModel(IEnumerable<SongDTO> songs, PageViewModel pageViewModel,
            FilterViewModel filterViewModel, SortViewModel sortViewModel, string find )
        {
            Songs = songs;
            PageViewModel = pageViewModel;
            FilterViewModel = filterViewModel;
            SortViewModel = sortViewModel;
            this.find = find;           
        }
    }
}

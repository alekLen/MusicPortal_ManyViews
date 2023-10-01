using MusicPortal.BLL.DTO;
using MusicPortal.Models;

namespace MusikPortal.Models
{
    public class SortViewModel
    {
        public SortState ArtSort { get; set; } 
        public SortState StyleSort { get; set; }    
        public SortState YearSort { get; set; }    
        public SortState Current { get; set; }     // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            ArtSort = SortState.ArtAsc;
            YearSort = SortState.YearAsc;
            StyleSort = SortState.StyleAsc;

            ArtSort = sortOrder == SortState.ArtAsc ? SortState.ArtDesc : SortState.ArtAsc;
            YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            StyleSort = sortOrder == SortState.StyleAsc ? SortState.StyleDesc : SortState.StyleAsc;
            Current = sortOrder;
        }
    }
}

using MusicPortal.BLL.DTO;
using MusicPortal.Models;

namespace MusikPortal.Models
{
    public class SortViewModel
    {
        public SortState ArtSort { get; set; } 
        public SortState StyleSort { get; set; }    
        public SortState YearSort { get; set; }    
        public SortState Current { get; set; }
        public SortState OldCurrent { get; set; }  // значение свойства, выбранного для сортировки
        public bool Up { get; set; }  // Сортировка по возрастанию или убыванию

        public SortViewModel(SortState sortOrder)
        {
            // значения по умолчанию
            ArtSort = SortState.ArtAsc;
            YearSort = SortState.YearDesc;
            StyleSort = SortState.StyleAsc;
            Up = true;
            if (sortOrder == SortState.ArtDesc || sortOrder == SortState.YearDesc || sortOrder == SortState.StyleDesc )
            {
                Up = false;
            }
           ArtSort = sortOrder == SortState.ArtAsc ? SortState.ArtDesc : SortState.ArtAsc;
            YearSort = sortOrder == SortState.YearAsc ? SortState.YearDesc : SortState.YearAsc;
            StyleSort = sortOrder == SortState.StyleAsc ? SortState.StyleDesc : SortState.StyleAsc;
            Current = sortOrder;         
          switch (sortOrder)
            {
                case SortState.ArtDesc:
                    OldCurrent=ArtSort = SortState.ArtAsc;
                    break;
                case SortState.StyleAsc:
                    OldCurrent = StyleSort = SortState.StyleDesc;
                    break;
                case SortState.StyleDesc:
                    OldCurrent = StyleSort = SortState.StyleAsc;
                    break;
                case SortState.ArtAsc:
                    OldCurrent = ArtSort = SortState.ArtDesc;
                    break;
                case SortState.YearDesc:
                    OldCurrent = YearSort = SortState.YearAsc;
                    break;               
                default:
                    OldCurrent = YearSort = SortState.YearDesc;
                    break;
            }
        }
    }
}

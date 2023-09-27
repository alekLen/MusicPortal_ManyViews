using System.ComponentModel.DataAnnotations;

namespace MusikPortal.Models
{
    public class AddSong
    {
        [Required]
        [Display(Name = "enterName", ResourceType = typeof(Resources.Resource))]
        public string Name { get; set; }      
        public int StyleId  { get; set; }
        public int ArtistId { get; set; }
        [Required]
        [Display(Name = "Year", ResourceType = typeof(Resources.Resource))]
        public string Year { get; set; }
        [Required]
        [Display(Name = "Album", ResourceType = typeof(Resources.Resource))]
        public string Album{ get; set; }
        [Required]
        [Display(Name = "Text", ResourceType = typeof(Resources.Resource))]
        public string text { get; set; }      
        [Display(Name = "File", ResourceType = typeof(Resources.Resource))]
        public string? file { get; set; }
        public int? SongId { get; set; }
    }
}

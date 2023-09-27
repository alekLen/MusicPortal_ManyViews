using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class Artist
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? photo { get; set; }

        public ICollection<Song>? Songs { get; set; } = new List<Song>();
    }
}

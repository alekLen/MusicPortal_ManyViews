using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.DAL.Entities
{
    public class Style
    {
        public int Id { get; set; }      
        public string Name { get; set; }
        public ICollection<Song>? Songs { get; set; } = new List<Song>();
    }
}

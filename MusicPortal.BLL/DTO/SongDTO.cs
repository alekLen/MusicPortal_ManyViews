using MusicPortal.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicPortal.BLL.DTO
{
    public class SongDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string? style { get; set; }
        public int? styleId { get; set; }
        public string? artist { get; set; }
        public string? artistPhoto { get; set; }
        public int? artistId { get; set; }
        public string Year { get; set; }
        public string Album { get; set; }
        public string file { get; set; }
        public string text { get; set; }
    }
}

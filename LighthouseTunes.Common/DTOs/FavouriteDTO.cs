using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Common.DTOs
{
    public record FavouriteDTO
    {
        public Guid? Id { get; set; } 

        public Guid SelectedSongId { get; set; }

        public Guid AddedById { get; set; }

        public DateTime? DateAdded { get; set; }

        public SongDTO? SelectedSong { get; set; }

        public UserDTO? AddedBy { get; set; }
    }
}

using LighthouseTunes.Common.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Entities
{
    public class Song
    {
        [Required]
        public Guid SongId { get; set; }

        [Required, MaxLength(150)]
        public string Title { get; set; }

        [Required, MaxLength(150)]
        public string Artist { get; set; }

        [Required, MaxLength(150)]
        public string Album { get; set; }

        [Required]
        public Genre Genre { get; set; }

        [MaxLength(200)]
        public string Featuring { get; set; }

        [Required]
        public DateTime ReleaseDate { get; set; }


        // Favourites referring to this song
        public List<Favourite> FavouritesList { get; set; }
    }
}

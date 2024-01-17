using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Entities
{
    public class Favourite
    {
        [Required]
        public Guid Id { get; set; }

        [Required]
        public Guid SelectedSongId { get; set; }

        [Required]
        public Guid AddedById { get; set; }

        [Required]
        public DateTime DateAdded { get; set; }

        // Navigation properties made possible by foreign-key relationships
        [Required]
        [ForeignKey ("SelectedSongId")]
        public Song SelectedSong { get; set; }

        [Required]
        [ForeignKey("AddedById")]
        public User AddedBy { get; set; }
    }
}

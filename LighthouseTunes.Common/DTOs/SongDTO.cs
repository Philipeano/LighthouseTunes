using LighthouseTunes.Common.Enums;

namespace LighthouseTunes.Common.DTOs
{
    public record SongDTO
    {
        public string Title { get; set; }

        public string Artist { get; set; }

        public string Album { get; set; }

        public Genre Genre { get; set; }

        public string Featuring { get; set; }

        public DateTime ReleaseDate { get; set; }

    }
}

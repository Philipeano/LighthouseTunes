using LighthouseTunes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Services
{
    public class SongService
    {
        private readonly LighthouseTunesDbContext _context = new LighthouseTunesDbContext();
        private List<Song> _songs;


        // Create a song
        public void CreateSong(string title, string artist, string album, string featuring, Genre genre, DateTime releaseDate)
        {
            var newSong = new Song()
            {
                SongId = new Guid(),
                Title = title,
                Artist = artist,
                Album = album,
                Featuring = featuring,
                Genre = genre,
                ReleaseDate = releaseDate
            };

            _context.Songs.Add(newSong);
            _context.SaveChanges();
        }


        // Fetch all songs
        public List<Song> FetchAllSongs()
        {
            return _context.Songs.ToList();
        }


        // Fetch songs filtered by one or more parameters
        
        public List<Song> FetchWithFilter(string titleFilter, string artistFilter, int genreFilter)
        {
            // First, retrieve all songs available
            _songs = FetchAllSongs();

            // Next, apply title filter if available
            if (!string.IsNullOrWhiteSpace(titleFilter))
                _songs = _songs.Where(s => s.Title == titleFilter).ToList();

            // Next, apply artist filter if available
            if (!string.IsNullOrWhiteSpace(artistFilter))
                _songs = _songs.Where(s => s.Artist == artistFilter).ToList();

            // Next, apply genre filter if available
            if (Enum.IsDefined(typeof(Genre), genreFilter) && (genreFilter != (int)Genre.Unknown))
                _songs = _songs.Where(s => (int)s.Genre == genreFilter).ToList();

            return _songs;
        }


        /*
         
        - Create
        - Fetch all
        - Fetch with filter (title, artist, genre)
        - Update 
        - Delete
         
         */

    }
}

using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Common.Enums;
using LighthouseTunes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Services
{
    public interface ISongService
    {
        void CreateSong(string title, string artist, string album, string featuring, Genre genre, DateTime releaseDate);
        void DeleteSong(Guid songId);
        List<Song> FetchAllSongs();
        List<Song> FetchSongsWithFilter(string titleFilter, string artistFilter, int genreFilter);
        void UpdateSong(Guid songId, SongDTO songWithNewInfo);
    }


    public class SongService : ISongService
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

        public List<Song> FetchSongsWithFilter(string titleFilter, string artistFilter, int genreFilter)
        {
            // First, retrieve all songs available
            _songs = FetchAllSongs();

            // Next, apply title filter if available
            if (!string.IsNullOrWhiteSpace(titleFilter))
                _songs = _songs.Where(s => s.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            // Next, apply artist filter if available
            if (!string.IsNullOrWhiteSpace(artistFilter))
                _songs = _songs.Where(s => s.Artist.Contains(artistFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            // Next, apply genre filter if available
            if (Enum.IsDefined(typeof(Genre), genreFilter) && (genreFilter != (int)Genre.Unknown))
                _songs = _songs.Where(s => (int)s.Genre == genreFilter).ToList();

            return _songs;
        }


        // Update a song
        public void UpdateSong(Guid songId, SongDTO songWithNewInfo)
        {
            // Attempt retrieving a song with the given Id
            var songToUpdate = FetchAllSongs()
                .Where(s => s.SongId == songId)
                .FirstOrDefault();

            // No match found. Exit the method.
            if (songToUpdate == null)
            {
                Console.WriteLine("Could not find a song with the given Id.");
                return;
            }

            // A match was found. Perform the update.
            if (!string.IsNullOrWhiteSpace(songWithNewInfo.Title))
                songToUpdate.Title = songWithNewInfo.Title;

            if (!string.IsNullOrWhiteSpace(songWithNewInfo.Artist))
                songToUpdate.Artist = songWithNewInfo.Artist;

            if (!string.IsNullOrWhiteSpace(songWithNewInfo.Album))
                songToUpdate.Album = songWithNewInfo.Album;

            if (Enum.IsDefined(typeof(Genre), songWithNewInfo.Genre) && (songWithNewInfo.Genre != (int)Genre.Unknown))
                songToUpdate.Genre = songWithNewInfo.Genre;

            if (!string.IsNullOrWhiteSpace(songWithNewInfo.Featuring))
                songToUpdate.Featuring = songWithNewInfo.Featuring;

            DateTime parsedReleaseDate;
            if (DateTime.TryParse(songWithNewInfo.ReleaseDate.ToString(), out parsedReleaseDate))
                songToUpdate.ReleaseDate = parsedReleaseDate;

            _context.SaveChanges();
        }


        // Delete a song
        public void DeleteSong(Guid songId)
        {
            // Attempt retrieving a song with the given Id
            var songToDelete = FetchAllSongs()
                .Where(s => s.SongId == songId)
                .FirstOrDefault();

            // No match found. Exit the method.
            if (songToDelete == null)
            {
                Console.WriteLine("Could not find a song with the given Id.");
                return;
            }

            // A match was found. Perform the deletion.
            _context.Songs.Remove(songToDelete);
            _context.SaveChanges();
        }
    }
}

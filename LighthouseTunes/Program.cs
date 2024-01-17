using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Common.Enums;
using LighthouseTunes.Data.Entities;
using LighthouseTunes.Data.Services;

namespace LighthouseTunes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ISongService songService = new SongService();
            var availableSongs = new List<Song>();

            // Call the FetchAll method to retrieve all songs
            availableSongs = songService.FetchAllSongs();

            // Call the CreateSong method a few times to populate the Songs table
            songService.CreateSong(title: "Confession", artist: "Usher", album: "Confession", featuring: "", genre: Genre.RnB, releaseDate: new DateTime(2006, 3, 1));
            songService.CreateSong(title: "Holla At Your Boy", artist: "Wizkid", album: "Ayo", featuring: "Ice Prince", genre: Genre.Afrobeats, releaseDate: new DateTime(2000, 3, 1));
            songService.CreateSong(title: "Back When", artist: "Davido", album: "Back When", featuring: "Naeto-C", genre: Genre.Afrobeats, releaseDate: new DateTime(2007, 10, 5));
            songService.CreateSong(title: "Funny Identity", artist: "Oliver De Coque", album: "Identity", featuring: "", genre: Genre.Highlife, releaseDate: new DateTime(1987, 1, 6));


            // Call FetchWithFilter
            var songsFilteredByTitleOnly = songService.FetchSongsWithFilter(titleFilter: "Boy", artistFilter: null, genreFilter: 0);
            var songsFilteredByArtistOnly = songService.FetchSongsWithFilter(titleFilter: null, artistFilter: "Oliver", genreFilter: 0);
            var songsFilteredByGenreOnly = songService.FetchSongsWithFilter(titleFilter: null, artistFilter: null, genreFilter: 1);


            // Call the FetchAll method to retrieve all songs
            availableSongs = songService.FetchAllSongs();


            // Call UpdateSong
            var songIdToUpdate = new Guid("81a70dd3-92f5-4fe6-5b00-08dc173f9a85");            
            var songWithNewInfo = new SongDTO()
            {
                Title = "IDK",
                Album = "Soundman 2",
                Featuring = "Zlatan",
                ReleaseDate = new DateTime(2023,12,1)
            };
            songService.UpdateSong(songIdToUpdate, songWithNewInfo);


            // Call the FetchAll method to retrieve all songs
            var availableSongsAfterUpdate = songService.FetchAllSongs();


            // Call DeleteSong
            var songIdToDelete = new Guid("3b4a92c9-cafe-4c47-5aff-08dc173f9a85");
            songService.DeleteSong(songIdToDelete);


            // Call the FetchAll method to retrieve all songs
            var availableSongsAfterDelete = songService.FetchAllSongs();
        }
    }
}
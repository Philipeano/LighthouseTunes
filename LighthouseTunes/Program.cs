using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Common.Enums;
using LighthouseTunes.Data.Entities;
using LighthouseTunes.Data.Services;
using System.Runtime.ConstrainedExecution;

namespace LighthouseTunes
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // CRUD OPERATIONS FOR USER

            IUserService userService = new UserService();
            var availableUsers = new List<UserDTO>();

            // Populate the Users table with a few records
            var user1 = new UserDTO() { UserId = new Guid(), FirstName = "Simon", LastName = "Kolawole", EmailAddress = "s.kolawole@gmail.com" };
            var user2 = new UserDTO() { UserId = new Guid(), FirstName = "Donald", LastName = "Trump", EmailAddress = "d.trump@hotmail.com" };
            var user3 = new UserDTO() { UserId = new Guid(), FirstName = "Lucas", LastName = "Podoski", EmailAddress = "l.podoski@yahoo.com" };

            userService.CreateUser(user1);
            userService.CreateUser(user2);
            userService.CreateUser(user3);

            // Retrieve all users
            availableUsers = userService.FetchAllUsers();


            // CRUD OPERATIONS FOR SONG

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
                ReleaseDate = new DateTime(2023, 12, 1)
            };
            songService.UpdateSong(songIdToUpdate, songWithNewInfo);

            // Call the FetchAll method to retrieve all songs
            var availableSongsAfterUpdate = songService.FetchAllSongs();


            // Call DeleteSong
            var songIdToDelete = new Guid("3b4a92c9-cafe-4c47-5aff-08dc173f9a85");
            songService.DeleteSong(songIdToDelete);


            // Call the FetchAll method to retrieve all songs
            var availableSongsAfterDelete = songService.FetchAllSongs();


            // CRUD OPERATIONS FOR FAVOURITE

            IFavouriteService favouriteService = new FavouriteService();
            var availableFavourites = new List<FavouriteDTO>();


            // Identify the users who want to favourite songs
            Guid addedById1 = new Guid("D4ECB57F-8CE9-4D1C-18CD-08DC1BEEEB00");  // Simon Kolawole
            Guid addedById2 = new Guid("BB6E1E9B-1BDD-4B72-18CE-08DC1BEEEB00");  // Donald Trump

            // Identify the songs to be marked as favourites
            Guid selectedSongId1 = new Guid("81A70DD3-92F5-4FE6-5B00-08DC173F9A85"); // IDK by Wizkid
            Guid selectedSongId2 = new Guid("7F1BB643-F0A1-41E8-5B02-08DC173F9A85"); // Funny Identity by Oliver De Coque

            // Mark some songs as favourites for some users
            favouriteService.CreateFavourite(addedById1, selectedSongId2);
            favouriteService.CreateFavourite(addedById2, selectedSongId1);
            favouriteService.CreateFavourite(addedById2, selectedSongId2);


            // Fetch all favourites
            availableFavourites = favouriteService.FetchAllFavourites();


            // Fetch favourites with filter
            var filteredFavourites1 = favouriteService.FetchFavouritesWithFilter(null, null);
            var filteredFavourites2 = favouriteService.FetchFavouritesWithFilter("IDK", "");
            var filteredFavourites3 = favouriteService.FetchFavouritesWithFilter("Funny", "Simon");

            // Delete a favourite
            var favouriteIdToDelete = new Guid("FC36A1AD-88D2-4D5A-9C6E-08DC1BF212C6");
            favouriteService.DeleteFavourite(favouriteIdToDelete);

        }
    }
}
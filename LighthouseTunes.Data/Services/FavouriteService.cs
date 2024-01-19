using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LighthouseTunes.Data.Extensions;

namespace LighthouseTunes.Data.Services
{
    public interface IFavouriteService
    {
        void CreateFavourite(Guid userId, Guid songId);

        List<FavouriteDTO> FetchAllFavourites();

        List<FavouriteDTO> FetchFavouritesWithFilter(string titleFilter, string userFilter);

        void DeleteFavourite(Guid id);
    }


    public class FavouriteService : IFavouriteService
    {
        private readonly LighthouseTunesDbContext _context = new LighthouseTunesDbContext();
        private List<FavouriteDTO> _favourites;

        public void CreateFavourite(Guid userId, Guid songId)
        {
            // TODO:
            // Validate the arguments (userId and songId) to be sure the records exist in the DB

            var user = _context.Users.Find(userId);
            var song = _context.Songs.Find(songId);

            var newFavourite = new Favourite
            {
                Id = new Guid(),
                DateAdded = DateTime.Now,
                SelectedSong = song,
                AddedBy = user
                
                // NOTE:
                // The FK props (SelectedSongId and AddedById) need not be assigned directly like the others,
                // since their associated navigaton objects have been assigned above
            };
            _context.Favourites.Add(newFavourite);
            _context.SaveChanges();
        }


        public void DeleteFavourite(Guid id)
        {
            // TODO:
            // Validate the given Id to be sure a favourite with that key exists in the DB

            var favouriteToDelete = _context.Favourites.Find(id);
            _context.Favourites.Remove(favouriteToDelete);
            _context.SaveChanges();
        }


        public List<FavouriteDTO> FetchAllFavourites()
        {
            return _context.Favourites
                .Include(f => f.AddedBy) // Fetch the entire object representing the user
                .Include(f => f.SelectedSong)  // Fetch the entire object representing the song
                .Select(f => new FavouriteDTO
            {
                Id = f.Id,
                DateAdded = f.DateAdded,
                SelectedSong = f.SelectedSong.ToSongDTO(),
                AddedBy = f.AddedBy.ToUserDTO(),
                SelectedSongId = f.SelectedSongId,
                AddedById = f.AddedById
            }).ToList();
        }

        public List<FavouriteDTO> FetchFavouritesWithFilter(string titleFilter, string userFilter)
        {
            // First, retrieve all available favourites
            _favourites = FetchAllFavourites();

            // Next, apply titleFilter if available
            if (!string.IsNullOrWhiteSpace(titleFilter))
                _favourites = _favourites
                    .Where(f => f.SelectedSong.Title.Contains(titleFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            // Next, apply userFilter if available
            if (!string.IsNullOrWhiteSpace(userFilter))
                _favourites = _favourites.
                    Where(f => f.AddedBy.FirstName.Contains(userFilter, StringComparison.OrdinalIgnoreCase)
                            || f.AddedBy.LastName.Contains(userFilter, StringComparison.OrdinalIgnoreCase))
                    .ToList();

            return _favourites;
        }
    }
}

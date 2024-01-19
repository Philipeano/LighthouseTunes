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
    public interface IUserService
    {
        void CreateUser(UserDTO userInfo);

        List<UserDTO> FetchAllUsers();

        List<UserDTO> FetchUsersWithFilter(string firstNameFilter, string lastNameFilter);

        void UpdateUser(UserDTO userWithNewInfo);

        void DeleteUser(Guid userId);
    }

    public class UserService : IUserService
    {
        private readonly LighthouseTunesDbContext _context = new LighthouseTunesDbContext();
        private List<UserDTO> _users;

        public void CreateUser(UserDTO userInfo)
        {
            // TODO: Validate all the props in the 'userInfo' argument before usage
            var newUser = new User()
            {
                UserId = new Guid(),
                FirstName = userInfo.FirstName,
                LastName = userInfo.LastName,
                EmailAddress = userInfo.EmailAddress,
            };
            _context.Users.Add(newUser);
            _context.SaveChanges();
        }

        public void DeleteUser(Guid userId)
        {
            // Attempt retrieving a user with the given Id
            var userToDelete = _context.Users.Find(userId);

            // No match found. Exit the method.
            if (userToDelete == null)
            {
                Console.WriteLine("Could not find a user with the given Id.");
                return;
            }

            // A match was found. Perform the deletion.
            _context.Users.Remove(userToDelete);
            _context.SaveChanges();
        }

        public List<UserDTO> FetchAllUsers()
        {
            return _context.Users.Select(u => new UserDTO {
                UserId = u.UserId,
                FirstName = u.FirstName,
                LastName = u.LastName,
                EmailAddress = u.EmailAddress
            }).ToList();
        }

        public List<UserDTO> FetchUsersWithFilter(string firstNameFilter, string lastNameFilter)
        {
            // First, retrieve all users available
            _users = FetchAllUsers();

            // Next, apply firstNameFilter if available
            if (!string.IsNullOrWhiteSpace(firstNameFilter))
                _users = _users.Where(u => u.FirstName.Contains(firstNameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            // Next, apply lastNameFilter if available
            if (!string.IsNullOrWhiteSpace(lastNameFilter))
                _users = _users.Where(u => u.LastName.Contains(lastNameFilter, StringComparison.OrdinalIgnoreCase)).ToList();

            return _users;
        }

        public void UpdateUser(UserDTO userWithNewInfo)
        {
            // Verify that UserId prop in 'userWithNewInfo' has a value
            if(!userWithNewInfo.UserId.HasValue || userWithNewInfo.UserId == Guid.Empty)
            {
                Console.WriteLine("No user Id specified for the update.");
                return;
            }

            // Attempt retrieving a user with the given Id
            var userToUpdate = _context.Users.Find(userWithNewInfo.UserId);

            // No match found. Exit the method.
            if (userToUpdate == null)
            {
                Console.WriteLine("Could not find a user with the given Id.");
                return;
            }

            // A match was found. Perform the update.
            if (!string.IsNullOrWhiteSpace(userWithNewInfo.FirstName))
                userToUpdate.FirstName = userWithNewInfo.FirstName;

            if (!string.IsNullOrWhiteSpace(userWithNewInfo.LastName))
                userToUpdate.LastName = userWithNewInfo.LastName;

            if (!string.IsNullOrWhiteSpace(userWithNewInfo.EmailAddress))
                userToUpdate.EmailAddress = userWithNewInfo.EmailAddress;

            _context.SaveChanges();
        }
    }
}

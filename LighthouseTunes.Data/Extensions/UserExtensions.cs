using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Extensions
{
    public static class UserExtensions
    {
        // An extension method to generate a UserDTO from a User entity
        public static UserDTO ToUserDTO(this User userEntity)
        {
            return new UserDTO
            {
                UserId = userEntity.UserId,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                EmailAddress = userEntity.EmailAddress,
            };
        }

        // An extension method to generate a User entity from a UserDTO
        public static User ToUserEntity(this UserDTO userDTO)
        {
            return new User
            {
                UserId = userDTO.UserId == null ? Guid.Empty : userDTO.UserId.Value,
                FirstName = userDTO.FirstName,
                LastName = userDTO.LastName,
                EmailAddress = userDTO.EmailAddress
            };
        }
    }
}

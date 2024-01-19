using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Common.DTOs
{
    public record UserDTO
    {
        public Guid? UserId { get; set; }  // Empty when performing Create but has a value when performing Fetch/Update

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string EmailAddress { get; set; }
    }
}

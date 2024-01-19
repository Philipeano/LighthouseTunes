using LighthouseTunes.Common.DTOs;
using LighthouseTunes.Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LighthouseTunes.Data.Extensions
{
    public static class SongExtensions
    {
        // An extension method to generate a SongDTO from a Song entity
        public static SongDTO ToSongDTO(this Song songEntity)
        {
            return new SongDTO
            {
                Title = songEntity.Title,
                Album = songEntity.Album,
                Artist = songEntity.Artist,
                Featuring = songEntity.Featuring,
                Genre = songEntity.Genre,
                ReleaseDate = songEntity.ReleaseDate,
            };
        }


        // An extension method to generate a Song entity from a SongDTO
        public static Song ToSongEntity(this SongDTO songDTO)
        {
            return new Song
            {
                Title = songDTO.Title,
                Album = songDTO.Album,
                Artist = songDTO.Artist,
                Featuring = songDTO.Featuring,
                Genre = songDTO.Genre,
                ReleaseDate = songDTO.ReleaseDate,
            };
        }
    }
}

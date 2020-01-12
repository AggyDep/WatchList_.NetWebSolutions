using API.DTOs.Genre;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IGenreRepository
    {
        Task<IEnumerable<GenreDTO>> GetGenres();
        Task<GenreDTO> GetGenre(int id);
        Task<GenrePostDTO> PostGenre(GenrePostDTO genrePostDTO);
        Task<GenreDTO> PutGenre(int id, GenreDTO genreDTO);
        Task<GenreDTO> DeleteGenre(int id);
    }
}

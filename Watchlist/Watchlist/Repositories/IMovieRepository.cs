using API.DTOs.Movie;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Repositories
{
    public interface IMovieRepository
    {
        Task<IEnumerable<MovieGetDTO>> GetMovies();
        Task<MovieDTO> GetMovie(int id);
        Task<MoviePostDTO> PostMovie(MoviePostDTO MoviePostDTO);
        Task<MovieDTO> PutMovie(int id, MovieDTO MovieDTO);
        Task<MovieDeleteDTO> DeleteMovie(int id);
    }
}

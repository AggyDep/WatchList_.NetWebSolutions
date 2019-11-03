using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPI.DTOs;
using WebAPI.DTOs.SerieMovie;

namespace WebAPI.Repositories
{
    public interface ISerieMovieRepository
    {
        Task<IEnumerable<SerieMovieDTO>> GetSerieMovies();
        Task<SerieMovieDTO> GetSerieMovie(int id);
        Task<SerieMoviePostDTO> PostSerieMovie(SerieMoviePostDTO serieMoviePostDTO);
        Task<SerieMovieDTO> PutSerieMovie(int id, SerieMovieDTO serieMovieDTO);
        Task<SerieMovieDeleteDTO> DeleteSerieMovie(int id);
    }
}

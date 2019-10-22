﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPI.DTOs
{
    public class MoviePutDTO
    {
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Synopsis { get; set; }
        public string Background { get; set; }
        public string Producer { get; set; }
        [Required]
        public string Director { get; set; }
        [Required]
        public string Status { get; set; }
        [Required]
        public string Aired { get; set; }
        [Required]
        public string Duration { get; set; }
        public float Score { get; set; }
        public int Ranking { get; set; }
        public int Member { get; set; }
        public string Image { get; set; }
        public ICollection<SerieMovieGenreDTO> SerieMovieGenreDTOs { get; set; }
        public ICollection<SerieMovieActorDTO> SerieMovieActorDTOs { get; set; }
        public ICollection<WatchListDTO> WatchListDTOs { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.Entidades
{
    public class PeliculaGenero
    {
        public int GeneroId { get; set; }
        public int PeliculaId { get; set; }
        public Genero Genero { get; set; }
        public Pelicula Pelicula { get; set; }
    }
}

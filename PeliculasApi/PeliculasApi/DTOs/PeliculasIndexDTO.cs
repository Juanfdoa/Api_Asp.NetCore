using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class PeliculasIndexDTO
    {
        public List<PeliculaDTO> Estrenos { get; set; }
        public List<PeliculaDTO> EnCines { get; set; }
    }
}

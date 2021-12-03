using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class PaginacionDTO
    {
        public int Pagina { get; set; } = 1;
        public int cantidadRegistrosPorPagina { get; set; } = 10;

        private readonly int CantidadMaximaPorPagina = 50;

        public int CantidadRegistrosPorPagina
        {
            get { return cantidadRegistrosPorPagina; }
            set { cantidadRegistrosPorPagina = (value > CantidadMaximaPorPagina) ? CantidadMaximaPorPagina : value; }
        }
    }
}

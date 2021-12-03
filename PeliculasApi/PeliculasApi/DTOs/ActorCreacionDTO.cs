using Microsoft.AspNetCore.Http;
using PeliculasApi.Validaciones;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PeliculasApi.DTOs
{
    public class ActorCreacionDTO : ActorPatchDTO
    {
        
        [PesoArchivoValidacion(4)]
        [TipoArchivoValidacion(GrupoTipoArchivo.imagen)]
        public IFormFile Foto { get; set; }
        
    }
}

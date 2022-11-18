using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorAlumnos.Shared.Dtos.Alumnos
{
    public class AlumnoDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage ="El campo {0} es requerido")]
        public string Nombre { get; set; }

        [Required]
        public string Matricula { get; set; }

        public string Foto { get; set; }
    }
}

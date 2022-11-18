using BlazorAlumnos.Server.Model;
using BlazorAlumnos.Server.Model.Entities;
using BlazorAlumnos.Shared.Dtos.Alumnos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BlazorAlumnos.Server.Controllers
{
    [ApiController]
    [Route("api/alumnos")]
    //[Route("api/[Controller]")]
    public class AlumnosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public AlumnosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<AlumnoDTO>>> Get()
        {
            var alumnos = await context.Alumnos.ToListAsync();

            var alumnosDto = new List<AlumnoDTO>(); 

            foreach(var alumno in alumnos)
            {
                var alumnoDTO = new AlumnoDTO();    
                alumnoDTO.Id = alumno.Id;
                alumnoDTO.Nombre = alumno.Nombre;
                alumnoDTO.Foto = "Foto del Alumno";
                alumnoDTO.Matricula = alumno.Matricula;

                alumnosDto.Add(alumnoDTO);  
            }

            return alumnosDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<AlumnoDTO>> Get(int id)
        {
            var alumno = await context.Alumnos.FirstOrDefaultAsync(x => x.Id == id);

            if (alumno == null)
            {
                return NotFound();
            }

            var alumnoDto = new AlumnoDTO();
            alumnoDto.Id = alumno.Id;
            alumnoDto.Nombre = alumno.Nombre;
            alumnoDto.Matricula = alumno.Matricula;

            return alumnoDto;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] AlumnoDTO alumnoDto)
        {
            var alumno = new Alumno();   
            alumno.Matricula = alumnoDto.Matricula;
            alumno.Nombre = alumnoDto.Nombre;

            context.Alumnos.Add(alumno);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] AlumnoDTO alumnoDto)
        {
            var alumnoDb = await context.Alumnos.FirstOrDefaultAsync(x => x.Id == alumnoDto.Id);

            if (alumnoDb == null)
            {
                return NotFound();
            }

            alumnoDb.Nombre = alumnoDb.Nombre;

            context.Alumnos.Update(alumnoDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id: int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var alumnoDb = await context.Alumnos.FirstOrDefaultAsync(x => x.Id == id);

            if (alumnoDb == null)
            {
                return NotFound();
            }

            context.Alumnos.Remove(alumnoDb);
            await context.SaveChangesAsync();
            return Ok();
        }

    }
}

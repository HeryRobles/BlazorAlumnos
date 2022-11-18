using BlazorAlumnos.Server.Model;
using BlazorAlumnos.Server.Model.Entities;
using BlazorAlumnos.Shared.Dtos.Materias;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace BlazorAlumnos.Server.Controllers
{
    [ApiController, Route("api/[Controller]")]
    public class MateriasController : ControllerBase 
    {
        private readonly ApplicationDbContext context;

        public MateriasController(ApplicationDbContext context)
        {
            this.context = context; 

        }

        [HttpGet]   
        public async Task<ActionResult<List<MateriaDTO>>> Get()
        {
            var materias = await context.Materias.ToListAsync();

            var materiasDto = new List<MateriaDTO>();    

            foreach(var materia in materias)
            {
                var materiaDto = new MateriaDTO();
                materiaDto.Id = materia.Id;
                materiaDto.Nombre = materia.Nombre;

                materiasDto.Add(materiaDto);
               
            }

            return materiasDto;
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<MateriaDTO>> Get(int id)
        {
            var materia = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (materia == null)
            {
                return NotFound();
            }

            var materiaDto = new MateriaDTO();
            materiaDto.Id = materia.Id;
            materiaDto.Nombre = materia.Nombre;
            

            return materiaDto;
        }

        [HttpPost]
        public async Task<ActionResult> Add([FromBody] MateriaDTO materiaDto)
        {
            var materia = new Materia();
            materia.Id = materiaDto.Id; 
            materia.Nombre = materiaDto.Nombre; 
          

            context.Materias.Add(materia);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpPut]
        public async Task<ActionResult> Edit([FromBody] MateriaDTO materiaDto)
        {
            var materiaDb = await context.Materias.FirstOrDefaultAsync(x => x.Id == materiaDto.Id);

            if (materiaDb == null)
            {
                return NotFound();
            }

            materiaDb.Nombre = materiaDb.Nombre;

            context.Materias.Update(materiaDb);
            await context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var materiaDb = await context.Materias.FirstOrDefaultAsync(x => x.Id == id);

            if (materiaDb == null)
            {
                return NotFound();
            }

            context.Materias.Remove(materiaDb);
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

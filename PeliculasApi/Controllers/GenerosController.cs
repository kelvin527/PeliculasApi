using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.JsonPatch;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Dtos;
using PeliculasApi.Entidades;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GenerosController : ControllerBase
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public GenerosController(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name ="obtenerGenero")]
        public async Task<ActionResult<List<GenerosDto>>> GetAll()
        {
            var genero =await context.Generos.ToListAsync();
            var listado=mapper.Map<List<GenerosDto>>(genero);
            return listado;
        }

        [HttpGet("{id:int}", Name ="Generoid")]
        public async Task<ActionResult<GenerosDto>> GetId(int id)
        {
            var generoid= await context.Generos.FirstOrDefaultAsync(x=> x.Id==id);
            if(generoid is null) { return NoContent(); }

            var genero=mapper.Map<GenerosDto>(generoid);
            return genero;
        }

        [HttpPost(Name ="agregarGenero")]
        public async Task<ActionResult> Post([FromBody]AgregarGeneroDto agregarGenerodto)
        {
            var agregar = mapper.Map<Genero>(agregarGenerodto);
            context.Add(agregar);
            await context.SaveChangesAsync();
            //aqui devolvemos al cliente la entidad con sus datos 
            var generodto = mapper.Map<GenerosDto>(agregar);
            return CreatedAtAction("Generoid", new { id = generodto.Id }, generodto);

          
        }

        [HttpPut("{id:int}", Name ="actualizarGenero")]
        public async Task<ActionResult> Put(int id, AgregarGeneroDto agregardto)
        {
            //var existe = await context.Generos.AnyAsync(x => x.Id == id);
            //if(!existe) { return BadRequest("no existe este genero"); }

            var agregar= mapper.Map<Genero>(agregardto);
            agregar.Id = id;
            context.Entry(agregar).State= EntityState.Modified;
            await context.SaveChangesAsync();

            return Ok();
        }

        [HttpPatch("{id:int}", Name = "actulizarPatch")]
        public async Task<ActionResult> Patch(int id, JsonPatchDocument<AgregarGeneroDto> jsonPatch)
        {
            if(jsonPatch is null) { return BadRequest("no puede ser nulo"); }

            var verificar = await context.Generos.FirstOrDefaultAsync(x => x.Id == id);
            if(verificar is null) { return BadRequest("el id no existe"); }

            var contenieId=mapper.Map<AgregarGeneroDto>(verificar);
            jsonPatch.ApplyTo(contenieId, ModelState);

            var esValido = TryValidateModel(verificar);
            if(!esValido) { return BadRequest(ModelState); }

            mapper.Map(contenieId, verificar);
            await context.SaveChangesAsync();

            return Ok();

        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult> Delete(int id)
        {
            var verificar= await context.Generos.AnyAsync(x=> x.Id == id);
            context.Remove(new Genero { Id= id });
            await context.SaveChangesAsync();
            return Ok();
        }
    }
}

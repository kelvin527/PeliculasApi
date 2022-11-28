using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApi.Dtos;
using PeliculasApi.Entidades;
using PeliculasApi.Migrations;

namespace PeliculasApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActoresController : ControllerBase
    {
        private readonly Context context;
        private readonly IMapper mapper;

        public ActoresController(Context context, IMapper mapper)
        {
            this.context = context;
            this.mapper = mapper;
        }

        [HttpGet(Name ="listadoDeAutore")]
        public async Task<ActionResult<List<ActorDto>>> GetAll()
        {
            var lista = await context.Actors.ToListAsync();
            return mapper.Map<List<ActorDto>>(lista);
        }

        [HttpGet("{id:int}", Name ="actorId")]
        public async Task<ActionResult<ActorDto>> GetId(int id)
        {
            var verificar = await context.Actors.AnyAsync(x => x.Id == id);
            if (!verificar) {return NotFound();}

            return mapper.Map<ActorDto>(verificar);
        }

        [HttpPost(Name ="agregarActor")]
        public async Task<ActionResult> Post(AgregarActorDto agregarActor)
        {
            var verificar = await context.Actors.Where(x => x.Nombre == agregarActor.Nombre).AnyAsync();
            if (verificar) { return NotFound("este nombe ya existe");}

            var actormap= mapper.Map<Actor>(agregarActor);
            context.Actors.Add(actormap);
            await context.SaveChangesAsync();

            var dto = mapper.Map<ActorDto>(actormap);

            return CreatedAtAction("actorId", new {id=actormap.Id}, dto);
        }

        [HttpPut("{id:int}", Name ="actulizarActor")]
        public async Task<ActionResult> Put(int id, AgregarActorDto agregarActor)
        {
            var verificar= await context.Actors.AnyAsync(x=> x.Id==id);
            if(!verificar) { return NotFound(); }

            var actulizado = mapper.Map<Actor>(agregarActor);
            actulizado.Id = id;

            context.Entry(actulizado).State = EntityState.Modified;
            await context.SaveChangesAsync();

            return NoContent();
        }

        [HttpDelete("{id:int}", Name ="borrarActor")]
        public async Task<ActionResult> Delete(int id)
        {
            var verificar = await context.Actors.AnyAsync(c => c.Id == id);
            if(!verificar) { return NotFound();}

            context.Remove( new Actor { Id = id});
            await context.SaveChangesAsync();

            return NoContent();
        }
    }
}

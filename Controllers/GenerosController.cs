using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PeliculasApii.DTOs;
using PeliculasApii.Entidades;
using PeliculasApii.Utilidades;

namespace PeliculasApii.Controllers
{
    //endPoinds
    [Route("api/generos")]
    [ApiController]
    public class GenerosController : ControllerBase //Heradamos la clase ControllerBase para tener acceso a ciertos metodos auxiliares
    {
        //Asinacion como campo (WeatherForecastController. GenerosController, etc)

        private readonly ILogger<GenerosController> logger;
        private readonly ApplicationDbContext context;
        private readonly IMapper mapper;

        public GenerosController(ILogger<GenerosController> logger,
            ApplicationDbContext context,
            IMapper mapper) // ,ApplicationDbContext context. Agregar despues de update-database. finlamente crear y asignar como campo context: ctrl . in up context
        {
            this.logger = logger;
            this.context = context;
            this.mapper = mapper;
        }

       //Justo antes de utilizar paginacion
        /*public async Task<ActionResult<List<GeneroDTO>>> Get()
        {
            var generos = await context.Generos.ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);

        }*/

        //Podemos definir multiples endPoinds por accion:
        [HttpGet]
        //Acciones. Responden cuando se le haga una peticion http al endPoind
        public async Task<ActionResult<List<GeneroDTO>>> Get([FromQuery] PaginacionDTO paginacionDTO)
        {
            var queryable = context.Generos.AsQueryable();
            await HttpContext.InsertarParametrosPaginacionEnCabecera(queryable);
            var generos = await queryable.OrderBy(x => x.Nombre).Paginar(paginacionDTO).ToListAsync();
            return mapper.Map<List<GeneroDTO>>(generos);

        }

        [HttpGet("{Id:int}")]
        public async Task<ActionResult<Genero>> Get(int Id)
        {
            throw new NotImplementedException();
        }

        [HttpPost]
        public async Task<ActionResult> Post([FromBody] GeneroCreacionDTO generoCreacionDTO)
        {
            var genero = mapper.Map<Genero>(generoCreacionDTO);
            context.Add(genero);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut]
        public ActionResult Put([FromBody] Genero genero)
        {
            throw new NotImplementedException();
        }   


        [HttpDelete]
        public ActionResult Delete()
        {
            throw new NotImplementedException();
        }

    }
}



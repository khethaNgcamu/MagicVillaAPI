using MagicVillaApi.Data;
using MagicVillaApi.Models;
using MagicVillaApi.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace MagicVillaApi.Controllers
{
    [Route("Api/VillaApi")]
    [ApiController]
    public class VillaApiController : ControllerBase
    {
        private readonly DataContext context;

        public VillaApiController(DataContext context)
        {
            this.context = context;
        }
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult> GetVillas()
        {
            List<Villa> villas = await context.Villas.ToListAsync();
            await context.SaveChangesAsync();   
            return Ok(villas);
        }

        [HttpGet("Id")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]

        public async Task<IActionResult> GetVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }

            var villa = await context.Villas.FirstOrDefaultAsync(u => u.Id == id);
            if (villa == null)
            {
                return NotFound();
            }
            await context.SaveChangesAsync();
            return Ok(villa);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CreateVilla([FromBody]VillaDto villa)
        {
            if(villa == null)
            {
                return BadRequest(villa);
            }

           // if(villa.Id > 0)
           // {
           //     return StatusCode(StatusCodes.Status500InternalServerError);
           // }
           //villa.Id = context.Villas.OrderByDescending(u => u.Id).FirstOrDefault().Id + 1;
            Villa villa1 = new Villa();
            villa1.Id = villa.Id;
            villa1.Name = villa.Name;
            villa1.Details = villa.Details;
            villa1.Rate = villa.Rate;
            villa1.Sqft = villa.Sqft;
            villa1.Occupancy = villa.Occupancy;
            villa1.ImageUrl = villa.ImageUrl;
            villa1.Amenity = villa.Amenity;

            await context.Villas.AddAsync(villa1);
            await context.SaveChangesAsync();
            //return Ok(villa);
            return CreatedAtRoute("GetVilla", new { id = villa.Id }, villa);
        }
        [HttpDelete("{id:int}", Name = "DeleteVilla")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType (StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeleteVilla(int id)
        {
            if(id == 0)
            {
                return BadRequest();
            }
            var checkedVilla = await context.Villas.FirstOrDefaultAsync(v => v.Id == id);
            if(checkedVilla == null)
            {
                return NotFound();
            }
            context.Villas.Remove(checkedVilla);
            await context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPut("{id:int}", Name = "UpdateVilla")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> UpdateVilla(int id, [FromBody]VillaDto villaDto)
        {
            if(villaDto == null || id != villaDto.Id)
            {
                return BadRequest();
            }
            var modelVilla = await context.Villas.FirstOrDefaultAsync(u => u.Id == id);
            modelVilla.Id = villaDto.Id;
            modelVilla.Name = villaDto.Name;
            modelVilla.Details = villaDto.Details;
            modelVilla.Rate = villaDto.Rate;
            modelVilla.Sqft = villaDto.Sqft;
            modelVilla.Occupancy = villaDto.Occupancy;
            modelVilla.ImageUrl = villaDto.ImageUrl;
            modelVilla.Amenity = villaDto.Amenity;

            await context.SaveChangesAsync();

            return NoContent();

        }
    }
}

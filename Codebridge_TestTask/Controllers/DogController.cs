using Codebridge_TestTask.Models;
using Codebridge_TestTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;

namespace Codebridge_TestTask.Controllers
{
    [Route("/")]
    [ApiController]
    public class DogController : ControllerBase
    {
        private readonly IDogService _dogService;

        public DogController(IDogService _dogService)
        {
            this._dogService = _dogService;
        }

        [HttpGet("ping")]
        public IActionResult GetPing()
        {
            return Ok(_dogService.GetPing());
        }

        [HttpPost("dog")]
        public async Task<IActionResult> AddDog(DogPOST dog_post)
        {
            if (await _dogService.CheckName(dog_post.Name) != null)
            {
                ModelState.AddModelError("Name", "Dog with such name already exists");
            }

            if(_dogService.CheckNegative(dog_post.TailLength))
            {
                ModelState.AddModelError("TailLength", "Can't be negative tail length");
            }
            bool vari = _dogService.CheckNegative(dog_post.Weight);

            if (_dogService.CheckNegative(dog_post.Weight))
            {
                ModelState.AddModelError("TailLength", "Can't be negative weight");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _dogService.AddDog(dog_post);

            return Created("/dog", dog_post);
        }

        [HttpGet("dogs")]
        public async Task<IActionResult> GetDogs()
        {
            return Ok(await _dogService.GetDogs());
        }

        [HttpGet("psdogs")]
        public async Task<IActionResult> GetPaginationSortedDogs(string attribute, string order, int pageNumber, int limit)
        {
            return Ok(await _dogService.PaginationSortDogs(attribute, order, pageNumber, limit));
        }
    }
}

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Data.Entities;
using URLShortener.Business.Interfaces;

namespace URLShortener.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LinkController : ControllerBase
    {
        private readonly ILinkService _linkService;
        private readonly IMapper _mapper;

        public LinkController(ILinkService linkService, IMapper mapper)
        {
            _linkService = linkService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> CreateLinkAsync(string linkOriginal)
        {
            await _linkService.CreateLinkAsync(linkOriginal);
            return Ok();
        }

        [HttpGet("GetOriginalByShortened")]
        public async Task<IActionResult> GetOriginalLinkByShortenedURLAsync(string shortenedURL)
        {
            var link = await _linkService.GetOriginalLinkByShortenedURLAsync(shortenedURL);
            return Ok(link);
        }

        [HttpGet]
        public async Task<IActionResult> GetLinkByIdAsync(int id)
        {
            var link = await _linkService.GetLinkByIdAsync(id);
            return Ok(link);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteLinkByIdAsync(int id)
        {
            await _linkService.DeleteLinkByIdAsync(id);
            return Ok();
        }
    }
}

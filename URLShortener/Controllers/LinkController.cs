using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using URLShortener.Data.Entities;
using URLShortener.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;

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
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> CreateLinkAsync(string linkOriginal)
        {
            try
            {
                await _linkService.CreateLinkAsync(linkOriginal);
                return Ok();
            }
            catch (InvalidDataException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("GetOriginalByShortened")]
        public async Task<IActionResult> GetOriginalLinkByShortenedURLAsync(string shortenedURL)
        {
            try
            {
                var link = await _linkService.GetOriginalLinkByShortenedURLAsync(shortenedURL);
                return Ok(link);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> GetLinkByIdAsync(int id)
        {
            try
            {
                var link = await _linkService.GetLinkByIdAsync(id);
                return Ok(link);
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet("GetAll")]
        public async Task<IActionResult> GetAllLinksAsync()
        {
            var links = await _linkService.GetAllLinksAsync();
            return Ok(links);
        }

        [HttpDelete]
        [Authorize(Roles = "Admin, User")]
        public async Task<IActionResult> DeleteLinkByIdAsync(int id)
        {
            try
            {
                await _linkService.DeleteLinkByIdAsync(id);
                return Ok();
            }
            catch (NullReferenceException ex)
            {
                return NotFound(ex.Message);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Unauthorized(ex.Message);
            }
        }
    }
}

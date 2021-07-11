using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iread_interaction_ms.Web.Dto.CommentDto;
using iread_interaction_ms.Web.Util;
using iread_interaction_ms.Web.DTO.Story;
using iread_interaction_ms.Web.DTO.StoryDto;
using iread_interaction_ms.Web.Dto;
using iread_interaction_ms.DataAccess.Data.Type;

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class HighLightController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly HighLightService _highLightService;
        private readonly InteractionsService _interactionServices;
        private readonly IConsulHttpClientService _consulHttpClient;

        public HighLightController(HighLightService highLightService, IMapper mapper, InteractionsService interactionServices, IConsulHttpClientService consulHttpClient)
        {
            _highLightService = highLightService;
            _mapper = mapper;
            _interactionServices = interactionServices;
            _consulHttpClient = consulHttpClient;
        }
        
        // GET: api/interaction/highLight/1/get
        [HttpGet("{id}/get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            HighLight highLight = await _highLightService.GetById(id);

            if (highLight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HighLightDto>(highLight));
        }

        // GET: api/interaction/highLight/get-by-interaction/1
        [HttpGet("get-by-interaction/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute]int id)
        {
            HighLight highLight = await _highLightService.GetByInteractionId(id);

            if (highLight == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<HighLightDto>(highLight));
        }


        //POST: api/interaction/drawing/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] HighLightCreateDto highLightCreateDto)
        {
            if (highLightCreateDto == null)
            {
                return BadRequest();
            }

            HighLight highLight = _mapper.Map<HighLight>(highLightCreateDto);
            ValidationLogicForAdding(highLight);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

           
            if (!_highLightService.Insert(highLight))
            {
                return BadRequest();
            }
            return CreatedAtAction("GetById", new { id = highLight.HighLightId }, _mapper.Map<HighLightDto>(highLight));
        }


        [HttpPut("{id}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] HighLightUpdateDto highLight, [FromRoute] int id)
        {
             if (highLight == null)
            {
                return BadRequest();
            }            
            
            HighLight oldHighLight = _highLightService.GetById(id).Result;
             if (oldHighLight == null)
            {
                return NotFound();
            }

            HighLight highLightEntity = _mapper.Map<HighLight>(highLight);
            highLightEntity.HighLightId = id;
            _highLightService.Update(highLightEntity, oldHighLight);
            return NoContent();
        }



        // DELETE: api/interaction/highLight/5/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            var highLight = _highLightService.GetById(id).Result;
            if (highLight == null)
            {
                return NotFound();
            }

           _highLightService.Delete(highLight);
            return NoContent();
        }


    private void ValidationLogicForAdding(HighLight highLight)
    {

        ViewStoryDto storyDto = _consulHttpClient.GetAsync<ViewStoryDto>("story_ms", $"/api/story/get/{highLight.Interaction.StoryId}").Result;

        if(storyDto == null || storyDto.StoryId < 1){
             ModelState.AddModelError("StoryId", "Story not found");    
        }

        PageDto pageDto = _consulHttpClient.GetAsync<PageDto>("story_ms", $"/api/story/Page/get/{highLight.Interaction.PageId}").Result;

        if(pageDto == null || pageDto.PageId < 1){
             ModelState.AddModelError("PageId", "Page not found");    
        }

        UserDto userDto = _consulHttpClient.GetAsync<UserDto>("identity_ms", $"/api/identity_ms/SysUsers/{highLight.Interaction.StudentId}/get").Result;

        if(userDto == null || string.IsNullOrEmpty(userDto.Id)){
             ModelState.AddModelError("StudentId", "Student not found");    
        }
        else
        {
            if(!userDto.Role.Equals(RoleTypes.Student.ToString()))
            {
             ModelState.AddModelError("StudentId", "User not a student");    
            }
        }        
    }

    }


    
}
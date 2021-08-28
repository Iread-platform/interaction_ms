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
using System.Collections.Generic;
using System.Linq;
using iread_interaction_ms.Web.Dto.ReadingDto;
using System;
using Microsoft.AspNetCore.Authorization;

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class ReadingController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ReadingService _readingService;
        private readonly InteractionsService _interactionServices;
        private readonly IConsulHttpClientService _consulHttpClient;

        public ReadingController(ReadingService readingService, IMapper mapper, InteractionsService interactionServices, IConsulHttpClientService consulHttpClient)
        {
            _readingService = readingService;
            _mapper = mapper;
            _interactionServices = interactionServices;
            _consulHttpClient = consulHttpClient;
        }

        [HttpGet("{id}/get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Reading reading = await _readingService.GetById(id);

            if (reading == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ReadingDto>(reading));
        }

        [HttpGet("get-by-interaction/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute] int id)
        {
            Reading reading = await _readingService.GetByInteractionId(id);

            if (reading == null)
            {
                return NotFound();
            }
            reading.TimeSpent = reading.TimeSpent.Duration();

            return Ok(_mapper.Map<ReadingDto>(reading));
        }

        [HttpGet("get-by-page/{pageId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPageId([FromRoute] int pageId)
        {
            List<Reading> readings = await _readingService.GetByPageId(pageId);

            if (readings == null || !readings.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<ReadingDto>>(readings));
        }


        [HttpGet("my-reading-stories-and-progress/{studentId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMyReadingStoriesAndProgress([FromRoute] string studentId)
        {
            List<ReadingWithProgressDto> readings = await _readingService.GetMyReadingStoriesAndProgress(studentId);

            if (readings == null || !readings.Any())
            {
                return NotFound();
            }
            await AddStoryDetialsToReading(readings);
            return Ok(readings);
        }

        [HttpGet("my-reading-stories")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [Authorize]
        public async Task<IActionResult> GetMyReadingStoryIds()
        {
            string myId = User.Claims.Where(c => c.Type == "sub")
                            .Select(c => c.Value).SingleOrDefault();
            List<StoryDto> readingsStoryIds = await _readingService.GetReadingStoryIds(myId);
            if (readingsStoryIds == null || !readingsStoryIds.Any())
            {
                return NotFound();
            }
            return Ok(readingsStoryIds);
        }

        private async Task AddStoryDetialsToReading(List<ReadingWithProgressDto> readings)
        {
            string storyIds = "";
            readings.ForEach(r =>
            {
                storyIds += r.StoryId + ",";
            });
            storyIds = storyIds.Remove(storyIds.Length - 1);
            Dictionary<string, string> formData = new Dictionary<string, string>();
            formData.Add("ids", storyIds);
            List<ReadStoryDto> res = new List<ReadStoryDto>();
            res = await _consulHttpClient.PostFormAsync<List<ReadStoryDto>>("story_ms", $"/api/Story/get-stories-to-read", formData, res);

            for (int index = 0; index < readings.Count; index++)
            {
                readings.ElementAt(index).StoryCover = res.ElementAt(index).StoryCover;
                readings.ElementAt(index).Title = res.ElementAt(index).Title;
                readings.ElementAt(index).PagesCount = res.ElementAt(index).PagesCount;
                readings.ElementAt(index).Progress = (double)readings.ElementAt(index).Count / (double)readings.ElementAt(index).PagesCount;
            }

        }

        [HttpPost("add")]
        public IActionResult Post([FromBody] ReadingCreateDto readingCreateDto)
        {
            if (readingCreateDto == null)
            {
                return BadRequest();
            }

            Reading reading = _mapper.Map<Reading>(readingCreateDto);
            ValidationLogicForAdding(reading);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            _readingService.Insert(reading);

            return CreatedAtAction("GetById", new { id = reading.ReadingId }, _mapper.Map<ReadingDto>(reading));
        }

        //     // GET: api/interaction/highLight/1/update
        //     [HttpPut("{id}/update")]
        //     [ProducesResponseType(StatusCodes.Status200OK)]
        //     public IActionResult Update([FromBody] HighLightUpdateDto highLight, [FromRoute] int id)
        //     {
        //         if (highLight == null)
        //         {
        //             return BadRequest();
        //         }

        //         HighLight oldHighLight = _highLightService.GetById(id).Result;
        //         if (oldHighLight == null)
        //         {
        //             return NotFound();
        //         }

        //         HighLight highLightEntity = _mapper.Map<HighLight>(highLight);
        //         highLightEntity.HighLightId = id;
        //         _highLightService.Update(highLightEntity, oldHighLight);
        //         return NoContent();
        //     }



        //     // DELETE: api/interaction/highLight/5/delete
        //     [HttpDelete("{id}/delete")]
        //     public IActionResult Delete([FromRoute] int id)
        //     {
        //         if (!ModelState.IsValid)
        //         {
        //             return BadRequest(ErrorMessage.ModelStateParser(ModelState));
        //         }
        //         var highLight = _highLightService.GetById(id).Result;
        //         if (highLight == null)
        //         {
        //             return NotFound();
        //         }

        //         _highLightService.Delete(highLight);
        //         return NoContent();
        //     }


        private void ValidationLogicForAdding(Reading reading)
        {

            ViewStoryDto storyDto = _consulHttpClient.GetAsync<ViewStoryDto>("story_ms", $"/api/story/get/{reading.Interaction.StoryId}").Result;

            if (storyDto == null || storyDto.StoryId < 1)
            {
                ModelState.AddModelError("StoryId", "Story not found");
            }

            PageDto pageDto = _consulHttpClient.GetAsync<PageDto>("story_ms", $"/api/story/Page/get/{reading.Interaction.PageId}").Result;

            if (pageDto == null || pageDto.PageId < 1)
            {
                ModelState.AddModelError("PageId", "Page not found");
            }

            UserDto userDto = _consulHttpClient.GetAsync<UserDto>("identity_ms", $"/api/identity/{reading.Interaction.StudentId}/get").Result;

            if (userDto == null || string.IsNullOrEmpty(userDto.Id))
            {
                ModelState.AddModelError("StudentId", "Student not found");
            }
            else
            {
                if (!userDto.Role.Equals(RoleTypes.Student.ToString()))
                {
                    ModelState.AddModelError("StudentId", "User not a student");
                }
            }
        }

    }



}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AttachmentDto;
using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iread_interaction_ms.Web.Util;
using iread_interaction_ms.Web.DTO.Story;
using iread_interaction_ms.Web.DTO;
using iread_interaction_ms.DataAccess.Data.Type;
using iread_interaction_ms.Web.DTO.StoryDto;
using iread_interaction_ms.Web.Dto;

namespace iread_interaction_ms.Web.Controller
{

    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class AudioController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AudioService _audioService;
        private readonly InteractionsService _interactionsService;
        private readonly IConsulHttpClientService _consulHttpClient;
        private readonly string _attachmentsMs = "attachment_ms";

        public AudioController(AudioService audioService, IMapper mapper,
         InteractionsService interactionsService,
          IConsulHttpClientService consulHttpClient)
        {
            _audioService = audioService;
            _mapper = mapper;
            _interactionsService = interactionsService;
            _consulHttpClient = consulHttpClient;
        }

        // GET: api/audio/get/1
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
        {
            Audio audio = await _audioService.GetById(id);

            if (audio == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AudioDto>(audio));
        }


        // GET: api/interaction/audio/get-by-interaction/1
        [HttpGet("get-by-interaction/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute] int id)
        {
            Audio audio = await _audioService.GetByInteractionId(id);

            if (audio == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AudioDto>(audio));
        }


        // GET: api/interaction/audio/get-by-page/1
        [HttpGet("get-by-page/{pageId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPageId([FromRoute] int pageId)
        {
            List<Audio> audios = await _audioService.GetByPageId(pageId);

            if (audios == null || !audios.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<AudioDto>>(audios));
        }

        //POST: api/audio/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Post([FromBody] AudioCreateDto audioCreateDto)
        {
            if (audioCreateDto == null)
            {
                return BadRequest();
            }

            Audio audioEntity = _mapper.Map<Audio>(audioCreateDto);
            ValidationLogicForAdding(audioEntity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            if (!_audioService.Insert(audioEntity))
            {
                return BadRequest();
            }

            return CreatedAtAction("GetById", new { id = audioEntity.AudioId }, _mapper.Map<AudioDto>(audioEntity));
        }


        [HttpPut("{id}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] AudioUpdateDto audio, [FromRoute] int id)
        {
            if (audio == null)
            {
                return BadRequest();
            }

            Audio oldAudio = _audioService.GetById(id).Result;
            if (oldAudio == null)
            {
                return NotFound();
            }

            Audio audioEntity = _mapper.Map<Audio>(audio);
            ValidationLogicForUpdating(audioEntity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            audioEntity.AudioId = id;
            _audioService.Update(audioEntity, oldAudio);
            return NoContent();
        }


        //DELETE: api/interaction/audio/5/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            var audio = _audioService.GetById(id).Result;
            if (audio == null)
            {
                return NotFound();
            }

            _audioService.Delete(audio);
            _consulHttpClient.Delete(_attachmentsMs, $"/api/Attachment/{audio.AttachmentId}/delete");
            return NoContent();
        }

        private void ValidationLogicForAdding(Audio audio)
        {

            ViewStoryDto storyDto = _consulHttpClient.GetAsync<ViewStoryDto>("story_ms", $"/api/story/get/{audio.Interaction.StoryId}").Result;

            if (storyDto == null || storyDto.StoryId < 1)
            {
                ModelState.AddModelError("StoryId", "Story not found");
            }

            AttachmentDTO attachmentDto = _consulHttpClient.GetAsync<AttachmentDTO>("attachment_ms", $"/api/Attachment/get/{audio.AttachmentId}").Result;

            if (attachmentDto == null || attachmentDto.Id < 1)
            {
                ModelState.AddModelError("AudioId", "Attachment not found");
            }
            else
            {
                if (!AudioExtensions.All.Contains(attachmentDto.Extension.ToLower()))
                {
                    ModelState.AddModelError("Audio", "Audio not have valid extension, should be one of [" + string.Join(",", AudioExtensions.All) + "]");
                }
            }

            PageDto pageDto = _consulHttpClient.GetAsync<PageDto>("story_ms", $"/api/story/Page/get/{audio.Interaction.PageId}").Result;

            if (pageDto == null || pageDto.PageId < 1)
            {
                ModelState.AddModelError("PageId", "Page not found");
            }

            UserDto userDto = _consulHttpClient.GetAsync<UserDto>("identity_ms", $"/api/identity/{audio.Interaction.StudentId}/get").Result;

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

            if (_audioService.Exists(audio.Interaction.PageId, audio.Interaction.StoryId, userDto.Id))
            {
                ModelState.AddModelError("Audio", "Audio interaction already exists.");
            }
        }

        private void ValidationLogicForUpdating(Audio audio)
        {
            AttachmentDTO attachmentDto = _consulHttpClient.GetAsync<AttachmentDTO>("attachment_ms", $"/api/Attachment/get/{audio.AttachmentId}").Result;

            if (attachmentDto == null || attachmentDto.Id < 1)
            {
                ModelState.AddModelError("AudioId", "Attachment not found");
            }
            else
            {
                if (!AudioExtensions.All.Contains(attachmentDto.Extension.ToLower()))
                {
                    ModelState.AddModelError("Audio", "Audio not have valid extension, should be one of [" + string.Join(",", AudioExtensions.All) + "]");
                }
            }
        }
    }
}
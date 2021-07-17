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

namespace iread_interaction_ms.Web.Controller
{
    public class AudioController:ControllerBase
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
        public async Task<IActionResult> GetById([FromRoute]int id)
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
        public async Task<IActionResult> GetByInteractionId([FromRoute]int id)
        {
            Audio audio = await _audioService.GetByInteractionId(id);

            if (audio == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<AudioDto>(audio));
        }
        
        //POST: api/audio/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> PostAudio([FromForm]AudioCreateDto audioCreateDto)
        {
            if (audioCreateDto == null)
            {
                return BadRequest();
            }
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            Audio audioEntity = _mapper.Map<Audio>(audioCreateDto);
            
            //for check if story has an audio
            if (await _audioService.HasAudio(audioEntity.InteractionId))
            {
                ModelState.AddModelError("Audio", ErrorMessage.AUDIO_ALREADY_EXIST);
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            
            //Get audio interaction's
            Interaction interaction = await _interactionsService.GetInteractionById(audioEntity.InteractionId);
            
            
            //TODO insert attachment before insert audio

            var parameters = new Dictionary<string, string>() { {"StoryId",interaction.StoryId.ToString()} };

            List<IFormFile> attachments = new List<IFormFile>();
            attachments.Add(audioCreateDto.Attachment);
            try
            {
                await _consulHttpClient.PostFormAsync<AttachmentsWithStoryId>(_attachmentsMs, "api/Attachment",
                    parameters, attachments?.ToList());
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
            
            if (!_audioService.Insert(audioEntity))
            {
                return BadRequest();
            }

            return CreatedAtAction("GetAudio", new { id = audioEntity.AudioId }, _mapper.Map<AudioDto>(audioEntity));
        }
        
    }
}
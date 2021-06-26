using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
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
        private readonly AudioServices _audioServices;

        public AudioController(AudioServices audioServices, IMapper mapper)
        {
            _audioServices = audioServices;
            _mapper = mapper;
        }
        
        // GET: api/audio/get/1
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAudio([FromRoute]int id)
        {
            Audio audio = await _audioServices.GetAudioByID(id);

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
        public async Task<IActionResult> PostAudio([FromBody]AudioCreateDto audioCreateDto)
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
            if (await _audioServices.IsInteractionHasAudio(audioEntity.InteractionId))
            {
                ModelState.AddModelError("Audio", ErrorMessage.AUDIO_ALREADY_EXIST);
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            
            //TODO insert attachment before insert audio

            if (!_audioServices.InsertAudio(audioEntity))
            {
                return BadRequest();
            }

            return CreatedAtAction("GetAudio", new { id = audioEntity.AudioId }, _mapper.Map<AudioDto>(audioEntity));
        }
        
    }
}
using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iread_interaction_ms.Web.Util;
using iread_interaction_ms.Web.Dto.CommentDto;
using System.Collections.Generic;
using iread_interaction_ms.DataAccess.Data.Type;
using iread_interaction_ms.Web.DTO.Story;
using iread_interaction_ms.Web.Dto;
using iread_interaction_ms.Web.DTO.StoryDto;
using System.Linq;
using iread_interaction_ms.Web.DTO;

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class DrawingController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly DrawingService _drawingService;
        private readonly InteractionsService _interactionServices;
        private readonly IConsulHttpClientService _consulHttpClient;

        public DrawingController(DrawingService commentsService, IMapper mapper, InteractionsService interactionServices, IConsulHttpClientService consulHttpClient)
        {
            _drawingService = commentsService;
            _mapper = mapper;
            _interactionServices = interactionServices;
            _consulHttpClient = consulHttpClient;
        }
        
        // GET: api/interaction/drawing/1/get
        [HttpGet("{id}/get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            Drawing drawing = await _drawingService.GetById(id);

            if (drawing == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DrawingDto>(drawing));
        }

        // GET: api/interaction/drawing/get-by-interaction/1
        [HttpGet("get-by-interaction/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute]int id)
        {
            Drawing drawing = await _drawingService.GetByInteractionId(id);

            if (drawing == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<DrawingDto>(drawing));
        }


        //POST: api/interaction/drawing/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] DrawingCreateDto drawingCreateDto)
        {
            if (drawingCreateDto == null)
            {
                return BadRequest();
            }

            Drawing drawing = _mapper.Map<Drawing>(drawingCreateDto);
            ValidationLogic(drawing);
            ValidationLogicForAdding(drawing);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

           
            if (!_drawingService.Insert(drawing))
            {
                return BadRequest();
            }
            return CreatedAtAction("GetById", new { id = drawing.DrawingId }, _mapper.Map<DrawingDto>(drawing));
        }


    //     [HttpPut("{id}/update")]
    //     [ProducesResponseType(StatusCodes.Status200OK)]
    //     public IActionResult Update([FromBody] CommentUpdateDto comment, [FromRoute] int id)
    //     {
    //          if (comment == null)
    //         {
    //             return BadRequest();
    //         }            
            
    //         Comment oldComment = _drawingService.GetById(id).Result;
    //          if (oldComment == null)
    //         {
    //             return NotFound();
    //         }

    //         // get word from database before checking
    //         Comment commentEntity = _mapper.Map<Comment>(comment);
    //         commentEntity.Word = oldComment.Word;            
    //         ValidationLogic(commentEntity);
    //         if (!ModelState.IsValid)
    //         {
    //             return BadRequest(ErrorMessage.ModelStateParser(ModelState));
    //         }
            
    //         commentEntity.CommentId = id;
    //         _drawingService.Update(commentEntity, oldComment);
    //         return NoContent();
    //     }



        // DELETE: api/interaction/drawing/5/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            var drawing = _drawingService.GetById(id).Result;
            if (drawing == null)
            {
                return NotFound();
            }

           _drawingService.Delete(drawing);
            return NoContent();
        }


    private void ValidationLogicForAdding(Drawing drawing)
    {

        ViewStoryDto storyDto = _consulHttpClient.GetAsync<ViewStoryDto>("story_ms", $"/api/story/get/{drawing.Interaction.StoryId}").Result;

        if(storyDto == null || storyDto.StoryId < 1){
             ModelState.AddModelError("StoryId", "Story not found");    
        }

        PageDto pageDto = _consulHttpClient.GetAsync<PageDto>("story_ms", $"/api/story/Page/get/{drawing.Interaction.PageId}").Result;

        if(pageDto == null || pageDto.PageId < 1){
             ModelState.AddModelError("PageId", "Page not found");    
        }

        UserDto userDto = _consulHttpClient.GetAsync<UserDto>("identity_ms", $"/api/identity_ms/SysUsers/{drawing.Interaction.StudentId}/get").Result;

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
                                                                                                                                    
        AttachmentDTO attachmentDto = _consulHttpClient.GetAsync<AttachmentDTO>("attachment_ms", $"/api/Attachment/get/{drawing.AudioId}").Result;

        if(attachmentDto == null || attachmentDto.Id < 1){
             ModelState.AddModelError("StudentId", "Audio not found");    
        }
        else
        {
            if (!AudioExtensions.All.Contains(attachmentDto.Extension.ToLower()))
            {
                ModelState.AddModelError("Audio", "Audio not have valid extension, should be one of [" + string.Join(",", AudioExtensions.All) +"]");
            }
        }
    }

    private void ValidationLogic(Drawing drawing)
        {
            // if(drawing.CommentType.Equals(CommentType.WORD_CLASS.ToString())){
                
            //     if(!WordClasses.elementsAsStr.Contains(comment.Value)){
            //         ModelState.AddModelError("Value", "Value should be one of [" + string.Join(",", WordClasses.elementsAsStr) +"]");    
            //     }
            // }

            // if(drawing.CommentType.Equals(CommentType.EXAMPLE.ToString())){
            //     List<string> wordsOfExample = comment.Value.Split(' ',',', '.', ':', '\t').ToList();
            //     List<string> delimiter = new List<string>(){"",",", ".", ":", "\t"};
            //     wordsOfExample.RemoveAll(word => delimiter.Contains(word));
                
            //     if(wordsOfExample.Count() < 2 || !wordsOfExample.Contains(comment.Word)){
            //         ModelState.AddModelError("Value", $"Value should be a sentence and contains the word \'{comment.Word}\'");    
            //     }
            // }

        }
    }
}
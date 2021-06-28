using System.Threading.Tasks;
using AutoMapper;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.Web.Dto.AudioDto;
using iread_interaction_ms.Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using iread_interaction_ms.Web.Util;
using iread_interaction_ms.Web.Dto.CommentDto;
using System;
using System.Collections.Generic;
using iread_interaction_ms.DataAccess.Data.Type;

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class CommentController:ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly CommentsService _commentsService;
        private readonly InteractionsService _interactionServices;
        private readonly IConsulHttpClientService _consulHttpClient;

        public CommentController(CommentsService commentsService, IMapper mapper, InteractionsService interactionServices, IConsulHttpClientService consulHttpClient)
        {
            _commentsService = commentsService;
            _mapper = mapper;
            _interactionServices = interactionServices;
            _consulHttpClient = consulHttpClient;
        }
        
        // GET: api/interaction/comment/get/1
        [HttpGet("get/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute]int id)
        {
            Comment comment = await _commentsService.GetById(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        // GET: api/interaction/comment/get-by-interaction/1
        [HttpGet("get-by-interaction/{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute]int id)
        {
            Comment comment = await _commentsService.GetByInteractionId(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommentDto>(comment));
        }



        //POST: api/interaction/comment/add
        [HttpPost("add")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Post([FromBody] CommentCreateDto commentCreateDto)
        {
            if (commentCreateDto == null)
            {
                return BadRequest();
            }

            Comment comment = _mapper.Map<Comment>(commentCreateDto);
            ValidationLogic(comment);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

           
            if (!_commentsService.Insert(comment))
            {
                return BadRequest();
            }
            return CreatedAtAction("GetById", new { id = comment.CommentId }, _mapper.Map<CommentDto>(comment));
        }

        [HttpPut("{id}/update")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult Update([FromBody] CommentUpdateDto comment, [FromRoute] int id)
        {
             if (comment == null)
            {
                return BadRequest();
            }

            Comment commentEntity = _mapper.Map<Comment>(comment);
            ValidationLogic(commentEntity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            if (!_commentsService.Exists(id))
            {
                return NotFound();
            }
            
            commentEntity.CommentId = id;
            _commentsService.Update(commentEntity);
            return NoContent();
        }



        // DELETE: api/interaction/comment/5/delete
        [HttpDelete("{id}/delete")]
        public IActionResult Delete([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }
            var comment = _commentsService.GetById(id).Result;
            if (comment == null)
            {
                return NotFound();
            }

           _commentsService.Delete(comment);
            return NoContent();
        }



    private void ValidationLogic(Comment comment)
        {
            ModelState.Clear();
            if(comment.CommentType.Equals(CommentType.WORD_CLASS.ToString())){
                
                if(!WordClasses.elementsAsStr.Contains(comment.Value)){
                    ModelState.AddModelError("Value", "Value should be one of [" + string.Join(",", WordClasses.elementsAsStr) +"]");    
                }
            }

        }
    }
}
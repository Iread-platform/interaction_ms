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

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/Interaction/[controller]/")]
    public class CommentController : ControllerBase
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

        // GET: api/interaction/comment/1/
        [HttpGet("{id}/get")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetById([FromRoute] int id)
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
        public async Task<IActionResult> GetByInteractionId([FromRoute] int id)
        {
            Comment comment = await _commentsService.GetByInteractionId(id);

            if (comment == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<CommentDto>(comment));
        }

        // GET: api/interaction/comment/get-by-page/1
        [HttpGet("get-by-page/{pageId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByPageId([FromRoute] int pageId)
        {
            List<Comment> comments = await _commentsService.GetByPageId(pageId);

            if (comments == null || !comments.Any())
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<CommentDto>>(comments));
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
            ValidationLogicForAdding(comment);
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

            Comment oldComment = _commentsService.GetById(id).Result;
            if (oldComment == null)
            {
                return NotFound();
            }

            // get word from database before checking
            Comment commentEntity = _mapper.Map<Comment>(comment);
            commentEntity.Word = oldComment.Word;
            ValidationLogic(commentEntity);
            if (!ModelState.IsValid)
            {
                return BadRequest(ErrorMessage.ModelStateParser(ModelState));
            }

            commentEntity.CommentId = id;
            _commentsService.Update(commentEntity, oldComment);
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


        private void ValidationLogicForAdding(Comment comment)
        {

            ViewStoryDto storyDto = _consulHttpClient.GetAsync<ViewStoryDto>("story_ms", $"/api/story/get/{comment.Interaction.StoryId}").Result;

            if (storyDto == null || storyDto.StoryId < 1)
            {
                ModelState.AddModelError("StoryId", "Story not found");
            }

            PageDto pageDto = _consulHttpClient.GetAsync<PageDto>("story_ms", $"/api/story/Page/get/{comment.Interaction.PageId}").Result;

            if (pageDto == null || pageDto.PageId < 1)
            {
                ModelState.AddModelError("PageId", "Page not found");
            }

            UserDto userDto = _consulHttpClient.GetAsync<UserDto>("identity_ms", $"/api/identity_ms/SysUsers/{comment.Interaction.StudentId}/get").Result;

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

        private void ValidationLogic(Comment comment)
        {
            if (comment.CommentType.Equals(CommentType.WORD_CLASS.ToString()))
            {

                if (!WordClasses.elementsAsStr.Contains(comment.Value))
                {
                    ModelState.AddModelError("Value", "Value should be one of [" + string.Join(",", WordClasses.elementsAsStr) + "]");
                }
            }

            if (comment.CommentType.Equals(CommentType.EXAMPLE.ToString()))
            {
                List<string> wordsOfExample = comment.Value.Split(' ', ',', '.', ':', '\t').ToList();
                List<string> delimiter = new List<string>() { "", ",", ".", ":", "\t" };
                wordsOfExample.RemoveAll(word => delimiter.Contains(word));

                if (wordsOfExample.Count() < 2 || !wordsOfExample.Contains(comment.Word))
                {
                    ModelState.AddModelError("Value", $"Value should be a sentence and contains the word \'{comment.Word}\'");
                }
            }

        }
    }
}
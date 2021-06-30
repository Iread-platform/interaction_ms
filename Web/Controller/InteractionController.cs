using System;
using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using AutoMapper;
using Consul;
using iread_interaction_ms.DataAccess.Data.Entity;
using iread_interaction_ms.DataAccess.Interface;
using iread_interaction_ms.Web.Dto.CommentDto;
using iread_interaction_ms.Web.Dto.InteractioDto;
using iread_interaction_ms.Web.Service;
using iread_interaction_ms.Web.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace iread_interaction_ms.Web.Controller
{
    [ApiController]
    [Route("api/[controller]/")]
    public class InteractionController : ControllerBase
    {

        private readonly ILogger<InteractionController> _logger;
        private readonly InteractionsService _interactionsService;
       
        private readonly IMapper _mapper;
        public InteractionController(ILogger<InteractionController> logger,
        InteractionsService interactionsService, IPublicRepository repository, IMapper mapper)
        {
            _logger = logger;
            _mapper = mapper;
            _interactionsService = interactionsService;
        }

        // GET: api/interaction/get-by-page/1
        [HttpGet("get-by-page/{pageId}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetByInteractionId([FromRoute]int pageId)
        {
            List<Interaction> interactions = await _interactionsService.GetByPageId(pageId);

            if (interactions == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<List<InteractionDto>>(interactions));
            
        }


        
    }
}

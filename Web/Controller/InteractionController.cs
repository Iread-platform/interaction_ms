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

        
    }
}

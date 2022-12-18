using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using LDST.Application.Playground.Commands.CreateCommand;
using LDST.Contracts.Playground;
using MapsterMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace LDST.Api.Controllers
{
    [Route("[controller]")]
    public class PlaygroundController : ApiController
    {
        private readonly IMapper _mapper;
        private readonly ISender _mediator;
        public PlaygroundController(IMapper mapper, ISender mediator)
        {
            _mapper = mapper;
            _mediator = mediator;
        }

        [HttpPost("hosts/{hostId}")]
        public async Task<IActionResult> CreatePlayground(CreatePlaygroundRequest request, string hostId)
        {
            var command = _mapper.Map<CreatePlaygroundCommand>((request, hostId));
            var createPlaygroundId = await _mediator.Send(command);

            return createPlaygroundId.Match(
                playground => Ok(createPlaygroundId),
                errors => Problem(errors));
        }
    }
}
﻿using ChatbotApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers
{
    [Route("api/{conversationId}/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("/messages/user")]
        public async Task<IActionResult> SendUserMessage([FromBody] SendUserMessageCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("/messages/chat")]
        public async Task<IActionResult> SendChatMessage([FromBody] SendChatMessageCommand command)
        {
            var response = await _mediator.Send(command);

            return Ok(response);
        }

        [HttpPost("/messages/reaction")]
        public async Task<IActionResult> SendUserReaction([FromBody] SendUserReactionCommand command)
        {
            var response = await _mediator.Send(command);
            if(response == true)
                return Ok();
            else return BadRequest();
        }
    }
}

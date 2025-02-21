using ChatbotApi.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ChatbotApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChatController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ChatController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{conversationId}/messages")]
        public async Task<IActionResult> SendMessage(Guid conversationId, [FromBody] SendMessageCommand command)
        {
            command.ConversationId = conversationId;

            var response = await _mediator.Send(command);

            return Ok(response);
        }
    }
}

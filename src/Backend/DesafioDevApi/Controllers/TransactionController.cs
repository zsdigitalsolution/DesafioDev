using DesafioDevApi.Domain.Commands.Inputs;
using DesafioDevApi.Domain.Commands.Outputs;
using DesafioDevApi.Domain.Common;
using Flunt.Notifications;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DesafioDevApi.Controllers;

[ApiController]
[Route("[controller]")]
public class TransactionController : ControllerBase
{
    private readonly IMediator _mediatr;

    public TransactionController(IMediator mediator)
    {
        _mediatr = mediator;
    }

    /// <summary>
    /// Uploads a CNAB file for processing.
    /// </summary>
    /// <param name="file">The CNAB file to upload.</param>
    /// <returns>Ok if the file was processed successfully, BadRequest otherwise.</returns>
    [HttpPost]
    public async Task<IActionResult> UploadFile([FromForm] IFormFile file)
    {
        var response = await _mediatr.Send(new TransactionFileRequestCommand(file: file));
        return this.ValidateResponse(201, response);
    }
    /// <summary>
    /// Gets a processed transactions.
    /// </summary>
    /// <param name="id"></param>
    /// <returns>A transactions.</returns>
    [HttpGet("{id}")]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResultMessage<TransactionResponseCommand>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetById(int id)
    {
        var response = await _mediatr.Send(new TransactionGetRequestCommand(id: id));
        return this.ValidateResponse(200, response);
    }
    /// <summary>
    /// Gets all processed transactions.
    /// </summary>
    /// <returns>A list of all transactions.</returns>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType(typeof(ResultMessage<IEnumerable<TransactionResponseCommand>>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(IEnumerable<Notification>), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ExceptionResponse), StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> GetAll()
    {
        var response = await _mediatr.Send(new TransactionGetAllRequestCommand());
        return this.ValidateResponse(200, response);
    }
}

using AutoMapper;
using CardStorageService.Data;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureDevelopment.Models.Requests;
using SecureDevelopment.Services;

namespace SecureDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepositoryService _clientRepositoryService;
        private readonly ILogger<CardController> _logger;
        private readonly IValidator<CreateClientRequest> _clientValidator;
        private readonly IMapper _mapper;

        public ClientController(
          ILogger<CardController> logger,
          IClientRepositoryService clientRepositoryService,
          IValidator<CreateClientRequest> clientValidator,
          IMapper mapper)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
            _clientValidator = clientValidator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                ValidationResult validatorResult = _clientValidator.Validate(request);
                if (!validatorResult.IsValid)
                    return BadRequest(validatorResult.ToDictionary());

                var clientId = _clientRepositoryService.Create(_mapper.Map<Client>(request));
                //var clientId = _clientRepositoryService.Create(new Client
                //{
                //    FirstName = request.FirstName,
                //    Surname = request.Surname,
                //    Patronymic = request.Patronymic
                //});
                return Ok(new CreateClientResponse
                {
                    ClientId = clientId
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create client error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 912,
                    ErrorMessage = "Create clinet error."
                });
            }
        }
    }
}

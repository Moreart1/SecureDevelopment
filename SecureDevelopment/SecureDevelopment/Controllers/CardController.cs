using AutoMapper;
using CardStorageService.Data;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SecureDevelopment.Models;
using SecureDevelopment.Models.Requests;
using SecureDevelopment.Services;

namespace SecureDevelopment.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CardController : ControllerBase
    {
        private readonly ILogger<CardController> _logger;
        private readonly ICardRepositoryService _cardRepositoryService;
        private readonly IValidator<CreateCardRequest> _cardValidator;
        private readonly IMapper _mapper;

        public CardController(ILogger<CardController> logger,
           ICardRepositoryService cardRepositoryService,
           IValidator<CreateCardRequest> cardValidator,
           IMapper mapper)
        {
            _logger = logger;
            _cardRepositoryService = cardRepositoryService;
            _cardValidator = cardValidator;
            _mapper = mapper;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(string), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateCardRequest request)
        {
            try
            {
                ValidationResult validationResult = _cardValidator.Validate(request);
                if (!validationResult.IsValid)
                    return BadRequest(validationResult.ToDictionary());

                var cardId = _cardRepositoryService.Create(_mapper.Map<Card>(request));
                //var cardId = _cardRepositoryService.Create(new Card
                //{                  
                //    ClientId = request.ClientId,
                //    CardNo = request.CardNo,
                //    ExpDate = request.ExpDate,
                //    CVV2 = request.CVV2
                //});
                return Ok(new CreateCardResponse
                {
                    CardId = cardId.ToString()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Create card error.");
                return Ok(new CreateCardResponse
                {
                    ErrorCode = 1012,
                    ErrorMessage = "Create card error."
                });
            }
        }

        [HttpGet("get-by-client-id")]
        [ProducesResponseType(typeof(GetCardsResponse), StatusCodes.Status200OK)]
        public IActionResult GetByClientId([FromQuery] string clientId)
        {
            try
            {
                var cards = _cardRepositoryService.GetByClientId(clientId);
                return Ok(new GetCardsResponse
                {
                    Cards = cards.Select(card => new CardDto
                    {
                        CardNo = card.CardNo,
                        CVV2 = card.CVV2,
                        Name = card.Name,
                        ExpDate = card.ExpDate.ToString("MM/yy")
                    }).ToList()
                });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Get cards error.");
                return Ok(new GetCardsResponse
                {
                    ErrorCode = 1013,
                    ErrorMessage = "Get cards error."
                });
            }
        }
    }
}

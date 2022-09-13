﻿using CardStorageService.Data;
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

        public ClientController(
          ILogger<CardController> logger,
          IClientRepositoryService clientRepositoryService)
        {
            _logger = logger;
            _clientRepositoryService = clientRepositoryService;
        }

        [HttpPost("create")]
        [ProducesResponseType(typeof(CreateClientResponse), StatusCodes.Status200OK)]
        public IActionResult Create([FromBody] CreateClientRequest request)
        {
            try
            {
                var clientId = _clientRepositoryService.Create(new Client
                {
                    FirstName = request.FirstName,
                    Surname = request.Surname,
                    Patronymic = request.Patronymic
                });
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
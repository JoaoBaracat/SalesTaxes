using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SalesTaxes.API.ViewModel;
using SalesTaxes.Domain.Apps;
using SalesTaxes.Domain.ValueObjects;
using SalesTaxes.Domain.Notifications;
using System;
using System.Collections.Generic;

namespace SalesTaxes.API.Controllers
{
    [Produces("application/json")]
    [ApiController]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiVersion("1.0")]
    public class SalesController : MainController
    {
        private readonly ISalesApp _salesApp;
        private readonly ILogger<SalesController> _logger;
        private readonly IMapper _mapper;

        public SalesController(ISalesApp salesApp, INotifier notifier, ILogger<SalesController> logger, IMapper mapper) : base(notifier, logger)
        {
            _salesApp = salesApp;
            _logger = logger;
            _mapper = mapper;
        }

        /// <summary>
        /// Calculate the total value and taxes for a sale
        /// </summary>
        /// <returns>An action result of a sale and its items with the total price and the taxes</returns>
        /// <response code="200">Sale calculate successfully</response>
        /// <response code="400">Some of the items data are not correct</response>
        /// <response code="500">The server could not process the sale</response>
        [HttpPut(Name = "CalculateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<SaleResultViewModel> CalculateSale(IEnumerable<SaleItemViewModel> saleItems)
        {
            try
            {
                _logger.LogInformation($"Request for new sale calculation: {JsonConvert.SerializeObject(saleItems)}");
                var saleItemResult = _salesApp.CalculateSale(_mapper.Map<IEnumerable<SaleItem>>(saleItems));
                return CustomResponse(_mapper.Map<SaleResultViewModel>(saleItemResult));
            }
            catch (Exception ex)
            {
                _logger.LogInformation($"An error ocurred in a sale calculation with message: {ex.Message}");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
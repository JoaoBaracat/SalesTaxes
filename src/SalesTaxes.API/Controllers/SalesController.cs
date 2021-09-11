using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SalesTaxes.API.ViewModel;
using SalesTaxes.Domain.Apps;
using SalesTaxes.Domain.Entities;
using SalesTaxes.Domain.Notifications;
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
        /// <returns>An action result of a sale and its items</returns>
        /// <response code="200">Sale calculate successfully</response>
        /// <response code="400">Some of the items data are not correct</response>
        [HttpPost(Name = "CalculateSale")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<SaleResultViewModel> CalculateSale(IEnumerable<SaleItemViewModel> saleItems)
        {
            var saleItemResult = _salesApp.CalculateSale(_mapper.Map<IEnumerable<SaleItem>>(saleItems));
            return CustomResponse(_mapper.Map<SaleResultViewModel>(saleItemResult));
        }
    }
}
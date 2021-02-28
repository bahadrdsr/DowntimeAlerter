using System;
using System.Threading.Tasks;
using Application.HealthCheckResult.Queries.GetHealthCheckResults;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    [Authorize]
    public class HealthCheckResultController : Controller
    {
        private readonly ILogger<HealthCheckResultController> _logger;
        private readonly IMediator _mediator;
        public HealthCheckResultController(ILogger<HealthCheckResultController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index(Guid id)
        {
            ViewData["Id"] = id;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetHealthCheckResults(JqueryCustomParameters model)
        {
            var pageNo = Convert.ToInt32(Math.Floor((double)model.Start / model.Length) + 1);
            var viewModel = await _mediator.Send(new GetHealthCheckResultsQuery(pageNo, model.Length, model.TargetAppId));
            // Jquery Datatable's desired format
            var dataTableJson = new
            {
                
                iTotalRecords = viewModel.TotalCount,
                iTotalDisplayRecords = viewModel.TotalCount,
                aaData = viewModel.Data
            };
            return Json(dataTableJson);
        }
    }
}
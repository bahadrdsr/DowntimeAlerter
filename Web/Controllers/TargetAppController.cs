using System;
using System.Threading.Tasks;
using Application.TargetApp.Commands.CreateTargetApp;
using Application.TargetApp.Commands.DeleteTargetApp;
using Application.TargetApp.Commands.UpdateTargetApp;
using Application.TargetApp.Queries.GetTargetAppById;
using Application.TargetApp.Queries.GetTargetApps;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class TargetAppController : Controller
    {
        private readonly ILogger<TargetAppController> _logger;
        private readonly IMediator _mediator;
        public TargetAppController(ILogger<TargetAppController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public ActionResult Index()
        {
            return View();
        }
        public ActionResult UpsertForm(TargetAppBindingModel model)
        {
            return PartialView("_UpsertForm", model);
        }
        public ActionResult DeleteConfirm()
        {
            return PartialView("_DeleteConfirm");
        }
        [HttpGet]
        public async Task<IActionResult> GetTargetApps(JqueryDatatableParameter model)
        {
            var pageNo = Convert.ToInt32(Math.Floor((double)model.iDisplayStart / model.iDisplayLength) + 1);
            var viewModel = await _mediator.Send(new GetTargetAppsQuery(pageNo, model.iDisplayLength, model.sSearch));
            // Jquery Datatable's desired format
            var dataTableJson = new
            {
                model.sEcho,
                iTotalRecords = viewModel.TotalCount,
                iTotalDisplayRecords = viewModel.Count,
                aaData = viewModel.Data
            };
            return Ok(dataTableJson);
        }
        [HttpGet]
        public async Task<IActionResult> GetTargetApp([FromQuery]Guid id)
        {
            var result = await _mediator.Send(new GetTargetAppByIdQuery
            {
                Id = id
            });
            return Ok(result);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SaveTargetApp(TargetAppBindingModel model)
        {
            var result = await _mediator.Send(new CreateTargetAppCommand
            {
                Name = model.Name,
                Url = model.Url,
                Interval = model.Interval,
                IsActive = model.IsActive
            });
            return Ok(result);
        }
        [HttpPut]
        [HttpPatch]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> UpdateTargetApp(TargetAppBindingModel model)
        {
            var result = await _mediator.Send(new UpdateTargetAppCommand
            {
                Id = model.Id.Value,
                Name = model.Name,
                Url = model.Url,
                Interval = model.Interval,
                IsActive = model.IsActive
            });
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTargetApp([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteTargetAppCommand { Id = id });
            return Ok(result);
        }
    }
}
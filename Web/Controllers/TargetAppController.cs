using System;
using System.Threading.Tasks;
using Application.HealthCheckResult.Commands.CreateHealthCheckResult;
using Application.TargetApp.Commands.CreateTargetApp;
using Application.TargetApp.Commands.DeleteTargetApp;
using Application.TargetApp.Commands.UpdateTargetApp;
using Application.TargetApp.Queries.GetTargetAppById;
using Application.TargetApp.Queries.GetTargetApps;
using Hangfire;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    [Authorize(Roles ="administrator")]
    public class TargetAppController : Controller
    {
        private readonly ILogger<TargetAppController> _logger;
        private readonly IMediator _mediator;
        private readonly IRecurringJobManager _jobManager;
        public TargetAppController(ILogger<TargetAppController> logger, IMediator mediator, IRecurringJobManager jobManager)
        {
            _logger = logger;
            _mediator = mediator;
            _jobManager = jobManager;
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
                iTotalDisplayRecords = viewModel.TotalCount,
                aaData = viewModel.Data
            };
            return Ok(dataTableJson);
        }
        [HttpGet]
        public async Task<IActionResult> GetTargetApp([FromQuery] Guid id)
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

            if (result != null && result != Guid.Empty)
            {
                if (model.IsActive)
                {
                    var job = Hangfire.Common.Job.FromExpression(() => RunHealthCheck(result));
                    _jobManager.AddOrUpdate(result.ToString(), job
                    ,
                     cronExpression: $"*/{model.Interval} * * * *",
                      new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc, QueueName = "default" });
                }
            }

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
            if (result == Unit.Value)
            {
                if (model.IsActive)
                {
                    var job = Hangfire.Common.Job.FromExpression(() => RunHealthCheck(model.Id.Value));
                    _jobManager.AddOrUpdate(model.Id.ToString(), job
                    ,
                     cronExpression: $"*/{model.Interval} * * * *",
                      new RecurringJobOptions { TimeZone = TimeZoneInfo.Utc, QueueName = "default" });
                }
            }
            return Ok(result);
        }
        [HttpDelete]
        public async Task<IActionResult> DeleteTargetApp([FromQuery] Guid id)
        {
            var result = await _mediator.Send(new DeleteTargetAppCommand { Id = id });
            _jobManager.RemoveIfExists(id.ToString());
            return Ok(result);
        }

        public async Task RunHealthCheck(Guid id)
        {
            await _mediator.Send(new CreateHealthCheckResultCommand(id));
        }

    }
}
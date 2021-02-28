using System.Threading.Tasks;
using Application.Account.Commands.Login;
using Application.Account.Commands.Logout;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Web.Models;

namespace Web.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        public AccountController(ILogger<AccountController> logger, IMediator mediator, IMapper mapper)
        {
            _logger = logger;
            _mediator = mediator;
            _mapper = mapper;
        }
        [HttpGet]
        public IActionResult Login(string ReturnUrl = null)
        {
            return View(new LoginModel
            {
                ReturnUrl = ReturnUrl
            });
        }
        public IActionResult AccessDenied()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            var currentUser = await _mediator.Send(new LoginCommand { Email = model.Email, Password = model.Password });
            if (currentUser != null)
            {
                return Ok(model.ReturnUrl ?? "~/");
            }
            return BadRequest(currentUser);
        }

        [AllowAnonymous]
        public async Task<IActionResult> Logout()
        {
            await _mediator.Send(new LogoutCommand());
            return Redirect("~/");

        }

    }
}
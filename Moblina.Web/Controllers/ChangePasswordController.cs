using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Moblina.Common;
using Moblina.Model.Identity;
using Moblina.Service.Identity;
using System.Threading.Tasks;

namespace Moblina.Web.Controllers
{
    [Route("api/[controller]")]
    [EnableCors("CorsPolicy")]
    [ApiController]
    public class ChangePasswordController : Controller
    {
        private readonly IUsersService _usersService;
        public ChangePasswordController(IUsersService usersService)
        {
            _usersService = usersService;
            _usersService.CheckArgumentIsNull(nameof(usersService));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Post([FromBody]ChangePasswordDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var user = await _usersService.GetCurrentUserAsync();
            if (user == null)
            {
                return BadRequest("NotFound");
            }

            var (Succeeded, Error) = await _usersService.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
            if (Succeeded)
            {
                return Ok();
            }

            return BadRequest(Error);
        }
    }
}
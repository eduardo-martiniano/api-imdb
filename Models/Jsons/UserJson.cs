using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api_imdb.Models.Jsons
{
    public class UserJson : IActionResult
    {
        public string Email { get; set; }

        public UserJson(IdentityUser user)
        {
            Email = user.Email;
        }
        public async Task ExecuteResultAsync(ActionContext context)
        {
            await new JsonResult(this).ExecuteResultAsync(context);
        }
    }
}

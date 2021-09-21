using Providers.Fake.Data;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Providers.Fake.diiorderingsvc.Controllers
{
    
    [Route("diiorderingsvc/api/themes")]
    [ApiController]
    public class ThemesController : ControllerBase
    {
        // GET: api/<ThemesController>
        [HttpGet]
        public IEnumerable<WebSiteTheme> Get()
        {
            foreach (string themeName in Enum.GetNames(typeof(ThemeType)))
            {
                yield return new WebSiteTheme() { Name = themeName };
            };
        }
    }
}

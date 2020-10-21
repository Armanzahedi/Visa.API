using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Visa.Infrastructure.Helpers;
using Visa.Infrastructure.Services;

namespace Visa.API.Controllers
{
    [ApiKeyAuth]
    public class BaseController : ControllerBase
    {
        public readonly IUriService uriService;
        public BaseController(IUriService uriService)
        {
            this.uriService = uriService;
        }
    }
}

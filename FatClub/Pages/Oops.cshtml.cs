﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Diagnostics;
using System.Web;
using System.Net;
using Microsoft.AspNetCore.Http.Features;

namespace FatClub.Pages
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public class OopsModel : PageModel
    {
        public string RequestId { get; set; }


        public int iStatusCode { get; set; }
        public string Message { get; set; }
        public string StackTrace { get; set; }


        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public void OnGet()
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;


            // Get the details of the exception that occurred

            iStatusCode = Response.StatusCode;
            //StackTrace = exception.Error.StackTrace;


        }
    }
}

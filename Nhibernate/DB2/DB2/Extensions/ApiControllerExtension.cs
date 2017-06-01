using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace DB2.Extensions
{
    public static class ApiControllerExtension
    {
        public static bool ExtensionMethod(this ApiController apiController, string parameter)
        {
            return true;
        }
    }
}
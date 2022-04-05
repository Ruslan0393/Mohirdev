﻿using Microsoft.AspNetCore.Http;

namespace Mohirdev.Service.Helpers
{
    public class HttpContextHelper
    {
        public static IHttpContextAccessor Accessor;
        public static HttpContext Context => Accessor?.HttpContext;
        public static IHeaderDictionary ResponseHeaders => Context.Response.Headers;
    }
}

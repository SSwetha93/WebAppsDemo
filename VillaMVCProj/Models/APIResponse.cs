﻿using System.Net;

namespace VillaMVCProj.Models
{
    public class APIResponse
    {
        public HttpStatusCode StatusCode { get; set; }
        public bool IsSuccess { get; set; } = true;
        public List<String> ErrorMessages { get; set; }
        public object Result { get; set; }
    }
}

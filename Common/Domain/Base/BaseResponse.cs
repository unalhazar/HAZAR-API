﻿namespace Domain.Base
{
    public class BaseResponse
    {
        public bool Durum { get; set; } = false;
        public string Mesaj { get; set; } = "";
        public Exception Hata { get; set; } = null;
    }
}

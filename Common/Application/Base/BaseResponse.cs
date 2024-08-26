namespace Application.Base
{
    public class BaseResponse
    {
        public bool Durum { get; set; } = false;
        public string Mesaj { get; set; } = "";
        public Exception Hata { get; set; } = null;
        public System.Net.HttpStatusCode HttpStatusCode { get; set; } = System.Net.HttpStatusCode.BadRequest;
    }
}

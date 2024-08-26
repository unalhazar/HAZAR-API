namespace Application.Base
{
    public class ProcessResult<T> : BaseResponse where T : new()
    {
        public T? Result { get; set; } = new T();
    }
}

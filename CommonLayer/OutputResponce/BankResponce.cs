
// Model for ALL API Responces
namespace CommonLayer.OutputResponce
{
        public class Responce<T>
        {
            public bool Suceess { get; set; } = false;
            public string? Message { get; set; }
            public T? Data { get; set; }
        }
}

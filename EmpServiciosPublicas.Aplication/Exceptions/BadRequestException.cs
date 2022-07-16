namespace EmpServiciosPublicas.Aplication.Exceptions
{
    public class BadRequestException : ApplicationException
    {
        public BadRequestException(string? message) : base(message)
        {
        }
    }
}

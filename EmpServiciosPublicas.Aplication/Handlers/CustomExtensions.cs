namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class CustomExtensions
    {
        public static bool CheckNumber(this string name)
        {
            return name.All(Char.IsNumber);
        }
    }
}

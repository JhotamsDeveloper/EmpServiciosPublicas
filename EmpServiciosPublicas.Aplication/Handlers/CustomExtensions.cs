using EmpServiciosPublicas.Aplication.Constants;

namespace EmpServiciosPublicas.Aplication.Handlers
{
    public static class CustomExtensions
    {
        public static bool CheckNumber(this string name)
        {
            return name.All(Char.IsNumber);
        }

        public static bool IsEnumName(this string value)
        {
            bool result = false;

            Enum.GetValues(typeof(EnumPQRSD)).Cast<EnumPQRSD>().ToList().ForEach(item => { 
                if (item.ToString().ToUpper() == value.ToUpper())
                    result = true;
            });
            return result;
        }
    }
}

namespace ColoradoLuxury.Extensions
{
    public static class ConvertDecimalExtension
    {
        public static decimal TryParseDecimal(this string param)
        {
            if (decimal.TryParse(param, out decimal variable))
            {
                variable = decimal.Parse(param);
                return variable;
            }

            return -1;
            
        }
    }
}

namespace DesafioDevApi.Domain.Extensions
{
    public static class DecimalExtencion
    {
        public static decimal ProcessValue(this int type, decimal value)
        {
            return type switch
            {
                // Tipo Positivo
                1 or 4 or 5 or 6 or 7 or 8 => value,
                // Tipo Negativo
                2 or 3 or 9 => -value,
                _ => throw new ArgumentException("Invalid type"),
            };
        }
    }
}

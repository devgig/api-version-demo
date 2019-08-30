namespace Demo.Shared.Extensions
{
    public static class StringExtensions
    {
        public static int ToNumber(this string value)
        {
            return int.TryParse(value, out int n) ? n : 0;
        }


        public static decimal ToDecimal(this string value)
        {
            return decimal.TryParse(value, out decimal n) ? n : 0.00M;
        }
    }
}

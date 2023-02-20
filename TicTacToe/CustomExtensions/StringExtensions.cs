namespace TicTacToe.CustomExtensions
{
    public static class StringExtensions
    {
        public static string PadCenterExtension(this string str, int width, char padChar = ' ')
        {
            if (width <= str.Length) return str;
            int padding = width - str.Length;
            return str.PadLeft(str.Length + padding / 2, padChar)
                      .PadRight(width, padChar);
        }
    }
}

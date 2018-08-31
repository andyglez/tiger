using System.Text;

namespace Tiger
{
    public static class StringConstant
    {
        public static string EscapeText(string text)
        {
            var result = new StringBuilder();
            for (int i = 1; i < text.Length - 1; i++)
            {
                int ascii = text[i];
                if (text[i] == '\\')
                {
                    i++;
                    if (text[i] == '\"')
                        result.Append('\"');

                    else if (text[i] == '\\')
                        result.Append('\\');

                    else if (text[i] == 'n')
                        result.Append('\n');

                    else if (text[i] == 't')
                        result.Append('\t');

                    else if (text[i] == '^')
                        result.Append((char)(text[++i] - '@'));

                    else if (char.IsWhiteSpace(text[i]))
                        while (char.IsWhiteSpace(text[i]))
                            i++;

                    else if (char.IsDigit(text[i]))
                    {
                        int intValue = int.Parse(text.Substring(i, 3));
                        if (intValue >= 128)
                            return null;
                        result.Append((char)intValue);
                        i += 2;
                    }
                }
                else if (ascii < 32 || ascii > 126)
                    return null;
                else
                    result.Append(text[i]);
            }
            return result.ToString();
        }
    }
}

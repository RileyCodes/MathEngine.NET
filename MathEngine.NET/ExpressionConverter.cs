using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine.NET
{
    public static class ExpressionConverter
    {

        public static string ApplyStyle(string exp,Symbol symbol)
        {
            exp = AsColor(exp, symbol.style.color);
            if (symbol.style.strikethrough)
                exp = AsRemoved(exp);

            return exp;
        }

        public static string GetFullExpression(Symbol symbol)
        {
            var exp = symbol.GetExpression();
            return ApplyStyle(exp, symbol);
        }


        public static string AsBracket(string exp)
        {
            return "(" + exp + ")";
        }

        public static string AsPower(string exp)
        {
            return @"^{" + exp + "}";
        }

        public static string AsColor(string exp, Style.Color color)
        {
            return @"{\color{" + color + "}" + exp + "}";
        }

        public static string AsRemoved(string exp)
        {
            return @"\xcancel{" + exp + "}";
        }


    }
}

using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Linq;

namespace question_16._26
{
    /// <summary>
    /// Gorgeous calculator class that exposes a `Calc` function.
    /// the Calc function recursively reduces the expression untill a final
    /// result is returned. Amazing!
    /// </summary>
    public class Calculator
    {
        private static Dictionary<string, Func<decimal, decimal, decimal>> _funcs = new Dictionary<string, Func<decimal, decimal, decimal>>
        {
            { "*", (a, b) => a * b },
            { "+", (a, b) => a + b },
            { "/", (a, b) => a / b },
            { "-", (a, b) => a - b },
        };

        private static List<string> _opsInOrder = new List<string>
        {
            "*",
            "/",
            "+",
            "-"
        };

        // Regex to get the digits from the first side of an op split.
        private static Regex _firstArgRegex = new Regex("\\d+$");
        // Regex to get the digits from the second side of an op split.
        private static Regex _secondArgRegex = new Regex("^\\d+");

        public Calculator() {

        }

        public string Calc(string expression)
        {
            return Calc(expression, 0);
        }

        private string Calc(string expression, int opIndex = 0)
        {
            // Get the operation that is to be done.
            var op = GetOp(opIndex);
            // If there are no expressions left to process, calculation is complete.
            if (op == null) return expression;
            var split = expression.Split(op);

            if (split.Length > 1)
            {
                // If we have enough in the split, get the first and second arg.
                decimal first = Decimal.Parse(_firstArgRegex.Matches(split[0]).FirstOrDefault()?.Value);
                decimal second = Decimal.Parse(_secondArgRegex.Matches(split[1]).FirstOrDefault()?.Value);
                // Calculate the result of this part of the expression.
                var result = _funcs[op](first, second);
                // Replace the result back into the expression string.
                expression = GetReducedExpression(split, op, result);
            }

            // If we split and there was more than the one multiplication case
            // recurse but do not increase the operation.
            if (split.Length > 2) return Calc(expression, opIndex);
            // Recurse to next operation!
            return Calc(expression, opIndex + 1);
        }

        private static string GetReducedExpression(string[] split, string op, decimal result)
        {
            string cleanedFirstSide = _firstArgRegex.Replace(split[0], "");
            string cleanedSecondSide = _secondArgRegex.Replace(split[1], "");
            string expr = $"{cleanedFirstSide}{result}{cleanedSecondSide}";
            // Are there more of the same operation to do? if so insert the op char back into the expr.
            string more = split.Length > 2 ? op : "";
            return $"{expr}{more}{String.Join(more, split.Skip(2))}";
        }

        private static string GetOp(int opIndex = 0)
        {
            if (opIndex >= _opsInOrder.Count) return null;
            return _opsInOrder[opIndex];
        }
    }
}
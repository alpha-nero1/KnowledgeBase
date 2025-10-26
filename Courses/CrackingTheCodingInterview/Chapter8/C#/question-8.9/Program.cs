using System.Collections.Generic;
using System;
using Core;

/*
    8.9 Parens: Implement an algorithm to print all valid (i.e., properly opened and closed) combinations 
    of n pairs of parentheses. 
    EXAMPLE 
    Input: 3 
    Output: (( () ) ) , ( () () ) , ( () ) () , () ( () ) , () () ()
*/

namespace question_8._9
{
    class Program
    {
        static void Main(string[] args)
        {
            var parenthesis = GenerateParens(4);
            Console.WriteLine("Parenthesis = \n");
            foreach (var p in parenthesis)
            {
                Console.Write(p);
                Console.Write(",");
            }
        }

        static void AddParen(
            List<string> list, 
            int leftRem, 
            int rightRem,
            char[] str,
            int index
        )
        {
            if (leftRem < 0 || rightRem < leftRem) return; // Invalid state.
            if (leftRem == 0 && rightRem == 0)
            {
                  
                list.Add(new string(str.DirectCopy()));
            }
            else
            {
                // Pretty much uses a binary tree traversal to find all possible pairs...
                str[index] = '('; // Add a left and recurse.
                AddParen(list, leftRem - 1, rightRem, str, index + 1);

                str[index] = ')'; // Add a left and recurse.
                AddParen(list, leftRem, rightRem - 1, str, index + 1);
            }
        }

        static List<string> GenerateParens(int count)
        {
            char[] str = new char[count * 2];
            var list = new List<string>();
            AddParen(list, count, count, str, 0);
            return list;
        }
    }
}

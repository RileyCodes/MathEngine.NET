using System;
using System.Collections.Generic;
using System.Text;

namespace MathEngine.NET
{
    class NotConstException : Exception
    {
    }

    class ExpandMultiplicationException : Exception
    {
    }

    class RightSideNotEquationException : ExpandMultiplicationException
    {
    }

    class EquationLessOrMoreThen1Exception : ExpandMultiplicationException
    {
    }

    class WrongDgreeInsideBracketException : ExpandMultiplicationException
    {
    }

    class InvalidUserInputException : Exception
    {
        public InvalidUserInputException(string msg)
        {

        }
    }


    //
}

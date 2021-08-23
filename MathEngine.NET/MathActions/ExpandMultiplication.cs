using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEngine.NET.MathActions
{

    public class ExpandMultiplication : MathAction
    {
        public ExpandMultiplication(MathContext mathContext) : base(mathContext)
        {
        }

        bool IsPolynomial(List<Term> terms)
        {
            //仅能存在常量和变量
            return terms.All(term =>
                term.Symbol.symbolType == Symbol.SymbolType.Variable ||
                term.Symbol.symbolType == Symbol.SymbolType.Constant);
        }

        bool IsNormalOperator(List<Operator> operators)
        {
            //仅能存在加减乘除
            return operators.All(@operator =>
                @operator.symbolType == Symbol.SymbolType.Add ||
                @operator.symbolType == Symbol.SymbolType.Minus ||
                @operator.symbolType == Symbol.SymbolType.Multiply ||
                @operator.symbolType == Symbol.SymbolType.Fraction);
        }

        public Equation Run(Equation src)
        {
            //展开X(X+B+C)的情况


            //至少要有2个项
            if (src.terms.Count < 2)
            {
                throw new InvalidUserInputException("two terms required");
            }

            //右边必须是方程项，方程项="(xxxx)"
            if (src.terms.Last().Symbol.symbolType != Symbol.SymbolType.Equation)
            {
                throw new RightSideNotEquationException();
            }

            //必须有且只有一个方程项
            if (src.terms.Count(term => term.Symbol.symbolType == Symbol.SymbolType.Equation) != 1)
            {
                throw new EquationLessOrMoreThen1Exception();
            }


            //括号方程的指数必须为1
            if (src.terms.Last().Dgree.AsConst_TryGetValue() == 1)
            {
                throw new WrongDgreeInsideBracketException();
            }

            //取方程中除了右边所有的项（最右边是括号方程）
            var pureLeftSideEquation = src.terms.GetRange(0, src.terms.Count - 1);

            //左侧必须全部是变量或常量
            if (!IsPolynomial(pureLeftSideEquation))
            {
                throw new InvalidUserInputException("const or var only");
            }
            //if(!IsNormalOperator(pureLeftSideEquation))


            var equationInsideBracket = (Equation) src.terms.Last().Symbol;
            //括号内必须全部是变量或常量
            if (!IsPolynomial(equationInsideBracket.terms))
            {
                throw new InvalidUserInputException("const or var only");
            }



            Equation resultEquation = new Equation();
            var step1 = src.Copy();
            mathContext.logMgr.PushEquation(step1);


            return resultEquation;
        }
    }
}

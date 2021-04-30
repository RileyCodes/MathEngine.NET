using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MathEngine.NET.MathActions
{

    interface ICombineConstant : IMathAction
    {
        Equation Run(Equation src);
    }

    public class CombineConstant : MathAction, ICombineConstant
    {
        public CombineConstant(MathContext mathContext) : base(mathContext)
        {
        }

        static bool IsConstant(Term term)
        {
            if (term.Symbol.symbolType != Symbol.SymbolType.Constant)
            {
                return false;
            }

            if (term.Dgree.symbolType != Symbol.SymbolType.Constant)
            {
                return false;
            }

            return true;
        }
        //static bool IsOperBothConst(Operator @Operator)
        //{
        //    return IsConstant(@Operator.left) && IsConstant(@Operator.right);
        //}

        public Equation Run(Equation src)
        {
            //Combine all constant terms
            //Ex: 3+3 = 6, 3^2*3^2 = 3^4

            Equation resultEquation = new Equation();

            mathContext.logMgr.PushEquation(src);

            var step1Equation = src.Copy();

            var constTermSrc = step1Equation.terms.FirstOrDefault(IsConstant);
            constTermSrc.Symbol.style.color = Style.Color.red;

            mathContext.logMgr.PushEquation(step1Equation);

            int x = 0;


            //var constTermDst = step1Equation.terms.FirstOrDefault(term =>

            //    constTermSrc.Symbol.AsConst_GetValue() ==  term.Symbol.AsConst_GetValue() && 
            //    constTermSrc != term);

            //constTermDst.Symbol.style.color = Style.Color.blue;




            //foreach (var @operator in src.operators)
            //{
            //    if (!IsOperBothConst(@operator))
            //        continue;

            //    Term combinedTerm = new Term();

            //    var leftSymbolConst = (@operator.left.Symbol as Constant);
            //    var leftDgreeConst = (@operator.left.Dgree as Constant);

            //    var rightSymbolConst = (@operator.right.Symbol as Constant);
            //    var rightDgreeConst = (@operator.right.Dgree as Constant);




            //    //combinedTerm.Dgree = new Constant()

            //}

            return resultEquation;
        }
    }
}

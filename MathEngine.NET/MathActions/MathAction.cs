using System;
using System.Collections.Generic;
using System.Text;
using MathEngine.NET;
using MathEngine.NET.MathActions;

namespace MathEngine.NET
{
    public interface IMathAction
    {

    }
    public class MathAction: IMathAction
    {
        protected MathContext mathContext = null;

        public MathAction(MathContext mathContext)
        {
            this.mathContext = mathContext;
        }
        //public static Equation Simplify(MathContext mathContext, Equation src)
        //{
        //    Equation SimplifyEquation = new Equation();

        //    var res = CombineConstant.Run(mathContext, src);

        //    return SimplifyEquation;
        //}
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathEngine.NET.MathActions;
using Microsoft.AspNetCore.SignalR;

namespace MathEngine.NET.Hubs
{
    public class MathHub : Hub
    {
        public async Task Run(string message)
        {
            MathContext mathContext = new MathContext(new LogMgr(Clients));
            var constCombine = new CombineConstant(mathContext);
            var expend = new ExpandMultiplication(mathContext);

            var testEquation = Equation.Create(() =>
            {
                
                Equation testEquation = new Equation();

                testEquation.AddTerm(new Term()
                {
                    Symbol = new Constant(0),
                    Dgree = new Constant(1)
                }, new OpMinus());

                testEquation.AddTerm(new Term()
                {
                    Symbol = new Constant(2),
                    Dgree = new Constant(1)
                }, new OpMultiply());

                testEquation.AddTerm(new Term()
                {
                    Symbol = new Variable("y"),
                    Dgree = new Constant(-3)
                }, new OpMultiply());


                var inside = Equation.Create(() =>
                {
                    Equation insideEquation = new Equation();
                    insideEquation.AddTerm(new Term()
                    {
                        Symbol = new Variable("y"),
                        Dgree = new Constant(1)
                    }, new OpAdd());


                    insideEquation.AddTerm(new Term()
                    {
                        Symbol = new Constant(5),
                        Dgree = new Constant(1)
                    }, new OpMultiply());

                    insideEquation.AddTerm(new Term()
                    {
                        Symbol = new Variable("y"),
                        Dgree = new Constant(3)
                    },new OpMultiply());

                    return insideEquation;
                });


                testEquation.AddTerm(new Term()
                {
                    Symbol = inside,
                    Dgree = new Constant(1)
                }, new OpNone());

                return testEquation;
            });

            expend.Run(testEquation);

            var copy = testEquation.Copy();
            string exp = testEquation.GetExpression();


            
            //await Clients.All.SendAsync("ReceiveMessage", "message2");

            //const string fileName = @"C:\Users\user\source\repos\MathHomework\MathHomework\Temp\formula.png";
            //var parser = new TexFormulaParser();
            //var formula = parser.Parse(exp);



            //var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
            //File.WriteAllBytes(fileName, pngBytes);



            //Console.WriteLine();





            //var x = Expr.Variable("x");
            //var y = Expr.Variable("y");

            //var exp = Infix.Parse("2/1/(a*b)");

            //Console.WriteLine(exp.ResultValue.ToString());

            //const string latex = @"\lim_{x\to\infty} f(x)";
            //const string fileName = @"C:\Users\user\source\repos\MathHomework\MathHomework\Temp\formula.png";

            //var parser = new TexFormulaParser();
            //var formula = parser.Parse(latex);
            //var pngBytes = formula.RenderToPng(20.0, 0.0, 0.0, "Arial");
            //File.WriteAllBytes(fileName, pngBytes);
            //Console.WriteLine("Hello World!");

        }
    }
}

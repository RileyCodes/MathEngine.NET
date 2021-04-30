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

            var testEquation = Equation.Create(() =>
            {
                Equation testEquation = new Equation();
                testEquation.AddTerm(new Term()
                {
                    Symbol = new Variable("x"),
                    Dgree = new Constant(2)
                }, new OpAdd());

                testEquation.AddTerm(new Term()
                {
                    Symbol = new Constant(123),
                    Dgree = new Constant(1)
                });

                return testEquation;
            });

            constCombine.Run(testEquation);

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

using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;


namespace MathEngine.NET
{
    public class Style
    {
        public enum Color
        {
            red,
            blue,
            none
        }

        public Color color = Color.none;
        public bool strikethrough = false;
    }

    public abstract class Symbol
    {
        public Style style = new Style();
        internal enum SymbolType
        {
            Variable,
            Constant,
            Add,
            Minus,
            Multiply,
            Equation,
            Fraction,
            Term,
            None
        }
        internal SymbolType symbolType;

        public virtual string GetExpression()
        {
            return "null";
        }

        public double AsConst_GetValue()
        {
            if (symbolType != SymbolType.Constant)
            {
                throw new NotConstException();
            }

            return (this as Constant).value;

        }

        public double? AsConst_TryGetValue()
        {
            return (this as Constant)?.value;
        }
    }

    abstract class NamedSymbol : Symbol
    {
        public string identifier;

        public override string GetExpression()
        {
            return identifier;
        }
    }


    class Variable : NamedSymbol
    {
        public Variable(string Identifier)
        {
            this.identifier = Identifier;
            this.symbolType = SymbolType.Variable;
        }
    }

    class Constant : NamedSymbol
    {
        public double value;
        public Constant(double value)
        {
            this.value = value;
            this.identifier = value.ToString();
            this.symbolType = SymbolType.Constant;
        }
    }

    class Fraction : Symbol
    {
        private Equation Numerator, Denominator;
        public Fraction(Equation Numerator, Equation Denominator)
        {
            this.Numerator = Numerator;
            this.Denominator = Denominator;
        }
    }

    public class Term : ILinkedUnits
    {
        public Symbol Symbol;
        public Symbol Dgree;
        public Operator Prev;
        public Operator Next;

        public Term()
        {
            //symbolType = SymbolType.Term;
        }

        public object GetPrev()
        {
            return Prev;
        }

        public object GetNext()
        {
            return Prev;
        }

    }

    public interface ILinkedUnits
    {
        public object GetPrev();
        public object GetNext();
    }

    public abstract class Operator : Symbol, ILinkedUnits
    {
        public Term Prev;
        public Term Next;

        public object GetPrev()
        {
            return Prev;
        }
        public object GetNext()
        {
            return Prev;
        }
    }

    class OpAdd : Operator
    {
        public OpAdd()
        {
            this.symbolType =  SymbolType.Add;
        }


        public override string GetExpression()
        {
            return " + ";
        }
    }

    class OpNone : Operator
    {
        public OpNone()
        {
            this.symbolType = SymbolType.None;
        }


        public override string GetExpression()
        {
            return "";
        }
    }

    class OpMultiply : Operator
    {
        public OpMultiply()
        {
            this.symbolType = SymbolType.Multiply;
        }


        public override string GetExpression()
        {
            return " * ";
        }
    }

    class OpMinus : Operator
    {
        public OpMinus()
        {
            this.symbolType = SymbolType.Minus;
        }
        public override string GetExpression()
        {
            return "-";
        }
    }


    public class Equation : Symbol
    {

        public Equation()
        {
            this.symbolType = SymbolType.Equation;
        }
        //private static Operator noneOperator = new Operator(Operator.OperatorType.None);

        private Func<Equation> MakeEquation = null;
        public List<Term> terms = new List<Term>();
        public List<Operator> operators = new List<Operator>();

        void LinkLastUnit()
        {
            //operators[^2]
            operators[^1].Prev = terms[^1];

            if (operators.Count >= 2)
                terms[^1].Prev = operators[^2];

            EquationWalker.Walk((unit =>
            {
                var term = (Term) unit;
                term.Next = operators[^1];
            }), operators[^1],EquationWalker.WalkDirection.Left,0,1);

            EquationWalker.Walk((unit =>
            {
                var oper = (Operator) unit;
                oper.Next = terms[^1];
            }), operators[^1],EquationWalker.WalkDirection.Left,0,2,EquationWalker.WalkMode.EndOnly);

            int x = 0;
        }
        public void AddTerm(Term term, Operator op)
        {
            operators.Add(op);

            terms.Add(term);
            LinkLastUnit();
        }

        public void Add(Term left, Term right)
        {

        }

        Equation SubEquation(int index,int count = -1)
        {
            var copy = Copy();
            EquationWalker.Walk(null, terms[0], EquationWalker.WalkDirection.Right, index, count);

            return copy;
        }

        public Equation Copy()
        {
            return Equation.Create(MakeEquation);
        }

        public static Equation Create(Func<Equation> makeEquation)
        {
            var equation = makeEquation.Invoke();

            equation.MakeEquation = makeEquation;
            return equation;
        }

        private bool IsTermDgree1(Term term)
        {
            return term.Dgree.AsConst_TryGetValue() == 1;
        }

        public override string GetExpression()
        {
            string expression = "";
            for(var i=0; i < terms.Count; i++)
            {

                var symbolExp = ExpressionConverter.GetFullExpression(terms[i].Symbol);

                expression += symbolExp;

                if (!IsTermDgree1(terms[i]))
                {
                    var powerExp = ExpressionConverter.AsPower(ExpressionConverter.GetFullExpression(terms[i].Dgree));

                    expression += powerExp;
                }


                if (i < operators.Count)
                {
                    var operExp = ExpressionConverter.GetFullExpression(operators[i]);
                    expression += operExp;
                }

            }
            return ExpressionConverter.AsBracket(expression);
        }
    }

}

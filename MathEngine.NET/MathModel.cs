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

    public class Term
    {
        public Symbol Symbol;
        public Symbol Dgree;
    }

    public abstract class Operator : Symbol
    {

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


    public class Equation : Symbol
    {
        //private static Operator noneOperator = new Operator(Operator.OperatorType.None);

        private Func<Equation> MakeEquation = null;
        public List<Term> terms = new List<Term>();
        public List<Operator> operators = new List<Operator>();
        public void AddTerm(Term term, Operator op = null)
        {
            if (op != null)
            {
                //op.left = terms[^1];
                //op.right = term;
                operators.Add(op);
            }
            terms.Add(term);
        }

        public void Add(Term left, Term right)
        {

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

        public string GetExpression()
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
            return expression;
        }
    }

}

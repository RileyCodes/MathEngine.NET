using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MathEngine.NET
{
    public class EquationWalker
    {
        public enum WalkDirection
        {
            Left,
            Right
        }

        public enum WalkMode
        {
            InvokeAll,
            EndOnly
        }

        public EquationWalker()
        {

        }

        public static object Walk(Action<object> action, ILinkedUnits unit,
            WalkDirection walkDirection,int index =0, int count = -1, 
            WalkMode walkMode = WalkMode.InvokeAll)
        {
            int step = 0;

            object next = unit;
            while (step < index + count)
            {
                if (walkDirection == WalkDirection.Left)
                {
                    next = ((ILinkedUnits)next).GetPrev();
                }
                else
                {
                    next = ((ILinkedUnits)next).GetNext();
                }

                if (next == null)
                    break;

                if(step < index)
                {
                    continue;
                }


                if (walkMode == WalkMode.InvokeAll)
                {
                    action?.Invoke(next);
                }

                if (walkMode == WalkMode.EndOnly)
                {
                    if(step == count -1)
                        action?.Invoke(next);
                }

                step++;
            }

            return next;
        }

    }
}

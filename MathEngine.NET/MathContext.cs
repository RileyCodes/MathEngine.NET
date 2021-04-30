using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MathEngine.NET.MathActions;
using Microsoft.AspNetCore.SignalR;

namespace MathEngine.NET
{
    public class MathContext
    {
        public LogMgr logMgr;
        public MathContext(LogMgr logMgr)
        {
            this.logMgr = logMgr;
        }

        
    }
}

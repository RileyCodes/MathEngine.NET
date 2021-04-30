using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

namespace MathEngine.NET
{
    public class LogMgr
    {
        private IHubCallerClients _clients = null;

        public LogMgr(IHubCallerClients clients)
        {
            _clients = clients;
        }


        public void PushEquation(Equation equation)
        {
            
            PushLog(equation.GetExpression());
        }

        public void PushLog(string msg)
        {
            //TODO: If send more then one log ,will they  arrive ordered?
            _clients.All.SendAsync("ReceiveMessage", msg);
        }
    }
}

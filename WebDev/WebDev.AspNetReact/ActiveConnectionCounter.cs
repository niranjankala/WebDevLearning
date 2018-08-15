using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace WebDev.AspNetReact
{
    public class ActiveConnectionCounter : Hub
    {
        static int activeConnections = 0;
        public void NotifyNewUser()
        {
            activeConnections += 1;
            //Notify all users about the active users by passing value to a dynamic method
            Clients.All.onAtiveConnectionChanged(activeConnections);
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            activeConnections -= 1;
            //Notify all users about the active users by passing value to a dynamic method
            Clients.All.onAtiveConnectionChanged(activeConnections);
            return base.OnDisconnected(stopCalled);
        }
    }   
}
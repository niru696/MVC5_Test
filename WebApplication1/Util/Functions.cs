﻿using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Hubs;

namespace WebApplication1.Util
{
    public class Functions
    {
        public static void SendProgress(string progressMessage, int progressCount, int totalItems)
        {
            //IN ORDER TO INVOKE SIGNALR FUNCTIONALITY DIRECTLY FROM SERVER SIDE WE MUST USE THIS
            var hubContext = GlobalHost.ConnectionManager.GetHubContext<ProgressHub>();

            //CALCULATING PERCENTAGE BASED ON THE PARAMETERS SENT
            var percentage = (progressCount * 100) / totalItems;

            //PUSHING DATA TO ALL CLIENTS
            hubContext.Clients.All.AddProgress(progressMessage, percentage + "%");
        }
    }
}
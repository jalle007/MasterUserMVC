using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MasterUserMVC.Hubs {
  public class MyHub : Hub {
    public void Send(string name, string message) {
      // Call the addNewMessageToPage method to update clients.
      //Clients.All.broadcastMessage(name, message);

      Clients.All.addNewMessageToPage(name, message);
    }
  }
}
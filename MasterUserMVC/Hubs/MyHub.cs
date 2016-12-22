using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.SignalR;

namespace MasterUserMVC.Hubs {
  public class MyHub : Hub {

    AES myAES = new AES();
    byte[]  passBytes = new byte[] { 1, 2, 3, 4, 5, 6, 7, 8 };
    private String encrypted = "";

    public void Send(string name, string message) {
      // Call the addNewMessageToPage method to update clients.
      //Clients.All.broadcastMessage(name, message);

      Clients.All.addNewMessageToPage(name, message);
    }

    public void aesEncrypt(string   message) {
      // Call the addNewMessageToPage method to update clients.
      var result = myAES.Encrypt(message, passBytes);
      encrypted = result;

      Clients.All.sendEncrypted(  result);
    }

    public void aesDecrypt(string message) {
      // Call the addNewMessageToPage method to update clients.
      var result = myAES.Decrypt(message, passBytes);

      Clients.All.sendDecrypted(  result);

    }

  }
}
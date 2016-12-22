(function ($) {
  
    // Reference the auto-generated proxy for the hub.
  var chat = $.connection.myHub;
  //console.log("chat", chat);

    chat.client.addNewMessageToPage = function (name, message) {
      // Add the message to the page. 
    
      console.log("addNewMessageToPage", name, message);
      var usrName = document.getElementById('username').value;
      console.log("usrName", usrName);
     
      if (name == usrName) {
        console.log("logOff", name);
      document.getElementById('logoutForm').submit();
      }
    };

    $.connection.hub.start().done(function () {
     
     // console.log("hub.start");
     // chat.server.send("1", "2");
    });
 
}(jQuery));
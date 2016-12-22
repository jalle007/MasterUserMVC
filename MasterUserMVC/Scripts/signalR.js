(function ($) {
  
    // Reference the auto-generated proxy for the hub.
  var signalr = $.connection.myHub;
  console.log("signalr", signalr);

    signalr.client.addNewMessageToPage = function (name, message) {
      // Add the message to the page. 
    
      console.log("addNewMessageToPage", name, message);
      var usrName = document.getElementById('username').value;
      console.log("usrName", usrName);
     
      if (name == usrName) {
        console.log("logOff", name);
      document.getElementById('logoutForm').submit();
      }
    };


    signalr.client.sendEncrypted = function ( message) {
      console.log("Encrypted: ", message);
      $("#Encrypted").val(message);
    };

    signalr.client.sendDecrypted = function ( message) {
      console.log("Decrypted: ", message);
      $("#Decrypted").val(message);
    };

    $.connection.hub.start().done(function () {

      // we set onclick event for buttons on the page (this can be moved to users page)
      var btEncrypt = document.getElementById("btEncrypt");
      var btDecrypt = document.getElementById("btDecrypt");

      if (btEncrypt != null) {
        btEncrypt.onclick = function () {
          var msg = $("#Message").val();
          console.log("msg", msg);

          signalr.server.aesEncrypt(msg);
          
          //counter
          var counter = 5;
          var myInterval = setInterval(function () {
            console.log("counter", counter);
            $("#Decrypted").val(counter);
            --counter;

            if (counter == 0) clearInterval(myInterval);
          }, 1000);
           
          


          //delay 5 s
          setTimeout(function () {
            var msg = $("#Encrypted").val();
            console.log("msg", msg);

            signalr.server.aesDecrypt(msg);
          }, 5000);

        };
      }

      if (btDecrypt != null) {
        btDecrypt.onclick = function () {
          var msg = $("#Encrypted").val();
          console.log("msg", msg);

          signalr.server.aesDecrypt(msg);
        };
      }


    });
 
}(jQuery));
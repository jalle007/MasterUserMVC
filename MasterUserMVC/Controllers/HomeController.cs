using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MasterUserMVC.Hubs;
using MasterUserMVC.Models;
using Microsoft.AspNet.SignalR;

namespace MasterUserMVC.Controllers {

  public class HomeController : Controller 
    {
    public ActionResult Index() {return View();}

    //[Authorize(Roles = "Admin")]
    [System.Web.Mvc.Authorize]
    public ActionResult Disconnect(String email)
    {
      AccountController.removeFromCache(email);

      var context = GlobalHost.ConnectionManager.GetHubContext<MyHub>();
      context.Clients.All.addNewMessageToPage(email, "logoff");


      return RedirectToAction("Admin", "Home");
    }

    //[Authorize(Roles = "Admin")]
    [System.Web.Mvc.Authorize]
    public ActionResult Admin() {
      this.HttpContext.Response.AddHeader("refresh", "3; url=" + Url.Action("Admin"));

      List<string> loggedInUsers = (List<string>)HttpRuntime.Cache["LoggedInUsers"];

      return View();
    }

    [System.Web.Mvc.Authorize]
    [ActionName("User")]
    public ActionResult UserAction(EncryptionModel data) {
      return View();
    }

    [System.Web.Mvc.Authorize]
    [HttpPost]
    [ActionName("User")]
    public ActionResult UserActionPost(EncryptionModel data) {

      return View(data);
    }
  }
}
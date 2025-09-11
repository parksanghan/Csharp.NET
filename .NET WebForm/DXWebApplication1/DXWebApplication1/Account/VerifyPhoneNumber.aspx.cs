using System;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using System.Threading.Tasks;
using DXWebApplication1.Models;

namespace DXWebApplication1 {
    public partial class VerifyPhoneNumber : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var phonenumber = Request.QueryString["PhoneNumber"];
            var code = manager.GenerateChangePhoneNumberToken(User.Identity.GetUserId(), phonenumber);           
            HiddenField["PhoneNumber"] = phonenumber;
        }

        protected void Code_Click(object sender, EventArgs e)
        {
            if (!ModelState.IsValid)
            {
                Code.ErrorText = "Invalid code";
                Code.IsValid = false;
                return;
            }

            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();

            var result = manager.ChangePhoneNumber(User.Identity.GetUserId(), HiddenField["PhoneNumber"].ToString(), Code.Text);

            if (result.Succeeded)
            {
                var appUser = manager.FindById(User.Identity.GetUserId());

                if (appUser != null)
                {
                    signInManager.SignIn(appUser, isPersistent: false, rememberBrowser: false);
                    Response.Redirect("/Account/Manage.aspx?m=AddPhoneNumberSuccess");
                }
            }

            // If we got this far, something failed, redisplay form
            Code.ErrorText = "Invalid code";
            Code.IsValid = false;
        }
    }
}
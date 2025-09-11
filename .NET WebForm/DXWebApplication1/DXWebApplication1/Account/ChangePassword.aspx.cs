using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DXWebApplication1;


namespace DXWebApplication1 {
    public partial class ChangePassword : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();

            if(!IsPostBack && !manager.HasPassword(User.Identity.GetUserId()))
            {
                PageHeader.InnerText = "Set password";
                PageDescription.InnerText = "You do not have a local password for this site. Add a local password so you can log in without an external login.";
                tbCurrentPassword.Visible = false;
                btnChangePassword.Visible = false;
                btnSetPassword.Visible = true;
            }
        }

        protected void btnChangePassword_Click(object sender, EventArgs e) {
            var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            var signInManager = Context.GetOwinContext().Get<ApplicationSignInManager>();
            IdentityResult result = manager.ChangePassword(User.Identity.GetUserId(), tbCurrentPassword.Text, tbPassword.Text);
            if (result.Succeeded)
            {
                var user = manager.FindById(User.Identity.GetUserId());
                signInManager.SignIn( user, isPersistent: false, rememberBrowser: false);
                Response.Redirect("~/Account/Manage.aspx?m=ChangePwdSuccess");
            }
            else
            {
                ErrorMessage.Text = result.Errors.FirstOrDefault();
            }
        }

        protected void btnSetPassword_Click(object sender, EventArgs e) {
            if(IsValid) 
            {
                // Create the local login info and link the local account to the user
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                IdentityResult result = manager.AddPassword(User.Identity.GetUserId(), tbPassword.Text);
                if(result.Succeeded) 
                {
                    Response.Redirect("~/Account/Manage.aspx?m=SetPwdSuccess");
                } 
                else 
                {
                    ErrorMessage.Text = result.Errors.FirstOrDefault();
                }
            }
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Security;
using Microsoft.AspNet.Identity.Owin;

namespace DXWebApplication1 {
    public partial class Login : System.Web.UI.Page {
        protected void Page_Load(object sender, EventArgs e) {
            
        }

        protected void btnLogin_Click(object sender, EventArgs e) {
            if(IsValid) {
                // Validate the user password
                var manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
                var signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();

                // This doen't count login failures towards account lockout
                // To enable password failures to trigger lockout, change to shouldLockout: true
                var result = signinManager.PasswordSignIn(tbUserName.Text, tbPassword.Text, isPersistent: false, shouldLockout: false);

                switch(result) {
                    case SignInStatus.Success:
                        IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                        break;
                    case SignInStatus.LockedOut:
                        Response.Redirect("~/Account/Lockout.aspx");
                        break;
                    case SignInStatus.RequiresVerification:
                        Response.Redirect(String.Format("/Account/TwoFactorAuthenticationSignIn.aspx?ReturnUrl={0}&RememberMe={1}",
                                                        Request.QueryString["ReturnUrl"],
                                                        false),
                                          true);
                        break;
                    case SignInStatus.Failure:
                    default:
                        tbUserName.ErrorText = "Invalid user";
                        tbUserName.IsValid = false;
                        break;
                }
            }
        }
    }
}
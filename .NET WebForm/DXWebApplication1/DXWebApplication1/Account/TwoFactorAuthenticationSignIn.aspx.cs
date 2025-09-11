using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using DXWebApplication1.Models;

namespace DXWebApplication1 {
    public partial class TwoFactorAuthenticationSignIn : System.Web.UI.Page
    {
        private ApplicationSignInManager signinManager;
        private ApplicationUserManager manager;

        public TwoFactorAuthenticationSignIn()
        {
            manager = Context.GetOwinContext().GetUserManager<ApplicationUserManager>();
            signinManager = Context.GetOwinContext().GetUserManager<ApplicationSignInManager>();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            var userId = signinManager.GetVerifiedUserId<ApplicationUser, string>();
            if (userId == null)
            {
                Response.Redirect("/Account/Error", true);
            }
            if(!IsPostBack) {
                var userFactors = manager.GetValidTwoFactorProviders(userId);
                Providers.DataSource = userFactors.Select(x => x).ToList();
                Providers.DataBind();
                Providers.SelectedIndex = 0;
            }
        }

        protected void CodeSubmit_Click(object sender, EventArgs e)
        {
            bool rememberMe = false;
            bool.TryParse(Request.QueryString["RememberMe"], out rememberMe);
            
            var result = signinManager.TwoFactorSignIn<ApplicationUser, string>(HiddenField["Provider"].ToString(), Code.Text, isPersistent: rememberMe, rememberBrowser: RememberBrowser.Checked);
            switch (result)
            {
                case SignInStatus.Success:
                    IdentityHelper.RedirectToReturnUrl(Request.QueryString["ReturnUrl"], Response);
                    break;
                case SignInStatus.LockedOut:
                    Response.Redirect("/Account/Lockout.aspx");
                    break;
                case SignInStatus.Failure:
                default:
                    Code.ErrorText = "Invalid Code";
                    Code.IsValid = false;
                    break;
            }
        }

        protected void ProviderSubmit_Click(object sender, EventArgs e)
        {
            if (!signinManager.SendTwoFactorCode(Providers.SelectedItem.Value.ToString()))
            {
                Response.Redirect("/Account/Error");
            }

            var user = manager.FindById(signinManager.GetVerifiedUserId<ApplicationUser, string>());
            if (user != null)
            {
                var code = manager.GenerateTwoFactorToken(user.Id, Providers.SelectedItem.Value.ToString());
            }

            HiddenField["Provider"] = Providers.SelectedItem.Value.ToString();
            sendcode.Visible = false;
            verifycode.Visible = true;
        }
    }
}
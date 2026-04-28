using System;
using System.Web.UI;
using AssetFlow.Legacy.Web.Auth;

namespace AssetFlow.Legacy.Web
{
    public partial class Logout : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            FormsAuthHelper.SignOut(Response);
            Response.Redirect("~/Login.aspx");
        }
    }
}
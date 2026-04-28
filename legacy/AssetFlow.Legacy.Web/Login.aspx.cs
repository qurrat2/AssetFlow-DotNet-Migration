using System;
using System.Threading.Tasks;
using System.Web.UI;
using AssetFlow.Core.Repositories;
using AssetFlow.Legacy.Web.App_Start;
using AssetFlow.Legacy.Web.Auth;

namespace AssetFlow.Legacy.Web;

public partial class Login : Page
{
    protected async void BtnLogin_Click(object sender, EventArgs e)
    {
        var users = WebFormsContainer.Resolve<IUserRepository>();
        var user = await users.GetByUsernameAsync(TxtUsername.Text);
        if (user == null || !BCrypt.Net.BCrypt.Verify(TxtPassword.Text, user.PasswordHash))
        {
            LblError.Text = "Invalid credentials";
            return;
        }
        FormsAuthHelper.SignIn(Response, user);
        Response.Redirect("~/Default.aspx");
    }
}
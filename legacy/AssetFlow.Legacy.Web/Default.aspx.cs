using System;
using System.Threading.Tasks;
using System.Web.UI;
using AssetFlow.Core.Entities;
using AssetFlow.Core.Services;
using AssetFlow.Legacy.Web.App_Start;
using AssetFlow.Legacy.Web.Auth;

namespace AssetFlow.Legacy.Web;

public partial class Default : Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (IsPostBack) return;
        var current = FormsAuthHelper.Current();
        if (current == null) { Response.Redirect("~/Login.aspx"); return; }

        var svc = WebFormsContainer.Resolve<IAssetService>();
        var user = new User
        {
            Id = current.Value.UserId,
            Role = current.Value.Role,
            DepartmentId = current.Value.DepartmentId
        };
        GridAssets.DataSource = await svc.ListForUserAsync(user);
        GridAssets.DataBind();
    }
}
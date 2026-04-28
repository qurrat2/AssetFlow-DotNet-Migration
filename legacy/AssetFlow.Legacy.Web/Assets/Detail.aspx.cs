using AssetFlow.Core.Repositories;
using AssetFlow.Legacy.Web.App_Start;
using AssetFlow.Legacy.Web.Auth;
using System;
using System.Web.UI;

namespace AssetFlow.Legacy.Web.Assets;

public partial class Detail : Page
{
    protected async void Page_Load(object sender, EventArgs e)
    {
        if (!int.TryParse(Request.QueryString["id"], out var id)) return;
        var repo = WebFormsContainer.Resolve<IAssetRepository>();
        var asset = await repo.GetByIdAsync(id);
        if (asset == null) { LitTag.Text = "(not found)"; return; }
        LitTag.Text = asset.Tag;
        LitStatus.Text = asset.Status;
        LitDesc.Text = asset.Description;

        var current = FormsAuthHelper.Current();
        if (current?.Role == "Admin" && asset.Status == "Available")
        {
            LnkAssign.NavigateUrl = $"~/Assets/Assign.aspx?assetId={id}";
            LnkAssign.Visible = true;
        }
    }
}
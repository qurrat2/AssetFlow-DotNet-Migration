using System;
using System.Web.UI;
using AssetFlow.Core.Repositories;
using AssetFlow.Legacy.Web.App_Start;
using AssetFlow.Legacy.Web.Auth;

namespace AssetFlow.Legacy.Web.Assets;

public partial class Assign : Page
{
    private int _assetId;

    protected async void Page_Load(object sender, EventArgs e)
    {
        var current = FormsAuthHelper.Current();
        if (current?.Role != "Admin") { Response.Redirect("~/Default.aspx"); return; }

        if (!int.TryParse(Request.QueryString["assetId"], out _assetId))
        {
            LblError.Text = "Missing or invalid assetId."; BtnAssign.Enabled = false; return;
        }

        if (!IsPostBack)
        {
            var repo = WebFormsContainer.Resolve<IAssetRepository>();
            var asset = await repo.GetByIdAsync(_assetId);
            LitTag.Text = asset?.Tag ?? "(not found)";
            if (asset == null || asset.Status != "Available")
            {
                LblError.Text = "Asset is not available for assignment.";
                BtnAssign.Enabled = false;
            }

            var userRepo = WebFormsContainer.Resolve<IUserRepository>();
            var users = await userRepo.GetAllAsync();
            DdlUsers.DataSource = users;
            DdlUsers.DataBind();
        }
    }

    protected async void BtnAssign_Click(object sender, EventArgs e)
    {
        if (!int.TryParse(DdlUsers.SelectedValue, out var userId))
        {
            LblError.Text = "Please select a user."; return;
        }

        try
        {
            var assignments = WebFormsContainer.Resolve<IAssignmentRepository>();
            await assignments.AssignAsync(_assetId, userId, TxtNotes.Text);
            Response.Redirect("~/Default.aspx");
        }
        catch (Exception ex)
        {
            LblError.Text = ex.Message;
        }
    }
}


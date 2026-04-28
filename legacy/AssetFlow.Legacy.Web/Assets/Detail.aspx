<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Detail.aspx.cs" Inherits="AssetFlow.Legacy.Web.Assets.Detail" %>
<!DOCTYPE html>
<html><head runat="server"><title>Asset Detail</title></head>
<body>

     <p style="text-align:right;"><a href="~/Logout.aspx" runat="server">Logout</a></p>
<form id="form1" runat="server">
  <h1>Asset <asp:Literal ID="LitTag" runat="server" /></h1>
  <p>Status: <asp:Literal ID="LitStatus" runat="server" /></p>
  <p>Description: <asp:Literal ID="LitDesc" runat="server" /></p>
  <asp:HyperLink ID="LnkAssign" runat="server" Visible="false" Text="Assign this asset" />
  <a href="../Default.aspx">Back</a>
</form>
</body>
</html>
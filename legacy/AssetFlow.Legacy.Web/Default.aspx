<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Default.aspx.cs" Inherits="AssetFlow.Legacy.Web.Default" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server"><title>AssetFlow — Assets</title></head>
<body>
<form id="form1" runat="server">
  <h1>Assets in your department</h1>
  <asp:GridView ID="GridAssets" runat="server" AutoGenerateColumns="false">
    <Columns>
      <asp:BoundField DataField="Tag" HeaderText="Tag" />
      <asp:BoundField DataField="Description" HeaderText="Description" />
      <asp:BoundField DataField="Status" HeaderText="Status" />
      <asp:HyperLinkField HeaderText="Action" Text="View"
        DataNavigateUrlFields="Id" DataNavigateUrlFormatString="~/Assets/Detail.aspx?id={0}" />
    </Columns>
  </asp:GridView>
</form>
</body>
</html>

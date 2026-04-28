<%@ Page Language="C#" AutoEventWireup="true" Async="true" CodeBehind="Assign.aspx.cs" Inherits="AssetFlow.Legacy.Web.Assets.Assign" %>

<!DOCTYPE html>

<html><head runat="server"><title>Assign Asset</title></head>
<body>
     <p style="text-align:right;"><a href="~/Logout.aspx" runat="server">Logout</a></p>
  <form id="form1" runat="server">
        <h1>Assign asset <asp:Literal ID="LitTag" runat="server" /></h1>
        <asp:Label ID="LblError" runat="server" ForeColor="Red" />
         <p>Assign to:
    <asp:DropDownList ID="DdlUsers" runat="server" DataTextField="Username" DataValueField="Id" /></p>
        <p>Notes: <asp:TextBox ID="TxtNotes" runat="server" TextMode="MultiLine" Rows="2" Columns="40" /></p>
        <asp:Button ID="BtnAssign" runat="server" Text="Assign" OnClick="BtnAssign_Click" />
        <br /><br /><a href="../Default.aspx">Cancel</a>
  </form>
</body>
</html>
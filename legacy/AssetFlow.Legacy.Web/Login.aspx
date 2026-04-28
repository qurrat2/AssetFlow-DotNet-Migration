<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="AssetFlow.Legacy.Web.Login" %>

<!DOCTYPE html>

<html>

<head runat="server"><title>AssetFlow — Login</title></head>
<body>
<form id="form1" runat="server">
  <h1>AssetFlow (Legacy WebForms 4.8)</h1>
  <asp:Label ID="LblError" runat="server" ForeColor="Red" />
  <p>Username: <asp:TextBox ID="TxtUsername" runat="server" /></p>
  <p>Password: <asp:TextBox ID="TxtPassword" runat="server" TextMode="Password" /></p>
  <asp:Button ID="BtnLogin" runat="server" Text="Sign in" OnClick="BtnLogin_Click" />
</form>
</body>
</html>
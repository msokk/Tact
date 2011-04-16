<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TactSVC.Login" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>


        <asp:TextBox ID="kasutajanimi_input" runat="server"></asp:TextBox>
        <asp:TextBox ID="parool_input" runat="server"></asp:TextBox>
        <asp:Button ID="logi_sisse_nupp" runat="server" onclick="Logi_sisse" Text="Button" />

    </div>
    </form>
</body>
</html>

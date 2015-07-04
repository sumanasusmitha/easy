<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="decrypt_data.aspx.cs" Inherits="file_encrypt_des.WebForm4" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <input type="number" id="ret"  runat="server"/>
    <asp:Button Text="info" OnClick="decrypt" runat="server"/>
    <p id="Text" runat="server" >data.....</p>
        <span id="na" runat="server"></span>
        <span id="ro" runat="server"></span>
    </div>
    </form>
</body>
</html>

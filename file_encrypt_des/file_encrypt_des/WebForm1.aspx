<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="file_encrypt_des.WebForm" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <h1>File Upload :</h1>
    <asp:FileUpload ID="uploading" runat="server" Height="27px" Width="263px" />
    <br />
    </div>
        <p>
    <asp:Button ID="btn" runat="server" OnClick="btn_click" Text="Encrypt" Height="22px" Width="74px" />
        </p>
       <p id="ext" runat="server">extension name</p>
    </form>
</body>
</html>

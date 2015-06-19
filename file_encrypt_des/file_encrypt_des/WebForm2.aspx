<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm2.aspx.cs" Inherits="file_encrypt_des.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<asp:FileUpload ID="upload" runat="server" />
<hr />
<asp:Button ID = "btnEncrypt" Text="Encrypt File" runat="server" OnClick = "EncryptFile" />
<asp:Button ID = "btnDecrypt" Text="Decrypt File" runat="server" OnClick = "DecryptFile" />

    </div>
    </form>
</body>
</html>

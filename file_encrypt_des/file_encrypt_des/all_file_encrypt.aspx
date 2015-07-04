<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="all_file_encrypt.aspx.cs" Inherits="file_encrypt_des.WebForm2" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
<asp:FileUpload ID="upload" runat="server" />
<br />
        <br />
Encryption Key:<input type="text" runat="server" id="keybox1" /> 
       
<asp:Button ID = "btnEncrypt" Text="Encrypt File" runat="server" OnClick = "EncryptFile" />
        <br />
        <hr />
Decryption Key: <input type="text" id="keybox2" runat="server"/>
       
<asp:Button ID = "btnDecrypt" Text="Decrypt File" runat="server" OnClick = "DecryptFile" />
        <br />
        <p id="check" runat="server">fghdzxfh</p>
    </div>
    </form>
</body>
</html>

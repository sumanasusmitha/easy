<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>
<script runat="server">
     
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>susmitha</title>
    <style type="text/css">
        #sub {
            width: 64px;
        }
    </style>
</head>
<body>
    
    <form id="form1" runat="server">
       <p>plain text :</p><input runat="server" type="text" id="Myte"/>
        <br />
        <input  runat="server" id="sub" type="submit" value="enter"  OnServerClick="convertoupper"/>
    <br />
       <b> Encrypted Text :</b>
        <span id="Change" runat="server">type plain text</span>
        <br />
        <b>Decrypted text :</b>
        <span id="decrypt" runat="server">decrypted text </span>
        <br />
        <b>Key :</b>
        <span id="key" runat="server">Encryption Key</span>
    </form>
</body>
</html>

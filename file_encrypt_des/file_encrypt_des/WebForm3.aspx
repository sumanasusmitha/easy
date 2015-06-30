<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm3.aspx.cs" Inherits="file_encrypt_des.WebForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #retrieve {
            width: 88px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
<div>
    <table>
        <tr  style="background-color:#004080;color:White;" >
            <td> id </td>                        
            <td> roll </td>            
            <td>name</td>                        
        </tr>
        <tr>
            <td>
                <input type="text" runat="server" id="id1"/>
            </td>
            <td>
                <input type="text" runat="server" id="roll1" />
            </td>
            <td>
            <input type="text" runat="server" id="name1" />
                </td>
        </tr>

    </table>
    <asp:Button runat="server" ID="update" Text="Insert" OnClick="btn" />
    <br />
    <br />
    <input type="number" id="retrieve" />
</div>
        <br />
    </form>
    <%--<asp:Content ID="BodyContent" runat="server"  ContentPlaceHolderID="ContentPlaceHolder">
    <table --%>
    <table border="0"  >
        <tr  style="background-color:#004080;color:White;" >
            <td> id </td>                        
            <td> roll </td>            
            <td>name</td>                        
        </tr>
        <%=Page_Load()%>

    </table>
<%--</asp:Content>--%>

</body>
</html>

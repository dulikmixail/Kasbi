<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Login" CodeFile="Login.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <title>[������������� ������������]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body>
        <form id="Form1" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="PageTitle">
            <tr>
                <td width="100%" class="HeaderTitle"  >
                    &nbsp;�������������&nbsp;������������</td>
            </tr>
        </table>
       <div id="parent">
           <table id="child" cellspacing="2" cellpadding="2" style="margin: auto">
                    <tr>
                        <td width="90px" style="font-weight: bold; font-size: 8pt">
                            &nbsp;��� :</td>
                        <td style="height: 20px">
                            <asp:TextBox ID="txtLoginUser" runat="server" BorderWidth="1px" Width="80px" BorderStyle="Solid"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="font-weight: bold; font-size: 8pt">
                            &nbsp;������ :</td>
                        <td>
                            <asp:TextBox ID="txtLoginPassword" runat="server" BorderWidth="1px" Width="80px"
                                BorderStyle="Solid" TextMode="Password"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:ImageButton ID="btnLogin" runat="server" ImageUrl="Images/login.gif"></asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label ID="lblError" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
                    </tr>
                </table>
       </div>
            
                <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
        
    
</body>
</html>

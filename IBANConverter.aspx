<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="IBANConverter.aspx.vb" Inherits="Kasbi.IBANConverter" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head  runat ="server">
    <title> [IBAN конвертер]</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;IBAN конвертер&nbsp;</td>
            </tr>
        </table>

        <table>
            <tr>
                <td>
                    <asp:LinkButton  ID="lnkLoadXMLFile" cssClass="LinkButton" runat="server">Получить XML файл со счетами</asp:LinkButton>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:FileUpload ID="fileUpload" runat="server"/>
                    <asp:LinkButton  ID="lnkUpdateBankAccount" cssClass="LinkButton" runat="server">&nbsp;Обновить счета до IBAN&nbsp;</asp:LinkButton>
                    <br />
                    <asp:Label ID="msgBoxError" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TextBox ID="txtLoadInfo" runat="server" BorderWidth="1px" Height="500px" MaxLength="13" Width="700px"></asp:TextBox>
                </td>
            </tr>
        </table>

        

        
        

    </form>
</body>
</html>

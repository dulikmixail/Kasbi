<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Export" CodeFile="Export.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
<head runat="server">
    <title>[Ёкспорт]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <LINK href="Styles.css" type="text/css" rel="stylesheet">
</HEAD>
<body onscroll="javascript:document.all['scrollPos'].value = document.body.scrollTop;" bottomMargin="0" leftMargin="0" topMargin="0" onload="javascript:document.body.scrollTop = document.all['scrollPos'].value;" rightMargin="0">
<form id="frmExport" method="post" runat="server">
    <uc1:Header id="Header1" runat="server"></uc1:Header>
    <p>
        <asp:Label runat="server" EnableViewState="False" ForeColor="Red" ID="msg" Font-Bold="True"></asp:Label>
    </p>
    <table width="100%" cellpadding="0" cellspacing="0">
        <TR class="Unit">
            <TD colSpan="2">
                &nbsp;Ёкспорт в 1—
            </TD>
        </TR>
        <tr>
            <td>&nbsp;</td>
        </tr>
        <tr visible="false">
            <td>
                <asp:linkbutton visible="false" id="btnExportCustomers" runat="server" CssClass="PanelHider" EnableViewState="False">
                    <asp:Image runat="server" ID="imgSelNew" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position: relative; left: 10px;"></asp:Image>&nbsp; лиенты
                </asp:linkbutton>
                <asp:hyperlink id="lblCustomersInfo" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink>
            </td>
        </tr>
        <tr visible=false>
            <td>
                <asp:linkbutton visible="false" id="btnExportSales" runat="server" CssClass="PanelHider" EnableViewState="False">
                    <asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position: relative; left: 10px;"></asp:Image>&nbsp;ѕродажи
                </asp:linkbutton>
                <asp:hyperlink id="lblSaleInfo" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:linkbutton id="lnk_exportSales" runat="server" CssClass="PanelHider" EnableViewState="False">
                    <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position: relative; left: 10px;"></asp:Image>&nbsp;Ёкспорт новых продаж
                </asp:linkbutton>
                <asp:hyperlink id="Hyperlink1" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:linkbutton id="lnk_exportCustomers" runat="server" CssClass="PanelHider" EnableViewState="False">
                    <asp:Image runat="server" ID="Image3" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position: relative; left: 10px;"></asp:Image>&nbsp;Ёкспорт новых клиентов
                </asp:linkbutton>
                <asp:hyperlink id="Hyperlink2" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td>
                <asp:linkbutton id="lnk_exportHistory" runat="server" CssClass="PanelHider" EnableViewState="False">
                    <asp:Image runat="server" ID="Image4" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position: relative; left: 10px;"></asp:Image>&nbsp;Ёкспорт новых записей истории
                </asp:linkbutton>
                <asp:hyperlink id="Hyperlink3" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink>
            </td>
        </tr>
        <tr>
            <td height="30">&nbsp;</td>
        </tr>
    </table>
    <table cellSpacing="0" cellPadding="0" width="100%">
        <tr>
            <td style="HEIGHT: 20px" align="right" width="100%" bgColor="#9c0001">
                <A class="TopLink" href="#top">вверх&nbsp;</A>
            </td>
        </tr>
    </table>
    <input runat="server" ID="scrollPos" type="hidden" value="0" NAME="scrollPos"/>
    <input runat="server" ID="CurrentPage" type="hidden" lang="ru" NAME="CurrentPage"/>
    <input runat="server" ID="Parameters" type="hidden" lang="ru" NAME="Parameters"/>
    <input runat="server" ID="FindHidden" type="hidden" NAME="FindHidden"/>
</form>
</body>
</HTML>
<%@ Reference Control="~/RebillingGrid.ascx" %>
<%@ Reference Control="~/Sale.ascx" %>
<%@ Reference Page = "~/Documents.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.SalesNeopl" CodeFile="SalesNeopl.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"  runat ="server">
    <title>[Продажи]</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;" bottomMargin="0"
		leftMargin="0" topMargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
		rightMargin="0">
		<form id="frmNewRequest" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr bgColor="#d3c9c7">
					<td class="HeaderTitle" width="100%">Неоплаченные заказы</td>
				</tr>
			</table>
			<asp:label id="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:label>
			<TABLE cellSpacing="0" cellPadding="0" width="100%">
				<TR>
					<TD colSpan="3"><asp:label id="msgClientInfo" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"
							CssClass="PanelHider"></asp:label></TD>
				</TR>
				<!--<TR class="Unit">
					<TD class="Unit" colSpan="3">&nbsp;Переоформление кассовых аппаратов</TD>
				</TR>
				<TR>
					<TD class="SectionRow" colSpan="3">
						<asp:hyperlink id="lnkRebilling" runat="server" EnableViewState="False" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image1" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Переоформление КСА</asp:hyperlink></TD>
				</TR>-->
				<TR>
					<TD colSpan="3" height="5">&nbsp;</TD>
				</TR>

				<TR class="DetailField">
					<TD colSpan="3">
						<asp:label id="msgDocumentsPanel" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"
							CssClass="PanelHider"></asp:label></TD>
				</TR>
				<TR>
					<TD align="center" colSpan="3"><asp:panel id="pnlSales" runat="server" EnableViewState="false"></asp:panel></TD>
				</TR>
			</TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server"> <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
			<input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
			<input id="FindHidden" type="hidden" name="FindHidden" runat="server">
		</form>
	</body>
</HTML>

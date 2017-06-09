<%@ Reference Page="~/Customer.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.ImportDelivery" CodeFile="ImportDelivery.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head  runat ="server">
    <title>[Импорт поставок из 1С]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="frmImport" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Администрирование -&gt; Импорт поставок из 1С</td>
				</tr>
			</table>
			<table cellPadding="2" cellSpacing="1" width="100%">
				<tr class="Unit">
					<td class="Unit" width="100%">&nbsp;Загрузка&nbsp;данных</td>
				</tr>
				<tr>
					<td width="100%" colSpan="2"><asp:label id="msgImport" runat="server" EnableViewState="false" ForeColor="#ff0000" Font-Bold="true"></asp:label></td>
				</tr>
				<tr>
					<td width="100%">
					
					<table width="100%">
					    <tr>
					        <td width="50%">
					            <asp:linkbutton id="btnLoadData" runat="server" EnableViewState="False"  CssClass="PanelHider">
						        <asp:Image runat="server" ID="imgSelNew" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Загрузить данные</asp:linkbutton>
					        </td>
					        <td>
					            <asp:Label ID="lbl_countlog" runat="server" Font-Size="12px"></asp:Label><br />
					            <asp:linkbutton id="btn_delimport" runat="server" EnableViewState="False"  CssClass="PanelHider" ForeColor="red">
						        <asp:Image runat="server" ID="Image1" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Откатить импорт</asp:linkbutton>
						        
					            <asp:linkbutton id="btn_clearlog" runat="server" EnableViewState="False"  CssClass="PanelHider">
						        <asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Очистить лог</asp:linkbutton>					        
					        
					        </td>
					    </tr>
					</table>
					
					
					</td>
				
				
				<tr height="10">
					<td width="100%" colSpan="2" style="font-size:12px">
					
					
					<asp:Label ID="lblerror" runat="server" ForeColor=red></asp:Label>
					<asp:Label ID="lblconent" runat="server"></asp:Label>
					
					
					</td>
				</tr>
				
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD height="10"></TD>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>

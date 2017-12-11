<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports._Default1" CodeFile="Default.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[Отчеты]</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;</td>
				</tr>
			</table>
			<table width="100%" border="0" cellpadding="2" cellspacing="1">
				<tr>
					<td class="Unit" vAlign="top">Статистика</td>
				</tr>
				<tr>
					<td class="SectionRow">
						<asp:hyperlink id="lnkSellingReports" runat="server" CssClass="PanelHider" NavigateUrl="SellingRequest.aspx"
							EnableViewState="False">
						<asp:Image runat="server" ID="Image0" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Продажи</asp:hyperlink></td>
				</tr>
				<tr>
					<td class="SectionRow" style="font-size:12px">
						<asp:HyperLink id="lnkRepairReports" runat="server" NavigateUrl="RepairRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Ремонт</asp:HyperLink>
					</td>				
				</tr>
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="lnkDetailsReports" runat="server" NavigateUrl="DetailRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image15" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Детали</asp:HyperLink></td>
				</tr>				
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="lnkDealerReports" runat="server" NavigateUrl="DealersRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image3" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Дилеры - долги</asp:HyperLink></td>
				</tr>
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="HyperLink1" runat="server" NavigateUrl="ClientReports.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image20" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Клиенты - долги</asp:HyperLink></td>
				</tr>				
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkSupplierReports" runat="server" NavigateUrl="GoodsRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image7" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Поставщики</asp:HyperLink></td>
				</TR>
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkRestReports" runat="server" NavigateUrl="RestRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image8" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Остатки</asp:HyperLink></td>
				</TR>
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkAdvertisingReports" runat="server" NavigateUrl="AdvertisingRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image10" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Реклама</asp:HyperLink></td>
				</TR>
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkRubrreport" runat="server" NavigateUrl="RubrRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image13" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Статистика клиентов по рубрикам</asp:HyperLink></td>
				</TR>	
                <TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkCashRegisters" runat="server" NavigateUrl="CashRegistersForTO.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image14" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Статистика по кассовым аппаратам</asp:HyperLink></td>
				</TR>
				<tr>
					<td class="Unit" vAlign="top">Техническое обслуживание</td>
				</tr>
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="lnkMasterTOReports" runat="server" NavigateUrl="MasterTORequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image5" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;ТО&nbsp;по&nbsp;мастерам</asp:HyperLink></td>
				</tr>
				<!--
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="HyperLink2" runat="server" NavigateUrl="TOReport.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image1" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Тестовые&nbsp;отчеты</asp:HyperLink></td> 
				</tr>-->
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkCTODismissReports" runat="server" NavigateUrl="CTODismissRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image6" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Снятие с ТО</asp:HyperLink></td>
				</TR>
				<TR>
					<td class="SectionRow">
						<asp:HyperLink id="lnkMasterreport" runat="server" NavigateUrl="masterworkrequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image34" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Отчет по работе мастеров</asp:HyperLink></td>
				</TR>
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="lnkQuartalMarkaReports" runat="server" NavigateUrl="QuartalMarkaRequest.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image4" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Квартальный отчет об использованных средствах контроля</asp:HyperLink></td>
				</tr>
				<tr>
					<td class="Unit" vAlign="top">Склад</td>
				</tr>
				<tr>
					<td class="SectionRow">
						<asp:HyperLink id="lnkTurnoverListReports" runat="server" NavigateUrl="TurnoverList.aspx" CssClass="PanelHider">
						<asp:Image runat="server" ID="Image9" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Оборотная ведомость</asp:HyperLink></td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>


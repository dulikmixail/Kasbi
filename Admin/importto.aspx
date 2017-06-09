<%@ Reference Page="~/Customer.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.ImportTO" CodeFile="ImportTO.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head  runat ="server">
    <title>[Импорт данных из 1С]</title>
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
					<td class="HeaderTitle" width="100%">&nbsp;Администрирование -&gt; Импорт данных из 
						1С</td>
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
					<td><asp:linkbutton id="btnLoadData" runat="server" CssClass="PanelHider" EnableViewState="False">
						<asp:Image runat="server" ID="imgSelNew" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Загрузить данные об оплате ТО клиентами</asp:linkbutton></td>
				<tr height="10">
					<td width="100%" colSpan="2"></td>
				</tr>
				<tr>
					<td align="center">
						<asp:datagrid id="grdImportData" runat="server" CellPadding="1" Width="80%" AutoGenerateColumns="False"
							AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
							<AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
								<asp:TemplateColumn HeaderText="№" SortExpression="id">
									<HeaderStyle Width="5%"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblId" runat="server">
											<%# DataBinder.Eval(Container.DataItem,"id")%>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="УНП" SortExpression="unn">
									<HeaderStyle Width="15%"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblUNN" runat="server">
											<%# DataBinder.Eval(Container.DataItem,"unn")%>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn SortExpression="name" HeaderText="Клиент">
									<HeaderStyle Width="60%"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblName" runat="server">
											<%# DataBinder.Eval(Container.DataItem,"name")%>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Баланс" SortExpression="sum">
									<HeaderStyle Width="10%"></HeaderStyle>
									<ItemTemplate>
										<asp:label id="lblSum" runat="server">
											<%# DataBinder.Eval(Container.DataItem,"sum")%>
										</asp:label>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<TR>
					<TD height="10"></TD>
				</TR>
				<TR>
					<TD class="Unit" align="center">
						<asp:imagebutton id="cmdCancel" runat="server" ImageUrl="../Images/cancel.gif"></asp:imagebutton>&nbsp;&nbsp;&nbsp;
						<asp:imagebutton id="cmdSave" runat="server" ImageUrl="../Images/update.gif"></asp:imagebutton></TD>
				</TR>
				<TR>
					<TD height="10"></TD>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>

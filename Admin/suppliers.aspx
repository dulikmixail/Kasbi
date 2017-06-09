<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.Suppliers" CodeFile="Suppliers.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
  <head  runat ="server">
    <title>[Поставщики]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
  </HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Администрирование&nbsp; 
						-&gt;&nbsp;Поставщики</td>
				</tr>
			</table>
			<table width="100%" cellPadding="2" cellSpacing="1" border="0">
				<tr class="Unit">
					<td class="Unit" width="100%">&nbsp;Информация&nbsp;о&nbsp;поставщиках</td>
					<td class="Unit" align="right"><asp:hyperlink id="btnNew" runat="server" NavigateUrl="SupplierDetail.aspx?SupplierDetailID=-9999"
							CssClass="PanelHider" EnableViewState="False">Новый&nbsp;поставщик</asp:hyperlink></td>
				</tr>
				<tr>
					<td width="100%" colSpan="2"><asp:label id="msgSuppliers" runat="server" EnableViewState="false" ForeColor="#ff0000" Font-Bold="true"></asp:label></td>
				</tr>
				<tr height="10">
					<td width="100%" colSpan="2"></td>
				</tr>
				<tr>
					<td align="center" colspan="2">
						<asp:datagrid id="grdSuppliers" runat="server" CellPadding="1" Width="80%" AutoGenerateColumns="False"
							BorderColor="#CC9966" BorderWidth="1px" AllowSorting="True">
							<AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
								<asp:TemplateColumn HeaderText="Supplier Name" SortExpression=" supplier_name">
									<HeaderStyle Width="50%"></HeaderStyle>
									<HeaderTemplate>
										Поставщик
									</HeaderTemplate>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"supplier_name") & " " & DataBinder.Eval(Container.DataItem,"supplier_abr")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Country" SortExpression=" country">
									<HeaderStyle Width="15%"></HeaderStyle>
									<HeaderTemplate>
										Страна
									</HeaderTemplate>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"country")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="City" SortExpression=" city">
									<HeaderStyle Width="15%"></HeaderStyle>
									<HeaderTemplate>
										Город
									</HeaderTemplate>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"city")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Надбавка">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# IIf(IIf(IsDBNull(DataBinder.Eval(Container.DataItem,"nadbavka")),0,DataBinder.Eval(Container.DataItem,"nadbavka")), "Да", "Нет")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn>
									<FooterStyle HorizontalAlign="Center" Width="30"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ToolTip="Изменить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ToolTip="Удалить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </ItemTemplate>
                                								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td height="5">&nbsp;</td>
				</tr>
				<TR>
					<TD class="Unit" align="center" colspan="2">
						<asp:imagebutton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:imagebutton>
					</TD>
				</TR>
				<tr>
					<td height="5">&nbsp;</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
		</form>
	</body>
</HTML>

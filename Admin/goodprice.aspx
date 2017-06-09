<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.Goodprice" Culture="ru-RU" CodeFile="Goodprice.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head  runat ="server">
    <title>[Типы товаров]</title>
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
					<td class="HeaderTitle" width="100%">&nbsp;Администрирование&nbsp; -&gt;&nbsp;Типы 
						товаров</td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td width="100%" colSpan="2"><asp:label id="msgError" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"></asp:label></td>
				</tr>

				<tr>
					<td align="center" colSpan="2">
					<asp:checkbox runat="server" AutoPostBack="True" Font-Size="10" ID="cbxShowDescription" Text="Показывать описание" OnCheckedChanged="ShowDescription" Checked="false" /></asp:checkbox>
				    
				    <asp:linkbutton id="btnLoadData" runat="server" EnableViewState="False"  CssClass="PanelHider">
						        <asp:Image runat="server" ID="imgSelNew" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Импорт прайслиста</asp:linkbutton>


				    <asp:linkbutton id="btnClear" runat="server" EnableViewState="False"  CssClass="PanelHider">
						        <asp:Image runat="server" ID="Image1" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Очистка прайс-листа</asp:linkbutton>
					
					
					
					
					<asp:datagrid id="grdGoodTypes" runat="server" ShowFooter="True" PageSize="1000" AllowPaging="True"
							AllowSorting="True" AutoGenerateColumns="False" Width="80%" CellPadding="1" BorderColor="#CC9966" BorderWidth="1px">
							<AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        	<Columns>

								<asp:TemplateColumn HeaderText="IDP" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"id_price")%>
									</ItemTemplate>
								</asp:TemplateColumn>
									
                                <asp:TemplateColumn HeaderText="Артикул" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "artikul")%>
									</ItemTemplate>
								</asp:TemplateColumn>							
																
								<asp:TemplateColumn HeaderText="Наименование" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"name")%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="false" HeaderText="Описание" HeaderStyle-ForeColor="White" SortExpression="param_str_description">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"description")%>
									</ItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="Группа" HeaderStyle-ForeColor="White" SortExpression="group_name">
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%#IIf(IsDBNull(DataBinder.Eval(Container.DataItem, "group_name")), "", DataBinder.Eval(Container.DataItem, "group_name"))%>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Цена" HeaderStyle-ForeColor="White" SortExpression="price" ItemStyle-HorizontalAlign="Center">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"price")%>
									</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:TemplateColumn HeaderText="Количество" HeaderStyle-ForeColor="White" SortExpression="available" ItemStyle-HorizontalAlign="Center">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"available")%>
									</ItemTemplate>
								</asp:TemplateColumn>															
								<asp:TemplateColumn HeaderText="Производитель" HeaderStyle-ForeColor="White" SortExpression="country">
									<HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"maker")%>
									</ItemTemplate>
		

								</asp:TemplateColumn>
								
								
														
								
								<asp:TemplateColumn>
									<FooterStyle HorizontalAlign="Center" Width="30"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ToolTip="Удалить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </ItemTemplate>


									</asp:TemplateColumn>
							</Columns>
							<PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
						</asp:datagrid></td>
				</tr>
				<asp:ListBox id="lstCountryEdit" runat="server" Width="129px" BorderWidth="1px" BackColor="#F6F8FC"
					cssclass="hiddenCountrylist" Rows="10"></asp:ListBox>
				<asp:ListBox id="lstCountryNew" runat="server" Width="129px" BorderWidth="1px" BackColor="#F6F8FC"
					cssclass="hiddenCountrylist" Rows="10"></asp:ListBox>
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

<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.GoodTypes" Culture="ru-RU" CodeFile="GoodTypes.aspx.vb" %>
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
					<asp:datagrid id="grdGoodTypes" runat="server" ShowFooter="True" PageSize="1000" AllowPaging="True"
							AllowSorting="True" AutoGenerateColumns="False" Width="80%" CellPadding="1" BorderColor="#CC9966" BorderWidth="1px">
							<AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        	<Columns>

								<asp:TemplateColumn Visible="false" HeaderText="ID" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"good_type_sys_id")%>
									</ItemTemplate>
								</asp:TemplateColumn>

								<asp:TemplateColumn HeaderText="IDP" HeaderStyle-ForeColor="White" SortExpression="idp">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"idp")%>
									</ItemTemplate>
								<FooterTemplate>
										<asp:TextBox id="tbxidpNew" runat="server" Columns="5" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxidpEdit" runat="server" Columns="5" Text='<%# DataBinder.Eval(Container.DataItem,"idp")%>' BackColor="#F6F8FC" BorderWidth="1px">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
																
								<asp:TemplateColumn HeaderText="Наименование" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"Name")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="tbxNameNew" runat="server" Columns="40" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxNameEdit" runat="server" Columns="40" Text='<%# DataBinder.Eval(Container.DataItem,"Name")%>' BackColor="#F6F8FC" BorderWidth="1px">
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Группа" HeaderStyle-ForeColor="White" SortExpression="group_name">
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<%# IIf(IsDBNull(DataBinder.Eval(Container.DataItem,"group_name")),"",DataBinder.Eval(Container.DataItem,"group_name"))%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:DropDownList id="lstGoodGroupNew" runat="server" BorderWidth="1px"></asp:DropDownList>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:DropDownList id="lstGoodGroupEdit" runat="server" BorderWidth="1px"></asp:DropDownList>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="ТО" HeaderStyle-ForeColor="White" SortExpression="allowCTO">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<ItemTemplate>
										<%# IIf(DataBinder.Eval(Container.DataItem,"allowCTO"), "Да", "Нет")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="cbxAllowCTONew" runat="server" BorderWidth="1px"></asp:CheckBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:CheckBox id="cbxAllowCTOEdit" runat="server" BorderWidth="1px" Checked='<%# DataBinder.Eval(Container.DataItem,"allowCTO")%>'>
										</asp:CheckBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Цена ТО" HeaderStyle-ForeColor="White" SortExpression="price_to" ItemStyle-HorizontalAlign="Center">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"group_id")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:DropDownList ID="lstPriceTO" runat="server" Width="50"> </asp:DropDownList>
									</FooterTemplate>
									<EditItemTemplate>
									  
                                    <asp:DropDownList ID="lstPriceTOEdit" runat="server" Width="50"> </asp:DropDownList>
                                     
									</EditItemTemplate>
								</asp:TemplateColumn>								
								<asp:TemplateColumn HeaderText="ККМ" HeaderStyle-ForeColor="White" SortExpression="is_cashregister">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<ItemTemplate>
										<%# IIf(DataBinder.Eval(Container.DataItem,"is_cashregister"), "Да", "Нет")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="cbxIsCashregisterNew" runat="server" BorderWidth="1px"></asp:CheckBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:CheckBox id="cbxIsCashregisterEdit" runat="server" BorderWidth="1px" Checked='<%# DataBinder.Eval(Container.DataItem,"is_cashregister")%>'>
										</asp:CheckBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Надбавка" HeaderStyle-ForeColor="White" SortExpression="nadbavka">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<ItemTemplate>
										<%# IIf(DataBinder.Eval(Container.DataItem,"nadbavka"), "Да", "Нет")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="cbxNadbavkaNew" runat="server" BorderWidth="1px"></asp:CheckBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:CheckBox id="cbxNadbavkaEdit" runat="server" BorderWidth="1px" Checked='<%# DataBinder.Eval(Container.DataItem,"nadbavka")%>'>
										</asp:CheckBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Гарантия" HeaderStyle-ForeColor="White" SortExpression="garantia">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"garantia")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="tbxGarantiaNew" runat="server" Columns="7" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxGarantiaEdit" runat="server" Columns="7" BackColor="#F6F8FC" BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"garantia")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn Visible="false" HeaderText="Описание" HeaderStyle-ForeColor="White" SortExpression="param_str_description">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"param_str_description")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="tbxDescriptionNew" runat="server" BackColor="#F6F8FC" Columns="10" BorderWidth="1px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxDescriptionEdit" runat="server" BackColor="#F6F8FC"  Columns="10"  BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"param_str_description")%>'>
										</asp:TextBox>
									</EditItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Страна" HeaderStyle-ForeColor="White" SortExpression="country">
									<HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container.DataItem,"country")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="tbxCountryNew" runat="server" BackColor="#F6F8FC" Columns="7" BorderWidth="1px"></asp:TextBox><nobr>
											<asp:label id="btnCountryNew" style="CURSOR: hand" runat="server" Font-Bold="True" ForeColor="SteelBlue"
												Font-Size="10pt">...</asp:label>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxCountryEdit" runat="server" BackColor="#F6F8FC" Columns="7" BorderWidth="1px"></asp:TextBox><nobr>
											<asp:label id="btnCountryEdit" style="CURSOR: hand" runat="server" Font-Bold="True" ForeColor="SteelBlue"
												Font-Size="10pt">...</asp:label>
									</EditItemTemplate>
								</asp:TemplateColumn>
								
								
								
                                <asp:TemplateColumn HeaderText="Артикул" HeaderStyle-ForeColor="White" SortExpression="Name">
									<ItemTemplate>
										<%#DataBinder.Eval(Container.DataItem, "Artikul")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:TextBox id="tbxArtikul" runat="server" Columns="40" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:TextBox id="tbxArtikulEdit" runat="server" Columns="40" Text='<%# DataBinder.Eval(Container.DataItem,"Artikul")%>' BackColor="#F6F8FC" BorderWidth="1px">
										</asp:TextBox>
									</EditItemTemplate>
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
                                <EditItemTemplate>
                                    <asp:ImageButton ID="cmdUpdate" runat="server" CommandName="Update" ToolTip="Сохранить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdCancel" runat="server" CommandName="Cancel" ToolTip="Отменить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </EditItemTemplate>
									<FooterTemplate>
										<asp:LinkButton id="btnAddGoodType" runat="server" CommandName="AddGoodType" Font-Size="8pt">Добавить</asp:LinkButton>
									</FooterTemplate>
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

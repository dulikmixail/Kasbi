<%@ Reference Page="~/Documents.aspx" %>
<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kasbi.RebillingGrid" CodeFile="RebillingGrid.ascx.vb" %>
<table id="tblMain" width="98%" class="DetailField">
	<tr>
		<td colSpan="2"><asp:label id="msgRebilling" runat="server" Font-Bold="true" EnableViewState="false" ForeColor="#ff0000"
				CssClass="PanelHider"></asp:label></td>
	</tr>
	<tr class="HeaderField">
		<td><asp:label id="HeaderSale" Runat="server"></asp:label></td>
		<td align="right" width="10%"><asp:label id="DateSale" runat="server"></asp:label></td>
	</tr>
	<tr class="DetailField">
		<td class="SectionRow" colSpan="2">&nbsp;Ответственный:&nbsp;<asp:label id="Saler" Runat="server"></asp:label></td>
	</tr>
	<tr>
		<td colSpan="2">
			<table width="100%">
				<tr class="DetailField">
					<td class="SectionRow" width="80">Документы:&nbsp;</td>
					<td class="SectionRow" width="80">&nbsp;<asp:hyperlink id="lnkSpisok_KKM" Runat="server" Target="_blank">Список&nbsp;ККМ</asp:hyperlink></td>
					<td class="SectionRow" width="50">&nbsp;&nbsp;<asp:hyperlink id="lnkDogovor_Na_TO" Runat="server" Target="_blank">Договор&nbsp;на&nbsp;тех.&nbsp;обслуживание</asp:hyperlink></td>
                    <td class="SectionRow" width="50">&nbsp;&nbsp;<asp:hyperlink id="lnkDogovor_Na_TO_Dop" Runat="server" Target="_blank">Дополнение&nbsp;к&nbsp;договору&nbsp;на&nbsp;ТО</asp:hyperlink></td>
					<td class="SectionRow" width="400">&nbsp;&nbsp;<asp:hyperlink id="lnkZayavlenie_IMNS" Runat="server" Target="_blank">Заявление&nbsp;в&nbsp;ИМНС</asp:hyperlink></td>
					<td class="SectionRow" align="right" width="20%"><asp:linkbutton id="btnCreateDocuments" runat="server">Сформировать весь<br>комплект документов</asp:linkbutton><asp:linkbutton id="btnPrint1" runat="server" Visible="False" ToolTip="Печать 1-й страницы Договора на ТО(3 шт.), Договора / Счет-фактуры, Списка ККМ, Акта списания показаний, Тех. заключения и Удостоверения кассира">Печать экз. 1</asp:linkbutton></td>
				</tr>
			</table>
		</td>
	</tr>
	<tr class="DetailField">
		<td colSpan="2"></td>
	</tr>
	<tr class="DetailField">
		<td colSpan="2"></td>
	</tr>
	<tr>
		<td class="SectionRow" colSpan="2"><asp:datagrid id="grdRebilling" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False"
				Font-Size="9pt" BorderWidth="1px" BorderColor="#CC9966">
				<HeaderStyle Font-Size="8pt" Font-Underline="True" HorizontalAlign="Center" ForeColor="#9C0001"></HeaderStyle>
				<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
            <ItemStyle CssClass ="itemGrid" />
            <Columns>
					<asp:TemplateColumn HeaderText="№">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:Label Runat="server" ID="lblNumGood" Font-Size="7pt" ForeColor="#9C0001"></asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="good_name" HeaderText="Товар">
						<ItemTemplate>
							<asp:Label id=Label5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
							</asp:Label>
						</ItemTemplate>
						<FooterStyle HorizontalAlign="Left" Font-Bold="false"></FooterStyle>
						<FooterTemplate>
							<asp:Label id="lblTotalCountByCash" ForeColor="#9C0001" runat="server" Font-Size="7pt"></asp:Label>
						</FooterTemplate>
						<EditItemTemplate>
							<asp:TextBox id=Textbox5 runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="good_description" HeaderText="Описание">
						<ItemTemplate>
							<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_description") %>' ID="Label2">
							</asp:Label>
						</ItemTemplate>
						<EditItemTemplate>
							<asp:TextBox runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_description") %>' ID="Textbox2" >
							</asp:TextBox>
						</EditItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn SortExpression="info" HeaderText="Доп. инф."></asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Акт">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:hyperlink id="lnkAkt_Pokazaniy" runat="server" Target="_blank" tooltip="Акт о снятии показаний счетчика">...</asp:hyperlink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Тех.&lt;br&gt;закл.">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:hyperlink id="lnkTeh_Zaklyuchenie" runat="server" Target="_blank" tooltip="Техническое заключение">...</asp:hyperlink>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Удост.&lt;br&gt;кассира">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:hyperlink id="lnkUdostoverenie_Kassira" runat="server" Target="_blank" tooltip="Техническое заключение">...</asp:hyperlink>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:datagrid></td>
	</tr>
	<tr>
		<td class="SectionRowLabel" vAlign="middle">&nbsp;</td>
		<td class="SectionRow" align="right"><asp:imagebutton id="btnDelete" runat="server" CommandName="Delete" ImageUrl="../Images/delete.gif"></asp:imagebutton></td>
	</tr>
	<TR class="DetailField">
		<td colspan="3" height="20">
		</td>
	</TR>
</table>

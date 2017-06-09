<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.GoodsReport" CodeFile="GoodsReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"  runat ="server">
	<title >[Отчет по поставщикам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table3" width="800" border="0" runat="server">
				<TBODY>
					<tr>
						<td width="20"></td>
						<td align="center" colSpan="3">
							<table width="100%">
								<tr>
									<td>
										<div align="left"><IMG height="92" src="../images/logotip.gif" width="222"></div>
									</td>
									<td align="center" colSpan="3">
										<h2>Отчёт по поставщикам</h2>
									</td>
								</tr>
							</table>
							</td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="10"></td>
						<td colSpan="3">
							<hr SIZE="1">
						</td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td style="FONT-SIZE: 9pt" align="left" width="15%">Начальная дата:</td>
						<td align="left" width="20"><asp:label id="lblStartDate" Runat="server" Font-Size="9"></asp:label></td>
						<td></td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td style="FONT-SIZE: 9pt" align="left" width="1%">Конечная дата:</td>
						<td align="left" width="20"><asp:label id="lblEndDate" Runat="server" Font-Size="9"></asp:label></td>
						<td align="right"><asp:label id="lblPrintDate" Runat="server" Font-Size="9"></asp:label></td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="10"></td>
						<td colSpan="3">
							<hr SIZE="1">
						</td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td align="center" colSpan="3">
							<asp:repeater id="rep" Runat="server">
								<HeaderTemplate>
									<table cellspacing="0" rules="all" border="1" style="FONT-SIZE:10pt;WIDTH:100%;BORDER-COLLAPSE:collapse">
										<tr align="center" style="FONT-WEIGHT:bold">
											<td>№<BR>
												п/п</td>
											<td>Товар</td>
											<td>Поставщик</td>
											<td>Количество</td>
											<td>Остатки</td>
											<td>Цена<BR>
												поставщика</td>
											<td>Цена<BR>
												продажи</td>
											<td>Рентабельность</td>
										</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td colspan="8" style="FONT-WEIGHT:bold" align="center">Дата :<%# DataBinder.Eval(Container.DataItem, "delivery_date")%></td>
									</tr>
									<asp:Repeater ID="repGood" Runat="server">
										<ItemTemplate>
											<tr>
												<td align="center"><%# DataBinder.Eval(Container, "ItemIndex") + 1%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "num_cashregister")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "marka_reestr_out")%><BR>
													<%# DataBinder.Eval(Container.DataItem, "marka_pzu_out")%>
													<BR>
													<%# DataBinder.Eval(Container.DataItem, "marka_mfp_out")%>
												</td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "dogovor")%></td>
												<td><%# DataBinder.Eval(Container.DataItem, "customer_name")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "unn")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "STO")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "zreport_out")%></td>
												<td><%# DataBinder.Eval(Container.DataItem, "employee_name")%></td>
												<td align="center"><%# CType(DataBinder.Eval(Container.DataItem, "dismissal_date"), DateTime).ToShortDateString()%></td>
											</tr>
										</ItemTemplate>
									</asp:Repeater>
								</ItemTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:repeater>
			<asp:DataGrid id="grid" Font-Size="9" runat="server" AutoGenerateColumns="False" Width="100%">
				<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
				<Columns>
					<asp:TemplateColumn HeaderText="№&lt;BR&gt;п/п">
						<ItemStyle HorizontalAlign="Center" Font-Bold="True"></ItemStyle>
						<ItemTemplate>
							<asp:Label ID="lblNumDelivery" Runat="server"></asp:Label>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Поставщик">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "supplier_abr")%>
							<%# DataBinder.Eval(Container.DataItem, "supplier_name")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Дата&lt;BR&gt;поставки">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# Format(DataBinder.Eval(Container.DataItem, "delivery_date"), "dd.MM.yyyy")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="ТТН">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "info")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Наименование продукции">
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "good_name")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Ед. изм">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "short_name")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Кол-во">
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "quantity")%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Цена&lt;BR&gt;поставки">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# DataBinder.Eval(Container.DataItem, "price").ToString() %>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Цена&lt;BR&gt;продажи">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# IIf(DataBinder.Eval(Container.DataItem, "p_price")<>-1,DataBinder.Eval(Container.DataItem, "p_price"), IIF(DataBinder.Eval(Container.DataItem, "p_price_opt")<>-1,DataBinder.Eval(Container.DataItem, "p_price_opt"),"Нет инфор-мации о цене"))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Рентабельность">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%# IIf(DataBinder.Eval(Container.DataItem, "p_price")<>-1,String.Format("{0:P}", (((DataBinder.Eval(Container.DataItem, "p_price")/DataBinder.Eval(Container.DataItem, "price"))-1))),IIf(DataBinder.Eval(Container.DataItem, "p_price_opt")<>-1,String.Format("{0:P}", (((DataBinder.Eval(Container.DataItem, "p_price_opt")/DataBinder.Eval(Container.DataItem, "price"))-1))),"-"))%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Цена RUR">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%#DataBinder.Eval(Container.DataItem, "rur").ToString()%>
						</ItemTemplate>
					</asp:TemplateColumn>
					<asp:TemplateColumn HeaderText="Стоимость RUR">
						<ItemStyle HorizontalAlign="Right"></ItemStyle>
						<ItemTemplate>
							<%#DataBinder.Eval(Container.DataItem, "rur") * DataBinder.Eval(Container.DataItem, "quantity").ToString()%>
						</ItemTemplate>
					</asp:TemplateColumn>
				</Columns>
			</asp:DataGrid></TD>
			<td width="20"></td>
			</TR></TBODY></TABLE>
		</form>
	</body>
</HTML>

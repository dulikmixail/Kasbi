<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.SellingReport" CodeFile="SellingReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"   runat ="server">
	<title >[Отчет по продажам по клиентам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" runat="server" ID="Table3">
				<TBODY>
					<tr>
						<td width="20"></td>
						<td align="center" colspan="3">
							<table width="100%">
								<tr>
									<td>
										<div align="left"><img src="../images/logotip.gif" width="222" height="92"></div>
									</td>
									<td align="center" colSpan="3">
										<h2>Отчёт по продажам по клиентам</h2>
									</td>
								</tr>
							</table>
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
						<td align="left" width="15%" style="FONT-SIZE: 9pt">Начальная дата:</td>
						<td align="left" width="20"><asp:label id="lblStartDate" Font-Size="9" Runat="server"></asp:label></td>
						<td></td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td align="left" width="1%" style="FONT-SIZE: 9pt">Конечная дата:</td>
						<td align="left" width="20"><asp:label id="lblEndDate" Font-Size="9" Runat="server"></asp:label></td>
						<td align="right"><asp:label id="lblPrintDate" Font-Size="9" Runat="server"></asp:label></td>
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
							<asp:repeater id="repCustomers" runat="server">
								<HeaderTemplate>
									<TABLE id="Table1" cellSpacing="2" cellPadding="1" width="100%" border="0">
								</HeaderTemplate>
								<ItemTemplate>
									<TR style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: center; ">
										<TD style="font-size:14">Клиент №<%# DataBinder.Eval(Container.DataItem, "customer_sys_id")%> (<%# DataBinder.Eval(Container.DataItem, "customer_name")%>)</TD>
										<TD width="50%" style="font-size:14">Привёл менеджер: <a style="color:White; text-decoration:none" href="SellingReportByManager.aspx?rt=3&db=05.01.2007&de=05.25.2007&cs=2&mana=<%# DataBinder.Eval(Container.DataItem, "sys_id")%>"><b><%# DataBinder.Eval(Container.DataItem, "name")%></a></b></TD>
									</TR>
									<TR>
										<TD colspan="2">
											<asp:repeater id="repSales" runat="server">
												<HeaderTemplate>
													<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
												</HeaderTemplate>
												<ItemTemplate>
													<tr style="COLOR: black; FONT-STYLE: italic; BACKGROUND-COLOR: gainsboro">
														<td colspan="2"><font size="2" style="FONT-WEIGHT: bold"><asp:label id="HeaderSale" Runat="server"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;UID:&nbsp;<%# DataBinder.Eval(Container.DataItem, "sale_sys_id")%></font></td>
														<td><font size="2">Ответственный: <font style="FONT-WEIGHT: bold">
																	<%# DataBinder.Eval(Container.DataItem, "saler_info")%>
																</font></font>
														</td>
														<td align="right" width="10%"><font size="2" style="FONT-WEIGHT: bold"><%# Format(DataBinder.Eval(Container.DataItem, "sale_date"), "dd.MM.yyyy")%></font></td>
													</tr>
													<tr>
														<td colspan="4">
															<asp:datagrid id="grdGoods" runat="server" Font-Size="9pt" AutoGenerateColumns="False" ShowFooter="True"
																Width="100%">
																<HeaderStyle Font-Size="10pt" Font-Underline="True" HorizontalAlign="Center"></HeaderStyle>
																<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
																<Columns>
																	<asp:TemplateColumn HeaderText="№">
																		<ItemStyle Width="10" HorizontalAlign="Center"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label Runat="server" ID="lblNumGood"></asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="good_name" HeaderText="Товар">
																		<ItemStyle Width="300"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label id="lblGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
																			</asp:Label>
																		</ItemTemplate>
																		<FooterStyle HorizontalAlign="Left" Font-Bold="false"></FooterStyle>
																		<FooterTemplate>
																			<asp:Label id="lblTotalCountByCash" runat="server"></asp:Label>
																		</FooterTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="good_description" HeaderText="Описание">
																		<ItemStyle Width="300"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" ID="lblGoodDescription"></asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Цена">
																		<ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>' ID="Label3" >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="quantity" HeaderText="Кол-во">
																		<ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' ID="Label4" >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Стоимость" FooterText="Итого:">
																		<ItemStyle Width="100" Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label id="lblCost" runat="server"></asp:Label>
																		</ItemTemplate>
																		<FooterTemplate>
																			Итого:
																			<asp:Label id="lblTotal" runat="server"></asp:Label>
																		</FooterTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid>
														</td>
													</tr>
													<tr>
														<td colspan="4" height="10"></td>
													</tr>
												</ItemTemplate>
												<FooterTemplate>
												<TR style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: right; ">
					
					<td colspan=3>Итого по клиенту :&nbsp;&nbsp;<b><asp:Label id="lblTotalClient" runat="server"></asp:Label></b>&nbsp;&nbsp;
					</td>
				</tr>	
			</table>
			</FooterTemplate> </asp:repeater>
			<table runat="server" border="0" width="100%" id="tblSaleNotExists" cellpadding="2" cellspacing="1">
				<tr>
					<td align="center">
						<font size="2" style="FONT-WEIGHT: bold">Заказы или продажи отсутсвуют</font>
					</td>
				</tr>
			</table>
			</TD> </TR>
			<TR>
				<td colspan="2" height="10"></td>
			</TR>
			</ItemTemplate>
			<FooterTemplate>
			
								</TABLE>
								</FooterTemplate>
			</asp:repeater></TD></TD>
			<td width="20"></td>
			</TR>
			<TR>
				<TD></TD>
				<TD align="right" bgColor="lightgrey" colSpan="3">
					<asp:label id="lblTotalSum" Runat="server" Font-Size="10pt" Font-Bold="True"></asp:label>
				</TD>
				<TD></TD>
			</TR>
			</TBODY></TABLE>
		</form>
	</body>
</HTML>

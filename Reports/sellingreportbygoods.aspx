<%@ Reference Page="~/admin/goodtypes.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.SellingReportByGoods" CodeFile="SellingReportByGoods.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <head runat ="server">
	<title >[Отчёт по продажам по типу товара]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
  </HEAD>
	<body>
		<form id="Form2" method="post" runat="server">
			<table width="800" border="0" runat="server">
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
										<h2>Отчёт по продажам по типу товара</h2>
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
						<td align="right"></td>
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
						<td align="center" colSpan="3"><asp:repeater id="repGoodTypes" runat="server">
								<HeaderTemplate>
									<TABLE id="Table1" cellSpacing="2" cellPadding="1" width="100%" border="0">
								</HeaderTemplate>
								<ItemTemplate>
									<TR style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: left; ">
										<TD colspan="4">
											<font size="3">&nbsp;
												<%# DataBinder.Eval(Container.DataItem, "name")%>
											</font>
										</TD>
									</TR>
									<TR>
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
															<asp:Label Runat="server" ID="lblSaleID" Visible="False"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="sale_date" HeaderText="Дата продажи">
														<ItemStyle Width="100" HorizontalAlign="Center"></ItemStyle>
														<ItemTemplate>
															<asp:Label runat="server" ID="lblSaleDate"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="customer_name" HeaderText="Клиент">
														<ItemStyle Width="200" HorizontalAlign="Left"></ItemStyle>
														<ItemTemplate>
															<asp:Label runat="server" ID="lblSaleCustomer"></asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="№ ККМ" Visible="False">
														<ItemStyle Width="30" HorizontalAlign="Left"></ItemStyle>
														<ItemTemplate>
															<asp:Label runat="server" ID="lblNum_Cashregister"></asp:Label>
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
														<FooterTemplate>
															Всего :
															<asp:Label id="lblGoodsCount" runat="server"></asp:Label>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn HeaderText="Стоимость" FooterText="Итого:">
														<ItemStyle Width="100" Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
														<ItemTemplate>
															<asp:Label id="lblCost" runat="server"></asp:Label>
														</ItemTemplate>
														<FooterTemplate>
															Всего :
															<asp:Label id="lblTotal" runat="server"></asp:Label>
														</FooterTemplate>
													</asp:TemplateColumn>
													<asp:TemplateColumn SortExpression="saler_info" HeaderText="Ответственный">
														<ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
														<ItemTemplate>
															<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.saler_info") %>' ID="Label1" >
															</asp:Label>
														</ItemTemplate>
													</asp:TemplateColumn>
												</Columns>
											</asp:datagrid>
										</td>
									</TR>
									<TR>
										<td colspan="4" height="10"></td>
									</TR>
								</ItemTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:repeater></TD></TR></TBODY>
			<asp:panel id="pnlTotal" Runat="server">
  <TBODY>
  <TR>
    <TD></TD>
    <TD align=right bgColor=lightgrey colSpan=3>
<asp:label id=lblTotalSum Runat="server" Font-Size="10pt" Font-Bold="True"></asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </TD>
    <TD></TD></TR>
			</asp:panel></TBODY></TABLE>
			<table id="tblSaleNotExists" cellSpacing="1" cellPadding="2" width="100%" border="0" runat="server">
				<tr>
					<td align="center"><font style="FONT-WEIGHT: bold" size="2">Заказы или продажи 
							отсутсвуют</font>
					</td>
				</tr>
			</table></TD>
			<td width="20"></td></TR></TABLE>
		</form>
	</body>
</HTML>

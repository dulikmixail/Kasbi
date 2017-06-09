<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.SellingReportBymanager" CodeFile="SellingReportBymanager.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"   runat ="server">
	<title >[Отчёт по клиентам по менеджерам]</title>
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
										<h2>Отчёт по клиентам по менеджерам</h2>
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
						<td style="font-size:12px; font-family:Arial" align="left" width="15%" style="FONT-SIZE: 9pt">Начальная дата:</td>
						<td style="font-size:12px; font-family:Arial" align="left" width="20"><asp:label id="lblStartDate" Font-Size="9" Runat="server"></asp:label></td>
						<td></td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td style="font-size:12px; font-family:Arial" align="left" width="1%" style="FONT-SIZE: 9pt">Конечная дата:</td>
						<td style="font-size:12px; font-family:Arial" align="left" width="20"><asp:label id="lblEndDate" Font-Size="9" Runat="server"></asp:label></td>
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
							<asp:repeater id="repManager" runat="server">
								<HeaderTemplate>
									<TABLE id="Table1" cellSpacing="2" cellPadding="1" width="100%" border="0">
								</HeaderTemplate>
								<ItemTemplate>
									<TR style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: center; ">
										<TD align="left" colspan="2"  style="font-size:14px; font-family:Arial">&nbsp;&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "name")%> - <asp:Label ID="lblTotalClientNum" runat="server"></asp:Label> клиента(ов) на <asp:Label ID="lblTotalClient2" runat="server"></asp:Label> рублей</b></TD>
									</TR>
									<TR>
										<TD colspan="2">
											<asp:repeater id="repSales" runat="server">
												<HeaderTemplate>
													<TABLE id="Table2" cellSpacing="2" cellPadding="1" width="100%" border="0">
												    <tr style="COLOR: black; FONT-STYLE: italic; BACKGROUND-COLOR: gainsboro">
												        <td align="center" colspan="1">Клиент</td>
												        <td align="center" width="100" colspan="1">Кол-во товаров</td>
												        <td align="center" width="100" colspan="1">На сумму</td>
												        <td align="center" width="100" colspan="1">Дата внесения</td>
												    </tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr style="COLOR: black; BACKGROUND-COLOR: gainsboro">
														<td colspan="1" style="font-size:12px; font-family:Arial">&nbsp;&nbsp;&nbsp;&nbsp;<a href="SellingReport.aspx?rt=1&db=01.01.2003&de=01.01.2015&cs=4&cust=<%#DataBinder.Eval(Container.DataItem, "customer_sys_id")%>"><%#DataBinder.Eval(Container.DataItem, "customer_sys_id")%>:&nbsp;<b><%#DataBinder.Eval(Container.DataItem, "customer_abr")%> <%#DataBinder.Eval(Container.DataItem, "customer_name")%></b></a></font></td>
														<td align="center" style="font-size:12px; font-family:Arial" width="100" colspan="1"><font size="2"><b><%#DataBinder.Eval(Container.DataItem, "orders")%> </b><font style="FONT-WEIGHT: bold">
														<td align="center" style="font-size:12px; font-family:Arial" width="100" colspan="1"><font size="2"><b><asp:Label ID="cost" runat="server"></asp:Label></b><font style="FONT-WEIGHT: bold">
		                                                <td align="center" style="font-size:12px; font-family:Arial" width="100" colspan="1"><b><%#DataBinder.Eval(Container.DataItem, "d")%></b></td>
																</font></font>
														</td>
													</tr>
													<tr>
														<td colspan="4">

														</td>
													</tr>
													<tr>
														<td colspan="4" height="10"></td>
													</tr>
												</ItemTemplate>
												<FooterTemplate>
												<TR style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: right; ">
					
				</tr>	
			</table>
			</FooterTemplate> </asp:repeater>
			<table runat="server" border="0" width="100%" id="tblSaleNotExists" cellpadding="2" cellspacing="1">
				<tr>
					<td align="center">
						<font size="2" style="FONT-WEIGHT: bold">Клиенты для данного менеджера отсутстуют</font>
					</td>
				</tr>
			</table>
			</TD></TR>
															    <tr>
			                                            <td colspan="4" align="right" style="font-size:12px; font-family:Arial">
			                                            Итого по менеджеру: <b><asp:Label ID="lblTotalClientNum2" runat="server"></asp:Label></b> клиентов и <b><asp:Label ID="lblTotalClient1" runat="server"></asp:Label></b> р.&nbsp;&nbsp;&nbsp;<br><br>
			                                            </td>
			                                        </tr>		
			</ItemTemplate>
			<FooterTemplate>
								</TABLE>
								</FooterTemplate>
			</asp:repeater></TD></TD>
			<td width="20"></td>
			</TR>
					<tr>
						<td width="10"></td>
						<td colSpan="3">
							<hr SIZE="1">
						</td>
						<td width="20"></td>
					</tr>
			</TBODY></TABLE>
		</form>
	</body>
</HTML>

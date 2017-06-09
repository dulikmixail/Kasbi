<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.CTODismissReport" CodeFile="CTODismissReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
		<head  runat ="server">
	<title >[Снятие с ТО]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="100%" border="0" runat="server" ID="Table3" style="font-family:Tahoma">
				<TBODY>
					<tr>
						<td width="20"></td>
						<td width="100%" align="center" colspan="3">
							<table width="100%">
								<tr>
									<td>
										<div align="left"><img src="../images/logotip.gif"></div>
									</td>
									<td align="center" colSpan="3">
										<h2>Отчёт снятий с ТО</h2>
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
						<td colSpan="3"><hr SIZE="1">
						</td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td align="center" colSpan="3">
							<asp:repeater id="rep" Runat="server">
								<HeaderTemplate>
									<table cellspacing="0" rules="all" border="1" style="FONT-SIZE:10pt;WIDTH:100%;BORDER-COLLAPSE:collapse; font-family:Tahoma">
										<tr align="center" style="FONT-WEIGHT:bold">
											<td>№<BR>
												п/п</td>
											<td>Дата<BR>
												снятия</td>
											<td>Заводской<BR>
												номер</td>
											<td>№&nbsp;СК.<BR>
												изготовителя</td>
											<td>№<BR>
												договора</td>
											<td>Клиент</td>
											<td>УНП</td>
											<td>Срок<BR>
												на ТО</td>
											<td>Последний<BR>
												№ z-отчета</td>
											<td>Исполнитель</td>
											<td>За кем закреплен</td>
										</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td colspan="10" style="FONT-SIZE: 9pt; COLOR: #ffffcc; BACKGROUND-COLOR: #990000; TEXT-ALIGN: center; "><%# DataBinder.Eval(Container.DataItem, "imns_name")%></td>
									</tr>
									<asp:Repeater ID="repGood" Runat="server">
										<ItemTemplate>
											<tr>
												<td align="center"><%# DataBinder.Eval(Container, "ItemIndex") + 1%></td>
												<td align="center"><%# Format(DataBinder.Eval(Container.DataItem, "dismissal_date"),  "dd.MM.yyyy")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "num_cashregister")%></td>
												<td align="center">
												    <%# DataBinder.Eval(Container.DataItem, "marka_reestr_out")%><BR>
													<%# DataBinder.Eval(Container.DataItem, "marka_pzu_out")%><BR>
													<%# DataBinder.Eval(Container.DataItem, "marka_mfp_out")%><BR>
													<%# DataBinder.Eval(Container.DataItem, "marka_cp_out")%><BR>
													<font color=Red>
													    <%# DataBinder.Eval(Container.DataItem, "marka_cto_in")%><br />
                                                        <%# DataBinder.Eval(Container.DataItem, "marka_cto2_in")%>
													</font>
												</td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "dogovor")%></td>
												<td><%# DataBinder.Eval(Container.DataItem, "customer_name")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "unn")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "STO")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "zreport_out")%></td>
												<td align="center"><%# DataBinder.Eval(Container.DataItem, "employee_name")%></td>
												<td><asp:Label ID="lblEmployeeCTO" runat="server"></asp:Label></td>
											  											
											</tr>
										</ItemTemplate>
									</asp:Repeater>
								</ItemTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:repeater>
			
			<br />
			<b>Подробно отработка по мастерам:</b><br />
			<br />
			
                   <asp:DataGrid ID="grdUsers" runat="server" CellPadding="1" Width="400px" Font-Size="13px" AutoGenerateColumns="False" AllowSorting="True" BFont-Names="Tahoma" BorderStyle="Solid" BorderColor="LightGrey">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="40%"></HeaderStyle>
                                <HeaderTemplate>
                                    Пользователь
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Name")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center">
                                <HeaderStyle Width="40%"></HeaderStyle>
                                <HeaderTemplate>
                                    Снято ККМ
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblDismiss" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>				
			
			
			
			</TD>
			<td width="20"></td>
			</TR></TBODY></TABLE>
		</form>
	</body>
</HTML>

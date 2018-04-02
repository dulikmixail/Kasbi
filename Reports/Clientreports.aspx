<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.ClientReports" CodeFile="ClientReports.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[Отчет по задолжености дилеров]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	    <link href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body><div align="center">
		<form id="Form1" method="post" runat="server">
			<table id="Table3" width="680" border="0" runat="server" style="font-family:Arial">
				<TBODY>
					<tr height="40">
						<td width="30"></td>
						<td align="center"></td>
						<td align="center">
							<h2>Отчёт по задолжености клиентов</h2>
						</td>
						<td width="30"></td>
					</tr>
					<tr>
						<td colspan="4">
						    Текущая дата:<asp:label id="lblCurrentDate" Font-Size="10" Runat="server"></asp:label>
						    (<a href="?">Все</a> | <a href="?11">Только с ККМ</a>)
						</td>

					</tr>
					<tr>
						<td align="left" colSpan="4">
						        <asp:datagrid id="grid" runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px" ShowFooter="True">
						        <ItemStyle CssClass="itemGrid" Font-Size="12px"></ItemStyle>
						        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" Font-Size="12px"></HeaderStyle>
						        <FooterStyle CssClass="footerGrid" Font-Size="12px"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="№">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblRecordNum" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Клиент" SortExpression="customer_name">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "customer_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="№&lt;BR&gt;договора" SortExpression="dogovor">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dogovor")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Аппаратов на ТО" SortExpression="count_cash">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "count_cash")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Долг" SortExpression="dolg">
										<ItemStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dolg")%>
										</ItemTemplate>
										<FooterTemplate>
											Итого:&nbsp;
											<asp:Label id="lblTotal" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>

								</Columns>
							</asp:datagrid>
						</td>
					</tr>
				</TBODY>
			</table>
		</form>
		</div>
	</body>
</HTML>

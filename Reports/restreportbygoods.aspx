<%@ Reference Page="~/admin/goodtypes.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.RestReportByGoods" CodeFile="RestReportByGoods.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head runat ="server">
	<title >[Отчет по остаткам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table id="Table3" width="680" border="0" runat="server">
				<TBODY>
					<tr height="40">
						<td width="30"></td>
						<td align="center"></td>
						<td align="center">
							<h2>Отчёт по остаткам</h2>
						</td>
						<td width="30"></td>
					</tr>
					<tr>
						<td width="30"></td>
						<td style="FONT-SIZE: 9pt" align="left" width="15%">Текущая дата:</td>
						<td align="left"><asp:label id=lblCurrentDate Runat="server" Font-Size="9" Text='<%= Format(DateTime.Now,"dd.MM.yyyy")%>'></asp:label></td>
						<td width="30"></td>
					</tr>
					<tr>
						<td width="30"></td>
						<td align="left" colSpan="2"><asp:datagrid id="grid" runat="server" Font-Size="10pt" ShowFooter="True" EnableViewState="False"
								Width="100%" AutoGenerateColumns="False">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="№№">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblRecordNum" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Наименование">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "good_name")%>
										</ItemTemplate>
										<FooterTemplate>
											Итого:&nbsp;
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Кол-во">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "quantity")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Цена&lt;BR&gt;приходная">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblPriceComming" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Сумма по&lt;BR&gt;приходу">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSummComming" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalSummComming" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Цена&lt;BR&gt;розничная">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblRetailPrice" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Сумма &lt;BR&gt;розничная">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblSummRetail" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalSummRetail" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Рентабель- ность">
										<ItemStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<asp:Label id="lblProfitability" runat="server"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											<asp:Label id="lblTotalProfitability" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid></td>
						<td width="30"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.DealersReports" CodeFile="DealersReports.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"   runat ="server">
	<title >[Отчет по дилерам]</title>
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
										<h2>Отчёт по дилерам</h2>
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
						<td align="left" width="20"><asp:label id="lblStartDate" Font-Size="9" Runat="server"></asp:label></td>
						<td></td>
						<td width="20"></td>
					</tr>
					<tr>
						<td width="20"></td>
						<td style="FONT-SIZE: 9pt" align="left" width="1%">Конечная дата:</td>
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
						<td align="center" colSpan="3"><asp:datagrid id="grid" runat="server" Font-Size="10pt" AutoGenerateColumns="False" Width="100%"
								EnableViewState="False">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="№№">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblRecordNum" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="ЦТО">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "customer_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="№&lt;BR&gt;договора">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dogovor")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Количество&lt;BR&gt;ККМ *">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "KKMCount")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Сумма&lt;BR&gt;за период">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "summa")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Сумма&lt;BR&gt;общая">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "full_summa")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Оплачено">
										<ItemStyle HorizontalAlign="Right"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "oplata")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Долг">
										<ItemStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dolg")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Последняя&lt;BR&gt;покупка">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Format(DataBinder.Eval(Container.DataItem, "lastorder"),"dd.MM.yyyy")%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
							<h5>*&nbsp; Количество ККМ представлено за период :
								<%= Format(date_start,"dd.MM.yyyy")%>
								-
								<%= Format(date_end,"dd.MM.yyyy")%>
							</h5>
						</td>
					
						<td width="20"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

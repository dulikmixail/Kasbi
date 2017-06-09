<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.MasterTOReport" CodeFile="MasterTOReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat="server">
    <title>[Отчет ТО по мастерам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body>
		<form id="Form1" method="post" runat="server">
			<table width="800" border="0" runat="server" ID="Table3">
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
										<h2>Отчёт ТО по мастерам</h2>
									</td>
								</tr>
							</table></td> 
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
							<asp:DataGrid id="grid" runat="server" AutoGenerateColumns="False" Width="100%" Font-Size="10pt">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="№&lt;BR&gt;п/п">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "ItemIndex") + 1%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Дата&lt;BR&gt;проведения">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Format(DataBinder.Eval(Container.DataItem, "change_state_date"), "dd.MM.yyyy")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Заводской&lt;BR&gt;номер">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "num_cashregister")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Средства&lt;BR&gt;контроля">
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "marka_cto_out")%>
											<BR>
											<%# DataBinder.Eval(Container.DataItem, "marka_cto2_out")%>
											<BR>
											<%# DataBinder.Eval(Container.DataItem, "marka_reestr_out")%>
											<BR>
											<%# DataBinder.Eval(Container.DataItem, "marka_pzu_out")%>
											<BR>
											<%# DataBinder.Eval(Container.DataItem, "marka_mfp_out")%>
											<BR>
											<%# DataBinder.Eval(Container.DataItem, "marka_cp_out")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Клиент">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "customer_name")%>
											<br>
											УНП:
											<%# DataBinder.Eval(Container.DataItem, "unn")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Закрываемый&lt;BR&gt;период">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label Runat="server" ID="lblClosePeriod"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Место&lt;BR&gt;установки">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "set_place")%> 
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Район&lt;BR&gt;установки">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<b> <%# DataBinder.Eval(Container.DataItem, "place_region")%></b>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Баланс">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "balance")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Мастер">
										<ItemTemplate>
											<nobr>
												<%# DataBinder.Eval(Container.DataItem, "employee_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:DataGrid>
						</td>
	
						<td width="20"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

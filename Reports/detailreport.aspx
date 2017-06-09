<%@ Reference Page="~/admin/details.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.DetailReport" CodeFile="DetailReport.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[Отчет по ремонтам]</title>
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
										<h2>Отчёт по деталям</h2>
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
							<asp:datagrid id="grid" runat="server" Font-Size="12px" Width="100%" AutoGenerateColumns="False">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>

									<asp:TemplateColumn HeaderText="№">
										<ItemStyle HorizontalAlign="center"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem, "detail_id")%>
										</ItemTemplate>
									</asp:TemplateColumn>
					
									<asp:TemplateColumn HeaderText="Название" HeaderStyle-Height="30">
										<ItemStyle HorizontalAlign="center"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem, "detail_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>							
									
									<asp:TemplateColumn HeaderText="Модель">
										<ItemStyle HorizontalAlign="center"></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem, "detail_notation")%>
										</ItemTemplate>
									</asp:TemplateColumn>

									<asp:TemplateColumn HeaderText="Количество">
										<ItemStyle HorizontalAlign="center" Font-Bold=true></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem, "num")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									
									<asp:TemplateColumn HeaderText="Цена">
										<ItemStyle HorizontalAlign="center" Font-Bold=true></ItemStyle>
										<ItemTemplate>
											<%#DataBinder.Eval(Container.DataItem, "price")%> р.
										</ItemTemplate>
									</asp:TemplateColumn>
																																									
								</Columns>
							</asp:datagrid>
						</td>
						<td width="20"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

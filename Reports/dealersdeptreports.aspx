<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.DealersDeptReports" CodeFile="DealersDeptReports.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
	<title >[����� �� ������������ �������]</title>
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
							<h2>����� �� ������������ �������</h2>
						</td>
						<td width="30"></td>
					</tr>
					<tr>
						<td width="30"></td>
						<td style="FONT-SIZE: 9pt" align="left" width="15%">������� ����:</td>
						<td align="left"><asp:label id="lblCurrentDate" Text ='<%= Format(DateTime.Now,"dd.MM.yyyy")%>' Font-Size="9" Runat="server"></asp:label></td>
						<td width="30"></td>
					</tr>
					<tr>
						<td width="30"></td>
						<td align="left" colSpan="2"><asp:datagrid id="grid" runat="server" Font-Size="10pt" AutoGenerateColumns="False" Width="100%"
								EnableViewState="False" ShowFooter="True">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="��">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblRecordNum" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="���">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "customer_name")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="�&lt;BR&gt;��������">
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dogovor")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="����">
										<ItemStyle Font-Bold="True" HorizontalAlign="Right" ForeColor="Red"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "dolg")%>
										</ItemTemplate>
										<FooterTemplate>
											�����:&nbsp;
											<asp:Label id="lblTotal" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="���������&lt;BR&gt;�������">
										<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<asp:Label ID="lblLastOrder" Runat="server"></asp:Label>
										</ItemTemplate>
									</asp:TemplateColumn>
								</Columns>
							</asp:datagrid>
						</td>
						<td width="30"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

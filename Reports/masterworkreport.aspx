<%@ Reference Page="~/admin/details.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.MasterworkReport" CodeFile="MasterworkReport.aspx.vb" %>
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
			<table width="100%" border="0" runat="server" ID="Table3" style="font-family:Tahoma">
				<TBODY>
					<tr>
						<td width="20"></td>
						<td align="center" colspan="3">
							<table width="100%">
								<tr>
									<td>
										<div align="left"><img src="../images/logotip.gif" ></div>
									</td>
									<td align="center" colSpan="3">
										<h2>Отчёт по работе мастеров</h2>
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
						<td width="100%" align="center" colSpan="3">
						
                <asp:DataGrid ID="grdUsers" runat="server" CellPadding="2" Width="800px" Font-Size="13px" AutoGenerateColumns="False" ShowFooter="true" AllowSorting="True" BFont-Names="Tahoma" BorderStyle="Solid" BorderColor="LightGrey">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true">
                                <HeaderStyle Width="30%"></HeaderStyle>
                                <HeaderTemplate>
                                    Мастер
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Name")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <HeaderTemplate>
                                    Отработано норма-часов
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblNormaHour" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblNormaHour" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <HeaderTemplate>
                                    Отработано норма-часов - гарания
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblNormaHour_garant" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblNormaHour_garant" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <HeaderTemplate>
                                    Поставлено на ТО
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblSetTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblSetTO" runat="server"></asp:Label>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="User Name" HeaderStyle-Font-Bold="true" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="center">
                                <HeaderStyle Width="20%"></HeaderStyle>
                                <HeaderTemplate>
                                    Снято с ТО
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblDelTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblDelTO" runat="server"></asp:Label>
                                </FooterTemplate>                               
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

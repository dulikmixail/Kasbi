<%@ Reference Page="~/admin/details.aspx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.RepairReport" CodeFile="RepairReport.aspx.vb" %>
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
						<td width="100%" align="center" colspan="3">
							<table width="100%">
								<tr>
									<td>
										<div align="left"><img src="../images/logotip.gif"></div>
									</td>
									<td align="center" colSpan="3">
										<h2>Отчёт по ремонтам</h2>
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
							<asp:datagrid id="grid" runat="server" Font-Size="10px" Width="100%" AutoGenerateColumns="False" ShowFooter="true" Font-Names="Tahoma" BorderStyle="Solid" BorderColor="LightGrey">
								<HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
								<Columns>
									<asp:TemplateColumn HeaderText="№&lt;BR&gt;п/п" >
										<ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container, "ItemIndex") + 1%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Даты&lt;BR&gt;ремонта">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Format(DataBinder.Eval(Container.DataItem, "repairdate_in"), "dd.MM.yyyy")%>
											&nbsp;/&nbsp;
											<%# Format(DataBinder.Eval(Container.DataItem, "repairdate_out"), "dd.MM.yyyy")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Тип">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "name")%>
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
											&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "marka_cto2_out")%>
											&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "marka_reestr_out")%>
											&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "marka_pzu_out")%>
											&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "marka_mfp_out")%>
											&nbsp;
											<%# DataBinder.Eval(Container.DataItem, "marka_cp_out")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Снятые СК">
										<ItemStyle HorizontalAlign="Center" VerticalAlign="Top"></ItemStyle>
										<ItemTemplate>
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_cto_in")<>DataBinder.Eval(Container.DataItem, "marka_cto_out"), DataBinder.Eval(Container.DataItem, "marka_cto_in"), "") %>
											&nbsp;
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_cto2_in")<>DataBinder.Eval(Container.DataItem, "marka_cto2_out"), DataBinder.Eval(Container.DataItem, "marka_cto2_in"), "") %>
											&nbsp;
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_reestr_in")<>DataBinder.Eval(Container.DataItem, "marka_reestr_out"), DataBinder.Eval(Container.DataItem, "marka_reestr_in"), "") %>
											&nbsp;
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_pzu_in")<>DataBinder.Eval(Container.DataItem, "marka_pzu_out"), DataBinder.Eval(Container.DataItem, "marka_pzu_in"), "") %>
											&nbsp;
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_mfp_in")<>DataBinder.Eval(Container.DataItem, "marka_mfp_out"), DataBinder.Eval(Container.DataItem, "marka_mfp_in"), "") %>
											&nbsp;
											<%# IIf(DataBinder.Eval(Container.DataItem, "marka_cp_in")<>DataBinder.Eval(Container.DataItem, "marka_cp_out"), DataBinder.Eval(Container.DataItem, "marka_cp_in"), "") %>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Клиент">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "customer_name")%>
											&nbsp;
											УНП:
											<%# DataBinder.Eval(Container.DataItem, "unn")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Дата&lt;BR&gt;продажи">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Format(DataBinder.Eval(Container.DataItem, "sale_date"), "dd.MM.yyyy")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Гарантийный&lt;BR&gt;срок">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# Format((DataBinder.Eval(Container.DataItem, "sale_date")).AddMonths(18),"dd.MM.yyyy")%>
											&nbsp;
											<font color="Red">
												<%# DataBinder.Eval(Container.DataItem, "garantia")%>
											</font>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="№&lt;BR&gt;Акта">
										<ItemStyle HorizontalAlign="Center"></ItemStyle>
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "akt")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Неисправность">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "info")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Детали">
										<ItemTemplate>
											<%# DataBinder.Eval(Container.DataItem, "details")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Мастер">
										<ItemTemplate>
											<nobr>
												<%# DataBinder.Eval(Container.DataItem, "employee_name")%><br />
												<%# Format(DataBinder.Eval(Container.DataItem, "change_state_date"), "dd.MM.yyyy")%>
										</ItemTemplate>
									</asp:TemplateColumn>
									<asp:TemplateColumn HeaderText="Норма/час" ItemStyle-HorizontalAlign="Center" FooterStyle-HorizontalAlign="Center">
										<ItemTemplate>
										<asp:Label ID="lblNormaHour" runat="server" Font-Bold="true" Font-Size="14px"></asp:Label>
										</ItemTemplate>
										<FooterTemplate>
											Итого:
											<asp:Label Font-Size="16px" Font-Bold="true" id="lblTotalNormaHour" runat="server"></asp:Label>
										</FooterTemplate>
									</asp:TemplateColumn>
								</Columns>
								
							</asp:datagrid>
					
					<div align="left">
					<br />
					<b>Детально отработки для каждого мастера:</b><br />
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
                                    Отработано норма-часов
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:Label Font-Size="16px" Font-Bold="true" id="lblNormaHour" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>							
					</div>
					
					
						</td>
						<td width="20"></td>
					</tr>
				</TBODY>
			</table>
		</form>
	</body>
</HTML>

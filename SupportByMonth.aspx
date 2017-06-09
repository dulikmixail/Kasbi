<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.SupportByMonth" CodeFile="SupportByMonth.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat ="server">
    <title>[Тех обслуживание]</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
		<meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="Styles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function OnChangeReportKeyword(lnk, txt)
		{
			lnk.href = "reports.aspx?" + txt.value;
		}
		</script>
	</HEAD>
	<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;" bottomMargin="0"
		leftMargin="0" topMargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
		rightMargin="0">
		<form id="frmSupportByMonth" method="post" runat="server">
			<uc1:Header id="Header1" runat="server"></uc1:Header>
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr class="Unit">
					<td colspan="2">&nbsp;Списки клиентов</td>
				</tr>
				<TR>
					<td align="right"><asp:hyperlink id="Hyperlink12" runat="server" CssClass="PanelHider" NavigateUrl="InternalCTO.aspx">ЦТО "Рамок"</asp:hyperlink></td>
				<TR>
					<TD colSpan="2" height="5">&nbsp;<asp:label id="Label1" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="2">
						<table style="LEFT: 20px; FONT-FAMILY: Verdana; POSITION: relative; TOP: -10px">
							<tr>
								<td colspan="3" class="DetailField">Минск</td>
							</tr>
							<tr>
								<td>
									<asp:hyperlink id="Hyperlink1" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?minsk-all"
										Target="_blank">
<asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>
&nbsp;Весь Минск</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink2" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?minsk-markets"
										Target="_blank">
<asp:Image runat="server" ID="Image3" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Минск - Рынки</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink3" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?minsk-others"
										Target="_blank">
<asp:Image runat="server" ID="Image4" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Минск - Остальные</asp:hyperlink></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td colspan="3" class="DetailField">Минск - Рынки</td>
							</tr>
							<tr>
								<td><asp:hyperlink id="Hyperlink4" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?zhdanovichi"
										Target="_blank">
<asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Ждановичи</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink5" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?dinamo"
										Target="_blank">
<asp:Image runat="server" ID="Image5" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Динамо</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink6" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?uruchye"
										Target="_blank">
<asp:Image runat="server" ID="Image6" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Уручье</asp:hyperlink></td>
							</tr>
							<tr>
								<td><asp:hyperlink id="Hyperlink7" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?akvabel"
										Target="_blank">
<asp:Image runat="server" ID="Image7" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Аквабел</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink8" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?malinovka"
										Target="_blank">
<asp:Image runat="server" ID="Image8" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Малиновка</asp:hyperlink></td>
								<td><asp:hyperlink id="Hyperlink9" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?nothing"
										Target="_blank">
<asp:Image runat="server" ID="Image9" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Митя так и не сказал какой рынок еще нужен</asp:hyperlink></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
							<tr>
								<td colspan="3" class="DetailField">Республика</td>
							</tr>
							<tr>
								<td>
									<asp:hyperlink id="Hyperlink10" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?republic-all"
										Target="_blank">
<asp:Image runat="server" ID="Image10" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>
&nbsp;Вся Республика</asp:hyperlink></td>
								<td colspan="2">
									<asp:hyperlink id="Hyperlink11" runat="server" CssClass="PanelHider" NavigateUrl="reports.aspx?republic-others"
										Target="_blank">
<asp:Image runat="server" ID="Image11" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>

&nbsp;Вся Республика кроме Минска</asp:hyperlink></td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
						</table>
						<table style="LEFT: 20px; FONT-FAMILY: Verdana; POSITION: relative; TOP: -10px">
							<tr>
								<td class="DetailField">Место установки аппарата:&nbsp;&nbsp;</td>
								<td class="DetailField"></td>
							</tr>
							<tr>
								<td>
									<asp:textbox id="txtCustomerName" runat="server" Width="100px" BorderWidth="1px" BackColor="#F6F8FC"
										ToolTip="Введите место" MaxLength="100"></asp:textbox>
									<asp:HyperLink id="lnkCustomReport" runat="server" CssClass="LinkButton" NavigateUrl="reports.aspx">Показать список</asp:HyperLink>
								</td>
								<td>
								</td>
							</tr>
							<tr>
								<td height="10"></td>
							</tr>
						</table>
					</TD>
				</TR>
				<tr class="Unit">
					<td>&nbsp;Информация&nbsp;об&nbsp;оплате&nbsp;технического&nbsp;обслуживания
					</td>
					<td align="right"></td>
				</tr>
				<TR>
					<TD colSpan="2" height="5">&nbsp;<asp:label id="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label></TD>
				</TR>
				<TR>
					<TD colSpan="2"><asp:linkbutton id="btnFilter" runat="server" CssClass="PanelHider">
<asp:Image runat="server" ID="imgSelFilter" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Фильтр</asp:linkbutton><asp:label id="lblFilterCaption" runat="server" Font-Size="8pt"></asp:label></TD>
				</TR>
				<TR>
					<TD style="FONT-SIZE: 7pt" align="center" colSpan="2"><asp:panel id="pnlFilter" style="BORDER-TOP: #cc9933 1px solid; BORDER-BOTTOM: #cc9933 1px solid"
							runat="server" Height="214px" Visible="False">
							<P style="MARGIN-TOP: 0px; MARGIN-BOTTOM: -4px">&nbsp;</P>
							<TABLE align="center">
								<TR vAlign="middle">
									<TD style="FONT-SIZE: 8pt; FONT-FAMILY: Verdana" vAlign="top" align="center" width="150"
										rowSpan="3">
										<TABLE style="FONT-SIZE: 8pt; FONT-FAMILY: Verdana" cellSpacing="0" cellPadding="0">
											<TR>
												<TD>
													<asp:CheckBox id="chkUseDate" runat="server" Text=" "></asp:CheckBox></TD>
												<TD style="COLOR: #990000">Использовать дату для выбора касс по</TD>
											</TR>
										</TABLE>
										<BR>
										<asp:RadioButtonList id="lstUseDateType" runat="server" Width="144px" Font-Size="7pt" CellPadding="0"
											CellSpacing="0">
											<asp:ListItem Value="0">дате продажи</asp:ListItem>
											<asp:ListItem Value="1">нахождению в ремонте</asp:ListItem>
											<asp:ListItem Value="2">занесению в базу</asp:ListItem>
										</asp:RadioButtonList></TD>
									<TD width="220" rowSpan="5">
										<asp:calendar id="cal" runat="server" ForeColor="#0066CC" ToolTip="Выделите интересующий Вас период времени (день, неделя, месяц). "
											BackColor="#FFFFCC" BorderWidth="1px" Width="220px" Font-Size="8pt" Height="128px" Font-Names="Verdana"
											ShowGridLines="True" BorderColor="#FFCC66" FirstDayOfWeek="Monday" SelectionMode="DayWeekMonth"
											SelectWeekText="<img src='Images/selweek.gif' border=0 />" SelectMonthText="<img src='Images/selmonth.gif' border=0/>">
											<TodayDayStyle ForeColor="DarkRed" BackColor="#FFFFCC"></TodayDayStyle>
											<SelectorStyle BackColor="#FFCC66"></SelectorStyle>
											<NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
											<DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
											<SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
											<TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></TitleStyle>
											<OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
										</asp:calendar></TD>
									<TD style="FONT-SIZE: 8pt; COLOR: #990000; FONT-FAMILY: Verdana" align="center" width="120">Наличие 
										на складе</TD>
									<TD style="HEIGHT: 18px">
										<P style="FONT-SIZE: 8pt; COLOR: #990000; FONT-FAMILY: Verdana" align="center">Тип</P>
									</TD>
									<TD style="FONT-SIZE: 8pt; COLOR: #990000; FONT-FAMILY: Verdana" align="center"></TD>
								</TR>
								<TR vAlign="top">
									<TD style="PADDING-LEFT: 10px; TEXT-JUSTIFY: newspaper; FONT-SIZE: 8pt; FONT-FAMILY: Verdana"
										width="100">
										<asp:checkbox id="chkFreeGoods" runat="server" Font-Size="9pt" Text="Свободные"></asp:checkbox>
										<asp:checkbox id="chkRequestedGoods" runat="server" Font-Size="9pt" Text="Заказанные"></asp:checkbox></TD>
									<TD width="100">
										<asp:checkbox id="chkKassa1" runat="server" Font-Size="9pt" Text="Касби-03Ф"></asp:checkbox>
										<asp:checkbox id="chkKassa2" runat="server" Font-Size="9pt" Text="Касби-03ФТ"></asp:checkbox></TD>
									<TD></TD>
								</TR>
								<TR>
									<TD width="100" height="65"></TD>
									<TD height="65"></TD>
									<TD width="100" height="65"></TD>
								</TR>
								<TR>
									<TD width="100"></TD>
									<TD></TD>
									<TD align="right">
										<asp:linkbutton id="lblShow" runat="server" CssClass="LinkButton" EnableViewState="False">Показать</asp:linkbutton></TD>
									<TD vAlign="bottom" align="right"></TD>
								</TR>
							</TABLE>
						</asp:panel></TD>
				</TR>
				<tr>
					<td colSpan="2" height="15"></td>
				</tr>
				<tr>
					<td align="center" colSpan="2"><asp:datagrid id="grd" Width="98%" CellPadding="1" Runat="server" AutoGenerateColumns="False"
							AllowSorting="True">
							<SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
							<EditItemStyle VerticalAlign="Middle"></EditItemStyle>
							<ItemStyle Font-Size="9pt" Font-Names="Verdana" BackColor="White"></ItemStyle>
							<HeaderStyle Font-Size="9pt" HorizontalAlign="Center" ForeColor="#FFFFCC" BackColor="#990000"></HeaderStyle>
							<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="№">
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<ItemTemplate>
										<asp:Label id="lblNum" ForeColor="#9C0001" EnableViewState="true" Font-Size="7pt" Runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Клиент">
									<ItemTemplate>
										<asp:HyperLink id=lnkClient runat="server" NavigateUrl='<%# "CustomerSales.aspx?" &amp; DataBinder.Eval(Container, "DataItem.customer_sys_id") %>' Text='<%# DataBinder.Eval(Container, "DataItem.customer_name") %>'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:BoundColumn DataField="date_to_pay" HeaderText="Дата" DataFormatString="{0:dd.MM.yyyy}"></asp:BoundColumn>
								<asp:TemplateColumn HeaderText="Оплатить">
									<ItemTemplate>
										<asp:HyperLink id="lnkPay" runat="server" NavigateUrl='<%# "Support.aspx?" & DataBinder.Eval(Container, "DataItem.customer_sys_id") %>' Text='Оплатить'>
										</asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
			</table>
			<table cellSpacing="0" cellPadding="0" width="100%">
				<tr>
					<td style="HEIGHT: 20px" align="right" width="100%" bgColor="#9c0001"><A class="TopLink" href="#top">вверх&nbsp;</A></td>
				</tr>
			</table>
			<input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server"> <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
			<input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
			<input id="FindHidden" type="hidden" name="FindHidden" runat="server">
		</form>
	</body>
</HTML>

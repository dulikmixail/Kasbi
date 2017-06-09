<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Register TagPrefix="cc1" Namespace="Infragistics.WebUI.UltraWebGrid" Assembly="Infragistics.WebUI.UltraWebGrid.v3" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.TurnoverListByGood" CodeFile="TurnoverListByGood.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1"    runat ="server">
	<title >[Оборотная ведомость по товару]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" style="HEIGHT: 24px" width="100%" colspan="2">&nbsp;Отчеты 
						-&gt; Оборотная ведомость по товару</td>
				</tr>
				<tr>
					<td height="10" colspan="2"><asp:label id="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label></td>
				</tr>
				<tr>
					<td align="center" height="10"><asp:label id="lbGoodName" runat="server" EnableViewState="false" ForeColor="Black" Font-Bold="true"
							Font-Size="12pt" Font-Names="Verdana" CssClass="PanelHider"></asp:label></td>
					<td class="SectionRow"><asp:linkbutton id="lnkRestReport" runat="server" CssClass="LinkButton">
						<asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>
						&nbsp;Карточка&nbsp;складского&nbsp;учета</asp:linkbutton></td>
				</tr>
				<tr>
					<td align="center" colspan="2"><asp:datagrid id="grdTurnoverListByGood" runat="server" Font-Size="9pt" Font-Names="Verdana" BorderWidth="1px"
							BorderColor="#CC9966" Width="98%" AutoGenerateColumns="False" ShowFooter="True">
							<FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
							<HeaderStyle Font-Size="9pt" Font-Names="Verdana" HorizontalAlign="Center" ForeColor="#FFFFCC"
								BackColor="#990000"></HeaderStyle>
							<Columns>
								<asp:TemplateColumn HeaderText="№">
									<ItemTemplate>
										<asp:Label id="lblRecordNum" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Дата записи" HeaderStyle-Width="10%">
									<ItemTemplate>
										<asp:Label id="lnkDateDoc" runat="server"></asp:Label>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="№ док-та">
									<ItemTemplate>
										<asp:HyperLink id="lnkDocRecord" runat="server" HeaderStyle-Width="50%"></asp:HyperLink>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="От кого получено, кому отпущено" HeaderStyle-Width="50%">
									<ItemTemplate>
										<asp:Label id="lblCustomer" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblCurrentRest" runat="server"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Приход">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container, "DataItem.prichod_Kol") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Расход">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<ItemTemplate>
										<%# DataBinder.Eval(Container, "DataItem.rashod_Kol") %>
									</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Остаток">
									<ItemStyle HorizontalAlign="Right"></ItemStyle>
									<FooterStyle HorizontalAlign="Right"></FooterStyle>
									<ItemTemplate>
										<asp:Label id="lblOstatok" runat="server"></asp:Label>
									</ItemTemplate>
									<FooterTemplate>
										<asp:Label id="lblCurrentOstatok" runat="server"></asp:Label>
									</FooterTemplate>
								</asp:TemplateColumn>
							</Columns>
						</asp:datagrid></td>
				</tr>
				<tr>
					<td height="5" colspan="2"><cc1:ultrawebgrid id="ugrdTurnoverListByGood" runat="server" Width="100%" Height="400px">
							<DisplayLayout ColFootersVisibleDefault="Yes" RowHeightDefault="20px" Version="2.00" ViewType="Hierarchical"
								SelectTypeRowDefault="Extended" SelectTypeCellDefault="Extended" HeaderClickActionDefault="SortSingle"
								IndentationDefault="27" AllowColSizingDefault="Free" CellSpacingDefault="1" RowSelectorsDefault="No"
								Name="ugrdTurnoverListByGood" TableLayout="Fixed" CellClickActionDefault="RowSelect" SelectTypeColDefault="Extended"
								>
								<AddNewBox>
									<Style BorderWidth="1px" BorderColor="Silver" BorderStyle="Solid" BackColor="LightGray">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
									<ButtonStyle BorderWidth="1px" BorderColor="White" BorderStyle="None" ForeColor="Black" BackColor="Gray"></ButtonStyle>
								</AddNewBox>
								<Pager>
									<Style BorderWidth="1px" BorderColor="Gray" BorderStyle="Solid" ForeColor="#330099" BackColor="LightGray"
										Height="20px">

<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White">
</BorderDetails>

									</Style>
								</Pager>
								<HeaderStyleDefault Cursor="Default" BorderWidth="1px" BorderColor="#CC9966" BorderStyle="Solid" ForeColor="#FFFFCC"
									BackColor="#990000" Height="30px" Wrap="True">
									<Padding Left="0px" Top="0px"></Padding>
									<BorderDetails ColorTop="204, 153, 102" WidthLeft="0px" WidthTop="0px" ColorLeft="204, 153, 102"></BorderDetails>
								</HeaderStyleDefault>
								<GroupByRowStyleDefault BorderWidth="1px" BorderColor="Black" BorderStyle="Solid" ForeColor="Navy" BackColor="OldLace"></GroupByRowStyleDefault>
								<RowSelectorStyleDefault Width="20px" BorderColor="#CC9966" BorderStyle="Solid" HorizontalAlign="Center"
									BackColor="NavajoWhite"></RowSelectorStyleDefault>
								<FrameStyle Width="100%" Cursor="Default" BorderWidth="1px" Font-Size="8pt" Font-Names="Verdana"
									BorderColor="#CC9966" BorderStyle="Solid" ForeColor="White" BackColor="White" Height="400px"></FrameStyle>
								<FooterStyleDefault BorderWidth="1px" BorderStyle="Solid" BackColor="#FFFFCC">
									<BorderDetails ColorTop="White" WidthLeft="1px" WidthTop="1px" ColorLeft="White"></BorderDetails>
								</FooterStyleDefault>
								<ActivationObject BorderColor="204, 153, 102"></ActivationObject>
								<GroupByBox>
									<Style BorderWidth="1px" BorderColor="White" BorderStyle="Outset" ForeColor="White" BackColor="Black">
									</Style>
									<BandLabelStyle BorderWidth="1px" BorderColor="White" BorderStyle="Outset" BackColor="Gray"></BandLabelStyle>
								</GroupByBox>
								<RowExpAreaStyleDefault HorizontalAlign="Left"></RowExpAreaStyleDefault>
								<SelectedHeaderStyleDefault BorderWidth="1px" BorderColor="#CC9966" BorderStyle="Solid"></SelectedHeaderStyleDefault>
								<SelectedGroupByRowStyleDefault BorderWidth="1px" BorderColor="White" BorderStyle="Outset" ForeColor="White" BackColor="#CF5F5B"></SelectedGroupByRowStyleDefault>
								<SelectedRowStyleDefault Font-Bold="True" ForeColor="White" BackColor="LightSteelBlue"></SelectedRowStyleDefault>
								<RowAlternateStyleDefault BorderColor="#CC9966" ForeColor="Black" BackColor="White"></RowAlternateStyleDefault>
								<RowStyleDefault BorderWidth="1px" BorderColor="#CC9966" BorderStyle="Solid" ForeColor="Black" BackColor="White">
									<Padding Left="0px" Top="1px"></Padding>
									<BorderDetails ColorTop="204, 153, 102" WidthLeft="0px" WidthTop="0px" ColorLeft="204, 153, 102"></BorderDetails>
								</RowStyleDefault>
							</DisplayLayout>
							<Rows>
								<cc1:UltraGridRow Height="">
									<Cells>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
									</Cells>
								</cc1:UltraGridRow>
								<cc1:UltraGridRow Height="">
									<Cells>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
									</Cells>
								</cc1:UltraGridRow>
								<cc1:UltraGridRow Height="">
									<Cells>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
									</Cells>
								</cc1:UltraGridRow>
								<cc1:UltraGridRow Height="">
									<Cells>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
									</Cells>
								</cc1:UltraGridRow>
								<cc1:UltraGridRow Height="">
									<Cells>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
										<cc1:UltraGridCell Key="" Text="abc"></cc1:UltraGridCell>
									</Cells>
								</cc1:UltraGridRow>
							</Rows>
							<Bands>
								<cc1:UltraGridBand>
									<Columns>
										<cc1:UltraGridColumn Key="" BaseColumnName=""></cc1:UltraGridColumn>
										<cc1:UltraGridColumn Key="" BaseColumnName=""></cc1:UltraGridColumn>
										<cc1:UltraGridColumn Key="" BaseColumnName=""></cc1:UltraGridColumn>
									</Columns>
								</cc1:UltraGridBand>
							</Bands>
						</cc1:ultrawebgrid></td>
				</tr>
				<TR>
					<TD class="Unit" align="center" colSpan="2"><asp:imagebutton id="btnBack" runat="server" CausesValidation="False" ImageUrl="../Images/back.gif"></asp:imagebutton></TD>
				</TR>
				<tr>
					<td height="5" colspan="2">&nbsp;</td>
				</tr>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>

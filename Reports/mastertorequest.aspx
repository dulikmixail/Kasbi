<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.MasterTORequest" Culture="ru-RU" CodeFile="MasterTORequest.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head  runat="server">
    <title>[Отчет ТО по мастерам]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
		<script language="JavaScript" src="../scripts/datepicker.js"></script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt; ТО по мастерам</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="12pt">Label</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" width="100%">
							<asp:Panel id="pnlDates" runat="server">
								<TBODY>
									<TR>
										<TD class="SectionRowLabel" style="WIDTH: 127px">
											<asp:label id="Label1" runat="server">Начальная дата:</asp:label></TD>
										<TD class="SectionRow">
											<asp:textbox id="tbxBeginDate" BorderWidth="1px" Runat="server"></asp:textbox><A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>
											<asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
												ErrorMessage="Начальная дата">*</asp:requiredfieldvalidator>&nbsp;
											<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
											<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
												Operator="DataTypeCheck" Type="Date" Display="Dynamic" EnableClientScript="False">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator></TD>
									</TR>
									<TR>
										<TD class="SectionRowLabel" style="WIDTH: 127px">
											<asp:label id="Label3" runat="server">Конечная дата:</asp:label></TD>
										<TD class="SectionRow">
											<asp:textbox id="tbxEndDate" BorderWidth="1px" Runat="server"></asp:textbox><A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>
											<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
												ErrorMessage="Конечная дата ">*</asp:requiredfieldvalidator>&nbsp;
											<asp:label id="lblDateFormat3" runat="server" CssClass="text02"></asp:label>
											<asp:comparevalidator id="CompareValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
												Operator="DataTypeCheck" Type="Date" Display="Dynamic" EnableClientScript="False">Пожалуйста, введите корректные значение конечной даты</asp:comparevalidator></TD>
									</TR>
							</asp:Panel>
							<TR class="SubTitleTextbox">
								<TD style="HEIGHT: 10px"></TD>
								<TD style="HEIGHT: 10px" align="left" colSpan="2">&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
									<asp:label id="Label2" Runat="server">Месяц</asp:label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									<asp:label id="Label4" Runat="server">Год</asp:label></TD>
								<TD style="HEIGHT: 10px" align="left"></TD>
							</TR>
							<tr class="SubTitleTextbox">
								<td class="SectionRowLabel" align="left" noWrap>
									<asp:checkbox id="cbxPeriod" runat="server" Text="Выберите период" AutoPostBack="True"></asp:checkbox></td>
								<TD class="SectionRow" align="left"><asp:dropdownlist id="lstMonth" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
										<asp:ListItem Value="1">Январь</asp:ListItem>
										<asp:ListItem Value="2">Февраль</asp:ListItem>
										<asp:ListItem Value="3">Март</asp:ListItem>
										<asp:ListItem Value="4">Апрель</asp:ListItem>
										<asp:ListItem Value="5">Май</asp:ListItem>
										<asp:ListItem Value="6">Июнь</asp:ListItem>
										<asp:ListItem Value="7">Июль</asp:ListItem>
										<asp:ListItem Value="8">Август</asp:ListItem>
										<asp:ListItem Value="9">Сентябрь</asp:ListItem>
										<asp:ListItem Value="10">Октябрь</asp:ListItem>
										<asp:ListItem Value="11">Ноябрь</asp:ListItem>
										<asp:ListItem Value="12">Декабрь</asp:ListItem>
									</asp:dropdownlist><asp:dropdownlist id="lstYear" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
										<asp:ListItem Value="2003">2003</asp:ListItem>
										<asp:ListItem Value="2004">2004</asp:ListItem>
										<asp:ListItem Value="2005">2005</asp:ListItem>
										<asp:ListItem Value="2006">2006</asp:ListItem>
										<asp:ListItem Value="2007">2007</asp:ListItem>
										<asp:ListItem Value="2008">2008</asp:ListItem>
										<asp:ListItem Value="2009">2009</asp:ListItem>
										<asp:ListItem Value="2010">2010</asp:ListItem>
										<asp:ListItem Value="2011">2011</asp:ListItem>
										<asp:ListItem Value="2012">2012</asp:ListItem>
										<asp:ListItem Value="2013">2013</asp:ListItem>
                                        <asp:ListItem Value="2014">2014</asp:ListItem>
										<asp:ListItem Value="2015">2015</asp:ListItem>
										<asp:ListItem Value="2016">2016</asp:ListItem>
										<asp:ListItem Value="2017" Selected="True">2017</asp:ListItem>
									</asp:dropdownlist></TD>
							<TR>
								<td class="SectionRow">&nbsp;</td>
								<TD class="SectionRow" style="FONT-SIZE: 8pt; FONT-STYLE: italic">&nbsp;Фильтр</TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel" vAlign="top">Мастера :</TD>
								<TD class="SectionRow">
									<asp:ListBox id="lbxExecutor" runat="server" Width="250px" BackColor="#F6F8FC" Rows="5" SelectionMode="Multiple"></asp:ListBox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td class="SectionRow"><asp:linkbutton id="lnkExportReportToExcel" runat="server" CssClass="LinkButton">
						<asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>
						&nbsp;Экспорт&nbsp;отчета&nbsp;в&nbsp;Microsoft&nbsp;Excel</asp:linkbutton></td>
				</tr>
				<TR>
					<TD class="Unit" align="center">
						<asp:ImageButton id="btnView" runat="server" ImageUrl="../Images/create.gif"></asp:ImageButton>&nbsp;&nbsp;
						<asp:ImageButton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:ImageButton>
					</TD>
				</TR>
				</TBODY></TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" HeaderText="Заполните обязательные поля :"
				ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>

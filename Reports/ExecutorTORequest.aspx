<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.ExecutorTORequest" Culture="Ru-ru" CodeFile="ExecutorTORequest.aspx.vb" %>
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
        <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
        <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
        <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />

	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt;&nbsp;Реест&nbsp;за&nbsp;ТО&nbsp;по&nbsp;ответсвенному&nbsp;мастеру&nbsp;</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
                <TBODY>
				<TR>
					<TD align="left"><asp:label id="lblError" runat="server" Visible="False" Font-Bold="True" ForeColor="Red" Font-Size="12pt">Label</asp:label></TD>
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
											<asp:textbox id="tbxBeginDate" BorderWidth="1px" Runat="server"></asp:textbox>
                                            <%--<A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
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
											<asp:textbox id="tbxEndDate" BorderWidth="1px" Runat="server"></asp:textbox>
                                            <%--<A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
											<asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
												ErrorMessage="Конечная дата ">*</asp:requiredfieldvalidator>&nbsp;
											<asp:label id="lblDateFormat3" runat="server" CssClass="text02"></asp:label>
											<asp:comparevalidator id="CompareValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
												Operator="DataTypeCheck" Type="Date" Display="Dynamic" EnableClientScript="False">Пожалуйста, введите корректные значение конечной даты</asp:comparevalidator></TD>
									</TR>
							</asp:Panel>
							<TR>
								<TD class="SectionRowLabel" vAlign="top">Мастера :</TD>
								<TD class="SectionRow">
									<asp:ListBox id="lbxExecutor" runat="server" Width="250px" BackColor="#F6F8FC" Rows="10" SelectionMode="Multiple"></asp:ListBox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<tr>
					<td class="SectionRow"><asp:linkbutton id="lnkExportReportToExcel" runat="server" CssClass="LinkButtonExport" Visible="False">
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

            <script language="javascript">
                jQuery(function () {

                    jQuery('#tbxBeginDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                        scrollMonth: false,
                    });

                    jQuery('#tbxEndDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                        scrollMonth: false,
                    });

                });

         </script>

			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" HeaderText="Заполните обязательные поля :"
				ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>

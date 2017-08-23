<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.DealersRequest" CodeFile="DealersRequest.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>УП Рамок - Отчет по дилерам</title>
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
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt; Отчет по дилерам</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblError" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True" Visible="False">Label</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" width="100%">
							<TR>
								<TD class="SectionRowLabel" style="WIDTH: 127px"><asp:label id="Label1" runat="server" CssClass="text02">Начальная дата:</asp:label></TD>
								<TD class="SectionRow"><asp:textbox id="tbxBeginDate" Runat="server" BorderWidth="1px"></asp:textbox>
                                    <%--<A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel" style="WIDTH: 127px"><asp:label id="Label3" runat="server" CssClass="text02">Конечная дата:</asp:label></TD>
								<TD class="SectionRow"><asp:textbox id="tbxEndDate" Runat="server" BorderWidth="1px"></asp:textbox>
                                    <%--<A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата "
										ControlToValidate="tbxEndDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat3" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="CompareValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение конечной даты</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD width="20"></TD>
								<TD class="SectionRow"><asp:checkbox id="chkDealersDept" runat="server" CssClass="text02" Text="Текущий долг по дилерам"
										AutoPostBack="True"></asp:checkbox></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
				<TR id="pnlGoods" runat="server">
					<TD align="center"></TD>
				</TR>
				<TR>
					<TD class="Unit" align="center"><asp:imagebutton id="btnView" runat="server" ImageUrl="../Images/create.gif"></asp:imagebutton>&nbsp;&nbsp;
						<asp:imagebutton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:imagebutton></TD>
				</TR>
			</TABLE>

            <script language="javascript">
            jQuery(function () {

                    jQuery('#tbxBeginDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                    });

                    jQuery('#tbxEndDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                    });

             });

         </script>

			<uc1:footer id="Footer1" runat="server"></uc1:footer><asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" ShowMessageBox="True"
				ShowSummary="False" HeaderText="Заполните обязательные поля :"></asp:validationsummary></form>
	</body>
</HTML>

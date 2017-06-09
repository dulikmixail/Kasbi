<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.SellingRequest" Culture="ru-RU" CodeFile="SellingRequest.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head id="Head1" runat ="server">
	<title >[Отчет по продажам]</title>
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
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt; Отчет по продажам</td>
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
								<TD class="SectionRow"><asp:textbox id="tbxBeginDate" Runat="server" BorderWidth="1px"></asp:textbox><A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel" style="WIDTH: 127px"><asp:label id="Label3" runat="server" CssClass="text02">Конечная дата:</asp:label></TD>
								<TD class="SectionRow"><asp:textbox id="tbxEndDate" Runat="server" BorderWidth="1px"></asp:textbox><A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата "
										ControlToValidate="tbxEndDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat3" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="CompareValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение конечной даты</asp:comparevalidator></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Выборка:
								</TD>
								<TD class="SectionRow" colSpan="3"><asp:radiobuttonlist id="rblReportType" runat="server" CssClass="text02" AutoPostBack="True" RepeatDirection="Horizontal">
										
										<asp:ListItem Value="1" Selected="True">По клиентам</asp:ListItem>
										<asp:ListItem Value="2">По товарам</asp:ListItem>
										<asp:ListItem Value="3">По менеджерам (свои клиенты)
										
										</asp:ListItem>
										<asp:ListItem Value="4">По менеджерам (фактические продажи)</asp:ListItem>
									</asp:radiobuttonlist>
									
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									&nbsp;&nbsp;
									<asp:CheckBox Visible="false" runat="server" ID="chkManager" Text="Считать клиентов по первым продажам за период" Font-Size="10"  />
									</TD>
							</TR>
							<TR id="pnlClient" runat="server">
								<TD class="SectionRowLabel">Клиенты:
								</TD>
								<TD class="SectionRow">
									<TABLE id="Table5" cellSpacing="1" cellPadding="2" width="100%" border="0">
										<TR>
											<TD><asp:radiobutton id="rbtnClientSet1" runat="server" CssClass="text02" Checked="True" GroupName="ClientSet"
													Text="По всем"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton AutoPostBack="true" id="rbtnClientSet2" runat="server" CssClass="text02" GroupName="ClientSet" Text="Только ЦТО"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rbtnClientSet3" runat="server" CssClass="text02" GroupName="ClientSet" Text="Исключая ЦТО"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rbtnClientSet4" runat="server" CssClass="text02" GroupName="ClientSet" Text="По списку"></asp:radiobutton>
											</TD>
										<TR>
											<TD>
											<asp:TextBox ID="txtCustomerFind" runat="server" BackColor="#F6F8FC" Width="70%"
                                                MaxLength="11" BorderWidth="1px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">Найти</asp:LinkButton>
                                            <br />
												<asp:listbox Width="70%" id="lstClient" runat="server" Rows="8" SelectionMode="Multiple"></asp:listbox>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR id="pnlGoods" runat="server">
								<TD class="SectionRowLabel">Товары:
								</TD>
								<TD class="SectionRow">
									<TABLE id="Table3" cellSpacing="1" cellPadding="2" width="100%" border="0">
										<TR>
											<TD vAlign="top">
												<asp:radiobutton id="rbtnGootTypeSet1" runat="server" CssClass="text02" Checked="True" GroupName="ClientSet"
													Text="По всем"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rbtnGootTypeSet2" runat="server" CssClass="text02" GroupName="ClientSet" Text="По списку"></asp:radiobutton>
											</TD>
										</TR>
										<TR>
											<TD><asp:listbox id="lstGoodType" runat="server" Rows="8" SelectionMode="Multiple"></asp:listbox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR id="pnlManager" runat="server"> 
								<TD class="SectionRowLabel">Менеджеры:
								</TD>
								<TD class="SectionRow">
									<TABLE id="Table4" cellSpacing="1" cellPadding="2" width="100%" border="0">
										<TR>
											<TD vAlign="top">
												<asp:radiobutton id="rbtnManagerSet1" runat="server" CssClass="text02" Checked="True" GroupName="ClientSet" Text="По всем"></asp:radiobutton>&nbsp;&nbsp;
												<asp:radiobutton id="rbtnManagerSet2" runat="server" CssClass="text02" GroupName="ClientSet" Text="По списку"></asp:radiobutton>
											</TD>
										</TR>
										<TR>
											<TD><asp:listbox id="lstManager" runat="server" Rows="8" Width="500" SelectionMode="Multiple"></asp:listbox></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>

						</TABLE>
					</TD>
				</TR>
				<TR>
					<TD class="Unit" align="center">
						<asp:imagebutton id="btnView" runat="server" ImageUrl="../Images/create.gif"></asp:imagebutton>&nbsp;&nbsp;
						<asp:imagebutton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:imagebutton>
					</TD>
				</TR>
			</TABLE>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" ShowMessageBox="True"
				ShowSummary="False" HeaderText="Заполните обязательные поля :"></asp:validationsummary></form>
	</body>
</HTML>

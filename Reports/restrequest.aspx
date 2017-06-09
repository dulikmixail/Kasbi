<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.RestRequest" CodeFile="RestRequest.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head runat ="server">
	<title >[Отчет по остаткам]</title>
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
					<td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt; Отчет по остаткам</td>
				</tr>
			</table>
			<TABLE id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<TR>
					<TD align="center"><asp:label id="lblError" runat="server" Font-Size="12pt" ForeColor="Red" Font-Bold="True" Visible="False">Label</asp:label></TD>
				</TR>
				<TR>
					<TD>
						<TABLE id="Table2" width="100%">
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
			<uc1:footer id="Footer1" runat="server"></uc1:footer></form>
	</body>
</HTML>

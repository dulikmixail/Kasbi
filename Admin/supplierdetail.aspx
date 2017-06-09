<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.SupplierDetail" CodeFile="SupplierDetail.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat ="server">
    <title>[���������� � ����������]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%">&nbsp;����������������� 
						-&gt;&nbsp;����������&nbsp;�&nbsp;����������</td>
				</tr>
			</table>
			<table cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="Unit" vAlign="top">����������&nbsp;�&nbsp;����������</td>
				</tr>
				<TR>
					<TD width="100%">
						<asp:label id="msg" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red"></asp:label></TD>
				</TR>
				<tr>
					<td>
						<TABLE cellSpacing="0" cellPadding="0" width="100%">
							<TBODY>
								<TR>
									<TD class="SectionRowLabel" align="left" width="20%">������� �:</TD>
									<TD class="SectionRow" colSpan="4">
										<asp:textbox id="txtDogovor" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="70px"
											MaxLength="10"></asp:textbox>
									</TD>
									<TD class="SectionRowLabel" colspan="2" width="35%">&nbsp;</TD>
								</TR>
								<TR class="SubTitleTextbox">
									<TD class="SubTitleTextbox" colspan="2">&nbsp;</TD>
									<TD class="SubTitleTextbox" align="left">��������</TD>
									<TD class="SubTitleTextbox" colspan="4"></TD>
								</TR>
								<TR>
									<TD class="SectionRowLabel" align="left">�����������:</TD>
									<TD class="SectionRow" colSpan="1">
										<asp:textbox id="txtSupplierAbr" BorderWidth="1px" BackColor="#F6F8FC" Width="80px" ToolTip="������� ������������ ����������� (������ ��� �����������)"
											MaxLength="50" Runat="server"></asp:textbox><asp:label id="btnSupplierAbr" style="CURSOR: hand" runat="server" ForeColor="SteelBlue" Font-Size="10pt"
											Font-Bold="True">...</asp:label><asp:listbox id="lstSupplierAbr" runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="75px"
											cssclass="hiddencitylist3" Rows="10"></asp:listbox>
									</TD>
									<TD class="SectionRow" colSpan="3">
										<asp:textbox id="txtSupplierName" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="98%"
											ToolTip="������� �������� ����������� (������ ��� �����������)" MaxLength="90"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="txtSupplierName"
											ErrorMessage="�������� ����������" Display="None">*</asp:requiredfieldvalidator>
									</TD>
									<TD class="SectionRowLabel">&nbsp;</TD>
								</TR>
								<TR>
									<TD class="SubTitleTextbox">&nbsp;</TD>
									<TD class="SubTitleTextbox">&nbsp;</TD>
									<TD class="SubTitleTextbox" align="left">���</TD>
									<TD class="SubTitleTextbox" align="left">��� �����</TD>
									<TD class="SubTitleTextbox" colspan="3"></TD>
								</TR>
								<TR>
									<td class="SectionRow" colspan="2">&nbsp;</td>
									<TD class="SectionRow">
										<asp:textbox id="txtUNN" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%" ToolTip="������� ��� �����������"
											MaxLength="9"></asp:textbox>
									</TD>
									<TD class="SectionRow">
										<asp:textbox id="txtOKPO" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%" ToolTip="������� ��� �����"
											MaxLength="15"></asp:textbox>
									</TD>
									<TD class="SectionRowLabel" colspan="2">&nbsp;</TD>
								</TR>
								<TR class="TitleTextbox">
									<TD class="SubTitleTextbox">&nbsp;</TD>
									<TD class="SubTitleTextbox" align="left">�������</TD>
									<TD class="SubTitleTextbox" align="left">���</TD>
									<TD class="SubTitleTextbox" align="left">��������</TD>
								</TR>

								<TR>
									<TD class="SectionRowLabel" align="left">������������:</TD>
									<TD class="SectionRow"><asp:textbox id="txtBoosLastName" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%"
											ToolTip="������� ������� ������������" MaxLength="50"></asp:textbox></TD>
									<TD class="SectionRow"><asp:textbox id="txtBoosFirstName" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%"
											ToolTip="������� ��� ������������" MaxLength="50"></asp:textbox></TD>
									<TD class="SectionRow"><asp:textbox id="txtBoosPatronymicName" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
											Width="95%" ToolTip="������� �������� ������������" MaxLength="50"></asp:textbox></TD>
									<TD class="SectionRowLabel" colspan="2">&nbsp;</TD>
								</TR>
								<TR class="TitleTextbox">
									<TD class="SubTitleTextbox">&nbsp;</TD>
									<TD class="SubTitleTextbox" align="left">������</TD>
									<TD class="SubTitleTextbox" align="left">�����</TD>
									<TD class="SubTitleTextbox" align="left">������</TD>
								</TR>
								<TR>
									<TD class="SectionRowLabel" align="left">�����:</TD>
									<TD class="SectionRow">
										<asp:textbox id="txtCountry" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%"
											ToolTip="������� ������ ����������" MaxLength="50"></asp:textbox>
										<asp:requiredfieldvalidator id="Requiredfieldvalidator3" runat="server" ControlToValidate="txtCountry" ErrorMessage="������ ����������"
											Display="None">*</asp:requiredfieldvalidator>
									</TD>
									<TD class="SectionRow">
										<asp:textbox id="txtCity" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%" ToolTip="������� ����� ����������"
											MaxLength="50"></asp:textbox>
										<asp:requiredfieldvalidator id="Requiredfieldvalidator4" runat="server" ControlToValidate="txtCity" ErrorMessage="����� ����������"
											Display="None">*</asp:requiredfieldvalidator>
									</TD>
									<TD class="SectionRow">
										<asp:textbox id="txtZipCode" runat="server" BorderWidth="1px" BackColor="#F6F8FC" Width="95%"
											ToolTip="������� ������ ����������" MaxLength="50"></asp:textbox>
									</TD>
									<TD class="SectionRowLabel" colspan="2">&nbsp;</TD>
								</TR>
								<TR class="TitleTextbox">
									<TD class="SubTitleTextbox" align="left">&nbsp;</TD>
									<TD class="SubTitleTextbox" colspan="3" align="left">&nbsp;����� , ���</TD>
									<TD class="SubTitleTextbox" colspan="2" align="left">&nbsp;</TD>
								</TR>
								<TR>
									<TD class="SectionRow" align="left">&nbsp;</TD>
									<TD class="SectionRow" colspan="3"><asp:textbox id="txtAddress" runat="server" BorderWidth="1px" BackColor="#F6F8FC" ToolTip="������� �����,���"
											Width="99%"></asp:textbox></TD>
									<td class="SectionRow" align="left" colspan="2">&nbsp;</td>
								</TR>
								<TR class="SubTitleTextbox">
									<TD width="80">&nbsp;</TD>
									<TD class="SubTitleTextbox" align="left">����</TD>
									<TD class="SubTitleTextbox" align="left">�������</TD>
									<TD class="SubTitleTextbox" align="left">��������</TD>
									<TD class="SubTitleTextbox" align="left">���������</TD>
									<TD class="SubTitleTextbox" align="left" colspan="2">&nbsp;</TD>
								</TR>
								<TR>
									<TD class="SectionRowLabel" align="left">��������:</TD>
									<TD class="SectionRow" align="left">
										<asp:textbox id="txtPhone1" runat="server" ToolTip="������� ����� �����" Width="95%" BorderWidth="1px"
											BackColor="#F6F8FC" MaxLength="20"></asp:textbox></TD>
									<TD class="SectionRow" align="left">
										<asp:textbox id="txtPhone2" runat="server" ToolTip="������� ������� 1" Width="95%" BorderWidth="1px"
											BackColor="#F6F8FC" MaxLength="20"></asp:textbox></TD>
									<TD class="SectionRow" align="left">
										<asp:textbox id="txtPhone3" runat="server" ToolTip="������� ������� 2" Width="95%" BorderWidth="1px"
											BackColor="#F6F8FC" MaxLength="20"></asp:textbox></TD>
									<TD class="SectionRow" align="left">
										<asp:textbox id="txtPhone4" runat="server" ToolTip="������� ������� 3" Width="95%" BorderWidth="1px"
											BackColor="#F6F8FC" MaxLength="20"></asp:textbox></TD>
									<td class="SectionRow" align="left" colspan="2">&nbsp;</td>
								</TR>
								<TR>
									<TD class="SectionRowLabel" align="left">��������:</TD>
									<TD class="SectionRow" align="left" colspan="5">
										<asp:CheckBox id="chkNadbavka" runat="server"></asp:CheckBox>
									</TD>
								</TR>
								<tr>
									<td colspan="6"></td>
								</tr>
								<tr>
									<td colspan="6" align="center">
										<asp:label id="lblSqlError" runat="server" ForeColor="Red" Font-Size="8pt"></asp:label>
									</td>
								</tr>
							</TBODY>
						</TABLE>
					</td>
				</tr>
				<TR>
					<td class="Unit" align="center" colSpan="2" height="19">
						<asp:imagebutton id="cmdCancel" runat="server" ImageUrl="../Images/cancel.gif" CommandName="Cancel"
							CausesValidation="False"></asp:imagebutton>&nbsp;&nbsp;
						<asp:imagebutton id="cmdSave" runat="server" ImageUrl="../Images/update.gif" CommandName="Edit"></asp:imagebutton>
					</td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" HeaderText="��������� ������������ ���� :"
				ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>

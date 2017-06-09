<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Customer" CodeFile="Customer.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <head  runat ="server">
    <title>[���������� � �������]</title>
<meta content="Microsoft Visual Studio.NET 7.0" name=GENERATOR>
<meta content="Visual Basic 7.0" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema><LINK href="Styles.css" type=text/css rel=stylesheet >
  </HEAD>
<body 
onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;" 
bottomMargin=0 leftMargin=0 topMargin=0 
onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;" 
rightMargin=0>
<form id=frmNewRequest method=post runat="server"><uc1:header id=Header1 runat="server"></uc1:header>
<table class=PageTitle cellSpacing=1 cellPadding=2 width="100%" border=0>
  <tr>
    <td class=HeaderTitle width="100%" 
      >&nbsp;��������&nbsp;�������</TD></TR></TABLE><asp:panel 
id=ControlsPanel Runat="server" EnableViewState="true">
<TABLE cellSpacing=1 cellPadding=2 width="100%" border=0>
  <TR class=Unit>
    <TD class=Unit width="100%">&nbsp;����������&nbsp;�&nbsp;������� </TD></TR>
  <TR>
    <TD width="100%">
<asp:label id=msg runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:label></TD></TR>
  <TR>
    <TD width="100%">
<asp:label id=msgAddCustomer runat="server" EnableViewState="false" ForeColor="#ff0000" Font-Bold="true"></asp:label></TD></TR>
  <TR>
    <TD width="100%">
<asp:Panel id=CustomerPanel Runat="server">
      <TABLE cellSpacing=0 cellPadding=0 width="90%" border=0>
        <TR>
          <TD>&nbsp;</TD>
          <TD colSpan=8>
            <HR>
          </TD></TR>
        <TR class=TitleTextbox>
          <TD width="20%" height=5>&nbsp;</TD>
          <TD width="5%">&nbsp;</TD>
          <TD width="5%">&nbsp;</TD>
          <TD width="5%">&nbsp;</TD>
          <TD width="10%">&nbsp;</TD>
          <TD width="30%">&nbsp;</TD>
          <TD width="30%">&nbsp;</TD>
          <TD width="10%">&nbsp;</TD>
          <TD width="10%">&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRowLabel>�����������:</TD>
          <TD class=SectionRow>
<asp:RadioButton id=rdbtnIP runat="server" Checked="True" GroupName="Organization" Font-Size="8pt" Text="��" ToolTip="�������������� ���������������"></asp:RadioButton><BR>
<asp:RadioButton id=rdbtnOrganization runat="server" GroupName="Organization" Font-Size="8pt" Text="�����������" ToolTip="�����������"></asp:RadioButton></TD>
          <TD class=SectionRow>&nbsp;</TD>
          <TD class=SectionRow colSpan=2>&nbsp; 
<asp:checkbox id=chkNDS runat="server" Font-Size="8pt" Text="���������� ���" ToolTip="���������� ���"></asp:checkbox></TD>
          <TD class=SectionRow>&nbsp;</TD>
          <TD class=SectionRow align=left> 
<asp:checkbox id=chkCTO runat="server" Font-Size="8pt" Text="������"></asp:checkbox><BR>
<asp:checkbox id=chkSupport runat="server" Checked="True" Font-Size="8pt" Text="��"></asp:checkbox></TD>
          <TD class=SectionRow colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD>&nbsp;</TD>
          <TD colSpan=8>
            <HR>
          </TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>������� �:</TD>
          <TD class=SectionRow align=left colSpan=3>
<asp:textbox id=txtDogovor runat="server" MaxLength="10" BackColor="#F6F8FC" BorderWidth="1px" Width="100px"></asp:textbox></TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>��������</TD>
          <TD class=SubTitleTextbox align=left>���</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left>
<asp:TextBox id=txtCustomerAbr Runat="server" ToolTip="������� ������������ ����������� (������ ��� �����������)" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100px"></asp:TextBox>
<asp:label id=btnCustomerAbr style="CURSOR: hand" runat="server" ForeColor="SteelBlue" Font-Bold="True" Font-Size="10pt">...</asp:label></TD>
          <TD class=SectionRow align=left colSpan=4>
<asp:textbox id=txtCustomerName runat="server" ToolTip="������� �������� ����������� (������ ��� �����������)" MaxLength="90" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtUNN runat="server" ToolTip="������� ��� �����������" MaxLength="9" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=5>�����������</TD>
          <TD class=SubTitleTextbox align=left>��� �����</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left colSpan=5>
<asp:textbox id=txtRegistration runat="server" ToolTip="������� ���������� � �����������" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtOKPO runat="server" ToolTip="������� ��� �����" MaxLength="9" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>���������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRowLabel align=left>���������� � ��������:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtBranch runat="server" ToolTip="������� ���������� � �������� � ������������������" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>�������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>���</TD>
          <TD class=SubTitleTextbox align=left>��������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRowLabel align=left>������������:</TD>
          <TD class=SectionRow align=right colSpan=3>
<asp:textbox id=txtBoosLastName runat="server" ToolTip="������� ������� ������������" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtBoosFirstName runat="server" ToolTip="������� ��� ������������" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtBoosPatronymicName runat="server" ToolTip="������� �������� ������������" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>
<asp:label id=lnkAccountant style="CURSOR: hand" runat="server">���, ��������</asp:label></TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>����.&nbsp;���.:</TD>
          <TD class=SectionRow align=right colSpan=6>
<asp:textbox id=txtAccountant runat="server" ToolTip="������� ��� � �������� �������� ����������" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=1>������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>�������</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>�����</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>�����:</TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtZipCode runat="server" ToolTip="������� �������� ������" MaxLength="6" BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>
<asp:DropDownList id=lstRegion runat="server" BackColor="#F6F8FC" Width="99%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value="������� ���.">�������</asp:ListItem>
													<asp:ListItem Value="��������� ���.">���������</asp:ListItem>
													<asp:ListItem Value="��������� ���.">���������</asp:ListItem>
													<asp:ListItem Value="���������� ���.">����������</asp:ListItem>
													<asp:ListItem Value="����������� ���.">�����������</asp:ListItem>
													<asp:ListItem Value="����������� ���.">�����������</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=right colSpan=3>
<asp:textbox id=txtRegion runat="server" ToolTip="������� �����" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>�����</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>�����</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>&nbsp;</TD>
          <TD class=SectionRow align=left>
<asp:DropDownList id=lstCityAbr runat="server" BackColor="#F6F8FC" Width="70%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem Value="�.">�.</asp:ListItem>
													<asp:ListItem Value="�.�.">�.�.</asp:ListItem>
													<asp:ListItem Value="�.">�.</asp:ListItem>
													<asp:ListItem Value="���.">���.</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtCity runat="server" ToolTip="������� �������� ������" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="97%">�����</asp:textbox></TD>
          <TD class=SectionRow align=left>
<asp:label id=btnCity style="CURSOR: hand" runat="server" ForeColor="SteelBlue" Font-Bold="True" Font-Size="10pt">...</asp:label></TD>
          <TD class=SectionRow align=left>
<asp:DropDownList id=lstStreetAbr runat="server" BackColor="#F6F8FC" Width="97%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem Value="��.">��.</asp:ListItem>
													<asp:ListItem Value="��.">��.</asp:ListItem>
													<asp:ListItem Value="���.">���.</asp:ListItem>
													<asp:ListItem Value="�-�">�-�</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtAddress runat="server" ToolTip="������� �����" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>����</TD>
          <TD class=SubTitleTextbox align=left>�������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>��������</TD>
          <TD class=SubTitleTextbox align=left>���������</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtPhone1 runat="server" ToolTip="������� ����� �����" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtPhone2 runat="server" ToolTip="������� ������� 1" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtPhone3 runat="server" ToolTip="������� ������� 2" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtPhone4 runat="server" ToolTip="������� ������� 3" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=3>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>���������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>��������� ��������� ��:</TD>
          <TD class=SectionRow vAlign=top align=right colSpan=6>
<asp:textbox id=txtTaxInspection runat="server" ToolTip="������� ��������� ��������� ���������" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%" Visible="False"></asp:textbox><BR>
<asp:DropDownList id=dlstIMNS runat="server" BackColor="#F6F8FC" Width="100%" CssClass="lstLineUp"></asp:DropDownList></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left>���</TD>
          <TD class=SubTitleTextbox align=left>�/�</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>��������</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>����:</TD>
          <TD class=SectionRow align=left>
<asp:dropdownlist id=lstBank runat="server" BackColor="#F6F8FC" Width="100px" Height="18px" AutoPostBack="True"></asp:dropdownlist></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtBankAccount runat="server" ToolTip="������� ��������� ����" MaxLength="13" BackColor="#F6F8FC" BorderWidth="1px" Width="99%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=4>
<asp:textbox id=txtBankName runat="server" ToolTip="������� �������� �����" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>�����</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtBankAddress runat="server" ToolTip="������� ����� �����" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>���������� ����:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtProxy runat="server" ToolTip="������� ��� ����������� ����" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>�������������� ����������:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtInfo runat="server" ToolTip="������� �������������� ���������� � ����� �����������" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>��������� �������� � ���:</TD>
          <TD class=SectionRow align=left colSpan=3>
<asp:DropDownList id=lstAdvertising runat="server" BackColor="#F6F8FC" Width="449px" CssClass="lstLineUp"></asp:DropDownList></TD>
          <TD class=SectionRow align=left colSpan=5>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>�������:</TD>
          <TD class=SectionRowLabel align=left colSpan=5>
<asp:CheckBox id=chkAlert runat="server" Font-Size="8pt" Text="����������� ������"></asp:CheckBox></TD></TR></TABLE></asp:Panel>
<asp:ListBox id=Listbox1 runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="129px" cssclass="hiddencitylist1" Rows="10"></asp:ListBox></TD>
<asp:ListBox id=Listbox2 runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="75px" cssclass="hiddencitylist2" Rows="10"></asp:ListBox></TD></TR></TABLE></asp:panel><asp:listbox id=lstCity runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="129px" cssclass="hiddencitylist1" Rows="10"></asp:listbox></TD><asp:listbox id=lstCustomerAbr runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="75px" cssclass="hiddencitylist2" Rows="10"></asp:listbox></TD></TR></TABLE><asp:panel 
id=AddClientPanel Runat="server">
<TABLE cellSpacing=0 cellPadding=0 width="100%">
  <TR>
    <TD width="100%"></TD></TR>
  <TR class=Unit>
    <TD class=Unit align=center>
<asp:imagebutton id=btnClear runat="server" ImageUrl="Images/clear.gif"></asp:imagebutton>&nbsp;&nbsp; 
<asp:ImageButton id=btnCancel runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel"></asp:ImageButton>&nbsp;&nbsp; 
<asp:imagebutton id=btnAdd runat="server" ImageUrl="Images/update.gif"></asp:imagebutton></TD></TR>
  <TR>
    <TD height=10></TD></TR></TABLE></asp:panel><uc1:footer id=Footer1 runat="server"></uc1:footer><input 
id=scrollPos type=hidden value=0 name=scrollPos 
runat="server"> <input lang=ru id=CurrentPage type=hidden name=CurrentPage runat="server"> <input lang=ru id=Parameters type=hidden 
name=Parameters runat="server"> <input id=FindHidden 
type=hidden name=FindHidden runat="server"> 
</FORM>
	</body>
</HTML>

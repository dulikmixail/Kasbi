<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Customer" CodeFile="Customer.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <head  runat ="server">
    <title>[Информация о клиенте]</title>
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
      >&nbsp;Карточка&nbsp;клиента</TD></TR></TABLE><asp:panel 
id=ControlsPanel Runat="server" EnableViewState="true">
<TABLE cellSpacing=1 cellPadding=2 width="100%" border=0>
  <TR class=Unit>
    <TD class=Unit width="100%">&nbsp;Информация&nbsp;о&nbsp;клиенте </TD></TR>
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
          <TD class=SectionRowLabel>Организация:</TD>
          <TD class=SectionRow>
<asp:RadioButton id=rdbtnIP runat="server" Checked="True" GroupName="Organization" Font-Size="8pt" Text="ИП" ToolTip="Индивидуальный предприниматель"></asp:RadioButton><BR>
<asp:RadioButton id=rdbtnOrganization runat="server" GroupName="Organization" Font-Size="8pt" Text="Организация" ToolTip="Организация"></asp:RadioButton></TD>
          <TD class=SectionRow>&nbsp;</TD>
          <TD class=SectionRow colSpan=2>&nbsp; 
<asp:checkbox id=chkNDS runat="server" Font-Size="8pt" Text="Плательщик НДС" ToolTip="Плательщик НДС"></asp:checkbox></TD>
          <TD class=SectionRow>&nbsp;</TD>
          <TD class=SectionRow align=left> 
<asp:checkbox id=chkCTO runat="server" Font-Size="8pt" Text="Дилеры"></asp:checkbox><BR>
<asp:checkbox id=chkSupport runat="server" Checked="True" Font-Size="8pt" Text="ТО"></asp:checkbox></TD>
          <TD class=SectionRow colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD>&nbsp;</TD>
          <TD colSpan=8>
            <HR>
          </TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Договор №:</TD>
          <TD class=SectionRow align=left colSpan=3>
<asp:textbox id=txtDogovor runat="server" MaxLength="10" BackColor="#F6F8FC" BorderWidth="1px" Width="100px"></asp:textbox></TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>Название</TD>
          <TD class=SubTitleTextbox align=left>УНП</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left>
<asp:TextBox id=txtCustomerAbr Runat="server" ToolTip="Введите аббревиатуру организации (только для организаций)" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100px"></asp:TextBox>
<asp:label id=btnCustomerAbr style="CURSOR: hand" runat="server" ForeColor="SteelBlue" Font-Bold="True" Font-Size="10pt">...</asp:label></TD>
          <TD class=SectionRow align=left colSpan=4>
<asp:textbox id=txtCustomerName runat="server" ToolTip="Введите название организации (только для организаций)" MaxLength="90" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtUNN runat="server" ToolTip="Введите УНП организации" MaxLength="9" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=5>Регистрация</TD>
          <TD class=SubTitleTextbox align=left>Код ОКЮЛП</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left colSpan=5>
<asp:textbox id=txtRegistration runat="server" ToolTip="Введите информацию о регистрации" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtOKPO runat="server" ToolTip="Введите код ОКЮЛП" MaxLength="9" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>Реквизиты</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRowLabel align=left>Информация о филиалах:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtBranch runat="server" ToolTip="Введите информацию о филиалах и представительствах" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>Фамилия</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>Имя</TD>
          <TD class=SubTitleTextbox align=left>Отчество</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRowLabel align=left>Руководитель:</TD>
          <TD class=SectionRow align=right colSpan=3>
<asp:textbox id=txtBoosLastName runat="server" ToolTip="Введите фамилию руководителя" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtBoosFirstName runat="server" ToolTip="Введите имя руководителя" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtBoosPatronymicName runat="server" ToolTip="Введите отчество руководителя" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>
<asp:label id=lnkAccountant style="CURSOR: hand" runat="server">ФИО, телефоны</asp:label></TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Глав.&nbsp;бух.:</TD>
          <TD class=SectionRow align=right colSpan=6>
<asp:textbox id=txtAccountant runat="server" ToolTip="Введите ФИО и телефоны главного бухгалтера" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=1>Индекс</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>Область</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>Район</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Адрес:</TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtZipCode runat="server" ToolTip="Введите почтовый индекс" MaxLength="6" BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>
<asp:DropDownList id=lstRegion runat="server" BackColor="#F6F8FC" Width="99%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem></asp:ListItem>
													<asp:ListItem Value="Минская обл.">Минская</asp:ListItem>
													<asp:ListItem Value="Брестская обл.">Брестская</asp:ListItem>
													<asp:ListItem Value="Витебская обл.">Витебская</asp:ListItem>
													<asp:ListItem Value="Гомельская обл.">Гомельская</asp:ListItem>
													<asp:ListItem Value="Гродненская обл.">Гродненская</asp:ListItem>
													<asp:ListItem Value="Могилевская обл.">Могилевская</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=right colSpan=3>
<asp:textbox id=txtRegion runat="server" ToolTip="Введите адрес" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>Город</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>Адрес</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>&nbsp;</TD>
          <TD class=SectionRow align=left>
<asp:DropDownList id=lstCityAbr runat="server" BackColor="#F6F8FC" Width="70%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem Value="г.">г.</asp:ListItem>
													<asp:ListItem Value="г.п.">г.п.</asp:ListItem>
													<asp:ListItem Value="д.">д.</asp:ListItem>
													<asp:ListItem Value="пос.">пос.</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=left>
<asp:textbox id=txtCity runat="server" ToolTip="Введите название города" MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="97%">Минск</asp:textbox></TD>
          <TD class=SectionRow align=left>
<asp:label id=btnCity style="CURSOR: hand" runat="server" ForeColor="SteelBlue" Font-Bold="True" Font-Size="10pt">...</asp:label></TD>
          <TD class=SectionRow align=left>
<asp:DropDownList id=lstStreetAbr runat="server" BackColor="#F6F8FC" Width="97%" cssclass="lstLineUp" Height="18px">
													<asp:ListItem Value="ул.">ул.</asp:ListItem>
													<asp:ListItem Value="пр.">пр.</asp:ListItem>
													<asp:ListItem Value="пер.">пер.</asp:ListItem>
													<asp:ListItem Value="б-р">б-р</asp:ListItem>
												</asp:DropDownList></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtAddress runat="server" ToolTip="Введите адрес" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>Факс</TD>
          <TD class=SubTitleTextbox align=left>рабочий</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>домашний</TD>
          <TD class=SubTitleTextbox align=left>мобильный</TD>
          <TD class=SubTitleTextbox align=left colSpan=3>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtPhone1 runat="server" ToolTip="Введите номер факса" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtPhone2 runat="server" ToolTip="Введите телефон 1" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=2>
<asp:textbox id=txtPhone3 runat="server" ToolTip="Введите телефон 2" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtPhone4 runat="server" ToolTip="Введите телефон 3" MaxLength="20" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=3>&nbsp;</TD></TR>
        <TR class=SubTitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>Реквизиты</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Налоговая инспекция по:</TD>
          <TD class=SectionRow vAlign=top align=right colSpan=6>
<asp:textbox id=txtTaxInspection runat="server" ToolTip="Введите реквизиты налоговой инспекции" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%" Visible="False"></asp:textbox><BR>
<asp:DropDownList id=dlstIMNS runat="server" BackColor="#F6F8FC" Width="100%" CssClass="lstLineUp"></asp:DropDownList></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left>Код</TD>
          <TD class=SubTitleTextbox align=left>Р/С</TD>
          <TD class=SubTitleTextbox align=left colSpan=4>Название</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Банк:</TD>
          <TD class=SectionRow align=left>
<asp:dropdownlist id=lstBank runat="server" BackColor="#F6F8FC" Width="100px" Height="18px" AutoPostBack="True"></asp:dropdownlist></TD>
          <TD class=SectionRow align=right>
<asp:textbox id=txtBankAccount runat="server" ToolTip="Введите расчетный счет" MaxLength="13" BackColor="#F6F8FC" BorderWidth="1px" Width="99%"></asp:textbox></TD>
          <TD class=SectionRow align=right colSpan=4>
<asp:textbox id=txtBankName runat="server" ToolTip="Введите название банка" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR class=TitleTextbox>
          <TD class=SubTitleTextbox align=left>&nbsp;</TD>
          <TD class=SubTitleTextbox align=left colSpan=6>Адрес</TD>
          <TD class=SubTitleTextbox align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRow align=left>&nbsp;</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtBankAddress runat="server" ToolTip="Введите адрес банка" MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Доверенное лицо:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtProxy runat="server" ToolTip="Введите ФИО доверенного лица" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Дополнительная информация:</TD>
          <TD class=SectionRow align=left colSpan=6>
<asp:textbox id=txtInfo runat="server" ToolTip="Введите дополнительную информацию о вашей организации" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:textbox></TD>
          <TD class=SectionRow align=left colSpan=2>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Рекламные сведения о нас:</TD>
          <TD class=SectionRow align=left colSpan=3>
<asp:DropDownList id=lstAdvertising runat="server" BackColor="#F6F8FC" Width="449px" CssClass="lstLineUp"></asp:DropDownList></TD>
          <TD class=SectionRow align=left colSpan=5>&nbsp;</TD></TR>
        <TR>
          <TD class=SectionRowLabel align=left>Заметки:</TD>
          <TD class=SectionRowLabel align=left colSpan=5>
<asp:CheckBox id=chkAlert runat="server" Font-Size="8pt" Text="Беспокойный клиент"></asp:CheckBox></TD></TR></TABLE></asp:Panel>
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

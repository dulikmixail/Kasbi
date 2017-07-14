<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.NewRequest" Culture="ru-Ru"
    CodeFile="NewRequest.aspx.vb" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat ="server">
    <title>[Новый клиент/заказ]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>

</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmNewRequest" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Оформление&nbsp;заказа</td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Оформление нового заказа
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="msgAddCustomer" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="CustomerPanel" runat="server">
                        <table cellspacing="0" cellpadding="0" width="90%" border="0">
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td colspan="8">
                                    <hr>
                                </td>
                            </tr>
                            <tr class="TitleTextbox" visible ="false">
                                <td width="20%" height="5">
                                    &nbsp;</td>
                                <td width="5%">
                                    &nbsp;</td>
                                <td width="5%">
                                    &nbsp;</td>
                                <td width="5%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="30%">
                                    &nbsp;</td>
                                <td width="30%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                                <td width="10%">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SectionRowLabel">
                                    Организация:</td>
                                <td class="SectionRow">
                                    <asp:RadioButton ID="rdbtnIP" runat="server" Checked="True" GroupName="Organization"
                                        Font-Size="8pt" Text="ИП" ToolTip="Индивидуальный предприниматель"></asp:RadioButton><br>
                                    <asp:RadioButton ID="rdbtnOrganization" runat="server" GroupName="Organization" Font-Size="8pt"
                                        Text="Организация" ToolTip="Организация"></asp:RadioButton></td>
                                <td class="SectionRow">
                                    &nbsp;</td>
                                <td class="SectionRow" colspan="2">
                                    &nbsp;
                                    <asp:CheckBox ID="chkNDS" runat="server" Font-Size="8pt" Text="Плательщик НДС" ToolTip="Плательщик НДС">
                                    </asp:CheckBox></td>
                                <td class="SectionRow">
                                    &nbsp;</td>
                                <td class="SectionRow" align="left">
                                    &nbsp;
                                    <asp:CheckBox ID="chkCTO" runat="server" Font-Size="8pt" Text="Дилеры"></asp:CheckBox><br>
                                    <asp:CheckBox ID="chkSupport_old" runat="server" Checked="True" Font-Size="8pt" Text="ТО"
                                        Visible="False"></asp:CheckBox></td>
                                <td class="SectionRow" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td>
                                    &nbsp;</td>
                                <td colspan="8">
                                    <hr>
                                </td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="4">
                                    Название</td>
                                <td class="SubTitleTextbox" align="left">
                                    УНП</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRow" align="left">
                                    &nbsp;</td>
                                <td class="SectionRow" align="left">
                                    <asp:TextBox ID="txtCustomerAbr" runat="server" ToolTip="Введите аббревиатуру организации (только для организаций)"
                                        MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100px"></asp:TextBox>
                                    <asp:Label ID="btnCustomerAbr" Style="cursor: hand" runat="server" ForeColor="SteelBlue"
                                        Font-Bold="True" Font-Size="10pt">...</asp:Label></td>
                                <td class="SectionRow" align="left" colspan="4">
                                    <asp:TextBox ID="txtCustomerName" runat="server" ToolTip="Введите название организации (только для организаций)"
                                        MaxLength="90" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right">
                                    <asp:TextBox ID="txtUNN" runat="server" ToolTip="Введите УНП организации" MaxLength="9"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="5">
                                    Регистрация</td>
                                <td class="SubTitleTextbox" align="left">
                                    Код ОКЮЛП</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRow" align="left">
                                    &nbsp;</td>
                                <td class="SectionRow" align="left" colspan="5">
                                    <asp:TextBox ID="txtRegistration" runat="server" ToolTip="Введите информацию о регистрации"
                                        MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right">
                                    <asp:TextBox ID="txtOKPO" runat="server" ToolTip="Введите код ОКЮЛП" MaxLength="9"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="6">
                                    Реквизиты</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SectionRowLabel" align="left">
                                    Информация о филиалах:</td>
                                <td class="SectionRow" align="left" colspan="6">
                                    <asp:TextBox ID="txtBranch" runat="server" ToolTip="Введите информацию о филиалах и представительствах"
                                        MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="3">
                                    Фамилия</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    Имя</td>
                                <td class="SubTitleTextbox" align="left">
                                    Отчество</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SectionRowLabel" align="left">
                                    Руководитель:</td>
                                <td class="SectionRow" align="right" colspan="3">
                                    <asp:TextBox ID="txtBoosLastName" runat="server" ToolTip="Введите фамилию руководителя"
                                        MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right" colspan="2">
                                    <asp:TextBox ID="txtBoosFirstName" runat="server" ToolTip="Введите имя руководителя"
                                        MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left">
                                    <asp:TextBox ID="txtBoosPatronymicName" runat="server" ToolTip="Введите отчество руководителя"
                                        MaxLength="50" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="SubTitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="6">
                                    <asp:Label ID="lnkAccountant" Style="cursor: hand" runat="server">ФИО, телефоны</asp:Label></td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Глав.&nbsp;бух.:</td>
                                <td class="SectionRow" align="right" colspan="6">
                                    <asp:TextBox ID="txtAccountant" runat="server" ToolTip="Введите ФИО и телефоны главного бухгалтера"
                                        MaxLength="100" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="1">
                                    Индекс</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    Область</td>
                                <td class="SubTitleTextbox" align="left" colspan="3">
                                    Район</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Адрес:</td>
                                <td class="SectionRow" align="left">
                                    <asp:TextBox ID="txtZipCode" runat="server" ToolTip="Введите почтовый индекс" MaxLength="6"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    <asp:DropDownList ID="lstRegion" runat="server" BackColor="#F6F8FC" Width="99%" CssClass="lstLineUp"
                                        Height="18px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="Минская обл.">Минская</asp:ListItem>
                                        <asp:ListItem Value="Брестская обл.">Брестская</asp:ListItem>
                                        <asp:ListItem Value="Витебская обл.">Витебская</asp:ListItem>
                                        <asp:ListItem Value="Гомельская обл.">Гомельская</asp:ListItem>
                                        <asp:ListItem Value="Гродненская обл.">Гродненская</asp:ListItem>
                                        <asp:ListItem Value="Могилевская обл.">Могилевская</asp:ListItem>
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="right" colspan="3">
                                    <asp:TextBox ID="txtRegion" runat="server" ToolTip="Введите адрес" MaxLength="100"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="SubTitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="4">
                                    Город</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    Адрес</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left" style="height: 25px">
                                    &nbsp;</td>
                                <td class="SectionRow" align="left" style="height: 25px">
                                    <asp:DropDownList ID="lstCityAbr" runat="server" BackColor="#F6F8FC" Width="70%"
                                        CssClass="lstLineUp" Height="18px">
                                        <asp:ListItem Value="г.">г.</asp:ListItem>
                                        <asp:ListItem Value="г.п.">г.п.</asp:ListItem>
                                        <asp:ListItem Value="д.">д.</asp:ListItem>
                                        <asp:ListItem Value="пос.">пос.</asp:ListItem>
                                        <asp:ListItem Value=" "> </asp:ListItem>
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="left" style="height: 25px">
                                    <asp:TextBox ID="txtCity" runat="server" ToolTip="Введите название города" MaxLength="50"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="97%">Минск</asp:TextBox></td>
                                <td class="SectionRow" align="left" style="height: 25px">
                                    <asp:Label ID="btnCity" Style="cursor: hand" runat="server" ForeColor="SteelBlue"
                                        Font-Bold="True" Font-Size="10pt">...</asp:Label></td>
                                <td class="SectionRow" align="left" style="height: 25px">
                                    <asp:DropDownList ID="lstStreetAbr" runat="server" BackColor="#F6F8FC" Width="97%"
                                        CssClass="lstLineUp" Height="18px">
                                        <asp:ListItem Value="ул.">ул.</asp:ListItem>
                                        <asp:ListItem Value="пл.">пл.</asp:ListItem>                                   
                                        <asp:ListItem Value="пр.">пр.</asp:ListItem>
                                        <asp:ListItem Value="пер.">пер.</asp:ListItem>
                                        <asp:ListItem Value="б-р">б-р</asp:ListItem>
                                        <asp:ListItem Value=" "> </asp:ListItem>
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="right" colspan="2" style="height: 25px">
                                    <asp:TextBox ID="txtAddress" runat="server" ToolTip="Введите адрес" MaxLength="100"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="98%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2" style="height: 25px">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    Факс</td>
                                <td class="SubTitleTextbox" align="left">
                                    рабочий</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    домашний</td>
                                <td class="SubTitleTextbox" align="left">
                                    мобильный</td>
                                <td class="SubTitleTextbox" align="left" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SectionRow" align="left">
                                    &nbsp;</td>
                                <td class="SectionRow" align="right" colspan="2">
                                    <asp:TextBox ID="txtPhone1" runat="server" ToolTip="Введите номер факса" MaxLength="20"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right">
                                    <asp:TextBox ID="txtPhone2" runat="server" ToolTip="Введите телефон 1" MaxLength="20"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right" colspan="2">
                                    <asp:TextBox ID="txtPhone3" runat="server" ToolTip="Введите телефон 2" MaxLength="20"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="right">
                                    <asp:TextBox ID="txtPhone4" runat="server" ToolTip="Введите телефон 3" MaxLength="20"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="3">
                                    &nbsp;</td>
                            </tr>
                            <tr class="SubTitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="6">
                                    Реквизиты</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>

                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Почтовый адрес</td>
                                <td class="SectionRow" align="left" colspan="6">
                                    <asp:TextBox ID="txt_post_adress" runat="server" ToolTip="Введите почтовый адрес" MaxLength="200"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>

                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    E-mail</td>
                                <td class="SectionRow" align="left" colspan="6">
                                    <asp:TextBox ID="txtemail" runat="server" ToolTip="Введите email" MaxLength="200"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>

                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Налоговая инспекция по:</td>
                                <td class="SectionRow" valign="top" align="right" colspan="6">
                                    <asp:TextBox ID="txtTaxInspection" runat="server" ToolTip="Введите реквизиты налоговой инспекции"
                                        Visible="False" MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox><br>
                                    <asp:DropDownList ID="dlstIMNS" runat="server" BackColor="#F6F8FC" Width="100%" CssClass="lstLineUp">
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left" style="height: 12px">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" style="height: 12px">
                                    Код</td>
                                <td class="SubTitleTextbox" align="left" colspan ="2" style="height: 12px">
                                    Р/С</td>
                                <td class="SubTitleTextbox" align="left" colspan="4" style="height: 12px">
                                    Название</td>
                                <td class="SubTitleTextbox" align="left" colspan="2" style="height: 12px">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Банк:</td>
                                <td class="SectionRow" align="left">
                                    <asp:DropDownList ID="lstBank" runat="server" BackColor="#F6F8FC" Width="90%" Height="18px"
                                        AutoPostBack="True">
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    <asp:TextBox ID="txtBankAccount" runat="server" ToolTip="Введите расчетный счет"
                                        MaxLength="28" BackColor="#F6F8FC" BorderWidth="1px" Width="90%"></asp:TextBox></td>
                                <td class="SectionRow" align="right" colspan="4">
                                    <asp:TextBox ID="txtBankName" runat="server" ToolTip="Введите название банка" BackColor="#F6F8FC"
                                        BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr class="TitleTextbox">
                                <td class="SubTitleTextbox" align="left">
                                    &nbsp;</td>
                                <td class="SubTitleTextbox" align="left" colspan="6">
                                    Адрес</td>
                                <td class="SubTitleTextbox" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRow" align="left">
                                    &nbsp;</td>
                                <td class="SectionRow" align="left" colspan="6">
                                    <asp:TextBox ID="txtBankAddress" runat="server" ToolTip="Введите адрес банка" MaxLength="100"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Дополнительная информация:</td>
                                <td class="SectionRow" align="left" colspan="6">
                                    <asp:TextBox ID="txtInfo" runat="server" ToolTip="Введите дополнительную информацию о вашей организации"
                                        MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="100%"></asp:TextBox></td>
                                <td class="SectionRow" align="left" colspan="2">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" align="left">
                                    Рекламные сведения о нас:</td>
                                <td class="SectionRow" align="left" colspan="3">
                                    <asp:DropDownList ID="lstAdvertising" runat="server" BackColor="#F6F8FC" Width="500px"
                                        CssClass="lstLineUp">
                                    </asp:DropDownList></td>
                                <td class="SectionRow" align="left" colspan="5">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td class="SectionRowLabel" colspan="3">
                                Кто привёл клиента: &nbsp;&nbsp;&nbsp;
                                <asp:DropDownList ID="lstManager" runat="server" BackColor="#F6F8FC" CssClass="ShiftRight3"></asp:DropDownList>
                                <asp:Label ID="man_name" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                    <asp:ListBox ID="lstCity" runat="server" BackColor="#F6F8FC" BorderWidth="1px" Width="129px"
                        CssClass="hiddencitylist1" Rows="10"></asp:ListBox>
                    <asp:ListBox ID="lstCustomerAbr" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                        Width="100px" CssClass="hiddencitylist2" Rows="10"></asp:ListBox>
                </td>
            </tr>
            <tr>
                <td height="20">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Добавление товаров
                </td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="msgGoods" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
        </table>
        <table width="100%">
            <tr  >
                <td colspan="4">
                     <asp:DropDownList ID="lstType" runat="server" EnableViewState="true" Visible="False"
                        BackColor="#F6F8FC" Width="440px" Height="31px" AutoPostBack="True">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td valign="center" style="font-size:12px">
                    
                    <div style="background-color:Silver; padding:7px">
                    <b>Введите артикул:</b> <asp:TextBox ID="txtArtikul" runat="server" ToolTip="Введите артикул" MaxLength="50"
                                        BackColor="#F6F8FC" BorderWidth="1px" Width="150"></asp:TextBox> 
                    
                    &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btnArtikul" runat="server" CssClass="PanelHider" EnableViewState="False"><b>Искать</b></asp:LinkButton>               
                    <asp:Label ID="lbl_artikul_error" runat="server" ForeColor="Red"></asp:Label>
                    </div>
                
                    <asp:Panel ID="pnlTreeView" runat="server" Height="400px" ScrollBars="Auto" Width="400px"
                        BorderColor="Black" BorderStyle="Solid" BorderWidth="1">
                        <asp:TreeView ID="TreeGroup" runat="server"
                            NodeWrap="True" 
                            CollapseImageToolTip="Свернуть '{0}'" ExpandImageToolTip="Развернуть '{0}'" ImageSet="arrows"  >
                            <SelectedNodeStyle  ForeColor="#5555DD" />
                            <NodeStyle CssClass="itemGrid"  />
                            <RootNodeStyle CssClass="" Font-Bold =True />
                            <ParentNodeStyle  CssClass="TreeView"  ForeColor ="#9c0001" />
                        </asp:TreeView>
                    </asp:Panel>
                </td>
                <td width="100%">
                    <table cellspacing="0" cellpadding="0" width="82%" border="0">
                        <tr>
                            <td colspan="4" style="font-size:12px; ">
                                <div style="background-color:Silver; padding:7px">
                                <b>Быстрое добавление ККМ:</b> <asp:TextBox ID="txt_fast_add" runat="server" ToolTip="Введите артикул" MaxLength="50"
                                                    BackColor="#F6F8FC" BorderWidth="1px" Width="150"></asp:TextBox> 
                                
                                &nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton ID="btn_fast_add" runat="server" CssClass="PanelHider" EnableViewState="False"><b>Добавить</b></asp:LinkButton>               
                                <asp:Label ID="lbl_fast_add_error" runat="server" ForeColor="Red"></asp:Label>
                                </div>                        
                            </td>
                        </tr>
                        <tr class="TitleTextbox">
                            <td class="SubTitleTextbox" align="left" colspan="4">
                                Прейскурант</td>
                        </tr>
                        <tr>
                            <td class="SectionRowLabel" colspan="4">
                                <asp:DropDownList ID="lstPriceList" runat="server" EnableViewState="true" BackColor="#F6F8FC"
                                    Width="100%" Height="31px" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                        <tr class="TitleTextbox">
                            <td class="SubTitleTextbox" align="left">
                                Дата сделки</td>
                            <td class="SubTitleTextbox" align="left">
                                Цена (без НДС)</td>
                            <td class="SubTitleTextbox" align="left">
                                <asp:Label ID="lblQuantity" runat="server">Кол-во:</asp:Label></td>
                            <td class="SubTitleTextbox" align="left">
                                №&nbsp;договора</td>
                        </tr>
                        <tr>
                            <td class="SectionRowLabel">
                                <asp:TextBox ID="calendar" runat="server" BorderWidth="1px" Width="82px"></asp:TextBox><a
                                    href="javascript:showdatepicker('calendar', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                        src="Images/cal_date_picker.gif" border="0"></a>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage"
                                    ErrorMessage="Дата сделки" ControlToValidate="calendar">*</asp:RequiredFieldValidator>&nbsp;
                                <asp:Label ID="lblDateFormat2" runat="server" CssClass="text02"></asp:Label>
                                <asp:CompareValidator ID="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="calendar"
                                    EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение даты сделки</asp:CompareValidator></td>
                            <td class="SectionRowLabel" align="left">
                                &nbsp;
                                <asp:TextBox ID="txtPrice" runat="server" MaxLength="9" BackColor="#F6F8FC" BorderWidth="1px"
                                    Width="100px"></asp:TextBox></td>
                            <td class="SectionRowLabel" align="left">
                                <asp:TextBox ID="txtQuantity" runat="server" MaxLength="5" BackColor="#F6F8FC" BorderWidth="1px"
                                    Width="45px"></asp:TextBox></td>
                            <td class="SectionRowLabel" align="left">
                                <asp:TextBox ID="txtDogovor" runat="server" MaxLength="10" BackColor="#F6F8FC" BorderWidth="1px"
                                    Width="60px" ToolTip="Введите номер договора и поддоговора через слэш (/)"></asp:TextBox></td>
                        </tr>
                        <tr class="TitleTextbox">
                            <td class="SubTitleTextbox" align="left" colspan="4">
                                Описание товара</td>
                        </tr>
                        <tr>
                            <td class="SectionRowLabel" colspan="4">
                                <asp:ListBox ID="lstDescription" runat="server" BackColor="#F6F8FC" BorderWidth="1" Font-Size=9
                                    Width="100%" Height="300px" SelectionMode="Multiple"></asp:ListBox></td>
                        </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:CheckBox ID="chkAddToSale" runat="server" Font-Size="8pt" Text="Добавить товары в последний заказ"
                                    Visible="False"></asp:CheckBox></td>
                            <td align="right" colspan="3">
                                <asp:LinkButton ID="btnAddGood" runat="server" ForeColor="#0066CC" CssClass="TopLink">Добавить&nbsp;товар</asp:LinkButton></td>
                        </tr>
                    </table>
                    <asp:TextBox ID="txtDate" runat="server" Visible="False"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Repeater ID="repSalesGoods" runat="server">
                        <HeaderTemplate>
                            <table border="0" width="90%" class="borders" bgcolor="#f6f8fc">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td colspan="7" style="font-weight: bold" class="DetailField">
                                    <%# DataBinder.Eval(Container.DataItem, "good_description")%>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" class="DetailField">
                                    Цена :</td>
                                <td align="center">
                                    <asp:TextBox ID="tbxPrice" BorderWidth="1px" BackColor="#F6F8FC" runat="server" Width="100"
                                        Text='<%# DataBinder.Eval(Container.DataItem, "price")%>'>
                                    </asp:TextBox></td>
                                <td align="right" class="DetailField">
                                    Количество :</td>
                                <td>
                                    <asp:TextBox ID="tbxQuantity" BorderWidth="1px" BackColor="#F6F8FC" runat="server"
                                        Width="50" Text='<%# DataBinder.Eval(Container.DataItem, "quantity")%>' ReadOnly='<%# DataBinder.Eval(Container.DataItem, "is_cashregister")%>'>
                                    </asp:TextBox></td>
                                <td align="right" class="DetailField">
                                    Стоимость :</td>
                                <td align="right" class="TitleTextbox">
                                    <asp:Label runat="server" ID="Label1" Text='<%# DataBinder.Eval(Container.DataItem, "cost")%>'>
                                    </asp:Label></td>
                                <td align="center">
                                    <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Images/delete.gif" CommandName="Delete"
                                        CommandArgument='<%# DataBinder.Eval(Container.DataItem, "good_sys_id")%>'></asp:ImageButton></td>
                            </tr>
                            <tr runat="server" id="trSetPlace">
                                <td colspan="7">
                                    <table style="font-size: 9pt" border="0">
                                        <tr>
                                            <td class="TitleTextbox" rowspan="2">
                                                Адрес установки кассового аппарата</td>
                                            <td colspan="2">
                                                <asp:DropDownList ID="ddlCity" Width="452px" runat="server">
                                                </asp:DropDownList></td>
                                        </tr>
                                        <tr>
                                            <td colspan="3">
                                                <asp:TextBox ID="txtPlace" Width="447px" BorderWidth="1px" BackColor="#F6F8FC" runat="server"
                                                    ToolTip="Введите адрес установки кассового аппарата, выставочно-торговый комплекс, стенд(ряд), место"
                                                    MaxLength="250" CssClass="ShiftRight3" Text='<%# DataBinder.Eval(Container.DataItem, "set_place")%>'>
                                                </asp:TextBox></td>
                                        </tr>
                                        <tr>
                                            <td rowspan="2" class="TitleTextbox">
                                                ФИО кассиров:</td>
                                            <td align="center" class="TitleTextbox">
                                                Кассир 1</td>
                                            <td align="center" class="TitleTextbox">
                                                Кассир 2</td>
                                        </tr>
                                        
                                        </asp:ImageMap>
                                        <tr>
                                            <td>
                                                <asp:TextBox ID="txtKasir1" Width="216px" BorderWidth="1px" BackColor="#F6F8FC" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "kassir1")%>' ToolTip="Введите ФИО первого кассира"
                                                    MaxLength="100" CssClass="ShiftRight3">
                                                </asp:TextBox></td>
                                            <td>
                                                <asp:TextBox ID="txtKasir2" Width="224px" BorderWidth="1px" BackColor="#F6F8FC" runat="server"
                                                    Text='<%# DataBinder.Eval(Container.DataItem, "kassir2")%>' ToolTip="Введите ФИО второго кассира"
                                                    MaxLength="100" CssClass="ShiftRight3">
                                                </asp:TextBox></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <hr size="1">
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr>
                                <td colspan="5" align="right" style="font-weight: bold" class="DetailField">
                                    Итого :</td>
                                <td align="right" style="font-weight: bold" class="DetailField">
                                    <asp:Label ID="lblTotalCost" runat="server" Text="123"></asp:Label></td>
                                <td align="center">
                                    <asp:LinkButton ID="lbtnRefresh" runat="server" ForeColor="#0066CC" CssClass="TopLink"
                                        CommandName="Refresh">Пересчитать</asp:LinkButton></td>
                            </tr>
                            <tr>
                                <td colspan="7">
                                    <hr size="1">
                                </td>
                            </tr>
                            </TABLE>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="90%">
            <tr>
                <td colspan="3">
                </td>
            </tr>
            <tr>
                <td class="SectionRowLabel" align="left" width="20%">
                    Доверенное лицо:</td>
                <td class="SectionRow" colspan="2">
                    <asp:TextBox ID="txtProxy" runat="server" ToolTip="Введите ФИО доверенного лица"
                        MaxLength="250" BackColor="#F6F8FC" BorderWidth="1px" Width="452px" CssClass="ShiftRight3"></asp:TextBox></td>
            </tr>
            <tr>
                <td colspan="4" height="20">
                </td>
            </tr>
            <tr>
                <td class="SectionRowLabel">
                    Ответственный исполнитель:</td>
                <td class="SectionRow">
                    <asp:DropDownList ID="lstSaler" runat="server" BackColor="#F6F8FC"
                        CssClass="ShiftRight3">
                    </asp:DropDownList>
                
                </td>
                <td valign="top" class="SectionRowLabel">
                
                </td>
            </tr>
            <tr>
                <td colspan="4" style="height: 26px">
                </td>
            </tr>
            <tr>
                <td class="SectionRowLabel">
                    Фирма продавец:</td>
                <td class="SectionRow" colspan="2">
                    <asp:DropDownList ID="lstFirm" runat="server" BackColor="#F6F8FC" CssClass="ShiftRight3">
                    </asp:DropDownList></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="90%">
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr class="TitleTextbox">
                <td class="SubTitleTextbox" align="left" width="20%">
                    &nbsp;</td>
                <td class="SubTitleTextbox" colspan="2">
                    Тип оплаты</td>
            </tr>
            <tr>
                <td class="SectionRowLabel">
                    Сведения&nbsp;об оплате:</td>
                <td class="SectionRow" width="20%">
                    <asp:CheckBox ID="chkPayed" runat="server" EnableViewState="False" Checked="True"
                        Text="Оплачено" CssClass="text02"></asp:CheckBox></td>
                <td style="border-right: black 1px ridge; border-top: black 1px ridge; font-size: 10pt;
                    border-left: black 1px ridge; border-bottom: black 1px ridge; background-color: #f6f8fc"
                    align="center">
                    <asp:Label ID="radiobuttons" runat="server">
                        <asp:RadioButton ID="optBeznal" runat="server" Text="Без/нал" GroupName="radioPayed">
                        </asp:RadioButton>&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optNal" runat="server" Text="Наличные" GroupName="radioPayed"></asp:RadioButton>&nbsp;&nbsp;
                        <asp:RadioButton ID="optSberkassa" runat="server" Text="Сберкасса" GroupName="radioPayed"
                            Checked="True"></asp:RadioButton></asp:Label></td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="90%">
            <tr>
                <td colspan="3" height="10">
                </td>
            </tr>
            <tr class="TitleTextbox">
                <td class="SubTitleTextbox" align="left" width="20%">
                    &nbsp;</td>
                <td class="SubTitleTextbox" colspan="2">
                    Оплата за период</td>
            </tr>
            <tr>
                <td class="SectionRowLabel">
                    ТО:</td>
                <td class="SectionRow" width="20%">
                    <asp:CheckBox ID="chkSupport" runat="server" EnableViewState="False" Checked="True"
                        Text="ТО оплачено" CssClass="text02"></asp:CheckBox></td>
                <td style="border-right: black 1px ridge; border-top: black 1px ridge; font-size: 10pt;
                    border-left: black 1px ridge; border-bottom: black 1px ridge; background-color: #f6f8fc"
                    align="center">
                    <asp:Label ID="radiobuttons1" runat="server">
                        <asp:RadioButton ID="optNone" runat="server" Text="нет" GroupName="radioTO"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                        <asp:RadioButton ID="optOneMonth" runat="server" Text="1 мес" GroupName="radioTO"></asp:RadioButton>&nbsp;&nbsp;
                        <asp:RadioButton ID="optThreeMonth" runat="server" Text="3 мес" GroupName="radioTO"
                            Checked="True"></asp:RadioButton>&nbsp;&nbsp;
                        <asp:RadioButton ID="optSixMonth" runat="server" Text="6 мес" GroupName="radioTO"
                            Checked="True"></asp:RadioButton></asp:Label></td>
            </tr>
        </table>
        <br />
        <table width="80%">
            <tr class="SectionRowLabel">
                <td align="center"><b>Услуги на ККМ</b></td>
                <td>
                        <asp:CheckBox ID="chkDocPack" runat="server" EnableViewState="False" Checked="True"
                        Text="Услуга - формарование пакета документов" CssClass="text02"></asp:CheckBox>
                        <br />
                        <asp:CheckBox ID="chkTO2" runat="server" EnableViewState="False" Checked="True"
                        Text="Услуга - ТО на 2 месяца" CssClass="text02"></asp:CheckBox>
                </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td height="10">
                </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" align="center">
                    <asp:ImageButton ID="btnClear" runat="server" ImageUrl="Images/clear.gif"></asp:ImageButton>&nbsp;&nbsp;&nbsp;&nbsp;
                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton></td>
            </tr>
            <tr>
                <td height="10">
                </td>
            </tr>
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>

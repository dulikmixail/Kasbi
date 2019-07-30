<%@ Reference Page="~/Documents.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.NewSupportConduct"
Culture="ru-Ru" CodeFile="NewSupportConduct.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>[Проведение ТО]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js">
    </script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet"/>

</head>
<body onscroll="javascript:document.all['scrollPos'].value = document.body.scrollTop;"
      bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop = document.all['scrollPos'].value;"
      rightmargin="0">
<form id="frmNewRequest" method="post" runat="server">
<uc1:Header ID="Header1" runat="server"></uc1:Header>
<table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
    <tr>
        <td class="HeaderTitle" width="100%">
            0
            &nbsp;Карточка ККМ -&gt; Постановка&nbsp;на&nbsp;ТО&nbsp;/&nbsp;Снятие&nbsp;с&nbsp;ТО&nbsp;/&nbsp;Приостановка&nbsp;ТО
        </td>
    </tr>
</table>
<table cellspacing="1" cellpadding="2" width="100%">
<tr class="Unit">
    <td class="Unit">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Кассовый аппарат
    </td>
    <td class="Unit" align="right">
        <asp:LinkButton ID="lnkGoods" runat="server" CssClass="PanelHider" EnableViewState="False">
            Товары
        </asp:LinkButton>
    </td>
</tr>
<tr>
    <td colspan="2">
        &nbsp;
        <asp:Label ID="msg" runat="server" EnableViewState="False" Font-Size="8pt" ForeColor="Red"
                   Font-Bold="True">
        </asp:Label>
    </td>
</tr>
<tr>
    <td align="center" colspan="2">
        <table style="font-size: 9pt; font-family: Verdana; position: relative; top: -10px"
               cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td width="40">
                </td>
                <td width="100">
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                    <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                   ToolTip="На техобслуживании">
                    </asp:HyperLink>
                    <asp:ImageButton ID="imgRepair" runat="server" ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                    </asp:ImageButton>
                </td>
                <td align="right">
                    <asp:Label ID="lblCashType" runat="server" Font-Bold="true"></asp:Label>
                    <asp:Label ID="lblSoftwareVersion" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:HyperLink ID="lblCash" runat="server" Font-Bold="true" ToolTip="Карточка ККМ"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cash">
                    <asp:Label ID="lblCaptionGarantia" runat="server">Гарантийный срок :</asp:Label>
                </td>
                <td class="cashDetail">
                    <asp:Label ID="lblGarantia" runat="server" ForeColor="Red"></asp:Label>
                    <asp:Label ID="lblRemovedFromWarranty" runat="server" ForeColor="Red"></asp:Label>
                </td>
                
            </tr>
            <tr>
                <td colspan="2" class="cash">
                    <asp:Label ID="lblCaptionNumbers" runat="server"> СК Реестра/ПЗУ/МФП:</asp:Label>
                </td>
                <td class="cashDetail">
                    <asp:Label ID="lblNumbers" runat="server"></asp:Label>
                </td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionMarka" runat="server" CssClass="cash">Марка ЦТО:</asp:Label>
                </td>
                <td >
                    <asp:Label ID="lblMarka" runat="server" CssClass="cashDetail"></asp:Label>
                </td>
            </tr>
            <tr height="0">
                <td colspan="2" class="cash">
                    <asp:Label ID="lblCaptionDateCreated" runat="server" CssClass="cash">Добавлен в базу:</asp:Label>
                </td>
                <td >
                    <asp:Label ID="lblDateCreated" runat="server" CssClass="cashDetail"></asp:Label>
                </td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionWorker" runat="server" CssClass="cash">Добавил:</asp:Label>
                </td>
                <td >
                    <asp:Label ID="lblWorker" runat="server" CssClass="cashDetail"></asp:Label>
                </td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionSetPlace" runat="server" CssClass="cash">Место установки:</asp:Label>
                </td>
                <td >
                    <asp:Label ID="lblSetPlace" runat="server" CssClass="cashDetail"></asp:Label>
                </td>

            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionSale" runat="server" CssClass="cash">Продан:</asp:Label>
                </td>
                <td >
                    <asp:HyperLink ID="lblSale" runat="server" CssClass="cashDetail"></asp:HyperLink>
                </td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblOwner" runat="server" CssClass="cash">Плательщик:</asp:Label>
                </td>
                <td >
                    <asp:HyperLink ID="lnkOwner" runat="server" CssClass="cashDetail"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblCaptionSupport" runat="server" CssClass="cash">ТО:</asp:Label>
                </td>
                <td align="left">
                    <asp:Label ID="lblSupport" runat="server" CssClass="cashDetail"></asp:Label>
                    <asp:Label ID="lblSupportSKNO" runat="server" CssClass="cashDetail"></asp:Label>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr class="Unit">
    <td class="Unit" colspan="2">
        <asp:Label ID="SectionTOName" runat="server">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Провеение
            технического обслуживания
        </asp:Label>
    </td>
</tr>

<tr>
<td>
<table id="TABLE1" width="70%" runat="server">
<tr>
    <td colspan="3">
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRowLabel" align="left">
        Тип товара:
    </td>
    <td class="SectionRow" align="left" colspan="2">
        <asp:DropDownList ID="lstGoodType" runat="server" BackColor="#F6F8FC" Width="150px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="SectionRowLabel" align="left">
        <asp:Label ID="lblGoodNumCashregister" runat="server">Заводской номер</asp:Label>&nbsp;
    </td>
    <td class="SectionRow" colspan="2">
        <asp:TextBox ID="txtGoodNumCashregister" runat="server" ToolTip="Введите заводской номер"
                     BackColor="#F6F8FC" Width="150px" MaxLength="8" BorderWidth="1px" Enabled="False">
        </asp:TextBox>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRowLabel" align="left">
        Исполнитель:
    </td>
    <td class="SectionRow" align="left" colspan="2">
        <asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="304px">
        </asp:DropDownList>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRowLabel" align="left">
        Дополнительная информация:
    </td>
    <td class="SectionRow" colspan="2">
        <asp:TextBox ID="txtGoodInfo" runat="server" ToolTip="Введите место установки" BackColor="#F6F8FC"
                     Width="100%" MaxLength="250" BorderWidth="1px">
        </asp:TextBox>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRowLabel" align="left">
        Место установки:
    </td>
    <td class="SectionRow" colspan="2">
        <asp:TextBox ID="txtPlace" runat="server" ToolTip="Введите место установки" BackColor="#F6F8FC"
                     Width="100%" MaxLength="250" BorderWidth="1px">
        </asp:TextBox>
        <br/><a style="font-size: 12px" href="https://ato.by/address" target="_blank">Узнать район по адресу можно здесь: https://ato.by/address </a>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRowLabel" align="left">
        Район установки:
    </td>
    <td class="SectionRow" colspan="2">
        <asp:DropDownList ID="lstPlaceRegion" runat="server" BackColor="#F6F8FC" Width="304px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td colspan="3">
        <table id="pnlTOType" width="100%" runat="server">
            <tr class="SubTitleTextbox">
                <td colspan="3">
                    Вид ТО
                    <hr>
                </td>
            </tr>
            <tr class="SectionRow">
                <td class="SectionRow" colspan="3">
                    <asp:RadioButtonList ID="rbTO" runat="server" CssClass="text02" Height="16px" Width="456px"
                                         RepeatColumns="3" AutoPostBack="True">
                        <asp:ListItem Value="0">Постановка на ТО</asp:ListItem>
                        <asp:ListItem Value="1">Снятие с ТО</asp:ListItem>
                        <asp:ListItem Value="2" Selected="True">Проведение ТО</asp:ListItem>
                    </asp:RadioButtonList>

                    <asp:CheckBox ID="chkDelayTO" runat="server" CssClass="text02" AutoPostBack="True"
                                  Text="Приостановка ТО">
                    </asp:CheckBox>
                    <asp:CheckBox ID="autoAktAboutRevomeFromTO" runat="server" CssClass="text02" AutoPostBack="True" Checked="True"
                                  Text="С формированием ремонтного Акта о снятии с ТО">
                    </asp:CheckBox>
                    <hr>
                </td>
            </tr>

            <%--<tr class="SubTitleTextbox" >
                                        <td colspan="3" >
                                            <br />
                                            СКНО
                                            <hr>
                                        </td>
                                    </tr>
                                    <tr class="SectionRow">
                                        <td class="SectionRow" colspan="3">
                                            <asp:ImageButton ID="btnSaveSKNOInfo" runat="server" ImageUrl="Images/update.gif" ImageAlign="Right"></asp:ImageButton>
                                            <asp:RadioButtonList ID="rbSKNO" runat="server" CssClass="text02" Height="16px" Width="385px"
                                                RepeatColumns="3" AutoPostBack="True">
                                                <asp:ListItem Value="1">установлено</asp:ListItem>
                                                <asp:ListItem Value="0" Selected="True">Не установлено</asp:ListItem>
                                            </asp:RadioButtonList>
                                            <asp:Label ID="lblSKNOExecutor" runat="server" CssClass="text02">Изменил: </asp:Label><asp:Label ID="lblSKNOExecutorInfo" runat="server" CssClass="text02"></asp:Label>
                                            
                                            <hr>
                                        </td>
                                    </tr>--%>

        </table>
    </td>
</tr>
<tr>
<td colspan="3">
<table id="pnlConduct" width="100%" runat="server">
    <tr class="SubTitleTextbox">
        <td>
            &nbsp;
        </td>
        <td align="left">
            <asp:Label ID="Label9" runat="server">до проведения</asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="Label10" runat="server">после проведения</asp:Label>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ЦТО:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CTO_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CTO_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCond_CTO2">
        <td class="SectionRowLabel" align="left">
            СК ЦТО 2:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CTO2_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CTO2_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК Реестра:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_Reestr_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_Reestr_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ПЗУ:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_PZU_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_PZU_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК МФП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_MFP_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_MFP_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCond_CP">
        <td class="SectionRowLabel" align="left">
            СК ЦП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CP_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cond_CP_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="SubTitleTextbox">
        <td>
            &nbsp;
        </td>
        <td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp; &nbsp;
            <asp:Label ID="Label2" runat="server">Месяц</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label1" runat="server">Год</asp:Label>
        </td>
        <td align="left">
            &nbsp;Дата выполнения
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Закрыть период:
        </td>
        <td class="SectionRow" align="left">
            <asp:DropDownList ID="lstMonth" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
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
            </asp:DropDownList>
            <asp:DropDownList ID="lstYear" runat="server" BackColor="#F6F8FC"
                              BorderWidth="1px">
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
                <asp:ListItem Value="2017">2017</asp:ListItem>
                <asp:ListItem Value="2018">2018</asp:ListItem>
                <asp:ListItem Value="2019">2019</asp:ListItem>
                <asp:ListItem Value="2020">2020</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="SectionRow">
            <asp:Panel ID="pnlDataPickerCloseDate" runat="server">
                <asp:TextBox ID="txtCloseDate" runat="server" Width="95px" BorderWidth="1px" style="height: 20px"></asp:TextBox>
                <%--<a href="javascript:showdatepicker('txtCloseDate', 0, false,'DD.MM.YYYY')">
                                                    <img alt="Date Picker" src="Images/cal_date_picker.gif" border="0" name="imgDpCloseDate"></a>--%>
            </asp:Panel>
        </td>
    </tr>
</table>
<table id="pnlDelay" width="100%" runat="server">
    <tr class="SubTitleTextbox">
        <td>
        </td>
        <td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label3" runat="server">Месяц</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <asp:Label ID="Label4" runat="server">Год</asp:Label>
        </td>
        <td align="left">
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="Label7" runat="server">Количество месяцев</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Приостановить:
        </td>
        <td class="SectionRow" style="width: 189px" align="left">
            &nbsp;c &nbsp;
            <asp:DropDownList ID="lstMonthDelayIn" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
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
            </asp:DropDownList>
            <asp:DropDownList ID="lstYearDelayIn" runat="server" BackColor="#F6F8FC"
                              BorderWidth="1px">
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
                <asp:ListItem Value="2017">2017</asp:ListItem>
                <asp:ListItem Value="2018">2018</asp:ListItem>
                <asp:ListItem Value="2019">2019</asp:ListItem>
                <asp:ListItem Value="2020">2020</asp:ListItem>
            </asp:DropDownList>
        </td>
        <td class="SectionRow" align="left">
            &nbsp;
            <asp:DropDownList ID="lstMonthQty" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                <asp:ListItem Value="1">1</asp:ListItem>
                <asp:ListItem Value="2">2</asp:ListItem>
                <asp:ListItem Value="3">3</asp:ListItem>
                <asp:ListItem Value="4">4</asp:ListItem>
                <asp:ListItem Value="5">5</asp:ListItem>
                <asp:ListItem Value="6">6</asp:ListItem>
                <asp:ListItem Value="7">7</asp:ListItem>
                <asp:ListItem Value="8">8</asp:ListItem>
                <asp:ListItem Value="9">9</asp:ListItem>
                <asp:ListItem Value="10">10</asp:ListItem>
                <asp:ListItem Value="11">11</asp:ListItem>
                <asp:ListItem Value="12">12</asp:ListItem>
            </asp:DropDownList>
        </td>
    </tr>
</table>
<table id="pnlDismissal" width="100%" runat="server">
    <tr class="SubTitleTextbox">
        <td>
        </td>
        <td align="left">
            <asp:Label ID="Label14" runat="server">до снятия</asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="Label15" runat="server">после снятия</asp:Label>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ЦТО:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCTO_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCTO_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCTO2" runat="server">
        <td class="SectionRowLabel" align="left">
            СК ЦТО 2:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCTO2_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCTO2_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК Реестра:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaReestr_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaReestr_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ПЗУ:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaPZU_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaPZU_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК МФП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaMFP_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaMFP_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCP" runat="server">
        <td class="SectionRowLabel" align="left">
            СК ЦП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCP_in" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarkaCP_out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Z-отчет:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtZReportIn" runat="server" BackColor="#F6F8FC" Width="160px" MaxLength="11"
                         BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtZReportOut" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Итог:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtItogIn" runat="server" BackColor="#F6F8FC" Width="160px" MaxLength="12"
                         BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtItogOut" runat="server" BackColor="#F6F8FC" Width="160px" MaxLength="12"
                         BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Дата снятия:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="tbxDismissalDate" runat="server" BorderWidth="1px"></asp:TextBox>
            <%--<a href="javascript:showdatepicker('tbxDismissalDate', 0, false,'DD.MM.YYYY')">
                                                <img alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>--%>
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator2" runat="server" CssClass="ErrorMessage"
                                        ErrorMessage="Дата снятия с ТО" ControlToValidate="tbxDismissalDate">
                *
            </asp:RequiredFieldValidator>&nbsp;
        </td>
        <td class="SectionRow" align="left">
            <asp:CheckBox ID="chkDismissalIMNS" runat="server" CssClass="text02" Text="Снять с учета в ИМНС">
            </asp:CheckBox>
        </td>
    </tr>
    <tr class="SubTitleTextbox">
        <td>

        </td>
        <td align="left" colspan="2">
            <asp:CompareValidator ID="Comparevalidator1" runat="server" CssClass="ErrorMessage"
                                  ControlToValidate="tbxDismissalDate" EnableClientScript="False" Display="Dynamic"
                                  Type="Date" Operator="DataTypeCheck">
                Пожалуйста, введите корректныое значение даты снятия с ТО
            </asp:CompareValidator>
        </td>
    </tr>
</table>
<table id="pnlSupport" width="100%" runat="server">
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Плательщик:
        </td>
        <td class="SectionRow" align="left" colspan="2">
            <asp:TextBox ID="txtCustomerFind" runat="server" BackColor="#F6F8FC" Width="90%"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
            <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">Найти</asp:LinkButton>
        </td>
    </tr>
    <tr class="SubTitleTextbox">
        <td class="SectionRowLabel" align="left">
            &nbsp;
        </td>
        <td class="SectionRow" align="left" colspan="2">
            <asp:ListBox ID="lstCustomers" runat="server" Width="100%" AutoPostBack="True"></asp:ListBox>
        </td>
    </tr>
    <tr>
        <td class="SectionRow">
            &nbsp;
        </td>
        <td class="SectionRow" colspan="2">
            &nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label>
        </td>
    </tr>
    <tr class="SubTitleTextbox">
        <td class="SectionRowLabel" align="left">
        </td>
        <td align="left">
            <asp:Label ID="Label13" runat="server">до постановки</asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="Label16" runat="server">после постановки</asp:Label>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ЦТО:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cto_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cto_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCto2_Sup" runat="server">
        <td class="SectionRowLabel" align="left">
            СК ЦТО 2:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cto2_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Cto2_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК Реестра:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Reestr_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_Reestr_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК ПЗУ:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_PZU_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_PZU_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            СК МФП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_MFP_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_MFP_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox" id="trMarkaCP_Sup" runat="server">
        <td class="SectionRowLabel" align="left">
            СК ЦП:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_CP_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtMarka_CP_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Z-отчет:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtZReport_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtZReport_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Итог:
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtItog_Sup_In" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
        <td class="SectionRow" align="left">
            <asp:TextBox ID="txtItog_Sup_Out" runat="server" BackColor="#F6F8FC" Width="160px"
                         MaxLength="11" BorderWidth="1px">
            </asp:TextBox>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            Дата постановки:
        </td>
        <td class="SectionRow" align="left" colspan="2">
            <asp:TextBox ID="tbxSupportDate" runat="server" BorderWidth="1px"></asp:TextBox>
            <%--<a href="javascript:showdatepicker('tbxSupportDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                                    src="Images/cal_date_picker.gif" border="0"></a>--%>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage"
                                        ErrorMessage="Дата постановки" ControlToValidate="tbxSupportDate">
                *
            </asp:RequiredFieldValidator>&nbsp;
            <asp:CompareValidator ID="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxSupportDate"
                                  EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">
                Пожалуйста, введите корректные значение даты постановки
            </asp:CompareValidator>
        </td>
    </tr>
    <tr class="TitleTextbox">
        <td class="SectionRowLabel" align="left">
            <asp:Label ID="lblSaleInfo" runat="server" CssClass="text02">Тип документа :</asp:Label>
        </td>
        <td class="SectionRow" colspan="2" align="left">
            <asp:DropDownList ID="cmbSalesInfo" runat="server" Width="305px" AutoPostBack="true">
            </asp:DropDownList>
            <asp:Label ID="lblDogovor" runat="server" CssClass="text02">Договор:&nbsp;</asp:Label>
            <asp:TextBox
                ID="txtDogovor" runat="server" Width="90px" BackColor="#F6F8FC" MaxLength="10"
                ToolTip="Введите номер договора и поддоговора через слэш (/)" BorderWidth="1px" ReadOnly="True">
            </asp:TextBox>
        </td>
    </tr>
    <tr>
    </tr>
</table>
</td>
</tr>
<tr>
    <td class="SectionRow" align="center" colspan="3">
        <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton>
    </td>
</tr>

</table>
</td>
</tr>
<tr>
    <td>
        <asp:Label ID="msgAddSupportConduct" runat="server" EnableViewState="false" ForeColor="#ff0000"
                   Font-Bold="true">
        </asp:Label>
    </td>
</tr>
</table>
<table width="100%">
<asp:Panel ID="pnlSupportConductHistory" runat="server">
<tr class="Unit">
    <td class="Unit" colspan="4">
        &nbsp;&nbsp;
        <asp:ImageButton ID="btnExpand1" runat="server" ImageUrl="Images/expanded.gif" ToolTip="Скрыть">
        </asp:ImageButton>&nbsp;История&nbsp;проведения&nbsp;технического&nbsp;обслуживания&nbsp;кассового&nbsp;аппарата
    </td>
</tr>
<asp:Panel ID="pnlSupportConductHistory_body" runat="server">
<tr>
    <td colspan="3">
        <asp:Label ID="msgSupportConductHistory" runat="server" EnableViewState="False" Font-Bold="True"
                   ForeColor="Red" Font-Size="8pt">
        </asp:Label>
    </td>
</tr>
<tr>
<td align="center" colspan="3">
<asp:DataGrid ID="grdSupportConductHistory" runat="server" Font-Size="9pt" Width="100%"
              BackColor="White" BorderWidth="1px" BorderColor="#CC9966" AutoGenerateColumns="False"
              BorderStyle="None" CellPadding="4">
<ItemStyle CssClass="itemGrid"></ItemStyle>
<HeaderStyle CssClass="headerGrid"></HeaderStyle>
<FooterStyle CssClass="footerGrid"></FooterStyle>
<Columns>
<asp:TemplateColumn HeaderText="Период ">
    <ItemTemplate>
        <asp:Label ID="lblPeriod" runat="server" Font-Size="9pt"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:Label ID="lbledtPeriod" runat="server"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Плательщик">
    <ItemTemplate>
        <asp:HyperLink ID="lnkPayer" runat="server"></asp:HyperLink>&nbsp;
    </ItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="executor" HeaderText="Исполнитель">
    <ItemTemplate>
        <asp:Label ID="lblExecutorTO" runat="server"></asp:Label>&nbsp;<br>
        <br>
        <asp:Label ID="lblUpdateRec" CssClass="TitleTextbox" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        Исполнитель:
        <asp:DropDownList ID="lstExecutor" runat="server" Height="22px" BackColor="#F6F8FC"
                          Width="189px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="ТО">
    <ItemTemplate>
        <asp:Label ID="lblStatus" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <table id="Table7" cellspacing="1" cellpadding="1" width="100%" border="0">
            <tr>
                <td colspan="3" class="SubCaption" align="left">
                    <asp:Label ID="lbledtStatus" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td class="SubCaption" align="right">
                    <asp:Label ID="lbltxtDate" runat="server" Visible="False">c&nbsp;</asp:Label>
                </td>
                <td class="SubCaption" align="right">
                    <asp:TextBox ID="txtDate" runat="server" Visible="False" BorderWidth="1px" BackColor="#F6F8FC" ReadOnly="True"></asp:TextBox>
                </td>
                <td class="SubCaption" align="left">
                    <asp:Label ID="lbltxtPeriod1" runat="server" Visible="False">на&nbsp;</asp:Label>
                    <asp:TextBox ID="txtPeriod" runat="server" Visible="False" BorderWidth="1px" BackColor="#F6F8FC"
                                 Width="20px">
                    </asp:TextBox>
                    <asp:Label ID="lbltxtPeriod2" runat="server" Visible="False">&nbsp;мес.&nbsp;</asp:Label>
                </td>
            </tr>
            <tr>
            </tr>
            <tr class="SubTitleTextbox">
                <td>
                </td>
                <td>
                    до
                </td>
                <td>
                    после
                </td>
            </tr>
            <tr>
                <td class="SubCaption" align="right">
                    СК ЦТО
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaCTOIn" runat="server" ToolTip="Введите марку ЦТО до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaCTOOut" runat="server" ToolTip="Введите марку ЦТО после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
            <asp:Panel ID="pnlMarkaCTO2" runat="server">
                <tr>
                    <td class="SubCaption" align="right">
                        СК ЦТО2
                    </td>
                    <td>
                        <asp:TextBox ID="txtedtMarkaCTO2In" runat="server" ToolTip="Введите марку ЦТО2 до обслуживания"
                                     BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtedtMarkaCTO2Out" runat="server" ToolTip="Введите марку ЦТО2 после обслуживания"
                                     BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                        </asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="SubCaption" align="right">
                    СК Реестра
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaReestrIn" runat="server" ToolTip="Введите марку Реестра до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaReestrOut" runat="server" ToolTip="Введите марку Реестра после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubCaption" align="right">
                    СК ПЗУ
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaPZUIn" runat="server" ToolTip="Введите марку ПЗУ до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaPZUOut" runat="server" ToolTip="Введите марку ПЗУ после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubCaption" align="right">
                    СК МФП
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaMFPIn" runat="server" ToolTip="Введите марку МФП до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtMarkaMFPOut" runat="server" ToolTip="Введите марку МФП после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
            <asp:Panel ID="pnlMarkaCP" runat="server">
                <tr>
                    <td class="SubCaption" align="right">
                        СК ЦП
                    </td>
                    <td>
                        <asp:TextBox ID="txtedtMarkaCPIn" runat="server" ToolTip="Введите марку ЦП до обслуживания"
                                     BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                        </asp:TextBox>
                    </td>
                    <td>
                        <asp:TextBox ID="txtedtMarkaCPOut" runat="server" ToolTip="Введите марку ЦП после обслуживания"
                                     BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                        </asp:TextBox>
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td class="SubCaption" align="right">
                    Z-отчёт
                </td>
                <td>
                    <asp:TextBox ID="txtedtZReportIn" runat="server" ToolTip="Введите номер Z-отчёта до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtZReportOut" runat="server" ToolTip="Введите номер Z-отчёта после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="SubCaption" align="right">
                    Итог
                </td>
                <td>
                    <asp:TextBox ID="txtedtItogIn" runat="server" ToolTip="Введите необнуляемый итог до обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
                <td>
                    <asp:TextBox ID="txtedtItogOut" runat="server" ToolTip="Введите необнуляемый итог после обслуживания"
                                 BorderWidth="1px" BackColor="#F6F8FC" Width="90px" MaxLength="11">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Дополнительная информация">
    <ItemTemplate>
        <asp:Label ID="lblInfo" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:Label ID="Label12" runat="server" CssClass="SubTitleTextbox">Дополнительная информация</asp:Label><br>
        <asp:TextBox ID="txtInfoEdit" runat="server" ToolTip="Введите дополнительную информацию"
                     Height="63px" BorderWidth="1px" BackColor="#F6F8FC" Width="252px" TextMode="MultiLine">
        </asp:TextBox><br>
        <asp:Label ID="lblSaleDoc" runat="server" CssClass="SubTitleTextbox" Visible="False">Тип документа</asp:Label><br>
        <asp:DropDownList ID="lstSaleDoc" runat="server" Height="22px" BackColor="#F6F8FC"
                          Visible="False">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Дата выполнения">
    <ItemTemplate>
        <asp:Label ID="lblCloseDate" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <asp:Label ID="lbledtCloseDate" runat="server"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Документы">
    <ItemTemplate>
        <asp:HyperLink ID="lnkAkt_TO" runat="server" Target="_blank"></asp:HyperLink><br>
        <asp:HyperLink ID="lnkDogovor_Na_TO" runat="server" Target="_blank"></asp:HyperLink><br>
        <asp:HyperLink ID="lnkAct" runat="server" Target="_blank"></asp:HyperLink><br>
        <asp:HyperLink ID="lnkTehZaklyuchenie" runat="server" Target="_blank"></asp:HyperLink><br>
        <br>
        <asp:LinkButton ID="btnDeleteDoc" runat="server" CommandName="DeleteDoc"></asp:LinkButton>
        <br>
        <br>
        <p style="margin-top: 0px; margin-bottom: 2px">
            <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ImageUrl="Images/edit.gif">
            </asp:ImageButton>
        </p>
        <br>
        <p style="margin-top: 0px; margin-bottom: 2px">
            <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ImageUrl="Images/delete.gif">
            </asp:ImageButton>
        </p>
    </ItemTemplate>
    <EditItemTemplate>
        <p>
            <asp:ImageButton ID="cmdUpdate" runat="server" ImageUrl="Images/update.gif" CommandName="Update">
            </asp:ImageButton>
        </p>
        <p>
            <asp:ImageButton ID="cmdCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel">
            </asp:ImageButton>
        </p>
    </EditItemTemplate>
</asp:TemplateColumn>
</Columns>
<PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
</asp:DataGrid>
</td>
</tr>
</asp:Panel>
</asp:Panel>
<tr>
    <td colspan="3" height="10">
    </td>
</tr>
</table>

<script type="text/javascript">

    jQuery(function() {

        jQuery('#txtCloseDate').datetimepicker({
            lang: 'ru',
            timepicker: false,
            format: 'd.m.Y',
            closeOnDateSelect: true,
            scrollMonth: false,
        });

        jQuery('#tbxDismissalDate').datetimepicker({
            lang: 'ru',
            timepicker: false,
            format: 'd.m.Y',
            closeOnDateSelect: true,
            scrollMonth: false,
        });

        jQuery('#tbxSupportDate').datetimepicker({
            lang: 'ru',
            timepicker: false,
            format: 'd.m.Y',
            closeOnDateSelect: true,
            scrollMonth: false,
        });

    });

</script>

<uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
<input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
<input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
<input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
<input id="FindHidden" type="hidden" name="FindHidden" runat="server">
</form>
</body>
</html>
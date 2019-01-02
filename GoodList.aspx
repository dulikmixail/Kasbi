<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.GoodList" CodeFile="GoodList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat="server">
    <title>[Кассовые аппараты]</title>

    <script language="javascript">
        <!--
        function OnLoad() {
            document.body.scrollTop = document.all['scrollPos'].value;
            var cal = document.all["cal"];
            var chk = document.all["chkUseDate"];
            if ((chk != null) & (cal != null)) {
                cal.disabled = ! chk.checked;
            }
            var lst1 = document.all["lstUseDateType_0"];
            var lst2 = document.all["lstUseDateType_1"];
            var lst3 = document.all["lstUseDateType_2"];
            if ((chk != null) & (lst1 != null) & (lst2 != null) & (lst3 != null)) {
                lst1.disabled = ! chk.checked;
                lst2.disabled = ! chk.checked;
                lst3.disabled = ! chk.checked;
            }
        }

        function isFind(s) {
            var theform = document.frmGoodList;
            theform.FindHidden.value = s;
        }
        -->

    </script>

    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value = document.body.scrollTop;"
      bottommargin="0" leftmargin="0" topmargin="0" onload="OnLoad();" rightmargin="0">
<form id="frmGoodList" method="post" runat="server">
<uc1:Header ID="Header1" runat="server"></uc1:Header>
<table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
    <tr>
        <td class="HeaderTitle" width="100%">
            &nbsp;Товары
        </td>
    </tr>
</table>
<table cellspacing="1" cellpadding="2" width="100%" border="0">
<tr class="Unit">
    <td class="Unit">
        &nbsp;Информация&nbsp;о&nbsp;кассовых&nbsp;аппаратах
    </td>
    <td class="Unit" align="right">
        <asp:HyperLink ID="btnNew" runat="server" EnableViewState="False" CssClass="PanelHider"
                       NavigateUrl="NewGood.aspx">
            Новый&nbsp;ККМ
        </asp:HyperLink>
    </td>
</tr>
<tr>
    <td colspan="2">
        <asp:Label ID="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:Label>
    </td>
</tr>
<tr>
    <td colspan="2">
        <asp:Label ID="msgCashregister" runat="server" EnableViewState="false" ForeColor="#ff0000"
                   Font-Bold="true">
        </asp:Label>
    </td>
</tr>
<tr class="SectionRow" height="10">
    <td class="SectionRowLabel" colspan="2">
        <table style="font-size: 7pt">
            <tr>
                <td nowrap>
                    <asp:LinkButton ID="btnFindGood" runat="server" EnableViewState="False" CssClass="PanelHider">Найти</asp:LinkButton>
                    &nbsp;&nbsp;<i>№</i>
                </td>
                <td>
                    <asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13" Width="100px"></asp:TextBox>
                </td>
                <td>
                    <i>СК&nbsp;ЦТО</i>
                </td>
                <td>
                    <asp:TextBox ID="txtFindGoodCTO" runat="server" BorderWidth="1px" Height="18px" MaxLength="11" Width="80px"></asp:TextBox>
                </td>
                <td>
                    <i>СК&nbsp;Изготовителя</i>
                </td>
                <td>
                    <asp:TextBox ID="txtFindGoodManufacturer" runat="server" BorderWidth="1px" Height="18px"
                                 MaxLength="11" Width="80px">
                    </asp:TextBox>
                </td>
                <td>
                    <i>Место&nbsp;установки</i>
                </td>
                <td>
                    <asp:TextBox ID="txtFindGoodSetPlace" runat="server" BorderWidth="1px" Height="18px"
                                 MaxLength="50" Width="280px">
                    </asp:TextBox>
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr>
    <td colspan="2">
        <asp:LinkButton ID="btnFilter" runat="server" CssClass="PanelHider">
            <asp:Image runat="server" ID="imgSelFilter" ImageUrl="Images/sel.gif" Style="z-index: 103; position: relative; left: 10px;"></asp:Image>&nbsp;Фильтр
        </asp:LinkButton>
        <asp:Label
            ID="lblFilterCaption" runat="server" Font-Size="8pt">
        </asp:Label>
    </td>
</tr>
<tr class="SectionRow">
    <td style="font-size: 7pt" align="center" colspan="2">
        <asp:Panel ID="pnlFilter" Style="border-top: #cc9933 1px solid; border-bottom: #cc9933 1px solid"
                   runat="server" Height="214px">
            <p style="margin-top: 0px; margin-bottom: -4px">
                &nbsp;
            </p>
            <table width="100%" align="center">
                <tr valign="middle">
                    <td style="font-size: 8pt; font-family: Verdana" valign="top" align="center" width="150"
                        rowspan="3">
                        <table style="font-size: 8pt; font-family: Verdana" cellspacing="0" cellpadding="0">
                            <tr>
                                <td>
                                    <asp:CheckBox ID="chkUseDate" runat="server" Text=" "></asp:CheckBox>
                                </td>
                                <td style="color: #990000">
                                    Использовать дату для выбора касс по
                                </td>
                            </tr>
                        </table>
                        <br>
                        <asp:RadioButtonList ID="lstUseDateType" runat="server" Width="144px" Font-Size="7pt"
                                             CellSpacing="0" CellPadding="0">
                            <asp:ListItem Value="0">дате продажи</asp:ListItem>
                            <asp:ListItem Value="1">нахождению в ремонте</asp:ListItem>
                            <asp:ListItem Value="2">занесению в базу</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td width="220" rowspan="5">
                        <asp:Calendar ID="cal" runat="server" ForeColor="#0066CC" Width="220px" Height="128px"
                                      BorderWidth="1px" Font-Size="8pt" SelectMonthText="<img src='Images/selmonth.gif' border=0/>"
                                      SelectWeekText="<img src='Images/selweek.gif' border=0 />" SelectionMode="DayWeekMonth"
                                      FirstDayOfWeek="Monday" BorderColor="#FFCC66" ShowGridLines="True" ToolTip="Выделите интересующий Вас период времени (день, неделя, месяц). "
                                      Font-Names="Verdana" BackColor="#FFFFCC">
                            <TodayDayStyle ForeColor="DarkRed" BackColor="#FFFFCC"></TodayDayStyle>
                            <SelectorStyle BackColor="#FFCC66"></SelectorStyle>
                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
                            <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
                            <SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
                            <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000">
                            </TitleStyle>
                            <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
                        </asp:Calendar>
                    </td>
                    <td style="font-size: 8pt; color: #990000; font-family: Verdana" align="center" width="120">
                        Наличие на складе
                    </td>
                    <td style="width: 134px; height: 18px">
                        <p style="font-size: 8pt; color: #990000; font-family: Verdana" align="center">
                            Тип
                        </p>
                    </td>
                    <td style="font-size: 8pt; color: #990000; font-family: Verdana" align="center">
                    </td>
                </tr>
                <tr valign="top">
                    <td style="padding-left: 10px; text-justify: newspaper; font-size: 8pt; font-family: Verdana"
                        width="100">
                        <asp:CheckBox ID="chkFreeGoods" runat="server" Font-Size="9pt" Text="Свободные" BorderWidth="1px"
                                      Width="100">
                        </asp:CheckBox>
                        <asp:CheckBox ID="chkRequestedGoods" runat="server" Font-Size="9pt" Text="Заказанные"
                                      BackColor="Honeydew" BorderWidth="1px" Width="100">
                        </asp:CheckBox>
                        <asp:CheckBox ID="chkSoldGoods" runat="server" Font-Size="9pt" Text="Проданные" BackColor="Azure"
                                      BorderWidth="1px" Width="100">
                        </asp:CheckBox>
                        <asp:CheckBox ID="chkOutSideGoods" runat="server" Font-Size="9pt" Text="Со стороны"
                                      BackColor="AntiqueWhite" BorderWidth="1px" Width="100">
                        </asp:CheckBox>
                    </td>
                    <td style="width: 134px" width="134">
                        <p>
                            <asp:ListBox ID="lstCashType" runat="server" Width="300px" SelectionMode="Multiple"
                                         Rows="5">
                            </asp:ListBox>
                        </p>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td width="100" height="65">
                    </td>
                    <td style="width: 134px" height="65">
                    </td>
                    <td width="100" height="65">
                    </td>
                </tr>
                <tr>
                    <td width="100">
                    </td>
                    <td>
                    </td>
                    <td style="width: 134px" align="right">
                        <asp:LinkButton ID="lblShow" runat="server" CssClass="LinkButton" EnableViewState="False">Показать</asp:LinkButton>
                    </td>
                    <td valign="bottom" align="right">
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </td>
</tr>
<tr>
<td align="center" colspan="2">
<asp:DataGrid ID="grdGood" Width="98%" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False"
              runat="server" BorderColor="#CC9966" BorderWidth="1px">
<ItemStyle CssClass="itemGrid"></ItemStyle>
<HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
<FooterStyle CssClass="footerGrid"></FooterStyle>
<Columns>
<asp:TemplateColumn HeaderText="№">
    <ItemStyle HorizontalAlign="Center"></ItemStyle>
    <ItemTemplate>
        <asp:Label runat="server" ID="lblNumGood" ForeColor="#9C0001" Font-Size="7pt" EnableViewState="true"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn>
    <HeaderStyle Width="80px"></HeaderStyle>
    <ItemStyle HorizontalAlign="Center"></ItemStyle>
    <ItemTemplate>
        <p style="margin-top: 2px; margin-bottom: 2px">
            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="Images/edit.gif" CommandName="Edit">
            </asp:ImageButton>
        </p>
        <p style="margin-top: 0px; margin-bottom: 2px">
            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Images/delete.gif" CommandName="Delete"
                             Visible="False">
            </asp:ImageButton>
        </p>
    </ItemTemplate>
    <EditItemTemplate>
        <p>
            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="Images/update.gif" CommandName="Update">
            </asp:ImageButton>
        </p>
        <p>
            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel">
            </asp:ImageButton>
        </p>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="delivery_sys_id" HeaderText="Поставка">
    <ItemTemplate>
        <asp:Label ID="lblGoodDelivery" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Поставка:
        </p>
        <asp:DropDownList ID="lstGoodDelivery" runat="server" Width="150px">
        </asp:DropDownList>
        <asp:Label ID="lblGoodDelivery" runat="server" Width="120px"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Товар">
    <ItemTemplate>
        <asp:Label ID="lbledtGoodName" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.good_name")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Товар:
        </p>
        <asp:DropDownList ID="lstGoodType" runat="server" Width="180px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="ownerInfo" HeaderText="Кому продан">
    <ItemTemplate>
        <asp:Label ID="lblSaleOwner" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Владелец:
        </p>
        <asp:DropDownList ID="lstSaleOwner" runat="server" Width="250px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="payerInfo" HeaderText="Плательщик (кто владеет)">
    <ItemTemplate>
        <asp:Label ID="lblCashOwner" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Пательщик ТО:
        </p>
        <asp:Label ID="lblCashOwner_edit" runat="server"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_cashregister" HeaderText="№">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:HyperLink ID="lbledtNum" runat="server" NavigateUrl='<%#
                "CashOwners.aspx?" & DataBinder.Eval(Container, "DataItem.good_sys_id") &
                "&cashowner=" & DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'
                       Text='<%#
                DataBinder.Eval(Container, "DataItem.num_cashregister")%>'>
        </asp:HyperLink>
        <p style="margin-top: 5px; margin-bottom: 0px" align="center">
            <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
            <asp:HyperLink ID="imgSupportSKNO" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/skno.gif" Visible="false"
                           ToolTip="установлено СКНО">
            </asp:HyperLink>
            <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                           ToolTip="На техобслуживании">
            </asp:HyperLink>
            <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%#
                "Repair.aspx?/" & DataBinder.Eval(Container, "DataItem.good_sys_id")%>'
                           ToolTip="В ремонте" ImageUrl="Images/repair.gif">
            </asp:HyperLink>
            <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%#
                "Repair.aspx?/" & DataBinder.Eval(Container, "DataItem.good_sys_id")%>'
                           ToolTip="Побывал в ремонте" ImageUrl="Images/repaired.gif">
            </asp:HyperLink>
        </p>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Заводской номер:
        </p>
        <asp:TextBox ID="txtedtNum" runat="server" BorderWidth="1px" Width="100%" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_cashregister")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_control_reestr" HeaderText="№ СК изг.">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtControl" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" &
                DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" &
                DataBinder.Eval(Container, "DataItem.num_control_mfp") & "<br>" &
                DataBinder.Eval(Container, "DataItem.num_control_cp")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            СК Реестра:
        </p>
        <asp:TextBox ID="txtedtReestr" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_reestr")%>'
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            СК ПЗУ:
        </p>
        <asp:TextBox ID="txtedtPZU" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_pzu")%>'
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            СК МФП:
        </p>
        <asp:TextBox ID="txtedtMFP" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_mfp")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
        <asp:Panel ID="pnlMarkaCP" runat="server">
            <p class="SubTitleEditbox">
                СК ЦП:
            </p>
            <asp:TextBox ID="txtedtCP" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_cp")%>'
                         BorderStyle="Solid">
            </asp:TextBox>
        </asp:Panel>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_control_reestr" HeaderText="№ СК СКНО.">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtControlSKNO" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.registration_number_skno") & "<br>" &
                DataBinder.Eval(Container, "DataItem.serial_number_skno")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Учетный номер СКНО:
        </p>
        <asp:TextBox ID="txtedtRegistrationNumberSkno" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.registration_number_skno")%>'
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            Заводской номер СКНО:
        </p>
        <asp:TextBox ID="txtedtSerialNumberSkno" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.serial_number_skno")%>'
                     BorderStyle="Solid">
        </asp:TextBox><br>
    </EditItemTemplate>
</asp:TemplateColumn>

<asp:TemplateColumn SortExpression="num_control_cto" HeaderText="№ СК ЦТО">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtCTO" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_cto") & "<br>" &
                DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            СК ЦТО:
        </p>
        <asp:TextBox ID="txtedtCTO" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_cto")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
        <asp:Panel ID="pnlMarkaCTO2" runat="server">
            <p class="SubTitleEditbox">
                СК ЦТО 2:
            </p>
            <asp:TextBox ID="txtedtCTO2" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'
                         BorderStyle="Solid">
            </asp:TextBox>
        </asp:Panel>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="price" HeaderText="Цена">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtPrice" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.price")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Цена:
        </p>
        <asp:TextBox ID="txtedtPrice" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.price")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="set_place" HeaderText="Место установки">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtPlace" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.set_place")%>'>
        </asp:Label>
        <br>
        <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='Район установки:'>
        </asp:Label>
        <b>
            <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.place_region")%>'>
            </asp:Label>
        </b>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Место установки:
        </p>
        <asp:TextBox ID="txtedtPlace" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.set_place")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
        <p class="SubTitleEditbox">
            Район установки:
        </p>
        <asp:DropDownList ID="lstRegionPlace" runat="server" Width="103px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Кассиры">
    <ItemTemplate>
        <asp:Label ID="lbledtKassir" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.kassir1") & "<br>" &
                DataBinder.Eval(Container, "DataItem.kassir2")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Кассир 1:
        </p>
        <asp:TextBox ID="txtedtKassir1" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.kassir1")%>'
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            Кассир 2:
        </p>
        <asp:TextBox ID="txtedtKassir2" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.kassir2")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Доп.инф.">
    <ItemTemplate>
        <asp:Label ID="lbledtInfo" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Дополнительная информация:
        </p>
        <asp:TextBox ID="txtedtInfo" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.good_info")%>'
                     Width="100%" BorderWidth="1px" BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:DataGrid>
</td>
</tr>
<tr class="Unit">
    <td colspan="2" class="Unit">
        &nbsp;Информация о торговом оборудовании
    </td>
</tr>
<tr>
    <td colspan="2" style="font-size: 9px">
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:LinkButton ID="lnk_findgoodto" runat="server" EnableViewState="False" CssClass="PanelHider">Найти</asp:LinkButton>
        &nbsp;&nbsp;<i>№</i>&nbsp;&nbsp;
        <asp:TextBox ID="txt_numgoodto" runat="server" BorderWidth="1px" Height="18px" MaxLength="12"
                     Width="100px">
        </asp:TextBox>&nbsp;(поиск по серийному номеру)
    </td>
</tr>
<tr>
<td colspan="2">

<asp:DataGrid ID="grdGoodTO" Width="98%" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False"
              runat="server" BorderColor="#CC9966" BorderWidth="1px">
<ItemStyle CssClass="itemGrid"></ItemStyle>
<HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
<FooterStyle CssClass="footerGrid"></FooterStyle>
<Columns>
<asp:TemplateColumn HeaderText="№">
    <ItemStyle HorizontalAlign="Center"></ItemStyle>
    <ItemTemplate>
        <asp:Label runat="server" ID="lblNumGood" ForeColor="#9C0001" Font-Size="7pt" EnableViewState="true"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn>
    <HeaderStyle Width="80px"></HeaderStyle>
    <ItemStyle HorizontalAlign="Center"></ItemStyle>
    <ItemTemplate>
        <p style="margin-top: 2px; margin-bottom: 2px">
            <asp:ImageButton ID="btnEdit" runat="server" ImageUrl="Images/edit.gif" CommandName="Edit">
            </asp:ImageButton>
        </p>
        <p style="margin-top: 0px; margin-bottom: 2px">
            <asp:ImageButton ID="btnDelete" runat="server" ImageUrl="Images/delete.gif" CommandName="Delete"
                             Visible="False">
            </asp:ImageButton>
        </p>
    </ItemTemplate>
    <EditItemTemplate>
        <p>
            <asp:ImageButton ID="btnUpdate" runat="server" ImageUrl="Images/update.gif" CommandName="Update">
            </asp:ImageButton>
        </p>
        <p>
            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel">
            </asp:ImageButton>
        </p>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="delivery_sys_id" HeaderText="Поставка">
    <ItemTemplate>
        <asp:Label ID="lblGoodDelivery" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Поставка:
        </p>
        <asp:DropDownList ID="lstGoodDelivery" runat="server" Width="120px">
        </asp:DropDownList>
        <asp:Label ID="lblGoodDelivery" runat="server" Width="120px"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Товар">
    <ItemTemplate>
        <asp:Label ID="lbledtGoodName" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.good_name")%>'>
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Товар:
        </p>
        <asp:DropDownList ID="lstGoodType" runat="server" Width="110px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="ownerInfo" HeaderText="Кому продан">
    <ItemTemplate>
        <asp:Label ID="lblSaleOwner" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Владелец:
        </p>
        <asp:DropDownList ID="lstSaleOwner" runat="server" Width="250px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="payerInfo" HeaderText="Плательщик (кто владеет)">
    <ItemTemplate>
        <asp:Label ID="lblCashOwner" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Пательщик ТО:
        </p>
        <asp:Label ID="lblCashOwner_edit" runat="server"></asp:Label>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_cashregister" HeaderText="№">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:HyperLink ID="lbledtNum" runat="server"
                       Text='<%#
                DataBinder.Eval(Container, "DataItem.good_num")%>'>
        </asp:HyperLink>
        <p style="margin-top: 5px; margin-bottom: 0px" align="center">
            <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
            <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                           ToolTip="На техобслуживании">
            </asp:HyperLink>
            <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%#
                "Repair.aspx?/" & DataBinder.Eval(Container, "DataItem.goodto_sys_id")%>'
                           ToolTip="В ремонте" ImageUrl="Images/repair.gif">
            </asp:HyperLink>
            <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%#
                "Repair.aspx?/" & DataBinder.Eval(Container, "DataItem.goodto_sys_id")%>'
                           ToolTip="Побывал в ремонте" ImageUrl="Images/repaired.gif">
            </asp:HyperLink>
        </p>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Заводской номер:
        </p>
        <asp:TextBox ID="txtedtNum" runat="server" BorderWidth="1px" Width="100%" Text='<%#
                DataBinder.Eval(Container, "DataItem.good_num")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_control_reestr" HeaderText="№ СК изг.">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtControl" runat="server">
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            СК Реестра:
        </p>
        <asp:TextBox ID="txtedtReestr" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            СК ПЗУ:
        </p>
        <asp:TextBox ID="txtedtPZU" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            СК МФП:
        </p>
        <asp:TextBox ID="txtedtMFP" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox>
        <asp:Panel ID="pnlMarkaCP" runat="server">
            <p class="SubTitleEditbox">
                СК ЦП:
            </p>
            <asp:TextBox ID="txtedtCP" runat="server" Width="100%" BorderWidth="1px"
                         BorderStyle="Solid">
            </asp:TextBox>
        </asp:Panel>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="num_control_cto" HeaderText="№ СК ЦТО">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtCTO" runat="server">
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            СК ЦТО:
        </p>
        <asp:TextBox ID="txtedtCTO" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox>
        <asp:Panel ID="pnlMarkaCTO2" runat="server">
            <p class="SubTitleEditbox">
                СК ЦТО 2:
            </p>
            <asp:TextBox ID="txtedtCTO2" runat="server" Width="100%" BorderWidth="1px"
                         BorderStyle="Solid">
            </asp:TextBox>
        </asp:Panel>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="price" HeaderText="Цена">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtPrice" runat="server">
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Цена:
        </p>
        <asp:TextBox ID="txtedtPrice" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn SortExpression="set_place" HeaderText="Место установки">
    <HeaderStyle Font-Underline="True"></HeaderStyle>
    <ItemTemplate>
        <asp:Label ID="lbledtPlace" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.set_place")%>'>
        </asp:Label>
        <br>
        <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='Район установки:'>
        </asp:Label>
        <b>
            <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%#
                DataBinder.Eval(Container, "DataItem.place_region")%>'>
            </asp:Label>
        </b>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Место установки:
        </p>
        <asp:TextBox ID="txtedtPlace" runat="server" Width="100%" BorderWidth="1px" Text='<%#
                DataBinder.Eval(Container, "DataItem.set_place")%>'
                     BorderStyle="Solid">
        </asp:TextBox>
        <p class="SubTitleEditbox">
            Район установки:
        </p>
        <asp:DropDownList ID="lstRegionPlace" runat="server" Width="103px">
        </asp:DropDownList>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Кассиры">
    <ItemTemplate>
        <asp:Label ID="lbledtKassir" runat="server">
        </asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Кассир 1:
        </p>
        <asp:TextBox ID="txtedtKassir1" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox><br>
        <p class="SubTitleEditbox">
            Кассир 2:
        </p>
        <asp:TextBox ID="txtedtKassir2" runat="server" Width="100%" BorderWidth="1px"
                     BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
<asp:TemplateColumn HeaderText="Доп.инф.">
    <ItemTemplate>
        <asp:Label ID="lbledtInfo" runat="server"></asp:Label>
    </ItemTemplate>
    <EditItemTemplate>
        <p class="SubTitleEditbox">
            Дополнительная информация:
        </p>
        <asp:TextBox ID="txtedtInfo" runat="server"
                     Width="100%" BorderWidth="1px" BorderStyle="Solid">
        </asp:TextBox>
    </EditItemTemplate>
</asp:TemplateColumn>
</Columns>
</asp:DataGrid>

</td>
</tr>
<tr>
    <td>
        &nbsp;
    </td>
</tr>
</table>
<uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
<input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server"/>
<input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server"/>
<input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server"/>
<input id="FindHidden" type="hidden" name="FindHidden" runat="server"/>
</form>
</body>
</html>
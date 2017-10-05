<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Main" Culture="ru-Ru"
    CodeFile="Main.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server" >
    <title>[Главная]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />

</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmMain" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Главная</td>
            </tr>
        </table>
        <asp:Label ID="lblLinks" runat="server">
        <table id="tblLinks" cellspacing="0" cellpadding="0" width="100%" runat="server">
            <tr>
                <td>
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
            </tr>
            <tr>
                <td class="SectionRow">
                    <asp:HyperLink ID="btnNew" runat="server" EnableViewState="False" CssClass="PanelHider"
                        NavigateUrl="NewRequest.aspx">
                        <asp:Image runat="server" ID="imgSelNew" ImageUrl="Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Новый клиент</asp:HyperLink></td>
                <td class="SectionRow">
                    <asp:HyperLink ID="lnkBookKeeping" runat="server" EnableViewState="False" CssClass="PanelHider"
                        NavigateUrl="BookKeeping.aspx">
                        <asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Бухгалтерия</asp:HyperLink></td>
            </tr>
        </table>
        </asp:Label>
        <table  cellspacing="0" cellpadding="0" width="100%" >
            <tr>
                <td valign="top">
                    <asp:Label ID="lblCash" runat="server">
                    <table  width="100%" id="StatisticTable" runat ="server">
                        <tr>
                            <td class="Unit" width="49%">
                            &nbsp;Информация о кассовых аппаратах</td>
                            <td class="Unit" width="49%">
                                &nbsp;Информация о ККМ за выбранный промежуток времени</td>
                        </tr>
                        <tr width="49%">
                            <td>
                                <asp:Label ID="msgCashInfo" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"></asp:Label></td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Repeater ID="repGoodStatistic" runat="server">
                                    <HeaderTemplate>
                                        <table align="center" id="Table2" cellspacing="0" cellpadding="0" width="90%" border="1">
                                    </HeaderTemplate>
                                    <ItemTemplate>
                                        <tr class="headerGrid">
                                            <td>
                                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                                            </td>
                                            <td width="10%">Продано</td>
                                            <td width="10%">Клиентам</td>
                                            <td width="10%">ЦТО</td>
                                        </tr>
                                        <tr class="itemGrid">
                                            <td >
                                                Всего:
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_all")%>
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_all_client")%>
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_all_cto")%>
                                            </td>
                                        </tr>
                                        <tr class="itemGrid">
                                            <td >
                                                Всего за месяц:
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_month")%>
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_month_client")%>
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_month_cto")%>
                                            </td>
                                        </tr>
                                        <tr class="itemGrid">
                                            <td >
                                                Всего за неделю:</td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_week")%>
                                            </td>
                                            <td  align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_week_client")%>
                                            </td>
                                            <td align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_week_cto")%>
                                            </td>
                                        </tr>
                                        <tr class="itemGrid">
                                            <td>
                                                Всего за день:
                                            </td>
                                            <td align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_day")%>
                                            </td>
                                            <td align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_day_client")%>
                                            </td>
                                            <td align="center" width="10%" style="font-weight: bold">
                                                    <%# DataBinder.Eval(Container.DataItem, "sale_day_cto")%>
                                                
                                            </td>
                                        </tr>
                                        <tr class="footerGrid" align="center">
                                            <td colspan="4">
                                                В базе:
                                                    <%# DataBinder.Eval(Container.DataItem, "cash_all")%>
                                                    &nbsp;&nbsp;Пришли со стороны:
                                                    <%# DataBinder.Eval(Container.DataItem, "cash_outside")%>
                                                    &nbsp;&nbsp;На складе:
                                                    <%# DataBinder.Eval(Container.DataItem, "cash_rest")%>
                                                
                                            </td>
                                        </tr>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        </table>
                                    </FooterTemplate>
                                </asp:Repeater>
                            </td>
                            <td valign="top" width="49%">
                                <table id="table3" width="90%" align="center">
                                    <tbody>
                                        <tr>
                                            <td width="100%">
                                                <asp:Label ID="msgCashDateRangeInfo" runat="server" Font-Bold="true" ForeColor="#ff0000"
                                                    EnableViewState="false"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td class="SectionRowLabel" style="width: 127px">
                                                <asp:Label ID="Label1" runat="server">Начальная дата:</asp:Label></td>
                                            <td class="SectionRow">
                                                <asp:TextBox ID="tbxBeginDate" runat="server" BorderWidth="1px"></asp:TextBox>
                                                <%--<a href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>--%>
                                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                            runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата" ControlToValidate="tbxBeginDate">*</asp:RequiredFieldValidator>&nbsp;<asp:Label
                                                                ID="lblDateFormat2" runat="server" CssClass="text02"></asp:Label>
                                                <asp:CompareValidator ID="typeValidator" runat="server" CssClass="SubTitleTextBox"
                                                    ControlToValidate="tbxBeginDate" EnableClientScript="False" Display="Dynamic"
                                                    Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:CompareValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="SectionRowLabel" style="width: 127px">
                                                <asp:Label ID="Label3" runat="server">Конечная дата:</asp:Label></td>
                                            <td class="SectionRow">
                                                <asp:TextBox ID="tbxEndDate" runat="server" BorderWidth="1px"></asp:TextBox>
                                                <%--<a href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>--%>
                                                <asp:RequiredFieldValidator
                                                        ID="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата "
                                                        ControlToValidate="tbxEndDate">*</asp:RequiredFieldValidator>&nbsp;<asp:Label ID="lblDateFormat3"
                                                            runat="server" CssClass="text02"></asp:Label>
                                                <asp:CompareValidator ID="CompareValidator1" runat="server" CssClass="SubTitleTextBox"
                                                    ControlToValidate="tbxEndDate" EnableClientScript="False" Display="Dynamic" Type="Date"
                                                    Operator="DataTypeCheck">Пожалуйста, введите корректные значение конечной даты</asp:CompareValidator></td>
                                        </tr>
                                        <tr>
                                            <td class="SectionRow" align="center" colspan="2">
                                                <asp:LinkButton ID="btnShow" runat="server" CssClass="PanelHider">Посчитать</asp:LinkButton></td>
                                        </tr>
                                        <tr>
                                            <tb>
                                                <asp:list
                                            </tb>
                                        </tr>
                                        <tr>
                                            <td class="SectionRow" colspan="2">
                                                <asp:Repeater ID="repGoodStatisticByDateRange" runat="server">
                                                    <HeaderTemplate>
                                                        <table align="center" id="Table5" cellspacing="0" cellpadding="0" width="100%" border="1">
                                                    </HeaderTemplate>
                                                    <ItemTemplate>
                                                        <tr class="headerGrid">
                                                            <td>
                                                                <%# DataBinder.Eval(Container.DataItem, "name") %>
                                                            </td>
                                                            <td width="10%">Продано</td>
                                                            <td width="10%">Клиентам</td>
                                                            <td width="10%">ЦТО</td>
                                                        </tr>
                                                        <tr class="itemGrid">
                                                            <td >
                                                                &nbsp;<asp:Label ID="lblDateRange" runat="server"></asp:Label>:</td>
                                                            <td  align="center" width="10%" style="font-weight: bold">
                                                                    <%# DataBinder.Eval(Container.DataItem, "sale")%>
                                                            </td>
                                                            <td  align="center" width="10%" style="font-weight: bold">
                                                                    <%# DataBinder.Eval(Container.DataItem, "sale_client")%>
                                                            </td>
                                                            <td  align="center" width="10%" style="font-weight: bold">
                                                                    <%# DataBinder.Eval(Container.DataItem, "sale_cto")%>
                                                            </td>
                                                        </tr>
                                                    </ItemTemplate>
                                                    <FooterTemplate>
                                                        </table>
                                                    </FooterTemplate>
                                                </asp:Repeater>
                                            </td>
                                        </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                    </asp:Label>
                </td>
            </tr>
            <tr height ="18">
                <td class="Unit" width="100%">
                    &nbsp;Информация о товарах на складе</td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="msgGoodInfo" runat="server" Font-Bold="true" ForeColor="#ff0000"></asp:Label></td>
            </tr>
            <tr>
                <td>
                    <table id="table" width="90%" align="center">
                        <tr>
                            <td class="SectionRowLabel" align="right">
                                Группы товаров:</td>
                            <td class="SectionRow">
                                <asp:DropDownList ID="lstGoodGroup" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                    Width="350px" AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td class="SectionRow">
                                <asp:LinkButton ID="lnkRestReport" runat="server" CssClass="LinkButton">
                                    <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>
                                    &nbsp;Экспорт данных в Excel&nbsp;</asp:LinkButton></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="2">
                    <asp:Repeater ID="repGoodRest" runat="server">
                        <HeaderTemplate>
                            <table align="center" id="Table4" cellspacing="0" cellpadding="0" width="90%" border=".1">
                                <tr class="headerGrid">
                                    <td width="10%">Артикул</td>
                                    <td width="55%">Наименование</td>
                                    <td width="15%">Остаток на складе</td>
                                    <td width="20%">Цена (Цена с НДС)</td>
                             
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td colspan="4" class = "itemGrid" style="color: #ffffcc; background-color: gray;" align ="center">
                                    <asp:Label ID="lblGoodGroupName" runat="server"> </asp:Label></td>
                            </tr>
                            <tr class = "itemGrid" align="center">
                            <td align="center">
                                  <%#DataBinder.Eval(Container.DataItem, "artikul")%>&nbsp;
                                </td>
                                <td align="left">
                                   <%# DataBinder.Eval(Container.DataItem, "name") %>
                                </td>
                                <td ><b><%# DataBinder.Eval(Container.DataItem, "quantity") %></b>
                                </td>
                                <td>
                                    <asp:Label ID="lblGoodPrice" runat="server" Font-Bold="True"></asp:Label></td>                                  
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <!-- <tr class=Caption align=center>
    <td><asp:label id=lblGoodInfo Font-Bold="true" Runat="server"></asp:label></td></tr>-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
        </table>

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

        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.SetRepair" Culture="ru-Ru"
CodeFile="SetRepair.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title>[Главная]</title>
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
<form id="frmMain" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <uc1:Header ID="Header1" runat="server"></uc1:Header>
    <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
        <tr>
            <td class="HeaderTitle" width="100%">&nbsp;Принятие ККМ в ремонт</td>
        </tr>
    </table>

    <br/>
    <div align="center">
        <asp:Label ID="lblCash" runat="server" Font-Size="12"></asp:Label>
    </div>

    <table id="TABLE1" width="90%" runat="server" style="margin: auto">
        <tr>
            <th style="width: 25%"></th>
            <th style="width: 75%" colspan="3"></th>
        </tr>
        <tr class="TitleTextbox">
            <td class="SectionRowLabel" align="left">Плательщик ремонта:</td>
            <td class="SectionRow" align="left" colspan="3" style="vert-align: middle">
                <asp:TextBox ID="txtCustomerFind" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                             Width="80%" MaxLength="11">
                </asp:TextBox>
                <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">
                    Найти
                </asp:LinkButton>

            </td>
        </tr>
        <tr class="SubTitleTextbox">
            <td class="SectionRowLabel" align="left">&nbsp;</td>
            <td class="SectionRow" align="left" colspan="3">
                <asp:ListBox ID="lstCustomers" runat="server" Width="100%" AutoPostBack="True"></asp:ListBox>
            </td>
        </tr>
        <tr>
            <td class="SectionRowLabel" align="left" style="height: 100%;">
                <asp:Label runat="server" ID="lblTelephoneNotice">
                    Телефоны оповещения, которые использовались ранее <br>(в верху самый последний который использовался):
                </asp:Label>
            </td>
            <td align="center" colspan="3">
                <asp:DropDownList ID="lstTelephoneNotice" runat="server" BackColor="#F6F8FC" Width="100%" BorderWidth="1px" AutoPostBack="True"/>
            </td>
        </tr>
        <tr class="TitleTextbox">
            <td class="SectionRowLabel" align="left">Телефон оповещения: </td>
            <td class="SectionRow" colspan="3" align="center">
                <asp:TextBox ID="txtTelephoneNotice" runat="server" ToolTip="Введите телефон оповещения" BackColor="#F6F8FC" Width="100%" MaxLength="250" BorderWidth="1px"/>
                <ajaxToolkit:MaskedEditValidator ID="txtTelephoneNotice_MaskedEditValidator" runat="server" ControlExtender="txtTelephoneNotice_MaskedEditExtender" ControlToValidate="txtTelephoneNotice" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="Введен некорректный мобильный телефон!" InvalidValueBlurredMessage="Введен некорректный мобильный телефон!" InvalidValueMessage="Введен некорректный мобильный телефон!" IsValidEmpty="True" ValidationExpression="^(29|25|44|33)(\d{7})$" ValidationGroup="GroupName">+375 (99) 999-99-99</ajaxToolkit:MaskedEditValidator>
                <ajaxToolkit:MaskedEditExtender ID="txtTelephoneNotice_MaskedEditExtender" runat="server" BehaviorID="txtTelephoneNotice_MaskedEditExtender" TargetControlID="txtTelephoneNotice" Mask="+375 (99) 999-99-99" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
            </td>
        </tr>
        <tr>
            <td class="SectionRow">&nbsp;</td>
            <td class="SectionRow" colspan="3">&nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
        </tr>
        <tr>
            <td class="SectionRowLabel" align="left">Выберите неисправности: </td>
            <td class="SectionRow" align="center" colspan="3">
                <table width="100%" style="font-size: 12px">
                    <tr>
                        <td align="center">
                            <asp:DataGrid ID="grdRepairBads" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                                          Width="95%" AllowPaging="False" BorderColor="#CC9966" BorderWidth="1px">
                                <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                                <ItemStyle CssClass="itemGrid"></ItemStyle>
                                <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                <FooterStyle CssClass="footerGrid"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Выбрать">
                                        <HeaderTemplate>
                                            Выбрать<br>
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                        </ItemTemplate>
                                        <ItemStyle Width="10%" HorizontalAlign="Center"></ItemStyle>
                                        <FooterTemplate>
                                            <asp:Label runat="server">Другое: </asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Наименование">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblName" Text='<%#
                DataBinder.Eval(Container, "DataItem.name")%>'>

                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="65%"></ItemStyle>
                                        <FooterTemplate>
                                            <asp:TextBox runat="server" ID="txtName" Width="100%"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Стоимость ремонта">
                                        <ItemTemplate>
                                            <asp:Label runat="server" ID="lblRepairSum" Text='<%# IIf(Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price_from_fix").ToString().Replace(".",","))<=0 And
                    Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price_to_fix").ToString().Replace(".",",")<=0),
                    "0 руб",
                    IIf(Not IsDBNull(DataBinder.Eval(Container.DataItem, "price_from_fix")),
                        "от " & DataBinder.Eval(Container.DataItem, "price_from_fix"), "") &
                    IIf(Not IsDBNull(DataBinder.Eval(Container.DataItem, "price_from_fix")) And Convert.ToDouble(DataBinder.Eval(Container.DataItem, "price_to_fix").ToString().Replace(".",","))>0,
                        " до " & DataBinder.Eval(Container.DataItem, "price_to_fix"), "") & " руб.")%>'>
                                            </asp:Label>
                                        </ItemTemplate>
                                        <ItemStyle Width="25%"></ItemStyle>
                                        <FooterTemplate>
                                            <asp:Label runat="server" Width="5%">&nbsp;от&nbsp;</asp:Label><asp:TextBox runat="server" ID="txtPriceFrom" Width="25%" TextMode="Number"></asp:TextBox>
                                            <asp:Label runat="server" Width="5%">&nbsp;до&nbsp;</asp:Label><asp:TextBox runat="server" ID="txtPriceTo" Width="25%" TextMode="Number"></asp:TextBox>
                                            <asp:Label runat="server" Width="5%">&nbsp;руб.&nbsp;</asp:Label>
                                        </FooterTemplate>
                                        <FooterStyle VerticalAlign="Middle"></FooterStyle>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="SectionRowLabel" align="left">Необходимо установить СКНО?:</td>
            <td>
                <asp:CheckBox runat="server" ID="isNeadSKNO" Checked="False"/>
            </td>
        </tr>
        <tr>
            <td class="SectionRowLabel" align="left">Повреждено средство конроля (СК):</td>
            <td>
                <asp:CheckBox runat="server" ID="isCtoControlDamaged" Checked="False"/>
            </td>
        </tr>
        <tr>
            <td class="SectionRowLabel" align="left">Документы:</td>
            <td class="SectionRowLabel">
                <asp:HyperLink runat="server" ID="lnkDefectAkt" Target="_blank" Text="Акт о принятии в ремонт" Visible="False"></asp:HyperLink>
            </td>
        </tr>
        <tr>
            <td></td>
            <td align="center" colspan="3">
                <asp:Label ID="lblErrors" runat="server" EnableViewState="false" ForeColor="#ff0000"
                           Font-Bold="true">
                </asp:Label>
            </td>
        </tr>
        <tr>
            <td align="center" colspan="4">
                <asp:Button ID="btnSetRepair" Text="Принять в ремонт" runat="server"/>
            </td>
        </tr>
    </table>


    <script type="text/javascript">

        jQuery(function() {
            jQuery('#tbxDismissalDate').datetimepicker({
                lang: 'ru',
                timepicker: false,
                format: 'd.m.Y',
                closeOnDateSelect: true,
                scrollMonth: false,
            });
        });

    </script>
    <script type="text/javascript">
        function generate_link(com) {
            if (com == 1)
                window.open("GoodList.aspx?numcashregister=" +
                    document.getElementById("search_kkm").value +
                    "&action=" +
                    document.getElementById("action_kkm").selectedIndex,
                    "_self")
            else if (com == 2)
                window.open("CustomerList.aspx?customer=" +
                    document.getElementById("search_cust").value +
                    "&action=" +
                    document.getElementById("action_cust").selectedIndex,
                    "_self")
        }
    </script>

    <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
    <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
    <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
    <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
</form>
</body>
</html>
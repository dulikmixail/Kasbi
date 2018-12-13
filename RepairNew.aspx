<%@ Reference Page="~/admin/details.aspx" %>
<%@ Reference Page="~/Documents.aspx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.RepairNew" CodeFile="RepairNew.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Рамок - [Ремонт кассовых аппаратов]</title>
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

    <style type="text/css">
        .auto-style4 {
            font-size: 10pt;
            font-style: italic;
            position: relative;
            top: 118px;
            left: 122px;
        }
    </style>

</head>
<body onscroll="javascript:document.all['scrollPos'].value = document.body.scrollTop;"
      bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.all['scrollPos'].value = document.body.scrollTop;"
      rightmargin="0">
<form id="frmRepairs" method="post" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
<uc1:Header ID="Header1" runat="server"></uc1:Header>
<table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
    <tr>
        <td class="HeaderTitle" width="100%">&nbsp;Карточка ККМ -&gt; Ремонт&nbsp;ККМ -&gt; Запись о ремонте</td>
    </tr>
</table>
<table cellspacing="1" cellpadding="2" width="100%">
    <tr class="Unit">
        <td class="Unit" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Кассовый аппарат</td>
    </tr>
    <tr>
        <td colspan="2">
            &nbsp;
            <asp:Label ID="msg" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red"
                       Font-Size="8pt">
            </asp:Label>
        </td>
    </tr>
    <tr>
        <td align="center" colspan="2">
            <table style="font-size: 9pt; font-family: Verdana; position: relative; top: -10px"
                   cellspacing="0" cellpadding="0" align="center">
            <tr>
                <td width="40"></td>
                <td width="100"></td>
                <td></td>
            </tr>
            <tr>
            <td>
                <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ToolTip="На техобслуживании"
                               ImageUrl="Images/support.gif">
                </asp:HyperLink>
                <asp:ImageButton ID="imgRepair" runat="server" ToolTip="В ремонте" ImageUrl="Images/repair.gif"></asp:ImageButton>
            </td>
        </td>
        <td align="right">
            <asp:Label ID="lblCashType" runat="server" Font-Bold="true"></asp:Label>
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
        <td>
            <asp:Label ID="lblMarka" runat="server" CssClass="cashDetail"></asp:Label>
        </td>
    </tr>
    <tr height="0">
        <td colspan="2">
            <asp:Label ID="lblCaptionSetPlace" runat="server" CssClass="cash">Место установки:</asp:Label>
        </td>
        <td>
            <asp:Label ID="lblSetPlace" runat="server" CssClass="cashDetail"></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="left" colspan="2">
            <asp:Label ID="lblCaptionSupport" runat="server" CssClass="cash">ТО:</asp:Label>
        </td>
        <td align="left">
            <asp:Label ID="lblSupport" runat="server" CssClass="cashDetail"></asp:Label>
        </td>
    </tr>
</table>
</td> </tr>
<tr class="Unit">
    <td class="Unit" colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Запись о ремонте</td>
</tr>
<tr>
<td colspan="2">
<asp:Label ID="msgNew" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red"
           Font-Size="8pt">
</asp:Label>
<table align="center">
<tr class="subCaption" align="center">
<td align="center" colspan="9">
<table id="pnlSupport" width="100%" runat="server">
<tr class="TitleTextbox">
    <td class="SubTitleTextbox">
        Дата принятия в ремонт:
        <br>
        <asp:CheckBox ID="chbRepairDateInEdit" runat="server" Text="изменить" AutoPostBack="True"></asp:CheckBox>
    </td>
    <td class="SectionRow" colspan="3">
        <asp:Label ID="lblRepairDateIn" runat="server" CssClass="text02"></asp:Label>
        <asp:Panel
            ID="pnlRepairDateIn" runat="server" Visible="False">
            <asp:TextBox ID="tbxRepairDateIn" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" CssClass="ErrorMessage"
                                        ErrorMessage="Дата приема в ремонт " ControlToValidate="tbxRepairDateIn" Display="Static">
                *
            </asp:RequiredFieldValidator>&nbsp;
            <asp:CompareValidator ID="Comparevalidator2" runat="server" CssClass="ErrorMessage"
                                  ControlToValidate="tbxRepairDateIn" Display="Dynamic" EnableClientScript="False"
                                  Type="Date" Operator="DataTypeCheck">
                Пожалуйста, введите корректные значение даты приема в ремонт
            </asp:CompareValidator>
        </asp:Panel>
    </td>
    <td class="SectionRow" colspan="5">
        Дата выдачи из ремонта:&nbsp;&nbsp;
        <asp:Label ID="lblRepairDateOut" runat="server" CssClass="text02"></asp:Label>
        <asp:TextBox ID="tbxRepairDateOut" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
        <%--<a href="javascript:showdatepicker('tbxRepairDateOut', 0, false,'DD.MM.YYYY')">
                                            <img id="imgRepairDateOut" alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>--%>
    </td>
</tr>

<tr class="SubCaption" align="center">
    <td width="60">&nbsp;</td>
    <td width="72">Марка ЦТО</td>
    <td width="72">
        <asp:Label ID="lblCaptionCTO2" runat="server" Text="Марка ЦТО2"></asp:Label>
    </td>
    <td width="72">Марка&nbsp;Реестра</td>
    <td width="72">Марка&nbsp;ПЗУ</td>
    <td width="72">Марка&nbsp;МФП</td>
    <td width="72">
        <asp:Label ID="lblCaptionCP" runat="server" Text="Марка ЦП"></asp:Label>
    </td>
    <td width="72">Z-отчёт</td>
    <td width="72">Итог</td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="top">
    <td class="SubCaption" valign="middle" align="right">до</td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaCTOIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaCTO2In" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО2 до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaReestrIn" runat="server" Font-Size="8pt" ToolTip="Введите марку Реестра до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaPZUIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ПЗУ до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaMFPIn" runat="server" Font-Size="8pt" ToolTip="Введите марку МФП до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewMarkaCPIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦП до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewZReportIn" runat="server" Font-Size="8pt" ToolTip="Введите номер Z-отчёта до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20">
        </asp:TextBox>
    </td>
    <td style="width: 78px">
        <asp:TextBox ID="txtNewItogIn" runat="server" Font-Size="8pt" ToolTip="Введите необнуляемый итог до ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr>
    <td class="SubCaption" valign="middle" align="right" colspan="1">после</td>
    <td>
        <asp:TextBox ID="txtNewMarkaCTOOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewMarkaCTO2Out" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО2 после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewMarkaReestrOut" runat="server" Font-Size="8pt" ToolTip="Введите марку Реестра после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewMarkaPZUOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ПЗУ после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewMarkaMFPOut" runat="server" Font-Size="8pt" ToolTip="Введите марку МФП после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewMarkaCPOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦП после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewZReportOut" runat="server" Font-Size="8pt" ToolTip="Введите номер Z-отчёта после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20">
        </asp:TextBox>
    </td>
    <td>
        <asp:TextBox ID="txtNewItogOut" runat="server" Font-Size="8pt" ToolTip="Введите необнуляемый итог после ремонта"
                     BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="middle">
    <td class="SubTitleTextbox" align="left"></td>
    <td class="SubTitleTextbox" align="left" colspan="3">Исполнитель</td>
    <td class="SubTitleTextbox" align="left" colspan="4">&nbsp;</td>
    <td class="SubTitleTextbox" align="left" width="72">Акт</td>
</tr>
<tr>
    <td style="height: 17px"></td>
    <td style="width: 154px; height: 17px" colspan="7">
        <asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="336px">
        </asp:DropDownList>
    </td>
    <td style="height: 17px">
        <asp:TextBox ID="txtNewAkt" runat="server" ToolTip="Введите номер акта" BorderWidth="1px"
                     BackColor="#F6F8FC" Width="80">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr class="TitleTextbox">
    <td class="SubTitleTextbox" align="left">Плательщик ремонта:</td>
    <td class="SectionRow" align="left" colspan="8">
        <asp:TextBox ID="txtCustomerFind" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                     Width="87%" MaxLength="11">
        </asp:TextBox>
        <asp:LinkButton ID="lnkCustomerFind" runat="server"
                        CssClass="LinkButton">
            &nbsp;&nbsp;&nbsp;Найти
        </asp:LinkButton>
    </td>
</tr>
<tr class="SubTitleTextbox">
    <td class="SectionRowLabel" align="left">&nbsp;</td>
    <td class="SectionRow" align="left" colspan="8">
        <asp:ListBox ID="lstCustomers" runat="server" Width="100%" AutoPostBack="True"></asp:ListBox>
    </td>
</tr>
<tr>
    <td class="SectionRow">&nbsp;</td>
    <td class="SectionRow" colspan="8">&nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
</tr>
<tr>
    <td class="SubTitleTextbox" style="height: 14px" colspan="9">
        Перечень деталей и работ
    </td>
</tr>
<tr>
    <td align="center" colspan="9">
        <asp:DataGrid ID="grdDetails" runat="server" BorderWidth="1px" Width="100%" CellPadding="1"
                      AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True" BorderColor="#CC9966">
            <AlternatingItemStyle Font-Size="8pt" Font-Names="Verdana" BackColor="#FCF8F6"></AlternatingItemStyle>
            <ItemStyle CssClass="itemGrid" VerticalAlign="Top"></ItemStyle>
            <HeaderStyle CssClass="headerGrid" VerticalAlign="Top"></HeaderStyle>
            <FooterStyle CssClass="footerGrid" Font-Bold="True" VerticalAlign="Top"></FooterStyle>
            <Columns>
                <asp:TemplateColumn HeaderText="Name">
                    <HeaderStyle Width="300pt"></HeaderStyle>
                    <HeaderTemplate>
                        Наименование
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="cbxIsDetail" runat="server" Visible="False" Checked='<%#
                IIf(DataBinder.Eval(Container.DataItem, "is_detail"), "True", "False")%>'>
                        </asp:CheckBox>
                        <asp:Label ID="lblName" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "name")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterStyle HorizontalAlign="Right"></FooterStyle>
                    <FooterTemplate>
                        <asp:DropDownList ID="lstAddDetail" runat="server" Width="100%" AutoPostBack="True"
                                          OnSelectedIndexChanged="LoadDetailInfo">
                        </asp:DropDownList><br>
                        <hr>
                        СТОИМОСТЬ&nbsp;:&nbsp;<br>
                        <asp:CheckBox ID="cbxWorkNotCall" runat="server" ForeColor="Red" AutoPostBack="True"
                                      Text="Не включать стоимость работ " OnCheckedChanged="RecalcCost">
                        </asp:CheckBox><br>
                        <asp:CheckBox ID="cbxGarantia" runat="server" ForeColor="Red" AutoPostBack="True"
                                      Text="Гарантийный ремонт" OnCheckedChanged="RecalcCost">
                        </asp:CheckBox>
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Количество">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "quantity")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:TextBox ID="txtAddQuantity" runat="server" Width="100%" Text='<%#
                DataBinder.Eval(Container, "DataItem.quantity")%>'
                                     BorderStyle="Solid" BorderWidth="1px">
                        </asp:TextBox><hr>
                    </FooterTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtQuantity" runat="server" Width="100%" Text='<%#
                DataBinder.Eval(Container, "DataItem.quantity")%>'
                                     BorderStyle="Solid" BorderWidth="1px">
                        </asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Цена детали">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPrice" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "price")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblAddPrice" Text="" runat="server" Height="22" ForeColor="Black"></asp:Label><br>
                        <hr>
                        <asp:Label ID="lblTotalPrice" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Стоимость услуги">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblCostService" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "cost_service")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblAddCostService" Text="" runat="server" Height="22" ForeColor="Black"></asp:Label><br>
                        <hr>
                        <asp:Label ID="lblTotalCostService" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Общая стоимость">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblTotalSum" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "total_sum")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblAddTotalSum" Text="" runat="server" Height="22" ForeColor="Black"></asp:Label><br>
                        <hr>
                        <asp:Label ID="lblTotalAllSum" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="Норма/час">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblNormaHour" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "norma_hour")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <FooterTemplate>
                        <asp:Label ID="lblAddTotalNormaHour" Text="" runat="server" Height="22" ForeColor="Black"></asp:Label><br>
                        <hr>
                        <asp:Label ID="lblTotalNormaHour" runat="server"></asp:Label>
                    </FooterTemplate>
                </asp:TemplateColumn>
                <asp:TemplateColumn>
                    <ItemStyle HorizontalAlign="center"/>
                    <ItemTemplate>
                        <asp:ImageButton ID="cmdEdit" runat="server" ToolTip="Изменить" CommandName="Edit"
                                         ImageUrl="Images/edit_small.gif">
                        </asp:ImageButton>&nbsp;
                        <asp:ImageButton ID="cmdDelete" runat="server" ToolTip="Удалить" CommandName="Delete"
                                         ImageUrl="Images/delete_small.gif">
                        </asp:ImageButton>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton ID="cmdUpdate" runat="server" ToolTip="Сохранить" CommandName="Update"
                                         ImageUrl="Images/edit_small.gif">
                        </asp:ImageButton>&nbsp;
                        <asp:ImageButton ID="cmdCancel" runat="server" ToolTip="Отменить" CommandName="Cancel"
                                         ImageUrl="Images/delete_small.gif">
                        </asp:ImageButton>
                    </EditItemTemplate>
                    <FooterTemplate>
                        <asp:LinkButton ID="btnAddDetail" runat="server" Font-Size="8pt" CommandName="AddDetail"
                                        Height="22">
                            Добавить
                        </asp:LinkButton><hr>
                    </FooterTemplate>
                </asp:TemplateColumn>
            </Columns>
            <PagerStyle Font-Size="8pt" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </td>
</tr>
<tr valign="middle">
    <td class="SubTitleTextbox" colspan="9">
        Перечень предполагаемых неисправностей или работ
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="top">
    <td colspan="9">
        <asp:TextBox ID="txtRepairBadsInfo" runat="server" ToolTip="Введите дополнительную информацию"
                     Height="120px" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" TextMode="MultiLine">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="middle">
    <td class="SubTitleTextbox" style="height: 14px" colspan="4">Детали</td>
    <td class="SubTitleTextbox" style="height: 14px" colspan="5">Проведенные ремонтные работы</td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="top">
    <td colspan="4">
        <asp:TextBox ID="txtNewDetails" runat="server" ToolTip="Перечислите детали, затраченные при ремонте"
                     Height="80px" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" TextMode="MultiLine">
        </asp:TextBox>
    </td>
    <td colspan="5">
        <asp:TextBox ID="txtNewInfo" runat="server" ToolTip="Введите дополнительную информацию"
                     Height="80px" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" TextMode="MultiLine">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="middle">
    <td class="SubTitleTextbox" colspan="9">
        Дополнительная информация
    </td>
    <td width="72">&nbsp;</td>
</tr>

<tr valign="top">
    <td colspan="9">
        <asp:TextBox ID="txtNewRepairInfo" runat="server" ToolTip="Введите дополнительную информацию"
                     Height="50px" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" TextMode="MultiLine">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr valign="top">
    <td class="SubTitleTextbox" align="left">Место хранения:</td>
    <td colspan="8">
        <asp:TextBox ID="txtStorageNumber" runat="server" ToolTip="Введите дополнительную информацию" BorderWidth="1px" BackColor="#F6F8FC" Width="100%">
        </asp:TextBox>
    </td>
    <td width="72">&nbsp;</td>
</tr>
<tr>
    <td colspan="9">&nbsp;</td>
</tr>
<tr class="SubTitleTextbox" >
    <td colspan="1">
        СКНО
    </td>
    <td colspan="4">
        <asp:CheckBox runat="server" ID="cbxNeadSKNO" Text="Небходимо установить/снять СКНО" Enabled="False" AutoPostBack="True"/>
    </td>
    <td colspan="4">
        <asp:Label runat="server" ID="lblErrorInfo" CssClass="errorInfo"></asp:Label>
    </td>
</tr>
<tr>
    <td colspan="9"><asp:Label runat="server" ID="lblNeadSKNO"><hr/></asp:Label></td>
</tr>
<tr class="SectionRow">
    <td colspan="9">
        <asp:Panel runat="server" Width="100%" ID="pnlSKNO">
<%--            <table width="100%">
                <tr>
                    <td width="50%" class="SubTitleTextbox">Исполнитель</td>
                    <td width="50%"></td>
                </tr>
                <tr>
                    <td>
                        <asp:DropDownList ID="lstWorkerSKNO" runat="server" BackColor="#F6F8FC" Width="95%">
                        </asp:DropDownList>
                    </td>
                    <td>
                        <asp:Label runat="server" ID="lblUpdateUser" CssClass="SubTitleTextbox"></asp:Label>
                    </td>
                </tr>
            </table>--%>

            <table width="100%">
                <tr>
                    <%--<td class="SectionRow" width="30%">
                        <asp:RadioButtonList ID="rbSKNO" runat="server" CssClass="text02" RepeatColumns="1" AutoPostBack="True">
                            <asp:ListItem Value="1">установить</asp:ListItem>
                            <asp:ListItem Value="0">снять</asp:ListItem>
                        </asp:RadioButtonList>
                    </td>--%>
                    <td width="80%">
                        <table width="100%">
                            <tr class="SubCaption" align="center">
                                <td>Учетный номер СКНО</td>
                                <td>Заводской номер СКНО</td>
                            </tr>
                            <tr align="center">
                                <td>
                                    <asp:TextBox runat="server" ID="txtRegistrationNumberSKNO" Width="95%"></asp:TextBox>
                                    <ajaxToolkit:MaskedEditValidator ID="txtRegistrationNumberSKNO_MaskedEditValidator" runat="server" ControlExtender="txtRegistrationNumberSKNO_MaskedEditExtender" ControlToValidate="txtRegistrationNumberSKNO" Display="Dynamic" EmptyValueBlurredText="*" IsValidEmpty="True" ValidationExpression="^\d{9}$" ValidationGroup="GroupName"></ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:MaskedEditExtender ID="txtRegistrationNumberSKNO_MaskedEditExtender" runat="server" BehaviorID="txtRegistrationNumberSKNO_MaskedEditExtender" TargetControlID="txtRegistrationNumberSKNO" Mask="999999999" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
                                </td>
                                <td>
                                    <asp:TextBox runat="server" ID="txtSerialNumberSKNO" Width="95%" ></asp:TextBox>
                                    <ajaxToolkit:MaskedEditValidator ID="txtSerialNumberSKNO_MaskedEditValidator" runat="server" ControlExtender="txtSerialNumberSKNO_MaskedEditExtender" ControlToValidate="txtSerialNumberSKNO" Display="Dynamic" EmptyValueBlurredText="*" IsValidEmpty="True" ValidationExpression="^\d{4,9}$" ValidationGroup="GroupName"></ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:MaskedEditExtender ID="txtSerialNumberSKNO_MaskedEditExtender" runat="server" BehaviorID="txtSerialNumberSKNO_MaskedEditExtender" TargetControlID="txtSerialNumberSKNO" Mask="999999999" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
                                </td>
                            </tr>
                            <tr class="SubCaption" align="center">
                                <td colspan="2">Комментарий</td>
                            </tr>
                            <tr align="center">
                                <td colspan="2"><asp:TextBox runat="server" ID="txtComment" Width="97%"></asp:TextBox></td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </asp:Panel>
    </td>
    
</tr>
<tr>
    <td colspan="9">&nbsp;</td>
</tr>
<tr class="TitleTextbox">
    <td class="SubTitleTextbox" colspan="9">
        СМС о готовности ремонта:
        <hr/>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SectionRow" colspan="1">
        <asp:CheckBox ID="cbxSmsSend" runat="server" Text="отправить" AutoPostBack="True" Checked="True" Font-Size="10pt"></asp:CheckBox>
        <asp:CheckBox ID="cbxEtitSmsSend" runat="server" Text="изменить" AutoPostBack="True" Checked="False" Font-Size="10pt"></asp:CheckBox>
    </td>
    <td class="SectionRow" colspan="2">
        <asp:Label runat="server" Font-Size="10pt">Номер: </asp:Label>
        <asp:TextBox ID="txtPhoneNumber" runat="server" Font-Size="10pt" ToolTip="Номер СМС" BorderWidth="1px" BackColor="#F6F8FC" Width="90%">
        </asp:TextBox>
        <asp:Label ID="lblPhoneNumber" runat="server" Font-Size="10pt" ToolTip="Номер СМС" Width="90%">
        </asp:Label>
    </td>
    <td class="SectionRow" colspan="6">
        <asp:Label runat="server" Font-Size="10pt">Текст: </asp:Label>
        <asp:TextBox ID="txtSmsText" runat="server" Font-Size="10pt" ToolTip="Текс СМС" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" Height="26pt" ReadOnly="False" TextMode="MultiLine">
        </asp:TextBox>
        <asp:Label ID="lblSmsText" runat="server" Font-Size="10pt" ToolTip="Текс СМС" Width="100%">
        </asp:Label>
    </td>
</tr>
<tr class="TitleTextbox">
    <td class="SubTitleTextbox" colspan="9">
        История отправленных СМС:
    </td>
</tr>
<tr>
    <td align="center" colspan="9">
        <asp:DataGrid ID="grdSmsHistory" runat="server" BorderWidth="1px" Width="100%" CellPadding="1"
                      AutoGenerateColumns="False" AllowSorting="True" ShowFooter="True" BorderColor="#CC9966">
            <AlternatingItemStyle Font-Size="8pt" Font-Names="Verdana" BackColor="#FCF8F6"></AlternatingItemStyle>
            <ItemStyle CssClass="itemGrid" VerticalAlign="Top"></ItemStyle>
            <HeaderStyle CssClass="headerGrid" VerticalAlign="Top"></HeaderStyle>
            <FooterStyle CssClass="footerGrid" Font-Bold="True" VerticalAlign="Top"></FooterStyle>
            <Columns>
                <asp:TemplateColumn HeaderText="SmsTelNumber">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <HeaderTemplate>Номер</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSmsTelNumber" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "recipient")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SmsText">
                    <HeaderStyle Width="35%"></HeaderStyle>
                    <HeaderTemplate>Текст</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSmsText" runat="server" Text='<%#DataBinder.Eval(Container.DataItem, "sms_text")%>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateColumn>

                <asp:TemplateColumn HeaderText="Status">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <HeaderTemplate>Статус</HeaderTemplate>
                    <ItemTemplate>  
                        <asp:Label ID="lblStatus" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "sms_status_rus")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="CountSendedSms">
                    <HeaderStyle Width="10%"></HeaderStyle>
                    <HeaderTemplate>Кол-во СМС</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblCountSendedSms" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "sms_count")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="UpdateStatusDate">
                    <HeaderStyle Width="15%"></HeaderStyle>
                    <HeaderTemplate>Дата начала отправки</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblUpdateStatusDate" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "start_date")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>
                <asp:TemplateColumn HeaderText="SenderName">
                    <HeaderStyle Width="20%"></HeaderStyle>
                    <HeaderTemplate>Отправитель</HeaderTemplate>
                    <ItemTemplate>
                        <asp:Label ID="lblSenderName" runat="server" Text='<%#
                DataBinder.Eval(Container.DataItem, "Name")%>'>
                        </asp:Label>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateColumn>

            </Columns>
            <PagerStyle Font-Size="8pt" HorizontalAlign="Center" Mode="NumericPages"></PagerStyle>
        </asp:DataGrid>
    </td>
</tr>

</table>
</td>
</tr>
</table>
</td>
</tr>
<tr>
    <td colspan="2" height="15">&nbsp;</td>
</tr>
</table>
<table cellspacing="0" cellpadding="0" width="100%">
    <tr>
        <td width="100%"></td>
    </tr>
    <tr class="Unit">
        <td class="Unit" align="center">
            <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel"
                             CausesValidation="False">
            </asp:ImageButton>&nbsp;&nbsp;
            <asp:ImageButton ID="btnSave" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton>
        </td>
    </tr>
    <tr>
        <td height="10"></td>
    </tr>
</table>

<script language="javascript">
    jQuery(function() {

        jQuery('#tbxRepairDateIn, #tbxRepairDateOut').datetimepicker({
            lang: 'ru',
            timepicker: true,
            format: 'd.m.Y H:i',
            closeOnDateSelect: true,
            scrollMonth: false,
            step: 30
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
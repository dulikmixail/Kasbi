<%@ Page Language="VB" AutoEventWireup="false" CodeFile="KkmSoftwareVersion.aspx.vb" Inherits="Kasbi.Reports.KkmSoftwareVersion" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>[Версии ПО ККМ]</title>
    <link href="../styles.css" type="text/css" rel="stylesheet"/>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet"/>
</head>
<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
<form id="form1" runat="server">
    <uc1:header id="Header1" runat="server"></uc1:header>
    <table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
        <tr>
            <td class="HeaderTitle" width="100%">&nbsp;Отчеты&nbsp;-&gt;&nbsp;Версии&nbsp;ПО&nbsp;ККМ</td>
        </tr>
    </table>
    <table cellSpacing="1" cellPadding="2" width="900px" border="0">
        <tbody>
        <tr>
            <td align="left" colspan="4">
                <asp:label id="lblError" runat="server" Visible="True" Font-Bold="True" ForeColor="Red" Font-Size="12pt">Label</asp:label>
            </td>
        </tr>
        <tr>
            <td class="SectionRowLabel" style="width: 150px">
                <asp:label id="lblCashRegister" runat="server">Кассовое оборудование:</asp:label>
            </td>
            <td class="SectionRow">
                <asp:ListBox runat="server" ID="lstCashRegister" Rows="10" Width="350px" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td class="SectionRowLabel" style="width: 150px">
                <asp:label id="lblSoftwareVersion" runat="server">Версия ПО:</asp:label>
            </td>
            <td class="SectionRow">
                <asp:ListBox runat="server" ID="lstSoftwareVersion" Rows="10" Width="300px" SelectionMode="Multiple"></asp:ListBox>
            </td>
        </tr>

        </tbody>
    </table>
    <table id="Table1" cellSpacing="1" cellPadding="2" width="100%" border="0">
        <tbody>
        <tr>
            <td class="Unit" align="center">
                <asp:ImageButton id="btnView" runat="server" ImageUrl="../Images/create.gif"></asp:ImageButton>&nbsp;&nbsp;
                <asp:ImageButton id="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False"></asp:ImageButton>
            </td>
        </tr>
        </tbody>
    </table>
    <asp:PlaceHolder runat="server" ID ="plhTotalItog"></asp:PlaceHolder>
    <asp:DataGrid runat="server" ID="grdSoftwareVersion" AutoGenerateColumns="False" Width="100%" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
        <ItemStyle CssClass="itemGrid"></ItemStyle>
        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
        <FooterStyle CssClass="footerGrid"></FooterStyle>
        <Columns>
            <asp:TemplateColumn HeaderText="№">
                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                <ItemTemplate>
                    <asp:Label ID="lblNum" runat="server" ForeColor="black"><%# NumRow%></asp:Label>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Владелец" SortExpression="customer_full_name">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%# "../CustomerList.aspx?edit=" & DataBinder.Eval(Container, "DataItem.customer_sys_id")%>'>
                        <%#
                DataBinder.Eval(Container, "DataItem.customer_full_name")%>
                    </asp:HyperLink>

                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Наименование кассы" SortExpression="good_name">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.good_name")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Номер кассы" SortExpression="num_cashregister">
                <ItemTemplate>
                    <asp:HyperLink runat="server" NavigateUrl='<%#
                "../CashOwners.aspx?" & DataBinder.Eval(Container, "DataItem.good_sys_id")%>'>
                        <%#
                DataBinder.Eval(Container, "DataItem.num_cashregister")%>
                    </asp:HyperLink>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Версия ПО" SortExpression="software_version">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.software_version")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Дата доработки" SortExpression="repairdate_out">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.repairdate_out")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Сумма доработки, руб" SortExpression="summa">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.summa")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Мастер сделавший доработку" SortExpression="executor_name">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.executor_name")%>
                </ItemTemplate>
            </asp:TemplateColumn>
            <asp:TemplateColumn HeaderText="Мастер ТО" SortExpression="master_cto_name">
                <ItemTemplate>
                    <%#
                DataBinder.Eval(Container, "DataItem.master_cto_name")%>
                </ItemTemplate>
            </asp:TemplateColumn>
        </Columns>
    </asp:DataGrid>

</form>
<style>
    .itog-table {
        border-collapse: collapse;
    }
    .itog-table td,
    .itog-table th{
        border: 1px solid black;
    }
</style>
</body>
</html>
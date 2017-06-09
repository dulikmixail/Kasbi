<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Reports.AdvertisingReports"
    CodeFile="AdvertisingReports.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title>[Отчет по рекламе]</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body>
    <form id="Form1" method="post" runat="server">
        <table id="Table3" width="800" border="0" runat="server">
            <tbody>
                <tr>
                    <td width="20">
                    </td>
                    <td align="center" colspan="3">
                        <table width="100%">
                            <tr>
                                <td>
                                    <div align="left">
                                        <img height="92" src="../images/logotip.gif" width="222"></div>
                                </td>
                                <td align="center" colspan="3">
                                    <h2>
                                        Отчёт по рекламе</h2>
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="10">
                    </td>
                    <td colspan="3">
                        <hr size="1">
                    </td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="20">
                    </td>
                    <td style="font-size: 9pt" align="left" width="15%">
                        Начальная дата:</td>
                    <td align="left" width="20">
                        <asp:Label ID="lblStartDate" Font-Size="9" runat="server"></asp:Label></td>
                    <td>
                    </td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="20">
                    </td>
                    <td style="font-size: 9pt" align="left" width="1%">
                        Конечная дата:</td>
                    <td align="left" width="20">
                        <asp:Label ID="lblEndDate" Font-Size="9" runat="server"></asp:Label></td>
                    <td align="right">
                        <asp:Label ID="lblPrintDate" Font-Size="9" runat="server"></asp:Label></td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="10">
                    </td>
                    <td colspan="3">
                        <hr size="1">
                    </td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="20">
                    </td>
                    <td align="center" colspan="3">
                        <asp:Repeater ID="repAdvertising" runat="server">
                            <HeaderTemplate>
                                <table id="Table1" cellspacing="2" cellpadding="1" width="100%" border="0">
                            </HeaderTemplate>
                            <ItemTemplate>
                                <tr style="font-size: 9pt; color: #ffffcc; background-color: #990000; text-align: center;">
                                    <td>
                                        <font size="3">
                                            <%#DataBinder.Eval(Container.DataItem, "adv_Name")%>
                                        </font>
                                    </td>
                                    <td>
                                        Кол-во клиентов: <font size="3"><b>
                                            <%# DataBinder.Eval(Container.DataItem, "customer_count")%>
                                        </b></font>на сумму: <b>
                                            <asp:Label ID="lbltotaladvertis1" runat="server"></asp:Label></b></td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Repeater ID="repCustomers" runat="server">
                                            <HeaderTemplate>
                                                <table id="Table2" cellspacing="2" cellpadding="1" width="100%" border="0">
                                            </HeaderTemplate>
                                            <ItemTemplate>
                                                <tr style="color: black; font-style: italic; background-color: gainsboro">
                                                    <td colspan="2" width="20%">
                                                        <font size="2" style="font-weight: bold">
                                                            <asp:Label ID="HeaderSale" runat="server"></asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;Клиент
                                                            №&nbsp;<%# DataBinder.Eval(Container.DataItem, "customer_sys_id")%></font></td>
                                                    <td>
                                                        <font size="2"><font style="font-weight: bold">
                                                            <%# DataBinder.Eval(Container.DataItem, "customer_name")%>
                                                        </font></font>
                                                    </td>
                                                    <td align="right" width="30%">
                                                        <font size="2" style="font-weight: bold">Дата внесения в БД:
                                                            <%#Format(DataBinder.Eval(Container.DataItem, "d"), "dd.MM.yyyy")%>
                                                        </font>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <asp:DataGrid ID="grdSales" runat="server" Font-Size="9pt" AutoGenerateColumns="False"
                                                            ShowFooter="True" Width="100%">
                                                            <HeaderStyle Font-Size="10pt" Font-Underline="True" HorizontalAlign="Center"></HeaderStyle>
                                                            <FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
                                                            <Columns>
                                                                <asp:TemplateColumn HeaderText="№">
                                                                    <ItemStyle Width="10" HorizontalAlign="Center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblNumSale"></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn SortExpression="sale_date" HeaderText="Дата продажи">
                                                                    <ItemStyle Width="300"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSaleDate" runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "sale_date"), "dd.MM.yyyy") %>'>
                                                                        </asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterStyle HorizontalAlign="Left" Font-Bold="false"></FooterStyle>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn SortExpression="goods_count" HeaderText="Кол-во покупок">
                                                                    <ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label runat="server" ID="lblGoodsCount" Text='<%# DataBinder.Eval(Container.DataItem, "goods_count") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalGoodsCount" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Сумма заказа">
                                                                    <ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCost" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "summa") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    <FooterTemplate>
                                                                        <asp:Label ID="lblTotalCost" runat="server"></asp:Label>
                                                                    </FooterTemplate>
                                                                </asp:TemplateColumn>
                                                                <asp:TemplateColumn HeaderText="Ответственный">
                                                                    <ItemStyle Width="300" HorizontalAlign="center"></ItemStyle>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblWorker" runat="server" Text='<%# DataBinder.Eval(Container.DataItem, "worker") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
                                                            </Columns>
                                                        </asp:DataGrid>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" height="10" align="right">
                                                    </td>
                                                </tr>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <tr>
                                                    <td colspan="5" align="right">
                                                    </td>
                                                </tr>
                                                </table>
                                            </FooterTemplate>
                                        </asp:Repeater>
                                        <table runat="server" border="0" width="100%" id="tblSaleNotExists" cellpadding="2"
                                            cellspacing="1">
                                            <tr>
                                                <td align="center">
                                                    <font size="2" style="font-weight: bold">Заказы или продажи отсутсвуют</font>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="right" valign="top" height="10">
                                        <b>Итого по данному виду рекламы:</b>
                                        <asp:Label ID="lbltotaladvertis2" runat="server"></asp:Label><br>
                                        <br>
                                    </td>
                                </tr>
                            </ItemTemplate>
                            <FooterTemplate>
                                </TABLE>
                            </FooterTemplate>
                        </asp:Repeater>
                    </td>
                    <td width="20">
                    </td>
                </tr>
                <tr>
                    <td width="20">
                    </td>
                    <td align="center" colspan="3">
                        <asp:DataGrid ID="grid" runat="server" Font-Size="10pt" AutoGenerateColumns="False"
                            Width="100%" EnableViewState="False">
                            <HeaderStyle Font-Bold="True" HorizontalAlign="Center"></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="№№">
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblRecordNum" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Источник рекламы">
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "adv_name")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Кол-во клиентов">
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "customer_count")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Количество продаж">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <%#DataBinder.Eval(Container.DataItem, "sale_Count")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Сумма&lt;BR&gt;за период">
                                    <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                    <ItemTemplate>
                                        <%# DataBinder.Eval(Container.DataItem, "summa")%>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid>
                    </td>
                    <td width="20">
                    </td>
                </tr>
            </tbody>
        </table>
    </form>
</body>
</html>

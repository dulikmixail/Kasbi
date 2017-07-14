<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.BookKeeping" CodeFile="BookKeeping.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <title>[Бухгалтерия]</title>

    <script language="javascript">
			function isFind()
			{
				var theform = document.frmCustomerList;
				theform.FindHidden.value = "1";
			}
    </script>

    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmCustomerList" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">&nbsp;Бухгалтерия</td>
            </tr>
        </table>
        <table cellpadding="2" cellspacing="1" width="100%">
            <tr class="Unit">
                <td class="Unit" width="100%">&nbsp;Информация&nbsp;о&nbsp;клиентах
                </td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <asp:Label ID="msgCust" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="2">
                    <p>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFilter" runat="server" BorderWidth="1px" Width="220px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:LinkButton ID="btnFind" runat="server" CssClass="PanelHider" EnableViewState="False">Искать</asp:LinkButton>&nbsp;
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkShowAll" runat="server" CssClass="PanelHider" EnableViewState="False">Показать всех клиентов</asp:LinkButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID ="lnkExportCashКegisters" runat="server" CssClass="PanelHider">Выгрузить кассовые аппараты и ответственных мастеров</asp:LinkButton>
                    &nbsp;&nbsp; &nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td height="20"></td>
            </tr>
            <tr class="Unit">
                <td class="Unit">&nbsp;Результат&nbsp;поиска&nbsp;</td>
            </tr>
            <tr>
                <td align="center" width="100%">
                    <asp:Repeater ID="repCustomers" runat="server">
                        <HeaderTemplate>
                            <table id="Table1" cellspacing="2" cellpadding="1" width="100%" border="0">
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td colspan="2" class="Caption">
                                    <font style="font-weight: bold">Клиент №<%# DataBinder.Eval(Container.DataItem, "customer_sys_id")%></font>
                                </td>
                                <td class="Caption">
                                    <font style="font-weight: bold">
                                        <%# DataBinder.Eval(Container.DataItem, "customer_name")%>
                                    </font>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" width="100%">
                                    <asp:DataGrid ID="grdGoods" runat="server" AutoGenerateColumns="False"
                                         BorderWidth="1px" BorderColor="#CC9966" Width="100%">
                                         <ItemStyle CssClass="itemGrid"></ItemStyle>
                                         <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateColumn HeaderText="№">
                                                <ItemStyle Width="10" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblNumGood"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Дата постановки">
                                                <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# Format(DataBinder.Eval(Container.DataItem, "support_date"), "dd.MM.yyyy") %>'
                                                        ID="Label2">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Товар">
                                                <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'
                                                        ID="Label6">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="№">
                                                <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:HyperLink ID="lbledtNum" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.owner_sys_id")%>'>
                                                    </asp:HyperLink>
                                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                                        <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                                            ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                                        </asp:HyperLink>
                                                        <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                                            ImageUrl="Images/repaired.gif" ToolTip="Побывал в ремонте">
                                                        </asp:HyperLink></p>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Описание">
                                                <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_description") %>'
                                                        ID="Label1">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Доп.инф-ция">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <nobr>
                                                    </nobr>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.info") %>'
                                                        ID="Label3">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Район<br/> установки">
                                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                <ItemTemplate>
                                                    <nobr>
                                                    </nobr>
                                                    <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region") %>'
                                                        ID="Label4">
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="История">
                                                <ItemTemplate>
                                                    <asp:DataGrid ID="grdCashHistory" runat="server" Font-Size="9pt" Width="100%" BackColor="White"
                                                        BorderWidth="1px" BorderColor="#CC9966" AutoGenerateColumns="False" BorderStyle="None"
                                                        CellPadding="4">
                                                        <ItemStyle CssClass="itemGrid" Font-Size="7"></ItemStyle>
                                                        <HeaderStyle Font-Size="7" HorizontalAlign="Center" ForeColor="Black" BackColor="#d3c9c7">
                                                        </HeaderStyle>
                                                        <Columns>
                                                            <asp:TemplateColumn HeaderText="Плательщик ТО">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblPayer" runat="server"></asp:Label>&nbsp;
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                            <asp:TemplateColumn HeaderText="ТО">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
                                                        </Columns>
                                                        <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                                                    </asp:DataGrid>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Последнее ТО">
                                                <ItemStyle Width="50" HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label runat="server" ID="lblLastTO"></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                        </Columns>
                                    </asp:DataGrid>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <table runat="server" border="0" width="100%" id="tblSaleNotExists" cellpadding="2"
                                        cellspacing="1">
                                        <tr>
                                            <td align="center">
                                                <font size="2" style="font-weight: bold">Кассовые аппараты не находятся на ТО</font>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3">
                                    <hr size="1">
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" height="20">
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2"> &nbsp;</td>
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

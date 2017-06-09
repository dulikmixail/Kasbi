<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.Details" CodeFile="Details.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head  runat ="server">
    <title>[Детали и работы]</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Администрирование&nbsp; - &gt;&nbsp;Детали и работы
                </td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td width="100%" colspan="2">
                    <asp:Label ID="msgError" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"></asp:Label></td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2">
                </td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="font-size:12px">
                    <b>Введите стоимость нормы-часа: </b> <asp:TextBox ID="txtNormHour" runat="server" MaxLength="10" ></asp:TextBox> <asp:Button runat="server" Text="Применить" id="btnAddNormHour" /></asp:Button>
                    <br /><br />
                    <asp:DataGrid ID="grdDetails" runat="server" ShowFooter="True" PageSize="100" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" Width="80%" CellPadding="1" BorderColor="#CC9966"
                        BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Name">
                                <HeaderTemplate>
                                    Наименование
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"detail_name")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxNameNew" runat="server" Columns="25" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxNameEdit" runat="server" Columns="25" Text='<%# DataBinder.Eval(Container.DataItem,"detail_name")%>'
                                        BackColor="#F6F8FC" BorderWidth="1px">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Децим.номер/ Обозначение">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"detail_notation")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxDetailNotationNew" runat="server" Columns="25" BackColor="#F6F8FC"
                                        BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxDetailNotationEdit" runat="server" Columns="25" BackColor="#F6F8FC"
                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"detail_notation")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Деталь">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <FooterStyle HorizontalAlign="Center"></FooterStyle>
                                <ItemTemplate>
                                    <%# IIf(DataBinder.Eval(Container.DataItem,"is_detail"), "Да", "Нет")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:CheckBox ID="cbxIsDetailNew" runat="server" BorderWidth="1px"></asp:CheckBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:CheckBox ID="cbxIsDetailEdit" runat="server" BorderWidth="1px" Checked='<%# DataBinder.Eval(Container.DataItem,"is_detail")%>'>
                                    </asp:CheckBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Цена детали">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"price")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxPriceNew" runat="server" Columns="10" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxPriceEdit" runat="server" Columns="10" BackColor="#F6F8FC" BorderWidth="1px"
                                        Text='<%# DataBinder.Eval(Container.DataItem,"price")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Стоимость услуги">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"cost_service")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxCostServiceNew" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxCostServiceEdit" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"cost_service")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Общая стоимость">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"total_sum")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxTotalSumNew" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxTotalSumEdit" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"total_sum")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Норма/час">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"norma_hour")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxNormaHourNew" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxNormaHourEdit" runat="server" Columns="10" BackColor="#F6F8FC"
                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"norma_hour")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <FooterStyle HorizontalAlign="Center" Width="30"></FooterStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ToolTip="Изменить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ToolTip="Удалить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:ImageButton ID="cmdUpdate" runat="server" CommandName="Update" ToolTip="Сохранить"
                                        ImageUrl="../Images/edit_small.gif"></asp:ImageButton>
                                    <asp:ImageButton ID="cmdCancel" runat="server" CommandName="Cancel" ToolTip="Отменить"
                                        ImageUrl="../Images/delete_small.gif"></asp:ImageButton>
                                </EditItemTemplate>
                                <FooterTemplate>
                                    <asp:LinkButton ID="btnAddIMNS" runat="server" CommandName="AddGoodType" Font-Size="8pt">Добавить</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass ="pagerGrid" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td height="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td class="Unit" align="center" colspan="2">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../Images/back.gif" CausesValidation="False">
                    </asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td height="5">
                    &nbsp;</td>
            </tr>
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.Pricelist" EnableViewState="True"
    CodeFile="Pricelist.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Прейскуранты]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmPricelist" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Администрирование&nbsp; -&gt;&nbsp;Прейскуранты</td>
            </tr>
        </table>
        <table width="100%" cellpadding="2" cellspacing="1" border="0">
            <tr class="Unit">
                <td class="Unit" colspan="2">&nbsp;Прейскуранты</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
                <tr>
                    <td style="padding-left: 10px; width: 110px" valign="top">
                        <asp:ListBox ID="lstPricelist" runat="server" AutoPostBack="True" Rows="8"></asp:ListBox>&nbsp;<br>
                            <asp:CheckBox ID="chkEditMode" runat="server" AutoPostBack="True" Text="Режим редактирования"
                                Font-Size="9pt" TextAlign="Left"></asp:CheckBox></td>
                    <td align="left">
                        <asp:Panel ID="pnlEdit" runat="server">
                            <table>
                                <tr>
                                    <td valign="top">
                                        <asp:TextBox ID="txtPricelistName" runat="server" BorderWidth="1px" BorderStyle="Solid"
                                             Width="340px" MaxLength="50"></asp:TextBox>
                                        <br />
                                        <span style="font-size:10px"><b>Коэффициент для изменения цен:</b><br />
                                            <asp:TextBox ID="txtKoefficient" runat="server" BorderWidth="1px" BorderStyle="Solid"
                                            Width="100px" Text="1.000" MaxLength="10"></asp:TextBox>
                                            <asp:Button Text="Изменить" ID="btnKoefficient" runat="server" /></asp:Button>
                                            <br />
                                            <asp:CheckBox ID="chkSavePrice" Text="Сформировать и сохранить прейскурант" runat="server" Font-Size="9pt" TextAlign="right"/>
                                        </span><br />
                                    </td>
                                    <td rowspan="2">
                                        <asp:Calendar ID="calendar" runat="server" ForeColor="#663399" Font-Size="8pt" BorderWidth="1px"
                                            Width="216px" ShowDayHeader="False" FirstDayOfWeek="Monday" DayNameFormat="FirstLetter"
                                            Height="128px" Font-Names="Verdana" BackColor="#FFFFCC" BorderColor="#FFCC66">
                                            <TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>
                                            <SelectorStyle BackColor="#FFCC66"></SelectorStyle>
                                            <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
                                            <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
                                            <SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
                                            <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000">
                                            </TitleStyle>
                                            <WeekendDayStyle BackColor="#F6F8FC"></WeekendDayStyle>
                                            <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
                                        </asp:Calendar>
                                    </td>
                                </tr>
                                <tr>
                                    <td valign="bottom">
                                        <asp:LinkButton ID="btnSave" runat="server" Font-Size="8pt">Сохранить</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnDelete" runat="server" Font-Size="8pt">Удалить</asp:LinkButton>&nbsp;
                                        <asp:LinkButton ID="btnArchived" runat="server" Font-Size="8pt">Отправить в архив</asp:LinkButton>&nbsp;
                                    </td>
                                </tr>
                            </table>
                            <br>
                        </asp:Panel>
                        <asp:DataGrid ID="grdGoods" runat="server" ShowHeader="False" ShowFooter="True" AutoGenerateColumns="False"
                            BorderColor="#CC9966" BorderWidth="1px" >
                            <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                            <Columns>
                                <asp:TemplateColumn HeaderText="Название">
                                    <ItemStyle Width="400px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblGood" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:DropDownList ID="lstAddGood" runat="server" Width="100%">
                                        </asp:DropDownList>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Цена">
                                    <ItemStyle Font-Bold="True" HorizontalAlign="Right" Width="100px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <FooterTemplate>
                                        <asp:TextBox ID="txtAddGoodPrice" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'
                                            BorderStyle="Solid">
                                        </asp:TextBox>
                                    </FooterTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtPrice" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'
                                            BorderStyle="Solid">
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
                                        &nbsp;&nbsp;
                                        <asp:LinkButton ID="btnAddGood" runat="server" CommandName="AddGood" Font-Size="8pt">Добавить</asp:LinkButton>
                                    </FooterTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid></td>
                </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;Формирование нового прейскуранта</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="msgNew" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="center">
                        <tr>
                            <td width="240" rowspan="2" valign="top">
                                <table width="200" border="0" cellpadding="2" cellspacing="1">
                                    <tr>
                                        <td class="SubTitleTextbox">
                                            <span>Название: </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <span class="SubTitleTextbox">
                                                <asp:TextBox ID="txtNewPricelistName" runat="server" Width="220px" BorderWidth="1px"
                                                    BackColor="#F6F8FC" ToolTip="Введите название прайслиста" MaxLength="50"></asp:TextBox>
                                            </span>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="SubTitleTextbox">
                                            Дата:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:Calendar ID="calendarNew" runat="server" ForeColor="#663399" Width="220px" BorderWidth="1px"
                                                BorderColor="#FFCC66" BackColor="#FFFFCC" Font-Size="8pt" Font-Names="Verdana"
                                                Height="168px" DayNameFormat="FirstLetter" ShowGridLines="True" FirstDayOfWeek="Monday">
                                                <TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>
                                                <SelectorStyle BackColor="#FFCC66"></SelectorStyle>
                                                <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
                                                <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
                                                <SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
                                                <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000">
                                                </TitleStyle>
                                                <WeekendDayStyle BackColor="#F6F8FC"></WeekendDayStyle>
                                                <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
                                            </asp:Calendar>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                            <td class="SubTitleTextbox">
                                Товары:</td>
                        </tr>
                        <tr>
                            <td style="padding-left: 10px" valign="top">
                                <asp:DataGrid ID="grdGoodsNew" runat="server" BorderStyle="None" BorderColor="#CC9966"
                                    BorderWidth="1px"  CellPadding="4" AutoGenerateColumns="False">
                                   <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                                    <ItemStyle CssClass="itemGrid"></ItemStyle>
                                    <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                    <FooterStyle CssClass="footerGrid"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Наименование товара">
                                               <ItemTemplate>
                                                <asp:CheckBox ID="chkGoodType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                                </asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Стоимость">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPriceNew" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                                    Width="100%" MaxLength="10"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle CssClass="pagerGrid"></PagerStyle>
                                </asp:DataGrid></td>
                            <tr>
                                <td colspan="2">
                                    &nbsp;</td>
                            </tr>
                        <tr>
                            <td align="right" colspan="2">
                                <asp:ImageButton ID="btnSavePricelist" runat="server" CommandName="Update" ImageUrl="../Images/update.gif">
                                </asp:ImageButton></td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                &nbsp;</td>
                        </tr>
                    </table>
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

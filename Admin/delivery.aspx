<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.Delivery" CodeFile="Delivery.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <title>[Поставки]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmDelivery" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp; Администрирование -&gt;&nbsp;Поставки</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" width="193" style="width: 193px; height: 15px">
                    &nbsp;Поставки</td>
                <td class="Unit" align="right" style="height: 15px">
                    <asp:HyperLink ID="btnNew" runat="server" NavigateUrl="SupplierDetail.aspx?SupplierDetailID=-9999"
                        CssClass="PanelHider" EnableViewState="False">Новый&nbsp;поставщик</asp:HyperLink></td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                            <td style="padding-left: 10px; width: 193px" valign="top" align="center">
                    <asp:ListBox ID="lstDelivery" runat="server" DataValueField="delivery_sys_id" DataTextField="info2"
                        DataTextFormatString="{0:dd/MM/yyyy}" Rows="5" Width="800" AutoPostBack="True">
                    </asp:ListBox>&nbsp;<br>
                    <div align="center">
                        <asp:CheckBox ID="chkEditMode" runat="server" Width="200px" AutoPostBack="True" Text="Режим редактирования"
                            Font-Size="9pt" TextAlign="Left"></asp:CheckBox></div><br><br>
                </td>
            </tr>
            <tr>

                <td align="left">
                    <asp:Panel ID="pnlEdit" runat="server" Width="100%">
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <asp:TextBox ID="txtDeliveryInfo" runat="server" Width="410px" MaxLength="250" 
                                        BorderStyle="Solid" BorderWidth="1px"></asp:TextBox></td>
                                <td rowspan="2">
                                    <asp:Calendar ID="calendar" runat="server" ForeColor="#663399" Width="216px" Font-Size="8pt"
                                        BorderWidth="1px" BorderColor="#FFCC66" BackColor="#FFFFCC" Font-Names="Verdana"
                                        Height="128px" DayNameFormat="FirstLetter" FirstDayOfWeek="Monday" ShowDayHeader="False">
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
                                    <asp:LinkButton ID="btnDelete" runat="server" Font-Size="8pt">Удалить поставку</asp:LinkButton>&nbsp;
                                </td>
                            </tr>
                        </table>
                        <br>
                    </asp:Panel>
                    <p style="margin-bottom: 10px">
                        <asp:Label ID="lblDeliveryInfo" runat="server" Font-Size="9pt" Font-Names="Verdana"></asp:Label></p>
                    <asp:DataGrid ID="grdGoods" runat="server" DataKeyField="good_type_sys_id" BorderColor="#CC9966"
                        BorderWidth="1px" ShowFooter="True" AutoGenerateColumns="False" Width="100%">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                             <asp:TemplateColumn HeaderText="Артикул">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblArtikul" runat="server" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.artikul") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtArtikul" runat="server" BorderWidth="1px" Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.artikul") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtArtikul" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.artikul") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>                      
                            <asp:TemplateColumn HeaderText="Название">
                                <ItemStyle Width="400"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblGood" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="lstAddGood" runat="server" Width="400">
                                    </asp:DropDownList>
                                    <hr width="100%" size="1">
                                    <asp:TextBox ID="tbxNewGood" runat="server" Width="100%" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                    <asp:CheckBox ID="chkNewCashRegister" runat="server" Text="ККМ"></asp:CheckBox>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Цена">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
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
                            <asp:TemplateColumn HeaderText="Цена (RUR)">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRur" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.rur") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddGoodRur" runat="server" BorderWidth="1px" Width="100%" Text='0'
                                        BorderStyle="Solid"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRur" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.rur") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Кол-во">
                                <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddGoodQuantity" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtQuantity" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Ед. Изм.">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblUnit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.UnitDesciption") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="lstNewUnits" Width="40pt" runat="server">
                                    </asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="lstUnits" Width="40pt" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Доп. инф. (№ ТТН)">
                                <ItemStyle HorizontalAlign="Center" Width="100pt"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.info") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtAddGoodInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.info") %>'
                                        BorderWidth="1px" BorderStyle="Solid" Width="100%" MaxLength="250">
                                    </asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtInfo" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.info") %>'
                                        BorderWidth="1px" BorderStyle="Solid" Width="100%" MaxLength="250">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Поставщики" ItemStyle-Width="200">
                                <ItemTemplate>
                                    <asp:Label ID="lblSupplier" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.supplier_abr")& " " & DataBinder.Eval(Container, "DataItem.supplier_name") %>'></asp:Label>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:DropDownList ID="lstSupplier" runat="server" Width="200"> </asp:DropDownList>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="lstSupplierEdit" runat="server" Width="200"> </asp:DropDownList>
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
                                    <asp:LinkButton ID="btnAddGood" runat="server" CommandName="AddGood">Добавить</asp:LinkButton>
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
                    &nbsp;Новая поставка</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="msgNew" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td width="100%" style="padding-left: width: 193px; height: 178px" valign="top" align="center">
                                        <span class="SubTitleTextbox">Название поставки:</span>
                    <br>
                    <asp:TextBox ID="txtNewInfo" runat="server" Width="424px" MaxLength="250" BorderWidth="1px"
                        BackColor="#F6F8FC" ToolTip="Введите название прайс-листа"></asp:TextBox>
                        
                        
<br><br>
                    <span class="SubTitleTextbox">Дата поставки:</span>
                    <br>
                    <asp:Calendar ID="calendarNew" runat="server" ForeColor="#663399" Width="153px" Font-Size="8pt"
                        BorderWidth="1px" BorderColor="#FFCC66" BackColor="#FFFFCC" Font-Names="Verdana"
                        Height="144px" DayNameFormat="FirstLetter" FirstDayOfWeek="Monday" ShowGridLines="True">
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
                    
                    <br /><br />
                    <asp:ImageButton ID="btnSaveDelivery" runat="server" CommandName="Update" ImageUrl="../Images/update.gif">
                           </asp:ImageButton>
                </td>
                <td valign="top" style="height: 178px">
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <table align="center" width="100%">
                        <tr>
                            <td style="padding-left: 10px" valign="top">
                                <asp:DataGrid ID="grdGoodsNew" runat="server" BorderStyle="None" BorderWidth="1px"
                                    BorderColor="#CC9966" BackColor="White" DataKeyField="good_type_sys_id" AutoGenerateColumns="False"
                                    CellPadding="4" Width="100%">
                                    <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                                    <ItemStyle CssClass="itemGrid"></ItemStyle>
                                    <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                    <FooterStyle CssClass="footerGrid"></FooterStyle>
                                    <Columns>
                                        <asp:TemplateColumn HeaderText="Наименование товара">
                                            <HeaderStyle Width="165pt"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:CheckBox ID="chkGoodType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                                </asp:CheckBox><br>
                                                <asp:TextBox ID="txtNewGoodType" runat="server" Width="200px" BorderWidth="1px" MaxLength="250"
                                                    BackColor="#F6F8FC" ToolTip="Введите название нового типа товара" Visible="False"></asp:TextBox><br>
                                                <asp:CheckBox ID="chkNewCashReg" runat="server" Text="ККМ" Visible="False" Checked="False">
                                                </asp:CheckBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Цена">
                                            <HeaderStyle Width="65pt"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPriceNew" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                                    Width="100%" MaxLength="10"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Цена (RUR)">
                                            <HeaderStyle Width="65pt"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtPriceRurNew" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                                    Width="100%" MaxLength="10"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Кол-во">
                                            <HeaderStyle Width="40pt"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtQuantityNew" runat="server" Width="100%" BorderWidth="1px" MaxLength="10"
                                                    BackColor="#F6F8FC"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Ед. Изм.">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="lstUnitsNew" runat="server">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Доп. инф. (№ ТТН)">
                                            <ItemTemplate>
                                                <asp:TextBox ID="txtInfoNew" runat="server" BorderWidth="1px" Width="100%" BackColor="#F6F8FC"
                                                    MaxLength="250"></asp:TextBox>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="Поставщик">
                                            <HeaderStyle Width="20%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:DropDownList ID="lstSupplierNew" runat="server" Width="100%">
                                                </asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle CssClass ="pagerGrid"></PagerStyle>
                                </asp:DataGrid></td>
                            <tr>
                                <td align="right" colspan="2">
                                    </td>
                            </tr>
                        <tr>
                            <td colspan="2">&nbsp;</td>
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

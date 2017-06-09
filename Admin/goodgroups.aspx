<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.GoodGroups"
    CodeFile="GoodGroups.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head  runat ="server">
    <title>[Группы товаров]</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="frmGoodGoups" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Администрирование&nbsp; -&gt;&nbsp;Группы товаров</td>
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
                <td align="center" colspan="2">
                    <asp:DataGrid ID="grdGoodGroups" runat="server" ShowFooter="True" PageSize="100" AllowPaging="True"
                        AllowSorting="True" AutoGenerateColumns="False" Width="80%" CellPadding="1" BorderColor="#CC9966"
                        BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Name">
                                <HeaderTemplate>
                                    Наименование
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"Name")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxNameNew" runat="server" Columns="45" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxNameEdit" runat="server" Columns="45" Text='<%# DataBinder.Eval(Container.DataItem,"name")%>'
                                        BackColor="#F6F8FC" BorderWidth="1px">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Описание">
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container.DataItem,"description")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="tbxDescriptionNew" runat="server" Columns="45" BackColor="#F6F8FC"
                                        BorderWidth="1px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="tbxDescriptionEdit" runat="server" Columns="45" BackColor="#F6F8FC"
                                        BorderWidth="1px" Text='<%# DataBinder.Eval(Container.DataItem,"description")%>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
							<asp:TemplateColumn HeaderText="Выводить в накладной" HeaderStyle-ForeColor="White" SortExpression="nadbavka">
									<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
									<ItemStyle HorizontalAlign="Center"></ItemStyle>
									<FooterStyle HorizontalAlign="Center"></FooterStyle>
									<ItemTemplate>
										<%# IIf(DataBinder.Eval(Container.DataItem,"is_show"), "Да", "Нет")%>
									</ItemTemplate>
									<FooterTemplate>
										<asp:CheckBox id="cbxIs_showNew" runat="server" BorderWidth="1px"></asp:CheckBox>
									</FooterTemplate>
									<EditItemTemplate>
										<asp:CheckBox id="cbxIs_showEdit" runat="server" BorderWidth="1px" Checked='<%# DataBinder.Eval(Container.DataItem,"is_show")%>'>
										</asp:CheckBox>
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
                                    <asp:LinkButton ID="btnAddGoodGroup" runat="server" CommandName="AddGoodGroup" Font-Size="8pt">Добавить</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
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

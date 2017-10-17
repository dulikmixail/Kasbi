<%@ Page Language="VB" AutoEventWireup="false" CodeFile="BankList.aspx.vb" Inherits="Kasbi.Admin.BankList" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat ="server">
    <title>[Информация о банках]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
    <meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="../Styles.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Администрирование -&gt; Банки</td>
            </tr>
            <tr>
                <td height="10">
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
            </tr>
            <tr>
                <td class="SectionRow" height="10" align="center">
                    &nbsp;
                    <table id="Table1" cellspacing="1" cellpadding="2" border="0">
                        <tr>
                            <td class="TitleTextbox">
                                Код банка:</td>
                            <td>
                                <asp:TextBox ID="txtFilterByCode" runat="server" MaxLength="11" BackColor="#F6F8FC"
                                    BorderWidth="1px"></asp:TextBox></td>
                            <td class="TitleTextbox">
                                Название банка:</td>
                            <td>
                                <asp:TextBox ID="txtFilterByName" runat="server" Width="250px" BackColor="#F6F8FC"
                                    BorderWidth="1px"></asp:TextBox></td>
                            <td>
                                <asp:LinkButton ID="lnkShow" runat="server" Font-Size="8pt">Показать</asp:LinkButton></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td height="10">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DataGrid ID="grdBankList" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                         Width="100%" PageSize="50" AllowPaging="True"  BorderColor="#CC9966" BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Код банка">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.bank_code") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtBankCodeNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankCodeEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11" Text='<%# DataBinder.Eval(Container, "DataItem.bank_code") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Наименование">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.name") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtBankNameNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="350px" TextMode="MultiLine" Height="50"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankNameEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="350px" TextMode="MultiLine" Height="50" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Адрес банка">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.address") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtBankAddressNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="250px" TextMode="MultiLine" Height="50"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtBankAddressEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="250px" TextMode="MultiLine" Height="50" Text='<%# DataBinder.Eval(Container, "DataItem.address") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="МФО / УНП">
                                <ItemTemplate>
                                    МФО:<%# DataBinder.Eval(Container, "DataItem.mfo") %><br />
                                    УНП:&nbsp;<%#DataBinder.Eval(Container, "DataItem.unn")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    МФО:<asp:TextBox ID="txtBankMFONew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11"></asp:TextBox><br />
                                    УНП:&nbsp;<asp:TextBox ID="txtBankUNNNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    МФО:<asp:TextBox ID="txtBankMFOEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11" Text='<%# DataBinder.Eval(Container, "DataItem.mfo") %>'></asp:TextBox><br />
                                    УНП:&nbsp;<asp:TextBox ID="txtBankUNNEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="120px" MaxLength="11" Text='<%# DataBinder.Eval(Container, "DataItem.unn") %>'></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Тел./факс">
                                <ItemTemplate>
                                    Тел.:&nbsp;<%# DataBinder.Eval(Container, "DataItem.phone") %>
                                    <br />
                                    Факс:<%# DataBinder.Eval(Container, "DataItem.fax") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    Тел.:&nbsp;<asp:TextBox ID="txtBankPhoneNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="100px"></asp:TextBox><br />
                                    Факс:<asp:TextBox ID="txtBankFaxNew" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="100px"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    Тел.:&nbsp;<asp:TextBox ID="txtBankPhoneEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.phone") %>'></asp:TextBox><br />
                                    Факс:<asp:TextBox ID="txtBankFaxEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Width="100px" Text='<%# DataBinder.Eval(Container, "DataItem.fax") %>'></asp:TextBox>
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
                                    <asp:LinkButton ID="btnAdd" runat="server" Font-Size="8pt" CommandName="AddBank">Добавить</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                </td>
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

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.PriceTO" CodeFile="PriceTO.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Единицы измерения]</title>
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
                    &nbsp;Администрирование -&gt; Стоимость технического обслуживания</td>
            </tr>
            <tr>
                <td height="10">
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
            </tr>
            <tr>
                <td height="10">
                </td>
            </tr>
            <tr>
                <td align="center">
                    <asp:DataGrid ID="grdPriceTO" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                        Width="75%" PageSize="100" AllowPaging="True"  BorderColor="#CC9966" BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Наименование">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container, "DataItem.priceto_name")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtName" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtNameEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.priceto_name") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Стоимость">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.priceto_value") %>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtValue" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtValueEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.priceto_value") %>'>
                                        
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Район" HeaderStyle-Width="200">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.place_name") %>
                                </ItemTemplate>
                                <FooterTemplate>
										<asp:DropDownList ID="lstPlaceRn" runat="server" Width="200"> </asp:DropDownList>
								</FooterTemplate>
								<EditItemTemplate>
									  
                                    <asp:DropDownList ID="lstPlaceRnEdit" runat="server" Width="200"> </asp:DropDownList>
                              
								</EditItemTemplate>
                            </asp:TemplateColumn>   
                            <asp:TemplateColumn HeaderText="Группа" ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container, "DataItem.group_id")%>
                                </ItemTemplate>
                                <FooterTemplate>
                                    <asp:TextBox ID="txtGroup" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                </FooterTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtGroupEdit" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.group_id") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>                                                    
                            <asp:TemplateColumn>
                                <FooterStyle HorizontalAlign="Center" Width=30></FooterStyle>
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
                                    <asp:LinkButton ID="btnAdd" runat="server" Font-Size="8pt" CommandName="AddUnit">Добавить</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass ="pagerGrid" Mode="NumericPages"></PagerStyle>
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

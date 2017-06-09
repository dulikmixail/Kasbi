<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.NewGoodTO" CodeFile="NewGoodTO.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Новый товар]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmNewRequest" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Товары -&gt; Новый товар</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0" style="font-size:12">
            <tr class="Unit">
                <td class="Unit" colspan="5">
                    &nbsp;Информация о клиенте и заказе
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label>
                    <asp:LinkButton ID="lnk_nav_back" runat="server">&nbsp;&nbsp;<< Вернуться к заказам</asp:LinkButton><br /><br />
                     <table style="font-size:12" width="100%">
                                <tr>
                                    <td valign="top" class="SectionRow" colspan="8">
                                        <asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
                               
                                    <td valign="top" width="500" class="SectionRow" colspan="8">
                                                       
                                                        <asp:datagrid id="grdGoods" runat="server" Font-Size="8pt" BorderColor="#CC9966" AutoGenerateColumns="False" ShowFooter="True"
																Width="100%">
																<HeaderStyle Font-Size="10pt" Font-Bold="true" Font-Underline="True" HorizontalAlign="Center" ForeColor="#9C0001"></HeaderStyle>
																<FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
																<Columns>

																	<asp:TemplateColumn SortExpression="good_name" HeaderStyle-HorizontalAlign="Left" HeaderText="Товар">
																		<ItemStyle Width="300"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label id="lblGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
																			</asp:Label>
																		</ItemTemplate>
																		<FooterStyle HorizontalAlign="Left" Font-Bold="false"></FooterStyle>
																		<FooterTemplate>
																			<asp:Label id="lblTotalCountByCash" runat="server"></asp:Label>
																		</FooterTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn HeaderText="Цена" ItemStyle-HorizontalAlign="Left">
																		<ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>' ID="Label3" >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																	<asp:TemplateColumn SortExpression="quantity" HeaderText="Кол-во">
																		<ItemStyle Width="100" HorizontalAlign="Right"></ItemStyle>
																		<ItemTemplate>
																			<asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>' ID="Label4" >
																			</asp:Label>
																		</ItemTemplate>
																	</asp:TemplateColumn>
																</Columns>
															</asp:datagrid>                                   
                                    
                                    </td>
                               </tr> 
                    </table>                   
                    
                    <br /><br />
                </td>
                <td>
                
                
                </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="5">
                    &nbsp;Добавленное оборудование</td>
            </tr>
            <tr>
                <td colspan="5">


                    <asp:DataGrid ID="grdTO" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                        Width="100%" PageSize="100" AllowPaging="True" BorderColor="#CC9966" BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>

                        <asp:TemplateColumn SortExpression="quantity" HeaderText="Тим товара">
	                        <ItemStyle Width="25%" HorizontalAlign="Right"></ItemStyle>
	                        <ItemTemplate>
		                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>' ID="Label1" >
		                        </asp:Label>
	                        </ItemTemplate>
                            <FooterTemplate>
                                <asp:DropDownList ID="lstAddGood" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                    Width="400px"></asp:DropDownList>
                            </FooterTemplate>                            
                            <EditItemTemplate>
                                <asp:DropDownList ID="lstAddGood" runat="server" Width="400">
                                </asp:DropDownList>
                            </EditItemTemplate> 
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="quantity" HeaderText="Серийный номер">
	                        <ItemStyle Width="25%" HorizontalAlign="Right"></ItemStyle>
	                        <ItemTemplate>
		                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_num") %>' ID="Label2" >
		                        </asp:Label>
	                        </ItemTemplate>
                            <FooterTemplate>
                                <asp:TextBox ID="txtgood_num" MaxLength="12" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                    Width="150px" Text=''>
                                </asp:TextBox>
                            </FooterTemplate>	                        
                            <EditItemTemplate>
                                <asp:TextBox ID="txtgood_num_edit" MaxLength="12" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                    Width="150px" Text='<%# DataBinder.Eval(Container, "DataItem.good_num") %>'>
                                </asp:TextBox>
                            </EditItemTemplate>	                        
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="quantity" HeaderText="Клиент">
	                        <ItemStyle Width="25%" HorizontalAlign="Right"></ItemStyle>
	                        <ItemTemplate>
		                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customer_name") %>' ID="Label3" >
		                        </asp:Label>
	                        </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="quantity" HeaderText="Дата добавления">
	                        <ItemStyle Width="25%" HorizontalAlign="Right"></ItemStyle>
	                        <ItemTemplate>
		                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.add_date") %>' ID="Label4" >
		                        </asp:Label>
	                        </ItemTemplate>
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
                                    <asp:LinkButton ID="btnAdd" runat="server" Font-Size="8pt" CommandName="AddGoodTO">Добавить</asp:LinkButton>
                                </FooterTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass="pagerGrid" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid>
                  
                    <br /><br />
                </td>
            </tr>
            
            <tr>
                <td colspan="5" height="20">
                </td>
            </tr>
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input runat="server" id="scrollPos" type="hidden" value="0" name="scrollPos">
        <input runat="server" id="CurrentPage" type="hidden" lang="ru" name="CurrentPage">
        <input runat="server" id="Parameters" type="hidden" lang="ru" name="Parameters">
        <input runat="server" id="FindHidden" type="hidden" name="FindHidden">
    </form>
</body>
</html>

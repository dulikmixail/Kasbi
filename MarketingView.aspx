<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.MarketingView" CodeFile="MarketingView.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat ="server">
    <title>[Клиенты]</title>

    <script language="javascript">
<!--
function isFind()
	{
		var theform = document.frmMarketingList;
		theform.FindHidden.value = "1";
	}
-->
    </script>

    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0" >
    <form id="frmCustomerList" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="PageTitle">
            <tr>
                <td class="HeaderTitle" width="100%">Маркетинг - раздел по работе с клиентами</td>
            </tr>
        </table>
        <table cellpadding="2" cellspacing="1" width="100%" style="font-size:12px">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Информация&nbsp;по&nbsp;работе с клиентами</td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <asp:Label ID="msgCust" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="2">
                        <table width="100%" style="font-size:12px;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td valign="top" style="padding:10px;" rowspan="2">
                                
                                    <b>Клиент&nbsp;<asp:label id="custUID" runat="server" ></asp:label>:&nbsp;</b><br />
                                    <asp:label id="msgClientInfo" runat="server" ForeColor="#000000" EnableViewState="false" CssClass="PanelHider"></asp:label>
                                    <br />
                                    <b>Сводная информация:</b><br />
                                    <br />
                                    Рекламный источник: <asp:Label ID="Advertise" runat="server" EnableViewState="false" ForeColor="#000000"></asp:Label><br />
                                    Добавил менеджер: <asp:Label ID="Manager" runat="server" EnableViewState="false" ForeColor="#000000"></asp:Label><br />
                                    Покупок по базе: <asp:Label ID="num_sales" runat="server" EnableViewState="false" ForeColor="#000000"></asp:Label><br />
                                    Покупок на сумму: <asp:Label ID="summ_sales" runat="server" EnableViewState="false" ForeColor="#000000"></asp:Label>
                                </td>
                                <td width="30%" valign="top" style="background-color:#F6F5F6">
                                    <div align="center" style="color:#666666"><b>Деятельность:</b></div>
                                    <br />
                                    <asp:DataGrid ID="grdActivity" runat="server" Width="98%" AutoGenerateColumns="False"
                                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="center" BackColor="#777777" Font-Size="12px" ForeColor="#F6F5F6" Font-Bold="true"></HeaderStyle>
                                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>  
                                            <asp:TemplateColumn HeaderText="Рубрики">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                                                                                                                                                                                                                                                                                                                                                                                             
                                        </Columns>
                                    </asp:DataGrid>                                                                      
                                </td>
                                <td width="30%" valign="top" style="background-color:#F4F3F4">
                                    <div align="center" style="color:#666666"><b>Знаменательные даты:</b></div>
                                    <br />
                                    <asp:DataGrid ID="grdDates" runat="server" Width="98%" AutoGenerateColumns="False"
                                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="center" BackColor="#888888" Font-Size="12px" ForeColor="#F6F5F6" Font-Bold="true"></HeaderStyle>
                                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="customer_sys_id"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Дата">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkId" runat="server" Text='<%#Format(DataBinder.Eval(Container.DataItem, "date"), "dd.MM.yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn> 
                                            <asp:TemplateColumn HeaderText="Событие" ItemStyle-Width="100%">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.description") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                                                                                                                                                                                                                                                                                                                                                  
                                        </Columns>
                                    </asp:DataGrid>                                    
                                </td>
                       </tr>
                       <tr>
                                <td valign="top" style="background-color:#F4F3F4">
                                    <br />
                                    <div align="center" style="color:#666666"><b>Интересы:</b></div>
                                    <br />
                                    <asp:DataGrid ID="grdInterests" runat="server" Width="98%" AutoGenerateColumns="False"
                                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="center" BackColor="#777777" Font-Size="12px" ForeColor="#F6F5F6" Font-Bold="true"></HeaderStyle>
                                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="id"></asp:BoundColumn>  
                                            <asp:TemplateColumn HeaderText="Рубрики">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblActivityName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.name") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                                                                                                                                                                                                                                                                                                                                                                                             
                                        </Columns>
                                    </asp:DataGrid>  
                                </td>
                                <td valign="top" style="background-color:#F4F3F4">
                                    <br />
                                    <div align="center" style="color:#666666"><b>Заметки:</b></div>
                                    <br />
                                    <asp:DataGrid ID="grdNotes" runat="server" Width="98%" AutoGenerateColumns="False"
                                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                                        <HeaderStyle HorizontalAlign="center" BackColor="#999999" Font-Size="12px" ForeColor="#F6F5F6" Font-Bold="true"></HeaderStyle>
                                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                                        <Columns>
                                            <asp:BoundColumn Visible="False" DataField="customer_sys_id"></asp:BoundColumn>
                                            <asp:TemplateColumn HeaderText="Дата">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkId" runat="server" Text='<%#Format(DataBinder.Eval(Container.DataItem, "date"), "dd.MM.yyyy")%>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>
                                            <asp:TemplateColumn HeaderText="Замечание" ItemStyle-Width="100%">
                                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lnkId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.note") %>'>
                                                    </asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateColumn>                                                                                                                                                                                                                                                                                                                                                                  
                                        </Columns>
                                    </asp:DataGrid>    
                                </td>                                                                                                                                
                            </tr>
                        </table>
                        <br />
                        <div align="right"><asp:HyperLink Font-Bold="true" ForeColor="green" ID="lnkEdit" runat="server" NavigateUrl="~/default.aspx">Редактировать</asp:HyperLink></div>
                        
                        <asp:Panel
                                ID="pnlFilter" Style="border-top: #cc9933 1px solid; margin-top: 10px; z-index: 103;
                                margin-bottom: -8px; border-bottom: #cc9933 1px solid" runat="server">
                        </asp:Panel>
                        
                        <br />
                        <b>История клиента:</b>
                        <br /><br />
                        
                    <asp:DataGrid ID="grdHistory" runat="server" Width="98%" AutoGenerateColumns="False"
                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="customer_sys_id"></asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.id") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="Цель:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.purpose") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>      
                             <asp:TemplateColumn SortExpression="dogovor" HeaderText="Обращение:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.text") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>     
                             <asp:TemplateColumn SortExpression="dogovor" HeaderText="Ответ:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.answer") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>  
                              <asp:TemplateColumn SortExpression="dogovor" HeaderText="Вывод:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.conclusion") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>   
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="Дата:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%#Format(DataBinder.Eval(Container.DataItem, "date"), "dd.MM.yyyy")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                             <asp:TemplateColumn SortExpression="dogovor" HeaderText="Менеджер:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.manager") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>                                                                                                                                                                                                                                                                                                                       
                        </Columns>
                    </asp:DataGrid>
                                            
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" colspan="2">
                    </td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2">
                    &nbsp;</td>
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

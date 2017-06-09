<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.MakeInvoice" Culture="ru-Ru"
    CodeFile="MakeInvoice.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server" >
    <title>[Главная]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>

</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmMain" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Формирование счета</td>
            </tr>
        </table>
        <asp:Label ID="lblLinks" runat="server">

        </asp:Label>
        <table  cellspacing="0" cellpadding="0" width="100%" >
            <tr>
                <td valign="top">
                    <asp:Label ID="lblCash" runat="server">
                    
                    </asp:Label>
                </td>
            </tr>
            <tr height ="18">
                <td class="Unit" width="100%">
                    &nbsp;Создание нового счета для клиента</td>
            </tr>
            <tr>
                <td width="100%">
                    <asp:Label ID="msgGoodInfo" runat="server" Font-Bold="true" ForeColor="#ff0000"></asp:Label></td>            
            
            </tr>
            
            <tr>
                <td>
                
                
                <table>
                <tr>
                    <td width="50%">
                                 <table id="pnlSupport" width="500" runat="server">
                                    <tr class="TitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            Плательщик: </td>
                                        <td width="80%" class="SectionRow" align="left" colspan="2">
                                            <asp:TextBox ID="txtCustomerFind" runat="server" BackColor="#F6F8FC" Width="90%"
                                                MaxLength="11" BorderWidth="1px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">Найти</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="SubTitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            &nbsp;</td>
                                        <td class="SectionRow" align="left" colspan="2">
                                            <asp:ListBox ID="lstCustomers" runat="server" Width="100%" AutoPostBack="True"></asp:ListBox></td>
                                    </tr>
                                    <tr>
                                        <td class="SectionRow">
                                            &nbsp;</td>
                                        <td class="SectionRow" colspan="2">
                                            &nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
                                    </tr>
                                    </table>                   
                    </td>
                    <td width="50%">
                    
                    </td>
                </tr>
                </table>            
                                    
                                    
                                    
                    <table id="table" width="90%" align="center">
                        <tr>
                            <td class="SectionRowLabel" align="right">
                                Группы товаров:</td>
                            <td class="SectionRow">
                                <asp:DropDownList ID="lstGoodGroup" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                    Width="350px" AutoPostBack="True">
                                </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="2">
                
                
                    <asp:Repeater ID="repGoodRest" runat="server">
                        <HeaderTemplate>
                            <table align="center" id="Table4" cellspacing="0" cellpadding="0" width="90%" border=".1">
                                <tr class="headerGrid">
                                    <td width="10%">Артикул</td>
                                    <td width="25%">Наименование</td>
                                     <td width="25%">Описание</td>                                   
                                    <td width="15%">Остаток на складе</td>
                                    <td width="20%">Цена (Цена с НДС)</td>
                             
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td colspan="5" class = "itemGrid" style="color: #ffffcc; background-color: gray;" align ="center">
                                    <asp:Label ID="lblGoodGroupName" runat="server"> </asp:Label></td>
                            </tr>
                            <tr class = "itemGrid" align="center">
                            <td align="center">
                                  <%#DataBinder.Eval(Container.DataItem, "artikul")%>&nbsp;
                                </td>
                                <td align="left">
                                   <%# DataBinder.Eval(Container.DataItem, "name") %>
                                </td>
                                <td align="left">
                                   <%# DataBinder.Eval(Container.DataItem, "description") %>
                                </td>                                
                                <td  align="left">
                                <asp:TextBox ID="txtcount" Width="50" Font-Size="12px" BorderWidth="0" BackColor="Gainsboro" runat="server"></asp:TextBox>
                                <b><%# DataBinder.Eval(Container.DataItem, "available") %></b>
                                
                                </td>
                                <td>
                                    
                                    <asp:Label ID="lblGoodPrice" runat="server" Font-Bold="True"></asp:Label></td>                                  
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            </table>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
                
            </tr>
            <tr>
                <td class="Unit" align="center" colspan="2">
                    <asp:ImageButton ID="btnBack" runat="server" ImageUrl="../Images/add.gif" CausesValidation="False">
                    </asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <!-- <tr class=Caption align=center>
    <td><asp:label id=lblGoodInfo Font-Bold="true" Runat="server"></asp:label></td></tr>-->
            <tr>
                <td>
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

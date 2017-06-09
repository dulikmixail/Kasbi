<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.SetRepair" Culture="ru-Ru"
    CodeFile="SetRepair.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat ="server" >
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
                    &nbsp;Принятие ККМ в ремонт</td>
            </tr>
        </table>
        
        <br />
        <div align="center"><asp:Label ID="lblCash" runat="server" Font-Size="12"></asp:Label></div>
        <br />
        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Выберите неисправности:<br />
        <br />
        
       <table width="100%" style="font-size:12">
       <tr>
       <td align="center">

            <asp:DataGrid ID="grdRepairBads" runat="server" ShowFooter="True" AutoGenerateColumns="False"
                        Width="75%" PageSize="14" AllowPaging="True"  BorderColor="#CC9966" BorderWidth="1px">
                        <AlternatingItemStyle CssClass="alternativeitemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Выбрать">
                                <HeaderTemplate>
                                    Выбрать<br>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Наименование">
                                <ItemTemplate>
                                    <%#DataBinder.Eval(Container, "DataItem.name")%>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Стоимость ремонта">
                                <ItemTemplate>
                                    <%# DataBinder.Eval(Container, "DataItem.price") %>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle CssClass ="pagerGrid" Mode="NumericPages"></PagerStyle>
                    </asp:DataGrid> 
                    
                    <br />
                    
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:Button ID="btnSetRepair" Text="Принять в ремонт" runat="server" />
                              
       
       </td>
       </tr>
       </table>
       
       
       
              
       
       
        
        <script language="javascript">
            function generate_link(com){
                if (com==1)
                   window.open("GoodList.aspx?numcashregister=" + document.getElementById("search_kkm").value + "&action=" + document.getElementById("action_kkm").selectedIndex, "_self")
                else if (com==2)
                   window.open("CustomerList.aspx?customer=" + document.getElementById("search_cust").value + "&action=" + document.getElementById("action_cust").selectedIndex, "_self")                
                }
        </script>
        
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>

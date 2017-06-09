<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Marketing" CodeFile="Marketing.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <script language="JavaScript" src="../scripts/datepicker.js"></script>

    <title>[Клиенты]</title>

    <script language="javascript">
<!--
function isFind()
	{
		var theform = document.frmCustomerList;
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
                <td class="SectionRow" align="left">
                        
                        
                                <table id="pnlSupport" width="100%" runat="server">
                                    <tr class="TitleTextbox">
                                        <td width="550" class="SectionRow" align="left" colspan="2">
                                            <asp:TextBox ID="txtCustomerFind" runat="server" BackColor="#F6F8FC" Width="500"
                                                MaxLength="20" BorderWidth="1px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">Найти</asp:LinkButton>
                                        </td>
                                        <td>
                                            
                                        </td>
                                    </tr>
                                    <tr class="SubTitleTextbox">
                                        <td width="510" class="SectionRow" align="left">
                                            <asp:ListBox ID="lstCustomers" runat="server" Width="500" Height="150" AutoPostBack="True"></asp:ListBox></td>
                                        <td align="left" valign="top">
                                            <br />
                                            <table class="SubTitleTextbox">
                                                <tr>
                                                    <td>Телефон:</td>
                                                    <td><asp:TextBox ID="TextBox1" runat="server" width="300" BackColor="#F6F8FC" MaxLength="50" BorderWidth="1px"></asp:TextBox></td>
                                                </tr>
                                                 <tr>
                                                    <td>Контактное лицо:</td>
                                                    <td><asp:TextBox ID="TextBox4" runat="server" width="300" BackColor="#F6F8FC" MaxLength="50" BorderWidth="1px"></asp:TextBox></td>
                                                </tr>                                               
                                                <tr>
                                                    <td>Адрес:</td>
                                                    <td><asp:TextBox ID="TextBox2" runat="server" width="300" BackColor="#F6F8FC" MaxLength="50" BorderWidth="1px"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td>Цель обращения:</td>
                                                    <td><asp:TextBox ID="TextBox3" runat="server" width="300" BackColor="#F6F8FC" MaxLength="50" BorderWidth="1px" Rows="4" TextMode="MultiLine"></asp:TextBox></td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td></td>
                                                </tr>                                                                                                                                            
                                            </table>
                                            
                                            
                                            <br />
                                        </td>                                    
                                    </tr>
                                </table>

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

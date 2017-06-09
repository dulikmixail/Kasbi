<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.NewGood" CodeFile="NewGood.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[����� �����]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Styles.css" type="text/css" rel="stylesheet">
    <script language="JavaScript" src="../scripts/datepicker.js"></script>
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmNewRequest" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;������ -&gt; ����� �����</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" colspan="5">
                    &nbsp;���������� � �������� ���������</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="msg" runat="server" EnableViewState="False" ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="5" >
                    <asp:Repeater ID="repGoodStatistic" runat="server">
                        <HeaderTemplate>
                            <table id="Table2" cellspacing="0" cellpadding="0" width="60%" border=".1">
                                <tr class="headerGrid">
                                    <td width="60%">
                                        ���</td>
                                    <td width="10%">
                                        � ����</td>
                                    <td width="10%">
                                        ������ �� �������</td>
                                    <td width="10%">
                                        ������� �� ������</td>
                                </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr>
                                <td class="itemGrid">
                                    <%# DataBinder.Eval(Container.DataItem, "name") %>
                                </td>
                                <td class="itemGrid" align="center" width="10%" style="font-weight: bold">
                                        <%# DataBinder.Eval(Container.DataItem, "cash_all")%>
                                </td>
                                <td class="itemGrid" align="center" width="10%" style="font-weight: bold">
                                        <%#DataBinder.Eval(Container.DataItem, "cash_outside")%>
                                </td>
                                <td class="itemGrid" align="center" width="10%" style="font-weight: bold">
                                        <%# DataBinder.Eval(Container.DataItem, "cash_rest")%>
                                </td>
                            </tr>
                        </ItemTemplate>
                        <FooterTemplate>
                            <tr class="footerGrid">
                                <td align="right">
                                    <font size="2">����� : </font>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblCashAll" runat="server"></asp:Label></td>
                                <td align="center">
                                    <asp:Label ID="lblCashOutSide" runat="server"></asp:Label></td>
                                <td align="center">
                                    <asp:Label ID="lblCashRest" runat="server"></asp:Label></td>
                            </tr>
                            </TABLE>
                        </FooterTemplate>
                    </asp:Repeater>
                </td>
            </tr>
            <tr>
                <td colspan="5">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="5">
                    &nbsp;���������� ������ ��������� ��������</td>
            </tr>
            <tr>
                <td colspan="5">
                    <asp:Label ID="msgAddGood" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                </td>
                <td colspan="3">
                    <table cellspacing="1" cellpadding="2" width="100%" border="0" align="left">
                        <tr class="SectionRow">
                            <td colspan="5" class="SectionRow">
                                <asp:RadioButtonList ID="rbtnKKM_Type" runat="server" AutoPostBack="True" RepeatDirection="Vertical"    CssClass="Caption">
                                    <asp:ListItem Selected="True">���� ���������� � ����� ��������� </asp:ListItem>
                                    <asp:ListItem>���� ���������� � ��������� ��������� </asp:ListItem>
                                </asp:RadioButtonList>
                             </td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            <td>
                                ��������</td>
                            <td colspan="2" align="left">
                                ��� ������</td>
                            <td colspan="2">
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" align="center">
                                <asp:DropDownList ID="lstGoodDelivery" runat="server" Width="200px" BackColor="#F6F8FC"
                                    AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td class="SectionRow" colspan="3">
                                <asp:DropDownList ID="lstGoodType" runat="server" Width="100%" BackColor="#F6F8FC"
                                    AutoPostBack="True">
                                </asp:DropDownList></td>
                            <td class="SectionRow">
                                &nbsp;</td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            <td>
                                ���������� � ��������</td>
                            <td align="left">
                                <asp:Label ID="lblGoodNumCashregister" runat="server">��������� �����</asp:Label></td>
                            <td align="left">
                                �������������� ����������</td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" align="left" rowspan="7" width="40%">
                                <asp:BulletedList ID="blstDeliveryInfo" runat="server" BulletImageUrl="~/Images/star.gif"
                                    CssClass="Caption" Width="100%" BulletStyle="CustomImage">
                                    <asp:ListItem>������</asp:ListItem>
                                    <asp:ListItem>������</asp:ListItem>
                                    <asp:ListItem>�������</asp:ListItem>
                                </asp:BulletedList>
                                
                                <asp:label id="Label1" runat="server" CssClass="text02">����:</asp:label> <asp:textbox id="tbxBeginDate" Runat="server" BorderWidth="1px"></asp:textbox><A href="javascript:showdatepicker('tbxBeginDate', 0, false,'MM.DD.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="��������� ����"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">����������, ������� ���������� �������� ��������� ����</asp:comparevalidator>
                            </td>
                            <td class="SectionRow">
                                <asp:TextBox ID="txtGoodNumCashregister" runat="server" Width="150px" BackColor="#F6F8FC"
                                    BorderWidth="1px" ToolTip="������� ��������� �����" MaxLength="100"></asp:TextBox></td>
                            <td class="SectionRow" colspan="2" align="left">
                                <asp:TextBox ID="txtGoodInfo" runat="server" Width="100%" BackColor="#F6F8FC" BorderWidth="1px"
                                    ToolTip="������� �������������� ���������� � ������" MaxLength="250"></asp:TextBox></td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            <td align="left">
                                <asp:Label ID="lblReestr" runat="server">�� �������</asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblPZU" runat="server">�� ���</asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblMFP" runat="server">�� ���</asp:Label></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtReestr" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td>
                            <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtPZU" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td>
                            <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtMFP" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td>
                            <td>
                            </td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            <td align="left">
                                <asp:Label ID="lblCP" runat="server">�� ��</asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblCTO" runat="server">�� ���</asp:Label></td>
                            <td align="left">
                                <asp:Label ID="lblCTO2" runat="server">�� ��� 2</asp:Label></td>
                           <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtCP" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td> 
                             <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtCTO" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td>
                            <td class="SectionRow" align="left">
                                <asp:TextBox ID="txtCTO2" runat="server" Width="150px" BackColor="#F6F8FC" BorderWidth="1px"
                                    MaxLength="11"></asp:TextBox></td>
                           
                            <td>
                            </td>
                        </tr>
                        <tr class="SubTitleTextbox">
                            &nbsp;<td colspan="3" align="left">
                                <asp:Label ID="lblWorker" runat="server">���������</asp:Label></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" colspan="3" align="left">
                                <asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="100%">
                                </asp:DropDownList></td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="5" style="height: 14px">
                            </td>
                        </tr>
                    </table>
                </td>
                <td>
                </td>
            </tr>
            <tr>
                <td class="Unit" align="center" colspan="5" style="height: 19px">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel">
                    </asp:ImageButton>&nbsp;&nbsp;
                    <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton>
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

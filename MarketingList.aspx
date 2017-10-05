<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.MarketingList" CodeFile="MarketingList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />

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
                <td class="SectionRow" colspan="2">
                        <table width="100%" style="font-size:12px;">
                            <tr>
                                <td valign="top">
                                    
                                    <div style="color:#666666"><b>Фильтр:</b></div><br />
                                    <asp:TextBox ID="txtFilter" runat="server" BorderWidth="1px" Width="220px"></asp:TextBox>&nbsp;&nbsp;
                                    <br />
                                    <span style="color:#666666"><b>Искать в: </b></span>
                                    <input checked="checked" type="checkbox" /> Имени
                                    <input checked="checked" type="checkbox" /> Истории
                                    <input checked="checked" type="checkbox" /> Интересах
                                   
                                    <br />
                                    <span style="color:#666666"><b>Среди тех, у кого: </b></span>
                                    <input checked="checked" type="checkbox" /> Есть записи в истории <input checked="checked" type="checkbox" /> Расширена информация

                                </td>
                                <td width="300" valign="top">
                                    <div style="color:#666666"><b>Сотрудники:</b></div><br />
                                    <asp:listbox id="lstEmployee" runat="server" Width="250px" BackColor="#FFFFFF" Rows="4" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true"></asp:listbox>                                
                                    <br />
                                    <input checked="checked" type="checkbox" /> Добавили <input checked="checked" type="checkbox" /> Работали
                                </td>
                                
                                    
                                <td width="300" valign="top">
                                    <div style="color:#666666"><b>Дата регистрации / работы:</b></div><br />
                                    <table width="100%" style="font-size:12px;">
      							        <TR>
								            <TD class="SectionRowLabel" style="WIDTH: 127px"><asp:label id="Label1" runat="server">Начальная дата:</asp:label></TD>
								            <TD class="SectionRow"><asp:textbox id="tbxBeginDate" Text="01.01.2007" Runat="server" BorderWidth="1px"></asp:textbox>
                                                <%--<A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										            ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server"></asp:label>
									            <asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										            EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator></TD>
							            </TR>
							            <TR>
								            <TD class="SectionRowLabel" style="WIDTH: 127px"><asp:label id="Label3">Конечная дата:</asp:label></TD>
								            <TD class="SectionRow"><asp:textbox id="tbxEndDate" Text="01.01.2008" Runat="server" BorderWidth="1px"></asp:textbox>
                                                <%--<A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                                <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата "
										            ControlToValidate="tbxEndDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat3" runat="server"></asp:label>
									            <asp:comparevalidator id="CompareValidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
										            EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение конечной даты</asp:comparevalidator></TD>
							            </TR>
                                    </table>                                                            
                                    
                                    <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                    position: relative; left: 10;"></asp:Image> &nbsp;&nbsp;&nbsp;
                                    
                                    <asp:LinkButton ID="btnFind" runat="server" CssClass="PanelHider" EnableViewState="False">Искать</asp:LinkButton>                               
                                </td>
                            </tr>
                        </table>

                        
                        <asp:Panel
                                ID="pnlFilter" Style="border-top: #cc9933 1px solid; margin-top: 10px; z-index: 103;
                                margin-bottom: -8px; border-bottom: #cc9933 1px solid" runat="server">
                                <p style="margin-top: 8px; margin-bottom: 9px">


<br />

                    <asp:DataGrid ID="grdCustomers" runat="server" Width="98%" AutoGenerateColumns="False"
                        CellPadding="1" AllowSorting="True" BorderColor="#CC9966" BorderWidth="1px">
                        <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                        <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                        <AlternatingItemStyle CssClass="itemGrid"></AlternatingItemStyle>
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="customer_sys_id"></asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="customer_sys_id" HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkId" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customer_sys_id") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="customer_name" HeaderText="Организация:" ItemStyle-HorizontalAlign="left">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.customer_name") %>' CausesValidation="false" CommandName="ViewDetail">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn> 
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="Руководитель:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkBoss" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.boos_last_name") %>'>
                                    </asp:Label>
                                    <asp:Label ID="Label1" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.boos_first_name") %>'>
                                    </asp:Label>
                                    <asp:Label ID="Label2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.boos_patronymic_name") %>'>
                                    </asp:Label>                                                                        
                                </ItemTemplate>
                            </asp:TemplateColumn>   
                             <asp:TemplateColumn SortExpression="address" HeaderText="Адрес:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkAdress" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.address") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn> 
                             <asp:TemplateColumn SortExpression="d" HeaderText="Дата внесения:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkRegister" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.d") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn> 
                             <asp:TemplateColumn SortExpression="dogovor" HeaderText="Дата последней работы:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkLast" runat="server" Text=''>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>                       
                              <asp:TemplateColumn SortExpression="num_history" HeaderText="Записей в истории:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkNum" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_history") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>   
                              <asp:TemplateColumn SortExpression="manag" HeaderText="Менеджер:">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lnkNum2" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.manag") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>                                                                                                                                                                                                   
                        </Columns>
                    </asp:DataGrid>





                                </p>
                            </asp:Panel>
                        <p>
                        </p>
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

        <script language="javascript">
                jQuery(function () {

                    jQuery('#tbxBeginDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                        scrollMonth: false,
                    });

                    jQuery('#tbxEndDate').datetimepicker({
                        lang: 'ru',
                        timepicker: false,
                        format: 'd.m.Y',
                        closeOnDateSelect: true,
                        scrollMonth: false,
                    });

                });

         </script>
        
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
        <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server">
        <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server">
        <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server">
        <input id="FindHidden" type="hidden" name="FindHidden" runat="server">
    </form>
</body>
</html>

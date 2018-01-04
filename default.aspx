<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.frmDefault" Culture="ru-Ru"
    CodeFile="default.aspx.vb" %>

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
    <meta charset="utf-8">
    <link href="Styles.css" type="text/css" rel="stylesheet">
    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmMain" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Главная</td>
            </tr>
        </table>
        
        <table width="100%" style="font-size:12">
            <tr>
                <td valign="top">
                    <asp:HyperLink ID="btnNew" runat="server" EnableViewState="False" CssClass="PanelHider"
                        NavigateUrl="NewRequest.aspx">
                        <asp:Image runat="server" ID="imgSelNew" ImageUrl="Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Новый клиент</asp:HyperLink>
                            
                    <asp:HyperLink ID="lnkNeopl" runat="server" EnableViewState="False" CssClass="PanelHider" NavigateUrl="SalesNeopl.aspx"><br><br><br><br>
                    <asp:Image runat="server" ID="Image3" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp; <span style="color:red">Неоплаченные заказы        <br>
                    <br></span>
                    </asp:HyperLink>
        

                        <asp:HyperLink ID="lnkMain" runat="server" EnableViewState="False" CssClass="PanelHider" NavigateUrl="Main.aspx"><br><br>
                        <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                            position: relative; left: 10;"></asp:Image>&nbsp; Информация о товарах на складе<br>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; и продажах ККМ
                        </asp:HyperLink>
                        
                        
                                <asp:HyperLink ID="lnkMakeInvoice" runat="server" EnableViewState="False" CssClass="PanelHider" NavigateUrl="MakeInvoice.aspx"><br><br>
                        <asp:Image runat="server" ID="Image8" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                            position: relative; left: 10;"></asp:Image>&nbsp; Сформировать счет
                        </asp:HyperLink>
                                                
                </td>
                <td valign="top">
                    Введите серийный номер:<br />
                    <br />
                    <asp:TextBox ID="txtRequest" runat="server"></asp:TextBox> &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; <asp:LinkButton CssClass="PanelHider" ID="lnk_search_cash" Text="найти" runat="server" /> <br />
                    <asp:ListBox ID="lstcash" runat="server" Rows="4"></asp:ListBox><br />


                   <asp:Image runat="server" ID="Image1" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton CssClass="PanelHider" ID="btnRequest" Text="Состояние ремонта" runat="server" />
                    <br /><br />
                    <asp:Image runat="server" ID="Image6" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton CssClass="PanelHider" ID="lnksetRepair" Text="Принятие в ремонт" runat="server" />
                    <br /><br />
                    <asp:Image runat="server" ID="Image7" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:LinkButton CssClass="PanelHider" ID="lnkShowRepair" Text="Показать оборудование в ремонте" runat="server" />        
                    
                </td>
                <td valign="top">
                    <asp:HyperLink ID="lnkBookKeeping" runat="server" EnableViewState="False" CssClass="PanelHider" NavigateUrl="BookKeeping.aspx"> <asp:Image runat="server" ID="Image5" ImageUrl="Images/sel.gif" Style="z-index: 103; position: relative; left: 10;"></asp:Image>&nbsp;Бухгалтерия</asp:HyperLink>
                </td>                
            </tr>
        </table>
        
        <table id="tblLinks" cellspacing="0" cellpadding="0" width="100%" runat="server">
            <tr>
                <td>
                    <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></td>
            </tr>
            <tr>
                <td class="SectionRow">
                    </td>
                <td class="SectionRow">
                    </td>
            </tr>
        </table>

        
        
<br /><br />
<asp:HyperLink ID="HyperLink2" runat="server" EnableViewState="False" CssClass="PanelHider" NavigateUrl="IBANConverter.aspx"> <asp:Image runat="server" ID="Image9" ImageUrl="Images/sel.gif" Style="z-index: 103; position: relative; left: 10;"></asp:Image>&nbsp;IBAN конвертер</asp:HyperLink>
 
        
        
<asp:Label ID="lblExport" runat="server" Visible="true">
        
<br /><br /><br /><br /><br /><br /><br /><br /><br /><br />
<asp:linkbutton id="lnk_export" runat="server" CssClass="PanelHider" EnableViewState="False">
<asp:Image runat="server" ID="Image4" ImageUrl="Images/sel.gif" style="Z-INDEX: 103; position:relative; left:10;"></asp:Image>&nbsp;Экспорт</asp:linkbutton>
						<asp:hyperlink id="Hyperlink3" runat="server" EnableViewState="False" Font-Size="8pt" Target="_blank"></asp:hyperlink><br /><br /> 
    
    <table>
        <tr>
            <td>
                <asp:RadioButtonList ID="radioButtonListExport" runat="server" Font-Size="8pt" AutoPostBack="True">
                <%--<asp:ListItem  Value="fullHistory">Все работы</asp:ListItem>--%>
                <asp:ListItem Value="warrantyHistory">Гарантийные ремонты</asp:ListItem>
                <asp:ListItem Value="notWorkHistory">Ремонты без стоимости работ</asp:ListItem>
                <asp:ListItem Value="standartHistory">Платные ремонты</asp:ListItem>
                <asp:ListItem Selected="True" Value="toHistory">ТО</asp:ListItem>
                <asp:ListItem Value="toHistoryByEmployee">ТО по мастерам</asp:ListItem>
                <asp:ListItem Value="toHistoryByEmployeeExcel">ТО по мастерам (Excel)</asp:ListItem>
                <asp:ListItem Value="removedFromTOExcel">ТО не проведено и снятые с ТО (Excel)</asp:ListItem>
                <asp:ListItem Value="toHistorySpecialRulesExcel">Список ТО - исключение (Excel)</asp:ListItem>

                </asp:RadioButtonList>
            </td>
            <td>
                <asp:ListBox runat="server" ID="lstEmployee" Rows="7" Width="250px" Visible="False" AutoPostBack="True"/>
            </td>
        </tr>
    </table>

<br />

&nbsp;&nbsp;Дата начала экспорта:
<asp:textbox id="tbxBeginDate" Runat="server" BorderWidth="1px"></asp:textbox>
   
    <%--<A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
    <asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator>
<br />

&nbsp;&nbsp;Дата конца экспорта:&nbsp;&nbsp;
<asp:textbox id="tbxEndDate" Runat="server" BorderWidth="1px"></asp:textbox>

    <%--<A href="javascript:showdatepicker('tbxEndDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
    <asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Конечная дата"
										ControlToValidate="tbxEndDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="Label1" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxEndDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator>


</asp:Label>



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

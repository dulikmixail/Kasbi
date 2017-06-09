<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.AddTO" CodeFile="AddTO.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[ЦТО "Рамок"]</title>

    <script language="javascript">
		function isFind(s)
		{
			var theform = document.frmInternalCTO;
			theform.FindHidden.value = s;
		}
    </script>
<script language="javascript">
  function ExportToExcel()        
  {


//      alert("ExportToExcel");
//      var sHTML = window.document.getElementById("grdTO").outerhtml;
//      alert(sHTML);
//      var m_objExcel = new Excel.Application();
//        var m_objBooks = (Excel.Workbooks)m_objExcel.Workbooks;
//var m_objBook = (Excel._Workbook)(m_objBooks.Add());
//m_objBook.HTMLProject.HTMLProjectItems("Sheet1").Text = sHTML;
//      m_objBook.HTMLProject.RefreshDocument();
//     m_objExcel.Visible = true;
//      m_objExcel.UserControl = true; 
////      var oXL = CreateObject("Excel.Application");
////      alert(oXL);
////      var oBook = oXL.Workbooks.Add();
////      alert(oBook);
////      oBook.HTMLProject.HTMLProjectItems("Sheet1").Text = sHTML;
////      oBook.HTMLProject.RefreshDocument;
////      oXL.Visible = true;
////      oXL.UserControl = true;
//	
}
</script>

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
    <form id="frmInternalCTO" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;ЦТО&nbsp;&nbsp;Мастеа&nbsp;"Рамок"&nbsp; - Добавление нового товара на ТО</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" border="0" style="font-size:12">
            <tr class="Unit">
                <td class="Unit" colspan="2" width="100%">Заполните&nbsp;необходимые&nbsp;данные</td>
            </tr>
            <tr>
                <td class="SubTitleTextbox" align="left" valign="top">
                    <asp:Label ID="lblMess" runat="server" Visible="false" ForeColor="#009999" Font-Size="13"></asp:Label><br />
                    <br />
                    Ответственный:<br />
                    <br />
                    <asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="400"></asp:DropDownList><br />
                    <br />
                    Тип товара:<br />
                    <br />
                    <asp:ListBox ID="lstGoodType" runat="server" Width="400"></asp:ListBox><br />
                    <br />
                    Установка: <asp:TextBox ID="txt_SetPlace" runat="server" Width="340" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox><br />
                    <br />
                    <asp:ListBox ID="lstPlaceRegion" runat="server" Width="400"></asp:ListBox><br />
                    <br />
                    Дата начала гарантийного срока: 
                    <asp:textbox id="tbxBeginDate" Runat="server" BorderWidth="1px"></asp:textbox><A href="javascript:showdatepicker('tbxBeginDate', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A><asp:requiredfieldvalidator id="RequiredFieldValidator1" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="lblDateFormat2" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="typeValidator" runat="server" CssClass="ErrorMessage" ControlToValidate="tbxBeginDate"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator>
					
                    <br /><br />
                    Дата постановки на ТО: 
                    <asp:textbox id="txtSupportStart" Runat="server" BorderWidth="1px"></asp:textbox><A href="javascript:showdatepicker('txtSupportStart', 0, false,'DD.MM.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A><asp:requiredfieldvalidator id="RequiredFieldValidator2" runat="server" CssClass="ErrorMessage" ErrorMessage="Начальная дата"
										ControlToValidate="tbxBeginDate">*</asp:requiredfieldvalidator>&nbsp;<asp:label id="Label1" runat="server" CssClass="text02"></asp:label>
									<asp:comparevalidator id="Comparevalidator1" runat="server" CssClass="ErrorMessage" ControlToValidate="txtSupportStart"
										EnableClientScript="False" Display="Dynamic" Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение начальной даты</asp:comparevalidator>
					<br /><br />
                    <asp:CheckBox runat="server" ID="chk_SetTO" Text="Поставить товар на ТО" Font-Size="10" Font-Italic="False" /><br />
                    <br />
                 </td>
                 <td valign="top">
                    <table style="font-size:12" width="600">
                       <tr class="TitleTextbox">
                                    <td class="SubTitleTextbox" style="width: 20px" align="left">
                                        Владелец:</td>
                                    <td class="SectionRow" align="left" colspan="8">
                                        <asp:TextBox ID="txtCustomerFind" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                            Width="87%" MaxLength="11"></asp:TextBox><asp:LinkButton ID="lnkCustomerFind" runat="server"
                                                CssClass="LinkButton">&nbsp;&nbsp;&nbsp;Найти</asp:LinkButton></td>
                                </tr>
                                <tr class="SubTitleTextbox">
                                    <td class="SectionRowLabel" style="width: 147px" align="left">
                                        &nbsp;</td>
                                    <td class="SectionRow" align="left" colspan="8">
                                        <asp:ListBox ID="lstCustomers" runat="server" Rows="10" Width="100%" AutoPostBack="True"></asp:ListBox></td>
                                </tr>
                                <tr>
                                    <td class="SectionRow" style="width: 147px">
                                        &nbsp;</td>
                                    <td class="SectionRow" colspan="8">
                                        &nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
                                </tr>
                    </table> 
                </td>               
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2" width="100%">Укажите&nbsp;информацию&nbsp;о&nbsp;товаре</td>
            </tr>
            <tr>
                <td colspan="2" class="SubTitleTextbox">
                    Номер товара:
                    <asp:TextBox ID="txt_GoodNum" runat="server" Width="320" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                    
                    <br /><br /><br />
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton>
                </td>
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

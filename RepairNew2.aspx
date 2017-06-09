<%@ Reference Page="~/admin/details.aspx" %>
<%@ Reference Page="~/Documents.aspx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.RepairNew2" CodeFile="RepairNew2.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Рамок - [Ремонт кассовых аппаратов]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>

</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    rightmargin="0">
    <form id="frmRepairs" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Карточка ККМ -&gt; Ремонт&nbsp;ККМ -&gt; Запись о ремонте</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%">
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Кассовый аппарат</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="msg" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red"
                        Font-Size="8pt"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <table style="font-size: 9pt; font-family: Verdana; position: relative; top: -10px"
                        cellspacing="0" cellpadding="0" align="center">
                        <tr>
                            <td width="40">
                            </td>
                            <td width="100">
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ToolTip="На техобслуживании"
                                    ImageUrl="Images/support.gif"></asp:HyperLink>
                                <asp:ImageButton ID="imgRepair" runat="server" ToolTip="В ремонте" ImageUrl="Images/repair.gif">
                                </asp:ImageButton></td>
                </td>
                <td align="right">
                    <asp:Label ID="lblCashType" runat="server" Font-Bold="true"></asp:Label>
                </td>
                <td>
                    <asp:HyperLink ID="lblCash" runat="server" Font-Bold="true" ToolTip="Карточка ККМ"></asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="cash">
                    <asp:Label ID="lblCaptionGarantia" runat="server">Гарантийный срок :</asp:Label></td>
                <td class="cashDetail">
                    <asp:Label ID="lblGarantia" runat="server" ForeColor="Red"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2" class="cash">
                    <asp:Label ID="lblCaptionNumbers" runat="server"> СК Реестра/ПЗУ/МФП:</asp:Label></td>
                <td class="cashDetail">
                    <asp:Label ID="lblNumbers" runat="server"></asp:Label></td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionMarka" runat="server" CssClass="cash">Марка ЦТО:</asp:Label></td>
                <td>
                    <asp:Label ID="lblMarka" runat="server" CssClass="cashDetail"></asp:Label></td>
            </tr>
            <tr height="0">
                <td colspan="2">
                    <asp:Label ID="lblCaptionSetPlace" runat="server" CssClass="cash">Место установки:</asp:Label></td>
                <td>
                    <asp:Label ID="lblSetPlace" runat="server" CssClass="cashDetail"></asp:Label></td>
            </tr>
            <tr>
                <td align="left" colspan="2">
                    <asp:Label ID="lblCaptionSupport" runat="server" CssClass="cash">ТО:</asp:Label></td>
                <td align="left">
                    <asp:Label ID="lblSupport" runat="server" CssClass="cashDetail"></asp:Label></td>
            </tr>
        </table>
        </td> </tr>
        <tr class="Unit">
            <td class="Unit" colspan="2">
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Запись о ремонте</td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:Label ID="msgNew" runat="server" EnableViewState="False" Font-Bold="True" ForeColor="Red"
                    Font-Size="8pt"></asp:Label>
                <table align="center">
                    <tr class="subCaption" align="center">
                        <td align="center" colspan="9">
                            <table id="pnlSupport" width="100%" runat="server">
                                <tr class="TitleTextbox">
                                    <td class="SubTitleTextbox" style="width: 127px">
                                        Дата принятия в ремонт:
                                        <br>
                                        <asp:CheckBox ID="chbRepairDateInEdit" runat="server" Text="изменить" AutoPostBack="True">
                                        </asp:CheckBox></td>
                                    <td class="SectionRow" colspan="3">
                                        <asp:Label ID="lblRepairDateIn" runat="server" CssClass="text02"></asp:Label><asp:Panel
                                            ID="pnlRepairDateIn" runat="server" Visible="False">
                                            <asp:TextBox ID="tbxRepairDateIn" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox><a
                                                href="javascript:showdatepicker('tbxRepairDateIn', 0, false,'DD.MM.YYYY')"><img id="imgRepairDateIn"
                                                    alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator1" runat="server" CssClass="ErrorMessage"
                                                ErrorMessage="Дата приема в ремонт " ControlToValidate="tbxRepairDateIn" Display="Static">*</asp:RequiredFieldValidator>&nbsp;
                                            <asp:CompareValidator ID="Comparevalidator2" runat="server" CssClass="ErrorMessage"
                                                ControlToValidate="tbxRepairDateIn" Display="Dynamic" EnableClientScript="False"
                                                Type="Date" Operator="DataTypeCheck">Пожалуйста, введите корректные значение даты приема в ремонт</asp:CompareValidator></asp:Panel>
                                    </td>
                                    <td class="SectionRow" colspan="5">
                                        Дата выдачи из ремонта:&nbsp;&nbsp;
                                        <asp:TextBox ID="tbxRepairDateOut" runat="server" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                        <a href="javascript:showdatepicker('tbxRepairDateOut', 0, false,'DD.MM.YYYY')">
                                            <img id="imgRepairDateOut" alt="Date Picker" src="Images/cal_date_picker.gif" border="0"></a>
                                    </td>
                                </tr>
                                <tr class="SubCaption" align="center">
                                    <td width="60">
                                        &nbsp;</td>
                                    <td width="72">
                                        Марка ЦТО</td>
                                    <td width="72">
                                        <asp:Label ID="lblCaptionCTO2" runat="server" Text="Марка ЦТО2"></asp:Label>
                                    </td>
                                    <td width="72">
                                        Марка&nbsp;Реестра</td>
                                    <td width="72">
                                        Марка&nbsp;ПЗУ</td>
                                    <td width="72">
                                        Марка&nbsp;МФП</td>
                                    <td width="72">
                                        <asp:Label ID="lblCaptionCP" runat="server" Text="Марка ЦП"></asp:Label></td>
                                    <td width="72">
                                        Z-отчёт</td>
                                    <td width="72">
                                        Итог</td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td class="SubCaption" valign="middle" align="right">
                                        до</td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaCTOIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaCTO2In" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО2 до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaReestrIn" runat="server" Font-Size="8pt" ToolTip="Введите марку Реестра до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaPZUIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ПЗУ до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaMFPIn" runat="server" Font-Size="8pt" ToolTip="Введите марку МФП до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewMarkaCPIn" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦП до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewZReportIn" runat="server" Font-Size="8pt" ToolTip="Введите номер Z-отчёта до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20"></asp:TextBox></td>
                                    <td style="width: 78px">
                                        <asp:TextBox ID="txtNewItogIn" runat="server" Font-Size="8pt" ToolTip="Введите необнуляемый итог до ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20"></asp:TextBox></td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr>
                                    <td class="SubCaption" valign="middle" align="right" colspan="1">
                                        после</td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaCTOOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaCTO2Out" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦТО2 после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaReestrOut" runat="server" Font-Size="8pt" ToolTip="Введите марку Реестра после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaPZUOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ПЗУ после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaMFPOut" runat="server" Font-Size="8pt" ToolTip="Введите марку МФП после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewMarkaCPOut" runat="server" Font-Size="8pt" ToolTip="Введите марку ЦП после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="11"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewZReportOut" runat="server" Font-Size="8pt" ToolTip="Введите номер Z-отчёта после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20"></asp:TextBox></td>
                                    <td>
                                        <asp:TextBox ID="txtNewItogOut" runat="server" Font-Size="8pt" ToolTip="Введите необнуляемый итог после ремонта"
                                            BorderWidth="1px" BackColor="#F6F8FC" Width="80px" MaxLength="20"></asp:TextBox></td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr valign="middle">
                                    <td class="SubTitleTextbox" align="left">
                                    </td>
                                    <td class="SubTitleTextbox" align="left" colspan="3">
                                        Исполнитель</td>
                                    <td class="SubTitleTextbox" align="left" colspan="4">
                                        &nbsp;</td>
                                    <td class="SubTitleTextbox" align="left" width="72">
                                        Акт</td>
                                </tr>
                                <tr>
                                    <td style="height: 17px">
                                    </td>
                                    <td style="width: 154px; height: 17px" colspan="7">
                                        <asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="336px">
                                        </asp:DropDownList></td>
                                    <td style="height: 17px">
                                        <asp:TextBox ID="txtNewAkt" runat="server" ToolTip="Введите номер акта" BorderWidth="1px"
                                            BackColor="#F6F8FC" Width="80"></asp:TextBox></td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr class="TitleTextbox">
                                    <td class="SubTitleTextbox" style="width: 147px" align="left">
                                        Плательщик ремонта:</td>
                                    <td class="SectionRow" align="left" colspan="8">
                                        <asp:TextBox ID="txtCustomerFind" runat="server" BorderWidth="1px" BackColor="#F6F8FC"
                                            Width="87%" MaxLength="11"></asp:TextBox><asp:LinkButton ID="lnkCustomerFind" runat="server"
                                                CssClass="LinkButton">&nbsp;&nbsp;&nbsp;Найти</asp:LinkButton></td>
                                </tr>
                                <tr class="SubTitleTextbox">
                                    <td class="SectionRowLabel" style="width: 147px" align="left">
                                        &nbsp;</td>
                                    <td class="SectionRow" align="left" colspan="8">
                                        <asp:ListBox ID="lstCustomers" runat="server" Width="100%" AutoPostBack="True"></asp:ListBox></td>
                                </tr>
                                <tr>
                                    <td class="SectionRow" style="width: 147px">
                                        &nbsp;</td>
                                    <td class="SectionRow" colspan="8">
                                        &nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
                                </tr>
                                <tr>
                                    <td class="SubTitleTextbox" style="height: 14px" colspan="9">
                                        Перечень деталей и работ
                                    </td>
                                </tr>
                                <tr>
                                    <td align="center" colspan="9">
            
                                    <table cellspacing="0" cellpadding="1" rules="all" bordercolor="#CC9966" border="1" id="Table1" style="border-color:#CC9966;border-width:1px;border-style:solid;width:100%;border-collapse:collapse;">
			<tr class="headerGrid" valign="top">
				<td style="width:300pt;">Наименование</td>
				<td align="center">Количество</td>
				<td align="center">Цена детали</td>
				<td align="center">Стоимость услуги</td>
				<td align="center">Общая стоимость</td>
				<td align="center">Норма/час</td>
			</tr>
			<asp:Repeater ID="rep_details" runat="server">
			<ItemTemplate>
			<tr class="footerGrid" valign="top" style="font-weight:bold;">
				<td align="left">
				    <div style="display:inline"><input type="checkbox" value="22" id="<%# DataBinder.Eval(Container.DataItem,"detail_id")%>"  onclick="process(<%# DataBinder.Eval(Container.DataItem,"detail_id")%>)"></inpt></div>
				    <div id="name_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" style="display:inline; color:#999999"><%# DataBinder.Eval(Container.DataItem,"detail_name")%> (<%# DataBinder.Eval(Container.DataItem,"detail_notation")%>)</div></td>
				    <input id="name<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"detail_name")%>" />
				    <input id="notation<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"detail_notation")%>" />
				    <input id="isdetail<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"is_detail")%>" />
				<td align="center">
				<input id="count_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" disabled="disabled" type="text" value="1" style="font-size:12; border:1ps solid" size="3" onkeyup="process_sum(<%# DataBinder.Eval(Container.DataItem,"detail_id")%>)"></td>
				<td align="center">
				    <div id="price_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" style="color:#999999"><%# DataBinder.Eval(Container.DataItem,"price")%></div>
				    <input id="price<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"price")%>" />
				</td>
				<td align="center">
				    <div id="cost_service_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" style="color:#999999"><%# DataBinder.Eval(Container.DataItem,"cost_service")%></div>
				    <input id="cost_service<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"cost_service")%>" />
				</td>
				<td align="center">
					<div id="total_sum_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" style="color:#999999"><%# DataBinder.Eval(Container.DataItem,"total_sum")%></div></td>
				    <input id="total_sum<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem, "total_sum")%>" />
				</td>
				<td align="center">
					<div id="norma_hour_<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" style="color:#999999"><%# DataBinder.Eval(Container.DataItem,"norma_hour")%></div>
				    <input id="norma_hour<%# DataBinder.Eval(Container.DataItem,"detail_id")%>" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"norma_hour")%>" />
				</td></td></td>
			</tr>
			</ItemTemplate>
			</asp:Repeater>
			<tr class="footerGrid" valign="top" style="font-weight:bold; font-size:12; color:Red">
			    <td align="left">Итого: </td>
			    <td align="center"><div id="total_count"></div></td>
			    <td align="center"><div id="total_price"></div></td>
			    <td align="center"><div id="total_service"></div></td>
			    <td align="center"><div id="total_sum"></div></td>
			    <td align="center"><div id="total_nhour"></div></td>
			</tr>
		</table>

<script language="javascript" type="text/javascript">
var m = new Object()
var num_details=1;
var i = 0;
var f = new Object(); 

function process(id){
    if (document.getElementById(id).checked==true){
        document.getElementById("name_" + id).style.color="#000000";
        document.getElementById("count_" + id).disabled="";
        document.getElementById("price_" + id).style.color="#000000";
        document.getElementById("cost_service_" + id).style.color="#000000";
        document.getElementById("total_sum_" + id).style.color="#000000";
        document.getElementById("norma_hour_" + id).style.color="#000000";
        if(Math.ceil(num_details) < Math.ceil(id)){num_details = id;}
        f[id] = Math.ceil(document.getElementById("count_" + id).value)
        calculate_all()
        }
    else{
        document.getElementById("name_" + id).style.color="#999999";
        document.getElementById("count_" + id).disabled="disabled";
        document.getElementById("price_" + id).style.color="#999999";
        document.getElementById("cost_service_" + id).style.color="#999999";
        document.getElementById("total_sum_" + id).style.color="#999999";
        document.getElementById("norma_hour_" + id).style.color="#999999";        
        f[id] = 0
        calculate_all()
        }
    }
  
function process_sum(id){
    price = document.getElementById("price" + id).value
    count = document.getElementById("count_" + id).value
    norma_hour = document.getElementById("norma_hour" + id).value
    norma_hour = norma_hour.replace("\,", ".");
    norma_hour = Math.round(norma_hour * 100, 2) / 100
    
    cost_service = document.getElementById("cost_service" + id).value

    document.getElementById("total_sum_" + id).innerHTML = (Math.ceil(price) + Math.ceil(cost_service)) * Math.ceil(count)
    document.getElementById("norma_hour_" + id).innerHTML = norma_hour * Math.ceil(count)
    calculate_all()
    }
    
    
function calculate_all(){
    document.getElementById("txtNewDetails").value = ""
    document.getElementById("txtNewInfo").value = ""
    var total_count = 0
    var total_price = 0
    var total_service = 0
    var total_sum = 0
    var total_nhour = 0
	for(var x in f){
	    if(f[x] != 0 && f[x] < 100){
	        if(document.getElementById("isdetail" + x).value == "True"){
	            if (document.getElementById("txtNewDetails").value != ""){
                    document.getElementById("txtNewDetails").innerHTML = document.getElementById("txtNewDetails").value + ", " + document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
                    document.getElementById("txtNewInfo").innerHTML = document.getElementById("txtNewInfo").value + ", Замена " + document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
		            }
		        else{
                    document.getElementById("txtNewDetails").innerHTML = document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
                    document.getElementById("txtNewInfo").innerHTML = "Замена " + document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
		            }
		        }
		    else{
	            if (document.getElementById("txtNewInfo").value != ""){
                    document.getElementById("txtNewInfo").innerHTML = document.getElementById("txtNewInfo").value + ", " + document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
		            }
		        else{
                    document.getElementById("txtNewInfo").innerHTML = document.getElementById("name" + x).value + " " + document.getElementById("notation" + x).value
		            }
		        }
		    count = Math.ceil(document.getElementById("count_" + x).value)
		    
		    price = Math.ceil(document.getElementById("price" + x).value)
		    service = Math.ceil(document.getElementById("cost_service" + x).value)
            nhour = document.getElementById("norma_hour" + x).value
            nhour = nhour.replace("\,", ".")
		    
		    total_count = Math.ceil(total_count) + count 
		    
		    total_price = total_price + Math.ceil(price) * count
		    total_service = total_service + Math.ceil(service) * count
		    total_sum = total_sum + total_price + total_service
		    total_nhour = total_nhour + (nhour * count)
		    total_nhour = Math.round(total_nhour * 100, 2) / 100;
		    
		    document.getElementById("total_count").innerHTML = total_count
		    document.getElementById("total_price").innerHTML = total_price
		    document.getElementById("total_service").innerHTML = total_service
		    document.getElementById("total_sum").innerHTML = total_sum
		    document.getElementById("total_nhour").innerHTML = total_nhour

		    }
		}
    }    
    
function calculate_all2(){
    document.getElementById("txtNewDetails").value = ""
    if(num_details != "0"){
        i = 0;
        while(i <= num_details){
            i=i+1;
            if(document.getElementById(i).checked == true && document.getElementById("isdetail" + i).value == "True"){
                document.getElementById("txtNewDetails").innerHTML = document.getElementById("txtNewDetails").value + ", " + document.getElementById("name" + i).value
                }       
            }
        $i = 0;
        }
    }    
</script>

<script language="javascript" type="text/javascript">
var sum_all;
var sum_nds_all;
var sum_with_nds_all;
var sum_nds;
var sum_with_nds;
var num;
var id;
var old_price;
var new_price;
var f = new Object(); 
f["1"] = 369930; f["2"] = 389400; f["4"] = 637200; f["5"] = 29500; f["14"] = 358720; f["16"] = 414440; f["17"] = 405300; f["19"] = 624000; 
function setprice(id){
	num = document.getElementById("num" + id).value;
	new_price = f[id] * num;
	sum_nds = Math.ceil(new_price * 0.2);
	sum_with_nds = Math.ceil(new_price + sum_nds);
	document.getElementById("price" + id).innerHTML = new_price;
	document.getElementById("sum_nds" + id).innerHTML = sum_nds;
	document.getElementById("sum_with_nds" + id).innerHTML = sum_with_nds;
	setsumm();
	}
function setsumm(){
	sum_all = 0;
	var sum_nds_all = 0;
	var sum_with_nds_all = 0;
	
	for(var x in f){
		num = document.getElementById("num" + x).value;
		sum_all = sum_all + f[x] * num;
		}; 
	
	sum_nds_all = Math.ceil(sum_all * 0.2);
	sum_with_nds_all = Math.ceil(sum_all + sum_nds_all);
		
	document.getElementById("summ").innerHTML = sum_all;
	document.getElementById("summ_nds").innerHTML = sum_nds_all;
	document.getElementById("summ_with_nds").innerHTML = sum_with_nds_all;
	
}
</script>

                                                                                
                                        </td>
                                </tr>


                                
                                
                                <tr valign="middle">
                                    <td class="SubTitleTextbox" style="height: 14px" colspan="4">
                                        Детали</td>
                                    <td class="SubTitleTextbox" style="height: 14px" colspan="5">
                                        Проведенные ремонтные работы</td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td colspan="4">
                                        <asp:TextBox ID="txtNewDetails" runat="server" ToolTip="Перечислите детали, затраченные при ремонте"
                                            Height="80px" BorderWidth="1px" BackColor="#F6F8FC"  Columns="55" TextMode="MultiLine"></asp:TextBox></td>
                                    <td colspan="5">
                                        <asp:TextBox ID="txtNewInfo" runat="server" ToolTip="Введите дополнительную информацию"
                                            Height="80px" BorderWidth="1px" BackColor="#F6F8FC"  Columns="55" TextMode="MultiLine"></asp:TextBox></td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr valign="middle">
                                    <td class="SubTitleTextbox" colspan="9">
                                        Дополнительная информация
                                    </td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                                <tr valign="top">
                                    <td colspan="9">
                                        <asp:TextBox ID="txtNewRepairInfo" runat="server" ToolTip="Введите дополнительную информацию"
                                            Height="50px" BorderWidth="1px" BackColor="#F6F8FC" Width="100%" TextMode="MultiLine"></asp:TextBox></td>
                                    <td width="72">
                                        &nbsp;</td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2" height="15">
                &nbsp;</td>
        </tr>
        </table>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr>
                <td width="100%">
                </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" align="center">
                    <asp:ImageButton ID="btnCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel"
                        CausesValidation="False"></asp:ImageButton>&nbsp;&nbsp;
                    <asp:ImageButton ID="btnSave" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton></td>
            </tr>
            <tr>
                <td height="10">
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

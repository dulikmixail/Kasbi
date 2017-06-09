<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Support" CodeFile="Support.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Тех обслуживание]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmSupport" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Оплата&nbsp;технического&nbsp;обслуживания</td>
            </tr>
        </table>
        <p>
            <asp:Label ID="msg" runat="server" Font-Bold="True" ForeColor="Red" EnableViewState="False"></asp:Label></p>

        <script language="javascript">

	function ChangeCost(cmbCount, txtSumm, txtTotal, sum1, sum2)
	{
		if (document.all[cmbCount].value == 6)
		{
			document.all[txtSumm].value = sum1;
			document.all[txtTotal].innerText = sum1 * 6;
		}
		else
		{
			document.all[txtSumm].value = sum2;
			document.all[txtTotal].innerText = sum2 * document.all[cmbCount].value;
		}
	}
        </script>

        <p>
            <asp:HyperLink ID="btnSupportByMonth" runat="server" NavigateUrl="SupportByMonth.aspx"
                CssClass="PanelHider">
                <asp:Image runat="server" ID="imgSelNew" ImageUrl="Images/sel.gif" Style="z-index: 103;
                    position: relative; left: 10;"></asp:Image>&nbsp;Техническое обслуживание по
                месяцам</asp:HyperLink></p>
        <table cellspacing="0" cellpadding="0" width="100%">
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;Выберите клиента:</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msgSupport" runat="server" Font-Bold="true" ForeColor="#ff0000" EnableViewState="false"
                        CssClass="PanelHider"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr class="Caption">
                <td width="100">
                    &nbsp;</td>
                <td>
                    <asp:DropDownList ID="cmbCustomers" runat="server" ForeColor="#9c0001" AutoPostBack="True"
                        BackColor="#fffaf0" Width="467px">
                    </asp:DropDownList></td>
            </tr>
            <tr>
                <td>
                    &nbsp;</td>
                <td>
                    <asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;Оплата за техническое обслуживание</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="msgAddSupport" runat="server" Font-Bold="true" ForeColor="#ff0000"
                        EnableViewState="false" CssClass="PanelHider"></asp:Label></td>
            </tr>
            <tr>
                <td colspan="2">
                    <table>
                        <tr class="SubCaption" align="center">
                            <td>
                                &nbsp;&nbsp;&nbsp;</td>
                            <td>
                                Месяц:</td>
                            <td>
                                Кол-во месяцев:</td>
                            <td>
                                Ст-сть:</td>
                            <td style="width: 63px">
                                Итого:</td>
                            <td>
                                Кассовые аппараты:</td>
                        </tr>
                        <tr class="Caption" valign="top" align="center">
                            <td>
                                &nbsp;&nbsp;&nbsp;</td>
                            <td>
                                <asp:DropDownList ID="lstMonth" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                                    <asp:ListItem Value="1">Январь</asp:ListItem>
                                    <asp:ListItem Value="2">Февраль</asp:ListItem>
                                    <asp:ListItem Value="3">Март</asp:ListItem>
                                    <asp:ListItem Value="4">Апрель</asp:ListItem>
                                    <asp:ListItem Value="5">Май</asp:ListItem>
                                    <asp:ListItem Value="6">Июнь</asp:ListItem>
                                    <asp:ListItem Value="7">Июль</asp:ListItem>
                                    <asp:ListItem Value="8">Август</asp:ListItem>
                                    <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                    <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                    <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                    <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                </asp:DropDownList><asp:DropDownList ID="lstYear" runat="server" BackColor="#F6F8FC"
                                    BorderWidth="1px">
                                    <asp:ListItem Value="2003">2003</asp:ListItem>
                                    <asp:ListItem Value="2004">2004</asp:ListItem>
                                    <asp:ListItem Value="2005">2005</asp:ListItem>
                                    <asp:ListItem Value="2006">2006</asp:ListItem>
                                    <asp:ListItem Value="2007">2007</asp:ListItem>
                                    <asp:ListItem Value="2008">2008</asp:ListItem>
                                    <asp:ListItem Value="2009">2009</asp:ListItem>
                                    <asp:ListItem Value="2010">2010</asp:ListItem>
                                    <asp:ListItem Value="2011">2011</asp:ListItem>
                                    <asp:ListItem Value="2012">2012</asp:ListItem>
                                    <asp:ListItem Value="2013">2013</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:DropDownList ID="lstCount" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                                    <asp:ListItem Value="1">1</asp:ListItem>
                                    <asp:ListItem Value="2">2</asp:ListItem>
                                    <asp:ListItem Value="3">3</asp:ListItem>
                                    <asp:ListItem Value="4">4</asp:ListItem>
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="6" Selected="True">6</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>
                                <asp:TextBox ID="txtCostPerMonth" runat="server" BackColor="#F6F8FC" Width="90px"
                                    BorderWidth="1px"></asp:TextBox></td>
                            <td style="width: 63px">
                                <asp:Label ID="lblTotal" Style="position: relative; top: 4px" runat="server" Font-Bold="True"></asp:Label></td>
                            <td rowspan="6">
                                <asp:CheckBoxList ID="lstSupportCash" Style="position: relative; top: -2px" runat="server"
                                    CssClass="Caption" RepeatColumns="2">
                                </asp:CheckBoxList><br>
                                <asp:Label ID="lblDelay" runat="server"></asp:Label><br>
                                <asp:CheckBoxList ID="lstDelayCash" Style="position: relative; top: -2px" runat="server"
                                    CssClass="Caption" RepeatColumns="2">
                                </asp:CheckBoxList><asp:Panel ID="pnlCashregisters" runat="server">
                                    Panel</asp:Panel>
                            </td>
                        </tr>
                        <tr class="SubCaption">
                            <td>
                            </td>
                            <td style="width: 418px" colspan="4">
                                Произведенные работы</td>
                        </tr>
                        <tr class="Caption">
                            <td>
                            </td>
                            <td style="width: 418px" colspan="4">
                                <asp:TextBox ID="txtWorks" runat="server" BackColor="#F6F8FC" Width="100%" BorderWidth="1px"></asp:TextBox></td>
                        </tr>
                        <tr class="SubCaption">
                            <td>
                            </td>
                            <td style="width: 418px" colspan="4">
                                Израсходованные материалы</td>
                        </tr>
                        <tr class="Caption">
                            <td>
                            </td>
                            <td style="width: 418px" colspan="4">
                                <asp:TextBox ID="txtMaterials" runat="server" BackColor="#F6F8FC" Width="100%" BorderWidth="1px"></asp:TextBox></td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="btnSaveSupport" runat="server" ImageUrl="Images/update.gif"
                                    CommandName="Update"></asp:ImageButton></td>
                            <td style="width: 275px" align="right" colspan="3">
                                <asp:CheckBox ID="chkPayed" runat="server" BackColor="#F6F8FC" Font-Names="Arial"
                                    Font-Name="Verdana" Font-Size="9pt" Text="Оплачено"></asp:CheckBox><asp:DropDownList
                                        ID="lstPayDay" runat="server" BackColor="#F6F8FC" BorderWidth="1px" Font-Size="9pt">
                                        <asp:ListItem Value="1">1</asp:ListItem>
                                        <asp:ListItem Value="2">2</asp:ListItem>
                                        <asp:ListItem Value="3">3</asp:ListItem>
                                        <asp:ListItem Value="4">4</asp:ListItem>
                                        <asp:ListItem Value="5">5</asp:ListItem>
                                        <asp:ListItem Value="6">6</asp:ListItem>
                                        <asp:ListItem Value="7">7</asp:ListItem>
                                        <asp:ListItem Value="8">8</asp:ListItem>
                                        <asp:ListItem Value="9">9</asp:ListItem>
                                        <asp:ListItem Value="10">10</asp:ListItem>
                                        <asp:ListItem Value="11">11</asp:ListItem>
                                        <asp:ListItem Value="12">12</asp:ListItem>
                                        <asp:ListItem Value="13">13</asp:ListItem>
                                        <asp:ListItem Value="14">14</asp:ListItem>
                                        <asp:ListItem Value="15">15</asp:ListItem>
                                        <asp:ListItem Value="16">16</asp:ListItem>
                                        <asp:ListItem Value="17">17</asp:ListItem>
                                        <asp:ListItem Value="18">18</asp:ListItem>
                                        <asp:ListItem Value="19">19</asp:ListItem>
                                        <asp:ListItem Value="20">20</asp:ListItem>
                                        <asp:ListItem Value="21">21</asp:ListItem>
                                        <asp:ListItem Value="22">22</asp:ListItem>
                                        <asp:ListItem Value="23">23</asp:ListItem>
                                        <asp:ListItem Value="24">24</asp:ListItem>
                                        <asp:ListItem Value="25">25</asp:ListItem>
                                        <asp:ListItem Value="26">26</asp:ListItem>
                                        <asp:ListItem Value="27">27</asp:ListItem>
                                        <asp:ListItem Value="28">28</asp:ListItem>
                                        <asp:ListItem Value="29">29</asp:ListItem>
                                        <asp:ListItem Value="30">30</asp:ListItem>
                                        <asp:ListItem Value="31">31</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="lstPayMonth" runat="server" BackColor="#F6F8FC"
                                        BorderWidth="1px" Font-Size="9pt">
                                        <asp:ListItem Value="1">Январь</asp:ListItem>
                                        <asp:ListItem Value="2">Февраль</asp:ListItem>
                                        <asp:ListItem Value="3">Март</asp:ListItem>
                                        <asp:ListItem Value="4">Апрель</asp:ListItem>
                                        <asp:ListItem Value="5">Май</asp:ListItem>
                                        <asp:ListItem Value="6">Июнь</asp:ListItem>
                                        <asp:ListItem Value="7">Июль</asp:ListItem>
                                        <asp:ListItem Value="8">Август</asp:ListItem>
                                        <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                        <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                        <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                        <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                    </asp:DropDownList><asp:DropDownList ID="lstPayYear" runat="server" BackColor="#F6F8FC"
                                        BorderWidth="1px" Font-Size="9pt">
                                        <asp:ListItem Value="2003">2003</asp:ListItem>
                                        <asp:ListItem Value="2004">2004</asp:ListItem>
                                        <asp:ListItem Value="2005">2005</asp:ListItem>
                                        <asp:ListItem Value="2006">2006</asp:ListItem>
                                        <asp:ListItem Value="2007">2007</asp:ListItem>
                                        <asp:ListItem Value="2008">2008</asp:ListItem>
                                        <asp:ListItem Value="2009">2009</asp:ListItem>
                                        <asp:ListItem Value="2010">2010</asp:ListItem>
                                        <asp:ListItem Value="2011">2011</asp:ListItem>
                                        <asp:ListItem Value="2012">2012</asp:ListItem>
                                        <asp:ListItem Value="2013">2013</asp:ListItem>
                                    </asp:DropDownList></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;История платежей</td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:Label ID="lblClearSupport" runat="server" Font-Bold="true" ForeColor="#ff0000"
                        EnableViewState="false" CssClass="PanelHider"></asp:Label><asp:DataGrid ID="grdSupportHistory"
                            runat="server" Width="98%" CellPadding="1" AutoGenerateColumns="False" AllowSorting="True"
                            BorderColor="#CC9966" BorderWidth="1">
                            <SelectedItemStyle Font-Bold="True" ForeColor="#663399" BackColor="#FFCC66"></SelectedItemStyle>
                            <EditItemStyle VerticalAlign="Middle"></EditItemStyle>
                            <AlternatingItemStyle Font-Size="9pt" Font-Names="Verdana" BackColor="White"></AlternatingItemStyle>
                            <ItemStyle Font-Size="9pt" Font-Names="Verdana" BackColor="White"></ItemStyle>
                            <HeaderStyle Font-Size="9pt" HorizontalAlign="Center" ForeColor="#FFFFCC" BackColor="#990000">
                            </HeaderStyle>
                            <FooterStyle ForeColor="#330099" BackColor="#FFFFCC"></FooterStyle>
                            <Columns>
                                <asp:BoundColumn Visible="False" DataField="sys_id"></asp:BoundColumn>
                                <asp:TemplateColumn SortExpression="number" HeaderText="№">
                                    <ItemTemplate>
                                        <asp:Label ID="lblNumber" runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Оплата">
                                    <HeaderStyle Width="75px"></HeaderStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="lblPay" runat="server"></asp:Label>
                                        <asp:LinkButton ID="lnkPay" runat="server" CommandName="Pay">Оплатить</asp:LinkButton><br>
                                        <asp:DropDownList ID="lstDayPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="13">13</asp:ListItem>
                                            <asp:ListItem Value="14">14</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="16">16</asp:ListItem>
                                            <asp:ListItem Value="17">17</asp:ListItem>
                                            <asp:ListItem Value="18">18</asp:ListItem>
                                            <asp:ListItem Value="19">19</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="21">21</asp:ListItem>
                                            <asp:ListItem Value="22">22</asp:ListItem>
                                            <asp:ListItem Value="23">23</asp:ListItem>
                                            <asp:ListItem Value="24">24</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="26">26</asp:ListItem>
                                            <asp:ListItem Value="27">27</asp:ListItem>
                                            <asp:ListItem Value="28">28</asp:ListItem>
                                            <asp:ListItem Value="29">29</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="31">31</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="lstMonthPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="1">Январь</asp:ListItem>
                                            <asp:ListItem Value="2">Февраль</asp:ListItem>
                                            <asp:ListItem Value="3">Март</asp:ListItem>
                                            <asp:ListItem Value="4">Апрель</asp:ListItem>
                                            <asp:ListItem Value="5">Май</asp:ListItem>
                                            <asp:ListItem Value="6">Июнь</asp:ListItem>
                                            <asp:ListItem Value="7">Июль</asp:ListItem>
                                            <asp:ListItem Value="8">Август</asp:ListItem>
                                            <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                            <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                            <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                            <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="lstYearPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="2003">2003</asp:ListItem>
                                            <asp:ListItem Value="2004">2004</asp:ListItem>
                                            <asp:ListItem Value="2005">2005</asp:ListItem>
                                            <asp:ListItem Value="2006">2006</asp:ListItem>
                                            <asp:ListItem Value="2007">2007</asp:ListItem>
                                            <asp:ListItem Value="2008">2008</asp:ListItem>
                                            <asp:ListItem Value="2009">2009</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBox ID="chkedtPayed" CssClass="SubTitleEditbox" runat="server" TextAlign="Left"
                                            Text="Оплачено:"></asp:CheckBox><br>
                                        <span class="SubTitleEditbox">Дата:</span><br>
                                        <asp:DropDownList ID="lstedtDayPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="1">1</asp:ListItem>
                                            <asp:ListItem Value="2">2</asp:ListItem>
                                            <asp:ListItem Value="3">3</asp:ListItem>
                                            <asp:ListItem Value="4">4</asp:ListItem>
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="6">6</asp:ListItem>
                                            <asp:ListItem Value="7">7</asp:ListItem>
                                            <asp:ListItem Value="8">8</asp:ListItem>
                                            <asp:ListItem Value="9">9</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="11">11</asp:ListItem>
                                            <asp:ListItem Value="12">12</asp:ListItem>
                                            <asp:ListItem Value="10">13</asp:ListItem>
                                            <asp:ListItem Value="10">14</asp:ListItem>
                                            <asp:ListItem Value="10">15</asp:ListItem>
                                            <asp:ListItem Value="10">16</asp:ListItem>
                                            <asp:ListItem Value="10">17</asp:ListItem>
                                            <asp:ListItem Value="10">18</asp:ListItem>
                                            <asp:ListItem Value="10">19</asp:ListItem>
                                            <asp:ListItem Value="10">20</asp:ListItem>
                                            <asp:ListItem Value="10">21</asp:ListItem>
                                            <asp:ListItem Value="10">22</asp:ListItem>
                                            <asp:ListItem Value="10">23</asp:ListItem>
                                            <asp:ListItem Value="10">24</asp:ListItem>
                                            <asp:ListItem Value="10">25</asp:ListItem>
                                            <asp:ListItem Value="10">26</asp:ListItem>
                                            <asp:ListItem Value="10">27</asp:ListItem>
                                            <asp:ListItem Value="10">28</asp:ListItem>
                                            <asp:ListItem Value="10">29</asp:ListItem>
                                            <asp:ListItem Value="10">30</asp:ListItem>
                                            <asp:ListItem Value="10">31</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="lstedtMonthPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="1">Январь</asp:ListItem>
                                            <asp:ListItem Value="2">Февраль</asp:ListItem>
                                            <asp:ListItem Value="3">Март</asp:ListItem>
                                            <asp:ListItem Value="4">Апрель</asp:ListItem>
                                            <asp:ListItem Value="5">Май</asp:ListItem>
                                            <asp:ListItem Value="6">Июнь</asp:ListItem>
                                            <asp:ListItem Value="7">Июль</asp:ListItem>
                                            <asp:ListItem Value="8">Август</asp:ListItem>
                                            <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                            <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                            <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                            <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:DropDownList ID="lstedtYearPay" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                                            Font-Size="7pt">
                                            <asp:ListItem Value="2003">2003</asp:ListItem>
                                            <asp:ListItem Value="2004">2004</asp:ListItem>
                                            <asp:ListItem Value="2005">2005</asp:ListItem>
                                            <asp:ListItem Value="2006">2006</asp:ListItem>
                                            <asp:ListItem Value="2007">2007</asp:ListItem>
                                            <asp:ListItem Value="2008">2008</asp:ListItem>
                                            <asp:ListItem Value="2009">2009</asp:ListItem>
                                            <asp:ListItem Value="2010">2010</asp:ListItem>
                                            <asp:ListItem Value="2011">2011</asp:ListItem>
                                            <asp:ListItem Value="2012">2012</asp:ListItem>
                                            <asp:ListItem Value="2013">2013</asp:ListItem>
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn SortExpression="start_date" HeaderText="Период">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPeriod" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <table>
                                            <tr class="SubCaption" align="center">
                                                <td>
                                                    Начало периода:</td>
                                                <td>
                                                    Кол. мес.:</td>
                                            </tr>
                                            <tr class="Caption" align="center">
                                                <td>
                                                    <asp:DropDownList ID="lstedtMonth" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                                                        <asp:ListItem Value="1">Январь</asp:ListItem>
                                                        <asp:ListItem Value="2">Февраль</asp:ListItem>
                                                        <asp:ListItem Value="3">Март</asp:ListItem>
                                                        <asp:ListItem Value="4">Апрель</asp:ListItem>
                                                        <asp:ListItem Value="5">Май</asp:ListItem>
                                                        <asp:ListItem Value="6">Июнь</asp:ListItem>
                                                        <asp:ListItem Value="7">Июль</asp:ListItem>
                                                        <asp:ListItem Value="8">Август</asp:ListItem>
                                                        <asp:ListItem Value="9">Сентябрь</asp:ListItem>
                                                        <asp:ListItem Value="10">Октябрь</asp:ListItem>
                                                        <asp:ListItem Value="11">Ноябрь</asp:ListItem>
                                                        <asp:ListItem Value="12">Декабрь</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:DropDownList ID="lstedtYear" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                                                        <asp:ListItem Value="2003">2003</asp:ListItem>
                                                        <asp:ListItem Value="2004">2004</asp:ListItem>
                                                        <asp:ListItem Value="2005">2005</asp:ListItem>
                                                        <asp:ListItem Value="2006">2006</asp:ListItem>
                                                        <asp:ListItem Value="2007">2007</asp:ListItem>
                                                        <asp:ListItem Value="2008">2008</asp:ListItem>
                                                        <asp:ListItem Value="2009">2009</asp:ListItem>
                                                        <asp:ListItem Value="2010">2010</asp:ListItem>
                                                        <asp:ListItem Value="2011">2011</asp:ListItem>
                                                        <asp:ListItem Value="2012">2012</asp:ListItem>
                                                        <asp:ListItem Value="2013">2013</asp:ListItem>
                                                    </asp:DropDownList></td>
                                                <td>
                                                    <asp:DropDownList ID="lstedtCount" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
                                                        <asp:ListItem Value="1">1</asp:ListItem>
                                                        <asp:ListItem Value="2">2</asp:ListItem>
                                                        <asp:ListItem Value="3">3</asp:ListItem>
                                                        <asp:ListItem Value="4">4</asp:ListItem>
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Value="6">6</asp:ListItem>
                                                    </asp:DropDownList></td>
                                            </tr>
                                        </table>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Сумма">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSumma" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.summ") %>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <span class="SubCaption">Ст-сть за месяц:</span>
                                        <asp:TextBox ID="txtedtCost" runat="server" Width="70px" BorderWidth="1px" BorderStyle="Solid"
                                            MaxLength="9"></asp:TextBox>
                                        <br>
                                        <span class="SubCaption">Итого:</span>
                                        <asp:Label ID="lbledtSumma" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.summ") %>
                                        </asp:Label>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Документы">
                                    <ItemTemplate>
                                        <asp:HyperLink ID="lnkInvoiceNDS" Target="_blank" runat="server">Счет&nbsp;фактура</asp:HyperLink>&nbsp;
                                        <asp:HyperLink ID="lnkAkt" Target="_blank" runat="server">Акт</asp:HyperLink>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Кассы">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCashList" runat="server">
											<%# DataBinder.Eval(Container, "DataItem.cash_list") %>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:CheckBoxList ID="lstedtSupportCash" Style="position: relative; top: -2px" runat="server"
                                            CssClass="Caption" RepeatColumns="2">
                                        </asp:CheckBoxList>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn HeaderText="Работы">
                                    <ItemTemplate>
                                        <asp:Label ID="lblWorksMaterials" runat="server">11</asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <span class="SubCaption">Работы</span>
                                        <asp:TextBox ID="txtedtWorks" runat="server" Width="100%" BackColor="#F6F8FC" BorderWidth="1px"></asp:TextBox>
                                        <span class="SubCaption">Материалы</span>
                                        <asp:TextBox ID="txtedtMaterials" runat="server" Width="100%" BackColor="#F6F8FC"
                                            BorderWidth="1px"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle Width="100px"></HeaderStyle>
                                    <ItemTemplate>
                                        <p style="margin-top: 2px; margin-bottom: 2px">
                                            <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="Images/edit.gif" CommandName="Edit">
                                            </asp:ImageButton></p>
                                        <p style="margin-top: 0px; margin-bottom: 2px">
                                            <asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="Images/delete.gif" CommandName="Delete">
                                            </asp:ImageButton></p>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <p>
                                            <asp:ImageButton ID="cmdUpdate" runat="server" ImageUrl="Images/update.gif" CommandName="Update">
                                            </asp:ImageButton></p>
                                        <p>
                                            <asp:ImageButton ID="cmdCancel" runat="server" ImageUrl="Images/cancel.gif" CommandName="Cancel">
                                            </asp:ImageButton></p>
                                    </EditItemTemplate>
                                </asp:TemplateColumn>
                            </Columns>
                        </asp:DataGrid></td>
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

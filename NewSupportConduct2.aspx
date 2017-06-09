<%@ Reference Page="~/Documents.aspx" %>
<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.NewSupportConduct2"
    Culture="ru-Ru" CodeFile="NewSupportConduct2.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
    <title>[Проведение ТО]</title>
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
    <form id="frmNewRequest" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Карточка товара -&gt; Постановка&nbsp;на&nbsp;ТО&nbsp;/&nbsp;Снятие&nbsp;с&nbsp;ТО&nbsp;/&nbsp;Приостановка&nbsp;ТО</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%" style="font-size:12px">
            <tr class="Unit">
                <td class="Unit">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Торговое оборудование</td>
            </tr>
            <tr>
                <td align="center">
                <br />
                <b>Тип торгового оборудования: <asp:Label ID="lblGoodType" runat="server"></asp:Label> (№ <asp:Label ID="lblGoodNum" runat="server"></asp:Label>)</b><br />
                <br />
                <table style="font-size:10px; color:Gray">
                    <tr><td>Гарантийный срок:</td><td><asp:Label ID="lblGarant" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>Добавлен в базу:</td><td><asp:Label ID="lblDateAdd" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>Добавил:</td><td><asp:Label ID="lblEmployeeAdd" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>Место установки:</td><td><asp:Label ID="lblSetPlace" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>Продан:</td><td><asp:Label ID="lblCustomer" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>Плательщик:</td><td><asp:Label ID="lblOwner" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                    <tr><td>ТО:</td><td><asp:Label ID="lblTO" runat="server" Font-Size="12px" ForeColor="black"></asp:Label></td></tr>
                </table>
                <br />
                </td>
            </tr>
            
            
            <table cellpadding="0" cellspacing="0">
                <tr>
                    <td style="vertical-align:middle; max-width:255"><input type="button" /></td>
                    <td style="vertical-align:middle; max-width:255"><input type="text" /></td>
                    <td style="vertical-align:middle; max-width:255"><input type="text" /></td>
                </tr>
            </table>
            
            
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    <asp:Label ID="SectionTOName" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; Проведение 
									технического обслуживания</asp:Label>
				</td>
            </tr>
            <tr>
                <td>
				    <table cellpadding="2" cellspacing="0" style="font-size:12px">
				        <tr><td>Тип товара:</td><td><asp:DropDownList Enabled="false" ID="lstGoodType" runat="server" BackColor="#F6F8FC" Width="150px"></asp:DropDownList></td></tr>
				        <tr><td>Номер:</td><td><asp:TextBox ID="txtGoodNumCashregister" runat="server" BackColor="#F6F8FC" Width="150px" MaxLength="8" BorderWidth="1px" Enabled="False"></asp:TextBox></td></tr>
				        <tr><td>Исполнитель:</td><td><asp:DropDownList ID="lstWorker" runat="server" BackColor="#F6F8FC" Width="304px"></asp:DropDownList></td></tr>
				        <tr><td>Дополнительная информация:</td><td><asp:TextBox ID="txtGoodInfo2" runat="server" BackColor="#F6F8FC" Width="100%" MaxLength="250" BorderWidth="1px"></asp:TextBox></td></tr>
				        <tr><td>Место установки:</td><td><asp:TextBox ID="txtPlace2" runat="server" BackColor="#F6F8FC" Width="100%" MaxLength="250" BorderWidth="1px"></asp:TextBox></td></tr>
				        <tr><td>Район установки:</td><td><asp:DropDownList ID="lstPlaceRegion" runat="server" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true" BackColor="#F6F8FC" Width="304px"></asp:DropDownList></td></tr>
				    </table>
				    
				   <hr>
				   <br />
				   
				   <asp:RadioButtonList ID="rbTO" runat="server" CssClass="text02" Height="16px" Width="456px"
                         RepeatColumns="3" AutoPostBack="True">
                         <asp:ListItem Value="0" Selected="True">Постановка на ТО</asp:ListItem>
                         <asp:ListItem Value="1">Снятие с ТО</asp:ListItem>
                         <asp:ListItem Value="2">Проведение ТО</asp:ListItem>
                   </asp:RadioButtonList>
                   
                   <br />
				   <hr>
				   <br />
				   
				   <table id="pnlSupport" width="100%" runat="server">
                                    <tr class="TitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            Плательщик:</td>
                                        <td width="80%" class="SectionRow" align="left">
                                            <asp:TextBox ID="txtCustomerFind" runat="server" BackColor="#F6F8FC" Width="60%"
                                                MaxLength="11" BorderWidth="1px"></asp:TextBox>
                                            <asp:LinkButton ID="lnkCustomerFind" runat="server" CssClass="LinkButton">Найти</asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr class="SubTitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            &nbsp;</td>
                                        <td class="SectionRow" align="left">
                                            <asp:ListBox ID="lstCustomers" runat="server" Width="70%" AutoPostBack="True" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true"></asp:ListBox></td>
                                    </tr>
                                    <tr>
                                        <td class="SectionRow">
                                            &nbsp;</td>
                                        <td class="SectionRow">
                                            &nbsp;<asp:Label ID="lblCustInfo" runat="server" CssClass="DetailField"></asp:Label></td>
                                    </tr>
                                    <tr class="TitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            Дата постановки:</td>
                                        <td class="SectionRow" align="left">
                                            <asp:TextBox ID="tbxSupportDate" runat="server" BorderWidth="1px"></asp:TextBox><a
                                                href="javascript:showdatepicker('tbxSupportDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                                    src="Images/cal_date_picker.gif" border="0"></a>
                                        </td>
                                    </tr>                                 
                             </table>
                               
                               
				   <table id="pnlDelSupport" width="100%" runat="server" visible="false">
                                    <tr class="TitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            Дата снятия с ТО:</td>
                                        <td class="SectionRow" align="left">
                                            <asp:TextBox ID="tbxDelsupportDate" runat="server" BorderWidth="1px"></asp:TextBox><a
                                                href="javascript:showdatepicker('tbxDelsupportDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                                    src="Images/cal_date_picker.gif" border="0"></a>
                                        </td>
                                    </tr>                                 
                   </table>                                


				   <table id="pnlSupportConduct" width="100%" runat="server" visible="false">
                                    <tr class="TitleTextbox">
                                        <td class="SectionRowLabel" align="left">
                                            Дата проведения ТО:</td>
                                        <td class="SectionRow" align="left">
                                            <asp:TextBox ID="txtSupportDate" runat="server" BorderWidth="1px"></asp:TextBox><a
                                                href="javascript:showdatepicker('txtSupportDate', 0, false,'DD.MM.YYYY')"><img alt="Date Picker"
                                                    src="Images/cal_date_picker.gif" border="0"></a>
                                        </td>
                                    </tr>
                       <tr class="TitleTextbox">
                           <td align="left" class="SectionRowLabel">
                               Закрыть период:</td>
                           <td align="left" class="SectionRow">
                                            <asp:DropDownList ID="DropDownList1" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
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
                                            <asp:DropDownList ID="DropDownList2" runat="server" BackColor="#F6F8FC" BorderWidth="1px">
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
                                                <asp:ListItem Value="2014">2014</asp:ListItem>
                                                <asp:ListItem Value="2015">2015</asp:ListItem>
                                                <asp:ListItem Value="2016">2016</asp:ListItem>
                                                <asp:ListItem Value="2017">2017</asp:ListItem>
                                                <asp:ListItem Value="2018">2018</asp:ListItem>
                                            </asp:DropDownList>
                           </td>
                       </tr>
                   </table>                                 
                                
                                <br />
                                <asp:ImageButton ID="btnAdd" runat="server" ImageUrl="Images/update.gif"></asp:ImageButton><br />
                                <br />
			    </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    <asp:Label ID="Label1" runat="server">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; История технического обслуживания</asp:Label>
                    
                </td>
            </tr>
            <tr>
                <td>
                    <br /><br />
                
<asp:DataGrid ID="grdSupportConductHistory" runat="server" Font-Size="9pt" Width="100%"
                                BackColor="White" BorderWidth="1px" BorderColor="#CC9966" AutoGenerateColumns="False"
                                BorderStyle="None" CellPadding="4">
                                <ItemStyle CssClass="itemGrid"></ItemStyle>
                                <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                                <FooterStyle CssClass="footerGrid"></FooterStyle>
                                <Columns>
                                    <asp:TemplateColumn HeaderText="Период ">
                                        <ItemTemplate>
                                            <asp:Label ID="lblPeriod" runat="server" Font-Size="9pt"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbledtPeriod" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Плательщик">
                                        <ItemTemplate>
                                            <asp:HyperLink ID="lnkPayer" runat="server"></asp:HyperLink>&nbsp;
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="executor" HeaderText="Исполнитель">
                                        <ItemTemplate>
                                            <asp:Label ID="lblExecutorTO" runat="server"></asp:Label>&nbsp;<br>
                                            <br>
                                            <asp:Label ID="lblUpdateRec" CssClass="TitleTextbox" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            Исполнитель:
                                            <asp:DropDownList ID="lstExecutor" runat="server" Height="22px" BackColor="#F6F8FC"
                                                Width="189px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="ТО">
                                        <ItemTemplate>
                                            <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <table id="Table7" cellspacing="1" cellpadding="1" width="100%" border="0">
                                                <tr>
                                                    <td colspan="3" class="SubCaption" align="left">
                                                        <asp:Label ID="lbledtStatus" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td class="SubCaption" align="right">
                                                        <asp:Label ID="lbltxtDate" runat="server" Visible="False">c&nbsp;</asp:Label>
                                                    </td>
                                                    <td class="SubCaption" align="right">
                                                        <asp:TextBox ID="txtDate" runat="server" Visible="False" BorderWidth="1px" BackColor="#F6F8FC"></asp:TextBox>
                                                    </td>
                                                    <td class="SubCaption" align="left">
                                                        <asp:Label ID="lbltxtPeriod1" runat="server" Visible="False">на&nbsp;</asp:Label>
                                                        <asp:TextBox ID="txtPeriod" runat="server" Visible="False" BorderWidth="1px" BackColor="#F6F8FC"
                                                            Width="20px"></asp:TextBox>
                                                        <asp:Label ID="lbltxtPeriod2" runat="server" Visible="False">&nbsp;мес.&nbsp;</asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Дополнительная информация">
                                        <ItemTemplate>
                                            <asp:Label ID="lblInfo" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="Label12" runat="server" CssClass="SubTitleTextbox">Дополнительная информация</asp:Label><br>
                                            <asp:TextBox ID="txtInfoEdit" runat="server" ToolTip="Введите дополнительную информацию"
                                                Height="63px" BorderWidth="1px" BackColor="#F6F8FC" Width="252px" TextMode="MultiLine"></asp:TextBox><br>
                                            <asp:Label ID="lblSaleDoc" runat="server" CssClass="SubTitleTextbox" Visible="False">Тип документа</asp:Label><br>
                                            <asp:DropDownList ID="lstSaleDoc" runat="server" Height="22px" BackColor="#F6F8FC"
                                                Visible="False">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Дата выполнения">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCloseDate" runat="server"></asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:Label ID="lbledtCloseDate" runat="server"></asp:Label>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="Документы">
                                        <ItemTemplate>
                                            <p style="margin-top: 0px; margin-bottom: 2px">
                                                <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ImageUrl="Images/edit.gif">
                                                </asp:ImageButton></p>
                                            <br>
                                            <p style="margin-top: 0px; margin-bottom: 2px">
                                                <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ImageUrl="Images/delete.gif">
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
                                <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                            </asp:DataGrid>
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

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.CustomerList" CodeFile="CustomerList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head runat ="server">
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
    rightmargin="0">
    <form id="frmCustomerList" method="post" runat="server" KeyPreview = "true">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table width="100%" border="0" cellpadding="2" cellspacing="1" class="PageTitle">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Клиенты</td>
            </tr>
        </table>
        <table cellpadding="2" cellspacing="1" width="100%">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Информация&nbsp;о&nbsp;клиентах</td>
                <td class="Unit" align="right">
                    <asp:HyperLink ID="btnNew" runat="server" NavigateUrl="NewRequest.aspx?0" CssClass="PanelHider"
                        EnableViewState="False">Новый&nbsp;клиент</asp:HyperLink></td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <asp:Label ID="msgCust" runat="server" EnableViewState="false" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2">
                </td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="2">
                    <p>
                        &nbsp;<asp:LinkButton ID="btnFilter" runat="server" CssClass="PanelHider" EnableViewState="False">
                            <asp:Image runat="server" ID="imgSelFilter" ImageUrl="Images/sel.gif" Style="z-index: 103;
                                position: relative; left: 10;"></asp:Image>&nbsp;Фильтр</asp:LinkButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:TextBox ID="txtFilter" runat="server" BorderWidth="1px" Width="220px"></asp:TextBox>&nbsp;&nbsp;
                        <asp:LinkButton ID="btnFind" runat="server" CssClass="PanelHider" EnableViewState="False">Искать</asp:LinkButton>&nbsp;
                        &nbsp;&nbsp;&nbsp;
                        <asp:LinkButton ID="lnkShowAll" runat="server" CssClass="PanelHider" EnableViewState="False">Показать всех клиентов</asp:LinkButton>&nbsp;&nbsp;
                        &nbsp;&nbsp;
                        <asp:LinkButton ID="lnkShowLastWeek" runat="server" CssClass="PanelHider" EnableViewState="False"
                            ToolTip="Выводятся клиенты, у которых есть заказы за последние 7 дней или, у которых нет заказов ">Показать клиентов за последние 7 дней</asp:LinkButton><asp:Panel
                                ID="pnlFilter" Style="border-top: #cc9933 1px solid; margin-top: 10px; z-index: 103;
                                margin-bottom: -8px; border-bottom: #cc9933 1px solid" runat="server">
                                <p style="margin-top: 8px; margin-bottom: 9px">
                                    <table width="100%">
                                        <tr>
                                            <td style="padding-left: 10px; text-justify: newspaper; font-size: 8pt; font-family: Verdana" width="100">
                                                Выделите интересующий Вас период времени (<i>день</i>, <i>неделя</i>, <i>месяц</i>).
                                                <br>
                                            </td>
                                            <td>
                                                <asp:Calendar ID="cal" runat="server" ForeColor="#0066CC" Width="220px" BorderWidth="1px"
                                                    Height="200px" BackColor="#FFFFCC" Font-Names="Verdana" Font-Size="8pt" ShowGridLines="True"
                                                    BorderColor="#FFCC66" FirstDayOfWeek="Monday" SelectionMode="DayWeekMonth" SelectWeekText="<img src='Images/selweek.gif' border=0 />"
                                                    SelectMonthText="<img src='Images/selmonth.gif' border=0/>">
                                                    <TodayDayStyle ForeColor="White" BackColor="#FFCC66"></TodayDayStyle>
                                                    <SelectorStyle BackColor="#FFCC66"></SelectorStyle>
                                                    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle>
                                                    <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle>
                                                    <SelectedDayStyle Font-Bold="True" BackColor="#CCCCFF"></SelectedDayStyle>
                                                    <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000">
                                                    </TitleStyle>
                                                    <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle>
                                                </asp:Calendar>
                                            </td>
                                        </tr>
                                    </table>
                                </p>
                            </asp:Panel>
                        <p>
                        
                        </p>
                </td>
            </tr>
            <tr>
                <td align="center" width="100%" colspan="2">
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
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkDogovor" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.dogovor") %>'
                                        CausesValidation="false" CommandName="ViewDetail">
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        № договора:</p>
                                    <asp:TextBox ID="txtDogovor" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.dogovor") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <asp:CheckBox ID="chkSupport" runat="server" CssClass="SubTitleEditbox" Text="ТО:"
                                        TextAlign="Left"></asp:CheckBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="customer_name" HeaderText="Организация">
                                <ItemTemplate>
                                    <asp:ImageButton ID="imgAlert" Style="margin-right: 4px" runat="server" ImageUrl="Images\sign.gif"
                                        Enabled="False"></asp:ImageButton>
                                    <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images\support.gif"
                                        ToolTip="На техобслуживании">
                                    </asp:HyperLink>
                                    <asp:LinkButton ID="lblCustomerName" CommandName="ViewDetail" runat="server"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Название:</p>
                                    <asp:TextBox ID="txtCustomerAbr" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.customer_abr") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtCustomerName" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.customer_name") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Регистрация:</p>
                                    <asp:TextBox ID="txtRegistration" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.registration") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Филиалы:</p>
                                    <asp:TextBox ID="txtBranch" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.branch") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Налоговая:</p>
                                    <asp:DropDownList ID="lstIMNS" runat="server" Width="100%">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtTaxInspection" runat="server" Width="100%" BorderWidth="1px"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.tax_inspection") %>' BorderStyle="Solid"
                                        Visible="False">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="boos_last_name" HeaderText="Руководители">
                                <ItemTemplate>
                                    <asp:Label ID="lblBoosAccountant" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Фамилия:</p>
                                    <asp:TextBox ID="txtLastName" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.boos_last_name") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Имя:</p>
                                    <asp:TextBox ID="txtFirstName" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.boos_first_name") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Отчество:</p>
                                    <asp:TextBox ID="txtPatronymicName" runat="server" Width="100%" BorderWidth="1px"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.boos_patronymic_name") %>' BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Глав. бух.:</p>
                                    <asp:TextBox ID="txtAccountant" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.accountant") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="unn" HeaderText="УНП, ОКЮЛП, НДС, ЦТО, ТО">
                                <HeaderStyle Width="75px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodes" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        УНП:</p>
                                    <asp:TextBox ID="txtUNN" runat="server" Width="70px" BorderWidth="1px" MaxLength="9"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.UNN") %>'>
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        ОКЮЛП:</p>
                                    <asp:TextBox ID="txtOKPO" runat="server" Width="70px" BorderWidth="1px" MaxLength="9"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.OKPO") %>'>
                                    </asp:TextBox><br>
                                    <asp:CheckBox ID="chkNDS" runat="server" CssClass="SubTitleEditbox" Text="НДС:" TextAlign="Left">
                                    </asp:CheckBox>
                                    <asp:CheckBox ID="chkCTO" runat="server" CssClass="SubTitleEditbox" Text="ЦТО:" TextAlign="Left">
                                    </asp:CheckBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="city" HeaderText="Адрес">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Индекс:</p>
                                    <asp:TextBox ID="txtZipcode" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.zipcode") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Область:</p>
                                    <asp:DropDownList ID="lstRegion" runat="server" Width="100%" CssClass="lstLineUp"
                                        Height="18px">
                                        <asp:ListItem></asp:ListItem>
                                        <asp:ListItem Value="Минская обл.">Минская</asp:ListItem>
                                        <asp:ListItem Value="Брестская обл.">Брестская</asp:ListItem>
                                        <asp:ListItem Value="Витебская обл.">Витебская</asp:ListItem>
                                        <asp:ListItem Value="Гомельская обл.">Гомельская</asp:ListItem>
                                        <asp:ListItem Value="Гродненская обл.">Гродненская</asp:ListItem>
                                        <asp:ListItem Value="Могилевская обл.">Могилевская</asp:ListItem>
                                    </asp:DropDownList><br>
                                    <p class="SubTitleEditbox">
                                        Район:</p>
                                    <asp:TextBox ID="txtRegion" runat="server" Width="100%" BorderWidth="1px" BorderStyle="Solid"></asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Город:</p>
                                    <asp:DropDownList ID="lstCityAbr" runat="server" Width="22%" CssClass="lstLineUp"
                                        Height="18px">
                                        <asp:ListItem Value="г.">г.</asp:ListItem>
                                        <asp:ListItem Value="г.п.">г.п.</asp:ListItem>
                                        <asp:ListItem Value="д.">д.</asp:ListItem>
                                        <asp:ListItem Value="пос.">пос.</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtCity" runat="server" Width="78%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.city") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Адрес:</p>
                                    <asp:DropDownList ID="lstStreetAbr" runat="server" Width="27%" CssClass="lstLineUp"
                                        Height="18px">
                                        <asp:ListItem Value="ул.">ул.</asp:ListItem>
                                        <asp:ListItem Value="пр.">пр.</asp:ListItem>
                                        <asp:ListItem Value="пер.">пер.</asp:ListItem>
                                        <asp:ListItem Value="б-р">б-р</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtAddress" runat="server" Width="73%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.address") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Почтовый адрес:</p>
                                    <asp:TextBox ID="txt_post_adress" runat="server" Width="73%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.post_adress")%>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        E-mail:</p>
                                    <asp:TextBox ID="txt_email" runat="server" Width="73%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.email")%>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Телефоны">
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPhone" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Факс:</p>
                                    <asp:TextBox ID="txtPhone1" runat="server" Width="100%" BorderWidth="1px" MaxLength="25"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.phone1") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Рабочий:</p>
                                    <asp:TextBox ID="txtPhone2" runat="server" Width="100%" BorderWidth="1px" MaxLength="25"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.phone2") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Домашний:</p>
                                    <asp:TextBox ID="txtPhone3" runat="server" Width="100%" BorderWidth="1px" MaxLength="25"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.phone3") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Мобильный:</p>
                                        
                                    <asp:TextBox ID="txtPhone4" runat="server" Width="100%" BorderWidth="1px" MaxLength="25"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.phone4") %>'>
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Для СМС:</p>
                                    <asp:TextBox ID="txtPhoneNotice" runat="server" ToolTip="Введите телефон для СМС" BackColor="#F6F8FC" Width="100%" MaxLength="250" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.phone_notice") %>'/>
                                    <ajaxToolkit:MaskedEditValidator ID="txtPhoneNotice_MaskedEditValidator" runat="server" ControlExtender="txtPhoneNotice_MaskedEditExtender" ControlToValidate="txtPhoneNotice" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="Введен некорректный мобильный телефон!" InvalidValueBlurredMessage="Введен некорректный мобильный телефон!" InvalidValueMessage="Введен некорректный мобильный телефон!" IsValidEmpty="True" ValidationExpression="^(29|25|44|33)(\d{7})$" ValidationGroup="GroupName">+375 (99) 999-99-99</ajaxToolkit:MaskedEditValidator>
                                    <ajaxToolkit:MaskedEditExtender ID="txtPhoneNotice_MaskedEditExtender" runat="server" BehaviorID="txtPhoneNotice_MaskedEditExtender" TargetControlID="txtPhoneNotice" Mask="+375 (99) 999-99-99" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Банк">
                                <ItemTemplate>
                                    <asp:Label ID="lblBank" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    
                                    <p class="SubTitleEditbox">
                                    Рекламные сведения:</p>
                                     <asp:DropDownList ID="lstAdvert" runat="server" Width="100%" BorderWidth="1px"
                                        BorderStyle="Solid">
                                    </asp:DropDownList>
                                    
                                    <p class="SubTitleEditbox">
                                        Код банка:</p>
                                    <asp:DropDownList ID="lstBankCode" runat="server" Width="100%" BorderWidth="1px"
                                        BorderStyle="Solid">
                                    </asp:DropDownList><br>
                                    <p class="SubTitleEditbox">
                                        Адрес банка:</p>
                                    <asp:TextBox ID="txtBankAddress" runat="server" Width="100%" BorderWidth="1px" BorderStyle="Solid"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.bank_address") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Расчетный счет:</p>
                                    <asp:TextBox ID="txtBankAccount" runat="server" Width="100%" BorderWidth="1px" BorderStyle="Solid"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.bank_account") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Заметки:&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:CheckBox ID="chkAlert" runat="server" Text="Беспокойный клиент"></asp:CheckBox></p>
                                    <asp:TextBox ID="txtInfo" runat="server" Width="100%" BorderWidth="1px" MaxLength="250"
                                        BorderStyle="Solid" Text='<%# DataBinder.Eval(Container, "DataItem.info") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <p style="margin-top: 2px; margin-bottom: 2px">
                                        <asp:ImageButton ID="cmdEdit" runat="server" CommandName="Edit" ImageUrl="Images/edit.gif">
                                        </asp:ImageButton></p>
                                    <p style="margin-top: 0px; margin-bottom: 2px">
                                        <asp:ImageButton ID="cmdDelete" runat="server" CommandName="Delete" ImageUrl="Images/delete.gif">
                                        </asp:ImageButton></p>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p>
                                        <asp:ImageButton ID="cmdUpdate" runat="server" CommandName="Update" ImageUrl="Images/update.gif">
                                        </asp:ImageButton></p>
                                    <p>
                                        <asp:ImageButton ID="cmdCancel" runat="server" CommandName="Cancel" ImageUrl="Images/cancel.gif">
                                        </asp:ImageButton></p>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid></td>
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

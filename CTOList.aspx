<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.CTOList" CodeFile="CTOList.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Дилеры]</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio.NET 7.0">
    <meta name="CODE_LANGUAGE" content="Visual Basic 7.0">
    <meta name="vs_defaultClientScript" content="JavaScript">
    <meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">
    <form id="frmNewRequest" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Дилеры</td>
            </tr>
        </table>
        <table width="100%" cellpadding="2" cellspacing="1" border="0">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Информация&nbsp;о&nbsp;ЦТО
                </td>
                <td class="Unit" align="right">
                    <asp:HyperLink ID="lnkNewClient" runat="server" CssClass="PanelHider" NavigateUrl="NewRequest.aspx">Новый&nbsp;клиент</asp:HyperLink></td>
            </tr>
            <tr>
                <td width="100%" colspan="2">
                    <asp:Label ID="msgCTO" EnableViewState="false" runat="server" ForeColor="#ff0000"
                        Font-Bold="true"></asp:Label></td>
            </tr>
            <tr height="10">
                <td width="100%" colspan="2">
                    &nbsp;</td>
            </tr>
            <tr>
                <td align="center" width="100%" colspan="2">
                    <asp:DataGrid ID="grdCTO" runat="server" Width="98%" AllowSorting="True" CellPadding="1"
                        AutoGenerateColumns="False" BorderColor="#CC9966" BorderWidth="1px">
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:BoundColumn Visible="False" DataField="customer_sys_id"></asp:BoundColumn>
                            <asp:TemplateColumn SortExpression="dogovor" HeaderText="№" >
                                <ItemTemplate>
                                    <asp:LinkButton runat="server" ID="lnkDogovor2" CommandName="ViewDetail" CausesValidation="false"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.dogovor") %>'>
                                    </asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        № договора:</p>
                                    <asp:TextBox ID="txtDogovor2" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.dogovor") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="customer_name" HeaderText="Организация">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lblCustomerName2" runat="server" CommandName="ViewDetail"></asp:LinkButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Название:</p>
                                    <asp:TextBox ID="txtCustomerAbr" runat="server" Width="17%" Text='<%# DataBinder.Eval(Container, "DataItem.customer_abr") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                    <asp:TextBox ID="txtCustomerName2" runat="server" Width="83%" Text='<%# DataBinder.Eval(Container, "DataItem.customer_name") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Регистрация:</p>
                                    <asp:TextBox ID="txtRegistration2" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.registration") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Филиалы:</p>
                                    <asp:TextBox ID="txtBranch2" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.branch") %>'
                                        BorderStyle="Solid" BorderWidth="1px">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        Налоговая:</p>
                                    <asp:DropDownList ID="lstIMNS" runat="server" Width="100%">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtTaxInspection2" runat="server" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.tax_inspection") %>'
                                        BorderStyle="Solid" BorderWidth="1px" Visible="False">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="boos_last_name" HeaderText="Руководители">
                                <ItemTemplate>
                                    <asp:Label ID="lblBoosAccountant2" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Фамилия:</p>
                                    <asp:TextBox ID="txtLastName2" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.boos_last_name") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Имя:</p>
                                    <asp:TextBox ID="txtFirstName2" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.boos_first_name") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Отчество:</p>
                                    <asp:TextBox ID="txtPatronymicName2" runat="server" Width="100%" BorderWidth="1px"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.boos_patronymic_name") %>' BorderStyle="Solid">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Глав. бух.:</p>
                                    <asp:TextBox ID="txtAccountant2" runat="server" Width="100%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.accountant") %>'
                                        BorderStyle="Solid">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="unn" HeaderText="УНП, ОКЮЛП, НДС">
                                <HeaderStyle Width="75px"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblCodes2" runat="server"></asp:Label>&nbsp;
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        УНП:</p>
                                    <asp:TextBox ID="txtUNN2" runat="server" BorderWidth="1px" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.UNN") %>'
                                        BorderStyle="Solid" MaxLength="9">
                                    </asp:TextBox>
                                    <p class="SubTitleEditbox">
                                        ОКЮЛП:</p>
                                    <asp:TextBox ID="txtOKPO2" runat="server" BorderWidth="1px" Width="70px" Text='<%# DataBinder.Eval(Container, "DataItem.OKPO") %>'
                                        BorderStyle="Solid" MaxLength="9">
                                    </asp:TextBox><br>
                                    <asp:CheckBox ID="chkNDS2" CssClass="SubTitleEditbox" runat="server" Text="НДС:"
                                        TextAlign="Left"></asp:CheckBox>
                                    <asp:CheckBox ID="chkCTO2" CssClass="SubTitleEditbox" runat="server" Text="ЦТО:"
                                        TextAlign="Left" Checked="true"></asp:CheckBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="city" HeaderText="Адрес">
                                <ItemTemplate>
                                    <asp:Label ID="lblAddress2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Индекс:</p>
                                    <asp:TextBox ID="txtZipcode2" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.zipcode") %>'
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
                                    <asp:TextBox ID="txtCity2" runat="server" Width="78%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.city") %>'
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
                                    <asp:TextBox ID="txtAddress2" runat="server" Width="73%" BorderWidth="1px" Text='<%# DataBinder.Eval(Container, "DataItem.address") %>'
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
                                    <asp:Label ID="lblPhone2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Факс:</p>
                                    <asp:TextBox ID="txtPhone12" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.phone1") %>'
                                        BorderStyle="Solid" MaxLength="20">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Рабочий:</p>
                                    <asp:TextBox ID="txtPhone22" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.phone2") %>'
                                        BorderStyle="Solid" MaxLength="20">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Домашний:</p>
                                    <asp:TextBox ID="txtPhone32" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.phone3") %>'
                                        BorderStyle="Solid" MaxLength="20">
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Мобильный:</p>
                                    <asp:TextBox ID="txtPhone42" runat="server" BorderWidth="1px" Width="100%" Text='<%# DataBinder.Eval(Container, "DataItem.phone4") %>'
                                        BorderStyle="Solid" MaxLength="20">
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
                                    <asp:Label ID="lblBank2" runat="server"></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p class="SubTitleEditbox">
                                        Код банка:</p>
                                    <asp:DropDownList ID="lstBankCode2" runat="server" BorderWidth="1px" Width="100%"
                                        BorderStyle="Solid">
                                    </asp:DropDownList><br>
                                    <p class="SubTitleEditbox">
                                        Адрес банка:</p>
                                    <asp:TextBox ID="txtBankAddress2" runat="server" BorderWidth="1px" Width="100%" BorderStyle="Solid"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.bank_address") %>'>
                                    </asp:TextBox><br>
                                    <p class="SubTitleEditbox">
                                        Расчетный счет:</p>
                                    <asp:TextBox ID="txtBankAccount2" runat="server" BorderWidth="1px" Width="100%" BorderStyle="Solid"
                                        Text='<%# DataBinder.Eval(Container, "DataItem.bank_account") %>'>
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn>
                                <HeaderStyle Width="100px"></HeaderStyle>
                                <ItemTemplate>
                                    <p style="margin-top: 2px; margin-bottom: 2px">
                                        <asp:ImageButton ID="cmdEdit2" runat="server" CommandName="Edit" ImageUrl="Images/edit.gif">
                                        </asp:ImageButton></p>
                                    <p style="margin-top: 0px; margin-bottom: 2px">
                                        <asp:ImageButton ID="cmdDelete2" runat="server" CommandName="Delete" ImageUrl="Images/delete.gif"
                                            Visible="False"></asp:ImageButton></p>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <p>
                                        <asp:ImageButton ID="cmdUpdate2" runat="server" CommandName="Update" ImageUrl="Images/update.gif">
                                        </asp:ImageButton></p>
                                    <p>
                                        <asp:ImageButton ID="cmdCancel2" runat="server" CommandName="Cancel" ImageUrl="Images/cancel.gif">
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
        <input runat="server" id="scrollPos" type="hidden" value="0" name="scrollPos">
        <input runat="server" id="CurrentPage" type="hidden" lang="ru" name="CurrentPage">
        <input runat="server" id="Parameters" type="hidden" lang="ru" name="Parameters">
        <input runat="server" id="FindHidden" type="hidden" name="FindHidden">
    </form>
</body>
</html>

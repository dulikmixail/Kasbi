<%@ Reference Page="Documents.aspx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Repair" CodeFile="Repair.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[Ремонт кассовых аппаратов]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="onload=&quot;javascript:document.body.scrollTop=document.all['scrollPos'].value;&quot;"
    rightmargin="0">
    <form id="frmRepairs" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Карточка ККМ -&gt; Ремонт&nbsp;ККМ</td>
            </tr>
        </table>
        <table cellspacing="1" cellpadding="2" width="100%">
            <tr class="Unit">
                <td class="Unit">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Кассовый аппарат</td>
                <td class="Unit" align="right">
                    <asp:LinkButton ID="lnkGoods" runat="server" EnableViewState="False" CssClass="PanelHider">
								Товары</asp:LinkButton></td>
            </tr>
            <tr>
                <td colspan="2">
                    &nbsp;
                    <asp:Label ID="msg" runat="server" EnableViewState="False" Font-Size="8pt" ForeColor="Red"
                        Font-Bold="True"></asp:Label></td>
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
                                <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                    ToolTip="На техобслуживании"></asp:HyperLink>
                                <asp:ImageButton ID="imgRepair" runat="server" ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                </asp:ImageButton>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblCashType" runat="server" Font-Bold="true"></asp:Label>
                                <asp:Label ID="lblSoftwareVersion" runat="server" Font-Bold="true"></asp:Label>
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
                            <td colspan="2"  >
                                <asp:Label ID="lblCaptionMarka" runat="server" CssClass="cash">Марка ЦТО:</asp:Label></td>
                            <td  >
                                <asp:Label ID="lblMarka" runat="server" CssClass="cashDetail"></asp:Label></td>
                        </tr>
                        <tr height="0">
                            <td colspan="2" class="cash" >
                                <asp:Label ID="lblCaptionDateCreated" runat="server" CssClass="cash">Добавлен в базу:</asp:Label></td>
                            <td >
                                <asp:Label ID="lblDateCreated" runat="server" CssClass="cashDetail"></asp:Label></td>
                        </tr>
                        <tr height="0">
                            <td colspan="2" >
                                <asp:Label ID="lblCaptionWorker" runat="server" CssClass="cash">Добавил:</asp:Label></td>
                            <td >
                                <asp:Label ID="lblWorker" runat="server" CssClass="cashDetail"></asp:Label></td>
                        </tr>
                        <tr height="0">
                            <td colspan="2" >
                                <asp:Label ID="lblCaptionSetPlace" runat="server" CssClass="cash">Место установки:</asp:Label></td>
                            <td >
                                <asp:Label ID="lblSetPlace" runat="server" CssClass="cashDetail"></asp:Label></td>
                        </tr>
                        <tr height="0">
                            <td colspan="2" >
                                <asp:Label ID="lblCaptionSale" runat="server" CssClass="cash">Продан:</asp:Label></td>
                            <td >
                                <asp:HyperLink ID="lblSale" runat="server" CssClass="cashDetail"></asp:HyperLink></td>
                        </tr>
                        <tr height="0">
                            <td colspan="2" >
                                <asp:Label ID="lblOwner" runat="server" CssClass="cash">Плательщик:</asp:Label></td>
                            <td >
                                <asp:HyperLink ID="lnkOwner" runat="server" CssClass="cashDetail"></asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td align="left" colspan="2" >
                                <asp:Label ID="lblCaptionSupport" runat="server" CssClass="cash">ТО:</asp:Label></td>
                            <td align="left" >
                                <asp:Label ID="lblSupport" runat="server" CssClass="cashDetail"></asp:Label>
                                <asp:Label ID="lblSupportSKNO" runat="server" CssClass="cashDetail"></asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <%--<tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Прием ККМ в ремонт
                </td>
            </tr>
            <tr>
                <td class="SectionRow" colspan="3">
                    <asp:LinkButton ID="lnkRepairIN" runat="server" CssClass="LinkButton">
                        <asp:Image runat="server" ID="Image2" ImageUrl="Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>
                        &nbsp;Принять ККМ в ремонт&nbsp;</asp:LinkButton></td>
            </tr>--%>
            <tr>
                <td colspan="3" height="5">
                    &nbsp;</td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">
                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;История&nbsp;ремонта</td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:Label ID="msgHistory" runat="server" EnableViewState="False" Font-Size="8pt"
                        ForeColor="Red" Font-Bold="True"></asp:Label></td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    &nbsp;
                    <asp:DataGrid ID="grdRepairs" runat="server" Font-Size="9pt" BorderWidth="1px" BackColor="White"
                        BorderColor="#CC9966" AutoGenerateColumns="False" CellPadding="4" BorderStyle="None">
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid"></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="Дата выполнения">
                                <ItemTemplate>
                                    <asp:Label ID="lblDates" runat="server" Font-Size="9pt">Label</asp:Label>
                                    <br>
                                    <br>
                                    <p style="margin-top: 2px; margin-bottom: 2px">
                                        <asp:ImageButton ID="cmdEdit" runat="server" ImageUrl="Images/edit.gif" CommandName="Edit">
                                        </asp:ImageButton></p>
                                    <p>
                                        <asp:ImageButton ID="cmdDelete" runat="server" ImageUrl="Images/delete.gif" CommandName="Delete">
                                        </asp:ImageButton></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="executor" HeaderText="Исполнитель">
                                <ItemTemplate>
                                    <asp:Label ID="lblExecutor" runat="server"></asp:Label>&nbsp;<br>
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
                            <asp:TemplateColumn HeaderText="Марки, Z-отчёт, итог">
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server"></asp:Label>
                                    <!--    <p>
                                        <asp:Label ID="lblMarkaCTOIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterMarkaCTOOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblMarkaCTOOut" runat="server"></asp:Label><br>
                                       <asp:Label ID="lblMarkaReestrIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterMarkaReestrOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblMarkaReestrOut" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblMarkaPZUIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterMarkaPZUOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblMarkaPZUOut" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblMarkaMFPIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterMarkaMFPOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblMarkaMFPOut" runat="server"></asp:Label><br>
                                        
                                         <asp:Label ID="lblZReportIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterZReportOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblZReportOut" runat="server"></asp:Label><br>
                                        <asp:Label ID="lblItogIn" runat="server"></asp:Label>
                                        <asp:Label ID="lblDelimeterItogOut" runat="server">&nbsp;/</asp:Label>
                                        <asp:Label ID="lblItogOut" runat="server"></asp:Label>-->
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Сумма">
                                <ItemTemplate>
                                    <asp:Label ID="lblCost" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Детали / работы">
                                <ItemTemplate>
                                    <asp:Label ID="lblCaptionDetails" runat="server" CssClass="TitleTextbox" Width="37px">Детали:</asp:Label>
                                    <asp:Label ID="lblDetails" runat="server"></asp:Label>
                                    <br>
                                    <asp:Label ID="lblCaptionInfo" runat="server" CssClass="TitleTextbox" Width="37px">Работы:</asp:Label>
                                    <asp:Label ID="lblInfo" runat="server"></asp:Label>
                                    <br>
                                    <asp:Label ID="lblCaptionRepairInfo" runat="server" CssClass="TitleTextbox">Доп. инф-ция:</asp:Label>
                                    <asp:Label ID="lblRepairInfo" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Документы">
                                <ItemTemplate>
                                    <asp:HyperLink ID="lnkActRepairIN_OUT" runat="server" Target="_blank" Text="Акт о принятии в ремонт"></asp:HyperLink><br>
                                    <br>
                                    <asp:HyperLink ID="lnkRepairAct" runat="server" Target="_blank"></asp:HyperLink><br>
                                    <br>
                                    <asp:HyperLink ID="lnkActRepairRealization" runat="server" Target="_blank"></asp:HyperLink><br>
                                    <br>
                                    <asp:HyperLink ID="lnkTTNRepair" runat="server" Target="_blank"></asp:HyperLink><br>
                                    <br>
                                    <asp:HyperLink ID="lnkInvoiceNDS" runat="server" Target="_blank"></asp:HyperLink><br>
                                    <br>
                                    <asp:LinkButton ID="btnDeleteRepairDoc" runat="server" CommandName="DeleteDoc"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" ForeColor="#330099" BackColor="#FFFFCC"></PagerStyle>
                    </asp:DataGrid></td>
            </tr>
            <tr>
                <td colspan="2" height="15">
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

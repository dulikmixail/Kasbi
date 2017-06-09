<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin._Default" CodeFile="Default.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html>
<head  runat ="server">
    <title> [Администрирование]</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="C#" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../styles.css" type="text/css" rel="stylesheet">
</head>
<body bottommargin="0" leftmargin="0" topmargin="0" rightmargin="0">
    <form id="Form1" method="post" runat="server">
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Администрирование&nbsp;</td>
            </tr>
        </table>
        <table cellpadding="2" cellspacing="1" width="100%" border="0" style="font-size:12">
            <tr>
                <td class="Unit">
                    &nbsp;Список пользователей</td>
            </tr>
            <tr>
                <td class="SectionRow">
                    <asp:HyperLink ID="Hyperlink2" runat="server" CssClass="PanelHider" NavigateUrl="Users.aspx"
                        EnableViewState="False">
                        <asp:Image runat="server" ID="Image1" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Пользователи</asp:HyperLink>
                </td>
            </tr>
            <tr>
                <td class="Unit">&nbsp;Импорт данных</td>
            </tr>
            <tr>
                <td class="SectionRow">
                    <asp:HyperLink ID="lnkTO" runat="server" CssClass="PanelHider" NavigateUrl="ImportTO.aspx"
                        EnableViewState="False">
                        <asp:Image runat="server" ID="Image3" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Импорт долгов из 1С</asp:HyperLink>
                 </td>
            </tr>
            <tr>
                <td class="SectionRow">
                    <asp:HyperLink ID="lnkImportDelivery" runat="server" CssClass="PanelHider" NavigateUrl="ImportDelivery.aspx"
                        EnableViewState="False">
                        <asp:Image runat="server" ID="Image17" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                            position: relative; left: 10;"></asp:Image>&nbsp;Импорт поставок из 1С</asp:HyperLink>                            
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Unit">
                    &nbsp;Справочники</td>
            </tr>
            <tr>
                <td valign="top">
                    <table width="100%" border="0" style="font-size:12">
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkGoodGroups" runat="server" EnableViewState="False" NavigateUrl="GoodGroups.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image10" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Группы&nbsp;товаров</asp:HyperLink>
                            </td>
                           <td class="SectionRow">
                                <asp:HyperLink ID="lnkDelivery" runat="server" CssClass="PanelHider" NavigateUrl="Delivery.aspx" EnableViewState="False">
                                    <asp:Image runat="server" ID="imgSelNew" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Поставки</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkGoodTypes" runat="server" EnableViewState="False" NavigateUrl="GoodTypes.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image7" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Типы&nbsp;товаров</asp:HyperLink>
                            </td>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkPrice" runat="server" EnableViewState="False" NavigateUrl="PriceList.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image5" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Прейскуранты</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkPriceTO" runat="server" EnableViewState="False" NavigateUrl="PriceTO.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image14" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Стоимость ТО</asp:HyperLink>
                            </td>
                            <td class="SectionRow">
                            <asp:HyperLink ID="lnkRepaiBads" runat="server" EnableViewState="False" NavigateUrl="RepairBads.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image15" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Типичные неисправности</asp:HyperLink>
                            
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkIMNS" runat="server" CssClass="PanelHider" NavigateUrl="IMNS.aspx" EnableViewState="False">
                                    <asp:Image runat="server" ID="Image6" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Налоговые инспекции</asp:HyperLink>
                            </td>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkSuppliers" runat="server" EnableViewState="False" NavigateUrl="Suppliers.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image2" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Поставщики</asp:HyperLink></td>
                        </tr>
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkRegionPlace" runat="server" EnableViewState="False" NavigateUrl="RegionPlace.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image4" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Районы установки ККМ</asp:HyperLink></td>
                        <td class="SectionRow">
                                <asp:HyperLink ID="lnkDetails" runat="server" EnableViewState="False" NavigateUrl="Details.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image8" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Детали и работы</asp:HyperLink>
                            </td>                 </tr>
                       
                            <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkAdvertising" runat="server" EnableViewState="False" NavigateUrl="Advertising.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image9" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Реклама</asp:HyperLink>
                            </td>
                            <td class="SectionRow"> 
                              <asp:HyperLink ID="lnkFirm" runat="server" EnableViewState="False" NavigateUrl="Firms.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image13" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Фирмы</asp:HyperLink>
                           
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkUnits" runat="server" EnableViewState="False" NavigateUrl="Units.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image12" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Единицы&nbsp;измерения</asp:HyperLink>
                            </td>
                            <td class="SectionRow">
                                <asp:HyperLink ID="lnkBanks" runat="server" EnableViewState="False" NavigateUrl="BankList.aspx"  CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image11" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Банки</asp:HyperLink>
                            </td>
                        </tr>
                        <tr>
                            <td class="SectionRow" style="height: 25px">
                                <asp:HyperLink ID="lnkClientRubr" runat="server" EnableViewState="False" NavigateUrl="ClietnRubr.aspx" CssClass="PanelHider">
                                    <asp:Image runat="server" ID="Image16" ImageUrl="../Images/sel.gif" Style="z-index: 103;
                                        position: relative; left: 10;"></asp:Image>&nbsp;Рубрики деятельности клиентов</asp:HyperLink>
                            </td>
                             <td class="SectionRow" style="height: 25px">
                                 <asp:LinkButton ID="lnk_clear_repair" runat="server" CssClass="PanelHider" EnableViewState="False"><img id="Image11" src="../Images/sel.gif" border="0" style="z-index: 103;position: relative; left: 10;">&nbsp;Очистить статусы ККМ в ремонте</asp:LinkButton></td>
                        </tr>                        
                        
                    </table>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="Unit">
                    &nbsp;Маркетинг</td>
            </tr> 
                       
            <tr>
                <td height="10">
 <br /><br /><br />
<asp:linkbutton id="lnk_export" runat="server" CssClass="PanelHider" EnableViewState="False">
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;Экспорт</asp:linkbutton>                
                
                </td>
            </tr>
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    </form>
</body>
</html>

<%@ Reference Page="~/Documents.aspx" %> 
<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kasbi.Sale" CodeFile="Sale.ascx.vb" %>
<table id="tblMain" width="98%">
    <tr>
        <td colspan="2">
            <asp:Label ID="msgSale" runat="server" Font-Bold="true" EnableViewState="false" ForeColor="#ff0000"
                CssClass="PanelHider"></asp:Label></td>
    </tr>
    <tr class="HeaderField">
        <td>
            <asp:Label ID="HeaderSale" runat="server"></asp:Label></td>
        <td align="right" width="10%">
            <asp:Label ID="DateSale" runat="server"></asp:Label></td>
    </tr>
    <tr class="SectionRowLabel">
        <td class="SectionRow" colspan="2">
            &nbsp;Ответственный:&nbsp;<asp:Label ID="Saler" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2">
            <table width="100%">
                <tr class="DetailField">
                    <td align=right colspan="8" class="SectionRow" width="100%">
                        Документы:&nbsp;&nbsp;&nbsp;
                        
                        <asp:HyperLink ID="lnkInvoice" runat="server" Target="_blank">Договор/Счет&nbsp;фактура</asp:HyperLink>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:HyperLink Visible="false" ID="lnkTTN" runat="server" Target="_blank">Накладная</asp:HyperLink>
                        <asp:LinkButton Visible="false" ID="btnTTN" runat="server">Накладная</asp:LinkButton>
                        <asp:HyperLink ID="lnkGarantia" runat="server" Target="_blank">Гарантийный талон</asp:HyperLink>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:HyperLink ID="lnkSpisok_KKM" runat="server" Target="_blank">Список&nbsp;ККМ</asp:HyperLink>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:HyperLink ID="lnkZayavlenie_IMNS" runat="server" Target="_blank">Заявление&nbsp;в&nbsp;ИМНС</asp:HyperLink>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:HyperLink ID="lnkIzveschenie" runat="server" Target="_blank">Извещение</asp:HyperLink>
                        &nbsp;&nbsp;|&nbsp;&nbsp;
                        <asp:LinkButton ID="btnCreateDocuments" runat="server">Сформировать весь комплект документов</asp:LinkButton><asp:LinkButton
                        ID="btnPrint1" runat="server" Visible="False" ToolTip="Печать 1-й страницы Договора на ТО(3 шт.), Договора / Счет-фактуры, Списка ККМ, Акта списания показаний, Тех. заключения и Удостоверения кассира">Печать экз. 1</asp:LinkButton>
                    </td>
                </tr>
                <tr class="DetailField">
                <asp:Panel ID="pnl_docs" runat="server" >
                    <td colspan="4" class="SectionRow" style="background-color:Silver; padding:3px">
                        <asp:HyperLink Visible="false" ID="lnkTTN_Transport" runat="server" Target="_blank">ТТН</asp:HyperLink>&nbsp;&nbsp;
                        <asp:LinkButton Visible="false" ID="btnTTNT" runat="server">ТТН</asp:LinkButton>
                        
                        
                        <asp:ListBox ID="type_doc" Rows="1" runat="server">
                            <asp:ListItem Value="1">Накладная</asp:ListItem>
                            <asp:ListItem Value="3">ТТН</asp:ListItem>   
                            <asp:ListItem Value="2">Счет-фактура по ндс</asp:ListItem>
                        </asp:ListBox>&nbsp;&nbsp;<b>Номер:</b>&nbsp;<asp:TextBox ID="doc_num" Columns="9" runat="server"></asp:TextBox>&nbsp;&nbsp;<asp:LinkButton ID="btndocs" runat="server"><b>Сформировать</b></asp:LinkButton>&nbsp;
                       
                       
                    </td>
                     </asp:Panel>
                    <td class="SectionRow">
                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:HyperLink Visible="false" ID="lnkInvoiceNDS" runat="server" Target="_blank">Счет&nbsp;фактура&nbsp;по&nbsp;НДС</asp:HyperLink>
                        <asp:HyperLink Visible="false" ID="lnkAktKKM" runat="server" Target="_blank">Счет&nbsp;фактура&nbsp;по&nbsp;НДС</asp:HyperLink>
                        <asp:LinkButton Visible="false" ID="btnInvoiceNDS" runat="server">Счет&nbsp;фактура&nbsp;по&nbsp;НДС</asp:LinkButton>                    
                    </td>
                    <td class="SectionRow">
                        <asp:HyperLink ID="lnkDogovor_Na_TO" runat="server" Target="_blank">Договор&nbsp;на&nbsp;тех.&nbsp;обслуживание</asp:HyperLink>
                    </td>
                    <td><asp:Button Width="170" ID="add_new_goods" runat="server" Text="Внести оборудование" Visible="false" /></td>
                    <td class="SectionRow" align="right">
                        &nbsp;<asp:HyperLink ID="lnkZayavlenieNaKniguKassira" runat="server" Target="_blank"
                            Visible="False">Заявление&nbsp;на&nbsp;книгу&nbsp;кассира</asp:HyperLink><asp:HyperLink
                                ID="lnkPrint" runat="server" Visible="False" NavigateUrl="\\by-mn-srv3\PrintDocs$\PrintDocs.exe">Печать</asp:HyperLink><asp:HyperLink
                                    ID="lnkZayavlenie" runat="server" Target="_blank" Visible="False">Заявление</asp:HyperLink><asp:LinkButton
                                        ID="btnPrint2" runat="server" Visible="False" ToolTip="Печать 2-й страницы Договора на ТО(3 шт.), Договора / Счет-фактуры, Списка ККМ и Акта списания показаний">Печать экз. 2</asp:LinkButton></td>
                
                </tr>
            </table>
        </td>
    </tr>
    <tr class="DetailField">
        <td colspan="2">
        </td>
    </tr>
    <tr class="DetailField">
        <td colspan="2">
        </td>
    </tr>
    <tr>
        <td class="SectionRow" colspan="2">
            <asp:DataGrid ID="grdSale" runat="server" Width="100%" ShowFooter="True" AutoGenerateColumns="False"
                Font-Size="9pt" BorderColor="#CC9966" BorderWidth="1px">
                <HeaderStyle Font-Size="8pt" Font-Underline="True" HorizontalAlign="Center" ForeColor="#9C0001">
                </HeaderStyle>
                <ItemStyle CssClass ="itemGrid" />
                <FooterStyle Font-Bold="True" HorizontalAlign="Right"></FooterStyle>
                <Columns>
                    <asp:TemplateColumn HeaderText="№">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label runat="server" ID="lblNumGood" Font-Size="7pt" ForeColor="#9C0001"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                              <asp:Label runat="server" ID="lblNumGoodEdit" Font-Size="7pt" ForeColor="#9C0001"></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="good_name" HeaderText="Товар">
                        <ItemTemplate>
                            <asp:Label ID="lblGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'></asp:Label>
                        </ItemTemplate>
                         <EditItemTemplate>
                           <asp:Label ID="lblGoodNameEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'></asp:Label>
                        </EditItemTemplate>
                        <FooterStyle HorizontalAlign="Left" Font-Bold="false"></FooterStyle>
                        <FooterTemplate>
                            <asp:Label ID="lblTotalCountByCash" ForeColor="#9C0001" runat="server" Font-Size="7pt"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="good_description" HeaderText="Описание">
                        <ItemTemplate>
                            <asp:Label ID="lblGoodDesc" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_description") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblGoodDescEdit" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_description") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Цена">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblPrice" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblPriceEdit" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.price") %>'></asp:Label>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="quantity" HeaderText="Кол-во">
                        <ItemStyle HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblQuantity" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="txtQuantityEdit" runat="server"  Width="50" Text='<%# DataBinder.Eval(Container, "DataItem.quantity") %>'></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Стоимость" FooterText="Итого:">
                        <ItemStyle Font-Bold="True" HorizontalAlign="Right"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblCost" runat="server"></asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:Label ID="lblCostEdit" runat="server"></asp:Label>
                        </EditItemTemplate>
                        <FooterTemplate>
                            Итого:
                            <asp:Label ID="lblTotal" runat="server"></asp:Label>
                        </FooterTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn SortExpression="info" HeaderText="Доп. инф.">
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Акт">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkAkt_Pokazaniy" runat="server" Target="_blank" ToolTip="Акт о снятии показаний счетчика">...</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Тех.&lt;br&gt;закл.">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkTeh_Zaklyuchenie" runat="server" Target="_blank" ToolTip="Техническое заключение">...</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn HeaderText="Удост.&lt;br&gt;кассира">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:HyperLink ID="lnkUdostoverenie_Kassira" runat="server" Target="_blank" ToolTip="Техническое заключение">...</asp:HyperLink>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:TemplateColumn>
                        <ItemStyle HorizontalAlign=center />
                        <ItemTemplate>
                            <asp:ImageButton ID="cmdDelete" runat="server" ToolTip="Удалить" CommandName="Delete" ImageUrl="~/Images/delete_small.gif"></asp:ImageButton>
                        </ItemTemplate>
                        <EditItemTemplate>
                        </EditItemTemplate>
                    </asp:TemplateColumn>
                </Columns>
            </asp:DataGrid></td>
    </tr>
    <tr>
        <td class="SectionRowLabel" valign="middle">
            <asp:LinkButton ID="btnConfirm" runat="server">Подтвердить оплату</asp:LinkButton>&nbsp;&nbsp;
            <asp:RadioButton ID="optBeznal" runat="server" GroupName="radioPayed" Text="Без/нал">
            </asp:RadioButton>&nbsp;
            <asp:RadioButton ID="optNal" runat="server" GroupName="radioPayed" Text="Наличные"></asp:RadioButton>&nbsp;
            <asp:RadioButton ID="optSberkassa" runat="server" GroupName="radioPayed" Text="Сберкасса">
            </asp:RadioButton></td>
        <td class="SectionRow" align="right">
            <asp:ImageButton ID="btnEdit" runat="server" Visible="False" CommandName="Edit" ImageUrl="~/Images/edit/gif">
            </asp:ImageButton>&nbsp;&nbsp;&nbsp;
            <asp:ImageButton ID="btnDelete" runat="server" CommandName="Delete" ImageUrl="~/Images/delete.gif">
            </asp:ImageButton></td>
    </tr>
</table>

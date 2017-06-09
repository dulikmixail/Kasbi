<%@ Control Language="vb" AutoEventWireup="false" Inherits="Kasbi.Controls.KassirsAndPlace"
    CodeFile="KassirsAndPlace.ascx.vb" %>
<table>
    <tr>
        <td colspan="4">
            <asp:Label ID="lblCashregisterDetail" Font-Size="9pt" ForeColor="#9c0001" runat="server"></asp:Label></td>
    </tr>
    <tr>
        <td colspan="4">
            <table id="Table1" cellspacing="1" cellpadding="1" width="100%" border="0">
                <tr>
                    <td></td>
                    <td></td>
                    <td width="100">
                        <asp:TextBox ID="tbxGoodCost" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                            Width="100px"></asp:TextBox></td>
                    <td width="50">
                        <asp:TextBox ID="tbxGoodAmount" runat="server" BackColor="#F6F8FC" BorderWidth="1px"
                            Width="50px"></asp:TextBox></td>
                    <td width="125">
                        <asp:Label ID="lblGoodCost" runat="server"></asp:Label></td>
                    <td align="center" width="75">
                        <asp:ImageButton ID="btnClear" runat="server" ImageUrl="../Images/delete.gif"></asp:ImageButton></td>
                </tr>
            </table>
        </td>
    </tr>
    <tr>
        <td class="TitleTextbox" rowspan="2">
            Адрес установки кассового аппарата</td>
        <td colspan="2">
            <asp:DropDownList ID="ddlCity" runat="server" Width="449px"></asp:DropDownList></td>
    </tr>
    <tr>
        <td colspan="3">
            <asp:TextBox ID="txtPlace" runat="server" CssClass="ShiftRight3" MaxLength="250"
                ToolTip="Введите адрес установки кассового аппарата, выставочно-торговый комплекс, стенд(ряд), место"
                BackColor="#F6F8FC" BorderWidth="1px" Width="449px"></asp:TextBox></td>
    </tr>
    <tr class="SubTitleTextbox">
        <td></td>
        <td align="center">Кассир 1</td>
        <td align="center">Кассир 2</td>
    </tr>
    <tr>
        <td class="TitleTextbox">ФИО кассиров:</td>
        <td>
            <asp:TextBox ID="txtKasir1" runat="server" CssClass="ShiftRight3" MaxLength="100"
                ToolTip="Введите ФИО первого кассира" BackColor="#F6F8FC" BorderWidth="1px" Width="216px"></asp:TextBox></td>
        <td>
            <asp:TextBox ID="txtKasir2" runat="server" CssClass="ShiftRight3" MaxLength="100"
                ToolTip="Введите ФИО второго кассира" BackColor="#F6F8FC" BorderWidth="1px" Width="232px"></asp:TextBox></td>
    </tr>
</table>

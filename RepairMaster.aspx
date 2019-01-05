<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.RepairMaster" Culture="ru-RU"
    CodeFile="RepairMaster.aspx.vb" %>

<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Рамок - [Ремонт]</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">

    <script language="JavaScript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />

    <script language="javascript">
        function isFind(s)
        {
            var theform = document.frmRepairList;
            theform.FindHidden.value = s;
        }
    </script>
        <style type="text/css">
/* The Modal (background) */
        .modal {
            display: none; /* Hidden by default */
            position: fixed; /* Stay in place */
            z-index: 1; /* Sit on top */
            padding-top: 100px; /* Location of the box */
            left: 0;
            top: 0;
            width: 100%; /* Full width */
            height: 100%; /* Full height */
            overflow: auto; /* Enable scroll if needed */
            background-color: rgb(0,0,0); /* Fallback color */
            background-color: rgba(0,0,0,0.4); /* Black w/ opacity */
        }

        /* Modal Content */
        .modal-content {
            position: relative;
            background-color: #fefefe;
            margin: auto;
            padding: 0;
            border: 1px solid #888;
            width: 440px;
            height: 280px;
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2),0 6px 20px 0 rgba(0,0,0,0.19);
            -webkit-animation-name: animatetop;
            -webkit-animation-duration: 0.4s;
            animation-name: animatetop;
            animation-duration: 0.4s
        }

        /* Add Animation */
        @-webkit-keyframes animatetop {
            from {top:-300px; opacity:0} 
            to {top:0; opacity:1}
        }

        @keyframes animatetop {
            from {top:-300px; opacity:0}
            to {top:0; opacity:1}
        }

        /* The Close Button */
        .close {
            color: white;
            float: right;
            font-size: 28px;
            font-weight: bold;
        }

        .close:hover,
        .close:focus {
            color: #000;
            text-decoration: none;
            cursor: pointer;
        }

        .modal-header {
            padding: 1% 2%;
            background-color: #d3c9c7;
            color: white;
            height: 20%;
        }

        .modal-body {
            padding: 5%;
            height: 60%;
            overflow: auto;
        }

        .modal-footer {
            padding: 1%;
            background-color: #d3c9c7;
            color: white;
        }

    </style>


</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">



    <form id="frmRepairList" method="post" runat="server" DefaultButton="lnkFind" DefaultFocus="txtFindGoodNum">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div id="myModal" runat="server" class="modal" >

        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
                <h3>Внесите данные СКНО</h3>
            </div>
            <div id="myModalBody" runat="server" class="modal-body">
                    <table>
                        <tr>
                            <td style="font-size: 14px">Учетный номер СКНО</td>
                            <td>
                                <asp:TextBox runat="server" style="margin: 5px" type="text" ID="txtRegistrationNumberSKNO" Value="123456789"></asp:TextBox>
                                <ajaxToolkit:MaskedEditValidator ID="txtRegistrationNumberSKNO_MaskedEditValidator" runat="server" ControlExtender="txtRegistrationNumberSKNO_MaskedEditExtender" ControlToValidate="txtRegistrationNumberSKNO" Display="Dynamic" EmptyValueBlurredText="*" IsValidEmpty="True" ValidationExpression="^\d{9}$" ValidationGroup="GroupName"></ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:MaskedEditExtender ID="txtRegistrationNumberSKNO_MaskedEditExtender" runat="server" BehaviorID="txtRegistrationNumberSKNO_MaskedEditExtender" TargetControlID="txtRegistrationNumberSKNO" Mask="999999999" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px">Заводской номер СКНО</td>
                            <td>
                                <asp:TextBox runat="server" style="margin: 5px" type="text" ID="txtSerialNumberSKNO" Value="123456789"></asp:TextBox>
                                <ajaxToolkit:MaskedEditValidator ID="txtSerialNumberSKNO_MaskedEditValidator" runat="server" ControlExtender="txtSerialNumberSKNO_MaskedEditExtender" ControlToValidate="txtSerialNumberSKNO" Display="Dynamic" EmptyValueBlurredText="*" IsValidEmpty="True" ValidationExpression="^\d{4,9}$" ValidationGroup="GroupName"></ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:MaskedEditExtender ID="txtSerialNumberSKNO_MaskedEditExtender" runat="server" BehaviorID="txtSerialNumberSKNO_MaskedEditExtender" TargetControlID="txtSerialNumberSKNO" Mask="999999999" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>

                            </td>
                        </tr>
                        <%--<tr>
                            <td style="font-size: 14px">Ранее использованные тел. оповещения</td>
                            <td>
                                <asp:DropDownList ID="lstTelephoneNotice" runat="server" style="margin: 5px" Width="100%" BackColor="#F6F8FC" AutoPostBack="True"/>
                            </td>
                        </tr>--%>
                        <tr>
                            <td style="font-size: 14px">Телефон оповещения</td>
                            <td align="center">
                                <asp:TextBox ID="txtTelephoneNotice" runat="server" ToolTip="Введите телефон оповещения" Value="294010101"/>
                                <ajaxToolkit:MaskedEditValidator ID="txtTelephoneNotice_MaskedEditValidator" runat="server" ControlExtender="txtTelephoneNotice_MaskedEditExtender" ControlToValidate="txtTelephoneNotice" Display="Dynamic" EmptyValueBlurredText="*" ErrorMessage="Введен некорректный мобильный телефон!" InvalidValueBlurredMessage="Введен некорректный мобильный телефон!" InvalidValueMessage="Введен некорректный мобильный телефон!" IsValidEmpty="True" ValidationExpression="^(29|25|44|33)(\d{7})$" ValidationGroup="GroupName">+375 (99) 999-99-99</ajaxToolkit:MaskedEditValidator>
                                <ajaxToolkit:MaskedEditExtender ID="txtTelephoneNotice_MaskedEditExtender" runat="server" BehaviorID="txtTelephoneNotice_MaskedEditExtender" TargetControlID="txtTelephoneNotice" Mask="+375 (99) 999-99-99" MaskType="Number" MessageValidatorTip="True" ErrorTooltipEnabled="True" ClearTextOnInvalid="True" ClearMaskOnLostFocus="True" AutoComplete="False"/>
                            </td>
                        </tr>
                        <tr>
                            <td style="font-size: 14px">Отправить СМС</td>
                            <td align="center">
                                <asp:CheckBox runat="server" ID="cbxModalSendSknoSms"/>
                            </td>
                        </tr>
                        <tr><td><asp:TextBox runat="server" type="text" id="modalGoodId" hidden="true"/></td></tr>
                        <tr><td><asp:TextBox runat="server" type="text" id="modalCustomerId" hidden="true"/></td></tr>
                        <tr style="text-align:center">
                            <td colspan="2">
                                <asp:LinkButton runat="server" ID="modalSubmit">Сохранить</asp:LinkButton>
                            </td>
                        </tr>
                    </table>
            </div>
            <div class="modal-footer">
            </div>
        </div>

    </div>


        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">
                    &nbsp;Ремонт кассовых аппаратов&nbsp;</td>
            </tr>
        </table>
        <table style="font-size:12px" id="Table1" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Критерий&nbsp;поиска&nbsp;ККМ</td>
            </tr>
 
             <tr>
                <td width="100%">
                
                <table style="font-size:12px">
                    <tr>
                        <td>Номер кассового аппарата:</td>
                        <td><asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13" Width="150px"></asp:TextBox></td>
                        <td><asp:LinkButton ID="lnkFind" runat="server" CssClass="LinkButton">&nbsp;Найти&nbsp;</asp:LinkButton></td>
                        <td width="500" align="right"><asp:LinkButton ID="lnkFindRepair" runat="server" CssClass="LinkButton">&nbsp;Показать аппараты, которые в ремонте&nbsp;</asp:LinkButton></td>
                    </tr>
                </table>
                <br />
                
                
                </td>
            </tr>           
            
             <tr class="Unit">
                <td class="Unit" width="100%">
                    &nbsp;Найдено по кассовому оборудованию</td>
            </tr>
            <tr>
                <td>
                <br />
                     <asp:DataGrid ID="grdRepair"  runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px"> 
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="№">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumGood" EnableViewState="true" ForeColor="#9C0001" runat="server" ></asp:Label>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
  
                           <asp:TemplateColumn HeaderText="Плательщик / Владелец" SortExpression="payerInfo"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodOwner" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Долг" SortExpression="dolg"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="Товар" SortExpression="good_name" > 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" Target="_blank" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupportSKNO" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/skno.gif" Visible="false"
                                                       ToolTip="установлено СКНО">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                         ToolTip="На техобслуживании">
                                        </asp:HyperLink>
                                        
                                        <asp:HyperLink ID="imgRepair" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="В ремонте">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="Побывал в ремонте">
                                        </asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Место хранения" SortExpression="num_cashregister" > 
                                <ItemTemplate>
                                    <asp:Label ID="lblStorageNumber" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.storage_number") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="№ СК изг./ЦТО" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_mfp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="№ СК СКНО" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtSknoControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.registration_number_skno") & "<br>" & DataBinder.Eval(Container, "DataItem.serial_number_skno")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>
                            <%--<asp:TemplateColumn HeaderText="Описание неисправности" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRepairBadsList" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.repair_bads_list") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateColumn>--%>
                            <asp:TemplateColumn HeaderText="Место установки" SortExpression="place_rn_id"> 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtPlace" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.set_place") %>'>
                                    </asp:Label>
                                    <br>
                                    <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='Район установки:'></asp:Label>
                                    <b>
                                        <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region")%>'>
                                        </asp:Label></b>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Операции">
                                <ItemTemplate>
                                   <input name="hiddenGoodId" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"good_sys_id")%>"/>
                                   <input name="hiddenCustomerId" type="hidden" value="<%# DataBinder.Eval(Container.DataItem,"payer_sys_id")%>"/>
                                   <%--<a href="#" id="<%# DataBinder.Eval(Container.DataItem,"good_sys_id")%>_lnkSetDataSkno" style="display: none">Внести&nbsp;данные&nbsp;СКНО!</a>--%>
                                   <asp:LinkButton ID="lnkSetDataSkno" runat="server">Внести&nbsp;данные&nbsp;СКНО<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkActivateRepairSkno" runat="server">Начать&nbsp;установку&nbsp;СКНО<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkEditRepairSkno" runat="server">Редактировать&nbsp;ремонт&nbsp;с&nbsp;СКНО<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkSetRepair" runat="server">Принять&nbsp;в&nbsp;ремонт<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkActivateRepair" runat="server">Начать&nbsp;ремонт<br /></asp:LinkButton>
                                   <asp:LinkButton ID="lnkEditRepair" runat="server">Редактировать&nbsp;ремонт<br /> </asp:LinkButton>
                                   <asp:LinkButton ID="lnkOutRepair" runat="server">Отдать&nbsp;владельцу<br /></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последнее ТО" SortExpression="repair">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="Последнее принятие в ремонт" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblReception" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>   
                            <asp:TemplateColumn HeaderText="Последний ремонт" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblCto_master" runat="server" ></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>  
                            <asp:TemplateColumn HeaderText="Последняя выдача из ремонта" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblIssue" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>  
                        </Columns>
                    </asp:DataGrid>
                                    
                </td>
            </tr>           
        </table>
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server"/>
    <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server"/>
    <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server"/>
    <input id="FindHidden" type="hidden" name="FindHidden" runat="server"/>
    </form>
<script type="text/javascript">
    
    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];
    $("*[id$=lnkSetDataSkno]").each(function (i, el) {
        el.href = "#";
        el.onclick = function () {
            var goodId = this.parentNode.querySelector("input[name=hiddenGoodId]");
            modal.querySelector("input[id=modalGoodId]").value = goodId.value;
            var customerId = this.parentNode.querySelector("input[name=hiddenCustomerId]");
            modal.querySelector("input[id=modalCustomerId]").value = customerId.value;
            showSknoData();
        };
    });
    // When the user clicks on <span> (x), close the modal
    span.onclick = function () {
        modal.style.display = "none";
    }

    // When the user clicks anywhere outside of the modal, close it
    window.onclick = function (event) {
        if (event.target == modal) {
            modal.style.display = "none";
        }
    }

    function showSknoData() {
        modal.style.display = "block";
    };
    function toggleButtonDataSkno(state, buttonDataSknoId) {
        console.log("toggleButtonDataSkno");
        document.getElementById(buttonDataSknoId).visible = state;
    }
</script>
</body>
</html>

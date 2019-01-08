<%@ Register TagPrefix="uc1" TagName="Footer" Src="Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="Controls/Header.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Inherits="Kasbi.MasterCTO" CodeFile="MasterCTO.aspx.vb" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head  runat ="server">
    <title>[��� "�����"]</title>

    <script type="text/javascript">
		function isFind(s)
		{
			var theform = document.frmInternalCTO;
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
            width: 90%;
            height: 80%;
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
            padding: 2px 16px;
            background-color: #d3c9c7;
            color: white;
            height: 9%;
        }

        .modal-body {
            padding: 2px 16px;
            height: 90%;
            overflow: auto;
        }

        .modal-footer {
            padding: 2px 16px;
            background-color: #d3c9c7;
            color: white;
            height: 1%;
        }

    </style>


    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="stylesheet">
    <script type="text/javascript" src="../scripts/datepicker.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.min.js"></script>
    <script type="text/javascript" src="../scripts/js/datetimepicker/jquery.datetimepicker2.js"></script>
    <link type="text/css" href="../scripts/js/datetimepicker/jquery.datetimepicker.css" rel="stylesheet" />
</head>
<body onscroll="javascript:document.all['scrollPos'].value=document.body.scrollTop;"
    bottommargin="0" leftmargin="0" topmargin="0" onload="javascript:document.body.scrollTop=document.all['scrollPos'].value;"
    rightmargin="0">


    <form id="frmInternalCTO" method="post" runat="server">
    
    <!-- The Modal -->
    <div id="myModal" runat="server" class="modal" >

        <!-- Modal content -->
        <div class="modal-content">
            <div class="modal-header">
                <span class="close">&times;</span>
                <h3>������ ��� ��������� ��</h3>
            </div>
            <div id="myModalBody" runat="server" class="modal-body">
                <asp:DataGrid ID="grdError"  runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px"> 
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                                            <Columns>
                            <asp:TemplateColumn HeaderText="�">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink Target="_blank" id="lblNumGood" runat="server" ForeColor="black"></asp:HyperLink>
                                    <asp:Label Visible="false" ID="lblGood" Text='<%# DataBinder.Eval(Container, "DataItem.good_sys_id") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
  
                            <asp:TemplateColumn HeaderText="�����" SortExpression="good_name" > 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" Target="_blank" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupportSKNO" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/skno.gif" Visible="false"
                                            ToolTip="����������� ����">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                         ToolTip="�� ���������������">
                                        </asp:HyperLink>
                                        
                                        <asp:HyperLink ID="imgRepair" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="� �������">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="������� � �������">
                                        </asp:HyperLink></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="� �� ���./���" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_mfp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="����� ���������" SortExpression="place_rn_id"> 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtPlace" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.set_place")%>'>
                                    </asp:Label>
                                    <br>
                                    <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='����� ���������:'></asp:Label>
                                    <b>
                                        <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region")%>'>
                                        </asp:Label></b>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="������" SortExpression="lastTO">
                                <ItemTemplate>
                                    <asp:Label ID="lblToExeption" runat="server" ForeColor="red"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="��" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" Target="_blank" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id")) %>'>����������</asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>

                            <asp:TemplateColumn HeaderText="�������������" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblCto_master" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.cto_master")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>                            
                        </Columns>

                    </asp:DataGrid>
            </div>
            <div class="modal-footer">
            </div>
        </div>

    </div>
        <uc1:Header ID="Header1" runat="server"></uc1:Header>
        <table class="PageTitle" cellspacing="1" cellpadding="2" width="100%" border="0">
            <tr>
                <td class="HeaderTitle" width="100%">&nbsp;���&nbsp; ������� "�����"&nbsp; </td>
            </tr>
        </table>
        <table cellspacing="0" cellpadding="2" width="100%" border="0">
            <tr class="Unit" style="display:none">
                <td class="Unit" width="100%"><asp:hyperlink ID="lnkAddSupport" runat="server" CssClass="LinkButton" NavigateUrl="~/AddTO.aspx">&nbsp;&nbsp;&nbsp;�������� ������������ �� ��</asp:hyperlink>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                      <asp:CheckBox ID="chk_show_kkm" EnableTheming="true" EnableViewState="true" runat="server" Checked="true" AutoPostBack="true" Text=" �������� ������������" />&nbsp;
                      <asp:CheckBox ID="chk_show_torg" EnableTheming="true" EnableViewState="true" runat="server" Checked="false" AutoPostBack="true" Text=" ������ �������� ������������" />                                
                </td>
            </tr>
            <tr>
                <td colspan="2" style="font-size:12px">
                    <asp:Label ID="msgCashregister" runat="server" EnableViewState="false" ForeColor="#ff0000" Font-Bold="true" Font-Size="14px"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <table id="Table2" cellspacing="1" cellpadding="2" width="100%" style="font-size:12px">
                        <tr>
                            <td class="SectionRowLabel" valign="top">
                                   &nbsp;&nbsp;<i>�</i><br />
                                   <asp:TextBox ID="txtFindGoodNum" runat="server" BorderWidth="1px" Height="18px" MaxLength="13"
                                   Width="150px"></asp:TextBox><br />
                                   <i>��&nbsp;���</i><br />
                                   <asp:TextBox ID="txtFindGoodCTO" runat="server" BorderWidth="1px" Height="18px" MaxLength="11"
                                    Width="150px"></asp:TextBox><br />
                                   <i>��&nbsp;������������</i><br />
                                   <asp:TextBox ID="txtFindGoodManufacturer" runat="server" BorderWidth="1px" Height="18px"
                                   MaxLength="11" Width="150px"></asp:TextBox><br />
                            </td>
                            <td>
                                  <asp:LinkButton ID="btnFindGood" runat="server" EnableViewState="False" CssClass="PanelHider"><div align="center">�������� ����<br /> ��������</div></asp:LinkButton>
                            </td>                            
                            <td class="SectionRowLabel" valign="top">
                                   <i>������������ </i><br />
                                   <asp:ListBox ID="lstGoodType" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td> 
                            <td class="SectionRowLabel" valign="top">
                                    <asp:Label ID="lbl_otv_master" runat="server">
                                   <i>������������� ������� </i><br /></asp:Label>
                                   <asp:ListBox ID="lstEmployee" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Single" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td>                            
                            <td class="SectionRowLabel" valign="top">
                                   <i>����� ���������</i><br />
                                   <asp:ListBox ID="lstPlaceRegion" runat="server" BorderWidth="1px" Rows="5"
                                   Width="200px" SelectionMode="Multiple" EnableTheming="true" EnableViewState="true" AppendDataBoundItems="true">
                                   </asp:ListBox> 
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="padding:5px" style="font-size:12">
                     <asp:LinkButton ID="lnkNeraspl" runat="server" CssClass="LinkButton">&nbsp;�������� ����������������&nbsp;</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkSetEmployee" cssClass="LinkButton" runat="server">��������� ��� �� ��������</asp:LinkButton> |
                <asp:Label ID="adm_panel" runat="server">
                     <asp:LinkButton ID="lnkRaspl" runat="server" CssClass="LinkButton">&nbsp;�������� ��������������&nbsp;</asp:LinkButton> | 
                     <asp:LinkButton ID="lnk_show_no_comfirmed" runat="server" CssClass="LinkButton">&nbsp;�������� ����������������&nbsp;</asp:LinkButton> |                                     
                     <asp:LinkButton Visible="false" ID="lnkNotTO" runat="server" CssClass="LinkButton">&nbsp;�� ������������� ��������� 3 ������</asp:LinkButton>   
                     <asp:LinkButton ID="lnkDelTO" cssClass="LinkButton" runat="server">&nbsp;�������� ��������� ��</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkConfirmEmployee" cssClass="LinkButton" runat="server">&nbsp;����������� ��� �������</asp:LinkButton> | 
                     <asp:LinkButton ID="lnkDelEmployee" cssClass="LinkButton" runat="server">&nbsp;����� ��� � �������</asp:LinkButton> |                    
                    </asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                       <div style="background-color:Silver; width:600; padding:5px; font-size:12; padding: 8px; float: right">
                                <b>������:&nbsp;</b>
                                 
                                <asp:LinkButton ID="lnkConduct" runat="server" CssClass="LinkButton">
                                    &nbsp;��&nbsp;���������&nbsp;</asp:LinkButton>&nbsp;|&nbsp;
                                
                                <asp:LinkButton ID="lnkNotConduct" runat="server" CssClass="LinkButton">
                                    &nbsp;��&nbsp;��&nbsp;���������&nbsp;</asp:LinkButton>&nbsp;|&nbsp;                      
                     
                                <asp:LinkButton ID="lnkBlackList" runat="server" CssClass="LinkButton" Visible="False">
                                    &nbsp;������&nbsp;������&nbsp;&nbsp;|&nbsp;  </asp:LinkButton>                   
                     
                                <asp:LinkButton ID="lnk_onTO" runat="server" CssClass="LinkButton">
                                    &nbsp;�� ��&nbsp;</asp:LinkButton> &nbsp;|&nbsp;  
                                    
                                <asp:LinkButton ID="lnk_stopTO" runat="server" CssClass="LinkButton">
                                    &nbsp;�� ��������������&nbsp;</asp:LinkButton> &nbsp;|&nbsp;  
                                    
                                <asp:LinkButton ID="lnk_delTO" runat="server" CssClass="LinkButton">
                                    &nbsp;����� � ��&nbsp;</asp:LinkButton>                                                                                               
                      </div>
                </td>
            </tr>
        <tr>
            <td>
                <div style="background-color:Silver; width:600px; padding:5px; font-size:12px; padding: 8px; float: right">
                    <b>���������:&nbsp;</b>
                                 
                    <asp:LinkButton ID="lnk_aktForTOandDolgWithtDate" runat="server" CssClass="LinkButton">
                        &nbsp;���&nbsp;��&nbsp;�&nbsp;�����&nbsp;</asp:LinkButton>&nbsp;|&nbsp;
                    
                    <asp:LinkButton ID="lnk_aktForTOandDolgWithoutDate" runat="server" CssClass="LinkButton">
                        &nbsp;���&nbsp;��&nbsp;���&nbsp;����&nbsp;</asp:LinkButton>&nbsp;|&nbsp;
                                
                </div>
            </td>
        </tr>

            <tr>
                <td align="left" colspan="2" height="15" valign="top" style="padding:5px; font-size:12px">
                <div style="background-color:Silver; width:980px; padding:5px">                         
                <b>����&nbsp;����������&nbsp;��:&nbsp;</b><asp:textbox id="tbxCloseDate" Runat="server" BorderWidth="1px"></asp:textbox><b>&nbsp;&nbsp;�����������&nbsp;������:&nbsp;</b>
                    <%--<A href="javascript:showdatepicker('tbxCloseDate', 0, false,'MM.DD.YYYY')"><IMG alt="Date Picker" src="../Images/cal_date_picker.gif" border="0"></A>--%>
                                <asp:DropDownList ID="lstMonth" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
                                    <asp:ListItem Value="01">������</asp:ListItem>
                                    <asp:ListItem Value="02">�������</asp:ListItem>
                                    <asp:ListItem Value="03">����</asp:ListItem>
                                    <asp:ListItem Value="04">������</asp:ListItem>
                                    <asp:ListItem Value="05">���</asp:ListItem>
                                    <asp:ListItem Value="06">����</asp:ListItem>
                                    <asp:ListItem Value="07">����</asp:ListItem>
                                    <asp:ListItem Value="08">������</asp:ListItem>
                                    <asp:ListItem Value="09">��������</asp:ListItem>
                                    <asp:ListItem Value="10">�������</asp:ListItem>
                                    <asp:ListItem Value="11">������</asp:ListItem>
                                    <asp:ListItem Value="12">�������</asp:ListItem>
                                </asp:DropDownList>                      
                      
                                <asp:DropDownList  ID="lstYear" runat="server" BorderWidth="1px" BackColor="#F6F8FC">
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
                                    <asp:ListItem Value="2019">2019</asp:ListItem>
                                    <asp:ListItem Value="2020">2020</asp:ListItem>
                                </asp:DropDownList>                  
                &nbsp;&nbsp;&nbsp;
                <asp:LinkButton ID="lnkSetTO" cssClass="LinkButton" runat="server">�������� ��</asp:LinkButton>&nbsp;|&nbsp;
                <%--<asp:LinkButton ID="LinkButton1" cssClass="LinkButton" runat="server">��� ������-����� ����������� �����</asp:LinkButton>&nbsp;|&nbsp;--%>               
                <asp:LinkButton ID="lnkExportData" cssClass="LinkButton" runat="server">������� � Excel</asp:LinkButton>&nbsp;|&nbsp;
                <asp:LinkButton ID="lnkSetRaon" cssClass="LinkButton" runat="server">��������� � ������</asp:LinkButton>       
                </div>   
                <br />
               
               </td>
            </tr>
            <tr class="Unit">
                <td class="Unit" colspan="2">&nbsp;������� �� ��������� ������������&nbsp;</td>
            </tr>
            <tr>
                <td align="center" colspan="2">
                    <asp:DataGrid ID="grdTO"  runat="server" AutoGenerateColumns="False"
                         Width="100%" AllowSorting="true"  BorderColor="#CC9966" BorderWidth="1px"> 
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="�������">
                                <HeaderTemplate>
                                    �������<br>
                                    <asp:CheckBox ID="cbxSelectAll" runat="server" AutoPostBack="True" OnCheckedChanged="SelectAll">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                    <asp:Label Visible="false" ID="lblPayerId" Text='<%# DataBinder.Eval(Container, "DataItem.payer_sys_id") %>' runat="server"></asp:Label>
                                    <asp:Label Visible="false" ID="lblGoodType" Text='<%# DataBinder.Eval(Container, "DataItem.good_type_sys_id") %>' runat="server"></asp:Label>
                                    <asp:Label Visible="false" ID="lblGood" Text='<%# DataBinder.Eval(Container, "DataItem.good_sys_id") %>' runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:HyperLink Target="_blank" id="lblNumGood" runat="server" ForeColor="black"></asp:HyperLink>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
  
                           <asp:TemplateColumn HeaderText="���������� / ��������" SortExpression="payerInfo"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodOwner" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�����" SortExpression="good_name" > 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" Target="_blank" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_cashregister") %>'
                                        NavigateUrl='<%# "CashOwners.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") &amp; "&amp;cashowner="&amp; DataBinder.Eval(Container, "DataItem.payer_sys_id")%>'>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        <asp:HyperLink ID="imgSupportSKNO" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/skno.gif" Visible="false"
                                            ToolTip="����������� ����">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgSupport" Target="_blank" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                         ToolTip="�� ���������������">
                                        </asp:HyperLink>
                                        
                                        <asp:HyperLink ID="imgRepair" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="� �������">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" Target="_blank" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="������� � �������">
                                        </asp:HyperLink></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="� �� ���./���" SortExpression="num_control_cto" > 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtControl" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.num_control_reestr") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_pzu") & "<br>" & DataBinder.Eval(Container, "DataItem.num_control_mfp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cp")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto")& "<br>" & DataBinder.Eval(Container, "DataItem.num_control_cto2")%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="����� ���������" SortExpression="place_rn_id"> 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtPlace" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.set_place")%>'>
                                    </asp:Label>
                                    <br>
                                    <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='����� ���������:'></asp:Label>
                                    <b>
                                        <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region")%>'>
                                        </asp:Label></b>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="����" SortExpression="dolg"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="��">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" Target="_blank" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id")) %>'>
                                        </asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="��������� ��" SortExpression="lastTO">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�������������" SortExpression="cto_master">
                                <ItemTemplate>
                                    <asp:Label ID="lblCto_master" runat="server"  Text='<%# DataBinder.Eval(Container, "DataItem.cto_master")%>'></asp:Label>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>                            
                        </Columns>
                    </asp:DataGrid>
                    
                                    
                
                </td>
            </tr>

            <tr class="Unit" style="display:none">
                <td class="Unit" colspan="2">&nbsp;������� �� ��������� ������������&nbsp;</td>
            </tr>            
            <tr>
                <td align="center" colspan="2">
                
                    <asp:DataGrid ID="grdTO_prod" BorderWidth="1px" Width="100%" runat="server" CellPadding="1"
                        AllowSorting="True" AutoGenerateColumns="False" BorderColor="#CC9966" Font-Bold="False" Font-Italic="False" Font-Overline="False" Font-Strikeout="False" Font-Underline="False">
                        <ItemStyle CssClass="itemGrid"></ItemStyle>
                        <HeaderStyle CssClass="headerGrid" ForeColor="#FFFFCC" ></HeaderStyle>
                        <FooterStyle CssClass="footerGrid"></FooterStyle>
                        <Columns>
                            <asp:TemplateColumn HeaderText="�������">
                                <HeaderTemplate>
                                    �������<br>
                                    <asp:CheckBox ID="cbxSelectAll" runat="server" AutoPostBack="True"  OnCheckedChanged="SelectAll_prod">
                                    </asp:CheckBox>
                                </HeaderTemplate>
                                <ItemTemplate>
                                    <asp:CheckBox ID="cbxSelect" Checked="False" runat="server"></asp:CheckBox>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblNumGood" EnableViewState="true" ForeColor="#9C0001" runat="server" ></asp:Label>
                                    <asp:Label ID="lblCustDogovor" runat="server" Visible="False">Label</asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                           <asp:TemplateColumn HeaderText="���������� / ��������" SortExpression="payerInfo"> 
                            
                                <ItemTemplate>
                                    <asp:Label ID="lblGoodOwner" runat="server"  Text=''></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�����" SortExpression="good_name" ItemStyle-HorizontalAlign="center"> 
                                <ItemTemplate>
                                    <asp:Label ID="lbledtGoodName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_name") %>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="�" SortExpression="num_cashregister" > 
                                  <ItemTemplate>
                                    <asp:HyperLink ID="lbledtNum" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.good_num") %>'
                                        NavigateUrl=''>
                                    </asp:HyperLink>
                                    <p style="margin-top: 5px; margin-bottom: 0px" align="center">
                                        <asp:HyperLink ID="imgAlert" runat="server" CssClass="CutImageLink" ImageUrl="Images/sign.gif"></asp:HyperLink>
                                        
                                        <asp:HyperLink ID="imgSupport" runat="server" CssClass="CutImageLink" ImageUrl="Images/support.gif"
                                            ToolTip="�� ���������������">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepair" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.goodto_sys_id") %>'
                                            ImageUrl="Images/repair.gif" ToolTip="� �������">
                                        </asp:HyperLink>
                                        <asp:HyperLink ID="imgRepaired" runat="server" CssClass="CutImageLink" NavigateUrl='<%# "Repair.aspx?" &amp; DataBinder.Eval(Container, "DataItem.goodto_sys_id") %>'
                                            ImageUrl="Images/repaired.gif" ToolTip="������� � �������">
                                        </asp:HyperLink></p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="����� ���������" SortExpression="set_place" ItemStyle-HorizontalAlign="center"> 
                                <HeaderStyle Font-Underline="True"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lbledtPlace" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.set_place")%>'>
                                    </asp:Label>
                                    <br>
                                    <asp:Label CssClass="SubTitleEditbox" ID="lblPlaceRegion" runat="server" Text='����� ���������:'></asp:Label>
                                    <b>
                                        <asp:Label ID="lbledtPlaceRegion" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.place_region")%>'>
                                        </asp:Label></b>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="������" SortExpression="dolg"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblDolg" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="��" ItemStyle-HorizontalAlign="center">
                                <ItemTemplate>
                                    <p>
                                        <asp:HyperLink ID="lnkStatus" Target="_blank" runat="server" NavigateUrl='<%# GetAbsoluteUrl("NewSupportConduct.aspx?" &amp; DataBinder.Eval(Container, "DataItem.good_sys_id")) %>'>����������</asp:HyperLink>
                                    </p>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn HeaderText="��������� ��" ItemStyle-HorizontalAlign="center"  SortExpression="lastTO"> 
                                <ItemTemplate>
                                    <asp:Label ID="lblLastTO" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                        </Columns>
                    </asp:DataGrid>                
                </td>
            </tr>  
            
                        
            <tr height="10">
                <td width="100%" colspan="2">
                    &nbsp;</td>
            </tr>
        </table>

        
    
        <uc1:Footer ID="Footer1" runat="server"></uc1:Footer>
    <input id="scrollPos" type="hidden" value="0" name="scrollPos" runat="server"/>
    <input lang="ru" id="CurrentPage" type="hidden" name="CurrentPage" runat="server"/>
    <input lang="ru" id="Parameters" type="hidden" name="Parameters" runat="server"/>
    <input id="FindHidden" type="hidden" name="FindHidden" runat="server"/>
    </form>

<script type="text/javascript">

    jQuery(function () {

        jQuery('#tbxCloseDate').datetimepicker({
            lang: 'ru',
            timepicker: false,
            format: 'd.m.Y',
            closeOnDateSelect: true,
            scrollMonth: false,
        });

    });

    // Get the modal
    var modal = document.getElementById('myModal');

    // Get the <span> element that closes the modal
    var span = document.getElementsByClassName("close")[0];

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

   

</script>
</body>
</html>

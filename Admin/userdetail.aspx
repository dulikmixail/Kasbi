<%@ Register TagPrefix="uc1" TagName="Footer" Src="../Controls/Footer.ascx" %>
<%@ Register TagPrefix="uc1" TagName="Header" Src="../Controls/Header.ascx" %>
<%@ Page language="vb" AutoEventWireup="false" Inherits="Kasbi.Admin.UserDetail" CodeFile="UserDetail.aspx.vb" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<head runat ="server">
    <title>[Информация о пользователе]</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="C#" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="stylesheet">
		<script language="javascript">
		function enable_salers(checked)
		{
			document.getElementById("txtDocument").disabled = !checked;
		}
		</script>
	</HEAD>
	<body bottomMargin="0" leftMargin="0" topMargin="0" rightMargin="0">
		<form id="Form1" method="post" runat="server">
			<uc1:header id="Header1" runat="server"></uc1:header>
			<table class="PageTitle" cellSpacing="1" cellPadding="2" width="100%" border="0">
				<tr>
					<td class="HeaderTitle" width="100%" style="height: 18px">&nbsp;Администрирование 
						-&gt;&nbsp;Информация&nbsp;о&nbsp;пользователе</td>
				</tr>
			</table>
			<table width="100%" cellPadding="2" cellSpacing="1" border="0">
				<tr>
					<td class="Unit" vAlign="top">Информация&nbsp;о&nbsp;пользователе</td>
				</tr>
				<tr>
					<td align="left">
						<table cellSpacing="1" cellPadding="2" width="100%" border="0">
							<tr>
								<td class="SectionRowLabel" width="20%">Имя :</td>
								<td class="SectionRow"><asp:textbox id="txtFirstName" runat="server" BackColor="#F6F8FC" BorderWidth="1px"></asp:textbox><asp:requiredfieldvalidator id="rfvFirstName" runat="server" ControlToValidate="txtFirstName" ErrorMessage="Имя"
										Display="None">*</asp:requiredfieldvalidator></td>
							</tr>
							<TR>
								<td class="SectionRowLabel">Логин :</td>
								<td class="SectionRow"><asp:textbox id="txtLogin" runat="server" BackColor="#F6F8FC" BorderWidth="1px"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator1" runat="server" ControlToValidate="txtLogin" ErrorMessage="Логин"
										Display="None">*</asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Пароль :</TD>
								<TD class="SectionRow"><asp:textbox id="txtPassword" runat="server" BackColor="#F6F8FC" BorderWidth="1px" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="rfvPassword" runat="server" ControlToValidate="txtPassword" ErrorMessage="Пароль"
										Display="None">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Подтверждение пароля :</TD>
								<TD class="SectionRow"><asp:textbox id="txtCPassword" runat="server" BackColor="#F6F8FC" BorderWidth="1px" TextMode="Password"></asp:textbox><asp:requiredfieldvalidator id="rfvCPassword" runat="server" ControlToValidate="txtCPassword" ErrorMessage="Подтверждение пароля"
										Display="None">*</asp:requiredfieldvalidator></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Администратор :</TD>
								<TD class="SectionRow"><asp:checkbox id="cbxIsAdministrator" runat="server"></asp:checkbox></TD>
							</TR>
							<tr>
								<td class="SectionRowLabel">Роль :</td>
								<td class="SectionRow"><asp:radiobuttonlist id="rlstRoles" runat="server" RepeatDirection="Horizontal" CssClass="TitleTextbox"></asp:radiobuttonlist></td>
							</tr>
							<TR>
								<td class="SectionRowLabel">Должность :</td>
								<td class="SectionRow"><asp:textbox id="txt_work_type" runat="server" BackColor="#F6F8FC" BorderWidth="1px" Columns="50"></asp:textbox><asp:requiredfieldvalidator id="Requiredfieldvalidator2" runat="server" ControlToValidate="txt_work_type" ErrorMessage="Должность" Display="None">*</asp:requiredfieldvalidator></td>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Ответственный :</TD>
								<TD class="SectionRow"><asp:checkbox id="cbxIsSaler" runat="server"></asp:checkbox></TD>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Документ :</TD>
								<TD class="SectionRow"><asp:textbox id="txtDocument" runat="server" BackColor="#F6F8FC" BorderWidth="1px" Columns="50"></asp:textbox></TD>
							</TR>
							<TR>
								<td colSpan="2" height="19"><asp:comparevalidator id="CompareValidator1" runat="server" ControlToValidate="txtPassword" ErrorMessage="Введенные пароли не совпадают"
										Display="None" ControlToCompare="txtCPassword"></asp:comparevalidator><asp:label id="lblError" runat="server" Font-Size="8pt" ForeColor="Red" Visible="False"> 'Пароль' должен содержать более шести символов</asp:label><asp:label id="lblSqlError" runat="server" Font-Size="8pt" ForeColor="Red"></asp:label></td>
							</TR>
							<TR>
								<TD class="SectionRowLabel">Деактивировать :</TD>
								<TD class="SectionRow"><asp:checkbox id="cbxInactive" runat="server"></asp:checkbox></TD>
							</TR>
						</table>
					</td>
				</tr>
				<tr>
					<td class="Unit" vAlign="top">Настройка&nbsp;прав&nbsp;пользователя</td>
				</tr>
				<tr>
				    <td align="left" class="SectionRowLabel">
				    
				    <asp:checkbox id="rule1" runat="server"></asp:checkbox> <b>Показывать Главную</b> <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule1_1" runat="server"></asp:checkbox> Показывать бухгалтерию <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule1_2" runat="server"></asp:checkbox> Информация об остатках <br />	
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule1_3" runat="server"></asp:checkbox> Информация о неоплаченных заказах <br />					        			    
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule1_4" runat="server"></asp:checkbox> Информация о продажах ККМ <br />	
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule1_5" runat="server"></asp:checkbox> Удаление неоплаченного заказа <br />	
				    <asp:checkbox id="rule2" runat="server"></asp:checkbox> <b>Показывать Клиентов</b> <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule2_1" runat="server"></asp:checkbox> Добавление клиента <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule2_2" runat="server"></asp:checkbox> Редактирование клиента <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule2_3" runat="server"></asp:checkbox> Удаление клиента <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule2_4" runat="server"></asp:checkbox> Добавление заказа <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule2_5" runat="server"></asp:checkbox> Удаление продажи <br />				        
				    <asp:checkbox id="rule3" runat="server"></asp:checkbox> <b>Показывать Диллеров</b> <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule3_1" runat="server"></asp:checkbox> Добавление дилера <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule3_2" runat="server"></asp:checkbox> Редактирование дилера <br />
				    <asp:checkbox id="rule4" runat="server"></asp:checkbox> <b>Показывать товары</b> <br /> 
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule4_1" runat="server"></asp:checkbox> Добавление нового кассового аппарата<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule4_2" runat="server"></asp:checkbox> Редактирование кассового аппарата<br />
				    <asp:checkbox id="rule9" runat="server"></asp:checkbox> <b>Показывать Мастер-ЦТО Рамок</b> <br />				    
				    <asp:checkbox id="rule5" runat="server"></asp:checkbox> <b>Показывать ЦТО Рамок</b> <br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule5_1" runat="server"></asp:checkbox> Постановка на ТО<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule5_2" runat="server"></asp:checkbox> Проведение ТО<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule5_3" runat="server"></asp:checkbox> Снятие с ТО<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule5_4" runat="server"></asp:checkbox> Приостановка ТО<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule5_5" runat="server"></asp:checkbox> Установка СКНО<br />
				    <asp:checkbox id="rule6" runat="server"></asp:checkbox> <b>Показывать Ремонт</b> <br /> 
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule6_1" runat="server"></asp:checkbox> Удаление записи о ремонте<br />
				        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:checkbox id="rule6_2" runat="server"></asp:checkbox> Редактирование записи о ремонте<br />
				        				    
				    <asp:checkbox id="rule7" runat="server"></asp:checkbox> <b>Показывать Отчеты</b> <br />   
				    <asp:checkbox id="rule8" runat="server"></asp:checkbox> <b>Показывать Администрирование</b> <br />   
  
				    </td>
				</tr>
								
				<TR>
					<td class="Unit" align="center" colSpan="2" height="19">
						<asp:imagebutton id="cmdEdit2" runat="server" ImageUrl="../Images/update.gif" CommandName="Edit"></asp:imagebutton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
						<asp:imagebutton id="cmdCancel" runat="server" ImageUrl="../Images/cancel.gif" CommandName="Cancel"
							CausesValidation="False"></asp:imagebutton>&nbsp;&nbsp;
						</td>
				</TR>
			</table>
			<uc1:footer id="Footer1" runat="server"></uc1:footer>
			<asp:validationsummary id="ValidationSummary1" runat="server" CssClass="ErrorMessage" HeaderText="Заполните обязательные поля :"
				ShowSummary="False" ShowMessageBox="True"></asp:validationsummary></form>
	</body>
</HTML>

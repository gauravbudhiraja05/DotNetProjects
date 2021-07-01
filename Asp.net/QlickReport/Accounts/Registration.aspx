<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Registration.aspx.vb" Inherits="Accounts_Registration" title="Untitled Page" %>

<%--Project Name: IDMS Phase 2
    Module Name: Accounts Management
    Page Name: Registration
    Summary: Regiseters the New User
    Created on: 10/05/08
    Created By: Yogesh Kumar Verma

--%>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">

<script language="javascript" type="text/javascript">


function CheckAvailable()
	{
	

	if(document.getElementById("<%=txtUserId.ClientId %>").value!="")
	{
	var str=document.getElementById("<%=txtUserId.ClientId %>").value;
	AjaxClass.CheckName(str,CheckUser);
	}
	else
	{
		alert("Please Enter UserId");
	}
	
	}
	
	function CheckUser(res)
	{
	alert(res.value);
	}

</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<asp:textbox id="txtEnteredDate" style="Z-INDEX: 102; LEFT: 989px; POSITION: absolute; TOP: 300px" runat="server" Visible="False"></asp:textbox>
<asp:dropdownlist id="cboState" style="Z-INDEX: 101; LEFT: 987px; POSITION: absolute; TOP: 270px" tabIndex="12" runat="server" Visible="False" Width="216px"></asp:dropdownlist>
	<table class="table" border="0" id="PassUtility" style="background-color:#CC6666" summary="Purpose of this form:Reset User Password, Unlock/Unlock Account, Set Default Password, Set Password Duration" cellpadding="0" cellspacing="0"  width="90%" align="center">
    <tr>
        <td style="background-color:#CC6666; height:22px; width:10px" scope="col"></td>
        <td style="background-color:#CC6666; height:22px; width:15px" align="left" scope="col">
            <Img src="../images/UserLogo.jpg" alt="User image"/>
        </td>
        <td scope="col" style="background-color:#CC6666; height:22px" valign="bottom" align="right"><span style="font-size: 12pt; color:White;"><strong>Create New User Account&nbsp;&nbsp;</strong></span></td>
    </tr>
    <tr><td scope="colgroup" style="background-color:#df9f9f; height:5px" colspan="3"></td></tr>
</table>
	<table width="90%" align="center" style="background:#cccccc;" >
	    <tr>
	        <td scope="rowgroup">
	            <table cellspacing="0" cellpadding="0" width="85%"  style="background:#cccccc;" align="center" class="table" summary="purpose of this page is registration of new employee">
		            <caption style="background:#666666">Personal Information</caption>
			            <tr>
				            <td style="WIDTH: 199px; HEIGHT: 15px" scope="col"></td>
				            <td style="HEIGHT: 15px; width: 166px;" scope="col">&nbsp;</td>
				            <td style="width: 167px" scope="col"></td>
				            <td style="width: 173px" scope="col"></td>
			            </tr>
			            <%--<tr>
				            <td class="label" style="background:#cccccc" style="HEIGHT: 15px; width: 166px;">Name<asp:label id="Label1" runat="server" ForeColor="#C00000" CssClass ="">*</asp:label></td>
				            <td class="label" style="background:#cccccc" style="HEIGHT: 15px; width: 166px;">Employee Id<asp:label id="Label6" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td class="label" style="background:#cccccc" style="HEIGHT: 15px; width: 166px;">Employee Id<asp:label id="Label7" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td class="label" style="background:#cccccc" style="HEIGHT: 15px; width: 166px;">Employee Id<asp:label id="Label8" runat="server" ForeColor="#C00000">*</asp:label></td>
			            </tr>
			            <tr>
				            <td class="label" style="background:#cccccc"  style="HEIGHT: 15px; width: 166px;"><asp:textbox id="Textbox1" tabIndex="2" Runat="server" MaxLength="50" CssClass="textBox" BorderColor="black" BorderWidth="1" Height="15"  ToolTip="Enter Name"/></td>
				            <td style="HEIGHT: 15px; width: 166px;"><asp:textbox id="Textbox2" tabIndex="3" width="145" Runat="server" MaxLength="10" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter EmployeeId"></asp:textbox></td>
				            <td style="width: 166px"><asp:textbox id="Textbox3" tabIndex="3" width="145" Runat="server" MaxLength="10" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter EmployeeId"></asp:textbox></td>
				            <td style="width: 166px"><asp:textbox id="Textbox4" tabIndex="3" width="145" Runat="server" MaxLength="10" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter EmployeeId"></asp:textbox></td>
			            </tr>
			          
			     
			     </table>
			     <table cellspacing="0" cellpadding="0" width="90%"  style="background:#cccccc;" align="center" class="table" summary="purpose of this page is registration of new employee">
			      --%>      <tr>
				            <td class="label" style="background:#cccccc; width: 199px; height: 37px;" scope="col">&nbsp;Name<asp:label id="lblName" runat="server" ForeColor="#C00000" CssClass ="">*</asp:label></td>
				            <td colspan="3" style="height: 37px" scope="colgroup">
				            <label for="ctl00_MainPlaceHolder_cboPrefix"></label>
				                <asp:dropdownlist id="cboPrefix" tabIndex="1" runat="server" CssClass="dropdownlist" ToolTip="Select Name Prefix" Width="55px">
					                <asp:ListItem Value="Mr.">Mr.</asp:ListItem>
					                <asp:ListItem Value="Miss">Miss</asp:ListItem>
					                <asp:ListItem Value="Mrs.">Mrs.</asp:ListItem>
					            </asp:dropdownlist>
					            <label for="ctl00_MainPlaceHolder_txtName"></label>
					            <asp:textbox id="txtName" tabIndex="2" Runat="server" MaxLength="50" CssClass="textBox" BorderColor="black" BorderWidth="1" Height="15"  ToolTip="Enter Name"/>
                                <asp:RegularExpressionValidator ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtName" ErrorMessage="Enter Alphabet,Dot  Only(Min 3 character)" ValidationExpression="([a-zA-Z][a-zA-Z.][a-zA-Z.])[a-z A-Z.]*"></asp:RegularExpressionValidator>
                            </td>
		                </tr>
		                <tr>
		                    <td style="height: 12px; width: 199px;" scope="col"></td><td colspan="3"  scope="colgroup" style="height: 12px">Name must contain Alphabets Only</td>
		                </tr>
			            <tr>
				            <td style="background:#cccccc; width: 199px;" class="label" scope="col"><label for="ctl00_MainPlaceHolder_txtEmpId">Employee ID</label><asp:label id="Label2" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td>
				            <asp:textbox id="txtEmpId" tabIndex="3" width="145" Runat="server" MaxLength="10" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter Employee Id"></asp:textbox></td><td colspan="2">&nbsp;<asp:CompareValidator ID="CompareValidator1" ControlToValidate="txtEmpId" Type="Integer" Operator="DataTypeCheck" runat="server" ErrorMessage="Enter Numerics Only!"></asp:CompareValidator></td>
			            </tr>
			            <tr>
			                <td style="width: 199px" scope="col"></td><td colspan="3" scope="colgroup">Employee Id must contain Numerics Only</td>
		                </tr>
			            <tr>
				            <td style="background:#cccccc;height: 20px; width: 199px;" class="label" scope="col"><label for="ctl00_MainPlaceHolder_cboDesignation">Designation</label><asp:label id="Label3" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td><asp:dropdownlist id="cboDesignation" tabIndex="4" CssClass="dropdownlist" runat="server" ToolTip="Select Designation" AutoPostBack="True"/></td>
				            <td style="text-align: right;background:#cccccc" class="label" scope="col">If Not in list&nbsp;&nbsp;&nbsp;&nbsp;</td>
				            <td style="width: 173px;" scope="col"><asp:textbox id="txtDesig" tabIndex="5" runat="server" MaxLength="40" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter Designation"></asp:textbox></td>
			            </tr>
			            <tr><td style="height: 8px; width: 199px;" scope="col"></td><td colspan ="3" scope="colgroup"><asp:RegularExpressionValidator ID="RegularExpressionValidator3" runat="server" ControlToValidate="txtDesig" ErrorMessage="Enter Alphabets,Underscore and Space Only(Min 3 character)" ValidationExpression="([a-z A-Z][a-z A-Z _][a-z A-Z _])[a-z A-Z _]*"></asp:RegularExpressionValidator>
                           </td></tr>
			            <tr>
				            <td style="background:#cccccc; width: 199px;" class="label" scope="col"><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label> </strong><asp:label id="Label5" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td colspan="3" scope="colgroup"><asp:dropdownlist id="DepartmentName" tabIndex="6" CssClass="dropdownlist" runat="server" BorderColor="black" BorderWidth="1" Height="20" AutoPostBack="True" ToolTip="Select Department"></asp:dropdownlist></td>
			            </tr>
			            <tr><td style="height: 8px; width: 199px;" scope="col"></td></tr>
			            <tr>
				            <td style="background:#cccccc; width: 199px;" class="label" scope="col"><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label> </strong></td>
				            <td style="HEIGHT: 20px" colspan="2" scope="colgroup"><asp:dropdownlist id="ClientName" tabIndex="7" runat="server" AutoPostBack="True" CssClass ="dropdownlist" ToolTip="Select Client"></asp:dropdownlist></td>
			            </tr>
			            <tr><td style="height: 8px; width: 199px;" scope="col"></td></tr>
			            <tr>
				            <td style="background:#cccccc; width: 199px;" class ="label" scope="col"><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label> </strong></td>
				            <td style="HEIGHT: 8px" colspan="2" scope="colgroup"><asp:dropdownlist id="cboLOBDept" tabIndex="8" CssClass="dropdownlist" runat="server" ToolTip="Select Lob"></asp:dropdownlist></td>
			            </tr>
			            <tr><td style="height: 8px; width: 199px;" scope="col"></td></tr><label for="ctl00_MainPlaceHolder_CBOBU" runat="server" visible="false"  >BU</label><asp:dropdownlist id="CBOBU" tabIndex="9" runat="server" Visible="false" CssClass="dropdownlist" ToolTip="Select BU"></asp:dropdownlist>&nbsp;&nbsp;&nbsp;
			           <tr>
				            <td style="background:#cccccc; width: 199px;" class="label" scope="col"><label for="ctl00_MainPlaceHolder_txtEmail">Email</label> <asp:label id="Label4" runat="server" ForeColor="#C00000">*</asp:label></td>
				            <td colspan="3" scope="colgroup"><asp:textbox id="txtEmail" tabIndex="10" Width="145px" Runat="server" MaxLength="40"  BorderColor="black" BorderWidth="1" Height="15" CssClass="textBox" ToolTip="Enter Email Id" CausesValidation="True"></asp:textbox><asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Fill Valid Email Id" ControlToValidate="txtEmail" ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$">Fill Valid Email Id</asp:RegularExpressionValidator></td>
			            </tr>
			            <tr><td style="height: 8px; width: 199px;" scope="col"></td></tr>
			            <tr>
				            <td style="background:#cccccc; width: 199px;" class="label" scope="col"><label for="ctl00_MainPlaceHolder_chkSelectscope">Scope</label> <asp:label id="lblscope" runat="server" ForeColor="#C00000" Visible="False">*</asp:label></td>
				            <td colspan="3" scope="colgroup"><asp:checkbox id="chkSelectscope" Visible="true" Runat="server" TabIndex="11" Text=" Local" ToolTip="Making Local"/></td>
			            </tr>
	            </table>
	            <table cellspacing="0" cellpadding="0" width="90%" class="table" summary="purpose of this page is registration of new employee">
		                <caption class="caption" style="background:#666666">Login Information</caption>
		                    <tr>
			                    <td style="WIDTH: 198px; HEIGHT: 3px" scope="col"></td>
			                    <td style="HEIGHT: 3px" scope="col">&nbsp;</td>
		                    </tr>
        	                <tr>
			                    <td style="background:#cccccc; height: 39px;" class="label" scope="col">&nbsp;Email Id<asp:label id="lblUserId" runat="server" ForeColor="#C00000">*</asp:label></td>
			                    <td style="height: 28px" scope="col">
			                    <label for="ctl00_MainPlaceHolder_txtUserId"></label>
			                    <asp:textbox id="txtUserId" tabindex="12"  BorderColor="black" BorderWidth="1" 
                                        type="text" maxlength="20" tooltip="Enter User Id" runat="server" 
                                        cssclass="textBox"></asp:textbox>&nbsp;&nbsp;&nbsp;&nbsp;<asp:Button ID="btnAvailable" TabIndex="13" width="125" ToolTip="To Check User Id Availability" text ="Check Availability" runat="server" CssClass="button" />
                                    <asp:RegularExpressionValidator ID="RegularExpressionValidator5" runat="server" ErrorMessage="Fill Valid Email Id" ControlToValidate="txtUserId"
                                     ValidationExpression="^[\w-\.]+@([\w-]+\.)+[\w-]{2,4}$">Fill Valid Email Id</asp:RegularExpressionValidator></td>
		                    </tr>
		                    <tr>
		                        <td scope="col"></td>
		                        <td scope="col">(Min 8 and Max 15 char in length)</td>
		                    </tr>
		                    <tr>
			                    <td scope="col" style="height: 28px; font-weight: bold;background:#cccccc" class="label"> <label for="ctl00_MainPlaceHolder_txtPwd">Password</label><asp:label id="lblPassword" runat="server" Width="3px" ForeColor="#C00000" Height="8px">*</asp:label></td>
			                    <td style="height: 28px" scope="col"><asp:textbox id="txtPwd" tabIndex="14"  BorderColor="black" BorderWidth="1" Runat="server" MaxLength="15" TextMode="Password" CssClass="textBox" ToolTip="Enter Password"></asp:textbox><asp:RegularExpressionValidator ID="RegularExpressionValidator4" ControlToValidate="txtPwd" ValidationExpression="^.*(?=.{8,})(?=.*\d)(?=.*[a-zA-Z])(?=.*[0-9])(?=.*[!*,@#$%\(\)\{\}\[\]\\\/|^&+=:;]).*$" runat="server" ErrorMessage="Enter 8-15 Characters in Length (Mix Of Alphabetic, Non Alphabetic & Special Characters)"></asp:RegularExpressionValidator></td>
		                    </tr>
		                    <tr>
		                        <td scope="col"></td>
		                        <td scope="col">(Min 8 and Max 15 char in length)</td>
		                    </tr>
		                    <tr>
			                    <td scope="col" style="background:#cccccc" class="label"><label for="ctl00_MainPlaceHolder_txtConfirmPwd">Confirm Password</label><asp:label id="lblConfPwd" runat="server" ForeColor="#C00000">*</asp:label></td>
			                    <td scope="col"><asp:textbox id="txtConfirmPwd" tabIndex="15" Runat="server" MaxLength="15" TextMode="Password" CssClass="textBox"  BorderColor="black" BorderWidth="1" Height="15" ToolTip="Enter again Password"></asp:textbox></td>
		                    </tr>
				            <tr>
			                    <td style="HEIGHT: 1px" scope="col"></td>
			                    <td style="HEIGHT: 1px" scope="col">&nbsp;</td>
		                    </tr>
		                    <tr>
			                    <td align="center" scope="colgroup" colspan="2" ><br/><asp:button id="cmdsave" tabindex="16"  Runat="server"  CssClass="button" Text="Save" BorderColor="black" BorderWidth="1" Height="18" ToolTip="Click to Register New User"></asp:button></td>
		                    </tr>
		                    <tr>
		                        <td align="left" style="font-style:italic;" scope="col"><font color="red">*</font>Indicate Mandatory Field</td>
		                    </tr>
		                    <tr><td height=10></td></tr>
	            </table>
	          </td>
	       </tr>
	    </table>
	    <table><tr><td height=10></td></tr></table>      
</asp:Content>


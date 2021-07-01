<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="dataimportfromoracle.aspx.vb" Inherits="TableTools_dataimportfromoracle" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type ="text/javascript" >
    function getclient()
			{
				if (document.getElementById("<%=cbodept.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cboclient.ClientId%>").length;i>=0;i--)
					{
						document.getElementById("<%=cboclient.ClientId%>").remove(i);
					}
					for(i=document.getElementById("<%=cbolob.ClientId%>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId%>").remove(i);
					}
				}
				else
				{
			
					AjaxClass1.bindclient(document.getElementById("<%=cbodept.ClientId %>").value,filclient)
					
					var usertype="<%=Session("typeofuser") %>"
					var userid="<%=Session("userid") %>"
					
//					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
					for(i=document.getElementById("<%=cbolob.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId %>").remove(i);
					}
					document.getElementById("<%=cbolob.ClientId %>").options[0]=new Option("--Select--");
				}
			}
			function filclient(res)
			{
				
				for(i=document.getElementById("<%=cboclient.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=cboclient.ClientId%>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cboclient.ClientId%>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cboclient.ClientId%>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
             function getlob()
			{
				var usertype="<%=Session("typeofuser") %>"
				var userid="<%=Session("userid") %>"
				
				if (document.getElementById("<%=cboclient.ClientId%>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=cbolob.ClientId%>").length;i>=0;i--)
					{
						document.getElementById("<%=cbolob.ClientId%>").remove(i);
					}
					}
				else
				{
					AjaxClass1.bindlob(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId%>").value,fillob)
					
				}
//				if (document.getElementById("<%=cboclient.ClientId%>").selectedIndex==0 && document.getElementById("<%=cbodept.ClientId %>").selectedIndex!=0)
//				{
//					AjaxClass1.bindepttabview(document.getElementById("<%=cbodept.ClientId %>").value,usertype,userid,filtab)
//				}
//				else
//				{
//					AjaxClass1.bindclienttabview(document.getElementById("<%=cbodept.ClientId %>").value,document.getElementById("<%=cboclient.ClientId%>").value,usertype,userid,filtab)
//				}
			}
			function fillob(res)
			{
				for(i=document.getElementById("<%=cbolob.ClientId%>").length;i>=0;i--)
				{
					document.getElementById("<%=cbolob.ClientId%>").remove(i);
				}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=cbolob.ClientId%>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=cbolob.ClientId%>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
</script>
    <table class="table" cellpadding="0" width="90%">
        <tr>
	 		<td align="center" colspan="2" class="tableHeader"><asp:label id="lblheading" Width="100%" Runat="server">Data Import From Oracle</asp:label></td>
					</tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label1" runat="server" style="color:Black" CssClass ="label" 
                    Text="Server Name :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="servername" runat="server" Width="168px" CssClass="textbox" 
                    AutoPostBack="True" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg1" Visible="false" Text="Plz enter the server name first" ForeColor="Red"   runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label2" runat="server" style="color:Black" CssClass ="label" 
                    Text="UserId :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="uid" runat="server" Width="168px" AutoPostBack="True"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg2" Visible="false" Text="Plz enter the userid first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
            <td class="style2">
                <asp:Label ID="Label3" runat="server" style="color:Black" CssClass ="label"
                    Text="Password :"></asp:Label>
            </td>
            <td>
                <asp:TextBox ID="pw" runat="server" Width="168px" SkinID="*" 
                    AutoPostBack="True" ></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="msg3" Visible="false" Text="Plz enter the password first" ForeColor="Red" runat="server"></asp:Label>  
            </td>
        </tr>
        <tr>
           <td colspan="2" align="center">
           <asp:Button  ID="getdb" runat="server" CssClass="button" Text="GetTables" /> 
           </td>  
        </tr>
    </table>
    <table class="style3" align="center">
        <tr>
            <td>
                (<span class="style1">*</span>)Only Select 5 tables at a time<strong><br />
                Select Tables</strong></td>
              <td>
                <asp:Label ID="error1" runat="server"  Visible="False" 
              Text="Plz select atleast </br> one table" ForeColor="Red"></asp:Label>
                
              </td>
            <td>
                You can change name of the tables<br />
                <strong>Selected Tables</strong></td>
        </tr>
        <tr>
            <td>
                <asp:ListBox ID="selecttb" runat="server" Height="173px" 
                    Width="180px" SelectionMode="Multiple"></asp:ListBox>
            </td>
            <td>
                <asp:Button ID="addtable" runat="server" CssClass="button" Text="AddTable" /> 
            </td>
            <td>
                <asp:Panel ID="Panel1" runat="server" Height="160px" Width="192px">
                    <asp:TextBox ID="tb0" runat="server" Height="23px" Visible="false" 
                        Width="168px" CssClass="textbox" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb1" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox" ></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb2" runat="server" Height="22px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb3" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                    <br />
                    <asp:TextBox ID="tb4" runat="server" Height="24px" Visible="false" 
                        Width="168px" CssClass="textbox"></asp:TextBox>
                </asp:Panel>
            </td>
            <td>
            <asp:Panel ID="Panel2" runat="server" Height="160px" Width="192px">
            <table>
            <tr><td>
            <asp:Label ID="lbl1" runat="server" Visible="false" Height="18px"    ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl2" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl3" runat="server" Visible="false" Height="18px"  ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl4" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
            <tr><td>
             <asp:Label ID="lbl5" runat="server" Visible="false" Height="18px"   ForeColor="Red" Text="Table are already exist"></asp:Label></td></tr>
                </table></td>
                </asp:Panel>
          <tr><td colspan="4" align="center"><asp:button id="Chkexist" Runat="server" CssClass="button" Text="Check Tables" TabIndex="8"></asp:button></td></tr>
                </table>
    <table runat="server" id="spandisplay" visible="false"> 
                 <tr>
					<td><asp:label id="lbl6" CssClass="label" Runat="server" text="Select Level 1"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="cbodept" tabIndex="4" runat="server" 
                            CssClass="dropdownlist" ToolTip='Select DepartmentName"' 
                            AutoPostBack="True"></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="lbl7" CssClass="label" Runat="server" text="Select Level 2"></asp:label>:-</td>
					<td colspan="2">
                        <asp:dropdownlist id="cboclient" tabIndex="5" runat="server" 
                            CssClass ="dropdownlist" ToolTip='Select ClientName"' AutoPostBack="True"></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="lbl8" CssClass="label" Runat="server" text="Select Level 3"></asp:label>:-</td>
					<td colspan="2">
                        <asp:dropdownlist id="cbolob" TabIndex="6" Runat="server" 
                            CssClass="dropdownlist" ToolTip='"Select Lob"' AutoPostBack="True"></asp:dropdownlist>&nbsp;
					</td>
				</tr>
                <tr>
					<td align="center" colspan="3">
				    <asp:button id="Importdata" Runat="server" CssClass="button" Text="Import Data" ToolTip='"Create Table"' TabIndex="9"></asp:button></td>
				</tr>
             </table>
             <asp:button id="ImportdataSin" Runat="server" Visible="false" CssClass="button" Text="Import Data" ToolTip='"Create Table"' TabIndex="9"></asp:button>
    <br />
    <br />
    <br />
    <br />
    <br />
</asp:Content>


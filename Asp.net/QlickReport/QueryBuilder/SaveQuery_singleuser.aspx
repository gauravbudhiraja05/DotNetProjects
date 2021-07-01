<%@ Page Language="VB" AutoEventWireup="false" CodeFile="SaveQuery_singleuser.aspx.vb" Inherits="QueryBuilder_SaveQuery_singleuser" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head id="Head1" runat="server">
    <title>Save Query</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" type="text/css" />
    <script language="javascript" type="text/javascript" >
        function FillText() {
            document.getElementById("<%=sfilename.ClientID%>").value = window.opener.document.getElementById("queryname").value;
            document.getElementById("<%=q.ClientID%>").value = window.opener.document.getElementById("queryname").value;
            document.Form1.hidtablename.value = window.opener.document.getElementById("hidtablename").value;
            document.Form1.WhereData.value = window.opener.document.getElementById("wheredata").value;
            document.Form1.WhereData1.value = window.opener.document.getElementById("wheredata1").value;
            document.Form1.sdata.value = window.opener.document.getElementById("Showdata1").value;
            document.Form1.crdata.value = window.opener.document.getElementById("crdata").value;
            document.Form1.Column.value = window.opener.document.getElementById("column").value;
            //				document.Form1.txtformula.value=window.opener.document.getElementById("txtaFormula").value;
            document.Form1.txtformula.value = window.opener.document.getElementById("formulaIds").value
            document.Form1.chkper.value = window.opener.document.getElementById("chkPercentage").value;
            document.Form1.hidforname.value = window.opener.document.getElementById("hidFormulaName").value;
        }
        function winclose() {
            //////////fill query name on welcome page
            window.opener.document.getElementById("queryname").value = document.getElementById("<%=sfilename.ClientID%>").value;
            setTimeout("close()", 800, "javascript");
        }
		</script>  
</head>

<body>        
		<form id="Form1" method="post"  runat="server">
			<table summary="Save Query" id="tablogin" style="WIDTH: 100%; POSITION: absolute; HEIGHT: 135px" cellspacing="0" cellpadding="0" runat="server">
				
				<tr>
				    <td scope="col">
				        <table summary="">
				        <caption class="caption" style="background-color:#0591d3">Save Or Update Query</caption>
				            <tr>
					            <td style="WIDTH: 120px; HEIGHT: 24px"><b>&nbsp;User</b>&nbsp;</td>
					            <td style="HEIGHT: 19px" width="100"><asp:label id="lblUser" runat="server" Font-Bold="True" Font-Names="Verdana"></asp:label></td>
				            </tr>
				          <%--  <tr>
					            <td style="WIDTH: 120px; HEIGHT: 24px"><label for="cbodept" class="label">Department</label></td>
					            <td style="HEIGHT: 24px"><asp:dropdownlist CssClass="dropdownlist" id="cbodept" runat="server" AutoPostBack="true"></asp:dropdownlist></td>
				            </tr>
				            <tr>
					            <td style="WIDTH: 120px; HEIGHT: 24px"><label for="cboclient" class="label">Client</label></td>
					            <td style="HEIGHT: 24px"><asp:dropdownlist id="cboclient" CssClass="dropdownlist"  runat="server" AutoPostBack="true"></asp:dropdownlist></td>
				            </tr>
				            <tr>
					            <td style="WIDTH: 120px; HEIGHT: 24px"><label for="cbolob" class="label">LOB</label></td>
					            <td style="HEIGHT: 24px"><asp:dropdownlist id="cbolob" CssClass="dropdownlist" runat="server" AutoPostBack="true" AppendDataBoundItems="True"></asp:dropdownlist></td>
				            </tr>--%>
				            <tr>
					            <td>&nbsp;<strong><label for="sfilename" class="label">Query Name</label></strong></td>
					            <td><asp:textbox id="sfilename" runat="server"></asp:textbox></td>
				            </tr>
				            <%--<tr>
				                <td><strong><label for="chkstatus" class="label">Local</label></strong></td>
				                <td><asp:CheckBox ID="chkstatus" runat="server" ToolTip="Check to make local" Text="Local/Nonlocal" /></td>
				            </tr>--%>
				            <tr>
					            <td align="center" colspan="2"><br/>
						            <input class="button" id="Button1" type="button" value="Save New" name="Button1" runat="server" />&nbsp;<input class="button" id="Button2" type="button" value="Update" name="Button2" runat="server" />
						            <asp:button id="cmdclose" Runat="server" Text="Close" CssClass="button"></asp:button>
					            </td>
				            </tr>
				        </table>
				    </td>
				</tr>
				<tr>
				    <td>
				        <table>
				            <tr>
				                <td><input id="Department1" style="WIDTH: 80%" type="hidden" name="Department" /><!--Newly added--></td>
				            </tr>
				            <tr>
                         
				                <td><label for="Client1" ></label><asp:textbox id="Client1" Runat="server" style="WIDTH: 80%" Visible="false" ></asp:textbox><!--Newly added--></td>
				            </tr>
				            <tr>
				                <td><label for="LOB1" ></label><asp:textbox id="LOB1" style="WIDTH: 80%" Runat="server" Visible="false"></asp:textbox><!--Newly added--></td>
				            </tr>
				            <tr>
				                <td><input id="sdata" style="WIDTH: 80%" type="hidden" name="sdata" runat="server" /><!--Orderbydata--></td>
				            </tr>
				            <tr>
				                <td><input id="WhereData" style="WIDTH: 80%" type="hidden" name="WhereData" runat="server" /><!--WhereData--></td>
				            </tr>
				            <tr>
				                <td><input id="WhereData1" style="WIDTH: 80%" type="hidden" name="WhereData1" runat="server" /><!--WhereData--></td>
				            </tr>
				            <tr>
				                <td><input id="crdata" style="WIDTH: 80%" type="hidden" name="crdata" runat="server" /><!--GroupData--></td>
				            </tr>
				            <tr>
				                <td><input id="selfield" style="WIDTH: 80%" type="hidden" name="selfield" runat="server" /><!--Header--></td>
				            </tr>
				            <tr>
				                <td><input id="txtformula" style="WIDTH: 80%" type="hidden" name="txtformula" runat="server" /><!--Footer--></td>
				            </tr>
				            <tr>
				                <td><input id="Column" style="WIDTH: 80%" type="hidden" name="Column" runat="server" /><!--Column--></td>
				            </tr>
				            <tr>
				                <td><input id="chkper" style="WIDTH: 80%" type="hidden" name="chkper" runat="server" /><!--TableName--></td>
				            </tr>
				            <tr>
				                <td><input id="hidforname" style="WIDTH: 80%" type="hidden" name="hidforname" runat="server" /><!--Headings--></td>
				            </tr>
				            <tr>
				                <td><input id="hidtablename" type="hidden" name="hidtablename" runat="server" /></td>
				            </tr>
				            <tr>
				                <td><label for="txtshowfield" ></label><asp:textbox id="txtshowfield"  runat="server" Visible="False"></asp:textbox></td>
				            </tr>
				            <tr>
				                <td><label for="txthidden" ></label><asp:textbox id="txthidden"  runat="server" Visible="False"></asp:textbox></td>
				            </tr>
				            <tr>
				                <%--<td><asp:textbox id="txttb" runat="server" Visible="false"></asp:textbox></td>--%>
				                <td><label for="txttb" ></label><asp:TextBox ID="txttb" runat="server" Visible="false"></asp:TextBox></td>
				            </tr>
				            <tr>
				                <td><asp:panel id="panconfirm" style="Z-INDEX: 104; LEFT: -8px; POSITION: absolute; TOP: 312px"
				                    runat="server" Visible="False" Height="88px" Width="344px" BorderWidth="1px" BorderStyle="Outset" BorderColor="lightgrey"
				                    BackColor="whitesmoke">
				                    <table id="Table1" style="WIDTH: 342px; HEIGHT: 64px" width="342" border="0" summary="File Already Exists.Do You Want to Replace 
								                    file?">
					                    <tr>
						                    <td style="HEIGHT: 19px" align="center"><b>File Already Exists.Do You Want to Replace 
								                    file?</b></td>
					                    </tr>
					                    <tr>
						                    <td align="center">
							                    <asp:Button id="cmdyes" runat="server" Text="Yes" Width="50px" Cssclass="button"></asp:Button>
							                    <asp:Button id="cmdno" runat="server" Text="No" CssClass="button" Width="50px"></asp:Button></td>
					                    </tr>
				                    </table>
			                        </asp:panel>
			                    </td>
				            </tr>
				            <tr>
				                <td><label for="txttbname" ></label><asp:textbox id="txttbname" runat="server" Visible="False"></asp:textbox></td>
				            </tr>
				            <tr>
				                <td><label for="txtCriteria" ></label><asp:textbox id="txtCriteria" runat="server" Visible="False"></asp:textbox></td>
				            </tr>				            
				        </table>
				    </td>
				</tr>
				            
		</table>  
		<asp:HiddenField ID="owner" runat="server" />         
			<asp:HiddenField ID="d" runat="server" /> 
				<asp:HiddenField ID="c" runat="server" /> 
					<asp:HiddenField ID="l" runat="server" /> 
						<asp:HiddenField ID="q" runat="server" /> 
	    </form>
	</body>
</html>



<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Showdata1.aspx.vb" Inherits="QueryBuilder_Showdata" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us" >
<head runat="server">
   
    <title>Showdata</title>
<script language="javascript" type="text/jscript">
function getclient() // function to bind client 
 {
 
    AjaxSearchBind.bindClientOnDept(document.getElementById("<%=ddlDept.ClientID%>").value,fillclient);
   
        if (document.getElementById("<%=ddlLob.ClientId%>").value!="--Select--")
        {
            GetLOB();
        }
}
		
function fillclient(Response)
{
    for(i=document.getElementById("<%=ddlClient.ClientId%>").length;i>=0;i--)
    {
        document.getElementById("<%=ddlClient.ClientId%>").remove(i);
    }
    var ds = Response.value
				
    if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
    {
        document.getElementById("<%=ddlClient.ClientId%>").options[0]=new Option("--Select--");
        for(i=0;i<ds.Tables[0].Rows.length;i++)
        {
            document.getElementById("<%=ddlClient.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoId);
        }
    }
}

function GetLOB() // function to bind LOB
{
    AjaxSearchBind.bindLobOnDeptClient(document.getElementById("<%=ddlDept.ClientID%>").value,document.getElementById("<%=ddlClient.ClientID%>").value,filllob);
    
}

function filllob(Response)
{
	for(i=document.getElementById("<%=ddlLob.ClientId%>").length;i>=0;i--)
    {
        document.getElementById("<%=ddlLob.ClientId%>").remove(i);
    }
    var ds = Response.value
				
    if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
    {
        document.getElementById("<%=ddlLob.ClientId%>").options[0]=new Option("--Select--");
        for(i=0;i<ds.Tables[0].Rows.length;i++)
        {
            document.getElementById("<%=ddlLob.ClientId%>").options[i+1]=new Option(ds.Tables[0].Rows[i].LOBName,ds.Tables[0].Rows[i].AutoID);
        }
    }
}

		
</script>
	
</head>
<body>
    <form id="form1" runat="server">
    	<input type="hidden" name="__EVENTTARGET"/> <input type="hidden" name="__EVENTARGUMENT"/>
			<input id="hidData" type="hidden" name="hidData" runat="server"/>			
			<input id="txtstrdiv" type="hidden" name="txtstrdiv" runat="server"/>
			<asp:TextBox ID="htmlheadtxt" Visible="False" Runat="server"></asp:TextBox>
			<asp:TextBox ID="htmlqueryname" Visible="False" Runat="server"></asp:TextBox>
			<input type="hidden" id="queryname" name="queryname" runat="server"/><!--Query Name-->
			<input type="hidden" id="txtheading" name="txtheading" runat="server"/>
			<input id="txtdivshow" type="hidden" name="txtstrdiv" runat="server"/>
			<table summary="">
			<tr align="center">
			<asp:label id="lblhtmlname" Runat="server"></asp:label></tr>
			</table>
			<table width="100%" summary="">
				<tr>
					<td align="center" scope="col">
						<asp:label id="lblMain" Runat="server"></asp:label>
						
					</td>
				</tr>
				
			</table>
			<table style="BACKGROUND-COLOR: white" cellspacing="0" cellpadding="0" border="0" summary="">
				<tr>
			
					<%--<td style="WIDTH: 35px"><IMG style="MARGIN-TOP: 0px; MARGIN-LEFT: 0px; MARGIN-RIGHT: 0px" alt="Send Mail" src="/IDMS/Images/mail.jpg" width="25"></td>--%>
					<!--td style="WIDTH:35px"><IMG style="MARGIN-TOP: 0px;MARGIN-LEFT: 0px;MARGIN-RIGHT: 0px" onclick="javascript:window.xls();"
							src="/IDMS/Images/excel_32.gif" alt="Export to Excel" width="25"></td-->
					<td style="WIDTH: 35px"><asp:imagebutton id="imgexl" BorderWidth="0" height="25" width="25" Runat="server" ImageUrl="/AutoWhiz/Images/excel_32.gif"></asp:imagebutton></td>
					<td style="WIDTH: 35px"><img style="MARGIN-TOP: 0px; MARGIN-LEFT: 0px; MARGIN-RIGHT: 0px" alt="View Chart" src="/AutoWhiz/Images/data_32.gif" width="25" /></td>
				</tr>
			</table>
			<table>
			    <tr>
					<td colspan="4" scope="colgroup">
						<div id="divhtml" runat="server">
							<table>								
								<tr>
									<td><strong><%=Request("queryname")%></strong></td>
								</tr>
								<tr>
								    <td>
								        <table>
								            <tr>
									            <td><label for="ddlDept" class="label">Department</label></td>
									            <td><select id="ddlDept" onchange="javascript:getclient();" name="cbodept" runat="server" style="width: 114px"></select></td>
								            </tr>
								            <tr>
									            <td><label for="ddlClient" class="label">Client</label></td>
									            <td><select id="ddlClient" onchange="javascript:GetLOB();" name="cboclient" runat="server" style="width: 114px"></select>
									            </td>
								            </tr>
								            <tr>
									            <td><label for="ddlLob" class="label">LOB</label></td>
									            <td><select id="ddlLob" name="cbolob" runat="server" style="width: 114px"></select></td>
								            </tr>   
								        </table>
								    </td>
								</tr>
								<tr>
								    <td>
								        <table summary="">
								            <tr>
								                <td><label for="cbolob" class="label">Report Name:</label></td>
								                <td><asp:textbox id="txtname" style="width: 144px; height: 22px;" Runat="server"></asp:textbox></td>
								                <td><asp:button id="cmdsave" Runat="server" Visible="False" CssClass="button" Text="Save as HTML"></asp:button></td>
								                <td><input type="button"  name="cmdGo" value="Save as HTML" onclick="ClickHTMLBut()" id="Button1" /></td>
								            </tr>
								        </table>
								    </td>
								</tr>								
							</table>
						</div>
					</td>
				</tr>
			</table>
    </form>
</body>
</html>

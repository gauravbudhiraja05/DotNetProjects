<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultOutput1.aspx.vb" Inherits="QueryBuilder_ResultOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Drill Down</title>
    <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
    <script language="javascript" type="text/jscript">
function getclient() // function to bind client 
 {
 
    AjaxSearchBind.bindClientOnDept(document.getElementById("<%=ddlDept.ClientID%>").value,fillclient);
   if (document.getElementById("<%=ddlDept.ClientId%>").value!="--Select--")
        {
        document.getElementById("<%=ddpt.ClientID%>").value=document.getElementById("<%=ddlDept.ClientID%>").value;
        }
   
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
 if (document.getElementById("<%=ddlClient.ClientId%>").value!="--Select--")
        {
        document.getElementById("<%=dclt.ClientID%>").value=document.getElementById("<%=ddlClient.ClientID%>").value;
        }
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
function LOBid()
{
if (document.getElementById("<%=ddlLob.ClientId%>").value!="--Select--")
        {
        document.getElementById("<%=dlob.ClientID%>").value=document.getElementById("<%=ddlLob.ClientID%>").value;
        }
}
		function xls()
		{ 
		    try
		    {
		        var emdata =("<%=Path1%>");			
			    window.open(emdata);
		    }
		    catch(e)
		    {
		        
		    }
			
			
		}		
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table summary="">
        <tr>    
            <td scope="col">
                <label id="ErrMsg" runat="server" style="color:Red" for=""></label>
            </td>
        </tr>
    </table>
    <asp:GridView ID="DataGridView1" runat="server" BackColor="White" BorderColor="#E7E7FF" BorderWidth="1px" CellPadding="3" Font-Size="X-Small"
        Height="32px" Width="544px" AllowSorting="True" BorderStyle="None" GridLines="Horizontal" Visible="False">
        <FooterStyle BackColor="#B5C7DE" ForeColor="#4A3C8C" />
        <RowStyle BackColor="#E7E7FF" ForeColor="#4A3C8C" />
        <SelectedRowStyle BackColor="#738A9C" Font-Bold="True" ForeColor="#F7F7F7" />
        <PagerStyle ForeColor="#4A3C8C" HorizontalAlign="Right" BackColor="#E7E7FF" />
        <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#F7F7F7" />
        <AlternatingRowStyle BorderStyle="Solid" HorizontalAlign="Left"
            VerticalAlign="Middle" Wrap="False" BackColor="#F7F7F7" />
    </asp:GridView>
   

    </div>
    <div id="divGen" runat="server"></div>
    <input type="hidden" name="__EVENTTARGET"/> <input type="hidden" name="__EVENTARGUMENT"/>
			<input id="hidData" type="hidden" name="hidData" runat="server"/>			
			<input id="txtstrdiv" type="hidden" name="txtstrdiv" runat="server"/>
			<asp:TextBox ID="htmlheadtxt" Visible="False" Runat="server"></asp:TextBox>
			<asp:TextBox ID="htmlqueryname" Visible="False" Runat="server"></asp:TextBox>
			<input type="hidden" id="queryname" name="queryname" runat="server"/><!--Query Name-->
			<input type="hidden" id="txtheading" name="txtheading" runat="server"/>
			<input id="txtdivshow" type="hidden" name="txtstrdiv" runat="server"/>
			<table>
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
					<td style="WIDTH: 35px"><asp:imagebutton id="imgexl" BorderWidth="0" height="25" AlternateText ="export to excel" width="25" Runat="server" ImageUrl="/AutoWhiz/Images/excel_32.gif"></asp:imagebutton></td>
					<%--<td style="WIDTH: 35px"><img style="MARGIN-TOP: 0px; MARGIN-LEFT: 0px; MARGIN-RIGHT: 0px" alt="View Chart" src="/AutoWhiz/Images/data_32.gif" width="25" /></td>--%>
				</tr>
			</table>
			<table summary="">
			    <tr>
					<td colspan="4" scope="col">
						<div id="divhtml" runat="server">
							<table>								
								<tr>
									<td scope="col"><b><%=Request("queryname")%></b></td>
								</tr>
								<tr>
								    <td>
								        <table>
								            <tr>
									            <td id="dept_td" runat="server" visible="false" scope="col"><label for="ddlDept" class="label">Department</label></td>
									            <td scope="col"><select id="ddlDept" runat="server" visible="false" onchange="javascript:getclient();" name="cbodept" runat="server" style="width: 114px"></select>
									            </td>
								            </tr>
								            <tr>
									            <td id="client_td" runat="server" visible="false" scope="col"><label for="ddlClient" class="label">Client</label></td>
									            <td scope="col"><select id="ddlClient" runat="server" visible="false" onchange="javascript:GetLOB();" name="cboclient" runat="server" style="width: 114px"></select>
									            </td>
								            </tr>
								            <tr>
									            <td id="lob_td" runat="server" visible="false" scope="col"><label for="ddlLob" class="label">LOB</label></td>
									            <td scope="col"><select id="ddlLob" runat="server" visible="false" name="cbolob" runat="server" style="width: 114px" onchange="javascript:LOBid();"></select></td>
								            </tr>   
								        </table>
								    </td>
								</tr>
								<tr>
								    <td>
								        <table>
								            <tr>
								                <td scope="col"><label for="txtname" class="label">Report Name:</label></td>
								                <td scope="col"><asp:textbox id="txtname" style="width: 144px; height: 22px;" Runat="server"></asp:textbox></td>
								                <td scope="col"><asp:button id="cmdsave" Visible="false" Runat="server" CssClass="button" Text="Save as HTML"></asp:button>
                                                    <asp:button id="cmdsave_singleuser" Visible="false" Runat="server" CssClass="button" Text="Save as HTML"></asp:button></td>
								                <td scope="col"><input type="button" runat="server"  name="cmdGo" value="Save as HTML"  id="Button1" disabled="disabled" visible="false" /></td>
								            </tr>
								        </table>
								    </td>
								</tr>								
							</table>
						</div>
					</td>
				</tr>
			</table>
			<input type="hidden" runat="server" id="ddpt" />
			<input type="hidden" runat="server" id="dclt" />
			<input type="hidden" runat="server" id="dlob" />
			
			<input type="hidden" runat="server" id="where" />
			<input type="hidden" runat="server" id="table1" />
			<input type="hidden" runat="server" id="txtfor" />
			<input type="hidden" runat="server" id="showd" />
			<input type="hidden" runat="server" id="showd1" />
			<input type="hidden" runat="server" id="crd" />
			<input type="hidden" runat="server" id="col" />
			<asp:HiddenField ID="tostrorecolumn" runat="server" />
	
    </form>
</body>
</html>

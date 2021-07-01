<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ResultOutput.aspx.vb" Inherits="QueryBuilder_ResultOutput" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
      <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
    <script language="javascript" type="text/jscript">
function getclient() // function to bind client 
 {
 
    AjaxSearchBind.bindClientOnDept(document.getElementById("<%=ddlDept.ClientID%>").value,fillclient);
//   if (document.getElementById("<%=ddlDept.ClientId%>").value!="--Select--")
//        {
//        document.getElementById("<%=ddpt.ClientID%>").value=document.getElementById("<%=ddlDept.ClientID%>").value;
//        }
//   
//        if (document.getElementById("<%=ddlLob.ClientId%>").value!="--Select--")
//        {
//            GetLOB();
//        }
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
<script language="javascript" type="text/javascript">
<!--





// -->
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
					<td scope="col" style="WIDTH: 35px; height: 25px;"><asp:imagebutton id="imgexl" 
                            BorderWidth="0" height="25" width="25" Runat="server" 
                            ImageUrl="/QlickReport/Images/excel_32.gif"></asp:imagebutton></td>
					<%--<td scope="col" style="WIDTH: 35px; height: 25px;"><img style="MARGIN-TOP: 0px; MARGIN-LEFT: 0px; MARGIN-RIGHT: 0px" alt="View Chart" src="/AutoWhiz/Images/data_32.gif" width="25" id="IMG1" language="javascript" onclick="return IMG1_onclick()" /></td>--%>
				    <td style="height: 25px"><asp:CheckBox ID="subtotal" runat="server" Text="Sub Total" ToolTip="Check to view subtotal" /></td>
				    <td style="height: 25px"></td>
			<td style="height: 25px"> <asp:button  text="Process" cssclass="button" runat="server" id="Button4" /></td>
			<td style="height: 25px"><asp:Button ID="drilldown" runat="server" ToolTip="Process DrillDown" Text="DrillDown" CssClass="button" /></td>
				</tr>
				
			</table>
			<table summary="" id="TABLE1" >
			    <tr>
					<td colspan="4" scope="colgroup">
						<div id="divhtml" runat="server">
							<table>								
								<tr>
									<td><b><%=Request("queryname")%></b></td>
								</tr>
								<tr>
								    <td>
								        <table>
								            <tr>
									            <td scope="col"><label  class="label" for="ddlDept">Department</label></td>
									            <td scope="col"><select id="ddlDept" onchange="javascript:getclient();" name="cbodept" runat="server" style="width: 114px"></select>
									            </td>
								            </tr>
								            <tr>
									            <td scope="col"><label for="cboclient" class="label">Client</label></td>
									            <td scope="col"><select id="ddlClient" onchange="javascript:GetLOB();" name="cboclient" runat="server" style="width: 114px"></select>
									            </td>
								            </tr>
								            <tr>
									            <td scope="col"><label for="cbolob" class="label">LOB</label></td>
									            <td scope="col"><select id="ddlLob" name="cbolob" runat="server" style="width: 114px" onchange="javascript:LOBid();"></select></td>
								            </tr>   
								        </table>
								    </td>
								</tr>
								<tr>
								    <td>
								        <table>
								            <tr>
								                <td scope="col"> <label for="cbolob" class="label">Report Name:</label></td>
								                <td scope="col"><asp:textbox id="txtname" style="width: 144px; height: 22px;" Runat="server"></asp:textbox></td>
								                <td scope="col"><asp:button id="cmdsave" Runat="server" CssClass="button" Text="Save as HTML"></asp:button></td>
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
			<asp:HiddenField ID="tostrorecolumn" runat="server" />
			<asp:HiddenField ID="h1" runat="server" />
			<asp:HiddenField ID="h2" runat="server" />
			<asp:HiddenField ID="h3" runat="server" />
			<asp:HiddenField ID="h4" runat="server" />
			<asp:HiddenField ID="h5" runat="server" />
			<asp:HiddenField ID="h6" runat="server" />
			<asp:HiddenField ID="h7" runat="server" />
			<asp:HiddenField ID="h8" runat="server" />
			<asp:HiddenField ID="h9" runat="server" />
			<asp:HiddenField ID="dr" runat="server" />
			
    </form>
</body>
</html>
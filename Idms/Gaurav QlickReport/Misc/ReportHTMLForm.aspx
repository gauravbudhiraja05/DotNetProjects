<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ReportHTMLForm.aspx.vb" Inherits="Misc_ReportHTMLForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>HTML Reports</title>
    <link href="../App_Themes/Themes/StyleSheet.css" rel="stylesheet" />
    <script language="javascript">
			
			function loadme()
			{
		
			   			  
			   if(document.getElementById("hiddpt").value!="")
			   {
			    document.getElementById("cbodept").value=document.getElementById("hiddpt").value;
			    getclient();
			    document.getElementById("hiddpt").value="";
			   }
			    
			}
			function getclient()
			{
				document.getElementById("cboclient").length=0;
				document.getElementById("cbolob").length=0;
				if (document.getElementById("cbodept").selectedIndex!=0)
				{
						AjaxSearchBind.bindClientOnDept(document.getElementById("cbodept").value,filclient)
					
				}
			}
			function filclient(res)
			{
							
				var ds = res.value
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				   	document.getElementById("cboclient").options[0]=new Option("--Select--","0");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
					
					    document.getElementById("cboclient").options[i+1]=new Option(ds.Tables[0].Rows[i].ClientName,ds.Tables[0].Rows[i].AutoId);
				    }
			    }
			    if(document.getElementById("hidcl").value!="")
			    { 
			        document.getElementById("cboclient").value=document.getElementById("hidcl").value;
			        document.getElementById("hidcl").value="";
			        getlob();
			        
			     }
			  
			}
			function getlob()
			{
				document.getElementById("cbolob").length=0;
				if (document.getElementById("cboclient").selectedIndex!=0)
				{
					AjaxSearchBind.bindLobOnDeptClient(document.getElementById("cbodept").value,document.getElementById("cboclient").value,fillob)
				}
			}
			function fillob(res)
			{
				var ds = res.value
				
				if(ds!= null && typeof(ds) == "object" && ds.Tables!=null)
				{
				   	document.getElementById("cbolob").options[0]=new Option("--Select--","0");
				    for(i=0;i<ds.Tables[0].Rows.length;i++)
				    {
				
					    document.getElementById("cbolob").options[i+1]=new Option(ds.Tables[0].Rows[i].LOBName,ds.Tables[0].Rows[i].AutoID);
				    }
			    }
			     if(document.getElementById("hidlb").value!="")
			    { 
			         document.getElementById("cbolob").value=document.getElementById("hidlb").value;
			         document.getElementById("hidlb").value="";
			    }
			}
		</script>
	</head>
	<body onload="loadme();">
		<form id="Form1" method="post" runat="server" >
		
			<table width="100%" align="center">
				
						
					
					<tr>
						<td style="width: 83px"><label title="Department" for="cbodept" class="label">Department</label></td>
						<td style="width: 41px"><label class="label" title="Client" for="cboclient">Client</label></td>
						<td style="width: 30px"><label class="label" title="LOB" for="cbolob">LOB</label></td>
						<td></td>
					</tr>
					<tr>
						<td style="width: 150px"><asp:dropdownlist id="cbodept" tabIndex="1" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
						<td style="width: 155px"><asp:dropdownlist id="cboclient" tabIndex="2" Runat="server" CssClass="dropdownlist"></asp:dropdownlist></td>
						<td style="width: 151px"><asp:dropdownlist id="cbolob" tabIndex="3" Runat="server" CssClass="dropdownlist"></asp:dropdownlist>&nbsp;
						<td><asp:Button id="cmdshow" ToolTip="Click To View HTML report" runat="server" Text="View HTML Report" CssClass="button"></asp:Button></td>
					</tr>
					<tr>
						<td colspan="4">&nbsp;</td>
					</tr>
					<tr>
						<td colspan="4">
							<asp:DataList ID="dlshow" Runat="server" Width="90%">
								<HeaderTemplate>
									<table width="90%">
										<tr bgcolor="whitesmoke">
											<td><b>Report</b></td>
											<td><b>CreatedOn</b></td>
											<td><b>Delete</b></td>
										</tr>
								</HeaderTemplate>
								<ItemTemplate>
									<tr>
										<td title="Click To View HTML Report">
											<asp:Label ID="lblname"  Runat="server" Text='<%#Container.DataItem("SavedFileName")%>' Visible="False">
											</asp:Label>
											<asp:HyperLink style="color:black;"  ID="hpkpath" Runat="server" Target="New" NavigateUrl='<%#Container.DataItem("Path")%>'>
												<%#Container.DataItem("SavedFileName")%>
											</asp:HyperLink>
										</td>
										<td title="File Created On">
										    <asp:Label ID="lblCreateddate" Runat="server" Text='<%#Container.DataItem("SavedOn")%>'>
											</asp:Label>
										</td>
										<td title="Click To Delete HTML Report"><asp:LinkButton style="color:black;" ID="lkbdelete" Runat="server" CommandName="delete" Text="Delete"></asp:LinkButton></td>
									</tr>
								</ItemTemplate>
								<FooterTemplate>
			</table>
			</FooterTemplate> </asp:DataList></TD></TR></TABLE>
			<asp:Panel ID="pandelete" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="whitesmoke"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 190px; POSITION: absolute; TOP: 112px"
				Height="84px" BorderWidth="1px" Visible="False">
				<TABLE style="WIDTH: 273px; HEIGHT: 64px" width="273">
					<TR>
						<TD align="center"><B>Are you sure, you want to delete?</B></TD>
					</TR>
					<TR>
						<TD align="center">
							<asp:Button id="cmdyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
							<asp:Button id="cmdno" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></TD>
					</TR>
				</TABLE>
			</asp:Panel>
			<asp:TextBox id="txtrepname" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 240px"
				runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox ID="txtclient" Runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox ID="txtlob" Runat="server" Visible="False"></asp:TextBox>
			<input type="hidden" runat="server" id="hiddpt" name="hiddpt" />
			<input type="hidden" runat="server" id="hidcl" name="hidcl" />
			<input type="hidden" runat="server" id="hidlb" name="hidlb" />
		</form>
</body>
</html>

<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="importfile.aspx.vb" Inherits="DataTransfer_importfile" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">
<script language ="javascript" type="text/javascript">
function validate()
{
if(document.getElementById("<%=DepartmentName.ClientId %>").selectedIndex==0)
{
   alert("select Department First")
   return false;
   }
   if(document.getElementById("<%=ddltabname.ClientId %>").selectedIndex==0)
   {
     alert("select tablename")
     return false;
   }
   
   if((document.getElementById("<%=rdAppend.ClientId %>").checked==false) && (document.getElementById("<%=rdOverwrite.ClientId %>").checked==false))
      {
      
   alert("Select operation")
   return;
   }   
   if(document.getElementById("<%=myfile.ClientId %>").value=="")
   {
   alert("Select file to upload")
   return false;
   }
   }
   
   
function seldate(control)
			{
				show_calendar('Form1.'+control);
			}
		function confirm_upload()
			{
			if (confirm("Are you sure you want to Upload  again?") == true)
				return true;
			else
				return false;
			}
		function getclient()
			{
		
			
				if (document.getElementById("<%=DepartmentName.ClientId %>") .selectedIndex==0)
				{
					for(i=document.getElementById ("<%=Clientname.ClientId %>").length;i>=0;i--)
					{
						document.getElementById ("<%=Clientname.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=ddltabname.clientId %>") .length;i>=0;i--)
					{
						document.getElementById("<%=ddltabname.clientId %>").remove(i);
					}
					for(i=document.getElementById("<%=ddlLobName.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddlLobName.ClientId %>").remove(i);
					}
				}
				else
				{
				for(i=document.getElementById ("<%=Clientname.ClientId %>").length;i>=0;i--)
					{
						document.getElementById ("<%=Clientname.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=ddltabname.clientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddltabname.clientId %>").remove(i);
					}
					for(i=document.getElementById("<%=ddlLobName.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddlLobName.ClientId %>").remove(i);
					}
				
				//alert(document.getElementById("<%=hfUserType.ClientId %>").value)
				
				
				  
				   // AjaxClass1.bindclient(document.getElementById("<%=DepartmentName.ClientId %>").value,filclient)
					//AjaxClass1.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value, filtab)
				    DataTransfer.bindclient(document.getElementById("<%=DepartmentName.ClientId %>").value,filclient)
					 var userid =" <%=Session("userid").ToString() %>"
					 DataTransfer.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid, filtab)

				}
			}
			function filclient(res)
			{
				for(i=document.getElementById ("<%=Clientname.ClientId %>").length;i>=0;i--)
					{
						document.getElementById ("<%=Clientname.ClientId %>").remove(i);
					}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById ("<%=Clientname.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById ("<%=Clientname.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function getlob()
			{
			 var userid =" <%=Session("userid").ToString() %>"
				if (document.getElementById ("<%=Clientname.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=ddlLobName.ClientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddlLobName.ClientId %>").remove(i);
					}
					for(i=document.getElementById("<%=ddltabname.clientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddltabname.clientId %>").remove(i);
					}
				}
				else
				{
					AjaxClass1.bindlob(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,fillob)
				}
				if (document.getElementById ("<%=Clientname.ClientId %>").selectedIndex==0 && ocument.getElementById("<%=DepartmentName.ClientId %>").selectedIndex!=0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value, filtab)
				DataTransfer.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid, filtab)

				}
				else
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
				DataTransfer.bindclienttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)

				}
			}
			function fillob(res)
			{
				for(i=document.getElementById("<%=ddlLobName.ClientId %>") .length;i>=0;i--)
					{
						document.getElementById("<%=ddlLobName.ClientId %>").remove(i);
					}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=ddlLobName.ClientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=ddlLobName.ClientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0]);
				}
			}
			function gettab()
			{
			
				
				 var userid =" <%=Session("userid").ToString() %>"
				if (document.getElementById("<%=ddlLobName.ClientId %>").selectedIndex==0)
				{
					for(i=document.getElementById("<%=ddltabname.clientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddltabname.clientId %>").remove(i);
					}
				}
				else if (document.getElementById("<%=ddlLobName.ClientId %>").selectedIndex==0 && document.getElementById ("<%=Clientname.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,filtab)
				DataTransfer.bindepttab(document.getElementById("<%=DepartmentName.ClientId %>").value,userid,filtab)

				}
				else if(document.getElementById("<%=ddlLobName.ClientId %>").selectedIndex==0)
				{
					//AjaxClass1.bindclienttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,filtab)
				DataTransfer.bindclienttab(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,userid,filtab)

				}
				else
				{
					//AjaxClass1.bindtable(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,document.getElementById("<%=ddlLobName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,filtab)
				DataTransfer.bindtable(document.getElementById("<%=DepartmentName.ClientId %>").value,document.getElementById ("<%=Clientname.ClientId %>").value,document.getElementById("<%=ddlLobName.ClientId %>").value,document.getElementById("<%=hfUserType.ClientId %>").value,userid,filtab)

				}
			}
			function filtab(res)
			{
				
				for(i=document.getElementById("<%=ddltabname.clientId %>").length;i>=0;i--)
					{
						document.getElementById("<%=ddltabname.clientId %>").remove(i);
					}
				var strdata = res.value
				var arrdata = strdata.split("$")
				var arrdata1
				document.getElementById("<%=ddltabname.clientId %>").options[0]=new Option("--Select--");
				for(i=0;i<arrdata.length;i++)
				{
					arrdata1 = arrdata[i].split("#")
					document.getElementById("<%=ddltabname.clientId %>").options[i+1]=new Option(arrdata1[1],arrdata1[0] + "$" + arrdata1[1]);
				}
			}
			
			function gettablename()
			{
			
			
			//alert( document.getElementById("<%=ddltabname.clientId %>").items(document.getElementById("<%=ddltabname.clientId %>").selectedIndex).text)
			//alert( document.getElementById("<%=ddltabname.clientId %>").item(document.getElementById("<%=ddltabname.clientId %>").selectedIndex).text)
			 //alert(document.getElementById("<%=ddltabname.clientId %>").text)
			document.getElementById("<%=HiddenField1.ClientId %>").value =  document.getElementById("<%=ddltabname.clientId %>").item(document.getElementById("<%=ddltabname.clientId %>").selectedIndex).text
			//alert(document.getElementById("<%=HiddenField1.ClientId %>").text)
			var table=document.getElementById("<%=ddltabname.clientId %>").item(document.getElementById("<%=ddltabname.clientId %>").selectedIndex).text
			
			"<%session("tablename")= HiddenField1.value  %>"
			
			//=document.getElementById("<%=ddltabname.clientId %>").item(document.getElementById("<%=ddltabname.clientId %>").selectedIndex).text
			//alert(document.getElementById("<%=HiddenField1.clientId %>").text)
			
			
			}
</script> 
<div id="importfile" align="center"  >

<table id="tabFutureclosingPrice" class ="table"   runat="server"   cellpadding="0" cellspacing="0" width="600" summary ="">
		
				<tr >					
				</tr>
				<tr>
					<td>
						<table id="tabtop" rules="rows" runat="server" style="width: 304px" summary ="Table" >
                            <%--<caption   >
                                Import File</caption>
                                --%>
						
							<tbody>
								<tr>
								<td align="center" colspan="2" class="caption" scope ="colgroup" style ="background-color:#0591D3">
                                    Import File</td>
								</tr>
								<tr>
									<td align="left"  class="label" style="width: 124px; color :Black " scope ="col">Department </td>
									<td style="width: 276px" scope ="col" >
										<select id="DepartmentName" tabindex="1" onchange="javascript:getclient();"
											name="DepartmentName" runat="server"  class ="dropdownlist" >
										</select>
									</td>
								</tr>
								<tr>
									<td align="left"  class="label" scope ="col" style="width: 124px; color :Black ">Client</td>
									<td style="width: 276px" scope ="col" > 
										
										<select id="Clientname" tabindex="2" name="Clientname" runat="server"
											 class ="dropdownlist" onchange="javascript:getlob();">
										</select>
										</td>
								</tr>
								
								<tr>
									<td align="left"  class="label" style="width: 124px; color :Black " scope ="col" >LOB</td>
									<td style="width: 276px" scope ="col"> 										
										<select id="ddlLobName" tabindex="3" onchange="javascript:gettab();"
											name="ddlLobName" runat="server" class ="dropdownlist">
										</select>
									</td>									
								</tr> 
								<tr>
									<td align="left"  class="label" style="width: 124px; color : Black " scope ="col">Select Table</td>
									 
										<td style="width: 276px" scope ="col">
										<select id="ddltabname"  class ="dropdownlist" tabindex="2" name="ddlTableName" runat="server" onchange ="gettablename();">
										</select>
										</td>
								</tr>
								<tr>
									<td  class="label" style="width: 124px" scope ="col"  >
									<asp:Label ID="lbloperation" runat ="server" ToolTip ="select operation" Text =" Operation"></asp:Label>
										
										</td>
										<td  class ="label" align ="center"  style="width: 276px" >
										Append Data
                                            <input id="rdAppend" runat="server" name="rdAppend" type="radio" value="Append Data" />
                                            <asp:Label ID="lblspacer" runat="server" Text ="vikas"  ForeColor ="white"  Visible ="false" ></asp:Label> 
                                            Overwrite Data <input type="radio"  id="rdOverwrite" runat="server" value="OverWrite Data" name="rdAppend"/>
									</td>
								</tr>
								<tr>
									<td style="width: 124px; color :Black " valign ="top"  align ="left" scope ="col" ><asp:label id="lblFileName" Runat="server" text="File Name" CssClass="label" Width="80px"></asp:label></td>
									<td style="width: 276px" scope ="col" ><input id="myfile" type="file" size="22" name="myfile" runat="server"/>
										&nbsp;
									</td>
								</tr>
								<tr>
									<td align="center" colspan="3" scope ="colgroup" ><input type="button" id="cmdUpload" runat="server" value="Upload" class="button" name="cmdUpload" title ="Click to upload file"/></td>
								</tr>
							</tbody>
						</table>
					</td>
				</tr>
			</table>
			<asp:TextBox ID="txthiddenserver" Runat="server" Visible="False"></asp:TextBox>
			<asp:textbox id="txthid1" Runat="server" Visible="False"></asp:textbox>
   
			<asp:textbox id="txtTableHidden" Runat="server" Visible="False"></asp:textbox>
			<asp:textbox id="txttablename" Runat="server" Visible="False"></asp:textbox>
			<asp:textbox id="txtFilename" Runat="server" Visible="False"></asp:textbox>&nbsp;
    
      <asp:HiddenField ID="hfUserType" runat="server" />
    <asp:HiddenField ID="hfUserId" runat="server" />
    <input id="hfUtype" type="text" runat ="server" visible="false"   />
    <asp:HiddenField ID="HiddenField1" EnableViewState="true" runat="server" />
    </div>
    
   
    

</asp:Content>


<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="AddNews.aspx.vb" Inherits="FunctionProcess_AddNews" title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainPlaceHolder" Runat="Server">

<script language="javascript" type="text/javascript">

function textCounter( maxlimit)
 {
if (document.getElementById("<%=txtnNews.ClientID%>").value.length > maxlimit) // if too long...trim it!
document.getElementById("<%=txtnNews.ClientID%>").value = document.getElementById("<%=txtnNews.ClientID%>").value.substring(0, maxlimit);
// otherwise, update 'characters left' counter
else 
document.getElementById("<%=remLen.ClientID%>").value = maxlimit - document.getElementById("<%=txtnNews.ClientID%>").value.length;
}
function confirm_delete()
{
  if (confirm("Are you sure you want to delete this item?")==true)
    return true;
  else
    return false;
}	
		</script>
		<script language="javascript" type="text/javascript">
		function ShowCalendar()
	{
	//debugger
	
	document.getElementById("<%=txtStDate.ClientID%>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtStDate.ClientID%>").value, 'dialogLeft:200px;dialogTop:375px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');

	}
	function ShowCalendar2()
	{
	//debugger
		document.getElementById("<%=txtEnDate.ClientId %>").value = window.showModalDialog('../Calendar/Calendar.htm',document.getElementById("<%=txtEnDate.ClientId %>").value, 'dialogLeft:200px;dialogTop:400px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
	
function check()
{
if(document.getElementById("<%=txtnNews.ClientID%>").value.length>100)
 alert("xdfgsdhsdjf");
}
		</script>
		
		
		<div>
		<asp:panel id="pnlConfirmDel" style="Z-INDEX: 104; LEFT: 400px; POSITION: absolute; TOP: 425px"
				runat="server" BorderColor="Gray" BorderStyle="Groove" BorderWidth="2px"  Width="380px" BackColor="#eeeeee" Height="110px">
				<table id="Table1"   style="height: 100% "  cellspacing="0" cellpadding="0" width="100%" summary ="Confirm Deletion Panel">
					<tr>
						<td class="tableHeader" colspan="2" scope="colgroup"  align="center"><strong>Confirm Deletion</strong></td>
					</tr>
					<tr>
					<td colspan="2" scope="colgroup" style="height: 12px"></td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" style ="color:black">&nbsp;Are you sure you want to delete the selected records?</td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup">&nbsp;</td>
					</tr>
					<tr>
						<td align="right" style="width:50%" scope="col" >
							<asp:Button id="cmdYes" runat="server" Text="Yes" Width="40" CssClass="button"></asp:Button></td>
						<td align="left" style="width:50%" scope="col" >
							<asp:Button id="cmdNo" runat="server" Text="No" Width="40" CssClass="button"></asp:Button></td>
					</tr>
					<tr>
						<td align="center" colspan="2" scope="colgroup">&nbsp;</td>
					</tr>
				</table>
			</asp:panel>
<table cellpadding="0" cellspacing="0" width="750" class="table" summary="purpose of this page is creating new news">
				<caption style ="background-color:#0591D3">Add New Messages In Ticker</caption>
				<tr>
					<td scope ="colgroup"  colspan="2">&nbsp;</td>
				</tr>
				<%--<tr>
					<th colspan="3" class="tableHeader">
						Add New Messages In Ticker
					</th>
				</tr>--%>
				<tr>
					<td scope="rowgroup"  style="height: 206px; color :Black ">
						<table id="tabMain" runat="server">
							<tbody>
								<tr>
									<td align="center" scope="rowgroup" >
										<div id="divDatalist" runat="server" style ="overflow:scroll; width:750px;" >
											<asp:datalist id="dlreg" DataKeyField="rec_id" Runat="server" ItemStyle-Wrap="False"
												AlternatingItemStyle-Wrap="False" ShowFooter="True" style ="color:black"  CssClass="datagrid">
												<HeaderStyle ></HeaderStyle>
												<ItemStyle  CssClass="datagrid"/>
												<HeaderTemplate>
													<table border="1"  cellpadding="0" cellspacing="0" width="100%">
														<tr class="datagridHeader">
															<td width="150px" align="center" scope ="col" ><strong>Start Date<br>(mm-dd-yyyy)</strong></td>
															<td width="150px" align="center" scope="col"><strong>End Date<br>(mm-dd-yyyy)</strong></td>
															<td width="250px" align="center"  scope="col"><strong>Message</strong></td>
															<td width="70px" align="center"  scope="col"><strong>Index</strong></td>
															<td width="110px" align="center"  scope="col">
																<asp:LinkButton id="lkbtnSelectAll" Runat="server" Font-Size="7" Font-Bold="true" CommandName="Select All" ToolTip="Select All CheckBox" ><strong>Select All/</strong></asp:LinkButton>
																<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" Font-Size="7" Font-Bold="true" CommandName="DeSelect All" ToolTip="DeSelect All CheckBox" ><strong>DeSelect All</strong></asp:LinkButton>
															</td>
															<td width="50px"  scope="col"><strong>Edit</strong></td>
														</tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr >
														<td valign="top" align="center"  scope="col" >
															<asp:Label text='<%#(container.dataitem("SDate"))%>' Runat ="server"  Width ="150px" ID="Label1">
															</asp:Label>
														</td>
														<td valign="top" align="center"  scope="col">
															<asp:Label text='<%#(container.dataitem("EDate"))%>' Runat="server" Width="150px" ID="Label2">
															</asp:Label>
														</td>
														<td valign="top" align="left"  scope="col" >
															<asp:Label text='<%#(container.dataitem("news"))%>' Runat="server"  Width="250px"  ID="lblLN">
															</asp:Label>
														</td>
														<td valign="top" align="center"  scope="col">
															<asp:Label text='<%#(container.dataitem("ShowIndex"))%>' Runat="server" Width="70px" ID="lblindex">
															</asp:Label>
														</td>
														<td align="center"  scope="col">
															<asp:Label ID="lbchkvisible" AssociatedControlID ="chkVisible" runat ="server"  	></asp:Label>
															<asp:CheckBox ID="chkVisible" Runat="server" ToolTip ="Click to Select"></asp:CheckBox>
														</td>
														<td  scope="col">
															<asp:LinkButton ID="cmdEdit" text="Edit" ToolTip="Edit Record" CommandName="Edit" Runat="server" Width="30px" ></asp:LinkButton>
														</td>
													</tr>
												</ItemTemplate>
												<EditItemTemplate>
													<tr>
														<td align="center"  scope="col">
														<asp:Label AssociatedControlID="txtstrdate" runat="server" ID="lbledittxtstrdate"></asp:Label>
														
															<asp:textbox text='<%#(container.dataitem("SDate"))%>' Runat ="server" Width ="150px" ID="txtstrdate"  >
															</asp:textbox>
														</td>
														<td align="center"  scope="col">
														<asp:Label AssociatedControlID="txtEnddate" runat="server" ID="lbltxtEnddate"></asp:Label>
															<asp:textbox text='<%#(container.dataitem("EDate"))%>' Runat ="server" Width ="150px" ID="txtEnddate" >
															</asp:textbox>
														</td>
														<td align="left"  scope="col">
														<asp:Label AssociatedControlID="txtNews" runat="server" ID="lbltxtNews"></asp:Label>
															<asp:TextBox text='<%#(container.dataitem("news"))%>' Runat ="server" Width ="250px" MaxLength ="100" ID="txtNews" >
															</asp:TextBox>
														</td>
														<td align="center"  scope="col">
															<asp:Label AssociatedControlID="txtindex" runat="server" ID="lbltxtindex"></asp:Label>
															<asp:TextBox text='<%#(container.dataitem("ShowIndex"))%>' Runat ="server" Width ="20px" MaxLength="8" ID="txtindex" >
															</asp:TextBox>
														</td>
														<td align="center"  scope="col"><asp:CheckBox ID="Checkbox1" Runat="server"></asp:CheckBox></td>
														<td>
															<asp:LinkButton ID="cmdUpdate" text="Update" Width="60" ToolTip="Update Record" CommandName="Update" Runat="server" Font-Italic="True" Visible ="true"></asp:LinkButton>
															<asp:LinkButton ID="Linkbutton1" text="Cancel" Width="60" ToolTip="Cancel Record" CommandName="Cancel" Runat="server" Font-Italic="True" Visible ="true"></asp:LinkButton></td>
													</tr>
												</EditItemTemplate>
												<FooterTemplate>
													<tr>
														<td colspan="4" scope ="colgroup" >&nbsp;</td>
														<td align="center"  scope="col">
															<asp:Button ID="btnDelete" Runat="server" Width="60" CommandName="Delete" ToolTip="Delete Checked Records" CssClass="button" Text="Delete"></asp:Button>
														</td>
														<td  scope="col">&nbsp;</td>
													</tr>
						</table>
						</FooterTemplate> </asp:datalist></div> </td>
				</tr></tbody>
			</table>
			</td>
			</tr> 
			
			
			<tr>
			
			<td  scope="col">
			<asp:Button runat="server" ID="cmdAddnew" Text="Add Message" ToolTip="Add Message" CssClass="button"></asp:Button>
			</td>
			</tr>
			<tr>
			<td style="height: 16px"  scope="rowgroup" >
			<asp:Table Runat="server" Visible="false" ID="tabAddNew" >
				<asp:TableRow runat="server">
					<asp:TableCell ColumnSpan="2" CssClass="tableHeader" runat="server"  style ="background-color:#0591D3" >
						<strong>
							<asp:label  ID="lbladd" ForeColor="White" text="Add New Message" runat="server" Font-Size="10pt"></asp:label>
						</strong>
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server">
					<asp:TableCell runat="server" style ="color:black">
						<asp:Label ID="lblStDate" Font-Bold="True" text="Start Date" Runat="server" Width="100px" CssClass="label"></asp:Label>
							<label for="ctl00_MainPlaceHolder_txtStDate"></label>
							<asp:TextBox ID="txtStDate"  CssClass="textBox" runat="server" MaxLength="10" TabIndex="1" ToolTip ="Start Date" ></asp:TextBox>
							<input id="cmdCalender" tabindex="2" style="BORDER-RIGHT: 0px solid; BORDER-TOP: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px;height:15; CURSOR: hand; BORDER-BOTTOM: 0px solid"
							onclick="ShowCalendar(this.id);" title="Show Calendar" type="button" name="imageFromDate1" />
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server">
					<asp:TableCell runat="server" style ="color:black">
						<asp:Label ID="lblEndDate" text="End Date" Font-Bold="True"  Runat="server" Width="100px" CssClass="label"></asp:Label>
							<label for="ctl00_MainPlaceHolder_txtEnDate"></label>
							<asp:TextBox ID="txtEnDate"  CssClass="textBox" runat="server" MaxLength="10" TabIndex="1" ToolTip ="End Date" ></asp:TextBox>
							<input id="cmdCalender1" tabindex="4" style="BORDER-RIGHT: 0px solid; BORDER-TOP: 0px solid; BACKGROUND: url(../Calendar/Calendar.gif); BORDER-LEFT: 0px solid; WIDTH: 16px;height:15; CURSOR: hand; BORDER-BOTTOM: 0px solid"
							onclick="ShowCalendar2(this.id);" title="Show Calendar" type="button" name="imageFromDate2" />
					</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server">
					<asp:TableCell CssClass="label" runat="server" style ="color:black" >
						<strong>Enter Message </strong>
						<%--<asp:TextBox ID="txtnNews" Runat="server" Width="200px"  TabIndex="5" ToolTip="Enter News" CssClass="greyHeader_1"  TextMode="multiline" MaxLength ="100"    Rows ="3" ></asp:TextBox>
                       --%> 
           <label for="ctl00_MainPlaceHolder_txtnNews"></label>           
 <textarea  id="txtnNews" runat ="server" cols="28" rows="4" tabindex ="5" name ="txtnNews" onkeydown ="javascript:textCounter(100)" onkeyup ="javascript:textCounter(100)"  ></textarea>
                         
                          </asp:TableCell>
                        		
				</asp:TableRow>
				<asp:TableRow ID="TableRow1" runat="server">
					<asp:TableCell ID="TableCell1" CssClass="label" runat="server">
					          <%-- <asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate ="txtnNews" runat="server" ErrorMessage="Range exceeded ( max length 100 )" Display="Dynamic" ValidationExpression="[\S\s\W]{1,100}"></asp:RegularExpressionValidator>
			--%>	
			  <label for="ctl00_MainPlaceHolder_remLen"></label> 
			 <input name="remLen" id ="remLen" type ="text"  runat ="server" maxlength ="3" value ="100" size ="3" readonly="readonly" />
			 
			</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server">
					<asp:TableCell ColumnSpan="2" HorizontalAlign="Center" runat="server">
						<asp:Button runat="server" Width="65px" ID="cmdSave" CssClass="button" TabIndex="6" ToolTip="Save News [ALT+V]" AccessKey="v" Text="Save" CausesValidation ="true" ></asp:Button>
						
					<asp:Button runat="server" Width="65px" ID="cmdCancel" CssClass="button" TabIndex="7" ToolTip="Reset Values[ALT+C]" AccessKey="c" Text="Cancel"></asp:Button>
				</asp:TableCell>
				</asp:TableRow>
				<asp:TableRow runat="server">
					<asp:TableCell CssClass="tableHeader" ColumnSpan="3" Height="20px" runat="server"></asp:TableCell>
				</asp:TableRow>
			</asp:Table>
			
			</td>
			</tr>
               
    </table>
</div>

</asp:Content>


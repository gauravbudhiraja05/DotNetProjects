<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="DisplayedEvents.aspx.vb" Inherits="Function_And_Process_DisplayedEvents" %>

<asp:Content ID="DisplayEvent" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

<script language="javascript" type="text/javascript">

function textCounter( maxlimit)
 {
if (document.getElementById("<%=txtdesc.ClientID%>").value.length > maxlimit) // if too long...trim it!
document.getElementById("<%=txtdesc.ClientID%>").value = document.getElementById("<%=txtdesc.ClientID%>").value.substring(0, maxlimit);
// otherwise, update 'characters left' counter
else 
document.getElementById("<%=remLen.ClientID%>").value = maxlimit - document.getElementById("<%=txtdesc.ClientID%>").value.length;
}
	//Show Calendar dialog
	function ShowCalendarstart()
	{
		document.all["txtTickerstartdate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtTickerstartdate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
	
	function ShowCalendarend()
	{
		document.all["txtTickerenddate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtTickerenddate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
		</script>
		<script language="javascript" type="text/javascript">
	//Show Calendar dialog
	function ShowCalendarstart1()
	{
		document.all["txtstartdate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtstartdate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
	
	function ShowCalendarend1()
	{
		document.all["txtenddate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtenddate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
 
//-->
		</script>
		<script language="javascript" type="text/javascript">
	//Show Calendar dialog
	function ShowCalendarstart2()
	{
		document.all["txtResStartDate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtResStartDate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
	
	function ShowCalendarend2()
	{
		document.all["txtResenddate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtResenddate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
 
//-->
		</script>
		<script language="javascript" type="text/javascript">
	//Show Calendar dialog
	function ShowCalendarstart3()
	{
		document.all["txtfeaturedstartdate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtfeaturedstartdate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
	
	function ShowCalendarend3()
	{
		document.all["txtfeaturedenddate"].value = window.showModalDialog('../Calender/Calendar.htm',document.all["txtfeaturedenddate"].value, 'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
	}
 
//-->
		</script>
		<asp:panel id="pnlConfirmDel" style="Z-INDEX: 104; LEFT: 400px; POSITION: absolute; TOP: 400px"
				runat="server" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px" BackColor="#eeeeee" Width="375px" Height="110px">
				<table id="Table1"   cellspacing="0" cellpadding="0" width="100%" summary ="Confirm Deletion">
					<tr>
						<td class="tableHeader"  colspan="2" align="center" scope="colgroup" style ="background-color:#0591D3" >Confirm Deletion</td>
					</tr>
					<tr>
					<td colspan="2" height="12" scope="colgroup" ></td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" style ="color:black" >&nbsp;Are you sure you want to delete the selected records?</td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" >&nbsp;</td>
					</tr>
					<tr>
						<td align="right" style="width:50%; height: 22px;" scope ="col">
							<asp:Button id="cmdYes" runat="server" Text="Yes" width="40"  CssClass="button"></asp:Button></td>
						<td align="left"  style="width:50%; height: 22px;" scope ="col">
							<asp:Button id="cmdNo" runat="server" Text="No" width="40" CssClass="button"></asp:Button></td>
					</tr>
					<%--<tr>
						<td  align="center" colspan="2">&nbsp;</td>
					</tr>--%>
				</table>
			</asp:panel>
		
		<table  width="688" border="0" class="table" summary="purpose of this page is adding new URL Link with description">
				<caption style ="background-color:#0591D3">Add URL</caption>
				<tbody>
					<%--<tr>
						<td class="tableHeader" align="center" colspan="4">Add URL</td>
					</tr>--%>
					<tr>
						<td class="label" scope="col" style ="color:black" >&nbsp;Headline&nbsp;</td>
						<td  colspan="3" scope="colgroup" >
<label for="ctl00_MainPlaceHolder_txthead"></label>
							<asp:textbox id="txthead" tabIndex="1" runat="server" Width="328px" CssClass="textBox" MaxLength="100" ToolTip="Enter Headline of URL"></asp:textbox></td>
					</tr>
					<tr >
						<td class="label" scope="col" style ="color:black">Start Date</td>
						<td scope="col" >
<label for="ctl00_MainPlaceHolder_txthead"></label>
							<asp:DropDownList id="cbonewsmonth" Runat="server"  Width="65" CssClass="dropdownlist" ToolTip="Select Month" TabIndex="2"></asp:DropDownList>&nbsp;
						<label for="ctl00_MainPlaceHolder_cbonewsdate"></label>			
					<asp:DropDownList id="cbonewsdate" Runat="server" Width="65" CssClass="dropdownlist" ToolTip="Select Date" TabIndex="3"></asp:DropDownList>&nbsp;
				<label for="ctl00_MainPlaceHolder_cbonewsyear"></label>			
				<asp:DropDownList id="cbonewsyear" Runat="server"  Width="65" CssClass="dropdownlist" ToolTip="Select Year" TabIndex="4"></asp:DropDownList>
						</td>
						<td  scope="col" style ="color:black" >&nbsp;<strong>End Date</strong></td>
						
						<td style="width: 292px"  scope="col" >
							<label for="ctl00_MainPlaceHolder_cbonewsmonth1"></label>
							<asp:DropDownList id="cbonewsmonth1" Runat="server"  Width="65" CssClass="dropdownlist" ToolTip="Select Month" TabIndex="5"></asp:DropDownList>&nbsp;
							<label for="ctl00_MainPlaceHolder_cbonewsdate1"></label>
							<asp:DropDownList id="cbonewsdate1" Runat="server"  Width="65" CssClass="dropdownlist" ToolTip="Select Date" TabIndex="6"></asp:DropDownList>&nbsp;
							<label for="ctl00_MainPlaceHolder_cbonewsyear1"></label>
							<asp:DropDownList id="cbonewsyear1" Runat="server" Width="65" CssClass="dropdownlist" ToolTip="Select Year" TabIndex="7"></asp:DropDownList></td>
					</tr>
					<tr>
						<td class="label" valign="top" rowspan="2" scope="rowgroup" style ="color:black" >&nbsp;Description</td>
						<td  rowspan="2" scope="rowgroup">
							<%--<asp:textbox id="txtdesc" tabIndex="8" runat="server" Height="80px" TextMode="MultiLine" Width="240px"
								MaxLength="500" Rows="10" CssClass="textBox" ToolTip="Enter Description About URL"></asp:textbox>
								<asp:RegularExpressionValidator ID="RegularExpressionValidator2" ControlToValidate ="txtdesc" runat="server" ErrorMessage="Range exceeded ( max length 100 )" Display="Dynamic" ValidationExpression="[\S\s\W]{1,100}"></asp:RegularExpressionValidator>
						--%>
<label for="ctl00_MainPlaceHolder_txtdesc"></label>	
						<textarea  id="txtdesc" runat ="server" cols="28" rows="4" tabindex ="5" name ="txtdesc" onkeydown ="javascript:textCounter(100)" onkeyup ="javascript:textCounter(100)"  ></textarea>
                      <label for="ctl00_MainPlaceHolder_remLen"></label>
                       <input name="remLen" id ="remLen" type ="text"  runat ="server" maxlength ="3" value ="100" size ="3" readonly="readonly" />
			
							</td>
						<td valign="top" class="label" style="width: 419px; height: 47px; color :Black" scope="col">&nbsp;Enter URL</td>
						<td valign="top" style="height: 47px; width: 292px; color :Black" scope="col" >
	<label for="ctl00_MainPlaceHolder_txturlnews"></label>						
<asp:textbox id="txturlnews" tabIndex="9" Runat="server" Width="283px" CssClass="textBox" ToolTip="Enter URL" MaxLength="90"></asp:textbox><br />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ControlToValidate="txturlnews"
                                Display="Dynamic" ErrorMessage="Enter Valid URL" Font-Italic="True" Font-Size="X-Small"
                                ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&=]*)?" Width="184px"></asp:RegularExpressionValidator></td>
					</tr>
					<tr>
						<td colspan="2" scope="colgroup" >&nbsp;
							<asp:button id="cmdNewssave" tabIndex="10" runat="server" Height="24px" Width="68px" Text="Save"
								CssClass="button" ToolTip="Save URL [ALT+V]" AccessKey="v"></asp:button>
							<asp:button id="cmdNewsClear" tabIndex="11" runat="server" Height="24px" Width="68px" Text="Reset"
								CssClass="button" ToolTip="Reset URL [ALT+C]" AccessKey="c" CausesValidation="False"></asp:button>
							<asp:button id="cmdNewsUpdate" tabIndex="12" runat="server" Height="24px" Width="68px" Text="Update"
								CssClass="button" Enabled="False" ToolTip="Update URL [ALT+U]" AccessKey="u"></asp:button></td>
					</tr>
					<tr>
						<td scope ="col" ></td>
					</tr>
					<tr>
						<td align="center" colspan="4" scope="colgroup" >
							<div id="div1" runat="server" style ="color:black; overflow:scroll; width:750px">
								<asp:datalist id="dlexistnews" Runat="server" Width="700px" 
									DataKeyField="autoid" cellpadding="0" cellspacing="0" CssClass="datagrid">
									<HeaderTemplate>
										<table border="1" cellspacing="0" cellpadding="0"  width="100%">
											<tr class="datagridHeader">
												<td style="width:30%" scope="col" > HeadLine</td>
												<td style="width:30%" scope="col" >Start Date</td>
												<td style="width:30%" scope="col">End Date</td>
												<td style="width:30%" scope="col"> URL</td>
												<td style="width:30%" scope="col">Description</td>
												<%--<td width="110px" align="center">
																<asp:LinkButton id="lkbtnSelectAll" Runat="server" Font-Size="7" Font-Bold="true" CommandName="Select All" ToolTip="Select All CheckBoxes" ><b>Select All/</b></asp:LinkButton>
																<asp:LinkButton id="lkbtnDeSelectAll" Runat="server" Font-Size="7" Font-Bold="true" CommandName="DeSelect All" ToolTip="DeSelect All CheckBoxes" ><b>DeSelect All</b></asp:LinkButton>
															</td>--%>
												<td  style="width:30%" scope="col">Edit</td>
											
											</tr>
									</HeaderTemplate>
									<ItemTemplate >
										<tr >
											<td class="white" align="left" scope="col">
												<asp:Label text='<%#container.dataitem("Headline")%>' Runat ="server" ID="lblN_H">
												</asp:Label></td>
											<td scope="col" >
												<asp:Label text='<%#container.dataitem("StartDate")%>' Runat ="server" ID="lblN_D">
												</asp:Label></td>
											<td scope="col" >
												<asp:Label text='<%#container.dataitem("EndDate")%>' Runat ="server" ID="Label2">
												</asp:Label></td>
												<td  scope="col" align ="left" >
												<asp:Label text='<%#container.dataitem("NewsUrl")%>'  Runat ="server" ID="Label1">
												</asp:Label></td>
												<td scope="col" align ="left" >
												<asp:Label text='<%#container.dataitem("News")%>' Runat ="server" ID="Label3">
												</asp:Label></td>
											<%--<td align="center">
															<asp:CheckBox ID="chkVisible" Runat="server"></asp:CheckBox>
											</td>--%>
											<td  scope="col">
												<asp:LinkButton ID="lkNewsEdit" Runat="server" CommandName="Edit" CausesValidation ="false"  ToolTip="Edit Record" >Edit</asp:LinkButton>&nbsp;/&nbsp;
												<asp:LinkButton ID="lkNewsDelete" Runat="server" CommandName="Delete" CausesValidation ="false"  ToolTip="Delete Record" >Delete</asp:LinkButton></td>
										</tr>
									</ItemTemplate>
									<FooterTemplate>
									
			</table>
			</FooterTemplate> 
                                    <HeaderStyle Font-Bold="False" />
                                </asp:datalist></div></td></tr></tbody></table>

			<asp:TextBox id="txtrec_id" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtResId"  runat="server"
				Visible="False"></asp:TextBox>
			<asp:TextBox id="txtnewsid" runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox id="txtTicId"  runat="server"
				Visible="False"></asp:TextBox><br/>
			<asp:ValidationSummary id="ValidationSummary1" runat="server" ShowMessageBox="True" ShowSummary="False"></asp:ValidationSummary>


</asp:Content>




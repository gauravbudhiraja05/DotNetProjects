<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Uploadtemptable.aspx.vb" Inherits="ReportDesigner_Uploadtemptable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type ="text/javascript">
			<!--
    function seldate(control) {
        show_calendar('Form1.' + control);
    }
    function confirm_upload() {
        if (confirm("Are you sure you want to Upload  again?") == true)
            return true;
        else
            return false;
    }
    //-->
    function getclientid() {

        if (document.Form1.DepartmentName.selectedIndex != 0) {
            IDMS.AjaxClass.BindClient(document.Form1.DepartmentName.value, filclient)
        }


    }

    function filclient(res) {
        var m = 0;
        for (m = document.Form1.Clientname.length - 1; m > 0; m--) {
            document.Form1.Clientname.remove(m)
        }
        if (res.value != "N") {
            var strdata = ""
            strdata = res.value;

            var strdataarr = strdata.split("$");
            var x;
            var y = 0;
            var str = ""
            for (x = 1; x <= strdataarr.length; x++) {
                y = y + 1
                strdvalue = strdataarr[x - 1].split("#");
                if (y == 1) {
                    document.Form1.Clientname.options[0] = new Option("--Select--", "Select");
                }
                document.Form1.Clientname.options[x] = new Option(strdvalue[1], strdvalue[0]);
            }

        }



    }
    function getLOB() 

        if (document.Form1.Clientname.selectedIndex != 0) {

            IDMS.AjaxClass.BindLOB(document.Form1.DepartmentName.value, document.Form1.Clientname.value, filLOB)
        }


    }

    function filLOB(res) {


        var m = 0;
        for (m = document.getElementById("ddlLobName").length - 1; m > 0; m--) {
            document.getElementById("ddlLobName").remove(m)

        }
        if (res.value != "N") {
            var strdata = ""
            strdata = res.value;

            var strdataarr = strdata.split("$");
            var x;
            var y = 0;
            var str = ""
            for (x = 1; x <= strdataarr.length; x++) {
                y = y + 1
                strdvalue = strdataarr[x - 1].split("#");
                if (y == 1) {
                    document.getElementById("ddlLobName").options[0] = new Option("--Select--", "Select");
                }
                document.getElementById("ddlLobName").options[x] = new Option(strdvalue[1], strdvalue[0]);
            }

        }

    }

    function ok_onclick()
    {
        window.open("")
    }
    function CheckGridList() {
        alert(document.getElementById("<%=dlreg.ClientId %>").rows.length)

        var grid = document.getElementById("<%= dlreg.ClientID %>");
        var cell;

        if (grid.rows.length > 0) {
            alert(grid.rows.length)
        }
        var n = document.getElementById("<%=dlreg.ClientId %>").rows.length;
        alert(n)

        var i;

        var j = 0;

        for (i = 2; i <= n; i++) {

            if (i < 10) {

                if (document.getElementById("dlreg_ctl0" + i + "chkprimary").checked == true) {

                    if (j > 0) {

                        alert('you can select only one...');

                        document.getElementById("dlreg_ctl0" + i + "chkprimary").checked = false;

                    }

                    else {

                        j++;

                    }

                }
            }

        }


    }

    //else

    //{

    //if(document.getElementById("GridView1_ctl0"+i+"_CheckBox1").checked==true)

    //{

    //if(j>0)

    //{

    //alert('you can select only one...');

    //document.getElementById("GridView1_ctl0"+i+"_CheckBox1").checked=false;

    //}

    //else

    //{

    //j++;

    //}

    //}

    //}

    //}
</script>
		
		<table id="tabFutureclosingPrice" cellspacing="0"  cellpadding="0" width="90%"  class="table" runat="server">
                <%--<tr>
            <td class="label"><asp:Label ID="lblScope" runat="server" Text="Scope" ToolTip="Scope"/>
            </td>
            <td>
                <asp:CheckBox ID="chkSelectscope" Visible="true" runat="server" TabIndex="11" Text=" Local"
                    AutoPostBack="false" Font-Bold="True" Width="72px" />
            </td>
            <td colspan="3">
            </td>
        </tr>
				<tr>
					<td><asp:label id="Label3" CssClass="label" Runat="server" text="Department Name"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="DepartmentName" tabIndex="4" runat="server" CssClass="dropdownlist" AutoPostBack="True" ToolTip='Select DepartmentName"'></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="Label4" CssClass="label" Runat="server" text="Client Name"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="Clientname" tabIndex="5" runat="server" CssClass ="dropdownlist" AutoPostBack="True" ToolTip='Select ClientName"'></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblSelectLOb" CssClass="label" Runat="server" text="Select LOB"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="ddlLobname" TabIndex="6" Runat="server" CssClass="dropdownlist" ToolTip='"Select Lob"' AutoPostBack="True"></asp:dropdownlist>&nbsp;
					</td>
				</tr>--%>
				<tr bgcolor="white">
					<td>
						<table id="tabtop" rules="rows" class="table" align="center" runat="server">
							<tbody>
								<tr>
									<td align="center" colspan="3" class="tableHeader"><asp:label id="lblheading"    Width="100%" Runat="server">Create\Import Table</asp:label></td>
								</tr>
								<tr>
									<td class="style1"><asp:label id="lblFileName" Runat="server" AssociatedControlID ="myfile" text="File Name" CssClass ="label" ToolTip='"File name for creating a table"'></asp:label>:-</td>
									<td class="style1"><input id="myfile" type="file" size="22" name="myfile" runat="server" title="Enter File Path " tabindex="1"/>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;					
									    
									</td>
									<td valign="top" class="style1"><asp:button id="cmdCancel1" Runat="server" CssClass="button" Text="Cancel" ToolTip='"Reset File Path[Alt+C]"' AccessKey="c" TabIndex="2"></asp:button>
                                        <br />
                                        
                                        &nbsp;&nbsp;</td>
								</tr>
                                <tr>
                                <td></td>
                                <td class="style2">(Only Tab Delimited File Allowed)</td>
                                <td class="style2">                                        
                                        <input id="Button1" type="button" 
    value="Tab Delimited Help" onclick="window.open('tabdelimtedhelpfile.aspx')" 
    class="button" /></td>
                                </tr>
								<tr>
									<td align="center" colspan="3"><asp:button id="cmdGetColumn" Width="90px" Runat="server" CssClass="button" Text="Get Column" ToolTip='"Import Column From Table[Alt+G]"' AccessKey="G" TabIndex="3"></asp:button></td>
								</tr>
								<tr>
									<td align="center" colspan="3">
										<div id="divDatalist" runat="server">
										<asp:datalist id="dlreg" Width="100%" Runat="server" CssClass="datagrid" BorderWidth="2px" DataKeyField="autoid" ToolTip="collection of all column">
												<HeaderTemplate >
													<table>
														<tr class="datagridHeader">
															<td width="150px"><b>Column Name</b></td>
															<td width="130px"><b>Data Type</b></td>
															<td width="50px"><b>Size</b></td>
															
															<td  width="80px" align="center" style="color:Black"> <strong>Selected Columns</strong>
																<asp:LinkButton Visible=false id="lkbtnSelectAll" Runat="server" Font-Size="7" CommandName="Select All" ToolTip="Select All Check Box">Select All/</asp:LinkButton>
																<asp:LinkButton  Visible="false" id="lkbtnDeSelectAll" Runat="server" Font-Size="7" CommandName="DeSelect All" ToolTip="DeSelect All Check Box">DeSelect All</asp:LinkButton>
															</td>
															<td width ="50px"><b>Primary Key</b></td>
															<td width="50px"><b>Edit</b></td>
														</tr>
												</HeaderTemplate>
												<ItemTemplate>
													<tr>
														<td>
															<asp:Label text='<%#(container.dataitem("colname"))%>' Runat ="server" Width ="20px" ID="Label1" >
															</asp:Label></td>
														<td>
															<asp:Label text='<%#(container.dataitem("datatype"))%>' Runat="server" Width="20px" ID="Label2">
															</asp:Label></td>
														<td>
															<asp:Label text='<%#(container.dataitem("size"))%>'  Runat="server" Width="30px" ID="lblLN">
															</asp:Label></td>
															
														<td align="center">
															<asp:CheckBox ID="chkVisible" Runat="server" Checked="true" Enabled="false" ></asp:CheckBox>
														</td>
														<td align ="center">
														<%--<input id="chkprimary" runat="server" onchange="CheckGridList();" type="checkbox" />  --%>
														<asp:CheckBox ID="chkprimary" runat="server"  /> 
                                                            <%--<asp:radiobutton id="chkprimary" runat ="server" groupname="primary" />="">--%>
                                                            <td>
															<asp:LinkButton ID="cmdEdit" ForeColor="blue" text="Edit" ToolTip="Edit Record"  CommandName="Edit" Runat="server" Width="30px"></asp:LinkButton></td>
													</tr>
												</ItemTemplate>
												<EditItemTemplate>
													<tr>
														<td>
															<asp:textbox text='<%#(container.dataitem("colname"))%>' Runat ="server" Width ="80px" ID="txtCol" MaxLength="128">
															</asp:textbox></td>
														<td>
															<asp:DropDownList ID="cboEx" Runat="server" width="100px" DataSource="<%# GetdataType() %>" DataTextField="type">
															</asp:DropDownList>
														</td>
														<td>
															<asp:TextBox text='<%#(container.dataitem("size"))%>' Runat ="server" Width ="80px" ID="txtSize">
															</asp:TextBox>
														</td>
														
														<td align="center"><asp:CheckBox ID="Checkbox1" Runat="server"></asp:CheckBox></td>
                                                        <td align="center">
                                                            <asp:CheckBox ID="Checkbox2" runat="server" /></td>
														<td >
															<asp:LinkButton  ID="cmdUpdate" ForeColor="blue" text="Update" ToolTip="Update Record" CommandName="Update" Runat="server" Font-Italic="True"></asp:LinkButton>
															<asp:LinkButton  ID="cmdCancel" ForeColor="blue" text="Cancel" ToolTip="Cancel Update" CommandName="Cancel" Runat="server" Font-Italic="True"></asp:LinkButton>
															
															</td>
													</tr>
												</EditItemTemplate>
												<FooterTemplate>
						</table>
						</FooterTemplate> </asp:datalist></div></td>
				</tr>
				 <%--<tr>
            <td class="label"><asp:Label ID="lblScope" runat="server" Text="Scope" ToolTip="Scope"/>
            </td>
            <td>
                <asp:CheckBox ID="chkSelectscope" Visible="true" runat="server" TabIndex="11" Text=" Local"
                    AutoPostBack="false" Font-Bold="True" Width="72px" />
            </td>
            <td colspan="3">
            </td>
        </tr>
				<tr>
					<td><asp:label id="Label3" CssClass="label" Runat="server" text="Department Name"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="DepartmentName" tabIndex="4" runat="server" CssClass="dropdownlist" AutoPostBack="True" ToolTip='Select DepartmentName"'></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="Label4" CssClass="label" Runat="server" text="Client Name"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="Clientname" tabIndex="5" runat="server" CssClass ="dropdownlist" AutoPostBack="True" ToolTip='Select ClientName"'></asp:dropdownlist>&nbsp;
					</td>
				</tr>
				<tr>
					<td><asp:label id="lblSelectLOb" CssClass="label" Runat="server" text="Select LOB"></asp:label>:-</td>
					<td colspan="2"><asp:dropdownlist id="ddlLobname" TabIndex="6" Runat="server" CssClass="dropdownlist" ToolTip='"Select Lob"' AutoPostBack="True"></asp:dropdownlist>&nbsp;
					</td>
				</tr>--%>
                <tr>
                <td>
                <table id="spandisplay" runat="server" visible="false" class="table">
                <tr>
            <td class="label"><asp:Label ID="lblDept" runat="server" 
                    AssociatedControlID ="DepartmentName" Text=" Select Level 1" 
                    ToolTip="Department"></asp:Label></td>
            <td>
                <asp:DropDownList ID="DepartmentName"  runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip="Select Department" TabIndex="1">
                </asp:DropDownList>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td class="label"><asp:Label ID="lblClient" runat="server" 
                    AssociatedControlID ="Clientname" Text="Select Level 2" ToolTip="Client"/>
            </td>
            <td>
                <asp:DropDownList ID="Clientname"   runat="server" AutoPostBack="True" CssClass="dropdownlist"
                    ToolTip='"Select Client"' TabIndex="2">
                </asp:DropDownList>
            </td>
            <td colspan="3">
            </td>
        </tr>
        <tr>
            <td class="label" style="height: 20px"><asp:Label ID="lblLOB" runat="server" 
                    AssociatedControlID ="ddlLobname" Text="Select Level 3" ToolTip='"LOB"'/></td>
            <td style="height: 20px">
                <asp:DropDownList ID="ddlLobname"  runat="server" CssClass="dropdownlist" ToolTip='"Select Lob"'
                    TabIndex="3" AutoPostBack="True">
                </asp:DropDownList>
            </td>
            <td colspan="3" style="height: 20px">
            </td>
        </tr>
                
                </td>
                </tr>            
                </table>
                </td>
                </tr>
				<tr>
					<td><asp:label id="lbltablename" Runat="server" CssClass="label" text="Table Name"></asp:label>:-</td>
					<td colspan="2"><asp:textbox id="txtTablename" MaxLength="50" CssClass="textbox" Runat="server" ToolTip='Enter Table Name'  TabIndex="7"></asp:textbox>&nbsp;
					</td>
				</tr>
				<tr>
					<td align="center" colspan="3"><asp:button id="cmdUpload_multiuser" Width="90" 
                            Runat="server" CssClass="button" Text="Create Table" 
                            ToolTip='"Create Table[Alt+T]"' AccessKey="T" TabIndex="8" Visible="False"></asp:button>
                        <asp:button id="cmdUpload_singleuser" Width="90" Runat="server" 
                            CssClass="button" Text="Create Table" ToolTip='"Create Table[Alt+T]"' 
                            AccessKey="T" TabIndex="8" Visible="False"></asp:button>&nbsp;&nbsp;&nbsp;
						<asp:button id="cmdImport" Width="90" Runat="server" CssClass="button" Text="Import File" ToolTip='"Import Data From Browse File[Alt+I]"' AccessKey="I" TabIndex="9"></asp:button></td>
				</tr>
				</tbody> 
			</table>
			</td></tr></table>
			<asp:textbox id="txtHidden" Runat="server"  Visible="False"></asp:textbox><asp:textbox id="txthiddenserver" Runat="server" Visible="False"></asp:textbox>
			<asp:textbox id="txthid1" Runat="server" Visible="False"></asp:textbox>
    </div>
    </form>
</body>
</html>

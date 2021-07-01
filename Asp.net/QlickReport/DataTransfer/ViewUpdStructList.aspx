<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation="false"  AutoEventWireup="false" CodeFile="ViewUpdStructList.aspx.vb" Inherits="DataTransfer_ViewUpdStructList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/ecmascript">

    function getclient() {
        if (document.getElementById("<%=cbodept.ClientId %>").selectedIndex == 0) {
            for (i = document.getElementById("<%=cboclient.ClientId %>").length; i >= 0; i--) {
                document.getElementById("<%=cboclient.ClientId %>").remove(i);
            }
            for (i = document.getElementById("<%=cbolob.ClientId %>").length; i >= 0; i--) {
                document.getElementById("<%=cbolob.ClientId %>").remove(i);
            }
        }
        else {
            AjaxClass1.bindclient(document.getElementById("<%=cbodept.ClientId %>").value, filclient)
            //new changes made by vini on 19/11/07
            for (i = document.getElementById("<%=cbolob.ClientId %>").length; i >= 0; i--) {
                document.getElementById("<%=cbolob.ClientId %>").remove(i);
            }
            document.getElementById("<%=cbolob.ClientId %>").options[0] = new Option("--Select--");

            //end on 19/11/07

        }
    }
    function filclient(res) {
        for (i = document.getElementById("<%=cboclient.ClientId %>").length; i >= 0; i--) {
            document.getElementById("<%=cboclient.ClientId %>").remove(i);
        }
        var strdata = res.value
        var arrdata = strdata.split("$")
        var arrdata1
        document.getElementById("<%=cboclient.ClientId %>").options[0] = new Option("--Select--");
        for (i = 0; i < arrdata.length; i++) {
            arrdata1 = arrdata[i].split("#")
            document.getElementById("<%=cboclient.ClientId %>").options[i + 1] = new Option(arrdata1[1], arrdata1[0]);
        }
    }
    function getlob() {
        if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex == 0) {
            for (i = document.getElementById("<%=cbolob.ClientId %>").length; i >= 0; i--) {
                document.getElementById("<%=cbolob.ClientId %>").remove(i);
            }
        }
        else {
            AjaxClass1.bindlob(document.getElementById("<%=cbodept.ClientId %>").value, document.getElementById("<%=cboclient.ClientId %>").value, fillob)
        }
    }
    function fillob(res) {
        for (i = document.getElementById("<%=cbolob.ClientId %>").length; i >= 0; i--) {
            document.getElementById("<%=cbolob.ClientId %>").remove(i);
        }
        var strdata = res.value
        var arrdata = strdata.split("$")
        var arrdata1
        document.getElementById("<%=cbolob.ClientId %>").options[0] = new Option("--Select--");
        for (i = 0; i < arrdata.length; i++) {
            arrdata1 = arrdata[i].split("#")
            document.getElementById("<%=cbolob.ClientId %>").options[i + 1] = new Option(arrdata1[1], arrdata1[0]);
        }
    }
function getspan() {

        document.getElementById("<%=txtclient.ClientId %>").value = 0
        document.getElementById("<%=txtlob.ClientId %>").value = 0
    }
    function getspan2() {

        document.getElementById("<%=txtclient.ClientId %>").value = document.getElementById("<%=cboclient.ClientId %>").value
        document.getElementById("<%=txtlob.ClientId %>").value = document.getElementById("<%=cbolob.ClientId %>").value
    }
</script>
		
		<table width="90%" align="center" class ="table">
		<caption  class ="caption" style ="background-color:#67A897" >Edit Structure</caption>
				<tbody>		
			    <tr>
                <td>
                <table runat="server" visible="false" id="spandisplay1">
                <tr>
                  <td scope="col" style ="color:black"  ><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label> </strong></td>
						<td scope="col" ><asp:dropdownlist id="cbodept"  tabIndex="1" Runat="server" CssClass ="dropdownlist"></asp:dropdownlist></td>
						<td scope="col" style ="color:black" ><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label> </strong></td>
						<td scope="col"><asp:dropdownlist id="cboclient" tabIndex="2" Runat="server" CssClass ="dropdownlist"></asp:dropdownlist></td>
						<td scope="col" style ="color:black"><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label> </strong></td>
						<td scope="col"><asp:dropdownlist id="cbolob" tabIndex="3" Runat="server"  CssClass ="dropdownlist"></asp:dropdownlist></td>
						<td scope="col"><asp:Button id="cmdshowmul" runat="server" Text="Show" CssClass="button" OnClientClick="getspan2();"></asp:Button></td>
                </tr>
                </table>
                </td>
				</tr>
                <tr><td scope="col"><asp:Button id="cmdshow" Visible="false" runat="server" Text="Show" CssClass="button" OnClientClick="getspan();"></asp:Button></td> </tr>
					<tr>
					<td height="30" scope="col"> </td>
					</tr>
					<tr>
						<td colspan="6"  valign="top" scope="col">
							<asp:DataGrid ID="dlshow" Runat="server"  AllowPaging="True" PagerStyle-Mode="NumericPages" Width="90%" PageSize =" 20" AutoGenerateColumns="False" CssClass ="datagrid" style ="color:black" ShowFooter="True">
								<Columns>
									<asp:TemplateColumn>
										<HeaderTemplate >
											<table width="100%" summary ="Table">
												<tr Class="datagridHeader" >
													<td scope="col">
														<asp:LinkButton ID="lnkname" Runat="server" Text="Name" CommandName="sortname"></asp:LinkButton>
													</td>
													<td scope="col">
														<asp:LinkButton ID="lnkdate" Runat="server" Text="Created On" CommandName="sortdate"></asp:LinkButton>
														
													</td>
													<td scope="col">
														<asp:LinkButton ID="lnkcreatedby" Runat="server" Text="Created by" CommandName="sortby"></asp:LinkButton>
													</td>
													<td align="center" scope="col">Delete</td>
												</tr>
										</HeaderTemplate>
										<ItemTemplate>
											<tr>
												<td scope="col"><asp:Label ID="lblid" Runat="server" Visible="False" text='<%#Container.DataItem("cmdid")%>'></asp:Label>
													<asp:LinkButton ID="lkbname" Runat="server" CommandName="select" Text='<%#Container.DataItem("cmdName")%>'>
													</asp:LinkButton></td>
												<td scope="col"><%#Container.DataItem("Createdon")%>&nbsp;</td>
												<td scope="col"><%#Container.DataItem("CreatedBy")%>&nbsp;</td>
												<td align="center" scope="col" ><asp:LinkButton ID="lkbdelete" Runat="server" CommandName="delete"  Text="Delete" ForeColor ="blue" ></asp:LinkButton></td>
											</tr>
										</ItemTemplate>
										<FooterTemplate>
			</table>
			</FooterTemplate> </asp:TemplateColumn> </Columns> 
                                <PagerStyle Mode="NumericPages" />
			</asp:DataGrid></td></tr></tbody></table>
			<asp:TextBox ID="txtrecid" Runat="server" Visible="False"></asp:TextBox>
			<asp:TextBox ID="txtname" Runat="server" Visible="False"></asp:TextBox>
			<asp:Panel ID="pandelete" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="whitesmoke"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 336px; POSITION: absolute; TOP:480px" Height="84px"
				BorderWidth="1px" Visible="False">
				<table style="WIDTH: 273px; HEIGHT: 64px" width="273" summary ="Table">
					<tr>
						<td align="center" style="height: 27px; width: 269px; color :Black " scope="col"><strong>Are you sure, you want to delete?</strong></td>
					</tr>
					<tr>
						<td align="center" style="width: 269px" scope="col">
							<asp:Button id="cmdyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
							<asp:Button id="cmdno" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></td>
					</tr>
				</table>
			</asp:Panel>
			<input type="hidden" id="txtdept" runat="server" name="txtdept"/>
			<input type="hidden" id="txtclient" runat="server" name="txtclient"/>
			<input type="hidden" id="txtlob" runat="server" name="txtlob"/>
</asp:Content>


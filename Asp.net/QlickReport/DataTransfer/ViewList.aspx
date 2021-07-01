<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" EnableEventValidation ="false"  AutoEventWireup="false" CodeFile="ViewList.aspx.vb" Inherits="DataTransfer_ViewList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">

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

 function SetClientLob() {
        if (document.getElementById("<%=cbodept.ClientId %>").selectedIndex != -1 && document.getElementById("<%=cbodept.ClientId %>").selectedIndex != 0) {

            if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex != -1 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex != 0) {

                document.getElementById("<%=hfClient.ClientId %>").value = document.getElementById("<%=cboclient.ClientId %>").item(document.getElementById("<%=cboclient.ClientId %>").selectedIndex).value;
            }
        }
        if (document.getElementById("<%=cboclient.ClientId %>").selectedIndex != -1 && document.getElementById("<%=cboclient.ClientId %>").selectedIndex != 0) {

            document.getElementById("<%=hfLob.ClientId %>").value = document.getElementById("<%=cbolob.ClientId %>").item(document.getElementById("<%=cbolob.ClientId %>").selectedIndex).value;
        }
    }
    function getspan2() {
        document.getElementById("<%=txtclient.ClientId %>").value = document.getElementById("<%=cboclient.ClientId %>").value
        document.getElementById("<%=txtlob.ClientId %>").value = document.getElementById("<%=cbolob.ClientId %>").value

    }
    function getspan() {
        document.getElementById("<%=txtclient.ClientId %>").value = '0'
        document.getElementById("<%=txtlob.ClientId %>").value = '0'
    }
		</script>
		<table width="90%" align="center" class="table" summary ="table">
				<tr><td><caption style ="background-color:#67A897">Edit/Delete View</caption></td></tr>
					<tr><td>
                    <table class="table" id="spandisplaymul" runat="server" visible="false">
                    <tr>
                        <td width="15%" class="label" scope ="col" style ="color:black" ><strong><asp:Label ID="lbl1" Text="Level 1" runat="server" ></asp:Label> </strong></td>
						<td width="20%" scope ="col"><asp:dropdownlist id="cbodept" CssClass="dropdownlist" tabIndex="1" Runat="server"></asp:dropdownlist></td>
						<td width="15%" class="label" scope ="col" style ="color:black; "><strong><asp:Label ID="lbl2" Text="Level 2" runat="server" ></asp:Label></strong></td>
						<td width="20%" scope ="col"><asp:dropdownlist id="cboclient" tabIndex="2" CssClass="dropdownlist" Runat="server"></asp:dropdownlist></td>
						<td width="10%" class="label" scope ="col" style ="color:black"><strong><asp:Label ID="lbl3" Text="Level 3" runat="server" ></asp:Label></strong></td>
						<td width="20%" scope ="col"><asp:dropdownlist id="cbolob" tabIndex="3" CssClass="dropdownlist" Runat="server"></asp:dropdownlist>&nbsp;
						</td>
                    </tr>
                    </table>
                    </td>
					</tr>
                    <tr><td colspan="3"></td><td><asp:Button id="cmdshow" runat="server" OnClientClick="getspan();" Text="Show View" CssClass="button" Visible="false" ></asp:Button></td></tr>
					<tr>
						<td><asp:Button id="cmdshow2" runat="server" OnClientClick="getspan2();" Text="Show" CssClass="button" Visible="false" ></asp:Button>
                            </td></tr>
					<tr>
						<td colspan="6" valign ="top" scope ="colgroup" >
							<asp:DataGrid ID="dlshow" Runat="server" Width="90%" AutoGenerateColumns="False" AllowPaging="True"
								PagerStyle-Mode="NumericPages" PageSize="20" BackColor="LightGrey" CssClass="dropdownlist">
								<Columns>
									<asp:TemplateColumn>
										<HeaderTemplate>
											<table width="100%" summary ="Table" style ="color:black">
												<tr>
													<td scope ="col">
														<asp:LinkButton ID="lnkname" Runat="server"  CommandName="sortname" Text="View Name"></asp:LinkButton>
													</td>
													<td scope ="col">
														<asp:LinkButton ID="lnkdate" Runat="server"  CommandName="sortdate" Text="Created On"></asp:LinkButton>
														(mm-dd-yyyy)
													</td>
													<td scope ="col">
														<asp:LinkButton ID="lnkcreatedby" Runat="server"  CommandName="sortby" Text="Created by"></asp:LinkButton>
													</td>
													<td align="center" scope ="col">Delete</td>
												</tr>
										</HeaderTemplate>
										<ItemTemplate>
											<tr>
												<td scope ="col"><asp:Label ID="lblid" Runat="server" Visible="False" text='<%#Container.DataItem("viewid")%>'></asp:Label>
													<asp:LinkButton ID="lkbname" Runat="server"  CommandName="select" Text='<%#Container.DataItem("ViewName")%>'>
													</asp:LinkButton></td>
												<td scope ="col"><%#Container.DataItem("CreatedOn")%>&nbsp;</td>
												<td scope ="col"><%#Container.DataItem("createdby")%>&nbsp;</td>
												<td align="center" scope ="col"><asp:LinkButton ID="lkbdelete" Runat="server"  CommandName="delete" Text="Delete"></asp:LinkButton></td>
											</tr>
										</ItemTemplate>
										<FooterTemplate>
			</table>
			</FooterTemplate> </asp:TemplateColumn> </Columns> 
                                <PagerStyle Mode="NumericPages" />
			</asp:DataGrid></td></tr></table>
			<label for="ctl00_MainPlaceHolder_txtrecid" > </label>
			<asp:TextBox ID="txtrecid" Runat="server" Visible="False"></asp:TextBox>
    <asp:HiddenField ID="hfClient" EnableViewState="true" runat="server" />
     <asp:HiddenField ID="hfLob" EnableViewState="true" runat="server" />
			<asp:Panel ID="pandelete" Width="272px" Runat="server" BorderColor="lightgrey" BackColor="#eeeeee"
				BorderStyle="Outset" style="Z-INDEX: 101; LEFT: 368px; POSITION: absolute; TOP: 450px"
				Height="84px" BorderWidth="1px" Visible="False">
<table style="WIDTH: 273px; HEIGHT: 64px" width="273" summary ="Table">
  <tr>
    <td align="center" scope ="col" style ="color:black"><strong>Are you sure, you want to delete?</strong></td></tr>
  <tr>
    <td align="center" scope ="col">
<asp:Button id="cmdyes" Runat="server" CssClass="button" Text="Yes" Width="38px"></asp:Button>
<asp:Button id="cmdno" Runat="server" CssClass="button" Text="No" Width="38px"></asp:Button></td></tr></table>
			</asp:Panel>
			<label for="ctl00_MainPlaceHolder_txtview" > </label>
			<asp:TextBox id="txtview" style="Z-INDEX: 102; LEFT: 16px; POSITION: absolute; TOP: 240px" runat="server"
				Visible="False"></asp:TextBox>
			 <input type="hidden" id="txtdept" runat="server" name="txtclient"/>
			<input type="hidden" id="txtclient" runat="server" name="txtclient"/>
			<input type="hidden" id="txtlob" runat="server" name="txtlob"/>
</asp:Content>


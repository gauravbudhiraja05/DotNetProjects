<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="ArchivalReport.aspx.vb" Inherits="DataManager_ArchivalReport" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        #Button1
        {
            width: 96px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript" >
    // <!CDATA[

    function TABLE1_onclick() {

    }


    function getClient() // function to bind client 
    {
        AjaxSearchBind.bindClientOnDept(document.getElementById("<%=ddlDept.ClientID%>").value, fillclient);


        if (document.getElementById("<%=ddlLob.ClientId%>").value != "--Select--") {
            GetLOB()
            restorevalue()
            ////    restorevalue1()


        }


    }

    function fillclient(Response) {

        for (i = document.getElementById("<%=ddlClient.ClientId%>").length; i >= 0; i--) {
            document.getElementById("<%=ddlClient.ClientId%>").remove(i);
        }


        var ds = Response.value

        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            document.getElementById("<%=ddlClient.ClientId%>").options[0] = new Option("--Select--");
            for (i = 0; i < ds.Tables[0].Rows.length; i++) {

                document.getElementById("<%=ddlClient.ClientId%>").options[i + 1] = new Option(ds.Tables[0].Rows[i].ClientName, ds.Tables[0].Rows[i].AutoId);
            }
        }
        //		            document.getElementById("<%=hidClient.ClientId%>").value=  document.getElementById("<%=ddlClient.ClientId%>").value  
    }

    //document.getElementById("<%=hidClient.ClientId%>").value=  document.getElementById("<%=ddlClient.ClientId%>").value  


    function GetLOB() // function to bind LOB
    {
        AjaxSearchBind.bindLobOnDeptClient(document.getElementById("<%=ddlDept.ClientID%>").value, document.getElementById("<%=ddlClient.ClientID%>").value, filllob);

        restorevalue()

    }

    function filllob(Response) {
        for (i = document.getElementById("<%=ddlLob.ClientId%>").length; i >= 0; i--) {
            document.getElementById("<%=ddlLob.ClientId%>").remove(i);
        }


        var ds = Response.value

        if (ds != null && typeof (ds) == "object" && ds.Tables != null) {
            document.getElementById("<%=ddlLob.ClientId%>").options[0] = new Option("--Select--");
            for (i = 0; i < ds.Tables[0].Rows.length; i++) {

                document.getElementById("<%=ddlLob.ClientId%>").options[i + 1] = new Option(ds.Tables[0].Rows[i].LOBName, ds.Tables[0].Rows[i].AutoID);
            }
        }

        //  document.getElementById("<%=hidLob.ClientId%>").value=  document.getElementById("<%=ddlLob.ClientId%>").value    


    }

    function restorevalue() {

        document.getElementById("<%=hidClient.ClientId%>").value = 0
        //     document.getElementById("<%=hidLob.ClientId%>").value=  document.getElementById("<%=ddlLob.ClientId%>").value    
        //alert(document.getElementById("<%=hidClient.ClientId%>").value)
        document.getElementById("<%=hidLob.ClientId%>").value = 0
    }

    ////function restorevalue1()
    ////{

    //////    document.getElementById("<%=hidClient.ClientId%>").value=  document.getElementById("<%=ddlClient.ClientId%>").value
    ////     document.getElementById("<%=hidLob.ClientId%>").value=  document.getElementById("<%=ddlLob.ClientId%>").value    
    ////        alert(document.getElementById("<%=hidLob.ClientId%>").value)
    ////         alert(document.getElementById("<%=hidClient.ClientId%>").value)
    ////}
</script>


<table cellpadding="0"  cellspacing="0" width="90%" style="margin-left:30px;margin-right:30px" summary="Contains Spans"> 
    <tr>
        <td colspan="3" style="height: 20px" ></td>
    </tr>
    <tr>
        <td  scope="row"> <%# Eval("SavedBy") %></td>
        <td  scope="row"><asp:DropDownList ID="ddlDept" runat="server"  
                ToolTip="Select Department" CssClass="dropdownlist" Visible="False"></asp:DropDownList> 
            </td>
        <td scope="row"><%#Eval("createdate")%></td>
        <td scope="row"> <asp:DropDownList ID="ddlClient" Visible="false"   ToolTip="Select Client"  runat="server" CssClass="dropdownlist"></asp:DropDownList></td>
        <td scope="row"><%--<label id="lblLOB" for="ctl00_MainPlaceHolder_ddlLob"  title="Select LOB"  class="label">LOB </label>--%></td>
        <td scope="row"> <asp:DropDownList ID="ddlLob"  ToolTip="Select LOB" runat="server" 
                CssClass="dropdownlist" Visible="False"></asp:DropDownList></td>
    </tr>
    <tr>
        <td  scope="row" colspan="6" style="height:20px">&nbsp;
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
        <td scope="row" align="left"><asp:Button ID="btnShow_singleuser" Text="Show" 
                runat="server" CssClass="button" ToolTip="Show Report"/>&nbsp;<asp:Button 
                ID="btnShow_multiuser" Text="Show" runat="server" CssClass="button" 
                ToolTip="Show Report"/></td>
     </tr>

</table>
<div id="divPaging" runat="server">
    <table cellpadding="0"  cellspacing="0" class="table" summary="Contains Paging ">
         <tr>
             <td>
                <label  for="ctl00_MainPlaceHolder_txtpageing" id="lblpaging"  class="label" >Page Size</label></td>
                <td><input type="text" id="txtpageing"  title=" Apply Page Size" runat="server" />
                    <input type="button" id="btnAPaging"  value="Apply" runat="server"  title="Apply Page Size" class="button"/>
            </td>
    </tr>

    </table>
</div>

<table cellpadding="0"  cellspacing="0" class="table" summary="Contains grid">
    <tr>
        <td colspan="3" style="height: 20px" ></td>
    </tr>
    <tr>
        <td scope="rowgroup">
        <asp:GridView ID="grdArchiverep" AllowPaging="true"  AllowSorting="true" AutoGenerateColumns="false"  runat="server" CssClass="datagrid">
            <Columns>
            <asp:TemplateField>
            <HeaderTemplate >
            ReportName
            </HeaderTemplate>
            <ItemTemplate>
            <asp:Label ID="lblId" Runat="server" Text='<%#Container.DataItem("Recordid")%>' Visible="False"></asp:Label>
            <asp:LinkButton id="lnkRepname" runat="server" Text='<%# Eval("QueryName") %>' CommandName="QueryName"  CssClass="a1"   ToolTip="View Report"> </asp:LinkButton></td>
            </ItemTemplate>
                <HeaderStyle CssClass="datagridHeader" />
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate>
            CreatedBy
            </HeaderTemplate>
            <ItemTemplate>
            <%# Eval("SavedBy") %>
            </ItemTemplate>
                <HeaderStyle CssClass="datagridHeader" />
            </asp:TemplateField>
            <asp:TemplateField>
            <HeaderTemplate >
            AddDate
            </HeaderTemplate>
            <ItemTemplate>
            <%#Eval("createdate")%>
            </ItemTemplate>
                <HeaderStyle CssClass="datagridHeader" />
            </asp:TemplateField>
            <asp:templatefield>
            <headertemplate>
            delete
            </headertemplate>
            <itemtemplate>
            <asp:linkbutton id="lnkdelrec" runat="server" text="Delete" commandname="Recdel" CssClass="a1" ToolTip="Delete Record" ></asp:linkbutton>
            </itemtemplate>
                <HeaderStyle CssClass="datagridHeader" />
            </asp:templatefield>
            <asp:TemplateField>
            <HeaderTemplate>
            UnArchive
            </HeaderTemplate>
            <ItemTemplate>
            <asp:LinkButton ID="lnkUnarchive" runat="server" Text="UnArchive" CommandName="UnArchive" CssClass="a1" ToolTip="UnArchive Record" ></asp:LinkButton>
            </ItemTemplate>
                <HeaderStyle CssClass="datagridHeader" />
            </asp:TemplateField>
           </Columns>
        <PagerStyle  ForeColor="Black"/>
   </asp:GridView>
    </td>
  </tr>
</table>
<div id="PnlDel" runat="server" style="Z-INDEX: 101; LEFT: 342px;  POSITION: absolute;bottom:5px; border-style:outset; border-width:1px; width:272px; height:84px" > 
<table width="100px" style="border-style:none; border-width:1px; width:272px; height:84px ; border-color: #f5f5f5; background-color:#f5f5f5" summary="To show Message in panel pnlDel">
    <tr>
        <td style="height: 19px" scope="row"></td></tr>
    <tr>
    <td  align="center"><label id="lblPurge" for="Pnlpurge">Are You sure You want to Delete Report!!!</label>
    </td>
    </tr>
    <tr>
        <td style="height: 18px" scope="row"></td>
     </tr>
     <tr>
        <td align="center" style="height: 27px" scope="row">
        <input type="button" id="btnDely" value="Yes"  class="buttonnew" runat="server"  title="Click to Delete Data"/>
        <input type="button" id="btnDeln" value="No"  class="buttonnew" runat="server"  title="Click to Cancel Deletion of of Data"/>
        </td>
     </tr>
</table>
</div>
<div id="divUnarchive" runat="server" style="Z-INDEX: 101; LEFT: 342px;POSITION: absolute;bottom:5px; border-style:outset; border-width:1px; width:272px; height:84px" > 
<table width="100px" style="border-style:none; border-width:1px; width:272px; height:84px ; border-color: #f5f5f5; background-color:#f5f5f5" summary="To show Message in panel divUnarcive">
    <tr>
        <td style="height: 19px" scope="row">&nbsp;</td></tr>
    <tr>
    <td  align="center"><label id="Label1" for="Pnlpurge">Are You sure You want to Unarchive the Report!!!</label>
    </td>
    </tr>
    <tr>
        <td style="height: 18px" scope="row"></td>
     </tr>
     <tr>
        <td align="center" style="height: 27px" scope="row">
        <input type="button" id="btnUnarchivey" value="Yes"  class="buttonnew" runat="server"  title="Click to Unarchive Data"/>
        <input type="button" id="btnUnarchiven" value="No"  class="buttonnew" runat="server"  title="Click to Cancel"/>
        </td>
     </tr>
</table>
</div>
<table summary="Contains hiddenfields">
    <tr>
    <td scope="row"><asp:HiddenField ID="hidClient" runat="server" />
    </td>
    <td scope="row"><asp:HiddenField ID="hidLob" runat="server" />
    </td>
     <td scope="row"><asp:HiddenField ID="hidQueryname" runat="server" />
    </td>
     <td scope="row"><asp:HiddenField ID="hidRecordid" runat="server" />
    </td>
    <td scope="row"><asp:HiddenField ID="hiddelrec" runat="server" />
    </td>
    </tr>
</table>
</asp:Content>


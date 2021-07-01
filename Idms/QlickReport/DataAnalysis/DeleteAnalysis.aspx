<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Delete Analysis
    Summary: to delete the selected analysis
    
    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="DeleteAnalysis.aspx.vb" Inherits="DataAnalysis_DeleteAnalysis" title="Delete Analysis" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <%--  <tr>
            <td scope="col" style="width: 40px">
            </td>
            <td scope="col" style="height: 24px; color :Black ">
                <label id="lbdept" for="ctl00_MainPlaceHolder_ddlDepartmant" class="label" title="Department">
                    Department
                </label>
            </td>
            <td scope="col" style="height: 24px; color :Black " title="Select Department">
                <asp:DropDownList ID="ddlDepartmant" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>--%><%--  <tr>
            <td scope="col" style="width: 40px">
            </td>
            <td scope="col" style="height: 25px; color : Black ">
                <label id="lbclient" for="ctl00_MainPlaceHolder_ddlClient" class="label" title="Client">
                    Client
                </label>
            </td>
            <td scope="col" style="height: 25px" title="Select Client">
                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>--%>
<div runat="server" id="paddiv" style="padding:20px">
    <table summary="Select Analysis">
        <caption class="caption" style ="background-color:#67A897; width: 805px;">
            SELECT ANALYSIS
        </caption>
        <%-- <td scope="col" style="width: 40px">
            </td>--%>      <%-- <tr><td style="width: 40px"></td>
    <td style="width: 141px; height: 24px;">Select Report
    </td>
    <td style="width: 174px; height: 24px;"><asp:DropDownList ID="ddlReport" runat="server"> 
        </asp:DropDownList>
    </td>
    </tr>--%>
        <tr>
            <%-- <td scope="col" style="width: 40px">
            </td>--%>
            <tr><td>
                <table runat="server" class="table" id="spandisplay" visible="false">
             <tr>
                <td title="Select level1" scope="col" id="td1"  style="color:Black;width: 171px"></td>
                   <td> <label class="label" title="Select Report" id="Label1" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label> 
                </td>
                <td title="Select Level1" scope="col" id="td2" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DepartmentName" runat="server" AutoPostBack="True">
                        <asp:ListItem>--- Select ---</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr>
                <td title="Select level1" scope="col" id="td3"  style="color:Black;width: 171px"></td>
                  <td><label class="label" title="Select Report" id="Label2" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level2</label> 
                </td>
                <td title="Select Level2" scope="col" id="td4" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ClientName" runat="server" AutoPostBack="True">
                        <asp:ListItem>-- Select--</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr>
                <td title="Select level3" scope="col" id="td5"  style="color:Black;width: 171px"></td>
                  <td><label class="label" title="Select Report" id="Label3" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level3</label> 
                </td>
                <td title="Select Level3" scope="col" id="td6" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ddlLobName" runat="server" AutoPostBack="True">
                        <asp:ListItem>-- Select --</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            </table>
            </td>
            <td>
                <asp:Button ID="getanalysis_singleuser" runat="server" CssClass="button" 
                    Text="Get Analysis" Width="99px" Visible="false" />
                </td></tr>
            <td scope="col" style="width: 141px; color :Black ">
                <label id="selctrpt" for="ctl00_MainPlaceHolder_ddlLob" class="label" title="LOB">
                    Select Analysis
                </label>
            </td>
            <td scope="col" style="width: 174px" title="Select LOB">
                <asp:DropDownList ID="ddlrpt" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>
        <%--<%#databinder.Eval(container.dataitem,"DepartmentName")%>
        --%>
                <tr>
            <%--<%#databinder.Eval(container.dataitem,"ClientName")%>--%>
            <td scope="col" style="height: 26px">
            </td>
            <td>
                <br />
                <br />
                <asp:Button ID="ShowReport" runat="server" CssClass="button" Text="Delete Analysis" 
                    ToolTip="Show Report" />
            </td>
        </tr>
    </table>
    </div>
    <div id="report" runat="server" style="padding:40px;">
  <table>
       <tr>
           <td scope="rowgroup" style="width: 568px; color :Black ">
             <asp:GridView id="grdhtmlreport" CssClass="datagrid"  runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="ReportName" Width="568px" ShowFooter="True">
                <HeaderStyle CssClass="datagridHeader" />
               <Columns>
          
           
           <asp:TemplateField>
           <HeaderTemplate><label runat="server" id="lbldephead">Department Name</label></HeaderTemplate>
           <ItemTemplate>
           <center>
           <label id="lbldept" runat="server"><%--<%#databinder.Eval(container.dataitem,"DepartmentName")%>--%></label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><asp:label id="lbldclthead" runat="server">Client Name</asp:label></HeaderTemplate>
           <ItemTemplate>
           <center>
           <asp:label id="lblclt"  runat="server"><%--<%#databinder.Eval(container.dataitem,"ClientName")%>--%></asp:label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><asp:label id="lbllobhead" runat="server">LOB Name</asp:label></HeaderTemplate><ItemTemplate>
           <center>
           <asp:label id="lbllob" runat="server"><%#databinder.Eval(container.dataitem,"LOBName")%></asp:label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><label id="lblrephead" runat="server">Analysis Name</label></HeaderTemplate>
           <ItemTemplate>
        <%--  
         <asp:HyperLink runat="server" Text='<%#databinder.Eval(container.dataitem,"ReportName")%>' NavigateUrl='<%#~/DataAnalysis/analysisresult.aspx?repname="+databinder.Eval(container.dataitem,"ReportName")%>'>
         </asp:HyperLink>--%>
     
             
           <%--<asp:Label ID="fndlbl"  runat="server" Text=<%#databinder.Eval(container.dataitem,"analysisname")%>></asp:Label>--%>
           
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <%--<HeaderTemplate><label id="lbldelete" runat="server">Delete</label></HeaderTemplate>--%>
           <ItemTemplate>
           <center>
          <%--<asp:LinkButton id="lbjdel" runat="server" Text="Delete" CommandName="delete"></asp:LinkButton>--%>
       <asp:Label AssociatedControlID="chk" runat="server" ID="lblchk"></asp:Label>
        <%-- <asp:CheckBox  ID="chk" runat="server" /></center>--%>
           </ItemTemplate>
           <FooterTemplate>
           <center>
          <%-- <asp:Button ID="lbjdel" class="button" runat="server" Text="Delete" CommandName="delete" /></center>--%>
           </FooterTemplate>
           </asp:TemplateField>
          
           
           
          </Columns>
      
       </asp:GridView>
     </td>
   </tr>
  </table>
 </div>
  <asp:HiddenField runat="server" ID="hid" />
  <asp:HiddenField runat="server" ID="hid1" />
  <asp:HiddenField runat="server" ID="hid2" />
</asp:Content>

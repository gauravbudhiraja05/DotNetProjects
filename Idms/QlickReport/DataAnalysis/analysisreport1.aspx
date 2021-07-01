
<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Html Reports
    Summary: Home Page of the Data Analysis
    
    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" AutoEventWireup="false" MasterPageFile="~/MasterPage/MasterPage.master" CodeFile="analysisreport1.aspx.vb" Inherits="analysisreport1" Title="Html Reports"%>
<%--
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server">
    <title>Html Reports</title>--%>
    <asp:Content ContentPlaceHolderID="LeftPlaceHolder" ID="lftContent" runat="server"> </asp:Content>
    <asp:Content ContentPlaceHolderID="ContentPlaceHolder1" runat="server" ID="mainContent">
     <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
        <%-- <label id="lbdept" for="ctl00_MainPlaceHolder_ddlDepartmant" class="label" title="Department">
         Department
         </label> --%><%-- <asp:DropDownList CssClass="dropdownlist" ID="ddlDepartmant" runat="server" AutoPostBack="True">
       
        </asp:DropDownList> --%>
   <div id="resulDt"  style="padding:20px;" runat="server">
 <table summary="Select Report">
 <center>
         <caption class="caption" style ="background-color:#67A897; width: 796px;"> SELECT REPORT
         </caption>
   </center>
   
     
    
    
     <tr>
               <td><table runat="server" class="table" id="spandisplay" visible="false">
             <tr>
                <td title="Select level1" scope="col" id="td1"  style="color:Black;width: 171px"></td>
                   <td> <label class="label" title="Select Report" id="Label1" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label> 
                </td>
                <td title="Select Level1" scope="col" id="td2" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="level1" runat="server" AutoPostBack="True">
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
                        ID="level2" runat="server" AutoPostBack="True">
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
                        ID="level3" runat="server" AutoPostBack="True">
                        <asp:ListItem>-- Select --</asp:ListItem>
                             </asp:DropDownList>
                </td>
            <td scope="col" style="height: 26px">
     &nbsp;<asp:Button ID="ShowReport_multiuser" CssClass="button" ToolTip="Show Report" runat="server" 
                    Text="Show Report" />
     </td>
    
            </tr>
            </table>
            </td>
            </tr>
 
     
    
    
    <tr>
        <td align="center">
                     <asp:Button ID="ShowReport_singleuser" Visible="false" CssClass="button" ToolTip="Show Report" runat="server" 
                    Text="Show Report" />
     
       </td>
      <td scope="col" style="height: 24px; color :Black " >
          <%-- <asp:DropDownList CssClass="dropdownlist" ID="ddlDepartmant" runat="server" AutoPostBack="True">
       
        </asp:DropDownList> --%>
       </td>
      <td scope="col" title="Select Department" style="height: 24px" >
        <%-- <asp:DropDownList CssClass="dropdownlist" ID="ddlDepartmant" runat="server" AutoPostBack="True">
       
        </asp:DropDownList> --%> 
          <br />
      </td>
    
  </tr> 
  
  <%--<tr>
        <td scope="col" style="width: 40px">
        </td>
        <%--<td scope="col" style="height: 25px; color :Black " > 
          <label id="lbclient" class="label" for="ctl00_MainPlaceHolder_ddlClient" title="Client">
           Client
           </label> 
       </td>
       <td  scope="col"style="height: 25px" title="Select Client" >   
          <asp:DropDownList CssClass="dropdownlist" ID="ddlClient"  runat="server" AutoPostBack="True" >
          </asp:DropDownList>  
      </td>--%>
  </tr>
  <tr>
     <td scope="col" style="width: 40px">
     </td>
     <%--<td scope="col" style="width: 141px; color :Black ">
          <label  class="label" for="ctl00_MainPlaceHolder_ddlLob" title="LOB">
           LOB
           </label> 
     </td>
     <td scope="col" style="width: 174px" title="Select LOB">
        <asp:DropDownList CssClass="dropdownlist"  ID="ddlLob" runat="server" AutoPostBack="True" >
        </asp:DropDownList>  
    
     </td>--%>
  </tr>
  --%> 
    
   <%-- <tr><td style="width: 40px"></td>
    <td style="width: 141px; height: 24px;">Select Report
    </td>
    <td style="width: 174px; height: 24px;"><asp:DropDownList ID="ddlReport" runat="server"> 
        </asp:DropDownList>
    </td>
    </tr>--%>
  <tr>
     <td scope="col" style="width: 40px">
     </td>
     <td scope="col" style="height: 26px">
     
     </td>
    </tr>
    
   
 </table>
        <br />
       
 <div id="report" runat="server" style="padding:40px;">
  <table>
       <tr>
           <td scope="rowgroup" style="width: 568px; color :Black ">
             <asp:GridView id="grdhtmlreport" CssClass="datagrid"  runat="server" AutoGenerateColumns="False" AllowPaging="True" DataKeyNames="ReportName" Width="568px" ShowFooter="True">
                <HeaderStyle CssClass="datagridHeader" />
               <Columns>
          
           
           <asp:TemplateField>
           <HeaderTemplate><asp:label runat="server"  id="lbldephead">DepartmentName</asp:label></HeaderTemplate><ItemTemplate>
           <center>
           <asp:label id="lbldept" runat="server" ><%#databinder.Eval(container.dataitem,"DepartmentName")%></asp:label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><asp:label id="lbldclthead" runat="server" >ClientName</asp:label></HeaderTemplate><ItemTemplate>
           <center>
           <asp:label id="lblclt"  runat="server"><%#databinder.Eval(container.dataitem,"ClientName")%></asp:label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><asp:label id="lbllobhead" runat="server" >LOBName</asp:label></HeaderTemplate><ItemTemplate>
           <center>
           <asp:label id="lbllob" runat="server"><%#databinder.Eval(container.dataitem,"LOBName")%></asp:label></center>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><asp:label id="lblrephead"  runat="server">ReportName</asp:label></HeaderTemplate>
           <ItemTemplate>
        <%--  
         <asp:HyperLink runat="server" Text='<%#databinder.Eval(container.dataitem,"ReportName")%>' NavigateUrl='<%#~/DataAnalysis/analysisresult.aspx?repname="+databinder.Eval(container.dataitem,"ReportName")%>'>
         </asp:HyperLink>--%>
         <%-- <asp:Label ID="lkrepname" runat="server"><a runat="server" id="find" href="analysisresult.aspx?repname="+"<%#databinder.Eval(container.dataitem,"ReportName")%>"><%#databinder.Eval(container.dataitem,"ReportName")%></a></asp:Label>--%>
             <center> <asp:Label  ID="lkrepname" runat="server">
              <a target="_blank" href="html/<%#databinder.Eval(container.dataitem,"ReportName")%>.html?repname=+<%#databinder.Eval(container.dataitem,"ReportName")%>"><font color="blue"><%#DataBinder.Eval(Container.DataItem,"ReportName")%></font>
            </a></asp:Label>
           <asp:Label ID="fndlbl" Visible="false" runat="server" Text='<%#databinder.Eval(container.dataitem,"ReportName")%>'></asp:Label>
           </center>
           </ItemTemplate>
           </asp:TemplateField>
           <%--<asp:TemplateField>
           <HeaderTemplate>View Graph</HeaderTemplate>
           <ItemTemplate>
                <a href="../DataAnalysis/Analysisgrpah.aspx?repnm=<%#databinder.Eval(container.dataitem,"ReportName")%>&source=analysis" target="_blank">View Graph</a>
           </ItemTemplate>
           </asp:TemplateField>--%>
           <asp:TemplateField>
           
          <%-- <HeaderTemplate><label id="lbldelete" runat="server" for="chk">Delete</label></HeaderTemplate>--%>
           
           <ItemTemplate>
           <center>
          <%--<asp:LinkButton id="lbjdel" runat="server" Text="Delete" CommandName="delete"></asp:LinkButton>--%>
          <%--<asp:Label AssociatedControlID="chk" runat="server" ID="lblchk"></asp:Label>--%>
              <%--<asp:CheckBox  ID="chk" runat="server" />--%></center>
           </ItemTemplate>
           <FooterTemplate>
           <center>
           <%--<asp:Button ID="lbjdel" class="button" runat="server" Text="Delete" CommandName="delete" />--%></center>
           </FooterTemplate>
           </asp:TemplateField>
          
           
           
          </Columns>
      
       </asp:GridView>
     </td>
   </tr>
  </table>
 </div>
   
        
        </div>
  <asp:HiddenField runat="server" ID="hid" />
  <asp:HiddenField runat="server" ID="hid1" />
  <asp:HiddenField runat="server" ID="hid2" />
        &nbsp;
        </asp:Content>
       
<%--    </form>
</body>
</html>--%>
<%--
---------------- Change History -------------------------
none
--%>

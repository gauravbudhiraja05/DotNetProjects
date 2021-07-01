<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Filter Percentage
    Summary: filter percentage, index,rating,formulas
    Created on: 10/03/08
    Created By: Ranjit Singh

--%>
<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="FilterPercentage.aspx.vb" Inherits="DataAnalysis_FilterPercentage" title="Filter Percentage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="LeftPlaceHolder" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">
function ok_onclick() 
{
window.open("SaveFilterReport.aspx","SaveFilterReport","height=450,width=450");
}



</script>
<%--<div style="padding:20px;"> 
 
 <table style="width: 600px">
 <caption id="cpt" class="caption">Save Report</caption>
 <tr><td></td></tr>
 <tr><td></td></tr>
            <tr>
            <td align="center">
            </td>
            
            </tr>
            </table>

 </div>--%>
<div id="filter" style="padding:20px">
<table style="width: 645px" summary="FILTER PERCENTAGE">
<caption class="caption">FILTER PERCENTAGE</caption> 
<tr>
<td style="width: 40px; color:black" scope="col"></td></tr>
<tr><td style="width: 40px" scope="col"></td></tr>
  <tr>
    <td scope="col" style="height: 27px; width: 40px;"><asp:Button CssClass="button" ToolTip="Addition" ID="add" runat="server" Text="+" Width="35px" />
    </td>
     <td scope="col" style="height: 27px; width: 40px;"><asp:Button CssClass="button" ToolTip="Subtraction" ID="minus" runat="server" Text="-" Width="35px" />
     </td>
      <td scope="col" style="height: 27px; width: 40px;"><asp:Button CssClass="button" ToolTip="Multiplication" ID="multy" runat="server" Text="*" Width="35px" />
      </td>
       <td scope="col" style="height: 27px; width: 40px;"><asp:Button CssClass="button" ToolTip="Division"  ID="divide" runat="server" Text="/" Width="35px" />
       </td>
        <td scope="col" style="height: 27px; width: 40px;"><asp:Button CssClass="button" ToolTip="Left Bracket" ID="leftb" runat="server" Text="(" Width="35px"/>
        </td>
         <td scope="col" style="height: 27px; width: 418px;"><asp:Button CssClass="button" ToolTip="Right Bracket" ID="rightb" runat="server" Text=")" Width="35px" />
         </td>
         <%--<td scope="col" style="height: 27px"><asp:Button CssClass="button" ToolTip="Min" ID="btnmin" runat="server" Text="Min" />
         </td>
         <td scope="col" style="height: 27px"><asp:Button CssClass="button" ToolTip="Max" ID="btnmax" runat="server" Text="Max" />
         </td>
         <td scope="col" style="height: 27px"><asp:Button CssClass="button" ToolTip="Average"  ID="btnavg" runat="server" Text="Avg" />
         </td>
         <td scope="col" style="height: 27px"><asp:Button CssClass="button" ToolTip="Count"  ID="btncount" runat="server" Text="Count" />
         </td>--%>
      </tr>
      
</table>
<table summary="Make Formula">
    <tr id ="rowarg" runat="server">
        <td style="width: 102px; height: 25px;color:black" scope="col">
        <asp:Label ID="lblselfun" runat="server" CssClass="label" ToolTip="Select Function" Width="134px" Height="15px">
          Select Function
        </asp:Label>
        </td>
        <td style="height: 25px" scope="col">
          <asp:DropDownList ID="ddlfunctions"  CssClass="dropdownlist"  ToolTip="Select Formula" runat="server">
              <asp:ListItem>---select---</asp:ListItem>
              <asp:ListItem>Max</asp:ListItem>
              <asp:ListItem>Min</asp:ListItem>
              <asp:ListItem>Sum</asp:ListItem>
              <asp:ListItem>Avg</asp:ListItem>
              <asp:ListItem>Count</asp:ListItem>
          </asp:DropDownList>
        </td>
        <td style="width: 130px;color:black" scope="col"> &nbsp;&nbsp;
        <asp:Label ID="lblcolumn" runat="server" CssClass="label" ToolTip="Select Column" Width="120px" Height="16px">
               Select Column
        </asp:Label>
        </td>
        <td scope="col" style="height: 25px">
          <asp:DropDownList CssClass="dropdownlist"  ToolTip="Select Column" ID="ddlcolfrfun" runat="server">
          </asp:DropDownList>
          </td>
          <td>
          <asp:Button CssClass="button" ID="btnfun" runat="server" Text="Ok" ToolTip="Ok" Width="35px" />
          
          </td>
       </tr>
       <%--<tr>
       <td>
        <asp:Label ID="lblfnname" runat="server" CssClass="label" ToolTip="Formula Name" Height="15px">
          Formula Name
        </asp:Label>
        </td>
          <td> 
             <asp:TextBox id="txtfuncction" runat="server">
             </asp:TextBox>
          </td> 
          
      </tr>--%>
</table>
<table summary="Make Formula">

        <tr>
           <td scope="rowgroup" style="width: 532px">
             &nbsp;
<table style="width: 221px" summary="Make Formula"> 
    <tr>
    <td scope="col" style="height: 173px; width: 20px;"> 
             
          <asp:ListBox CssClass="listBox"  ToolTip="Columns To Make Formula" ID="listcolumns" runat="server" Height="159px" Width="85px">
           </asp:ListBox>
           </td>
    <td scope="col" style="width: 38px; height: 173px;">
        &nbsp;<asp:Button CssClass="button"  ToolTip="Make Formula" ID="formulafilter" Text="Formula" runat="server" Width="76px" />
        <asp:Button CssClass="button" ToolTip="Clear Formula" ID="clear" runat="server" Text="Clear" Width="76px" />
         </td>

   
     <td scope="col" >
         <asp:TextBox CssClass="textBox" ToolTip="Formula" ID="finalformula" runat="server" Width="85px" Height="144px" TextMode="MultiLine"></asp:TextBox></td>
  </tr>
</table>
      </td> 
      <td scope="col" style="width: 5px">
      </td>
   </tr>
   <%--<tr>
   <td> <asp:Button CssClass="button" ToolTip="Click To Make Group By" ID="GrouBy" runat="server" Text="GroupBy" Width="76px" />
  <asp:TextBox ID="gpby" runat="server" CssClass="textBox" ToolTip="Group By"></asp:TextBox> </td>
   </tr>--%>
   <tr>
   
       <td >
           <label id="nameformula" for="alais" style="color:Black" class="label" title="Formula Name">Formula Name</label>
         
      
     
           <asp:TextBox ID="alais" runat="server" CssClass="textBox" ToolTip="Formula Name"></asp:TextBox>
            <asp:Button CssClass="button" ToolTip="Ok" ID="ok" runat="server" Text="Ok" Width="76px" />
          </td>
   
   </tr>
   <tr>
                    <td style="height: 25px">
                     <asp:TextBox ID="alias1" runat="server" CssClass="textBox" ToolTip="Formula" TextMode="MultiLine" Height="60px" Width="325px"></asp:TextBox>
                    <%-- <input type="text" id="nowcolumns" runat="server" style="width: 198px; height: 26px" />--%>
                    </td>
   </tr>
   <tr>
   <td  scope="col" >
          <asp:Button CssClass="button" ToolTip="Show Report" ID="formulafield" runat="server" Text="Show Report" Width="136px" />&nbsp;
          
          <input onclick="return  ok_onclick()" type="button" id="showcalculator" title="Click To Save The Report" value="Save Report" class="button" />
          
          <%--<asp:Button CssClass="button" ToolTip="Apply Formulas On Report" ID="setfrm" runat="server" Text="Set Formula" />--%>
   </td>
 </tr>
 <tr>
     <td scope="colgroup" colspan="2" style="height: 24px">
         <asp:DropDownList CssClass="dropdownlist" ToolTip="Filter Report Columns" ID="allcolumns" runat="server" AutoPostBack="True">
         </asp:DropDownList>
         <asp:Button CssClass="button" ToolTip="Apply Index" ID="inde" runat="server" Text="Index" />
         <asp:Button CssClass="button" ToolTip="Click To Apply Rating" ID="Rating" runat="server" Text="Rating" Width="64px" />
     </td>
 </tr>
 <tr id="trcols" runat="server">
      <td scope="col"  style="width: 532px">
      <asp:DropDownList CssClass="dropdownlist" ToolTip="Select Formula" ID="selectionformula" runat="server" AutoPostBack="True">
          <asp:ListItem>---Select---</asp:ListItem>
             <asp:ListItem>Greater Than</asp:ListItem>
             <asp:ListItem>Less Than</asp:ListItem>
             <asp:ListItem>Equal To</asp:ListItem>
             <asp:ListItem>Starts With</asp:ListItem>
             <asp:ListItem>Ends With</asp:ListItem>
             <asp:ListItem>Between</asp:ListItem>
       </asp:DropDownList>  
         <asp:TextBox CssClass="textBox" ToolTip="Fill Value To See Filtered Result" ID="valueinput" runat="server" MaxLength="15"></asp:TextBox>
         <asp:Button CssClass="button" ID="go" ToolTip="Go" Text="Go" runat="server" Width="35px" />
      </td>
 </tr>
 <tr id="trcols1" runat="server">
    <td scope="col" id="tdrang" runat="server" style="width: 532px;color:Black">
    <asp:label ID="lblrng" CssClass="label" runat="server" ToolTip="Range From" Height="19px" Width="97px">Range From</asp:label>
            <asp:TextBox CssClass="textBox" ToolTip="Range From" ID="txtfrm" runat="server" MaxLength="15"></asp:TextBox> <asp:label ID="Labfrm" CssClass="label" runat="server" ToolTip="Range To" Height="19px" Width="91px">
            Range To</asp:label><asp:TextBox CssClass="textBox"  ToolTip="Range To"  ID="Textto" runat="server" style="position: static" MaxLength="15"></asp:TextBox></td>
 </tr>
</table></div>
<table style="border:2;" summary="Null"><tr><td>
 <table summary="Null">
    <tr>
                      <td>
                         <div id="Divhead" style="margin-left:20px; width:700px;" runat="server">
                            </div>
                       </td>
         </tr>
 </table>
  <table summary="Null">
           <tr>
                            <td>
                                 <div id="relax" style="margin-left:20px; width:700px; height:300px; overflow:scroll;" runat="server">
                                    </div>
                                    </td>
           </tr>
 </table>
                                    </td>
           </tr>
</table>
        <asp:HiddenField ID="hidfun" runat="server" />
         <asp:HiddenField ID="GroupBy" runat="server" />
         <asp:HiddenField ID="allcolumn" runat="server" />
         <asp:HiddenField ID="sdd" runat="server" />
        <asp:HiddenField ID="nowcolumns" runat="server" />
</asp:Content>

<%--
---------------- Change History -------------------------
none
--%>


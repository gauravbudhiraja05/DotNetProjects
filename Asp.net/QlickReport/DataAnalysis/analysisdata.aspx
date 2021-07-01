

<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Data Analysis
    Summary: Home Page of the Data Analysis
    Created on: 10/03/08
    Created By: Ranjit Singh

--%>
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 171px;
        }
    </style>
</asp:Content>
<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="analysisdata.aspx.vb" Inherits="analysisdata" title="Data Analysis"  Debug="true"%>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
 <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
  <script language="javascript" type="text/javascript">
// <!CDATA[
function opentosaveanalysis()
{
window.open("Saved_Analysis.aspx?","savedanalysis","height=400,width=500")
}
function formula_onclick() {

}
//Function ok_onclick() is use to open the formulas page
function ok_onclick() 
{
var st =document.getElementById("<%=hid.ClientID%>").value
if (st == "")

{
alert("First Select Columns");
}
else
{

    window.open("analysisformulasimple.aspx", "analysisfomula1", "height=400,width=750");

}
}
//Function ShowCalendar() is use to insert date in TextBox3 using calendar
//function ShowCalendar()
//		{
//		    // debugger;
//		     window.txtBox = document.getElementById('<%=TextBox3.ClientID%>');
//			 
//			 window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
//		  // getfrmdatevalue();
//			datecheck();		
//		}
//		//Function ShowCalendar1() is use to insert date in TextBox4 using calendar
//		function ShowCalendar1()
//		{
//		     //debugger;
//		     window.txtBox = document.getElementById('<%=TextBox4.ClientID%>');
//			 
//			 window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
////			 getfrmdatevalue();
//			datecheck1();		
//		}
//function datecheck()
//		{
//	
//		var txtBox = document.getElementById('<%=TextBox3.ClientID%>');
//		if (txtBox.value == "")
//        {
//        alert("Date should not be blank")
//		}
//		else
//		{
//	          AjaxSearchBind.datecheck(txtBox.value,valcum);
//	     }
//		
//		}
//		function valcum(response)
//		{
//		
//		if (response.value=="1")
//		{
//		alert("Insert Only Valid Date In mm/dd/yyyy Format");
//		var txtBox = document.getElementById('<%=TextBox3.ClientID%>')
//		txtBox.value=""
//		}
//		
//		}
//		function datecheck1()
//		{
//	
//		var txtBox = document.getElementById('<%=TextBox4.ClientID%>');
//		if (txtBox.value=="") {
//		    alert("Date should not be blank")
//		}
//		else
//		{
//	          AjaxSearchBind.datecheck(txtBox.value,valcum1);
//	          }
//		
//		}
//		function valcum1(response)
//		{
//		
//		if (response.value=="1")
//		{
//		alert("Insert Only Valid Date In mm/dd/yyyy Format");
//		var txtBox = document.getElementById('<%=TextBox4.ClientID%>')
//		txtBox.value=""
//		}
//		
//		}
</script> 
<table>
       <caption style=" width :191%; height: 16px;">Select Table Or Report</caption>
            <tr><td><asp:Label ID="rbt_lable" runat="server" Visible="true" Text="Select" CssClass="label"></asp:Label></td>
            <td class="style1">
                <asp:RadioButton ID="table_radio" Text="Tables" runat="server" Font-Bold="True" 
                     GroupName="Select_radio" AutoPostBack="True" />
                <asp:RadioButton ID="report_radio" Text="Report" runat="server" 
                    Font-Bold="True"  GroupName="Select_radio" AutoPostBack="True" />
                </td>
            </tr>
             <tr><td colspan="2"></td></tr>
            <tr runat="server" visible="false" id="Tr1">
                   <td> <%--<label class="label" title="Select Report" id="Label1" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label>--%> 
                       <asp:Label ID="Label1" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level1" scope="col" id="td1" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDownList1" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr><td colspan="2"></td></tr>
            <tr runat="server" visible="false" id="dept_row">
                   <td> <%--<label class="label" title="Select Report" id="Label1" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label>--%> 
                       <asp:Label ID="lbldept" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level1" scope="col" id="td2" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DepartmentName" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
               <tr runat="server" visible="false" id="client_row">
                  <td><%--<label class="label" title="Select Report" id="Label2" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level2</label>--%> 
                <asp:Label ID="lblclient" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level2" scope="col" id="td4" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ClientName" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr runat="server" id="date_row" visible="false">
            <td><asp:TextBox ID="Textbox4" runat="server" Visible="false"></asp:TextBox></td>
            
            <td class="style1"><asp:TextBox ID="Textbox3" runat="server" Visible="false"></asp:TextBox></td></tr>
            <tr runat="server" visible="false" id="lob_row">  
            <td><%--<label class="label" title="Select Report" id="Label3" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level3</label>--%> 
                <asp:Label ID="lbllob" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level3" scope="col" id="td6" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ddlLobName" runat="server" AutoPostBack="True">
                            </asp:DropDownList>
                </td>
           
            </tr>
             <tr>
            <td colspan="2" align="center"><asp:Button ID="Gettable" runat="server" Text="Get Table"  CssClass="button" 
                    Visible="False"/>
                    <asp:Button ID="GetReport" runat="server" Visible="false" Text="Get Report" CssClass="button" /></td>
            </tr>
            <tr>
                <td title="Select Report" scope="col" id="tdreportname"  style="color:Black;">
                    <asp:Label ID="select_l1" Visible="false" runat="server" CssClass="label"></asp:Label>
                </td>
                <td title="Select Report" scope="col" id="tdrepddl" runat="server" 
                    class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ddlTable" runat="server" Visible="false" AutoPostBack="True"> 
                             </asp:DropDownList>
                </td>
       
            </tr>
            <tr><td colspan="3" align="center">&nbsp;<asp:Button ID="getcols_table" runat="server" Text="Get Coloumn" 
                     CssClass="button" Visible="false" />&nbsp;<asp:Button ID="getcols_report" 
                     runat="server" Text="Get Coloumn" 
                     CssClass="button" Visible="false" />
                 <br />
                 &nbsp;<asp:Button ID="getcols_table_single" runat="server" Text="Get Coloumn" 
                     CssClass="button" Visible="false" />
                 &nbsp;<asp:Button ID="getcols_report_single" 
                     runat="server" Text="Get Coloumn" 
                     CssClass="button" Visible="false" />
                 </td>
                 </tr>
                 <tr><td> 
                  <asp:ListBox CssClass="listBox" ToolTip="Report Columns" ID="repcols" 
                     runat="server" SelectionMode="Multiple">
                      </asp:ListBox>
                        </td>
                      <td scope="col" id="tdaddcolumn" align="center" runat="server" class="style1">
                 <asp:Button  ToolTip="Add Columns"  ID="Button1" 
                     runat="server"  Text=">>" CssClass="button" /> 
                 <br />
                 <asp:Button  CssClass="button" ToolTip="Remove Columns" ID="Button2" runat="server" Text="<<" /> 
             </td>
                      <td title="Report Columns" scope="col" id="tdbtnshowrepcolumn" runat="server"> 
                          <asp:ListBox CssClass="listBox"  ID="selectedcols" ToolTip="Selected Columns"  
                     runat="server" style="position: static;" Height="154px" Width="149px">
                      </asp:ListBox>
                        </td>
                   </tr>
                   <tr>
                   <td colspan="3" align="center">
                   <input class="button" title="Click To Set Formula"  type="button" id="ok" value="Set Formula" onclick="return ok_onclick()" accesskey="f"/>&nbsp;<asp:Button CssClass="button" ToolTip="Click To Show Result" ID="Button3" runat="server"  Text="Show Result" AccessKey="r" />
                   </td>
                   </tr>
                     <tr runat="server" visible="false" id="dept_row1">
                   <td><%-- <label class="label" title="Select Report" id="Label4" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label>--%> 
                <asp:Label ID="lbldept1" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level1" scope="col" id="td8" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDowndept" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
                <tr runat="server" visible="false" id="client_row1">
                  <td><%--<label class="label" title="Select Report" id="Label5" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level2</label>--%> 
                <asp:Label ID="lblclient2" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level2" scope="col" id="td10" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDownclient" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr runat="server" visible="false" id="lob_row1">
                  <td><%--<label class="label" title="Select Report" id="Label6" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level3</label>--%> 
                <asp:Label ID="lbllob2" runat="server" Text="" CssClass="label"></asp:Label>
                </td>
                <td title="Select Level3" scope="col" id="td12" runat="server" class="style1">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDownlob" runat="server" AutoPostBack="True">
                             </asp:DropDownList>
                </td>
           
            </tr>
             <tr>
            <td><label for="ctl00_MainPlaceHolder_textreportname" class="label">HTML Report Name</label></td>
                 <td class="style1">
                 &nbsp;
                 <asp:TextBox CssClass="textBox" ToolTip="Fill Report Name" ID="textreportname" 
                     runat="server" Width="144px" MaxLength="20"></asp:TextBox></td>
             </tr>
           <tr><td colspan="2" align="center"><asp:Button csc="button" ID="SAVE_singleuser" runat="server" Text="Save Report" 
                    CssClass="button "/>&nbsp;<asp:Button csc="button" ID="SAVE_multiuser" 
                    runat="server" Text="Save Report" CssClass="button "/></td></tr>
</table>
                                    
                 
         
            
         
         
         <%--
---------------- Change History -------------------------
none
--%>
        
        <br />
        
    <label for="ctl00_MainPlaceHolder_TextBox1"></label>
         <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
            
            
            
            <table>
           
        
            </table>
            
            
            
            
  
   <%--
---------------- Change History -------------------------
none
--%><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%><%--
---------------- Change History -------------------------
none
--%>&nbsp;
         <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
    <div title="Save Report" runat="server" id="divsavereport" style="padding:20px;">
        <table summary="Save as HTML">
            <%--
---------------- Change History -------------------------
none
--%>
            
        <tr>
                <td></td>
            </tr>
            <tr><td></td></tr>
        
            
            <tr>
                <td title="Select Department" scope="col" id="tddepttname" style ="color:black">
                    <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
                 </td>
                <td  title="Select Department" scope="col"  id="tdddldept" style ="color:black" >
                     <%--
---------------- Change History -------------------------
none
--%>
                 </td>
              
            </tr>
            <tr>
                <td  title="Client" scope="col" id="tdClienttname" style ="color:black">
                     <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
                </td>
                <td title="Client" scope="col"  id="tdddlclient">
                       <%--
---------------- Change History -------------------------
none
--%>
                </td>
                
           </tr>
           <tr>
                <td title="Select LOB" scope="col" id="tdloobname" style ="color:black">
                    <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
                    </td>
                <td title="LOB" scope="col"  id="tdddllob">
                           <%--
---------------- Change History -------------------------
none
--%>
                 </td>
            </tr>
        
        
           <tr>
               <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
              <td>
                  <%--
---------------- Change History -------------------------
none
--%>
               </td>
           </tr>
           <tr id="gopaldidthis" runat="server" visible="false"><td style ="color:black"><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%></td>
           <td><%--
---------------- Change History -------------------------
none
--%></td></tr>
          <tr>
             <td style="height: 24px">
             </td>
             <td style="height: 24px">
                 <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
                </td>
        
          </tr>
          
     </table> 
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        <p>
        </p>
        
        </div>
         
         <%--
---------------- Change History -------------------------
none
--%>
        
    <table><tr><td>
    
    <div id="result" visible="false" runat="server" style="margin-left:20px">
   
   </div></td></tr><tr>
   <td>
 <div id="headdiv" visible="false"  style="margin-left:20px; width:700px;" runat="server">
 </div></td></tr></table>
 <table><tr><td>
 <div id="report" visible="false" style="width:700px; height:300px; overflow:scroll;"  runat="server"></div>
</td></tr></table></div><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%><%--
---------------- Change History -------------------------
none
--%><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%><%--
---------------- Change History -------------------------
none
--%><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%><%--
---------------- Change History -------------------------
none
--%><%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%><asp:HiddenField ID="hid" runat="server" />
     <asp:HiddenField ID="stCal" runat="server" />
     <asp:HiddenField ID="strdivreport" runat="server" />
     <asp:HiddenField ID="reportcolumnarray" runat="server" />
     <asp:HiddenField ID="colspanforgo" runat="server" />
          <asp:HiddenField ID="hidfun" runat="server" />
</asp:Content>
<%--
---------------- Change History -------------------------
none
--%>



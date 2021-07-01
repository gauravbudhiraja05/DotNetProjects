

<%--Project Name: IDMS Phase 2
    Module Name: Data Analysis
    Page Name: Data Analysis
    Summary: Home Page of the Data Analysis
    Created on: 10/03/08
    Created By: Ranjit Singh

--%>
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

window.open("analysisfomula.aspx","analysisfomula1","height=400,width=750");

}
}
//Function ShowCalendar() is use to insert date in TextBox3 using calendar
function ShowCalendar()
		{
		    // debugger;
		     window.txtBox = document.getElementById('<%=TextBox3.ClientID%>');
			 
			 window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
		  // getfrmdatevalue();
			datecheck();		
		}
		//Function ShowCalendar1() is use to insert date in TextBox4 using calendar
		function ShowCalendar1()
		{
		     //debugger;
		     window.txtBox = document.getElementById('<%=TextBox4.ClientID%>');
			 
			 window.txtBox.value = window.showModalDialog('../Calendar/Calendar.htm',window.txtBox.value,'dialogLeft:200px;dialogTop:200px;dialogHeight:210px;dialogWidth:265px;center:No;help:No;scroll:No;resizable:No;status:No;');
//			 getfrmdatevalue();
			datecheck1();		
		}
function datecheck()
		{
	
		var txtBox = document.getElementById('<%=TextBox3.ClientID%>');
		if (txtBox.value == "")
        {
        alert("Date should not be blank")
		}
		else
		{
	          AjaxSearchBind.datecheck(txtBox.value,valcum);
	     }
		
		}
		function valcum(response)
		{
		
		if (response.value=="1")
		{
		alert("Insert Only Valid Date In mm/dd/yyyy Format");
		var txtBox = document.getElementById('<%=TextBox3.ClientID%>')
		txtBox.value=""
		}
		
		}
		function datecheck1()
		{
	
		var txtBox = document.getElementById('<%=TextBox4.ClientID%>');
		if (txtBox.value=="") {
		    alert("Date should not be blank")
		}
		else
		{
	          AjaxSearchBind.datecheck(txtBox.value,valcum1);
	          }
		
		}
		function valcum1(response)
		{
		
		if (response.value=="1")
		{
		alert("Insert Only Valid Date In mm/dd/yyyy Format");
		var txtBox = document.getElementById('<%=TextBox4.ClientID%>')
		txtBox.value=""
		}
		
		}



		function ok_onclick() {
		    window.open("analysisfomula.aspx")
		}

		

		

  </script>
   
    <div style="padding:20px;"> 
        
         <table id="TABLE1" summary="Select Table" class="table">
       <caption class="caption" style ="background-color:#67A897" >Select Table</caption>
             <%--
---------------- Change History -------------------------
none
--%><%--</td></tr></table>
       <table><tr><td>--%><%--<div id="relax" style="margin-left:20px; width:700px; height:300px; overflow:scroll;"  runat="server">
    </div>--%><%--</td></tr></table>
    </td></tr>
        </table>--%>
            <tr>
                 <td  colspan='3' title="Select User" scope="col" id="tdusername"  style="color:Black;width: 171px">
                     &nbsp;</td>
                 <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
            </tr>
            <tr>
            <td>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; </td>
            <td>&nbsp;</td>
            </tr>
            <tr><td><table runat="server" class="table" id="spandisplay" visible="false">
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
            <td><asp:Button ID="Gettable" runat="server" Text="Get Table"  CssClass="button" 
                    Height="26px" Width="93px" Visible="False"/>&nbsp;</td>
            </tr>  
            <tr><td></td><td>&nbsp;</td></tr>       
             
            <tr>
                <td title="Select Report" scope="col" id="tdreportname"  style="color:Black;" 
                    class="style1">
                    <asp:Label ID="select_l1" runat="server" CssClass="label" Text="Select "></asp:Label>
                </td>
                <td title="Select Report" scope="col" id="tdrepddl" runat="server" 
                    class="style2">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="ddlTable" runat="server" AutoPostBack="True"> 
                             </asp:DropDownList>
                </td>
                <%--
---------------- Change History -------------------------
none
--%>
            </tr>
             <tr><td></td><td><asp:Button ID="getcols_table" runat="server" Text="Get Coloumn" 
                     CssClass="button" Width="88px" />&nbsp;</td></tr>
            <tr>
                <td  scope="col" id="tddate"  style="color:Black;height: 26px; width: 171px;">
                    <label class="label" title="Date From" id="lbldate" for="ctl00_MainPlaceHolder_TextBox3">
                    Date From
                    </label> 
                 </td>
                <td  scope="col" id="tddateto" valign="top" style="height: 26px" >
                <table summary="Select From Date Field Here">
                    <tr>
                        <td scope="col"><asp:TextBox CssClass="textBox" ToolTip="Date From" ID="TextBox3" runat="server"></asp:TextBox></td>
                        <td scope="col"><img alt="Select Date From" id="imgCalender2"  
                                src="../images/Calendar.gif" onclick="return ShowCalendar()" 
                                title="Select From Date"/></td>
                    </tr>
                  </table> 
                 </td>
                <td  scope="col" id="tddatefrom" runat="server" valign="top" rowspan="3"  style="color:Black;width: 369px">
                 <table summary="Select To Date Field Here"><tr>  <td><label title="Date To" class="label"  id="Lblfrom"  for="ctl00_MainPlaceHolder_TextBox4">
                           Date To
                             </label></td>
                             <td>
                        <asp:TextBox CssClass="textBox" ID="TextBox4" ToolTip="Date To" runat="server"></asp:TextBox>
                        </td>
                        <td><img alt="Select Date To" id="img1"  src="../images/Calendar.gif"onclick="return ShowCalendar1()" title="Select To Date"/></td>
                        </tr></table>
                         <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>&nbsp;
                       <asp:Calendar ToolTip="Select Date" ID="Calendar1" runat="server" Height="200px" Width="220px" BackColor="White" BorderColor="#3366CC" BorderWidth="1px" CellPadding="1" DayNameFormat="Shortest" Font-Names="Verdana" Font-Size="8pt" ForeColor="#003399">
                         <SelectedDayStyle BackColor="#009999" Font-Bold="True" ForeColor="#8080FF" />
                         <SelectorStyle BackColor="#99CCCC" ForeColor="#336666" />
                         <WeekendDayStyle BackColor="#CCCCFF" />
                         <TodayDayStyle BackColor="#99CCCC" ForeColor="White" />
                         <OtherMonthDayStyle ForeColor="#00C0C0" />
                         <NextPrevStyle Font-Size="8pt" ForeColor="White" />
                         <DayHeaderStyle BackColor="#99CCCC" ForeColor="#336666" Height="1px" />
                         <TitleStyle BackColor="Teal" BorderColor="White" BorderWidth="1px" Font-Bold="True" Font-Size="10pt" ForeColor="Navy" Height="25px" />
                      </asp:Calendar>  
                </td>
            </tr>
            <tr>
                <td scope="col" id="tdfree2" runat="server"  style="width: 171px; height: 27px;">
                </td>
                <td  title="Show Report" scope="col" id="tdbtnshowrep" runat="server" style="width: 224px" >
                    <%--
---------------- Change History -------------------------
none
--%>
                </td>
        
            </tr>
            <tr>
                <td scope="col" id="tdfree3" runat="server" style="width: 171px">
                </td>
                <td scope="rowgroup" id="tdtable" runat="server" style="width: 224px">
               
    <table style="width: 221px" summary="Select Report Columns Here">
        <tr>
             <td title="Report Columns" scope="col" id="tdbtnshowrepcolumn" runat="server" style="height: 173px"> 
                 <label for="ctl00_MainPlaceHolder_repcols"></label>
                  <asp:ListBox CssClass="listBox" ToolTip="Report Columns" ID="repcols" 
                     runat="server" SelectionMode="Multiple" Height="159px" Width="145px">
                      </asp:ListBox>
             </td>
             <td scope="col" id="tdaddcolumn" runat="server" style="width: 24px; height: 173px;">
                 <asp:Button CssClass="button" ToolTip="Add Columns"  ID="Button1" runat="server"  Text=">>" Width="24px" /> 
                 <asp:Button CssClass="button" ToolTip="Remove Columns" ID="Button2" runat="server" Text="<<" Width="24px" /> 
             </td>

   
            <td title="Selected Columns" scope="col" id="tdbselectedcol" runat="server" style="height: 173px; width: 118px;">
                 <label for="ctl00_MainPlaceHolder_selectedcols"></label>
                 <asp:ListBox CssClass="listBox"  ID="selectedcols" ToolTip="Selected Columns"  
                     runat="server" style="position: static;" Height="154px" Width="149px">
                      </asp:ListBox>
             </td>
         </tr>
     </table>
               </td>
           </tr>
           <tr>
                <td scope="rowgroup" id="tdtablerange" runat="server"  colspan="2" style="width: 312px; height: 16px;">
      <table summary="Buttons">
           <tr>
               <td style="width: 123px">
                   &nbsp;
                   <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%></td>
               <td >
                   <input class="button" title="Click To Set Formula"  type="button" id="ok" value="Set Formula" onclick="return ok_onclick()" accesskey="f"   />&nbsp;
               </td>                 
                 <td>
                     <asp:Button CssClass="button" ToolTip="Click To Show Result" ID="Button3" runat="server"  Text="Show Result" AccessKey="r" /></td>
                
         
           </tr>
        </table> 
               
                  </td>
                    <td scope="col" style="height: 16px; width: 369px;">
      <table summary="Buttons">
              <tr>
                  <td scope="col" style="width: 123px">
                    
                 </td>
                 <td scope="col" style="width: 123px">
                     <%--
---------------- Change History -------------------------
none
--%>
                 </td>
                 <td scope="col" style="width: 123px">
                     <%--<table><tr><td>
       <div id ="formula"  runat="server" style="margin-left:150px; padding:20px; width:800px; overflow:scroll;">
   </div></td></tr></table>
 --%>
                 </td>
             </tr>
       </table>
                   </td>                 
            </tr>  
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
            <tr>
            <td>
            <table runat="server" class="table" id="spandisplay1" visible="false">
             <tr>
                <td title="Select level1" scope="col" id="td7"  style="color:Black;width: 171px"></td>
                   <td> <label class="label" title="Select Report" id="Label4" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level1</label> 
                </td>
                <td title="Select Level1" scope="col" id="td8" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDowndept" runat="server" AutoPostBack="True">
                        <asp:ListItem>--- Select ---</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr>
                <td title="Select level1" scope="col" id="td9"  style="color:Black;width: 171px"></td>
                  <td><label class="label" title="Select Report" id="Label5" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level2</label> 
                </td>
                <td title="Select Level2" scope="col" id="td10" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDownclient" runat="server" AutoPostBack="True">
                        <asp:ListItem>-- Select--</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            <tr>
                <td title="Select level3" scope="col" id="td11"  style="color:Black;width: 171px"></td>
                  <td><label class="label" title="Select Report" id="Label6" runat="server" for="ctl00_MainPlaceHolder_ddlReport">Select Level3</label> 
                </td>
                <td title="Select Level3" scope="col" id="td12" runat="server">
                    &nbsp;<asp:DropDownList ToolTip="Select Report" CssClass="dropdownlist"
                        ID="DropDownlob" runat="server" AutoPostBack="True">
                        <asp:ListItem>-- Select --</asp:ListItem>
                             </asp:DropDownList>
                </td>
           
            </tr>
            </table>
            </td>
            </tr>
             <tr>
            <td><label for="ctl00_MainPlaceHolder_textreportname">HTML Report Name</label></td><td>
                 <asp:TextBox CssClass="textBox" ToolTip="Fill Report Name" ID="textreportname" 
                     runat="server" Width="144px" MaxLength="20" Enabled="False"></asp:TextBox></td>
             </tr>
            <tr>
            <td>
                &nbsp;</td>
            </tr>
            <tr><td><asp:Button csc="button" ID="SAVE_singleuser" runat="server" Text="SAVE" 
                    CssClass="button "/>&nbsp;<asp:Button csc="button" ID="SAVE_multiuser" 
                    runat="server" Text="SAVE" CssClass="button "/></td></tr>
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
<asp:Content ID="Content3" runat="server" contentplaceholderid="head">
    <style type="text/css">
        .style1
        {
            width: 171px;
            height: 23px;
        }
        .style2
        {
            height: 23px;
        }
    </style>
</asp:Content>


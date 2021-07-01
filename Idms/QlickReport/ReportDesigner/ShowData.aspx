<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: ShowData
    Summary: Output page of the advance report designer
    Created on: 12/05/08
    Created By: Usha Sehokand

--%>


<%@ Page Language="VB" AutoEventWireup="false" EnableEventValidation="false" EnableViewStateMac="false" ViewStateEncryptionMode="Never" CodeFile="ShowData.aspx.vb" Inherits="ReportDesigner_nShowData" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head id="Head1" runat="server">
    <title>Show Data</title>
    <link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet" />
    <script type="text/javascript" src="../js/sortable.js"></script>
    <script type="text/javascript" language="Javascript">
      
      function assignToparent()
    {
        document.forms[0].target="_parent";
        document.forms[0].action="../Graphicalpresentation/graphdata.aspx?currentreport="+document.getElementById("hidReportname").value;
        document.forms[0].submit();
    }
      // to export the report to Excel file
        function xls()
		{ 
		    try
		    {
		        var emdata =("<%=Path%>");			
			    window.open(emdata);
		    }
		    catch(e)
		    {
		        document.getElementById("lblCaption").value="This Application Is Not Supported By Your system.";
		    }
			
			
		}
		
    function imgMail_onclick()
    {
           //var emdata = document.getElementById("divDetails").innerHTML;
		   //document.getElementById("hidData").value = emdata;
           window.open("SendMail.aspx","SendMail","width=600px,height=300px");    
    }

    </script>
</head>
<body >

    <form id="frmShowdata" title="Report" runat="server">
       
        <div title="Header" id="divHeader" style="width:750px;height:0px; font-family:Verdana; font-size:8pt" runat="server">            
        </div>
         <div id="divDetails"  title="Details Pane" runat="server" style="width:750px;">
        </div>             

        <asp:Label ID="lblCheck" runat="server"></asp:Label>
        <div id="divFooter" title="Footer" style="width:750px;height:0px; font-family:Verdana; font-size:8pt" runat="server">
        </div>
        <div id="divAddedvalue">
        <asp:Label runat="server" CssClass="label" ID="lblCaption" style ="color:Black"  Height="21px" Width="325px"></asp:Label>
            <table id="tblAddedvalue"  style="width: 328px">
                <caption>
                    &nbsp;</caption>
                <tr>
                    <td style="width: 107px" scope ="col">
                    </td>
                    <td scope="col" title="">
                                       <img id="imgPrint" style="height: 26px" src="../images/printer.jpg" alt="Print this report" onclick="javascript:window.print();" />     
                                       <%--<img  id="imgMail" style="height: 23px;" src="../images/mail.jpg" alt="Mail this report" onclick="return imgMail_onclick()"/>    --%>
                                       <asp:ImageButton id="imgXls" runat="server" AlternateText ="Export to excel" Height="23px" ImageUrl="~/images/excel_32.gif" ToolTip="Export to excel" Width="35px" />
                                       <asp:imagebutton ID="imgChart" AlternateText ="View graph"  runat="server" Height="29px" ImageUrl="~/images/data_32.gif" Width="27px" ToolTip="View graphs"/>
                                       <asp:ImageButton id="imgAnalysis" runat="server" AlternateText ="Analysis on Temp Table" Visible="false" Height="23px" 
                                           ImageUrl="~/images/DA.png" ToolTip="Analysis on Temp Table" Width="35px" /> 
                                          
                    </td>
                </tr>
                <%--<tr>
                    <td title="Department" colspan="cols" style="width: 107px; color : Black " scope ="col">
                        <label class="label" title="Department" >Department</label>    
                    </td>
                    <td title="Select Department" colspan="cols" scope ="col">


                   <label class="label" for="ddlDepartment" ></label>   

 
                        <asp:dropdownlist runat="server" ID="ddlDepartment" ToolTip="Select Department" CssClass="dropdownlist" Width="187px" AutoPostBack="True"></asp:dropdownlist>
                    </td>
                </tr>
                  <tr>
                    <td title="Client" colspan="cols" style="width: 107px; color : Black " scope ="col">
                        <label class="label" title="Client" for="ddlClient">Client</label>    
                    </td>
                    <td title="Select Department" colspan="cols" scope ="col">
                        <asp:dropdownlist runat="server" ID="ddlClient" ToolTip="Select Client" CssClass="dropdownlist" Width="187px" AutoPostBack="True"></asp:dropdownlist>
                    </td>
                </tr>
                  <tr>
                    <td title="Lob" colspan="cols" style="width: 107px; color :Black " scope ="col">
                        <label class="label" title="LOB" for="ddlLob">LOB</label>    
                    </td>
                    <td title="Select LOB" colspan="cols" scope ="col">
                        <asp:dropdownlist runat="server" ID="ddlLob" ToolTip="Select LOB" CssClass="dropdownlist" Width="187px"></asp:dropdownlist>
                    </td>
                </tr>--%>
                <tr>
                    <td title="Report Name" colspan="cols" style="width: 107px; color : Black " scope ="col">
                        <label class="label" title="Report Name" for="txtHTMLreport">Report Name</label>    
                    </td>
                    <td title="Select LOB" colspan="cols" scope ="col">
                        <asp:TextBox runat="server" ID="txtHTMLreport" CssClass="textBox" Width="179px"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="2" align="center" title="Save as HTML report" scope ="col">
                        <asp:Button ID="btnSavehtml" runat="server" CssClass="button" Text="Save as HTML Report" Width="175px" />
                    </td>
                </tr>
            </table>        
           <asp:Panel ID="divmsgboxdeltable" BorderColor="Gray" BorderStyle="Groove" BorderWidth="1px"
        BackColor="#eeeeee" Style="z-index: 102; left: 300px;   position: absolute; top: 250px;
        text-decoration: none" runat="server" visibility="false">
        <table id="tabmsgboxdeltable" width="250">
            <tr>
                <th class="tableHeader" style="width: 95%">
                    <strong>Message Box</strong></th>
                
            </tr>
            <tr>
                <td align="left" colspan="2" style="height: 14px; text-align: center">
                    <strong>Do you want to Analysis on this Table?</strong></td>
            </tr>
            <tr>
                <td align="center" colspan="2" style="height: 24px">
                    <asp:Button ID="cmdyesdt" runat="server" Text="Yes" CssClass="button"></asp:Button>&nbsp;
                    <asp:Button ID="cmdnodt" runat="server" Text="No" CssClass="button"></asp:Button>
                </td>
            </tr>
        </table>
    </asp:Panel>
            <asp:Label ID="lblMsg" runat="server" CssClass="label" ForeColor="Red" Height="179px"
                Style="left: 338px; position: relative; top: -179px" Width="356px"></asp:Label></div>
        <input type="hidden" runat="server" name="hidRepname" id="hidRepname" /><%-- Hidden Report name --%>
        <input type="hidden" runat="server" name="hidHeader" id="hidHeader" /><%-- Hidden Header elements --%>
        <input type="hidden" runat="server" name="hidHpos" id="hidHpos" /><%-- Hidden Header elements position--%>
        <input type="hidden" runat="server" name="hidDpos" id="hidDpos" /><%-- Hidden Details elements position--%>
        <input type="hidden" runat="server" name="hidColumns" id="hidColumns" /><%-- Hidden Details columns--%>
        <input type="hidden" runat="server" name="hidTables" id="hidTables" /><%-- Hidden Tables names--%>
        <input type="hidden" runat="server" name="hidFpos" id="hidFpos" /><%-- Hidden Footer elements position--%>
        <input type="hidden" runat="server" name="hidHformat" id="hidHformat" /><%-- Hidden Header element format --%>
        <input type="hidden" runat="server" name="hidHformula" id="hidHformula" /><%-- Hidden Headerelements formula --%>
        <input type="hidden" runat="server" name="hidDformula" id="hidDformula" /><%-- Hidden detailselements formula --%>
        <input type="hidden" runat="server" name="hidDformat" id="hidDformat" /><%-- Hidden detailselements format --%>
        <input type="hidden" runat="server" name="hidColorcondition" id="hidColorcondition" /><%-- Hidden details color condition --%>
        <input type="hidden" runat="server" name="hidFformula" id="hidFformula" /><%-- Hidden footerelements formula --%>
        <input type="hidden" runat="server" name="hidFcolorcon" id="hidFcolorcon" /><%-- Hidden footer color condition --%>
        <input type="hidden" runat="server" name="hidHcolorcon" id="hidHcolorcon" /><%-- Hidden Header color condition --%>
        <input type="hidden" runat="server" name="hidStart" id="hidStart" /><%-- Hidden start date --%>
        <input type="hidden" runat="server" name="hidEnd" id="hidEnd" /><%-- Hidden end date --%>
        <input type="hidden" runat="server" name="hidReportname" id="hidReportname" /><%-- Hidden report name --%>
        <input type="hidden" runat="server" name="hidReporttype" id="hidReporttype" /><%-- Hidden report type --%>
        <input type="hidden" runat="server" name="hidReportscope" id="hidReportscope" /><%-- Hidden report scope --%>
        <input type="hidden" runat="server" name="hidDepartment" id="hidDepartment" /><%-- Hidden Department --%>
        <input type="hidden" runat="server" name="hidClient" id="hidClient" /><%-- Hidden client --%>
        <input type="hidden" runat="server" name="hidLob" id="hidLob" /><%-- Hidden report type --%>
        <input type="hidden" name="hidData" id="hidData" /><%-- stores details division data --%>
        <input type="hidden" runat="server" name="hidDatetable" id="hidDatetable" /><%-- stores Date table --%>
        <br />
    </form>
   <%-- <script type="text/javascript" language="javascript">
        
        if('<%=Session("typeofuser")%>'=="Super Admin")
            {
                document.getElementById("btnSavehtml").disabled=true;
            }
      </script>--%>
   
</body>
</html>

<%--Change History:None
--%>

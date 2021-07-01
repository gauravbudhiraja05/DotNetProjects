<%--Project Name: IDMS Phase 2
    Module Name: Advance Report Designer
    Page Name: OpenReport
    Summary: This page is used to open an existing report. It is a component of the report designer module.
    Created on: 10/03/08
    Created By: Usha Sehokand

--%>


<%@ Page Language="VB" AutoEventWireup="false" ValidateRequest="false" CodeFile="OpenReport.aspx.vb" Inherits="ReportDesigner_OpenReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en">
<head runat="server">
    <title>OpenReport</title>
    <meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1"/>
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1"/>
		<meta name="vs_defaultClientScript" content="JavaScript"/>
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5"/>
		<link href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet"/>
<script language="javascript" type="text/javascript">
<!--

// This function closes the current window
    function btnClose_onclick() 
    {
           window.parent.focus();
           window.close();
    }
// btnClose_close function ends

// to confirm deletion of a report
    function confirm() {

            if (document.getElementById("ddlReport").value == "--Select--" || document.getElementById("ddlReport").value == "0") {
                document.getElementById("lblMsg").innerText = "Please select report";
            }
            else {
                //document.getElementById("ddlClient").style.visibility = 'hidden';
                //document.getElementById("ddlLob").style.visibility = 'hidden';
                //document.getElementById("ddlReport").style.visibility='hidden';
                document.getElementById("divConfirm").style.visibility = 'visible';
            }
        }

//

 //this function assign report values fetched frm the database to the parent window in order to open the report.
    function assignToparent()
    {

         // assign report name to the parent window
         
        window.opener.document.getElementById("hidRecordid").value="<%=reportId%>";
        window.opener.document.getElementById("hidReportname").value="<%=repName%>";
        window.opener.document.getElementById("hidReportscope").value="<%=hidReportscope%>";
        window.opener.document.getElementById("hidReporttype").value="<%=repType%>";
        window.opener.document.getElementById("hidDepartment").value="<%=dept%>";
        window.opener.document.getElementById("hidClient").value="<%=client%>";
        window.opener.document.getElementById("hidLob").value="<%=lob%>";
        
       //  assign details pane value
        window.opener.document.getElementById("hidDpos").value="<%=hidDpos%>";
        window.opener.document.getElementById("hidTables").value="<%=hidTables%>";
        window.opener.document.getElementById("hidWhere").value="<%=hidWhere%>";
        window.opener.document.getElementById("hidGroupby").value="<%=hidGroupby%>";
        window.opener.document.getElementById("hidOrderby").value="<%=hidOrderby%>";
        window.opener.document.getElementById("hidHaving").value="<%=hidHaving%>";
        window.opener.document.getElementById("hidColorcondition").value="<%=hidColorcondition%>";
        window.opener.document.getElementById("hidDformula").value="<%=hidDFormula%>";
        window.opener.document.getElementById("hidDetailsformat").value="<%=hidDetailsformat%>";
        window.opener.document.getElementById("hidReporttype").value="<%=hidReporttype%>";
        window.opener.document.getElementById("hidReportscope").value="<%=hidReportscope%>";
        window.opener.document.getElementById("hidDatetable").value="<%=hidDatetable%>";
        window.opener.document.getElementById("hidDformat").value="<%=hidDformat%>";
        
        // assign header values
        
        window.opener.document.getElementById("hidHeight").value="<%=hidHeight%>";        // assign combined height of header & footer
        window.opener.document.getElementById("hidHeaderformat").value="<%=hidHeaderformat%>";
        window.opener.document.getElementById("hidHpos").value="<%=hidHpos%>";
        window.opener.document.getElementById("hidHformat").value="<%=hidHformat%>";
        window.opener.document.getElementById("hidHformula").value="<%=hidHformula%>";
        window.opener.document.getElementById("hidHcolorcon").value="<%=hidHcolorcon%>";
        
        // assign footer values
        
        window.opener.document.getElementById("hidFooterformat").value="<%=hidFooterformat%>";
        window.opener.document.getElementById("hidFpos").value="<%=hidFpos%>";
        window.opener.document.getElementById("hidFformat").value="<%=hidfformat%>";
        window.opener.document.getElementById("hidFformula").value="<%=hidFformula%>";
        window.opener.document.getElementById("hidFcolorcon").value = "<%=hidfcolorcon%>";
     
        
        window.opener.document.getElementById("hidReportmode").value="Edit";  // assign report openening mode
        
        // call parent window function to open report at design time
        window.opener.openReportdesign();
        window.parent.focus();
        window.close();
    }
    
    // assignToparent ends///////
    
    // if user confirms the deletion of the report
    function btnYes_onclick() {

        document.getElementById("divConfirm").style.visibility = "hidden";
        __doPostBack(document.getElementById('<%=Button1.ClientID%>').name,'');
    }
/// if user denied the deletion of the report
    function btnNo_onclick() {

        document.getElementById("divConfirm").style.visibility="hidden";
    }

// -->

// check if the user is super admin??
    function load()
    {
        if('<%=Session("typeofuser")%>'=="Super Admin")
        {
            document.getElementById("btnDelete").disabled=true;
        }
        document.getElementById("btnEx").value=window.opener.document.getElementById("btnEx").value;
    }
function btnDelete_onclick() {

}

</script>
</head>
<body onload="return load();">
    <form id="formOpenreport" runat="server">
        <div>
            <table style="width: 486px">
                   <caption style ="background-color:#0591D3">Open Report</caption>	
                   <tr>
                        <td>
                            <table id="spandisplay" runat="server" visible="false" >
                                 <tr>
		                <td style="width: 80px; height: 25px; color : Black "  scope ="col" >
                            <asp:Label ID="lbl1" Text="Select Level 1" CssClass="label" runat="server" ></asp:Label>                     
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlDepartment" AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>
		            <tr>
		                <td style="width: 80px; height: 25px; color : Black " scope ="col">
                            <asp:Label ID="lbl2" Text="Select Level 2" CssClass="label" runat="server" ></asp:Label>		                     
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlClient" AutoPostBack="True" ></asp:DropDownList>
                        </td>
		            </tr>
		             <tr>
		                <td style="width: 80px; height: 25px; color : Black " scope ="col">
		                   <asp:Label ID="lbl3" Text="Select Level 3" CssClass="label" runat="server" ></asp:Label>
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlLob" AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>
                            </table>
                        </td>
                   </tr>	           
		           
		            <tr>
		                <td style="width: 80px; height: 25px; color : Black " scope ="col">
                            <label title="Report" class="label" for="ddlReport">Report:</label>
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlReport"></asp:DropDownList>
                            <asp:Label ID="lblMsg" runat="server" ForeColor="Red" Height="106px" Style="z-index: 100;
                                left: 320px; position: absolute; top: 45px" Width="171px" Font-Bold="True" Font-Size="8pt"></asp:Label>
		                </td>
		            </tr>	      
		            <tr>
		                <td colspan="2" style="height: 25px" scope ="colgroup"   >
                            <asp:Button runat="server" ID="btnDonemul" Text="Open" Visible="false" ToolTip="Click to open this report" cssclass="button" />
                            <asp:Button runat="server" ID="btnDone" Text="Open" Visible="false"  ToolTip="Click to open this report" cssclass="button" />
                            <input type="button" title="Click to delete this report" onclick="return confirm();" id="btnDelete" value="Delete" class="button" onclick="return btnDelete_onclick()" />
                            <asp:Button runat="server" ToolTip="Click to delete this report" ID="Button1"  style="z-index: 100; left: 2px; position: absolute; top: 558px" />
                            <input type="button" id="btnClose" title="Close this window" value="Close" class="button" language="javascript"  onclick="return btnClose_onclick()" />
		                </td>
		            </tr>

            </table>
            &nbsp;
           <div id="divConfirm"  style="position:absolute; left: 17px; top: 183px; visibility:hidden; width: 305px;">
                <table id="tblConfirm" style="width: 299px; margin-left: 2px; margin-right: 2px;">
                    <caption style ="background-color:#0591D3">Confirm Deletion</caption>
                    <tr style="color: #000000">
                        <td scope="col" title="" style="text-align: justify; color : Black ">
                            <label class="label" for="btnYes">
                                The report you are going to delete may
                                contain graphs, analysed data or alerts. Are you sure you want to delete it?<br />
                            </label>
                         </td>
                    </tr>
                    <tr style="color: #000000">
                        <td align="center" scope ="col" >
                                <input type="button" id="btnYes" class="button" title="Yes" value="Yes" language="javascript" onclick="return btnYes_onclick()" />
                                <input type="button" id="btnNo" class="button" title="No" value="No" language="javascript" onclick="return btnNo_onclick()" />
                        </td>
                    </tr>
                </table>
           </div>
        </div>
   
        <input type="hidden" id="btnEx" runat="server" />
    </form>
</body>
</html>
<%--
Change History: none
--%>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="OpenGraph.aspx.vb" Inherits="OpenGraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Open Graph</title>
    <%--<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<LINK href="../App_Themes/Themes/StyleSheet.css" type="text/css" rel="stylesheet">--%>
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
 //this function assign report values fetched frm the database to the parent window in order to open the report.
    function assignToparent()
    {
         // assign report name to the parent window
        window.opener.document.getElementById("Report").value="<%=repName%>";    
        window.opener.document.getElementById("Department").value="<%=dept%>";
       window.opener.document.getElementById("Client").value="<%=client%>";
       window.opener.document.getElementById("lob").value="<%=lob%>";
//        
       window.opener.document.getElementById("Graph").value="<%=hidgraphtype%>"; 
       window.opener.document.getElementById("ColumnName").value="<%=hidcolumnname%>";
        window.opener.document.getElementById("ColumnSeries").value="<%=hidcolumnseries%>";
        window.opener.document.getElementById("ToDate").value="<%=hidtodate%>";
        window.opener.document.getElementById("FromDate").value="<%=hidfromdate%>";
        window.opener.document.getElementById("abc").value="<%=hidcommanformat%>";
//        window.opener.document.getElementById("hidspecificproperties").value="<%=hidspecificproperties%>";
       window.opener.document.getElementById("totalcolumn").value="<%=hidtotalcolumn%>";
        
        
      
        
       
     
        
        //window.opener.document.getElementById("hidReportmode").value="Edit";  // assign report openening mode
        
        // call parent window function to open report at design time
        window.opener.opengraphdesign();
        window.parent.focus();
        window.close();
    }
    
    // -->
</script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <table>
                        <tr>
             <td scope="col" title="Department" style="width: 106px" >
                <label for="ddlDepartmant" class="label">
                    Department</label>
             </td>
             <td scope="col" title=" Select Department" >
                    <asp:DropDownList ID="ddlDepartment" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                    </asp:DropDownList>
                </td>
         </tr>
                <tr>
             <td scope="col" title="Client" style="width: 106px"  >
                <label for="ddlClient" class="label">
                    Client</label>
             </td>
             <td scope="col" title="Select Client" >
                <asp:DropDownList ID="ddlClient" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>
                <tr>
            <td scope="col" title="LOB" style="width: 106px"  >
                <label for="ddlLob" class="label">
                    LOB</label>
            </td>
            <td scope="col" title="Select Lob"  >
                <asp:DropDownList ID="ddlLob" runat="server" AutoPostBack="True" CssClass="dropdownlist">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td scope="col" title=" Report Name"  align="left" style="width: 106px"  >
              <label for="rbnAnalysis" class="label">View Graph</label>   
            </td>
            <td align="left" colspan="1" scope="col"  title=" Report Name">
            <asp:RadioButton  id="rbnAnalysis" runat="server" Text="Analysis" Width="80px" GroupName="Report" AutoPostBack="True"></asp:RadioButton><asp:RadioButton  id="rbnReport" runat="server" Text="Report" Width="72px" GroupName="Report" AutoPostBack="True"></asp:RadioButton>
            </td>
        </tr>
                <tr>
            <td scope="col" title="Current Report" style="width: 106px"  >
                Report Name</td>
            <td scope="col" title="Current Report Name"  >
                <asp:DropDownList ID="ddlReport" runat="server" CssClass="dropdownlist" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
                        <tr>
                            <td id="labelAnalysis" runat="server" scope="col"  title="Analysis Report table Name" style="width: 106px">
                                <label  for="ddlAnalysistable" class="label" >Analysis Table</label>
                            </td>
                            <td scope="col"  title="Analysis Report table Name ">
                            <asp:DropDownList ID="ddlAnalysistable" runat="server" CssClass="dropdownlist">
                                </asp:DropDownList>
                            </td>
                        </tr>
        <tr>
            <td runat="server" scope="col" style="width: 106px" title="Analysis Report table Name">
                <asp:Button ID="btnopen" runat="server" Text="Open"/>
            </td>
            <td scope="col" title="Analysis Report table Name ">
            </td>
        </tr>
      </table>
    </div>
    <input type="hidden" name="pReport" id="pReport" />
    </form>
</body>
</html>

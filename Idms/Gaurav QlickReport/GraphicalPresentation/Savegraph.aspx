<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Savegraph.aspx.vb" Inherits="Graphical_Presentation_Savegraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Save Graph</title>
    <link rel="Stylesheet" href="../App_Themes/Themes/StyleSheet.css" type="text/css" />
    <script language="Javascript">
//   function Close()
//   {
//   window.close();
//   }
        function onLoad()
        {
            document.getElementById("ll").value=window.opener.document.getElementById("abc").value;
            document.getElementById("ll1").value=window.opener.document.getElementById("abc1").value;
            document.getElementById("ll2").value=window.opener.document.getElementById("abc2").value;
             document.getElementById("leg").value=window.opener.document.getElementById("Leg1").value;
            document.getElementById("spec").value=window.opener.document.getElementById("prp").value;//get hidden Tables
            document.getElementById("txtReportname").value=window.opener.document.getElementById("Report").value;
            document.getElementById("hiddepartment").value=window.opener.document.getElementById("department").value;
            document.getElementById("hidclient").value=window.opener.document.getElementById("client").value;
            document.getElementById("hidlob").value=window.opener.document.getElementById("lob").value;
            document.getElementById("txtGraphtype").value=window.opener.document.getElementById("Graph").value;
            document.getElementById("DateTo").value=window.opener.document.getElementById("ToDate").value;
            document.getElementById("DateFrom").value=window.opener.document.getElementById("FromDate").value;
            document.getElementById("SelectedColumn").value=window.opener.document.getElementById("ColumnName").value;
            document.getElementById("Columnseries").value=window.opener.document.getElementById("ColumnSeries").value;
            document.getElementById("Save").value=window.opener.document.getElementById("SavedBy").value;
            document.getElementById("totalcolumn").value=window.opener.document.getElementById("totalcolumn").value;
            document.getElementById("savereport").value=window.opener.document.getElementById("Report").value;
            document.getElementById("savegraphtype").value=window.opener.document.getElementById("Graph").value;
            document.getElementById("savecolumnseries").value=window.opener.document.getElementById("ColumnSeries").value;
            document.getElementById("Repgraph").value=window.opener.document.getElementById("ReportType").value;
    	             
        }
//        function loadGraphname()
//        {
//           
//            window.opener.document.getElementById("ctl00_MainPlaceHolder_lblgraph").value="Saved Graph Name: "&'<%=Session("Savegp")%>';
//            alert("Graph Saved Successfully.");
//        }
    </script>
    </head>
<body onload="return onLoad();">
    <form id="form1" runat="server">
   <table style="width: 394px">
		           <caption>Save Chart</caption>
		           <tr>
		                <td style="width: 91px" scope="col" title="">
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                 <asp:HiddenField ID="hiddepartment" runat="server" />
		                 </td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                <asp:HiddenField ID="hidclient" runat="server" />
		               </td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                <asp:HiddenField ID="hidlob" runat="server" />
		                </td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                    Report Name&nbsp;</td>
		                <td style="width: 270px" scope="col" title="Report name" valign="top" align="left">
                            <asp:TextBox ID="txtReportname" runat="server" CssClass="textbox" 
                                ReadOnly="true" ToolTip="Report Name" Width="136px" Enabled="False"></asp:TextBox>
                        </td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                    <label class="label" title="Graph Name" for="txtGraphname1">
                                Graph Name:</label>
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                    <asp:TextBox runat="server" ToolTip="Enter Graph Name" ID="txtGraphname" CssClass="textbox" Width="136px"></asp:TextBox></td>
		            </tr>
		            <tr>
		                <td style="width: 91px" scope="col" title="">
		                    <label class="label" title="Graph Type" for="txtGraphtype1">
                                Graph Type</label>
		                </td>
		                <td style="width: 270px" scope="col" title="" align="left">
		                    <asp:TextBox runat="server" ToolTip="Graph type" ID="txtGraphtype" 
                                ReadOnly="true" CssClass="textbox" Width="136px" Enabled="False"></asp:TextBox></td>
		            </tr>
		            
		            <tr>
		                <td colspan="2" scope="colgroup" title="" align="left">
		                    <asp:Button runat="server" ID="btnSavenew" Text="Save " cssclass="button" />
		                    <%--<input type="button" id="btnClose" value="Close" class="button" onclick="return Close();" />--%>
                            
		                </td>
		            </tr>
		            <tr>
		                <td colspan="2" scope="colgroup" title="" align="left">
		                    <asp:Label ID="savemesssage" runat="server"></asp:Label>                            
		                </td>
		            </tr>
		    </table>
		    <input type="hidden" name="ll1" runat="server"  id="ll" />
		     <input type="hidden" name="leg" runat="server"  id="leg" />
		    <input type="hidden" name="ll11" runat="server"  id="ll1" />
		     <input type="hidden" name="ll21" runat="server"  id="ll2" />
		    <input type="hidden" name="DateTo1" runat="server" id="DateTo" />
		    <input type="hidden" name="savereport1" runat="server" id="savereport" />
		    <input type="hidden" name="savegraphtype1" runat="server" id="savegraphtype" />
		    <input type="hidden" name="savecolumnseries1" runat="server" id="savecolumnseries" />
		    <input type="hidden" name="DateFrom1" runat="server" id="DateFrom" />
		    <input type="hidden" name="SelectedColumn1" runat="server" id="SelectedColumn" />
		    <input type="hidden" name="Columnseries1" runat="server" id="Columnseries" />
		    <input type="hidden" name="GraphCreated1" runat="server" id="GraphCreated" />
		    <input type="hidden" name="Save1" runat="server" id="Save" />
		    <input type="hidden" name="totalcolumn1" runat="server" id="totalcolumn" />
		    <input type="hidden" name="spec1" runat="server" id="spec" />
		    <input type="hidden" name="Repgraph" runat="server" id="Repgraph" />
		     <input type="hidden" name="Savegraph" runat="server" id="Savegraph" />
		    		    
    </form>
    
</body>

</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="ArchiveReport.aspx.vb" Inherits="ReportDesigner_ArchiveReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
    <script type="text/javascript" language="javascript">
        function btnClose_onclick() {
            window.parent.focus();
            window.self.close();
        }
    </script>
</head>
<body>
    <form id="frmArchivereport" runat="server">
    <div>
        <table style="width: 486px">
                   <caption style ="background-color:#67A897">Archive Report</caption>	
                   <tr>
                        <td>
                            <table id="spandisplay" runat="server" visible="false">
                                <tr>
		                            <td style="width: 80px; height: 25px; color : Black " scope ="col">
                                    <asp:Label ID="lbl1" runat="server" CssClass="label" Text="Select Level 1" ></asp:Label> 
		                             </td>
		                        <td style="width: 200px; height: 25px;" scope ="col">
		                         <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlDepartment" 
                                        AutoPostBack="True"></asp:DropDownList>
		                        </td>
		                         </tr>
                   
                   <tr>
		                <td style="width: 80px; height: 25px; color : Black " scope ="col">
                             <asp:Label ID="lbl2" runat="server" CssClass="label"  Text="Select Level 2" ></asp:Label> 
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlclient" 
                                AutoPostBack="True"></asp:DropDownList>
		                </td>
		            </tr>

                    <tr>
		                <td style="width: 80px; height: 25px; color : Black " scope ="col">
                            <asp:Label ID="lbl3" runat="server" CssClass="label"  Text="Select Level 3" ></asp:Label> 
		                </td>
		                <td style="width: 200px; height: 25px;" scope ="col">
		                    <asp:DropDownList runat="server" CssClass="dropdownlist" ID="ddlLob" 
                                AutoPostBack="True"></asp:DropDownList>
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
		                <td colspan="2" scope="colgroup" style="height: 25px"   >
                            <asp:Button runat="server" Visible="false"   ID="btnArchivemul" Text="Archive"  ToolTip="Click to open this report" cssclass="button" />
                            <asp:Button runat="server" ID="btnArchive" Visible="false"   Text="Archive"  ToolTip="Click to open this report for Single User" cssclass="button"/>
                            <input type="button" id="btnClose" title="Close this window" value="Close" class="button" language="javascript"  onclick="return btnClose_onclick()" />
		                </td>
		            </tr>

            </table>
    </div>
    </form>
</body>
</html>

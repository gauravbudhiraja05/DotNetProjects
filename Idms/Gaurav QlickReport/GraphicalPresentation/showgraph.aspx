<%@ Page Language="VB" AutoEventWireup="false" CodeFile="showgraph.aspx.vb" Inherits="Graphicalpresentation_showgraph" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us" >
<head runat="server">
    <title>Report</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <table>
            <caption><b><asp:Label ID ="lblReportname" runat="server" BackColor="#339999" ForeColor="white" ></asp:Label></b></caption>
            <tr>
                <td scope="colgroup">
                     <asp:GridView ID="gdGraphreport" Font-Size="8pt"  CssClass="datagrid" runat="server" AllowPaging="true"  width="432px" height="224px" ShowFooter="True" PageSize="20" ></asp:GridView>
                </td>
            </tr>
        </table>
    <asp:HiddenField ID="hidtablename" runat="server" />
      <asp:HiddenField ID="hidgoupbyvalue" runat="server" />
        <asp:HiddenField ID="hidformulavalue" runat="server" />
          <asp:HiddenField ID="hidorderbyvalue" runat="server" />
            <asp:HiddenField ID="hidhavingvalue" runat="server" />
              <asp:HiddenField ID="hidwherevalue" runat="server" />
                <asp:HiddenField ID="hidcolumnvalue" runat="server" />
    </div>
    </form>
</body>
</html>

<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="open_multiple.aspx.vb" Inherits="Graphs_open_multiple" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <style type="text/css">
        .style1
        {
            width: 331px;
            height: 27px;
        }
        .style4
        {
            width: 422px;
            height: 41px;
        }
        .style10
        {
            width: 220px;
            height: 41px;
        }
        .style11
        {
            width: 403px;
        }
        .style12
        {
            width: 403px;
            height: 41px;
        }
        .style13
        {
            width: 403px;
            height: 6px;
        }
        .style14
        {
            width: 403px;
            height: 8px;
        }
        .style15
        {
            width: 220px;
            height: 8px;
        }
        .style16
        {
            width: 220px;
            height: 6px;
        }
        .style19
        {
            width: 220px;
            height: 27px;
        }
        .style21
        {
            width: 220px;
        }
        </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">

    function savenew_multiple_onclick() {
        window.open("Savegraphnew_multiple.aspx", "SaveGraphNew", "width=400,height=220,scrollbars=yes,status=yes");
    }
</script>
<table runat="server" id="spandisplay" class="table">
<tr>
    <td class="style11">
        <asp:Label ID="select_level1"  CssClass="label" runat="server" Text="Select Level 1"></asp:Label>
    </td>
    <td class="style21"> 
        <asp:DropDownList ID="DepartmentName" CssClass="dropdownlist" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="style11">
        <asp:Label ID="Select_level2" CssClass="label" runat="server" Text="Select Level 2"></asp:Label>
    </td>
    <td class="style21"> 
        <asp:DropDownList ID="ClientName" CssClass="dropdownlist" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td class="style11">
        <asp:Label ID="select_level3" runat="server"  CssClass="label" Text="Select Level 3"></asp:Label>
    </td>
    <td class="style21"> 
        <asp:DropDownList CssClass=" dropdownlist " ID="ddlLobName" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
   
</tr>
<tr>
<td class="style13">
        <asp:Label ID="Label1" runat="server"  CssClass="label" Text="Query Name"></asp:Label>
        &nbsp;</td>
        <td class="style16"><asp:DropDownList CssClass=" dropdownlist " ID="queryname" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
    <tr>
    <td class="style14"> 
        <asp:Label ID="graphname" runat="server" Visible="false"   CssClass="label" Text="Graph Name"></asp:Label>
        &nbsp;</td>
        <td class="style15"><asp:DropDownList CssClass=" dropdownlist " Visible="false" ID="ddlgraph" runat="server" 
            AutoPostBack="True"></asp:DropDownList>
    </td>
</tr>
<tr>
<td valign="top" class="style12">
    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
    <asp:ListBox ID="repcols" Height="120px" Width="171px" CssClass="listBox" 
        runat="server" SelectionMode="Multiple" ></asp:ListBox>
    
</td>
<td valign="middle" class="style10">              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;              <asp:Button CssClass="button" ToolTip="Add Columns"  
            ID="add_clmn" runat="server"  Text=">>" Width="35px" /> 
                 <br />
                 &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                 <asp:Button CssClass="button" ToolTip="Remove Columns" 
            ID="remove_cols" runat="server" Text="<<" Width="36px" /> 
                 </td>
            <td class="style4">
    <asp:ListBox ID="selectedcols" runat="server" 
        Height="116px" Width="171px" CssClass="listBox" AutoPostBack="True" 
                    SelectionMode="Multiple" ></asp:ListBox> 
                    </td>
                   
</tr>
 <br /><br /><br />
<tr>
<td valign="middle" class="style11">
    <asp:Button ID="plotgraph" runat="server" Text="Plot Graph" CssClass="button" 
        Width="76px" />
    &nbsp;<asp:Button ID="update" runat="server" Text="Update" CssClass="button" />
    &nbsp;<asp:Button ID="delete" runat="server" Text="Delete" CssClass="button" />&nbsp;<asp:Button ID="Reset" runat="server" Text="Reset" CssClass="button" />
    <asp:Button ID="save_new" runat="server" Text="Save As " CssClass="button" OnClientClick ="savenew_multiple_onclick();" />
    </td>
    <td class="style19">
        &nbsp;&nbsp;</td>
    <td valign="middle">
    <asp:Button ID="update_axis" Text="Update Axis Member" Visible="false" runat="server" CssClass="button" />
        </td>
    <td class="style1">
        </td>
</tr>
<tr>
<td class="style11">
    <asp:Chart ID="chart_plot" runat="server">
        <Series>
            <asp:Series Name="Series1">
            </asp:Series>
            <asp:Series Name="Series2">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</td>
<td valign="top" class="style21">
        <table>
        <tr>
        <td valign="top"><asp:Label ID="sltchart" runat="server" CssClass="label" Text="Select Chart Type" Visible="false"></asp:Label>
            <br />
            </td>
        <td valign="top"></td>
        </tr>
        <tr>
        <td valign="top"><asp:Label ID="xaxis" Visible="false" runat="server" Text="X Axis Member" CssClass="label" ></asp:Label>
            <br />
        </td>
        </tr>
        <tr>
        <td valign="top"><asp:Label ID="yaxis1" Visible="false" runat="server" Text="Y Axis Member Ist" CssClass="label" ></asp:Label>
            <br />
        </td>
        </tr>
        <tr>
        <td valign="top"><asp:Label ID="yaxis2" Visible="false" runat="server" Text="Y Axis Member IInd" CssClass="label"></asp:Label>
        </td>
        </tr>
        </table>
        </td>
        
        <td valign="top">
        <table>
        <tr>
        <td valign="top">
            <asp:DropDownList Visible="false" ID="select_chart" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        <td></td>
        <td><asp:Button ID="plt_upgrph" runat="server" Visible="false" CssClass="button" 
                Text="Plot Updated Graph" Width="130px" /></td>
        </tr>
        <tr>
        <td valign="top">
            <asp:DropDownList Visible="false" ID="ddl_xaxis" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
            <br />
        </td>
        </tr>
        <tr>
        <td valign="top">
            <asp:DropDownList Visible="false" ID="ddl_yaxis1" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
        <tr valign="top">
        <td valign="top">
            <asp:DropDownList Visible="false" ID="ddl_yaxis2" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
        </table>
        
            
</td>
</tr>
</table>
</asp:Content>


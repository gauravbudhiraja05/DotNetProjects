<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="open_single.aspx.vb" Inherits="Graphs_open_single" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script language="javascript" type="text/javascript">

    function savenew_single_onclick() {
        window.open("Savegraphnew_single.aspx", "SaveGraphNew", "width=400,height=220,scrollbars=yes,status=yes");
    }
</script>
<table>
<tr>
<td>
<table>
<tr>
<td></td>
    <td align="center"> 
        <asp:Button ID="get_query" runat="server" Text="Get Query" CssClass="button" />
    </td>
   
</tr>
<tr>
        <td class="style3">
        <asp:Label ID="Label1" runat="server"  CssClass="label" Text="Query Name"></asp:Label>
        </td>
        <td class="style2"><asp:DropDownList CssClass=" dropdownlist " ID="queryname" runat="server" 
            AutoPostBack="True">
        </asp:DropDownList>
    </td>
    </tr>
<tr>
    <td class="style3"> <asp:Label ID="graphname" runat="server" Visible="false"   CssClass="label" Text="Graph Name"></asp:Label>
        &nbsp;</td>
        <td class="style2"><asp:DropDownList CssClass=" dropdownlist " Visible="false" ID="ddlgraph" runat="server" 
            AutoPostBack="True"></asp:DropDownList>
    </td>
</tr>
<tr>
            <td>
                <asp:ListBox ID="repcols" Height="120px" Width="171px" CssClass="listBox" runat="server" ></asp:ListBox>
            </td>
            <td align="center">
                 <asp:Button CssClass="button" ToolTip="Add Columns"  
                ID="add_clmn" runat="server"  Text=">>" Width="35px" /> 
                 <br />
                 <asp:Button CssClass="button" ToolTip="Remove Columns" 
                ID="remove_cols" runat="server" Text="<<" Width="36px" />
            </td>
            <td>
                <asp:ListBox ID="selectedcols" runat="server" 
                 Height="116px" Width="171px" CssClass="listBox" AutoPostBack="True" ></asp:ListBox>
            </td>
    </tr>
<tr>
<td colspan="3">
    <asp:Button ID="plotgraph" runat="server" Text="Plot Graph" CssClass="button" 
        Width="76px" />
    &nbsp;
    <asp:Button ID="update" runat="server" Text="Update" CssClass="button" />
    &nbsp;<asp:Button ID="delete" runat="server" Text="Delete" CssClass="button" />&nbsp;<asp:Button ID="Reset" runat="server" Text="Reset" CssClass="button" />
    &nbsp;<asp:Button ID="save_new" runat="server" Text="Save As " CssClass="button" OnClientClick ="savenew_single_onclick();" />
    &nbsp;
    <asp:Button ID="update_axis" Text="Update Axis Member" Visible="false" 
        runat="server" CssClass="button" Width="147px" />
        </td>
   </tr>
<tr>
        <td><asp:Label ID="sltchart" runat="server" CssClass="label" Text="Select Chart Type" Visible="false"></asp:Label>
            </td>
        <td>    <asp:DropDownList Visible="false" ID="select_chart" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
<tr>
        <td><asp:Label ID="xaxis" Visible="false" runat="server" Text="X Axis Member" CssClass="label" ></asp:Label></td>
        <td>    <asp:DropDownList Visible="false" ID="ddl_xaxis" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
<tr>
        <td><asp:Label ID="yaxis1" Visible="false" runat="server" Text="Y Axis Member Ist" CssClass="label" ></asp:Label>
        </td>
        <td>    <asp:DropDownList Visible="false" ID="ddl_yaxis1" CssClass="dropdownlist" runat="server">
            </asp:DropDownList>
        </td>
        </tr>
<tr>
        <td><asp:Label ID="yaxis2" Visible="false" runat="server" Text="Y Axis Member IInd" CssClass="label"></asp:Label>
        </td>
        <td><asp:DropDownList Visible="false" ID="ddl_yaxis2" CssClass="dropdownlist" runat="server">
            </asp:DropDownList></td>
        </tr>
<tr>
        <td></td>
        <td>
            <asp:Button ID="plt_upgrph" runat="server" Visible="false" CssClass="button" 
                Text="Plot Updated Graph" Width="130px" />
        </td>
        </tr>
</table>
</td>
</tr>
 <tr> 
<td id="chart_data1" runat="server" visible="false">
    <asp:Chart ID="chart_data" visible="false" runat="server" Height="500px" Width="425px">
        <series>
            <asp:Series Name="Series1">
            </asp:Series>
                 <asp:Series Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data2" runat="server"  visible="false">
    <asp:Chart ID="chart_data_pie" visible="false" runat="server" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Pie" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Pie" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data3" runat="server" visible="false">
    <asp:Chart ID="chart_data_bar" runat="server" visible="false" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Bar" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Bar" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data4" runat="server" visible="false">
<asp:Chart ID="chart_data_pyramid" visible="false" runat="server" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Pyramid" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Pyramid" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data5" runat="server" visible="false">
<asp:Chart ID="chart_data_funnel" runat="server" visible="false" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Funnel" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Funnel" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data6" runat="server" visible="false">
<asp:Chart ID="chart_data_doughnut" runat="server" visible="false" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Doughnut" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Doughnut" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data7" runat="server" visible="false">
<asp:Chart ID="chart_data_radar" runat="server" visible="false" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Radar" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Radar" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
<td id="chart_data8" runat="server" visible="false">
<asp:Chart ID="chart_data_polar" runat="server" visible="false" Height="500px" Width="426px">
        <series>
            <asp:Series ChartType="Polar" Name="Series1">
            </asp:Series>
            <asp:Series ChartType="Polar" Name="Series2">
            </asp:Series>
        </series>
        <chartareas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </chartareas>
    </asp:Chart>
</td>
</tr>
</table>
</asp:Content>


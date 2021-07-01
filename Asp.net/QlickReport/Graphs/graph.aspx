<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="graph.aspx.vb" Inherits="Graphs_graph" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
    <script language="javascript" type="text/javascript">
    function save_multiple_onclick() {
            window.open("Savegraph_multiple.aspx", "SaveGraph", "width=400,height=220,scrollbars=yes,status=yes");
            return false;
        }
        function save_singleuser_onclick() {
            window.open("Savegraph_singleuser.aspx", "SaveGraph", "width=400,height=220,scrollbars=yes,status=yes");
            return false;
        }
    </script>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<table>
<tr>
<td align="right"></td>
<td align="left">
<asp:LinkButton ID="show_multi" runat="server" Text="Show Graph" Visible="false" 
        PostBackUrl="~/Graphs/View Graph_multi.aspx"></asp:LinkButton>
<asp:LinkButton ID="show_single" runat="server" Text="Show Graph" Visible="false"></asp:LinkButton>
</td>
</tr>
<tr>
<td >
</td>
<td align="left" class="style6"> 
    <asp:Button ID="opengraph_single" runat="server" 
        Text="Open Graph" CssClass="button" Visible="False" />
            <asp:Button ID="opengraph_multiple" runat="server" Text="Open Graph" 
        CssClass="button" Visible="False" /></td>
</tr>
    <tr>
        <td align="center"><asp:Label ID="select" runat="server" CssClass="label" Text="Select" ></asp:Label>            
        </td>
        <td align="left" class="style6">
        <asp:RadioButton ID="rbtable" runat="server" GroupName="Radio_list" 
                Text="Tables" AutoPostBack="True" />
            <asp:RadioButton ID="rbreport" runat="server" GroupName="Radio_list" 
                Text="Report" AutoPostBack="True" />
        </td>
    </tr>
          <tr>
                    <td valign="top" align="center">
                        <asp:Label ID="select_level1"  CssClass="label" runat="server" Text="Select Level 1"></asp:Label>
                    </td>
                    <td valign="top" class="style6"> 
                        <asp:DropDownList ID="DepartmentName" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" >
                        <asp:Label ID="Select_level2" CssClass="label" runat="server" Text="Select Level 2"></asp:Label>
                    </td>
                    <td valign="top" class="style6"> 
                        <asp:DropDownList ID="ClientName" CssClass="dropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center">
                        <asp:Label ID="select_level3" runat="server"  CssClass="label" Text="Select Level 3"></asp:Label>
                    </td>
                    <td valign="top" class="style6"> 
                        <asp:DropDownList CssClass=" dropdownlist " ID="ddlLobName" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
   
                </tr>
                <tr>
        <td valign="top" class="style1">
            &nbsp;</td>
            <td class="style6"><asp:Button ID="get_tables" runat="server" CssClass="button" Visible="false"  Text="Get Tables" />
                <br />
            <asp:Button ID="get_report" runat="server"  CssClass="button" Visible="false"  Text="Get Report" />
        </td>
    </tr>
    <tr>
        <td  align="center" ><asp:Label ID="selected_radio"  CssClass="label" runat="server" Text="" Visible="false"></asp:Label></td>
        <td class="style6">
            <asp:DropDownList ID="ddlTable" CssClass="dropdownlist"  runat="server" 
                AutoPostBack="True" Visible="False"></asp:DropDownList> </td>
                
    </tr>
    <tr>
        <td class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style6">
                        <br />
                        <asp:Button ID="get_tbcolumn" Visible="False" runat="server" Text="Get Columns" 
                            CssClass="button" />                        
                        <asp:Button ID="get_tbcolumn_singleuser" Visible="False" runat="server" 
                            Text="Get Columns" CssClass="button" />
                        <br />
                        <asp:Button ID="get_rpcolumn" runat="server" CssClass="button" 
                            Text="Get Columns" Visible="False" />
                        <asp:Button ID="get_rpcolumn_singleuser" runat="server" CssClass="button" 
                            Text="Get Columns" Visible="False" />
    </td>
    </tr>
               <tr>
                 <td  valign="middle" align="right"><asp:ListBox  ID="repcols" runat="server" 
                         Height="120px" Width="171px" 
                         CssClass="listBox" SelectionMode="Multiple" ></asp:ListBox> 
                    </td>
    <td class="style6" align="center">&nbsp;<asp:Button CssClass="button" ToolTip="Add Columns"  
            ID="add_clmn" runat="server"  Text=">>" Width="35px" /> 
                 <br />
                 &nbsp;<asp:Button CssClass="button" ToolTip="Remove Columns" 
            ID="remove_cols" runat="server" Text="<<" Width="36px" /> 
                 </td>
               <td class="style5">
    <asp:ListBox ID="selectedcols" runat="server" 
        Height="116px" Width="171px" CssClass="listBox" SelectionMode="Multiple" ></asp:ListBox> 
                    </td>
</tr>
<tr>
<td class="style1">
    <br />
</td>
<td class="style6">
    <br />
&nbsp;</td>
<td class="style5">
    <br />
    <br />
&nbsp;</td>
</tr>
<tr>
<td align="center">
    <asp:Button ID="plot_graph" runat="server" CssClass="button" 
        Text="Default Graph" />
    <asp:Button ID="plot_graph_singleuser" runat="server" CssClass="button" 
        Text="Default Graph" /></td>
    <td class="style6">
        &nbsp;</td>
        <td class="style5">&nbsp;</td>
</tr>
<tr>
<%--<td>
    <asp:Chart ID="chart_data"  runat="server"  Width="400px" Height="422px">
        <Series>
            <asp:Series Name="Series1" >
            </asp:Series>
            <asp:Series Name="Series2">
            </asp:Series>
        </Series>
        <ChartAreas>
            <asp:ChartArea Name="ChartArea1">
            </asp:ChartArea>
            <asp:ChartArea Name="ChartArea2">
            </asp:ChartArea>
        </ChartAreas>
    </asp:Chart>
</td>--%>
<td valign="top">
<table>
<tr>
<td class="style4" >
    <asp:Label ID="sltchart" runat="server" CssClass="label" 
        Text="Select Chart Type" Visible="False" Font-Size="Small"></asp:Label></td>
</tr>
<tr>
<td class="style3" >
    <asp:Label ID="xaxis" runat="server" CssClass="label" 
        Text="X Axis Member" Visible="False" Font-Size="Small"></asp:Label></td>
</tr>
<tr>
<td class="style3" >
    <asp:Label ID="yaxis1" runat="server" CssClass="label" 
        Text="Y Axis Member Ist" Visible="False" Font-Size="Small"></asp:Label></td>
</tr>
<tr>
<td class="style3">
    <asp:Label ID="yaxis2" runat="server" CssClass="label" 
        Text="Y Axis Member IInd" Visible="False" Font-Size="Small"></asp:Label>
    <br />
    </td>
</tr>
<tr>
<td>
    <input id="save_multiple" type="button" visible="false"  class="button" runat="server" value="Save Graph" onclick="return save_multiple_onclick()" /><br />
    <input id="save_singleuser" type="button" visible="false"  class="button" runat="server" value="Save Graph" onclick="return save_singleuser_onclick()" />
    </td></tr>
</table>
</td>
<td valign="top" class="style6">
<asp:DropDownList Visible="false" ID="select_chart" CssClass="dropdownlist" 
        runat="server">
    </asp:DropDownList>
    <br />
    <asp:DropDownList Visible="false" ID="xaxis_select" CssClass="dropdownlist" runat="server">
    </asp:DropDownList>
    <br />
    <asp:DropDownList Visible="false" ID="yaxix1_select" CssClass="dropdownlist" runat="server">
    </asp:DropDownList>
    <br />
    <asp:DropDownList Visible="false" ID="yaxix2_select" CssClass="dropdownlist" runat="server">
    </asp:DropDownList>
    <br />
    <br />
     <asp:Button ID="plt_gph" runat="server" Text="Plot Graph" Visible="false" CssClass="button" />
    <br />
    <asp:Button ID="plt_gph_singleuser" runat="server" Text="Plot Graph" 
        Visible="false" CssClass="button" />
</td>
</tr>
</table>

<table>
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
<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="graph.aspx.vb" Inherits="Graphs_graph" %>

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
    <style type="text/css">
        .style1
        {
            width: 214px;
        }
        .style2
        {
            width: 190px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<table>
<tr>
<td >
</td>
<td align="left" class="style2"> <asp:Button ID="opengraph_single" runat="server" 
        Text="Open Graph" CssClass="button" />
            <asp:Button ID="opengraph_multiple" runat="server" Text="Open Graph" CssClass="button" /></td>
</tr>
    <tr>
        <td align="center"><asp:Label ID="select" runat="server" CssClass="label" Text="Select" ></asp:Label>            
        </td>
        <td align="left" class="style2">
        <asp:RadioButton ID="rbtable" runat="server" GroupName="Radio_list" 
                Text="Tables" AutoPostBack="True" />
            <asp:RadioButton ID="rbreport" runat="server" GroupName="Radio_list" 
                Text="Report" AutoPostBack="True" />
        </td>
        <%--<td valign="top">
            <asp:RadioButtonList TextAlign="Right" ID="list_tbrt" runat="server" AutoPostBack="True" 
                style="margin-left: 0px; margin-top: 0px; margin-right: 0px;" 
                Width="144px" Height="16px">
                <asp:ListItem>Tables</asp:ListItem><asp:ListItem>Report</asp:ListItem>
            </asp:RadioButtonList>
            
        &nbsp;</td>--%>
        <td>
        </td>
        <td></td>
    </tr>
          <tr>
                    <td valign="top" align="center">
                        <asp:Label ID="select_level1"  CssClass="label" runat="server" Text="Select Level 1"></asp:Label>
                    </td>
                    <td valign="top" class="style2"> 
                        <asp:DropDownList ID="DepartmentName" CssClass="dropdownlist" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center" >
                        <asp:Label ID="Select_level2" CssClass="label" runat="server" Text="Select Level 2"></asp:Label>
                    </td>
                    <td valign="top" class="style2"> 
                        <asp:DropDownList ID="ClientName" CssClass="dropdownlist" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td valign="top" align="center">
                        <asp:Label ID="select_level3" runat="server"  CssClass="label" Text="Select Level 3"></asp:Label>
                    </td>
                    <td valign="top" class="style2"> 
                        <asp:DropDownList CssClass=" dropdownlist " ID="ddlLobName" runat="server" AutoPostBack="True"></asp:DropDownList>
                    </td>
   
                </tr>
                <tr>
        <td valign="top" class="style1">
            &nbsp;</td>
            <td class="style2"><asp:Button ID="get_tables" runat="server" CssClass="button" Visible="false"  Text="Get Tables" />
                <br />
            <asp:Button ID="get_report" runat="server"  CssClass="button" Visible="false"  Text="Get Report" />
        </td>
    </tr>
    <tr>
        <td  align="center" ><asp:Label ID="selected_radio"  CssClass="label" runat="server" Text="" Visible="false"></asp:Label></td>
        <td class="style2"><asp:DropDownList ID="ddlTable" CssClass="dropdownlist"  runat="server" 
                AutoPostBack="True"></asp:DropDownList> </td>
                
    </tr>
    <tr>
        <td class="style1">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                    </td>
                    <td class="style2">
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
                 <td  valign="middle" align="right"><asp:ListBox  ID="repcols" runat="server" Height="120px" Width="171px" 
                         CssClass="listBox" ></asp:ListBox> 
                    </td>
    <td class="style2" align="center"><asp:Button CssClass="button" ToolTip="Add Columns"  
            ID="add_clmn" runat="server"  Text=">>" Width="35px" /> 
                 <br />
                 <asp:Button CssClass="button" ToolTip="Remove Columns" 
            ID="remove_cols" runat="server" Text="<<" Width="36px" /> 
                 </td>
               <td class="style31">
    <asp:ListBox ID="selectedcols" runat="server" 
        Height="116px" Width="171px" CssClass="listBox" ></asp:ListBox> 
                    </td>
</tr>
<tr>
<td class="style1">
    <br />
</td>
<td class="style2">
    <br />
&nbsp;</td>
<td class="style31">
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
    <td class="style2">
        &nbsp;</td>
        <td class="style31">&nbsp;</td>
</tr>
<tr>
<td class="style1">
    <asp:Chart ID="chart_data" runat="server" Width="400px" Height="422px">
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
</td>
<td valign="top" class="style2" >
<table>
<tr>
<td ><asp:Label ID="sltchart" runat="server" CssClass="label" Text="Select Chart Type" Visible="false"></asp:Label></td>\
</tr>
<tr>
<td ><asp:Label ID="xaxis" runat="server" CssClass="label" Text="X Axis Member" Visible="false"></asp:Label></td>
</tr>
<tr>
<td ><asp:Label ID="yaxis1" runat="server" CssClass="label" Text="Y Axis Member Ist" Visible="false"></asp:Label></td>
</tr>
<tr>
<td><asp:Label ID="yaxis2" runat="server" CssClass="label" Text="Y Axis Member IInd" Visible="false"></asp:Label></td>
</tr>
</table>
</td>
<td valign="top" class="style31">
<asp:DropDownList Visible="false" ID="select_chart" CssClass="dropdownlist" runat="server">
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
<td valign="top">
   </td>
</tr>
</table>
<table>
<tr>
<td>
    <input id="save_multiple" type="button"  class="button" runat="server" value="Save Graph" onclick="return save_multiple_onclick()" />
    <input id="save_singleuser" type="button"  class="button" runat="server" value="Save Graph" onclick="return save_singleuser_onclick()" /></td>
</tr>
</table>

</asp:Content>


<%@ Page Title="" Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="View Graph_multi.aspx.vb" Inherits="Graphs_View_Graph_multi" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<table >
<tr>
<td>
<asp:LinkButton ID="back" runat="server" Text="Click Here To Go Back" 
        Visible="false" PostBackUrl="~/Graphs/graph.aspx"></asp:LinkButton>
</td>
</tr>
<tr id="row1" runat="server" visible="true"></tr>
</table>
<table runat="server" visible="false">

<tr>
<td>
    <asp:Chart ID="Chart1" runat="server" Width="811px">
        <Series>
            <asp:Series Name="Series1">
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
</tr>
<tr>
<td>
    <asp:Chart ID="Chart2" runat="server"  Width="811px">
        <Series>
            <asp:Series Name="Series1">
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
</tr>
<tr>
<td>
    <asp:Chart ID="Chart3" runat="server"  Width="811px">
        <Series>
            <asp:Series Name="Series1">
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
</tr>
<tr>
<td>
    <asp:Chart ID="Chart4" runat="server"  Width="811px">
        <Series>
            <asp:Series Name="Series1">
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
</tr>
<tr>
<td>
    <asp:Chart ID="Chart5" runat="server"  Width="811px">
        <Series>
            <asp:Series Name="Series1">
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
</tr>
</table>
</asp:Content>


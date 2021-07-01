<%@ Page Language="VB" AutoEventWireup="false" CodeFile="analysisresult.aspx.vb" Inherits="analysisresult" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
     <link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <%--<table style="width:100;">
    <tr></tr>
<tr><td style="width: 60667px"></td>
<td style="width: 75201px">
<asp:Label ID="l1" runat="server" text="Regression" Width="144px"></asp:Label>
</td>
<td style="width:35px;"><asp:Label ID="reg" runat="server" text="Regression"></asp:Label></td>

<td style="width:100;">
<asp:LinkButton ID="reggh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px;">&nbsp;&nbsp;<asp:Label ID="l2" runat="server" text="Correlation"></asp:Label></td>
<td style="width:100;"><asp:Label ID="corel" runat="server" text="Correlation"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="corelgh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">Compare Samples</td>

<td style="width:35px;"><asp:Label ID="comsep" runat="server" text="Compare Samples" Width="216px"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="comsepgh" runat="server">Graph</asp:LinkButton>
</td>


<td style="width:217px;">
&nbsp;&nbsp;Row Percentage
</td>
<td style="width:100;"><asp:Label ID="rowper" runat="server" text="Row Percentage"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="rowergh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">Column percentage</td>
<td style="width:35px;"><asp:Label ID="colper" runat="server" text="Column percentage" Width="133px"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="colpergh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px;">&nbsp;&nbsp;Non-Weighted Number</td>

<td style="width:100;"><asp:Label ID="nonwegnum" runat="server" text="Non-Weighted Number" Width="157px"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="nonwegnumgh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">
Filter Percentage
</td>
<td style="width:35px;"><asp:Label ID="ftper" runat="server" text="Filter Percentage" Width="151px"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="ftpergh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px;">&nbsp;&nbsp;<asp:Label ID="l3" runat="server" text="Index" Width="150px"></asp:Label></td>
<td style="width:100;"><asp:Label ID="index" runat="server" text="Index" Width="50px"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="indexgh" runat="server">Graph</asp:LinkButton>
</td></tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">Mean</td>

<td style="width:35px;"><asp:Label ID="mean" runat="server" text="Mean"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="meangh" runat="server">Graph</asp:LinkButton>
</td>


<td style="width:217px;">&nbsp;&nbsp;
Median
</td>
<td style="width:100;"><asp:Label ID="median" runat="server" text="Median"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="mediangh" runat="server">Graph</asp:LinkButton>
</td></tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px; height: 22px;">Mode</td>
<td style="width:35px; height: 22px;"><asp:Label ID="mode" runat="server" text="Mode"></asp:Label></td>
<td style="width:100; height: 22px;"> 
<asp:LinkButton ID="modegh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px; height: 22px;">&nbsp;&nbsp;Range</td>

<td style="width:100; height: 22px;"><asp:Label ID="range" runat="server" text="Range"></asp:Label></td>
<td style="width:312px; height: 22px;">
<asp:LinkButton ID="rangegh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">
Standard deviatio
</td>
<td style="width:35px;"><asp:Label ID="stdev" runat="server" text="Standard deviatio" Width="125px"></asp:Label></td>
<td>
<asp:LinkButton ID="stdevgh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px;">&nbsp;&nbsp;Standard error</td>
<td style="width:100;"><asp:Label ID="ster" runat="server" text="Standard error"></asp:Label></td>

<td style="width:312px;">
<asp:LinkButton ID="stergh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px; height: 21px;"></td>
<td style="width:75201px; height: 21px;">Row Sum Percentage</td>

<td style="width:35px; height: 21px;"><asp:Label ID="rowsumper" runat="server" text="Row Sum Percentage" Width="159px"></asp:Label></td>
<td style="width:100; height: 21px;">
<asp:LinkButton ID="rowsumpergh" runat="server">Graph
</asp:LinkButton>
</td>

<td style="width:217px; height: 21px;">
&nbsp;&nbsp;Column Sum Percentage
</td>
<td style="width:100; height: 21px;"><asp:Label ID="colsumper" runat="server" text="Column Sum Percentage" Width="175px"></asp:Label></td>
<td style="width:312px; height: 21px;">
<asp:LinkButton ID="colsumpergh" runat="server">Graph</asp:LinkButton>
</td></tr><tr><td style="width: 60667px"></td>
<td style="width:75201px;">Accumulated Sum</td>
<td style="width:35px;"><asp:Label ID="accumsum" runat="server" text="Accumulated Sum"></asp:Label></td>
<td>
<asp:LinkButton ID="accumsumgh" runat="server">Graph</asp:LinkButton>
</td>

<td style="width: 217px">&nbsp;&nbsp;Min</td>

<td style="width:100;"><asp:Label ID="min" runat="server" text="Min"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="mingh" runat="server">Graph</asp:LinkButton>
</td>

</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">
Max
</td>
<td style="width:35px;"><asp:Label ID="max" runat="server" text="Max"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="maxgh" runat="server">Graph</asp:LinkButton>
</td>
<td style="width:217px;">&nbsp;&nbsp;Count</td>
<td style="width:100;"><asp:Label ID="count" runat="server" text="Count"></asp:Label></td>
<td style="width:312px;">
<asp:LinkButton ID="countgh" runat="server">Graph</asp:LinkButton>
</td>
</tr>
<tr><td style="width: 60667px"></td>
<td style="width:75201px;">Average</td>

<td style="width:35px;"><asp:Label ID="average" runat="server" text="Average"></asp:Label></td>
<td style="width:100;">
<asp:LinkButton ID="averagegh" runat="server">Graph
</asp:LinkButton>
</td>
</tr>
</table>--%>
<div id="report" runat="server" style="padding:20px;">
   <table><tr><td><asp:GridView id="grdhtmlreport" runat="server" AutoGenerateColumns="False">
       <Columns>
          
           
           <asp:TemplateField>
           <HeaderTemplate><label id="lbldephead">DepartmentName</label></HeaderTemplate><ItemTemplate>
           <label id="lbldept"><%#databinder.Eval(container.dataitem,"DepartmentName")%></label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><label id="lbldclthead">ClientName</label></HeaderTemplate><ItemTemplate>
           <label id="lblclt"><%#databinder.Eval(container.dataitem,"ClientName")%></label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><label id="lbllobhead">LOBName</label></HeaderTemplate><ItemTemplate>
           <label id="lbllob"><%#databinder.Eval(container.dataitem,"LOBName")%></label>
           </ItemTemplate>
           </asp:TemplateField>
           <asp:TemplateField>
           <HeaderTemplate><label id="lblrephead">ReportName</label></HeaderTemplate><ItemTemplate>
           <%--<asp:HyperLink runat="server" id="lhprept" Text="<%#databinder.Eval(container.dataitem,"ReportName")%>" NavigateUrl="~/DataAnalysis/analysisresult.aspx?repname="+"<%# databinder.Eval(container.dataitem,"ReportName")%>"></asp:HyperLink>--%>
           <asp:Label ID="lkrepname" runat="server"><a href="analysisresult.aspx?repname="+"<%#databinder.Eval(container.dataitem,"ReportName")%>"><%#databinder.Eval(container.dataitem,"ReportName")%></a></asp:Label>
           </ItemTemplate>
           </asp:TemplateField>
            <asp:HyperLinkField HeaderText="Delete" Text="Delete" />
       </Columns>
   </asp:GridView></td></tr></table>
       </div>
   
    </div>
    <div id="result" runat="server">
    </div>
        <asp:Button ID="Button1" runat="server" Style="z-index: 100; left: 56px; position: absolute;
            top: 311px" Text="Show Result" Width="170px" CssClass="button"  />
    </form>
</body>
</html>

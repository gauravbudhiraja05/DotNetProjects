<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" title="Home Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
<link rel="stylesheet" type="text/css" href="../App_Themes/Themes/StyleSheet.css" />
<script type="text/javascript">
    function popupwindow(DepartmentID, ClientId, UnderLOB, TableName,Queryname) 
    {
        var Dept, client, lob, table,queryname;
        dept = DepartmentID
        client = ClientId
        lob = UnderLOB
        table = TableName
        queryname=Queryname
        var win = window.open("../Misc/DispQuery1.aspx?dept="+dept+"&client="+client+"&lob="+lob+"&Table="+table+"&Queryname="+queryname+"", "Experi", "height=600,width=600");

    }
</script> 
<%--<table style="margin-left:150px;margin-top:100px; background-image:url(../images/cent.jpg) ; background-repeat:no-repeat; width: 490px; height: 274px;">--%>
<table style="background-image:url(../images/cent.jpg) ; background-repeat:no-repeat; width: 600px; height: 274px;">
    <tr>
        <td style="height:10PX;"></td>
    </tr>
    <tr>
        <td valign="top">
            <div>
                <table style="margin-left:10px">
                    <tr>
                        <td style="width: 79px" class="label">Welcome:</td>
                        <td style="width: 138px"><asp:Label runat="Server" Font-Bold="true"  cssclass="label" ID="lblUserId" Text="UserID"></asp:Label></td>
                    </tr>
                    <tr>
                        <td style="width: 79px" class="label">Login Time:</td>
                        <td style="width: 138px"><asp:Label runat="server" ID="lblLoginTime" Font-Bold="true"   cssclass="label" Text="LoginTime" Width="243px"></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="2" style="height: 18px"><asp:Label ID="lblPasswordEx" runat="server" ForeColor="Red" Text="Your password will Expire in " Visible="False" Width="300px"></asp:Label></td>
                    </tr>
                </table>
            </div>
        </td>
    </tr>
    <tr>
        <td style="height:10PX;"></td>
    </tr>
    <tr>
        <td>
            <table style="width:900px;" cellpadding="0" cellspacing="0" >
                <tr>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                        <table style="width:100%;" runat="server" id="tblreport" visible="false" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Report Designer</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="lblreport" runat="server" Font-Bold="true"   Text="No Recent Report Available" ForeColor="Blue"  Visible="false" ></asp:Label>     
                                <asp:GridView ID="gridreport" Height="135px" CssClass="gridview" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:TemplateField><HeaderTemplate>Report Name</HeaderTemplate> 
                                        <ItemTemplate>
                                       <%-- <asp:LinkButton ID="lnkbtn" runat="server" Text='<%#Eval("Queryname")%>' PostBackUrl='<%#"../Misc/DispQuery1.aspx?dept="+Eval("DepartmentID")+"&client="+Eval("ClientID")+"&lob="+Eval("UnderLOB")+"&Table="+Eval("TableName")+"&Queryname="+Eval("Queryname")+""%>'>
                                        </asp:LinkButton>--%>
                                         <asp:LinkButton ID="lnkbtn" runat="server" Text='<%#Eval("Queryname")%>'>
                                        </asp:LinkButton>
                                         </ItemTemplate> 
                                        </asp:TemplateField>
                                        <%--<asp:BoundField HeaderText="Report Name" DataField="Queryname" />--%>
                                        <asp:BoundField HeaderText="Createdby" DataField="SavedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                                </td>
                            </tr>
                        </table>
                        <table style="width:100%;" runat="server" id="tblquery" visible="false" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Query Builder</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="lblquery" runat="server" Font-Bold="true"   Text="No Recent Query Available" ForeColor="Blue" Visible="false" ></asp:Label> 
                                   <asp:GridView ID="gridquery" Height="135px" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <%-- <asp:TemplateField><HeaderTemplate>Query Name</HeaderTemplate> 
                                        <ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn2" runat="server" Text='<%#Eval("Queryname")%>' PostBackUrl='<%#"../Misc/ResultOutput.aspx?dept="+Eval("DepartmentId")+"&client="+Eval("ClientId")+"&lob="+Eval("lobyName")+"&name="+Eval("Queryname")+""%>'>
                                        </asp:LinkButton>
                                         </ItemTemplate> 
                                        </asp:TemplateField>--%>
                                        <asp:BoundField HeaderText="Query Name" DataField="Queryname" />
                                        <asp:BoundField HeaderText="Createdby" DataField="SavedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                                
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                       <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">HTML Reports</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="lblhtmlreport" runat="server" Font-Bold="true" Text="No Recent HTML Report Available" ForeColor="Blue" Visible="false" ></asp:Label> 
                                 <asp:GridView ID="gridhtmlreport" Height="135px" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField HeaderText="HtmlFile Name" DataField="SavedFilename" />
                                        <asp:BoundField HeaderText="Createdby" DataField="SavedBy" />
                                        </Columns>
                                        </asp:GridView>
                                </td>
                            </tr>
                        </table>  
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Graphs</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                  <asp:Label ID="lblgraph" runat="server" Font-Bold="true" Text="No Recent Graph Available" ForeColor="Blue" Visible="false" ></asp:Label> 
                                     <asp:GridView ID="gridgraph" Height="135px" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField HeaderText="Graph Name" DataField="Graphname" />
                                        <asp:BoundField HeaderText="Createdby" DataField="SavedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                            </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr><td style="height:10px;"></td></tr>
                <tr>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Views</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="lblview" runat="server" Font-Bold="true" Text="No Recent View Available" ForeColor="Blue" Visible="false" ></asp:Label> 
                                        <asp:GridView ID="gridview" Height="135px" Width="180px"  runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField HeaderText="View Name" DataField="Viewname" />
                                        <asp:BoundField HeaderText="Createdby" DataField="CreatedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                              </td>
                            </tr>
                        </table>
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Tables</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="lbltable" runat="server" Font-Bold="true" Text="No Recent Table Available" ForeColor="Blue" Visible="false" ></asp:Label> 
                                        <asp:GridView ID="gridtable" Height="135px" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField HeaderText="Table Name" DataField="tablename" />
                                        <asp:BoundField HeaderText="Createdby" DataField="CreatedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;" valign="top">
                      <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Graph 
                                    Preview</td>
                            </tr>
                            <tr>
                                <td style="height:135px; background-color:#e7e7e7;" valign="top">
                                <asp:Label ID="Label1" runat="server" Font-Bold="true" Text="No Recent Preview Available" ForeColor="Blue"></asp:Label> 
                                        <asp:GridView ID="GridView1" Height="135px" runat="server" AutoGenerateColumns="false" BackColor="White" 
                                        BorderColor="#336666" BorderStyle="Double" BorderWidth="3px" CellPadding="4"  GridLines="Horizontal" >
                                        <FooterStyle BackColor="White" ForeColor="#333333" />
                                        <HeaderStyle BackColor="#336666" Font-Bold="True" ForeColor="White" />
                                        <PagerStyle BackColor="#336666" ForeColor="White" HorizontalAlign="Center" />
                                        <RowStyle BackColor="White" ForeColor="#333333" />
                                        <SelectedRowStyle BackColor="#339966" Font-Bold="True" ForeColor="White" />
                                        <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                        <SortedAscendingHeaderStyle BackColor="#487575" />
                                        <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                        <SortedDescendingHeaderStyle BackColor="#275353" />
                                        <Columns>
                                        <asp:TemplateField><HeaderTemplate>S.No</HeaderTemplate><ItemTemplate><%#Container.DataItemIndex+1 %></ItemTemplate></asp:TemplateField>
                                        <asp:BoundField HeaderText="Table Name" DataField="tablename" />
                                        <asp:BoundField HeaderText="Createdby" DataField="CreatedBy" />
                                        </Columns>
                                        </asp:GridView>
                                </td>
                            </tr>
                        </table>  
                    </td>
                </tr>

            </table>
        </td>
    </tr>
</table>

</asp:Content>


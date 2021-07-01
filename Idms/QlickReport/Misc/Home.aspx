<%@ Page Language="VB" MasterPageFile="~/MasterPage/MasterPage.master" AutoEventWireup="false" CodeFile="Home.aspx.vb" Inherits="Home" title="Home Page" %>

<%@ Register assembly="System.Web.DataVisualization, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35" namespace="System.Web.UI.DataVisualization.Charting" tagprefix="asp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
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
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Report Designer</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                
                          <asp:GridView ID="gridreport" runat="server" AutoGenerateColumns="false" BackColor="White" 
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
                                        <asp:LinkButton ID="lnkbtn" runat="server" Text='<%#Eval("Queryname")%>' PostBackUrl='<%#"../Misc/DispQuery1.aspx?dept="+Eval("DepartmentID")+"&client="+Eval("ClientID")+"&lob="+Eval("UnderLOB")+"&Table="+Eval("TableName")+""%>'>
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
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Query Builder</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                
                                
                                
                                    <asp:GridView ID="gridquery" runat="server" AutoGenerateColumns="false" BackColor="White" 
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
                                         <asp:TemplateField><HeaderTemplate>Query Name</HeaderTemplate> 
                                        <%--<ItemTemplate>
                                        <asp:LinkButton ID="lnkbtn2" runat="server" Text='<%#Eval("Queryname")%>' PostBackUrl='<%#"../Misc/ResultOutput.aspx?dept="+Eval("DepartmentId")+"&client="+Eval("ClientId")+"&lob="+Eval("lobyName")+""%>'>
                                        </asp:LinkButton>
                                         </ItemTemplate>--%> 
                                        </asp:TemplateField>
                                        <asp:BoundField HeaderText="Report Name" DataField="Queryname" />
                                        <asp:BoundField HeaderText="Createdby" DataField="SavedBy" />
                                        </Columns>
                                        </asp:GridView>
                                
                                
                                
                                </td>
                            </tr>
                        </table>
                    </td>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Graphs</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                    
                                     <asp:Chart ID="Chart1" runat="server" Height="200px" style="margin-top: 15px" 
                                         Width="200px">
                                         <series>
                                             <asp:Series Name="Series1">
                                             </asp:Series>
                                         </series>
                                         <chartareas>
                                             <asp:ChartArea Name="ChartArea1">
                                             </asp:ChartArea>
                                         </chartareas>
                                     </asp:Chart>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr><td style="height:10px;"></td></tr>
                <tr>
                    <td style="Width:10PX;"></td>
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Views</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                              <asp:GridView ID="gridview" runat="server" AutoGenerateColumns="false" BackColor="White" 
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
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">Recent Tables</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                        <asp:GridView ID="gridtable" runat="server" AutoGenerateColumns="false" BackColor="White" 
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
                    <td style="Width:180PX;">
                        <table style="width:100%;" cellpadding="0" cellspacing="0">
                            <tr>
                                <td style="background-color:#000000; font-family:Verdana; color:White;">HTML Reports</td>
                            </tr>
                            <tr>
                                <td style="height:180px; background-color:#e7e7e7;">
                                 <asp:GridView ID="gridhtmlreport" runat="server" AutoGenerateColumns="false" BackColor="White" 
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
                            <tr>
                            <td></td>
                            </tr>
                        </table>
                    </td>
                </tr>

            </table>
        </td>
    </tr>
</table>

</asp:Content>


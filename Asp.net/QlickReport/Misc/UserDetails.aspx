<%@ Page Language="VB" AutoEventWireup="false" CodeFile="UserDetails.aspx.vb" Inherits="Misc_UserDetails" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" BackColor="White"
            BorderColor="White" BorderStyle="Ridge" BorderWidth="2px" CellPadding="3" CellSpacing="1"
            DataSourceID="SqlDataSource1" EnableSortingAndPagingCallbacks="True" Font-Size="Smaller"
            GridLines="None" Height="132px" Width="1056px">
            <FooterStyle BackColor="#C6C3C6" ForeColor="Black" />
            <Columns>
                <asp:BoundField DataField="UserID" HeaderText="UserID" SortExpression="UserID" />
                <asp:BoundField DataField="UserName" HeaderText="UserName" SortExpression="UserName">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Designation" HeaderText="Designation" SortExpression="Designation">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Email" HeaderText="Email" SortExpression="Email">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Status" HeaderText="Status" SortExpression="Status" />
                <asp:BoundField DataField="PWDStatus" HeaderText="PWDStatus" SortExpression="PWDStatus" />
                <asp:BoundField DataField="LockReason" HeaderText="LockReason" SortExpression="LockReason" />
                <asp:BoundField DataField="LockDate" HeaderText="LockDate" SortExpression="LockDate">
                    <ItemStyle Wrap="False" />
                </asp:BoundField>
                <asp:BoundField DataField="Createdby" HeaderText="Createdby" SortExpression="Createdby" />
                <asp:BoundField DataField="Department" HeaderText="Department" SortExpression="Department" />
                <asp:BoundField DataField="Client" HeaderText="Client" SortExpression="Client" />
                <asp:BoundField DataField="LOB" HeaderText="LOB" SortExpression="LOB" />
            </Columns>
            <RowStyle BackColor="#DEDFDE" ForeColor="Black" />
            <SelectedRowStyle BackColor="#9471DE" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#C6C3C6" ForeColor="Black" HorizontalAlign="Right" />
            <HeaderStyle BackColor="#4A3C8C" Font-Bold="True" ForeColor="#E7E7FF" />
            <AlternatingRowStyle BorderStyle="Ridge" BorderWidth="1px" />
        </asp:GridView>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:Bmp10ConnectionString %>"
            SelectCommand="select UserID,UserName,Designation,Email,Status,PWDStatus,LockReason,LockDate,Createdby,(select departmentname from idmsdepartment where autoid=deptid) Department, (select clientname from idmsClient where autoid=clientid) Client,(select lobname from warslobmaster where autoid=lobid) LOB from registration ">
        </asp:SqlDataSource>
    
    </div>
    </form>
</body>
</html>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="temptreeview.aspx.vb" Inherits="Misc_temptreeview" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml" lang="en-us" xml:lang="en-us">
<head runat="server">
    <title>Untitled Page</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
     <table>
				<tr>
					<td align="left" valign="top" style="width: 111px"  >
						<table class="grid">
							<tr>
								<td >
									<div id="Divdisplay" runat="server">
							<asp:TreeView id ="menu" runat="server" ExpandDepth="1" ImageSet="Arrows">
                                        <ParentNodeStyle Font-Bold="False" ForeColor="White" />
                                        <HoverNodeStyle Font-Underline="True" ForeColor="Aqua" />
                                        <SelectedNodeStyle Font-Underline="True" HorizontalPadding="0px" VerticalPadding="0px" />
                                        <NodeStyle Font-Names="Verdana" Font-Size="8pt" ForeColor="White" HorizontalPadding="5px"
                                            NodeSpacing="0px" VerticalPadding="0px" />
                                    </asp:TreeView>
						

									<%--<iewc:treeview id="menu" runat="server"></iewc:treeview></div>--%>
									</div>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
    </div>
    </form>
</body>
</html>

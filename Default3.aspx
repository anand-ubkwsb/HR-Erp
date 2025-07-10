<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default3.aspx.cs" Inherits="Default3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>

       <asp:GridView ID="GridView11" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowFooter="true" ShowHeaderWhenEmpty="true" DataKeyNames="Sno" DataSourceID="SqlDataSource1">
                                                        <Columns>
                                                            <asp:BoundField DataField="Sno" HeaderText="Sno" InsertVisible="False" ReadOnly="True" SortExpression="Sno" />
                                                            <asp:BoundField DataField="AccountCode" HeaderText="AccountCode" SortExpression="AccountCode" />
                                                            <asp:BoundField DataField="AccntDesc" HeaderText="AccntDesc" SortExpression="AccntDesc" />
                                                            <asp:BoundField DataField="Narration" HeaderText="Narration" SortExpression="Narration" />
                                                            <asp:BoundField DataField="Rowsno_RowsAll" HeaderText="Rowsno_RowsAll" SortExpression="Rowsno_RowsAll" />
                                                            <asp:BoundField DataField="ChangeToCost" HeaderText="ChangeToCost" SortExpression="ChangeToCost" />
                                                            <asp:BoundField DataField="Amount" HeaderText="Amount" SortExpression="Amount" />
                                                        </Columns>
                                                        <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                        <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Height="5px" />
                                                        <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                                    </asp:GridView>

                                                    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [AccountCode], [AccntDesc], [Narration], [Rowsno_RowsAll], [ChangeToCost], [Amount] FROM [Pur_Addition]"></asp:SqlDataSource>



    </div>
    </form>
</body>
</html>

<%@ Page Language="C#" AutoEventWireup="true" CodeFile="Default6.aspx.cs" Inherits="Default6" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <link rel="stylesheet" type="text/css" href="//netdna.bootstrapcdn.com/bootstrap/3.0.3/css/bootstrap.min.css">
     <link rel="stylesheet" href="../../bower_components/bootstrap/dist/css/bootstrap.min.css" />
    <!-- Font Awesome -->
    <link rel="stylesheet" href="../../bower_components/font-awesome/css/font-awesome.min.css" />
    <!-- Ionicons -->
    <link rel="stylesheet" href="../../bower_components/Ionicons/css/ionicons.min.css" />
    <!-- Theme style -->
    <link rel="stylesheet" href="../../dist/css/AdminLTE.min.css" />
    <!-- AdminLTE Skins. Choose a skin from the css/skins
       folder instead of downloading all of them to reduce the load. -->
    <link rel="stylesheet" href="../../dist/css/skins/_all-skins.min.css" />
    
    <link rel="stylesheet" href="../../bower_components/select2/dist/css/select2.min.css"/>
    <style>
        .tableFixHead123 {
            overflow-y: auto;
            height: 500px;
        }
        .tableFixHead {
            overflow-y: auto;
            height: 100px;
        }

            .tableFixHead  th {
                position: sticky;
                top: 0;
            }

        /* Just common table stuff. Really. */
        table {
            border-collapse: collapse;
        }

        th, td {
            padding: 8px 16px;
        }

        th {
            background: #eee;
        }
    </style>
    <script src="Scripts/jquery-1.7.js"></script>
   

    <script type="text/javascript"  lang="js">

        $(function () {
            
            $("#<%=TextBox1.ClientID %>").keyup(function () {
                document.getElementById("Label1").innerHTML = document.getElementById("TextBox1").value;
              
                
            });

        });
 

    </script> 
</head>
<body>
    <form id="form1" runat="server">
   <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
  <p>
        <asp:TextBox ID="TextBox1" runat="server"  Width="274px"></asp:TextBox>
      
        </p>
        <p>

            <asp:DropDownList ID="DropDownList1" runat="server" Width="216px">
            </asp:DropDownList>
            
        </p>
        <p>

            
            

           </p>
        <p>

            <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        </p>
        <p>
            

            <asp:FormView ID="FormView1" runat="server" >
                <EditItemTemplate>
                    CompName:
                    <asp:TextBox ID="CompNameTextBox" runat="server" Text='<%# Bind("CompName") %>' />
                    <br />
                    GOCName:
                    <asp:TextBox ID="GOCNameTextBox" runat="server" Text='<%# Bind("GOCName") %>' />
                    <br />
                    BusinessNature:
                    <asp:TextBox ID="BusinessNatureTextBox" runat="server" Text='<%# Bind("BusinessNature") %>' />
                    <br />
                    MainProduct_Services:
                    <asp:TextBox ID="MainProduct_ServicesTextBox" runat="server" Text='<%# Bind("MainProduct_Services") %>' />
                    <br />
                    EntryDate:
                    <asp:TextBox ID="EntryDateTextBox" runat="server" Text='<%# Bind("EntryDate") %>' />
                    <br />
                    <asp:LinkButton ID="UpdateButton" runat="server" CausesValidation="True" CommandName="Update" Text="Update" />
                    &nbsp;<asp:LinkButton ID="UpdateCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </EditItemTemplate>
                <InsertItemTemplate>
                    CompName:
                    <asp:TextBox ID="CompNameTextBox" runat="server" Text='<%# Bind("CompName") %>' />
                    <br />
                    GOCName:
                    <asp:TextBox ID="GOCNameTextBox" runat="server" Text='<%# Bind("GOCName") %>' />
                    <br />
                    BusinessNature:
                    <asp:TextBox ID="BusinessNatureTextBox" runat="server" Text='<%# Bind("BusinessNature") %>' />
                    <br />
                    MainProduct_Services:
                    <asp:TextBox ID="MainProduct_ServicesTextBox" runat="server" Text='<%# Bind("MainProduct_Services") %>' />
                    <br />
                    EntryDate:
                    <asp:TextBox ID="EntryDateTextBox" runat="server" Text='<%# Bind("EntryDate") %>' />
                    <br />
                    <asp:LinkButton ID="InsertButton" runat="server" CausesValidation="True" CommandName="Insert" Text="Insert" />
                    &nbsp;<asp:LinkButton ID="InsertCancelButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" />
                </InsertItemTemplate>
                <ItemTemplate>
                    <table style="width:100%; " class="table table-bordered table-hover">
                        <tr>
                            <td>CompName</td>
                            <td>
                                <asp:Label ID="CompNameLabel" runat="server" Text='<%# Bind("CompName") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>GOCName</td>
                            <td>
                                <asp:Label ID="GOCNameLabel" runat="server" Text='<%# Bind("GOCName") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>BusinessNature</td>
                            <td>
                                <asp:Label ID="BusinessNatureLabel" runat="server" Text='<%# Bind("BusinessNature") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>MainProduct_Services</td>
                            <td>
                                <asp:Label ID="MainProduct_ServicesLabel" runat="server" Text='<%# Bind("MainProduct_Services") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                        <tr>
                            <td>EntryDate</td>
                            <td>
                                <asp:Label ID="EntryDateLabel" runat="server" Text='<%# Bind("EntryDate") %>' />
                            </td>
                            <td>&nbsp;</td>
                        </tr>
                    </table>
                    <br />
                    <br />
                    <br />

                </ItemTemplate>
            </asp:FormView>

            <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" DataKeyNames="AuthorityId" DataSourceID="SqlDataSource1">
                <Columns>
                    <asp:TemplateField ShowHeader="False">
                        <ItemTemplate>
                            <asp:LinkButton ID="LinkButton1" runat="server" CausesValidation="False" CommandName="Delete" Text="Delete"></asp:LinkButton>
                            <asp:Button ID="Button1" runat="server" Text="Button" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="AuthorityId" HeaderText="AuthorityId" InsertVisible="False" ReadOnly="True" SortExpression="AuthorityId" />
                    <asp:BoundField DataField="AuthorityName" HeaderText="AuthorityName" SortExpression="AuthorityName" />
                    <asp:BoundField DataField="AuthorityLevel" HeaderText="AuthorityLevel" SortExpression="AuthorityLevel" />
                    <asp:BoundField DataField="Remarks" HeaderText="Remarks" SortExpression="Remarks" />
                    <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
                    <asp:BoundField DataField="CompID" HeaderText="CompID" SortExpression="CompID" />
                    <asp:BoundField DataField="SysDate" HeaderText="SysDate" SortExpression="SysDate" />
                    <asp:BoundField DataField="UserId" HeaderText="UserId" SortExpression="UserId" />
                    <asp:BoundField DataField="EntryDate" HeaderText="EntryDate" SortExpression="EntryDate" />
                    <asp:CheckBoxField DataField="Record_Deleted" HeaderText="Record_Deleted" SortExpression="Record_Deleted" />
                </Columns>
            </asp:GridView>
       
            

            <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT * FROM [SET_Authority]"></asp:SqlDataSource>
       
            

            </p>
    </form>
   
</body>
</html>

<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_pay_Employee.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });
        }
        function wrong() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Something went wrong!',

            })
        }

        function noupdate() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'No Update',
                text: 'Nothing Change',

            })
        }

        function periodoverlap() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Date period overlap!!',

            })
        }
        function fillrequird() {
            swal({
                type: 'error',
                icon: "warning",
                title: 'Oops...',
                text: 'Please select the row to delete!',

            })
        }
        function nodatafound() {
            swal({
                type: 'error',
                icon: "error",
                title: 'Data Not Found !',
                text: 'Search again ...',

            })
        }


        function alertDelete() {

            swal({
                type: 'error',
                icon: "dist/img/del.png",
                title: 'Deleted',
                text: 'Delete Data Successfully!'
            }).then(function () {
                window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
        }
    </script>
    <style type="text/css">
        .shadowbox {
            box-shadow: 0 4px 8px 0 rgba(0,0,0,0.2), 0 6px 20px 0 rgba(0,0,0,0.19);
            background-color: #026dbd;
        }

        .gap {
            margin-bottom: 10px;
        }

        .example button {
            float: left;
            background-color: #4E3E55;
            color: white;
            border: none;
            box-shadow: none;
            font-size: 17px;
            font-weight: 500;
            font-weight: 600;
            border-radius: 3px;
            padding: 15px 35px;
            margin: 26px 5px 0 5px;
            cursor: pointer;
        }

            .example button:focus {
                outline: none;
            }

            .example button:hover {
                background-color: #33DE23;
            }

            .example button:active {
                background-color: #81ccee;
            }

        .fas {
            top: 0px;
            display: inline;
            font: normal normal normal 14px/1 FontAwesome;
            font-size: 20px;
            text-rendering: auto;
            -webkit-font-smoothing: antialiased;
            -moz-osx-font-smoothing: grayscale;
        }

        .fa-save, .fa-edit, .fa-trash, .fa-search, .fa-remove {
            font-size: 14px;
        }

        .backcolorlbl {
            height: 25px;
            width: 250px;
            /*background-color:#fbfcf2;*/
            padding-left: 10px;
            padding-top: 5px;
            background-color: #eeeeee;
        }

        .rowpadd {
            padding-bottom: 8px;
        }
        .rowpadd1 {
            padding-top: 15px;
        }

        .padspace {
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
        }

        .ddlHeight {
            height: 25px;
        }

        /*tab style*/
        #tabmenu {
            clear: both;
            float: left;
            list-style: none;
            margin: 0px 10px 0 0px;
            border-bottom: 1px solid #333333; /*Remove to create tab effect*/
            width: 800px; /*If tab effect is not being used, this will define how long the bottom border will be*/
        }

            #tabmenu h3 {
                display: inline;
                float: left;
            }

            #tabmenu h3 {
                margin-right: 5px;
                margin-top: 20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height: 40px;
                border-radius: 10px 10px 0px 0px;
                font: 14px/100% Arial, Helvetica, sans-serif;
                line-height: 62px;
                text-decoration: none;
                vertical-align: middle;
                padding: 0 20px;
                background-image: url(nav-tab-bg.png);
                background-color: #1f4f8a; /*A basic background color is set incase the image will not load*/
                border: 1px solid #333333;
                border-bottom: none;
            }
        /*End tab style*/
        .rdbgap{
            padding-right:30px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
  
    <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">
                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Employee</strong></h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 310px;">
                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click" ValidationGroup="aaa">
                     <i class="fas fa-save"></i>&nbsp Save
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEdit" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_edit" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-edit"></i>&nbsp Edit
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnUpdate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdate_Click">
                     <i class="fas fa-edit"></i>&nbsp Update
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete_after" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnDelete_Click">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnFind" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_find" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-search"></i>&nbsp Find
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnCancel_Click">
                     <i class="fas fa-remove"></i>&nbsp Cancel
                            </asp:LinkButton>
                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">

                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>



                                        <td class="auto-style6"><strong>User Group Name:</strong></td>
                                        <td class="auto-style7">
                                            <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                                        </td>


                                        <td class="auto-style4"><strong>Description</strong></td>
                                        <td class="auto-style1 ">
                                            <asp:DropDownList ID="DropDownList1" CssClass="select2" Width="250px" runat="server"></asp:DropDownList>
                                        </td>

                                </tr>
                                    
                                  

                                    <tr>
                                    <td class="auto-style6"><strong>User Group Name:</strong></td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="TextBox3" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4"><strong>Description</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="TextBox4" runat="server"></asp:TextBox>
                                    </td>
                                </tr>


                                    <tr>
                                    <td class="auto-style6"><strong>User Group Name:</strong></td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="TextBox5" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4"><strong>Description</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="TextBox6" runat="server"></asp:TextBox>
                                    </td>
                                </tr>

                                    <tr>
                                    <td class="auto-style6"><strong>User Group Name:</strong></td>
                                    <td class="auto-style7">
                                        <asp:TextBox ID="TextBox7" runat="server"></asp:TextBox>
                                    </td>
                                    <td class="auto-style4"><strong>Description</strong></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="TextBox8" runat="server"></asp:TextBox>
                                    </td>
                                </tr>
                                        
                                </table>
                                  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [CityName], [CountryID], [EntryDate], [User], [IsActive], [Record_Deleted] FROM [SET_City]"></asp:SqlDataSource>

                            </div>



                            <%-- Table View --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CheckBoxField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>

                                            

                                            

                                            

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <%-- Table View --%>



                            <div id="DeleteBox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged">
                                                       <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                      <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CheckBoxField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>


                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <div style="width: 100%; height: 400px; overflow: scroll">
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" >
                                                       <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document Name" SortExpression="DocDescription" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="RequisitionNo" HeaderText="Requisition No" SortExpression="RequisitionNo" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="RequistionDate" HeaderText="Requistion Date" SortExpression="Requistion Date" DataFormatString = "{0:dd-MMM-yyyy}"  >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName">
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    
                                                     <asp:BoundField DataField="DepartmentName" HeaderText="Issue Department Name" SortExpression="DepartmentName" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CheckBoxField>
                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>

                                        </div>
                                    </div>
                                    <!-- /.box-body -->
                                </div>
                            </div>


                        </div>
                        <!-- /.box-body -->
                        <div class="box-footer">

                            <asp:Label ID="Label1" runat="server" Text="" ForeColor="Red"></asp:Label>

                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
              
            </div>
        </div>

    </section>
            


    <%-- Start Modals --%>
    <%-- Start Modals --%>

    
    <div class="modal fade" id="modal_edit">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit</h4>
                </div>
                <div class="modal-body">


                    <table style="width: 100%;">
                        <tr>
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >

                                <asp:DropDownList ID="ddlEdit_ReqTypr" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                   

                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEdit_ReqDate" CssClass="form-control texthieht" Width="350px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton3" TargetControlID="txtEdit_ReqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>

                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                       <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkEdit_Active" runat="server" Checked="true" Text="Active" />
                            </td>
                        </tr>



                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btnSearchEdit" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearchEdit_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modal_find">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Find </h4>
                </div>
                <div class="modal-body">


                     <table style="width: 100%;">
                       <tr>
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_ReqDate" CssClass="form-control texthieht" Width="350px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtFind_ReqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                    <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllFind_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        

                     <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkFind_Active" runat="server" Checked="true" Text="Active" />
                            </td>
                        </tr>
                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_Search" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_Search_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>

    <div class="modal fade" id="modal_delete">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Delete</h4>
                </div>
                <div class="modal-body">

                  <table style="width: 100%;">
                      <tr>
                            <td class="rowpadd"><strong>Document Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_DocName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                       <tr>
                            <td class="rowpadd"><strong>Requisition Type</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_ReqType" CssClass="form-control select2" Width="100%" runat="server">
                                     <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Department</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Purchase</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                         <tr>
                            <td class="rowpadd"><strong>Requisition Date</strong> </td>
                            <td class="rowpadd" >
                                
                              <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDelete_REqDate" CssClass="form-control texthieht" Width="400px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" TargetControlID="txtDelete_REqDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>

                                </div>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Priority</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_Priority" CssClass="form-control select2" Width="100%" runat="server">
                                      <asp:ListItem>Please select...</asp:ListItem>
                                    <asp:ListItem Text="Department" Value="D">Normal</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Urgent</asp:ListItem>
                                    <asp:ListItem Text="Purchase" Value="P">Very Urgent</asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="dllDelete_Department" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                     <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkDel_Active" runat="server" Checked="true" Text="Active" />
                            </td>
                        </tr>
                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_delete" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_delete_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--  ENd Modals --%>
    <%--  ENd Modals --%>

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>
   
</asp:Content>


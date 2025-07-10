<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_ItemHead.aspx.cs" Inherits="frm_Period" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {

                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_CostCenter.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
        }

        function myfocus(elementRef) {
            //  elementRef.style.backgroundColor = "yellow";

        }

        function myBlur(elementRef) {
            elementRef.style.backgroundColor = "red";

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
        .vali{
            background-color:#ffd8d8
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

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
        }

        .ddlHeight {
            height: 25px;
        }

        .auto-style6 {
            width: 173px;
        }

        .auto-style9 {
            font-weight: bold;
        }

        .auto-style10 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
        }
        .imginline{
            display:inline-block;
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
        .auto-style11 {
            padding-bottom: 7px;
            width: 360px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Item Head</strong></h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 310px;">
                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" ValidationGroup="changest">
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click" ValidationGroup="aaa">
                     <i class="fas fa-save"></i>&nbsp Save
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnEdit" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_edit" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-edit"></i>&nbsp Edit
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnUpdate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdate_Click" ValidationGroup="aaa">
                     <i class="fas fa-edit"></i>&nbsp Update
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete_after" OnClientClick="return sweetAlertConfirm(this);" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnDelete_Click">
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
                                                <td class="auto-style6"><strong>Item Head Name</strong></td>
                                                <td class="auto-style11">
                                                    
                                                    <asp:TextBox ID="txtItemHeadName" CssClass="form-control " Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtItemHeadName" Display="None" ErrorMessage="<b> Missing Field</b><br />A name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style10">Item Type</td>
                                                <td class="padspace">
                                                    <div class="texthieht">
                                                          <asp:DropDownList ID="ddlItemType" Width="250px" CssClass="form-control select2" runat="server"></asp:DropDownList>
                                                    </div>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Urdu Category Name</span></td>
                                                <td class="auto-style11">

                                                     <asp:TextBox ID="txtUrduCatName" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ></asp:TextBox>
                                                </td>
                                                  <td class="auto-style5"><strong>Image</strong></td>
                                                <td class="padspace">
                                                    <div class="imginline">
                                                     <asp:Image ID="imgDisplayArea" runat="server"  Height="85px" Width="74px" AlternateText="  NO IMAGE" />
                                                        <asp:FileUpload ID="FileUpload1"  runat="server" />
                                                    <asp:Button ID="btnUpload" runat="server" Text="Upload"  OnClick="btnUpload_Click" /><br />
                                                        
                                                        </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style10">Created By</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtCreatedBy" CssClass="form-control " Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="4"></asp:TextBox>
                                                </td>
                                                <td class="auto-style10">Created Date</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtSystemDate" CssClass="form-control " Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="5"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr id="updatecol" runat="server">
                                                <td class="auto-style10">Updated By</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtUpdatedBy" CssClass="form-control " Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="6"></asp:TextBox>

                                                </td>
                                                <td class="auto-style10">Updated Date</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtUpadtedDate" CssClass="form-control " Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Style="margin-bottom: 1" TabIndex="7"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Is Invisible Upon Stock Reports?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <asp:CheckBox ID="chkIsInvisibleUponStockReports" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td class="auto-style3"><span class="auto-style9">Is Inventory?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel2" runat="server">
                                                        <asp:CheckBox ID="chkIsInventory" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Is Fixed Asset?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel4" runat="server">
                                                        <asp:CheckBox ID="chkIsFixedAsset" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td class="auto-style3"><span class="auto-style9">Is Salleable?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel5" runat="server">
                                                        <asp:CheckBox ID="chkIsSalleable" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>

                                            </tr>

                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Is Project?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel6" runat="server">
                                                        <asp:CheckBox ID="chkIsProject" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td class="auto-style3"><span class="auto-style9">Is Expensable?</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel7" runat="server">
                                                        <asp:CheckBox ID="chkIsExpensable" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>

                                            </tr>
                                            
                                            <tr>
                                              <td class="auto-style3"><span class="auto-style9">Active</span></td>

                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="YES" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>

                                            </tr>


                                        </table>
                                   
                            </div>



                            <%-- Table View --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="ItemHeadID">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemHeadID" HeaderText="ID" SortExpression="ItemHeadID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemHeadName" HeaderText="Item Head Name" SortExpression="ItemHeadName">
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="Description" HeaderText="Item Type" SortExpression="Description">
                                                    <HeaderStyle Width="500px" />
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

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound">
                                              <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemHeadID" HeaderText="ID" SortExpression="ItemHeadID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemHeadName" HeaderText="Item Head Name" SortExpression="ItemHeadName">
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="Description" HeaderText="Item Type" SortExpression="Description">
                                                    <HeaderStyle Width="500px" />
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
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" OnRowDataBound="GridView3_RowDataBound">
                                              <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemHeadID" HeaderText="ID" SortExpression="ItemHeadID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemHeadName" HeaderText="Item Head Name" SortExpression="ItemHeadName">
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="Description" HeaderText="Item Type" SortExpression="Description">
                                                    <HeaderStyle Width="500px" />
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
                            <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
                        </div>
                        <!-- /.box-footer -->
                    </div>
                </div>
                <!-- /.box -->


            </div>
        </div>

    </section>

            

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
                            <td><strong>Item Head Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlEdit_ItemHeadName" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtEdit_ItemHead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                            </td>
                        </tr>
                         <tr>
                            <td><strong>Item Type</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlEdit_ItemType" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtEdit_ItemHead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                            </td>
                        </tr>
                       
                        <tr>
                            <td><strong>Active</strong> </td>
                            <td>
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
                            <td><strong>Item Head Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_itemhead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="ddlFind_itemhead" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td><strong>Item Type</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlFind_ItemType" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtEdit_ItemHead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                            </td>
                        </tr>
                       <tr>
                            <td><strong>Active</strong> </td>
                            <td>
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
                            <td><strong>Item Head Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlDelete_ItemHead" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td><strong>Item Type</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlDel_ItemType" Width="100%" CssClass="select2" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtEdit_ItemHead" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                            </td>
                        </tr>
                      <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkdelete_Active" runat="server" Checked="true" Text="Active" />
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

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>


</asp:Content>


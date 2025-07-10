<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_COA_Detail.aspx.cs" Inherits="frm_SET_COA_Master" MaintainScrollPositionOnPostback=" true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    

    <link href="bower_components/datatables.net-bs/css/dataTables.bootstrap.min.css" rel="stylesheet" />


   <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_COA_Detail.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_COA_Detail.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SET_COA_Detail.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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


        .uppperCase {
            text-transform: uppercase;
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
            padding-left: 10px;
            padding-top: 5px;
            background-color: #eeeeee;
        }

        .texthieht {
            height: 25px;
        }


        .padspace {
            padding-bottom: 0px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style8 {
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style9 {
            font-weight: bold;
        }

        .auto-style11 {
            width: 173px;
            padding-bottom: 7px;
            height: 36px;
        }

        .auto-style12 {
            padding-bottom: 7px;
            width: 296px;
            height: 36px;
        }

        .auto-style15 {
            width: 173px;
            padding-bottom: 7px;
            height: 47px;
        }

        .auto-style16 {
            padding-bottom: 7px;
            width: 296px;
            height: 47px;
        }

        .auto-style17 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
            height: 47px;
        }

        .auto-style18 {
            padding-bottom: 7px;
            height: 47px;
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
    </style>
    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
   
  
         
    <section class="content">

        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height: 75px;">

                        <div style="padding-top: 0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>COA Detail</strong></h3>
                            </div>
                        </div>
                        <div style="height: 60px; width: 650px; margin-left: 310px;"">

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
                                            <tr id="rowid" runat="server">
                                                <td class="auto-style3"><span class="auto-style9">Serial No</span></td>
                                                <td class="auto-style8">
                                                    <div class="backcolorlbl">
                                                        <asp:Label ID="lblserialNo" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </td>
                                                <td class="auto-style5"><strong>Master ID </strong></td>
                                                <td>
                                                   <div class="backcolorlbl">
                                                        <asp:Label ID="lblMasterID" runat="server" Text="Label"></asp:Label>
                                                    </div>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Account Code</span></td>
                                                <td class="auto-style8">
                                                     
                                                    <asp:TextBox ID="txtAcoount_Code" CssClass="form-control texthieht" TabIndex="1" Width="250px" runat="server" Font-Bold="False" ForeColor="Black" AutoPostBack="True" OnTextChanged="txtAcoount_Code_TextChanged"></asp:TextBox>
                                                    <ajaxToolkit:MaskedEditExtender ID="MaskedEditExtender1" TargetControlID="txtAcoount_Code" Mask=" " ClearMaskOnLostFocus="false" runat="server" ClearTextOnInvalid="True" />

                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtAcoount_Code" InitialValue="_-__-__-____" Display="None" ErrorMessage="<b> Missing Field</b><br />A Account Code is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <asp:Label ID="lbl_error" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lbl_head1" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lbl_head2" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lbl_head3" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lblaccout_code" runat="server" Text="label"></asp:Label>


                                                </td>
                                                <td class="auto-style5"><strong>Account Description </strong></td>
                                                <td>
                                                    <asp:TextBox ID="txtAccountDescrip" CssClass="texthieht uppperCase" TabIndex="2" Width="250px" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtAccountDescrip"  Display="None" ErrorMessage="<b> Missing Field</b><br />A Account Description is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>

                                                </td>
                                            </tr>
                                            <tr>


                                                <td class="auto-style15"><span class="auto-style9">Account type</span></td>
                                                <td class="auto-style16">
                                                    
                                                    <asp:DropDownList ID="ddlAcc_type" CssClass="texthieht" Width="250px"   TabIndex="3" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAcc_type_SelectedIndexChanged">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlAcc_type" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />A Account Type is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                               
                                                </td>

                                                <td class="auto-style15"><span class="auto-style9">Account Sub type</span></td>
                                                <td class="auto-style16">

                                                    <asp:DropDownList ID="ddlAcc_sub_type" CssClass="texthieht" Width="250px" TabIndex="4" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlAcc_sub_type" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />A Account Sub Type is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            <tr>

                                                <td class="auto-style17">Base Account Code</td>
                                                <td class="auto-style18">
                                                    <asp:TextBox ID="txtbaseAcc_Code" CssClass="form-control texthieht" Width="250px" TabIndex="5" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>
                                                </td>
                                                <td class="auto-style5"><strong>Head / Detail</strong></td>
                                                <td class="auto-style8">

                                                    
                                                    <asp:TextBox ID="txtHead_Detail" CssClass="form-control texthieht" Width="250px" runat="server" TabIndex="6" Font-Bold="False"  ForeColor="Black"></asp:TextBox>                                        
                                           
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">System Date</span></td>
                                                <td class="auto-style8">
                                                    <asp:TextBox ID="txtSystemDATE" CssClass="form-control texthieht" Width="250px" runat="server" TabIndex="7" Font-Bold="False" ForeColor="Black"></asp:TextBox>


                                                </td>
                                                <td class="auto-style11"><span class="auto-style9">Transaction Type</span></td>
                                                <td class="auto-style12">

                                                    <asp:TextBox ID="txtTrancstion_type" CssClass="form-control  texthieht" Width="250px" TabIndex="8" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>
                                                    

                                                </td>
                                                

                                            </tr>
                                            <tr>
                                                <td class="auto-style3"><span class="auto-style9">Group of Company </span></td>
                                                <td class="auto-style8">
                                                   <%-- <asp:TextBox ID="txtGOC" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>--%>
                                                    <asp:DropDownList ID="ddlGOC" CssClass="texthieht" Width="250px" TabIndex="9" runat="server" AutoPostBack="True">
                                                    </asp:DropDownList>

                                                </td>
                                               <td class="auto-style5"><strong>User Name </strong></td>
                                                <td>
                                                    <asp:TextBox ID="txtUserName" CssClass="form-control texthieht" Width="250px" TabIndex="10" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>
                                                </td>

                                            </tr>
                                            <tr>
                                                  <td class="auto-style5"><strong>Active </strong></td>
                                                <td>
                                                    <asp:Panel ID="Panel3" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" TabIndex="11" Text="Active" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>
                                                <td class="auto-style3"><span class="auto-style9">Bar Code</span></td>
                                                <td class="auto-style8">
                                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                            <ContentTemplate>
                                                                <asp:LinkButton ID="LinklblGenerarte" runat="server" TabIndex="12" OnClick="LinklblGenerarte_Click">Generate</asp:LinkButton>
                                                                <asp:PlaceHolder ID="plBarCode" runat="server"  />
                                                    </ContentTemplate>
                                                        </asp:UpdatePanel>
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
                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="COA_D_ID">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="COA_D_ID" HeaderText="ID" SortExpression="COA_D_ID" InsertVisible="False" ReadOnly="True">
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Code" HeaderText="Account Code" SortExpression="Acct_Code">
                                                <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Description" HeaderText="Account Description" SortExpression="Acct_Description" >
                                                <HeaderStyle Width="250px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                       <div style="width: 100%; height: 400px; overflow: scroll">
                                       <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="COA_D_ID">
                                           <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="COA_D_ID" HeaderText="ID" SortExpression="COA_D_ID" InsertVisible="False" ReadOnly="True">
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Code" HeaderText="Account Code" SortExpression="Acct_Code">
                                                <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Description" HeaderText="Account Description" SortExpression="Acct_Description" >
                                                <HeaderStyle Width="250px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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
                                        <%--CssClass="table table-bordered table-hover"--%>
                                         <div style="width: 100%; height: 400px; overflow: scroll">
                                          <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="COA_D_ID">
                                           <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="COA_D_ID" HeaderText="ID" SortExpression="COA_D_ID" InsertVisible="False" ReadOnly="True">
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Code" HeaderText="Account Code" SortExpression="Acct_Code">
                                                <HeaderStyle Width="80px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Acct_Description" HeaderText="Account Description" SortExpression="Acct_Description" >
                                                <HeaderStyle Width="250px" />
                                                </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CheckBoxField>
                                            </Columns>
                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Width="250  " />
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

             

    <%-- Start MOdals --%>
    <div class="modal fade" id="modal_edit">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Edit </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Account Code</strong> </td>
                            <td>
                               <%-- <asp:TextBox ID="txtEdit_AccountCode" CssClass="form-control gap" runat="server"></asp:TextBox>--%>


                                  <!--   asdasddsad -->
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="txtEdit_AccountCode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="txtEdit_AccountCode_ItemsRequested"
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Text")%>
                                    </td>
                                    <td style="width: 175px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%>
                                    </td>   
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                <!-- asdasdsadasdsa-->


                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_AccountDes" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_AccountDes" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account type</strong> </td>
                            <td>
                               <%-- <asp:TextBox ID="txtEdit_AccountType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_AccountType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account sub type</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_AccountSubType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_AccountSubType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                       <%-- <tr>
                            <td><strong>Transaction type</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_TransactionType" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>--%>
                        <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                <asp:CheckBox ID="ChkEdit_Active" runat="server" Checked="true" Text="Active" />
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
                            <td><strong>Account Code</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_AccountCode" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                                  <!--   asdasddsad -->
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="txtFind_AccountCode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="txtFind_AccountCode_ItemsRequested"
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Text")%>
                                    </td>
                                    <td style="width: 175px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%>
                                    </td>   
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                <!-- asdasdsadasdsa-->


                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_AccountDesc" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_AccountDesc" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account type</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_AccountType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_AccountType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account sub type</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_AccountSubT" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_AccountSubT" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                    <h4 class="modal-title">Delete </h4>
                </div>
                <div class="modal-body">

                    <table style="width: 100%;">
                        <tr>
                            <td><strong>Account Code</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_AccountCode" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                                  <!--   asdasddsad -->
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="txtDelete_AccountCode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="txtDelete_AccountCode_ItemsRequested"
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 60px;">
                                        Acct Code
                                    </td>
                                    <td style="width: 175px;">
                                        
                                        Account Description
                                    </td>
                                    <td style="width: 40px;">
                                        Acct Type
                                    </td>
                                     <td style="width: 40px;">
                                        Tran Type
                                    </td>
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Text")%>
                                    </td>
                                    <td style="width: 175px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%>
                                    </td>
                                    <td style="width: 60px;">
                                        <%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%>
                                    </td>   
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                <!-- asdasdsadasdsa-->
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account Description</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_AccountDesc" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_AccountDesc" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account type</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_AccountType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_AccountType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Account sub type</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_AccountStype" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDelete_AccountStype" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                       
                        <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                <asp:CheckBox ID="chkDelete_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>
                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_sdelete" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_delete_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>
    <%--  ENd Modals --%>
    


 


    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>


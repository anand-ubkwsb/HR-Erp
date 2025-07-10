<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_PurchaseOpening.aspx.cs" Inherits="frm_Period" enableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        


        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }
        //Once deleted, you will not be able to recover this imaginary file!
       

        function fileupd() {
            swal("Upload Successfuly!", "Your file has been uploaded!!!", "success")
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

        .ModalPopupBG {
            background-color: #666699;
            filter: alpha(opacity=50);
            opacity: 0.7;
        }

        .HellowWorldPopup {
            min-width: 200px;
            min-height: 150px;
            background: white;
        }



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

        .auto-style6 {
            width: 173px;
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
            width: 271px;
        }
        .vali{
            background-color:#ffd8d8
        }
        .auto-style13 {
            padding-bottom: 7px;
            width: 293px;
        }
        .auto-style14 {
            width: 233px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Purchase Opening</strong></h3>
                            </div>
                        </div>

                      
                        <div style="height: 60px; width: 650px; margin-left: 310px;">
                            <asp:LinkButton ID="btnInsert" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnInsert_Click" >
                     <i class="fas fa-save"></i>&nbsp Add
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnSave" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnSave_Click"  ValidationGroup="aaa">
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
                                            <tr>
                                                <td class="auto-style6"><strong>Doc Name</strong></td>
                                                <td class="auto-style11">
                                                 <asp:DropDownList ID="ddlDocName" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlDocName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>  
                                                   
                    
                                                </td>
                                                <td class="auto-style6"><strong>Supplier</strong></td>
                                                <td class="auto-style11">
                                               <asp:DropDownList ID="ddlSupplier" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                </asp:DropDownList>
                                                </td>
                                                 <td class="auto-style6"><strong>Status</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="ddlStatus" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator10" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>


                                                </td>

                                            </tr>
                                           
                                            <tr>
                                               
                                                <td class="auto-style6"><strong>Active</strong></td>
                                                <td class="auto-style11">

                                                    <asp:CheckBox ID="chkActive" Text="Active" runat="server" />
                                                   
                                                </td>
                                                 <td class="auto-style6" ><strong>Description</strong></td>
                                                <td class="auto-style11" colspan="3">
                                                   <asp:TextBox ID="txtDesc" CssClass="texthieht" Width="760px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator19" ControlToValidate="txtDesc" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select Payment mode." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender20" TargetControlID="RequiredFieldValidator19" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>

                                                </td>

                                            </tr>

                                            <tr>
                                               
                                                <td class="auto-style6"><strong>Created Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCreatedDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                                </td>
                                                <td class="auto-style6"><strong>Updated Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>
                                             </table>
                                        
                                        
                                
                                
                                      <table id="uploadtable" runat="server" style="width: 100%;">
                                    <tr>
                                        <td>
                                         Type
                                        </td>
                                        <td>
                                            <asp:RadioButton ID="rdbMaster" Text="MASTER" GroupName="md" runat="server" />
                                            <asp:RadioButton ID="rdbDetail" Text="DETAIL" GroupName="md" runat="server" />
                                        </td>
                                    </tr>
                                          <tr>
                                        <td class="auto-style14">
                                            <strong>File Upload</strong>
                                        </td>
                                        <td class="auto-style13">
                                            <asp:FileUpload ID="FileUpload1" runat="server" />

                                        </td>
                                        <td>
                                            <asp:Button ID="Button1" runat="server" Text="Upload File" />
                                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender1" runat="server"
                                                CancelControlID="btnNO"
                                                TargetControlID="Button1" PopupControlID="Panel2"
                                                PopupDragHandleControlID="PopupHeader" Drag="true"
                                                BackgroundCssClass="ModalPopupBG">
                                            </ajaxToolkit:ModalPopupExtender>

                                            <asp:Panel ID="Panel2" Style="display: none" runat="server">
                                                <div class="HellowWorldPopup">
                                                    <div class="popup_Titlebar" id="PopupHeader">Upload File</div>
                                                    <div class="popupBody">
                                                        <p>Do you want to upload?</p>
                                                    </div>
                                                    <div class="Controlsbtn">
                                                        <asp:Button ID="btnYes" runat="server" Text="Yes" OnClick="btnYes_Click" />
                                                        <asp:Button ID="btnNO" runat="server" Text="NO" OnClick="btnNO_Click" />

                                                    </div>
                                                </div>
                                            </asp:Panel>

                                        </td>
                                    </tr>
                                </table>

                            </div>

                            




                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender3" runat="server"
                                CancelControlID="btnNo_2"
                                TargetControlID="Button2" PopupControlID="Panel4"
                                PopupDragHandleControlID="PopupHeader2" Drag="true"
                                BackgroundCssClass="ModalPopupBG">
                            </ajaxToolkit:ModalPopupExtender>

                             <asp:Panel ID="Panel4" Style="display:none" runat="server">
                                                        <div class="HellowWorldPopup">
                                                            <div class="popup_Titlebar" id="PopupHeader2">Successfully uploaded</div>
                                                            <div class="popupBody">
                                                                <p>Your file has been Uploaded</p>
                                                            </div>
                                                            <div class="Controlsbtn">
                                                                <asp:Button ID="btn_ok" runat="server" Text="OK" OnClick="btnYes_2_Click"/>
                                                            </div>
                                                        </div>
                                                    </asp:Panel>


                            <%-- Table View --%>
                            <ajaxToolkit:ModalPopupExtender ID="ModalPopupExtender2" runat="server"
                                CancelControlID="btnNo_2"
                                TargetControlID="btn_ok" PopupControlID="Panel3"
                                PopupDragHandleControlID="PopupHeader1" Drag="true"
                                BackgroundCssClass="ModalPopupBG">
                            </ajaxToolkit:ModalPopupExtender>

                             <asp:Panel ID="Panel3" Style="display:none" runat="server">
                                                        <div class="HellowWorldPopup">
                                                            <div class="popup_Titlebar" id="PopupHeader1">Final</div>
                                                            <div class="popupBody">
                                                                <p>Is that Final?</p>
                                                            </div>
                                                            <div class="Controlsbtn">

                                                                <asp:Button ID="btnYes_2" runat="server" Text="Yes" OnClick="btnYes_2_Click"/>


                                                                <asp:Button ID="btnNo_2" runat="server" Text="NO" OnClick="btnNo_2_Click"  />
                                                                
                                                            </div>
                                                        </div>
                                                    </asp:Panel>

                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno" >
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="BPartnerName" SortExpression="BPartnerName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="StatusName" SortExpression="StatusName" />
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" >
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
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="BPartnerName" SortExpression="BPartnerName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="StatusName" SortExpression="StatusName" />
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" >
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
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="BPartnerName" HeaderText="BPartnerName" SortExpression="BPartnerName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="StatusName" SortExpression="StatusName" />
                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive" >
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
                <!-- /.box -->

                
            </div>
        </div>
              <div id="Div2" runat="server">
                    <div class="box">

                        <!-- /.box-header -->

                        <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                            <%-- CssClass="table table-bordered table-hover"--%>

                            <div style="width: 100%; height: 200px; overflow: scroll">

                                <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" ShowFooter="true" ShowHeaderWhenEmpty="true" OnRowCommand="GridView6_RowCommand" OnRowDataBound="GridView6_RowDataBound" OnRowDeleting="GridView6_RowDeleting">

                                    <Columns>
                                        <asp:TemplateField HeaderText="Sno" ItemStyle-Width="50">
                                            <ItemTemplate>
                                                <asp:Label ID="lblRowNumber" runat="server" />
                                            </ItemTemplate>

                                            <ItemStyle Width="100px"></ItemStyle>
                                        </asp:TemplateField>


                                             <asp:TemplateField HeaderText="A/c code" SortExpression="A/c code">
                                            <EditItemTemplate>
                                              <asp:Label ID="lblAcctCodeEdit" runat="server" Enabled="false" ></asp:Label>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("AccountCode") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>


                                                <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_CodeFooter" runat="server" Height="200" Width="150"
                                                    DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                                    EnableLoadOnDemand="true" Filter="StartsWith"
                                                    OnItemsRequested="RadComboAcct_CodeFooter_ItemsRequested"
                                                    
                                                    Label="">
                                                    <HeaderTemplate>
                                                        <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                            <tr>
                                                                <td style="width: 60px;">Acct Code
                                                                </td>
                                                                <td style="width: 175px;">Account Description
                                                                </td>
                                                                <td style="width: 40px;">Acct Type
                                                                </td>
                                                                <td style="width: 40px;">Tran Type
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






                                                <%--RAD Account Code--%>
                                            </FooterTemplate>
                                        </asp:TemplateField>

                                           <asp:TemplateField HeaderText="Entry Date" SortExpression="EntryDate">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEntryDateEdit" runat="server" ></asp:TextBox>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEntryDate" runat="server" Width="100px" Text='<%# Bind("EntryDate") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>




                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtEntrydateFooter" CssClass="form-control texthieht" Width="120px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender9" PopupButtonID="imgPop33" TargetControlID="txtEntrydateFooter" Format="dd-MMM-yyyy" runat="server" />
                                                        
                                                        <span class="input-group-btn">

                                                            <asp:ImageButton ID="imgPop33" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>

                                            </FooterTemplate>
                                        </asp:TemplateField>





                                        <asp:TemplateField HeaderText="Voucher No." SortExpression="UOM">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtVoucherNoEdit" runat="server"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblVoucherNo" runat="server" Text='<%# Bind("VoucherNo") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtVoucherNoFooter" runat="server" ></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Bill No" SortExpression="CurrentStock">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillNoEdit" runat="server" Width="100px"></asp:TextBox>
                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillNo" runat="server" Width="100px" Text='<%# Bind("BillNo") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBillNoFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                         <asp:TemplateField HeaderText="Bill Date" SortExpression="EntryDate">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillDateEdit" runat="server"></asp:TextBox>

                                            </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillDate" Width="100px" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>




                                                <div style="display: inline-block">

                                                    <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtBillDateFooter" CssClass="form-control texthieht" Width="120px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender10" PopupButtonID="img1" TargetControlID="txtBillDateFooter" Format="dd-MMM-yyyy" runat="server" />
                                                        
                                                        <span class="input-group-btn">

                                                            <asp:ImageButton ID="img1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                        </span>
                                                    </div>
                                                </div>

                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Bill Amount" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillAmountEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillAmount" runat="server" Width="150px"  Text='<%# Bind("BillAmount") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBillAmountFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Gst Bill No" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGstBillNoEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGstBillNo" runat="server" Width="150px" Text='<%# Bind("GstBillNo") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtGstBillNoFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>
                                        



                                          <asp:TemplateField HeaderText="Gst Amount" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGstAmountEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGstAmount" runat="server" Width="150px" Text='<%# Bind("GstAmount") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtGstAmountFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="Total Amount" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtTotalAmountEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblTotalAmount" runat="server" Width="150px" Text='<%# Bind("TotalAmount") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtTotalAmountFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                        <asp:TemplateField HeaderText="Bill Balance" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtBillBalanceEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblBillBalance" runat="server" Width="150px" Text='<%# Bind("BillBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtBillBalanceFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                        <asp:TemplateField HeaderText="GST Balance" SortExpression="CostCenter">
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtGSTBalanceEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblGSTBalance" runat="server" Width="150px" Text='<%# Bind("GstBalance") %>'></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtGSTBalanceFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                         <asp:TemplateField HeaderText="PO/GRN No." SortExpression="Project">
                                              <EditItemTemplate>
                                                <asp:TextBox ID="txtPoGRNEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblPoGRN" runat="server" Width="150px" Text='<%# Bind("PoGRn") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtPoGRNFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>




                                         <asp:TemplateField HeaderText="Status" SortExpression="Project">
                                              <EditItemTemplate>
                                                <asp:TextBox ID="txtStatusEdit" Width="100px" runat="server"></asp:TextBox>
                                             </EditItemTemplate>
                                            <ItemTemplate>
                                                <asp:Label ID="lblStatus" runat="server" Width="150px" Text='<%# Bind("Status") %>' ></asp:Label>
                                            </ItemTemplate>
                                            <FooterTemplate>
                                                <asp:TextBox ID="txtStatusFooter" Width="100px" runat="server"></asp:TextBox>
                                            </FooterTemplate>
                                        </asp:TemplateField>


                                       <asp:TemplateField HeaderText="Buttons" HeaderStyle-ForeColor="#000080" >
                                            <ItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                                            </EditItemTemplate>
                                            <FooterTemplate>
                                                <asp:ImageButton ImageUrl="~/Images123/addnew.png" runat="server" CommandName="AddNew" ToolTip="Add New" Width="20px" Height="20px" />
                                            </FooterTemplate>
                                        </asp:TemplateField>



                                    </Columns>

                                    <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                    <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                    <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />
                                </asp:GridView>
                                 
                                 
                                    
                                <%--  <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [Sno], [ItemSubHeadName], [Description], [UOM], [CurrentStock], [RequiredQuantity], [Supplier] FROM [View_StockReq]"></asp:SqlDataSource>--%>
                            </div>
                        </div>
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
                            <td class="rowpadd"><strong>Account Code</strong> </td>
                            <td class="rowpadd">
                       <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboBoxProduct" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="RadComboBoxProduct_ItemsRequested"
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
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="RadComboBoxProduct"
                        Display="Dynamic" ErrorMessage="!" CssClass="validator">
                    </asp:RequiredFieldValidator>--%>
                        

                               
                            </td>
                        </tr>

                
                        
                          <tr>

                            <td class="rowpadd"><strong>GL Date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEdit_GlDate" CssClass="form-control texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup1" TargetControlID="txtEdit_GlDate" Format="dd-MMM-yyyy" runat="server" />
                                        <%--<asp:RequiredFieldValidator runat="server" ID="req5" ControlToValidate="txtEdit_GlDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="req5" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="imgPopup1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>



                                </div>

                            </td>


                        </tr>
                        <tr>

                            <td class="rowpadd"><strong>Openning Value</strong> </td>
                            <td class="rowpadd">
                                <asp:TextBox ID="txtEdit_OpenValue" CssClass="form-control gap" runat="server"></asp:TextBox>


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
                            <td class="rowpadd"><strong>Account Code</strong> </td>
                            <td class="rowpadd">
                                <%--<asp:DropDownList ID="ddlFind_AcctCode" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                    
                        <telerik:RadComboBox RenderMode="Lightweight" ID="cmbFind_Accountcode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="ddlFind_AcctCode_ItemsRequested"
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
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="cmbFind_Accountcode"
                        Display="Dynamic" ErrorMessage="!" CssClass="validator">
                    </asp:RequiredFieldValidator>--%>

                            </td>
                        </tr>

                        <tr>

                            <td class="rowpadd"><strong>GL Date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_GlDate" CssClass="form-control texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="imgPopup2" TargetControlID="txtFind_GlDate" Format="dd-MMM-yyyy" runat="server" />
                                        <%--<asp:RequiredFieldValidator runat="server" ID="req6" ControlToValidate="txtFind_GlDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="req6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="imgPopup2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>



                                </div>

                            </td>


                        </tr>
                        <tr>

                            <td class="rowpadd"><strong>Openning Value</strong> </td>
                            <td class="rowpadd">
                                <asp:TextBox ID="txtFind_openValue" CssClass="form-control gap" runat="server"></asp:TextBox>


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
                            <td class="rowpadd"><strong>Account Code</strong> </td>
                            <td class="rowpadd">
                                <%--<asp:DropDownList ID="ddlDel_AcctCode" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>

                          <telerik:RadComboBox RenderMode="Lightweight" ID="cmbDel_AcctCode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="cmbDel_AcctCode_ItemsRequested"
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
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="cmbDel_AcctCode"
                        Display="Dynamic" ErrorMessage="!" CssClass="validator">
                    </asp:RequiredFieldValidator>--%>

                            </td>
                        </tr>

                        <tr>

                            <td class="rowpadd"><strong>GL Date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm" >
                                        <asp:TextBox ID="txtDel_GlDate" CssClass="form-control texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="imgPopup3" TargetControlID="txtDel_GlDate" Format="dd-MMM-yyyy" runat="server" />
                                        <%--<asp:RequiredFieldValidator runat="server" ID="req7" ControlToValidate="txtDel_GlDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="req7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                        <span class="input-group-btn">
                                            <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                            <asp:ImageButton ID="imgPopup3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>



                                </div>

                            </td>


                        </tr>
                        <tr>

                            <td class="rowpadd"><strong>Openning Value</strong> </td>
                            <td class="rowpadd">
                                <asp:TextBox ID="txtDel_OpenValue" CssClass="form-control gap" runat="server"></asp:TextBox>


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

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

    <asp:Panel id="panelhidde" runat="server" Style="display: none">
    <asp:Button ID="Button2" runat="server" Text="Button" OnClick="Button2_Click" />
    <asp:Button ID="Button3" runat="server" Text="Button" OnClick="Button3_Click" />
         </asp:Panel>

</asp:Content>


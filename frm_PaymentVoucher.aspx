<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_PaymentVoucher.aspx.cs" Inherits="frm_Period"  EnableEventValidation="false"%>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">

    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_Set_Department.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });

        }

        function alertEmail() {
            swal("Email Send Successfully!", "Email has been Sent to all suppplier!!!", "success")
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

        function DetEditalert() {
            swal("Update Successfuly!", " Detail record has been saved!!!", "success")
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

        .modalBackground {
            background-color: Black;
            filter: alpha(opacity=90);
            opacity: 0.8;
        }

        .modalPopup {
            background-color: #fff;
            border: 3px solid #ccc;
            padding: 10px;
            width: 300px;
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

        .rdbgap {
            padding-right: 30px;
        }

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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Payment Voucher</strong></h3>
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
                            <asp:LinkButton ID="btnDelete_after" OnClientClick="return sweetAlertConfirm(this);" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnDelete_Click">
                     <i class="fas fa-trash"></i>&nbsp Delete
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnFind" Height="40" CssClass="btn btn-app " runat="server" data-toggle="modal" data-target="#modal_find" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-search"></i>&nbsp Find
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnCancel" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnCancel_Click">
                     <i class="fas fa-remove"></i>&nbsp Cancel
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnUpdatePO" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;">
                     <i class="fas fa-remove"></i>&nbsp Update PO
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnShowJV" Height="40" CssClass="btn btn-app" runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnShowJV_Click">
                     <i class="fas fa-remove"></i>&nbsp Show JV
                            </asp:LinkButton>
                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">

                                <table id="tablecontent1" runat="server" style="width: 100%;">
                                    <tr>
                                        <td class="auto-style6"><strong>Document Name</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlDocName" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" OnSelectedIndexChanged="ddlDocName_SelectedIndexChanged" AutoPostBack="true"></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlDocName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>


                                        </td>
                                        <td class="auto-style6"><strong>Payment Date</strong></td>
                                        <td class="auto-style11">

                                            <div style="display: inline-block">

                                                <div class="input-group input-group-sm">
                                                    <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                    <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender11" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                    <span class="input-group-btn">

                                                        <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                    </span>
                                                </div>

                                            </div>

                                        </td>
                                        <td class="auto-style6"><strong>Voucher No</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtVoucherNo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator8" ControlToValidate="txtVoucherNo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender10" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                    </tr>
                                    <tr>
                                        <td class="auto-style6"><strong>Payment Type</strong></td>
                                        <td class="auto-style11">
                                            
                                            <asp:RadioButton ID="chkBillPayment" Text="Bill Payment" runat="server"  AutoPostBack="true" GroupName="GRPODI"  />

                                            <asp:RadioButton ID="chkNormalPayment" Text="Normal Payment" runat="server" AutoPostBack="True"    GroupName="GRPODI" />

                                           
                                        </td>

                                        <td class="auto-style6"><strong>B.Partner Type</strong></td>
                                        <td class="auto-style11">
                                            <asp:Label ID="lblDrCode" runat="server" Text="" Visible="false"></asp:Label>
                                            <asp:DropDownList ID="ddlbpType" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlbpType_SelectedIndexChanged"  ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="ddlbpType" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                        <td class="auto-style6"><strong>Business Partner</strong></td>
                                        <td class="auto-style11">
                                          
                                              <asp:DropDownList ID="ddlBusinessPartner" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlBusinessPartner_SelectedIndexChanged" ></asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="ddlBusinessPartner" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                         
                                        </td>
                                    </tr>

                                    <tr>
                                        <td class="auto-style6"><strong>Paid To</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtPaidTo" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtPaidTo" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                        <td class="auto-style6"><strong>Document Autority</strong></td>
                                        <td class="auto-style11">
                                             <asp:DropDownList ID="ddlDocAuth" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="ddlDocAuth" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select document Authority." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                         <td class="auto-style6"><strong>PO NO.</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtPoNO" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtPoNO" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                       

                                    </tr>

                                    <tr>
                                        <td class="auto-style6"><strong>Account Code</strong></td>
                                        
                                              <td class="auto-style11" >
                                                  <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%><%-- gf_Sno --%>
                                            <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_Code" runat="server" Height="200" Width="250"
                                                DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                                EnableLoadOnDemand="true" Filter="StartsWith"
                                                AutoPostBack="true"
                                                OnSelectedIndexChanged="RadComboAcct_Code_SelectedIndexChanged"
                                                OnItemsRequested="RadComboAcct_Code_ItemsRequested"
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
                                        </td>
                                            
                                   

                                        <td class="auto-style6"><strong>CNIC/ NTN No.</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtNtn" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator10" ControlToValidate="txtNtn" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender9" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                        <td class="auto-style6"><strong>GST No.</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtGST" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtGST" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender12" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                    </tr>
                                    <tr>
                                          <td class="auto-style6"><strong>Active</strong></td>
                                        <td class="auto-style11">

                                            <asp:Panel ID="Panel8" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            </asp:Panel>
                                        </td>

                                        <td class="auto-style6"><strong>Description</strong></td>
                                        <td class="auto-style11" colspan="3">

                                            <asp:TextBox ID="txtDescription" CssClass="texthieht" Width="770px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator13" ControlToValidate="txtDescription" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender14" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                      
                                    </tr>

                                       <tr>
                                          <td class="auto-style6"><strong>Create Date</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtCreateBy" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator14" ControlToValidate="txtCreateBy" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender15" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>

                                        <td class="auto-style6"><strong>Update Date</strong></td>
                                        <td class="auto-style11">

                                            <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator15" ControlToValidate="txtUpdateDate" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender16" TargetControlID="RequiredFieldValidator8" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                            
                                           <td class="auto-style6"><strong>Status</strong></td>
                                        <td class="auto-style11">

                                            <asp:DropDownList ID="ddlStatus" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="ddlStatus" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Status." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator15" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                      
                                    </tr>
                                    <tr>
                                       
                                            <td class="auto-style6"><strong>Document Type</strong></td>
                                            <td class="auto-style11">
                                             <asp:DropDownList ID="ddlDocType" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator9" ControlToValidate="ddlDocAuth" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select document Type." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                        </td>
                                    </tr>
                                </table>


                            </div>

                            <asp:Label ID="lbltotalvaldetail" runat="server" Text="Label"></asp:Label>

                            <%-- gf_Sno --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno">
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" SortExpression="VoucherNo"></asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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

                            <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>



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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" SortExpression="VoucherNo"></asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                        <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno"></asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="Document" SortExpression="DocDescription"></asp:BoundField>
                                                    <asp:BoundField DataField="VoucherNo" HeaderText="Voucher No" SortExpression="VoucherNo"></asp:BoundField>
                                                    <asp:BoundField DataField="StatusName" HeaderText="Status" SortExpression="StatusName" />
                                                    <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString="{0:dd-MMM-yyyy}" />

                                                    <asp:CheckBoxField DataField="IsActive" HeaderText="IsActive" SortExpression="IsActive"></asp:CheckBoxField>
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
                <asp:Button ID="btnFetchData" runat="server" Text="Fetch data" OnClick="btnFetchData_Click" />

                   <div>

                       <div id="Div4" runat="server">
                                            <div class="box">

                                                <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                                    <div style="width: 100%; height: 200px; overflow: scroll">

                                                        <asp:GridView ID="GridView7" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowHeaderWhenEmpty="True" OnRowDataBound="GridView7_RowDataBound" OnRowCancelingEdit="GridView7_RowCancelingEdit" OnRowDeleting="GridView7_RowDeleting" OnRowEditing="GridView7_RowEditing" OnRowUpdating="GridView7_RowUpdating">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sno"  Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" runat="server" />
                                                                    </ItemTemplate>

                                                                    <ItemStyle Width="20px"></ItemStyle>
                                                                </asp:TemplateField>
                                                               
                                                                 <asp:TemplateField HeaderText="Master Sno" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtlblMSnoEdit" runat="server" Enabled="true" Text='<%# Bind("Sno") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbMSno" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Detail Sno" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtlblDSnoEdit" runat="server" Enabled="false" Text='<%# Bind("PurDetail") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbDSno" runat="server" Text='<%# Bind("PurDetail") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="CrAcct" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtCrAcctEdit" runat="server" Enabled="false" Text='<%# Bind("CrAccountCode") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCrAcct" runat="server" Text='<%# Bind("CrAccountCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="DrAcct" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtDrAcctEdit" runat="server" Enabled="false" Text='<%# Bind("DrAccountCode") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbDrAcct" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Bill No." SortExpression="BillNo">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillNoEdit" runat="server" Enabled="true" Text='<%# Bind("BillNo") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Bill Date" SortExpression="BillDate">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillDateEdit" runat="server" Enabled="true" Text='<%# Bind("BillDate","{0:dd-MMM-yyyy}") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GL Date" SortExpression="GLDate">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGLDateEdit" runat="server" Enabled="true" Text='<%# Bind("GLDATE") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGLDate" runat="server" Text='<%# Bind("GLDATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Supplier/Contractor" SortExpression="BPartnerName">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSupplierEdit" runat="server" Enabled="false" Text='<%# Bind("BPartnerName") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("BPartnerName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GST Claimable" SortExpression="gstclaim">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGSTClaimableEdit" runat="server" Enabled="true" Text='<%# Bind("gstclaim") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTClaimable" runat="server" Text='<%# Bind("gstclaim") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bill Payable" SortExpression="BalanceAmount">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillPayableEdit" runat="server" Enabled="true" Text='<%# Bind("BalanceAmount") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillPayable" runat="server" Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="" SortExpression="CheckBOX" Visible="false">
                                                                    <EditItemTemplate>
                                                                         <asp:CheckBox ID="chkEdit" runat="server" />
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk" runat="server" />
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Approved Amount" SortExpression="ApprovedAmount">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtApprovedAmountEdit" runat="server" Enabled="true" Text='<%# Bind("ApprovedAmount") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprovedAmount" runat="server" Text='<%# Bind("ApprovedAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="GST Balance" SortExpression="BalanceTax">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGSTBalanceEdit" runat="server" Enabled="true" Text='<%# Bind("BalanceTax") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTBalance" runat="server" Text='<%# Bind("BalanceTax") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bill Balance" SortExpression="BillBalance">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillBalanceEdit" runat="server" Enabled="true" Text='<%# Bind("BillBalance") %>'></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillBalanceBalanceHide" runat="server" Visible="false" Text='<%# Bind("BillBalance") %>'></asp:Label>
                                                                        <asp:Label ID="lblBillBalanceBalance" runat="server" Text='<%# Bind("BillBalance") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField>
                                                                    <ItemTemplate>
                                                                        <asp:ImageButton ImageUrl="~/Images123/edit.png" runat="server" CommandName="Edit" ToolTip="Edit" Width="20px" Height="20px" />
                                                                        <asp:ImageButton ImageUrl="~/Images123/delete.png" runat="server" CommandName="Delete" ToolTip="Delete" Width="20px" Height="20px" />
                                                                    </ItemTemplate>
                                                                    <EditItemTemplate>
                                                                        <asp:ImageButton ImageUrl="~/Images123/save.png" runat="server" CommandName="Update" ToolTip="Update" Width="20px" Height="20px" />
                                                                        <asp:ImageButton ImageUrl="~/Images123/cancel.png" runat="server" CommandName="Cancel" ToolTip="Cancel" Width="20px" Height="20px" />
                                                                    </EditItemTemplate>
                                                                   
                                                                </asp:TemplateField>



                                                            </Columns>

                                                            <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                            <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" Height="5px" />
                                                            <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                                        </asp:GridView>

                                                        <asp:SqlDataSource ID="SqlDataSource1" runat="server"></asp:SqlDataSource>

                                                    </div>
                                                </div>

                                            </div>

                                        </div>


                       <div id="Div7" runat="server">
                           <div class="box">



                               <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                   <%-- gf_Sno --%>

                                   <div style="width: 100%; height: 130px; overflow: scroll">

                                       <asp:GridView ID="GridView10" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" ShowFooter="true" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" ShowHeaderWhenEmpty="true" OnRowDataBound="GridView10_RowDataBound" OnRowEditing="GridView10_RowEditing" OnRowUpdating="GridView10_RowUpdating" OnRowCommand="GridView10_RowCommand">
                                           <Columns>

                                               <%-- gf_Sno --%>
                                               <asp:TemplateField HeaderText="Sno" Visible="true" ItemStyle-Width="10">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRowNumber" runat="server" />
                                                   </ItemTemplate>

                                                   <ItemStyle Width="10px"></ItemStyle>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                       <asp:DropDownList ID="SNODetail" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                       <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblSno" runat="server" Text="1"></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="A/c code" Visible="true" SortExpression="CrAccountCodeToDr">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("CrAccountCodeToDr") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                          <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_CodeFooter" runat="server" Height="200" Width="150"
                                                    DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                                                    EnableLoadOnDemand="true" Filter="StartsWith"
                                                    OnItemsRequested="RadComboAcct_CodeFooter_ItemsRequested10"
                                                    Enabled="true"
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
                                                   </FooterTemplate>
                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Dr/Cr" SortExpression="ItemSubHeadName">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lbldr_CR" runat="server"  Text='<%# Bind("dr_CR_f") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                    <%--   <asp:TextBox ID="txtdr_CR_f" runat="server"  Text=""></asp:TextBox>--%>

                                                       <asp:DropDownList ID="ddlDr_CR" CssClass="form-control select2" Width="80px" runat="server">
                                                          <asp:ListItem Value="0">Please select...</asp:ListItem>
                                                           <asp:ListItem Value="1">Debit</asp:ListItem>
                                                           <asp:ListItem Value="0">Credit</asp:ListItem>
                                                       </asp:DropDownList>



                                                   </FooterTemplate>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Bill No" SortExpression="BillNo">

                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBillNo" runat="server" Width="100px" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtdrBillNo_f" runat="server"  Text=""></asp:TextBox>
                                                   </FooterTemplate>

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Bill Date" SortExpression="BillDate">
                                                   <ItemTemplate>
                                                   <%--<asp:Calendar ID="Calendar1" Width="100px" Text='<%# Bind("BillDate","{0:dd-MMM-yyyy}") %>' runat="server"></asp:Calendar>--%>
                                                       <asp:Label ID="lblBillDate" runat="server" Width="100px" Text='<%# Bind("BillDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                   </ItemTemplate>
                                                  <FooterTemplate>
                                                      <asp:TextBox ID="txtbilldate_f" runat="server"  Text=""></asp:TextBox>
                                                  </FooterTemplate>

                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="Item" SortExpression="UOM">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlItemMasterFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlitemMaster_SelectedIndexChanged"></asp:DropDownList>
                                                   </FooterTemplate>
                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblUOM" runat="server" Width="70px" Text='<%# Bind("UOMName") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlUOMFooter" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                   </FooterTemplate>
                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Qty" SortExpression="RequiredQuantity" Visible="true" ItemStyle-HorizontalAlign="Right">
                                                   <EditItemTemplate>
                                                       <asp:Label ID="lblDCQtyEdit" runat="server" Width="50px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblDCQty" runat="server" Width="50px" Text='<%# Bind("Quantity") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtDCQtyFooter" Width="70px" runat="server" OnTextChanged="txtDCQtyFooter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Rate" SortExpression="Rate" ItemStyle-HorizontalAlign="Right">

                                                   <EditItemTemplate>
                                                       <asp:TextBox ID="lblRateEdit" runat="server" Width="100px" Text='<%# Bind("Rate") %>' AutoPostBack="true" OnTextChanged="lblRateEdit_TextChanged" ></asp:TextBox>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRate" runat="server" Width="100px" Text='<%# Bind("Rate") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtRateFooter" Width="100px" runat="server" OnTextChanged="txtRateFooter_TextChanged" AutoPostBack="true"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Amount" SortExpression="Value" ItemStyle-HorizontalAlign="Right">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblValue" runat="server" Width="100px" Text='<%# Bind("Value") %>'></asp:Label>

                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtAmount_footer" runat="server" Font-Bold="true" Text=""></asp:TextBox>
                                                   </FooterTemplate>

                                                   <ItemStyle HorizontalAlign="Right" />
                                                   <FooterStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Narration" SortExpression="Project">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Remarks") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtnarration_footer" runat="server" Font-Bold="true" Text=""></asp:TextBox>
                                                   </FooterTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Store_Department") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="lblLocationFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                   </FooterTemplate>
                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlCostCenterFooter" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                   </FooterTemplate>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <FooterTemplate>
                                                        <asp:TextBox ID="lblProjectFooter" Width="100px" runat="server"></asp:TextBox>
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
                                           

                                            <HeaderStyle ForeColor="Navy"></HeaderStyle>
                                        </asp:TemplateField>



                                           </Columns>
                                           <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                           <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                           <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                       </asp:GridView>








                                   </div>

                               </div>
                               <!-- /.box-body -->
                               <asp:Label ID="lblGrid10_Err" runat="server" Text=""></asp:Label>
                           </div>

                       </div>
                      
                       <div id="Div1" runat="server">
                           <div class="box">



                               <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                   <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>

                                   <div style="width: 100%; height: 130px; overflow: scroll">

                                       <asp:GridView ID="GridView4" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true" ShowFooter="true" OnRowCommand="GridView4_RowCommand" OnRowDataBound="GridView4_RowDataBound" OnRowEditing="GridView4_RowEditing" OnRowCancelingEdit="GridView4_RowCancelingEdit" OnRowDeleting="GridView4_RowDeleting" OnRowUpdating="GridView4_RowUpdating">
                                           <Columns>

                                               <%-- gf_Sno --%>
                                               <asp:TemplateField HeaderText="Sno" Visible="true" ItemStyle-Width="10">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRowNumber" runat="server" />
                                                   </ItemTemplate>

                                                   <ItemStyle Width="10px"></ItemStyle>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                       <asp:DropDownList ID="SNODetail" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                       <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblSno" runat="server" ></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Bank" Visible="true" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlBankID_Edit" CssClass="select2" runat="server" DataTextField="BankName" DataValueField="BankID" Width="125px">
                                                       </asp:DropDownList>
                                                       <asp:Label ID="lblBankNameE" runat="server" Enabled="false" Text='<%# Bind("Bank") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlBankID_Footer" CssClass="select2" runat="server" DataTextField="BankName" DataValueField="BankID" Width="125px" AutoPostBack="true" OnSelectedIndexChanged="ddlBankID_Footer_SelectedIndexChanged">
                                                       </asp:DropDownList>
                                                      
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("Bank") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Branch" SortExpression="ItemSubHeadName">
                                                   <EditItemTemplate>
                                                        <asp:DropDownList ID="ddlBankBranch_Edit" CssClass="select2" runat="server" DataTextField="BankBranchName" DataValueField="BankBranchID">
                                                       </asp:DropDownList>
                                                       <asp:Label ID="lblbankBranchEdit" runat="server" Enabled="false" Text='<%# Bind("Branch") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlBankBranch_Footer" CssClass="select2" runat="server" DataTextField="BankBranchName" DataValueField="BankBranchID">
                                                       </asp:DropDownList>
                                                     
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("Branch") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>

                                             
                                               <asp:TemplateField HeaderText="Account No." SortExpression="Description">

                                                   <FooterTemplate>
                                                        <asp:DropDownList ID="ddlAcctNo_Footer" CssClass="select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAcctNo_Footer_SelectedIndexChanged"></asp:DropDownList>
                                                   </FooterTemplate>

                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAccountNo" runat="server" Width="100px" Text='<%# Bind("AccountNo") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <EditItemTemplate>

                                                       <asp:DropDownList ID="ddlAcctNo_Edit" CssClass="select2" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlAcctNo_Footer_SelectedIndexChanged"></asp:DropDownList>
                                                       <asp:Label ID="lblAcctNoEdit" runat="server" Enabled="true" Text='<%# Bind("AccountNo") %>'></asp:Label>
                                                   </EditItemTemplate>

                                               </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="A/c Code" SortExpression="Description">

                                                   <FooterTemplate>
                                                       <telerik:RadComboBox ID="RadComboAcct_CodeFooter" runat="server" DropDownWidth="500" EmptyMessage="Choose an Account Code" Enabled="true" EnableLoadOnDemand="true" Filter="StartsWith" Height="200" HighlightTemplatedItems="true" Label="" OnItemsRequested="RadComboAcct_CodeFooter_ItemsRequested" RenderMode="Lightweight" Width="150">
                                                           <HeaderTemplate>
                                                               <table cellpadding="0" cellspacing="0" style="width: 100%">
                                                                   <tr>
                                                                       <td style="width: 60px;">Acct Code </td>
                                                                       <td style="width: 175px;">Account Description </td>
                                                                       <td style="width: 40px;">Acct Type </td>
                                                                       <td style="width: 40px;">Tran Type </td>
                                                                   </tr>
                                                               </table>
                                                           </HeaderTemplate>
                                                           <ItemTemplate>
                                                               <table cellpadding="0" cellspacing="0" style="width: 500px">
                                                                   <tr>
                                                                       <td style="width: 60px;"><%# DataBinder.Eval(Container, "Text")%></td>
                                                                       <td style="width: 175px;"><%# DataBinder.Eval(Container, "Attributes['Acct_Description']")%></td>
                                                                       <td style="width: 60px;"><%# DataBinder.Eval(Container, "Attributes['Acct_Type_Name']")%></td>
                                                                       <td style="width: 60px;"><%# DataBinder.Eval(Container, "Attributes['Tran_Type']")%></td>
                                                                   </tr>
                                                               </table>
                                                           </ItemTemplate>
                                                       </telerik:RadComboBox>
                                                   </FooterTemplate>

                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAcctCode" runat="server" Width="100px" Text='<%# Bind("AcctCode") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <EditItemTemplate>

                                                       <%--<asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblAcctCode_Edit" runat="server" Enabled="true" Text='<%# Bind("AcctCode") %>'></asp:Label>
                                                   </EditItemTemplate>

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Pay Through" SortExpression="UOM">
                                                   <EditItemTemplate>
                                                       <asp:DropDownList ID="ddlpay_Edit" runat="server" CssClass="select2" DataSourceID="SqlDataSource4" DataTextField="PaymentModeDescription" Width="100px" DataValueField="PaymentModeId">
                                                       </asp:DropDownList>
                                                       <asp:SqlDataSource ID="SqlDataSource4" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [PaymentModeId], [PaymentModeDescription] FROM [Set_PaymentMode]"></asp:SqlDataSource>
                                                       <asp:Label ID="lblItemMaster" runat="server"  Enabled="false"></asp:Label>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:DropDownList ID="ddlpay_Footer" runat="server" CssClass="select2" DataSourceID="SqlDataSource3" DataTextField="PaymentModeDescription" DataValueField="PaymentModeId">
                                                       </asp:DropDownList>
                                                       <asp:SqlDataSource ID="SqlDataSource3" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [PaymentModeId], [PaymentModeDescription] FROM [Set_PaymentMode]"></asp:SqlDataSource>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblPaythrough" runat="server" Text='<%# Bind("Pay") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="Instrument No." SortExpression="CurrentStock">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlUOMEdit" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:TextBox ID="lblInstrumentNoEdit" runat="server" Width="70px" Text='<%# Bind("InstrumentNo") %>'></asp:TextBox>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtInsNo_Footer"  Width="100px" runat="server"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblInstrumentNo" runat="server" Width="70px" Text='<%# Bind("InstrumentNo") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Instrument Date" SortExpression="RequiredQuantity" Visible="true" >
                                                   <EditItemTemplate>
                                                       <asp:TextBox ID="lblInstrument_Date_Edit" runat="server" Width="50px" Text='<%# Bind("InstrumentDate") %>'></asp:TextBox>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtINSDate_Footer" runat="server" Width="100px"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblInstrument_Date" runat="server" Width="50px" Text='<%# Bind("InstrumentDate") %>'></asp:Label>
                                                   </ItemTemplate>

                                                 

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Narration" SortExpression="Project">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <%--<asp:TextBox ID="txtLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>' Width="80px"></asp:TextBox>--%>
                                                       <asp:TextBox ID="lblNarrationEdit" runat="server" Visible="true" Text='<%# Bind("Narration") %>' ></asp:TextBox>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtNarration_Footer" runat="server"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Narration") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Amount" SortExpression="Project">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <%--<asp:TextBox ID="txtLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>' Width="80px"></asp:TextBox>--%>
                                                       <asp:Label ID="lblAmountEditr" runat="server" Visible="true"  Text='<%# Bind("Amount") %>'></asp:Label>
                                                       <asp:TextBox ID="lblAmountEdit" runat="server" Visible="true"  Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                   </EditItemTemplate>
                                                   <FooterTemplate>
                                                       <asp:TextBox ID="txtAmount_Footer" runat="server" Width="100px"></asp:TextBox>
                                                   </FooterTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAmount" runat="server"  Text='<%# Bind("Amount") %>'></asp:Label>
                                                   </ItemTemplate>

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

                                            <HeaderStyle ForeColor="Navy"></HeaderStyle>
                                        </asp:TemplateField>

                                              
                                           </Columns>
                                           <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                           <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                           <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                       </asp:GridView>








                                   </div>

                               </div>
                               <!-- /.box-body -->
                           </div>

                       </div>


                       <div id="Div2" runat="server">
                           <div class="box">



                               <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                   <%-- gf_Sno --%>

                                   <div style="width: 100%; height: 130px; overflow: scroll">

                                       <asp:GridView ID="GridView5" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" DataKeyNames="Sno" ShowHeaderWhenEmpty="true" OnRowDataBound="GridView5_RowDataBound">
                                           <Columns>

                                               <%-- gf_Sno --%>
                                               <asp:TemplateField HeaderText="Sno" Visible="true" ItemStyle-Width="10">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRowNumber" runat="server" />
                                                   </ItemTemplate>

                                                   <ItemStyle Width="10px"></ItemStyle>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                       <asp:DropDownList ID="SNODetail" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>
                                                       <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblSno" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="A/c code" Visible="true" SortExpression="CrAccountCodeToDr">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAccountCode" runat="server" Text='<%# Bind("CrAccountCodeToDr") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Dr/Cr" SortExpression="ItemSubHeadName">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lbldr_CR" runat="server"  Text='<%# Bind("CrAccountCodeToDr") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Bill No" SortExpression="BillNo">

                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBillNo" runat="server" Width="100px" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Bill Date" SortExpression="BillDate">

                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBillDate" runat="server" Width="100px" Text='<%# Bind("BillDate","{0:dd-MMM-yyyy}") %>'>></asp:Label>
                                                   </ItemTemplate>
                                                  

                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="Item" SortExpression="UOM">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblItemMaster" runat="server" Text='<%# Bind("Description") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="UOM" SortExpression="CurrentStock">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblUOM" runat="server" Width="70px" Text='<%# Bind("UOMName") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Qty" SortExpression="RequiredQuantity" Visible="true" ItemStyle-HorizontalAlign="Right">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblDCQty" runat="server" Width="50px" Text='<%# Bind("Quantity") %>'>></asp:Label>
                                                   </ItemTemplate>

                                                   <ItemStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Rate" SortExpression="Rate" ItemStyle-HorizontalAlign="Right">

                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRate" runat="server" Width="100px" Text='<%# Bind("Rate") %>'>></asp:Label>
                                                   </ItemTemplate>

                                                   <ItemStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Amount" SortExpression="Value" ItemStyle-HorizontalAlign="Right">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblValue" runat="server" Width="100px" Text='<%# Bind("Value") %>'>></asp:Label>

                                                   </ItemTemplate>

                                                   <ItemStyle HorizontalAlign="Right" />

                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Narration" SortExpression="Project">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Remarks") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Location" SortExpression="Project">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblLocation" runat="server" Text='<%# Bind("Store_Department") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Cost Center" SortExpression="Project">
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblCostCenter" runat="server" Text='<%# Bind("CostCenter") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="Project/Job/Batch No." SortExpression="Project">
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblProject" runat="server" Text='<%# Bind("Project") %>'>></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>

                                           </Columns>
                                           <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                           <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                           <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                       </asp:GridView>








                                   </div>

                               </div>
                               <!-- /.box-body -->
                               <asp:Label ID="Label2" runat="server" Text=""></asp:Label>
                           </div>

                       </div>
                                


                         <div id="Div3" runat="server">
                           <div class="box">



                               <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                   <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>

                                   <div style="width: 100%; height: 130px; overflow: scroll">

                                       <asp:GridView ID="GridView6" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" runat="server" ShowHeaderWhenEmpty="true" ShowHeader="true"  OnRowDataBound="GridView4_RowDataBound" OnRowEditing="GridView6_RowEditing" OnRowCancelingEdit="GridView6_RowCancelingEdit" OnRowDeleting="GridView6_RowDeleting" OnRowUpdating="GridView6_RowUpdating">
                                           <Columns>

                                               <%-- gf_Sno --%>
                                               <asp:TemplateField HeaderText="Sno" Visible="true" ItemStyle-Width="10">
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblRowNumber" runat="server" />
                                                   </ItemTemplate>

                                                   <ItemStyle Width="10px"></ItemStyle>
                                               </asp:TemplateField>

                                               <asp:TemplateField HeaderText="SNODetail" Visible="false" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                       <asp:Label ID="SNODetail" Width="150px" Text='<%# Bind("Sno") %>' runat="server"></asp:Label>
                                                       <%--<asp:Label ID="lblAcctCode" runat="server" Enabled="false" Text='<%# Bind("Accode") %>'></asp:Label>--%>
                                                   </EditItemTemplate>
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblSno" runat="server" ></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Bank" Visible="true" SortExpression="A/c code">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblBankNameE" runat="server" Enabled="false" Text='<%# Bind("Bank") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBankName" runat="server" Text='<%# Bind("Bank") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Branch" SortExpression="ItemSubHeadName">
                                                   <EditItemTemplate>
                                                       <asp:Label ID="lblBranchEdit" runat="server" Enabled="false" Text='<%# Bind("BankBranch") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblBranch" runat="server" Text='<%# Bind("BankBranch") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>

                                             
                                               <asp:TemplateField HeaderText="Account No." SortExpression="Description">

                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAccountNo" runat="server" Width="100px" Text='<%# Bind("AccountNo") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <EditItemTemplate>

                                                       <%--<asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblAcountNoEdit" runat="server" Enabled="true" Width="100px" Text='<%# Bind("AccountNo") %>'></asp:Label>
                                                   </EditItemTemplate>

                                               </asp:TemplateField>

                                                 <asp:TemplateField HeaderText="A/c Code" SortExpression="Description">

                                              
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAcctCode" runat="server" Width="100px" Text='<%# Bind("AccountCode") %>'></asp:Label>
                                                   </ItemTemplate>
                                                   <EditItemTemplate>

                                                       <%--<asp:DropDownList ID="ddlItemSubHeadEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblAcctCodeEdit" runat="server" Enabled="true"  Width="100px" Text='<%# Bind("AccountCode") %>'></asp:Label>
                                                   </EditItemTemplate>

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Pay Through" SortExpression="UOM">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlItemMasterEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblPaythroughEdit" runat="server" Width="250px" Enabled="false" Text='<%# Bind("PayThrough") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblPaythrough" runat="server" Text='<%# Bind("PayThrough") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>




                                               <asp:TemplateField HeaderText="Instrument No." SortExpression="CurrentStock">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlUOMEdit" Width="70px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <asp:Label ID="lblInstrumentNoEdit" runat="server" Width="70px" Text='<%# Bind("InstrumentNo") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblInstrumentNo" runat="server" Width="70px" Text='<%# Bind("InstrumentNo") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>



                                               <asp:TemplateField HeaderText="Instrument Date" SortExpression="RequiredQuantity" Visible="true" >
                                                   <EditItemTemplate>
                                                       <asp:Label ID="lblInstrument_DateEdit" runat="server" Width="80px" Text='<%# Bind("InstrumentDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblInstrument_Date" runat="server" Width="80px" Text='<%# Bind("InstrumentDate","{0:dd-MMM-yyyy}") %>'></asp:Label>
                                                   </ItemTemplate>

                                                 

                                               </asp:TemplateField>


                                               <asp:TemplateField HeaderText="Narration" SortExpression="Project">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <%--<asp:TextBox ID="txtLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>' Width="80px"></asp:TextBox>--%>
                                                       <asp:Label ID="lblNarrationEdit" runat="server" Visible="true" Enabled="false" Text='<%# Bind("Narration") %>'></asp:Label>
                                                   </EditItemTemplate>
                                                  
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblNarration" runat="server" Text='<%# Bind("Narration") %>'></asp:Label>
                                                   </ItemTemplate>

                                               </asp:TemplateField>
                                               <asp:TemplateField HeaderText="Amount" SortExpression="Project">
                                                   <EditItemTemplate>
                                                       <%--<asp:DropDownList ID="ddlLocationEdit" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%>
                                                       <%--<asp:TextBox ID="txtLocationEdit" runat="server" Enabled="false" Text='<%# Bind("LocName") %>' Width="80px"></asp:TextBox>--%>
                                                       <asp:TextBox ID="lblAmountEdit" runat="server" Visible="true"  Text='<%# Bind("Amount") %>'></asp:TextBox>
                                                   </EditItemTemplate>
                                                   
                                                   <ItemTemplate>
                                                       <asp:Label ID="lblAmount" runat="server"  Text='<%# Bind("Amount") %>'></asp:Label>
                                                   </ItemTemplate>

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

<HeaderStyle ForeColor="Navy"></HeaderStyle>
                                        </asp:TemplateField>

                                              
                                           </Columns>
                                           <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                           <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" BorderStyle="Solid" BorderWidth="1px" Wrap="True" Height="5px" />
                                           <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" Height="5px" />
                                       </asp:GridView>








                                   </div>

                               </div>
                               <!-- /.box-body -->
                           </div>

                       </div>
                            </div>

                
             
            </div>
           
            <br />
            
            <br />
           
        </div>

    </section>





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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEditEntrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtEditEntrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtEdit_deliveryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton3" TargetControlID="txtEdit_deliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Depart" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlEdit_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>AFQ/Requisition No</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlFind_AFQRequisitionNo" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_Entrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton4" TargetControlID="txtFind_Entrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtFind_DeliveryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" TargetControlID="txtFind_DeliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton5" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Department" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlFind_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkFind" runat="server" Checked="true" Text="Active" />
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
                            <td class="rowpadd"><strong>Document</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Document" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Doc NO</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_DocNO" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>AFQ/Requisition No</strong> </td>
                            <td class="rowpadd">
                                <asp:DropDownList ID="ddlDel_AFQRequisitionNo" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                </asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Entry date</strong> </td>
                            <td class="rowpadd">

                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDel_Entrydate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender7" PopupButtonID="ImageButton6" TargetControlID="txtEditEntrydate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton6" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Delivery date</strong> </td>
                            <td class="rowpadd">
                                <div style="display: inline-block">

                                    <div class="input-group input-group-sm">
                                        <asp:TextBox ID="txtDel_DelviDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender8" PopupButtonID="ImageButton7" TargetControlID="txtEdit_deliveryDate" Format="dd-MMM-yyyy" runat="server" />
                                        <span class="input-group-btn">
                                            <asp:ImageButton ID="ImageButton7" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        </span>
                                    </div>
                                </div>

                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Department</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Department" Width="100%" CssClass=" form-control select2" runat="server" ValidationGroup="aaa">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Status</strong> </td>
                            <td class="rowpadd">

                                <asp:DropDownList ID="ddlDel_Status" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>

                        <tr>
                            <td class="rowpadd"><strong>Active</strong> </td>
                            <td class="rowpadd">
                                <asp:CheckBox ID="chkDel" runat="server" Checked="true" Text="Active" />
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
    <%--<asp:DropDownList ID="ddlAccode" Width="150px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"></asp:DropDownList>--%><%# DataBinder.Eval(Container, "Text")%>




     <div class="modal fade" id="modal_pageFrame">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-label="Close">
                        <span aria-hidden="true">&times;</span></button>
                    <h4 class="modal-title">Select Payment</h4>
                </div>
                <div class="modal-body">

                    <iframe src="frm_Select_Bill_ForPayment.aspx?UserID=6b9c1166-0f4b-41dc-99e8-b47be96c8157&UsergrpID=ff43b221-f9e1-4423-aa61-f12880a9e13d&fiscaly=2020-2021&FormID=797acfee-34d4-469a-be60-53ba7bc21b11&Menuid=0b7b1f65-e6d3-45e5-881b-a61df68652d9" style="width:100%;height:500px"></iframe>
                    

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="Button1" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btnSearchEdit_Click" />

                </div>
            </div>
            <!-- /.modal-content -->
        </div>
        <!-- /.modal-dialog -->
    </div>


    
    

    <script src="https://unpkg.com/sweetalert/dist/sweetalert.min.js"></script>

</asp:Content>


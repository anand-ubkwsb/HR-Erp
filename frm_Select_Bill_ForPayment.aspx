<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Select_Bill_ForPayment.aspx.cs" Inherits="frm_Period" %>

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
        .auto-style1 {
            width: 118px;
           
        }
        .firstcol{
              background-color:gainsboro;
        }
        .headcolor{
             background-color:gainsboro;
             
        }
        .auto-style2 {
            width: 120px;
            border:1px solid #000;
        }
        .auto-style3 {
            width: 114px;
            border:1px solid #000;
        }
        .auto-style4 {
            width: 93px;
            border:1px solid #000;
        }
        .auto-style5 {
            width: 35px;
            border:1px solid #000;
        }
        .auto-style6 {
            height: 20px;
        }
        .auto-style7 {
            width: 120px;
            border: 1px solid #000;
            height: 20px;
        }
        .auto-style8 {
            width: 114px;
            border: 1px solid #000;
            height: 20px;
        }
        .auto-style9 {
            width: 93px;
            border: 1px solid #000;
            height: 20px;
        }
        .auto-style10 {
            width: 35px;
            border: 1px solid #000;
            height: 20px;
        }
        table tr td{
            padding: 0px 5px;
        }
        .auto-style11 {
            height: 41px;
        }
        .auto-style12 {
            width: 120px;
            border: 1px solid #000;
            height: 41px;
        }
        .auto-style13 {
            width: 114px;
            border: 1px solid #000;
            height: 41px;
        }
        .auto-style14 {
            width: 93px;
            border: 1px solid #000;
            height: 41px;
        }
        .auto-style15 {
            width: 35px;
            border: 1px solid #000;
            height: 41px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Select Bills for payment</strong></h3>
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
                        </div>

                    </div>


                    <div class="form-row">

                        <div class="box-body" style="background-color: #cddbf2;">
                            <div id="fieldbox" runat="server">
                                
                                        
                                        <table>
                                            <tr>
                                                <td style="padding-right:30px"><strong>Supplier / Contractor</strong></td>
                                                <td style="padding-right:30px">
                                                  <asp:DropDownList ID="ddlsupplier" CssClass="select2" Width="250px" runat="server"></asp:DropDownList></td>
                                                <td>
                                                    <asp:Button ID="btnShow" runat="server" Text="Show Data" OnClick="btnShow_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                        <br />

                                        <div class="row">
                                            <div class="col-md-6">

                                                <table style="background-color:white; border:1px solid #000;">
                                           
                                            
                                            <tr class="headcolor" style="border:1px solid #000;">
                                                <td class="auto-style1  "><strong>Period</strong></td>
                                                <td class="auto-style2"><strong>No.of Invoice</strong></td>
                                                <td class="auto-style3"><strong>GST Claimable</strong></td>
                                                <td class="auto-style4"><strong>Bill Payable</strong></td>
                                                <td class="auto-style5"><strong></strong>
                                                    <asp:CheckBox ID="CheckBox1" runat="server" />
                                                </td>
                                                <td><strong>Approved Payment</strong></td>
                                            </tr>
                                            <tr style="border:1px solid #000;">
                                                <td class="auto-style6">Up to 30 Days</td>
                                                <td class="auto-style7">
                                                    <asp:Label ID="lbl30_InvNO" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style8">
                                                    <asp:Label ID="lbl30_GstPay" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style9">
                                                    <asp:Label ID="lbl30_billPay" runat="server" Text=""></asp:Label>
                                                    <asp:Label ID="lbl30_billPayHide" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                                <td class="auto-style10">
                                                    <asp:CheckBox ID="chk30" runat="server" AutoPostBack="True" OnCheckedChanged="chk30_CheckedChanged" />
                                                </td>
                                                <td class="auto-style6">
                                                    <asp:TextBox ID="txt30Appr" runat="server" AutoPostBack="True" OnTextChanged="txt30Appr_TextChanged"></asp:TextBox>
                                                    <asp:Label ID="lbl30Appr" runat="server" Text="" Visible="false"></asp:Label>
                                                </td>
                                            </tr>
                                            <tr style="border:1px solid #000;">
                                                <td class="auto-style6">31 to 60 Days</td>
                                                <td class="auto-style7">
                                                    <asp:Label ID="lbl31_InvNo" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style8">
                                                    <asp:Label ID="lbl31_GstPay" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style9">
                                                    <asp:Label ID="lbl31_BillPay" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style10">
                                                    <asp:CheckBox ID="CheckBox5" runat="server" />
                                                </td>
                                                <td class="auto-style6">
                                                    <asp:TextBox ID="txt31_ApprPay" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="border:1px solid #000;">
                                                <td class="auto-style6">61 to 91 Days</td>
                                                <td class="auto-style7">
                                                    <asp:Label ID="lbl61_InvNO" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style8">
                                                    <asp:Label ID="lbl61_gstpay" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style9">
                                                    <asp:Label ID="lbl61_billPay" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style10">
                                                    <asp:CheckBox ID="CheckBox3" runat="server" />
                                                </td>
                                                <td class="auto-style6">
                                                    <asp:TextBox ID="txt60Appr" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="border:1px solid #000;">
                                                <td class="auto-style1 firstcol">Above 90 Days</td>
                                                <td class="auto-style2">
                                                    <asp:Label ID="lbl90_InvNO" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style3">
                                                    <asp:Label ID="lbl90_gstpay" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbl90_billPay" runat="server" Text=""></asp:Label>
                                                </td>
                                                <td class="auto-style5">
                                                    <asp:CheckBox ID="CheckBox4" runat="server" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txt90Appr" runat="server"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr style="border:1px solid #000;">
                                                <td class="auto-style1"><strong>Total</strong></td>
                                                <td class="auto-style2">
                                                    <asp:Label ID="lbltotal_inv" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style3">
                                                    <asp:Label ID="lbltotal_gst" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style4">
                                                    <asp:Label ID="lbltotal_bill" runat="server"></asp:Label>
                                                </td>
                                                <td class="auto-style5">&nbsp;</td>
                                                <td>&nbsp;</td>
                                            </tr>
                                        </table>
                                            </div>
                                            <div class="col-md-6">
                                                <asp:RadioButton ID="rdbBillPay" Text="Bill Payable" GroupName="pay_gst" runat="server" AutoPostBack="True" OnCheckedChanged="rdbBillPay_CheckedChanged" />
                                                <br />
                                                <asp:RadioButton ID="rdbGstClaimable" Text="Gst Claimable" GroupName="pay_gst" runat="server" AutoPostBack="True" OnCheckedChanged="rdbGstClaimable_CheckedChanged" />

                                            </div>
                                        </div>
                                        


                                        <br />
                                        <br />
                                        <div id="Div2" runat="server">
                                            <div class="box">

                                                <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                                    <div style="width: 100%; height: 200px; overflow: scroll">

                                                        <asp:GridView ID="GridView6" runat="server" AutoGenerateColumns="False" CssClass="table table-bordered table-hover tableFixHead" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal"  ShowHeaderWhenEmpty="True" OnRowDataBound="GridView6_RowDataBound">

                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Sno"  Visible="true">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblRowNumber" runat="server" />
                                                                    </ItemTemplate>

                                                                    <ItemStyle Width="20px"></ItemStyle>
                                                                </asp:TemplateField>
                                                               
                                                                 <asp:TemplateField HeaderText="Master Sno" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtlblMSnoEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbMSno" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Detail Sno" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtlblDSnoEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbDSno" runat="server" Text='<%# Bind("PurDetail") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="CrAcct" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtCrAcctEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblCrAcct" runat="server" Text='<%# Bind("CrAccountCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="DrAcct" SortExpression="Sno" Visible="false">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtDrAcctEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lbDrAcct" runat="server" Text='<%# Bind("DrAccountCode") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Bill No." SortExpression="BillNo">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillNoEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillNo" runat="server" Text='<%# Bind("BillNo") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Bill Date" SortExpression="BillDate">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillDateEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillDate" runat="server" Text='<%# Bind("BillDate") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GL Date" SortExpression="GLDate">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGLDateEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGLDate" runat="server" Text='<%# Bind("GLDATE") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="Supplier/Contractor" SortExpression="BPartnerName">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtSupplierEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblSupplier" runat="server" Text='<%# Bind("BPartnerName") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="GST Claimable" SortExpression="gstclaim">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGSTClaimableEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTClaimable" runat="server" Text='<%# Bind("gstclaim") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bill Payable" SortExpression="BalanceAmount">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillPayableEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblBillPayable" runat="server" Text='<%# Bind("BalanceAmount") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="" SortExpression="CheckBOX">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtCheckBOXEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:CheckBox ID="chk" runat="server" />
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Approved Amount" SortExpression="ApprovedAmount">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtApprovedAmountEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblApprovedAmount" runat="server" Text=""></asp:Label>
                                                                    </ItemTemplate>
                                                                   
                                                                </asp:TemplateField>


                                                                <asp:TemplateField HeaderText="GST Balance" SortExpression="BalanceTax">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtGSTBalanceEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
                                                                    </EditItemTemplate>
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblGSTBalance" runat="server" Text='<%# Bind("BalanceTax") %>'></asp:Label>
                                                                    </ItemTemplate>
                                                                    
                                                                </asp:TemplateField>

                                                                <asp:TemplateField HeaderText="Bill Balance" SortExpression="BillBalance">
                                                                    <EditItemTemplate>
                                                                        <asp:TextBox ID="txtBillBalanceEdit" runat="server" Enabled="false" Text=""></asp:TextBox>
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
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillVouchaer" HeaderText="BillVouchaer" SortExpression="BillVouchaer" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillNo" HeaderText="BillNo" SortExpression="BillNo" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="BillDate" SortExpression="BillDate" />
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

                                            <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound">
<Columns>
                                                     <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillVouchaer" HeaderText="BillVouchaer" SortExpression="BillVouchaer" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillNo" HeaderText="BillNo" SortExpression="BillNo" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="BillDate" SortExpression="BillDate" />
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
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" OnRowDataBound="GridView3_RowDataBound">
                                                <Columns>
                                                     <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <ItemStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" ReadOnly="True">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillVouchaer" HeaderText="BillVouchaer" SortExpression="BillVouchaer" >
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="BillNo" HeaderText="BillNo" SortExpression="BillNo" />
                                                    <asp:BoundField DataField="BillDate" HeaderText="BillDate" SortExpression="BillDate" />
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
                            <td class="rowpadd"><strong>Supplier Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlEdit_Supplier" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Supplier Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlFind_Supplier" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td class="rowpadd"><strong>Supplier Name</strong> </td>
                            <td class="rowpadd" >
                                
                                <asp:DropDownList ID="ddlDel_Supplier" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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


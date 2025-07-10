<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_BankAccount1.aspx.cs" Inherits="frm_Period" %>

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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>BANK ACCOUNT OPENNING</strong></h3>
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
                              
                                        <table id="tablecontent1" runat="server" style="width: 100%;">
                                            <tr>
                                                <td class="auto-style6"><strong>Bank Name</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlbankName" CssClass="form-control select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlbankName_SelectedIndexChanged"></asp:DropDownList>
                                                    <%-- <asp:TextBox ID="txtBankAccountName" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                                     <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddlbankName" Display="None" ErrorMessage="<b> Missing Field</b><br />A Bank Account name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                              <td class="auto-style6"><strong>Bank Branch</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlBankBranch" CssClass="form-control select2" Width="250px" runat="server"></asp:DropDownList>
                                                     
                                                </td>
                                                </tr>
                                           <tr>
                                                <td class="auto-style6"><strong>Bank Account Number</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtBankAccountNumber" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtBankAccountNumber" Display="None" ErrorMessage="<b> Missing Field</b><br />A Bank Account Number is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
                                                <td class="auto-style6"><strong>Account Title</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtAcctTitle" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtAcctTitle" Display="None" ErrorMessage="<b> Missing Field</b><br />A Bank Account name is required." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                     
                                                </td>
   
                                                
                                                </tr>
                                            <tr>
                                                 <td class="auto-style6"><strong>Entry Date</strong></td>
                                                <td class="auto-style11">
                                                   <%-- <asp:TextBox ID="txtEntryDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>

                                                     <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>
                                                     
                                                </td>
                                                 <td class="auto-style6"><strong>Bank Account Type</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlBankAccountType" CssClass="form-control select2" Width="250px" runat="server"></asp:DropDownList>
                                                     
                                                </td>
                                            </tr>
                                             
                                            <tr>
                                                <td class="auto-style6"><strong>Date Valid From</strong></td>
                                                <td class="auto-style11">
                                                   <%-- <asp:TextBox ID="txtEntryDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>

                                                     <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtdatevalidfrom" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa" style="left: 0px; top: 0px"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton2" TargetControlID="txtdatevalidfrom" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtdatevalidfrom" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>
                                                     
                                                </td>
                                                 <td class="auto-style6"><strong>Currency</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlCurrency" CssClass="form-control select2" Width="250px" runat="server"></asp:DropDownList>
                                                     
                                                </td>

                                            </tr>
                                            
                                                <tr>
                                                <td class="auto-style6"><strong>Account Holder Name 1</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtAccountHolderName1" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     
                                                </td>
                                                <td class="auto-style6"><strong>Account Holder Name 2</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtAccountHolderName2" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     
                                                </td>
                                                </tr>
                                              <tr>
                                                <td class="auto-style6"><strong>Account Holder Name 3</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtAccountHolderName3" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     
                                                </td>
                                                <td class="auto-style6"><strong>Account Holder Name 4</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtAccountHolderName4" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     
                                                </td>
                                                </tr>
                                             <tr>
                                               <td class="auto-style6"><strong>Account Code</strong></td>
                                                <td class="auto-style11">
                                                    <%--<asp:DropDownList ID="ddlAcct_Code" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa" AutoPostBack="True" OnSelectedIndexChanged="ddlAcct_Code_SelectedIndexChanged" ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddlAcct_Code" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Account code." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>--%>


                     <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_Code" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                         OnItemsRequested="RadComboAcct_Code_ItemsRequested" 
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
                    <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="RadComboAcct_Code"
                        Display="Dynamic" ErrorMessage="!" CssClass="validator">
                    </asp:RequiredFieldValidator>--%>



                                                </td>
                                               
                                                 
                                                 
                                                 
                                                
                                                       
                                                       <td class="auto-style6"><strong>Closing Date</strong></td>
                                                <td class="auto-style11">
                                                   <%-- <asp:TextBox ID="txtDateValidForm" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>

                                                     <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtInActiveDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton1" TargetControlID="txtInActiveDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtInActiveDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>
                                                     
                                                </td>
                                               

                                            </tr>
                                            <tr>
                                             <td class="auto-style6"><strong>Active</strong></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                              </td>
                                                 <td class="auto-style6"><strong>Description</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtDescription" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                     
                                                </td>
                                            </tr>
                                            <tr>

                                                <td class="auto-style6"><strong>Created By</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCreatedby" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                                <td class="auto-style6"><strong>Created Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtCreatedDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                                </td>
                                            </tr>
                                           

                                            <tr id="updatecol" runat="server">
                                                <td class="auto-style6"><strong>Updated By</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtUpdateBy" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                                <td class="auto-style6"><strong>Updated Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtUpdateDate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    
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

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" >
                                                <Columns>
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="BankAccountID" HeaderText="ID" SortExpression="BankAccountID">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountTitle" HeaderText="Bank Account Name" SortExpression="AccountTitle" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="BankName" HeaderText="Bank Name" SortExpression="BankName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="BankBranchName" HeaderText="Bank Branch Name" SortExpression="BankBranchName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                         <asp:BoundField DataField="BankAcctTypeName" HeaderText="Bank Account Type" SortExpression="BankAcctTypeName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                        <asp:BoundField DataField="BankAccountNumber" HeaderText="Bank Account Number" SortExpression="BankAccountNumber" >
                                                    <HeaderStyle Width="100px" />
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
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="BankAccountID" HeaderText="ID" SortExpression="BankAccountID">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountTitle" HeaderText="Bank Account Name" SortExpression="AccountTitle" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="BankName" HeaderText="Bank Name" SortExpression="BankName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="BankBranchName" HeaderText="Bank Branch Name" SortExpression="BankBranchName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                         <asp:BoundField DataField="BankAcctTypeName" HeaderText="Bank Account Type" SortExpression="BankAcctTypeName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                        <asp:BoundField DataField="BankAccountNumber" HeaderText="Bank Account Number" SortExpression="BankAccountNumber" >
                                                    <HeaderStyle Width="100px" />
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
                                                    <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="BankAccountID" HeaderText="ID" SortExpression="BankAccountID">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="AccountTitle" HeaderText="Account Title" SortExpression="AccountTitle" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>

                                                   <asp:BoundField DataField="BankName" HeaderText="Bank Name" SortExpression="BankName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="BankBranchName" HeaderText="Bank Branch Name" SortExpression="BankBranchName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                         <asp:BoundField DataField="BankAcctTypeName" HeaderText="Bank Account Type" SortExpression="BankAcctTypeName" >
                                                    <HeaderStyle Width="100px" />
                                                    </asp:BoundField>
                                                        <asp:BoundField DataField="BankAccountNumber" HeaderText="Bank Account Number" SortExpression="BankAccountNumber" >
                                                    <HeaderStyle Width="100px" />
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
                            <td class="rowpadd"><strong>Account Title</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlEdit_AcctTitle" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlEdit_BankName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Branch Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlEdit_BankBranchName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Account Type</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlEdit_BankAcctType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Account Number</strong> </td>
                            <td class="rowpadd" >
                                <asp:TextBox ID="txtEdit_BankAcctno" CssClass="texthieht form-control" runat="server"></asp:TextBox>

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
                            <td class="rowpadd"><strong>Account Title</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlFind_AcctTitle" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlFind_BankName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Branch Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlFind_BankBranchName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                         <tr>
                            <td class="rowpadd"><strong>Bank Account Type</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlFind_BankAcctType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Account Number</strong> </td>
                            <td class="rowpadd" >
                            
                                 <asp:TextBox ID="txtFind_BankAcctNo" CssClass="texthieht form-control" runat="server"></asp:TextBox>
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
                            <td class="rowpadd"><strong>Account Title</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlDel_AcctTitle" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlDel_BankName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Branch Name</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlDel_BankBranchName" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                      <tr>
                            <td class="rowpadd"><strong>Bank Account Type</strong> </td>
                            <td class="rowpadd" >
                            <asp:DropDownList ID="ddlDel_BankAcctType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td class="rowpadd"><strong>Bank Account Number</strong> </td>
                            <td class="rowpadd" >
                                <asp:TextBox ID="txtDel_bankAcctNo" CssClass="texthieht form-control" runat="server"></asp:TextBox>
                            
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


<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Tax.aspx.cs" Inherits="frm_Period" %>

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

        .padspace {
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .texthieht {
            height: 25px;
             display:inline-block
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
                margin-top:20px;
                color: #FFFFFF;
                display: block;
                float: left;
                height:40px;
                border-radius:10px 10px 0px 0px;
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
                    <div class="box-header with-border" style="height:75px;">
                        <div style="padding-top:0px; width: 120px; float: left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>TAX</strong></h3>
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
                            <asp:LinkButton ID="btnUpdate" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" OnClick="btnUpdate_Click" ValidationGroup="aaa">
                     <i class="fas fa-edit"></i>&nbsp Update
                            </asp:LinkButton>
                            <asp:LinkButton ID="btnDelete" Height="40" CssClass="btn btn-app " runat="server" Style="left: 180px; background-color: #1f4f8a; color: white;" data-toggle="modal" data-target="#modal_delete" >
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
                                    <td class="auto-style6"><strong>Tax Description</strong></td>
                                    <td class="auto-style11">
                                        <asp:TextBox ID="txtTaxDesc" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                          <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtTaxDesc" Display="None" ErrorMessage="<b> Missing Field</b><br />A name is required." ValidationGroup="aaa" />
                                          <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td class="auto-style10">Percentage</td>
                                    <td>
                                        <asp:TextBox ID="txtPercentage" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                    </td>
                                    
                                </tr>
                                <tr>
                                    <td class="auto-style3"><span class="auto-style9">Tax Type</span></td>
                                    <td class="auto-style11">
                                       <asp:TextBox ID="txtTaxType" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                    </td>
                                    <td class="auto-style3"><span class="auto-style9">Account Code</span></td>
                                    <td class="auto-style11">
                                       <%--<asp:TextBox ID="txtAccountCode" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                        <%--<asp:DropDownList ID="ddlAccountCode" Width="250px" CssClass="select2" runat="server"></asp:DropDownList>--%>
                    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboAcct_Code" runat="server" Height="200" Width="250"
                        DropDownWidth="500" EmptyMessage="Choose an Account Code" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" 
                        AutoPostBack="true" 
                        OnSelectedIndexChanged="RadComboAcct_Code_SelectedIndexChanged"
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
                                    </td>
                                   
                                </tr>
                                <tr>
                                    <td class="auto-style3"><span class="auto-style9">Active Status</span></td>
                                  
                                      <td class="auto-style11">
                                        <asp:Panel ID="Panel2" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                      <td class="auto-style3"><span class="auto-style9">Apply Date</span></td>
                                    <td class="auto-style11">
                                       <%--<asp:TextBox ID="txtApplydate" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" runat="server"  TargetControlID ="txtApplydate" Format="dd-MMM-yyyy"/>--%>
                                       
                                         <asp:TextBox ID="txtApplydate" CssClass="form-control texthieht"  Width="230px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                        <asp:ImageButton ID="imgPopup" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtApplydate" Format="dd-MMM-yyyy" runat="server" />
                                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtApplydate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                          <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>

                                    </td>
                                </tr>
                                <tr>
                                     <td class="auto-style10">Created By</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtCreatedBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="4"></asp:TextBox>
                                        </td>
                                     <td class="auto-style10">Created Date</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtCreatedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="5"></asp:TextBox>
                                    </td>
                                </tr>

                                 <tr id="updatecol" runat="server">
                                     <td class="auto-style10">Updated By</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtUpdatedBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="6"></asp:TextBox>
                                        </td>
                                     <td class="auto-style10">Updated Date</td>
                                    <td class="padspace">
                                        <asp:TextBox ID="txtUpadtedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" style="margin-bottom: 1" TabIndex="7"></asp:TextBox>
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

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="TaxTypeID">
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="TaxTypeID" HeaderText="ID" SortExpression="TaxTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TaxDescription" HeaderText="Tax Description" SortExpression="TaxDescription" >
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
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
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="TaxTypeID" HeaderText="ID" SortExpression="TaxTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TaxDescription" HeaderText="Tax Description" SortExpression="TaxDescription" >
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
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
                                            <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView3_SelectedIndexChanged">
                                             <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="TaxTypeID" HeaderText="ID" SortExpression="TaxTypeID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="TaxDescription" HeaderText="Tax Description" SortExpression="TaxDescription" >
                                                    <HeaderStyle Width="500px" />
                                                    </asp:BoundField>
                                                 <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
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
                            <td><strong>TAX Description:</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_TaxDescription" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_TaxDescription" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                               
                                </td>
                            </tr>
                        <tr><td><strong>Tax Type :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_TaxType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_TaxType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                       <tr><td><strong>Account Code :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_AccountCode" CssClass="form-control gap" runat="server"></asp:TextBox>--%>


                                <!--   asdasddsad -->
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="txtEdit_AccountCode" runat="server" Height="200" Width="100%" 
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
                                <!-- asdasdsadasdsa-->


                            </td>

                        </tr>
                        <tr>
                            <td><strong>Active :</strong> </td>
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
                            <td><strong>TAX Description:</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_TaxDescription" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_TaxDescription" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                               
                                </td>
                            </tr>
                        <tr><td><strong>Tax Type :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_TaxType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_TaxType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                       <tr><td><strong>Account Code :</strong> </td>
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
                            <td><strong>Active :</strong> </td>
                            <td>
                                  <asp:CheckBox ID="chkFind_Active" runat="server" Checked="true" Text="Active" />
                            </td>

                        </tr>

                    </table>

                </div>

                <div class="modal-footer">
                    <button type="button" class="btn btn-default pull-left" data-dismiss="modal">Close</button>

                    <asp:Button ID="btn_Search" CssClass="btn btn-primary" runat="server" Text="Search" OnClick="btn_Search_Click"/>

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
                            <td><strong>TAX Description:</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtdelete_tax" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtdelete_tax" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>

                               
                                </td>
                            </tr>
                        <tr><td><strong>Tax Type :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtdelete_TaxType" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtdelete_TaxType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                       <tr><td><strong>Account Code :</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtdelete_AccountCode" CssClass="form-control gap" runat="server"></asp:TextBox>--%>


                                 <!--   asdasddsad -->
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="txtdelete_AccountCode" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="txtdelete_AccountCode_ItemsRequested"
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
                            <td><strong>Active :</strong> </td>
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


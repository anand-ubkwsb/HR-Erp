<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_Set_UserGrp_Document.aspx.cs" Inherits="frm_Period" EnableEventValidation="false" %>

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

        .paddingZERO{
            padding:0px;
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
        .linee{
            display:inline-block;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>USER GROUP ASSIGN DOCUMENT</strong></h3>
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
                                                 <td class="auto-style6"><strong>User Group Name</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlUsergrp" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  AutoPostBack="True" OnSelectedIndexChanged="ddlUsergrp_SelectedIndexChanged"></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="ddlUsergrp" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select User Group Name." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                                </td>
                                                <td class="auto-style6"><strong>Document Description</strong></td>
                                                <td class="auto-style11">
                                                    <asp:TextBox ID="txtDocumentDesc" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px" ValidationGroup="aaa"></asp:TextBox>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="txtDocumentDesc" Display="None" ErrorMessage="<b> Missing Field</b><br />Please input document name." ValidationGroup="aaa" />
                                                     <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                

                                            </tr>
                                            <tr>
                                                <td class="auto-style6"><strong>Document</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlDocument" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="req1" ControlToValidate="ddlDocument" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Document." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="req1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                                </td>

                                                 <td class="auto-style10">SortOrder</td>
                                                <td class="padspace">
                                                    <div class="texthieht">
                                                    <asp:TextBox ID="txtSortOrder" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    </div>
                                                </td>
                                            </tr>
                                                
                                             <tr>
                                                 <td class="auto-style6"><strong>Start Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlStartDate" Width="250px" CssClass="select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="ddlStartDate" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Menu." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                 <td class="auto-style6"><strong>End Date</strong></td>
                                                <td class="auto-style11">
                                                    <asp:DropDownList ID="ddlEndDate" Width="250px" CssClass="select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    
                                                </td>
                                            </tr>

                                            <tr>
                                                <td class="auto-style10">Apply Date</td>
                                                <td class="padspace">
                                                     <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                        <asp:TextBox ID="txtAppleDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtAppleDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtAppleDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                           
                                                             <span class="input-group-btn">
                                                               <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                                </td>
                                                 <td class="auto-style6"><strong>Is Hide?</strong></td>
                                                <td class="auto-style11">
                                                     <asp:RadioButton ID="rdb_Hide_Y" runat="server" GroupName="Hide2" Text="YES" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdb_Hide_N" runat="server" GroupName="Hide2" Text="NO" />
                                                </td>
                                                
                                            </tr>
                                            
                                            
                                            <tr>
                                                <td class="auto-style6"><strong>Main Site</strong></td>
                                                <td class="auto-style11">

                                                    <asp:RadioButton ID="rdb_Main" runat="server" GroupName="Hide1" Text="Main" />
                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:RadioButton ID="rdb_Site" runat="server" GroupName="Hide1" Text="Site" />
                                                </td>
                                                 
                                                 <td class="auto-style3"><span class="auto-style9">Active</span></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel8" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style10">Created By</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtCreatedBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="4"></asp:TextBox>

                                                </td>
                                                <td class="auto-style10">Created Date</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtcreatedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="5"></asp:TextBox>
                                                    
                                                </td>
                                            </tr>

                                            <tr id="updatecol" runat="server">
                                                <td class="auto-style10">Updated By</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtUpdatedBy" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" TabIndex="6"></asp:TextBox>

                                                </td>
                                                <td class="auto-style10">Updated Date</td>
                                                <td class="padspace">
                                                    <asp:TextBox ID="txtUpdatedDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Style="margin-bottom: 1" TabIndex="7"></asp:TextBox>
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

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno" >
                                                <Columns>
                                                     <asp:CommandField ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" ShowSelectButton="True">
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                        <HeaderStyle Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="DocID" HeaderText="Doc ID" SortExpression="DocID">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="Doc Name" SortExpression="DocName">
                                                    </asp:BoundField>

                                                    <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
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
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                        <HeaderStyle Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                        <asp:BoundField DataField="DocID" HeaderText="Doc ID" SortExpression="DocID">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="Doc Name" SortExpression="DocName">
                                                    </asp:BoundField>
                                                      <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName">
                                                    </asp:BoundField>
                                                      <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
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
                                                    <HeaderStyle Width="50px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="Sno" HeaderText="Sno" SortExpression="Sno" InsertVisible="False" ReadOnly="True">
                                                        <HeaderStyle Width="50px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocDescription" HeaderText="DocDescription" SortExpression="DocDescription">
                                                    </asp:BoundField>
                                                     <asp:BoundField DataField="DocID" HeaderText="Doc ID" SortExpression="DocID">
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="DocName" HeaderText="Doc Name" SortExpression="DocName">
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName">
                                                    </asp:BoundField>
                                                   <asp:BoundField DataField="ApplyDate" HeaderText="Apply Date" SortExpression="ApplyDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
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
               <%-- new table add --%>
              
               

                         <%-- Table View --%>

                

                    

                            <div id="Div1" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView4" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView4_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="DocID"  OnRowDataBound="GridView4_RowDataBound"  >
                                                <Columns>
                                                    <asp:CommandField SelectImageUrl="~/dist/img/iconeditdata.png" ShowSelectButton="True" ButtonType="Image" />
                                                    <asp:TemplateField HeaderText="ID" SortExpression="DocID">
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblNatureID" runat="server" Text='<%# Eval("DocID") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNatureID" runat="server" Text='<%# Bind("DocID") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="20px" Height="10px" />
                                                        <ItemStyle Height="10px" />
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Document Description" SortExpression="DocDescription">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("DocDescription") %>'></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("DocDescription") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="300px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>
                                                    <asp:TemplateField HeaderText="Base Document " SortExpression="DocName">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DocName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200px" />
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="Start Date" SortExpression="StartDate">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtstdate" runat="server" Text='<%# Bind("StartDate") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblStDate" runat="server" Text='<%# Bind("StartDate") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlGridView4stdate" CssClass="form-control select2"   width="150px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" />
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>


                                                     <asp:TemplateField HeaderText="End Date" SortExpression="EndDate">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="txtEnddate" runat="server" Text='<%# Bind("EndDate") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbleddate" runat="server" Text='<%# Bind("EndDate") %>' Visible="false"></asp:Label>
                                                            <asp:DropDownList ID="ddlGridView4Enddate" CssClass="form-control select2"  width="150px" runat="server"></asp:DropDownList>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="100px" />
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Hide?">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_HYes" runat="server" Text =" YES" GroupName="rdbcheck"/>
                                                            <asp:RadioButton ID="rdb_NYes" runat="server" Text =" NO" Checked ="true" GroupName="rdbcheck" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_HYes" runat="server" Text =" YES" GroupName="rdbcheck" />
                                                            <asp:RadioButton ID="rdb_NYes" runat="server" Text =" NO"  Checked ="true"  GroupName="rdbcheck"/>
                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Main or Site?">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_HMain" runat="server" Text =" Main"  Checked ="true"  GroupName="rdbcheckMain"/>
                                                            <asp:RadioButton ID="rdb_NSite" runat="server" Text =" Site" GroupName="rdbcheckMain" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_HMain" runat="server" Text =" Main"  Checked ="true"  GroupName="rdbcheckMain" />
                                                            <asp:RadioButton ID="rdb_NSite" runat="server" Text =" Site" GroupName="rdbcheckMain"/>
                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>
                                                   
                                                    <asp:TemplateField HeaderText="Apply Date">
                                                        <HeaderTemplate>

                                                            &nbsp;
                                                            <asp:Label ID="Label2" runat="server" Text="Apply Date"></asp:Label>
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                            <%--<asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" />--%>
                                                        </HeaderTemplate>
                                                        <EditItemTemplate>
                                                             <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtAppleDate123" CssClass="form-control" Width="100px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1"  PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req212" ControlToValidate="txtAppleDate123" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req212" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtAppleDate123" CssClass="form-control" Width="100px"  runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" SelectedDate='<%# DateTime.Now %>' PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req212" ControlToValidate="txtAppleDate123" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req212" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="50px" />
                                                    </asp:TemplateField>
                                                     <asp:TemplateField HeaderText="">
                                                         <HeaderTemplate>
                                                              <asp:CheckBox ID="chkall" runat="server" AutoPostBack="True" OnCheckedChanged="chkall_CheckedChanged" Text="All" Width="83px" />
                                                         </HeaderTemplate>
                                                        <EditItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="chkSelect" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="40px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>


                                                   


                                                </Columns>
                                                <FooterStyle BackColor="#CCCC99" ForeColor="Black" />
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White" />
                                                <PagerStyle BackColor="White" ForeColor="Black" HorizontalAlign="Right" />

                                                <SortedAscendingCellStyle BackColor="#F7F7F7" />
                                                <SortedAscendingHeaderStyle BackColor="#4B4B4B" />
                                                <SortedDescendingCellStyle BackColor="#E5E5E5" />
                                                <SortedDescendingHeaderStyle BackColor="#242121" />
                                            </asp:GridView>

                                           <%-- <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:db_HRERPSys %>" SelectCommand="SELECT [DocID], [DocDescription], [DocName], [IsActive], [IsHide] , [Main_Site], [ApplyDate] FROM [viewDOCUSER] order by DocDescription asc"></asp:SqlDataSource>--%>

                                        </div>

                                    </div>
                                    <!-- /.box-body -->
                                </div>
                                <div style="float:right;">
                     <asp:Button ID="Button1" runat="server" CssClass="btn btn-primary" Text="Save" Width="120px" OnClick="Button1_Click" />
                    <asp:Button ID="Button2" runat="server" CssClass="btn btn-default" Text="Cancel" Width="120px" OnClick="Button2_Click"  />
                    </div>
                            </div>
               
              <%--  <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label><br />
                <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label><br />
                <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label><br />
                <asp:Label ID="Label6" runat="server" Text="Label"></asp:Label><br />--%>
                 
                
                            <%-- Table View --%>
               <%-- new table add --%>


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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlEdit_USERGRP" CssClass="form-control select2" width="100%" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                         

                        <tr>
                            <td><strong>Document Name</strong> </td>
                            <td id="rad1" runat="server">
                               <telerik:RadComboBox RenderMode="Lightweight" ID="radEdit_DocumetName" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="radEdit_DocumetName_ItemsRequested"
                        Label="" Visible="False">
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 175px;">
                                        Document Name
                                    </td>
                                    <td style="width: 175px;">
                                        
                                       Document Abbr
                                    </td>
                                    
                                </tr>
                            </table>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                <tr>
                                    <td style="width: 175px;">
                                        <%# DataBinder.Eval(Container, "Text")%>
                                    </td>
                                    <td style="width: 175px;">
                                        <%# DataBinder.Eval(Container, "Attributes['DocAbbr']")%>
                                    </td>
                                    
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                

                            </td>




                            <td>
                                <asp:DropDownList ID="ddlEdit_DocName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>

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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlFind_USerGrpName" CssClass="form-control select2" width="100%" runat="server"></asp:DropDownList>

                            </td>
                        </tr>


                        <tr>
                            <td><strong>Document Name</strong> </td>
                            <td id="rad2" runat="server">
                            <telerik:RadComboBox RenderMode="Lightweight" ID="radFind_DocumentName" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="radFind_DocumetName_ItemsRequested"
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 60px;">
                                        Document Name
                                    </td>
                                    <td style="width: 175px;">
                                        
                                       Document Abbr
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
                                        <%# DataBinder.Eval(Container, "Attributes['DocAbbr']")%>
                                    </td>
                                    
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                </td>

                             <td>
                                <asp:DropDownList ID="ddlFind_DOCNAme" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>

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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="ddlDel_UserGRP" CssClass="form-control select2" width="100%" runat="server"></asp:DropDownList>

                            </td>
                        </tr>
                        <tr>
                            <td><strong>Document Name</strong> </td>
                            <td id ="rad3" runat="server">
                             <telerik:RadComboBox RenderMode="Lightweight" ID="radDel_DocumentName" runat="server" Height="200" Width="100%" 
                        DropDownWidth="500" EmptyMessage="Choose a Product" HighlightTemplatedItems="true"
                        EnableLoadOnDemand="true" Filter="StartsWith" OnItemsRequested="radDel_DocumentName_ItemsRequested"
                        Label="" >
                        <HeaderTemplate>
                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                <tr>
                                   <td style="width: 60px;">
                                        Document Name
                                    </td>
                                    <td style="width: 175px;">
                                        
                                       Document Abbr
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
                                        <%# DataBinder.Eval(Container, "Attributes['DocAbbr']")%>
                                    </td>
                                    
                                </tr>
                            </table>
                        </ItemTemplate>
                    </telerik:RadComboBox>
                                </td>



                            <td>
                                <asp:DropDownList ID="ddlDel_DocNAme" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>

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


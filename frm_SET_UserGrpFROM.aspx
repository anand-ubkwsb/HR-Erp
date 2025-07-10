<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_UserGrpFROM.aspx.cs" Inherits="frm_SET_COA_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
  
    <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserGrpFROM.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";

                });

        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_UserGrpFROM.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                window.location = "frm_SET_UserGrpFROM.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
            height: 54px;
        }

        .auto-style3 {
            height: 36px;
        }

        .auto-style4 {
            height: 44px;
        }

        .auto-style5 {
            height: 40px;
        }

        .FixedHeader {
            position: relative;
            top: 10px;
            height: 30px;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">

   


     <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">

                        <div style=" padding-top:0px; width:120px;float:left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>User Group Form</strong></h3>
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
                                    <td class="auto-style6"><strong>User Group Name:</strong></td>
                                    <td class="auto-style7">
                                        
                                            <asp:DropDownList ID="lblUserGrpName" CssClass="ddlHeight select2" Width="250px" runat="server" AutoPostBack="True" OnSelectedIndexChanged="lblUserGrpName_SelectedIndexChanged" ></asp:DropDownList>
                                            <%--<asp:Label ID="lblUserGrpName" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>--%>

                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="lblUserGrpName" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select User Group Name." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                        
                                    </td>
                                    <td class="auto-style4"><strong>Menu Title</strong></td>
                                    <td class="auto-style1 ">
                                       
                                           <asp:DropDownList ID="lblmenu" CssClass="ddlHeight select2" Width="250px" runat="server" ></asp:DropDownList>
                                            <%--<asp:Label ID="lblmenu" runat="server"  Font-Italic="False" ForeColor="Black"></asp:Label>--%>


                                         <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="lblmenu" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Menu." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>
                                        
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Form Title</strong></td>
                                    <td class="auto-style8">
                                       
                                            <asp:DropDownList ID="lblform" CssClass="ddlHeight select2" Width="250px" runat="server" ></asp:DropDownList>
                                            <%--<asp:Label ID="lblform" runat="server"  Font-Italic="False" ForeColor="Black"></asp:Label>--%>

                                           <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="lblform" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Form." ValidationGroup="aaa" />
                                            <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Right"></ajaxToolkit:ValidatorCalloutExtender>

                                        
                                    </td>
                                    <td class="auto-style5"><strong>Sort Order</strong></td>
                                    <td>
                                        
                                            <asp:TextBox ID="txtSortOrder" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>
                                        </td>
                                </tr>
                                      <tr>
                                    <td class="auto-style3"><strong>Form Date</strong></td>
                                    <td class="auto-style8">
                                       
                                            <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtDateFrom" runat="server" CssClass="form-control texthieht" Width="220px"  Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton1" TargetControlID="txtDateFrom" Format="dd-MMM-yyyy" runat="server" />
                                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtDateFrom" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                             <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                          
                                                            
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>

                                        
                                    </td>
                                    <td class="auto-style5"><strong>To Date</strong></td>
                                    <td>
                                        <div style="display: inline-block">

                                            <div class="input-group input-group-sm">
                                                
                                               <asp:TextBox ID="txtdateTo1" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton2" TargetControlID="txtdateTo1" Format="dd-MMM-yyyy" runat="server" />
                                                        <%--<asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" ControlToValidate="txtdateTo1" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator5" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                                 <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/Close_Icon.png" ImageAlign="Bottom" runat="server" OnClick="ImageButton3_Click" />
                                                            </span>




                                              

                                            </div>

                                        </div>
                                           
                                   </td>
                                </tr>
                                <tr>
                                    <td class="auto-style5"><strong>Hide</strong></td>
                                    <td class="padspace">
                                        <asp:RadioButton ID="rdb_Hide_Y" runat="server" GroupName="Hide1" Text="YES" />
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:RadioButton ID="rdb_Hide_N" runat="server" GroupName="Hide1" Text="NO" />
                                    </td>

                                    <td class="auto-style3"><strong>DML Allowed</strong></td>
                                    <td class="padspace">
                                         <asp:RadioButton ID="rdb_DML_Y" runat="server" GroupName="Hide" Text="Allow" />
                                              &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                       <asp:RadioButton ID="rdb_DML_N" runat="server" GroupName="Hide" Text="Not Allow" />
                                    </td>
                                    
                                     
                                </tr>
                                <tr>
                                    <td class="auto-style3"><strong>Entry User Name</strong></td>
                                    <td class="auto-style8">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblEntryUSer_Name" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style10"><strong>Active</strong></td>
                                   <td class="auto-style8">
                                        <asp:Panel ID="GrpActiveStatus" runat="server">
                                            <asp:CheckBox ID="chkActive_Status" runat="server" Text="Active" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                </tr>
                                      <tr>
                                          <td class="auto-style3"><strong>Entry Date</strong></td>
                                          <td class="auto-style8">
                                             
                                                  <%--<asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:TextBox>--%>
                                                    <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            
                                                            <asp:TextBox ID="txtEntryDate" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa" ></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtEntryDate" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtEntryDate" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                                          </td>
                                          <td class="auto-style10"><strong>System Date</strong></td>
                                          <td class="padspace">
                                             
                                              <div class="backcolorlbl texthieht">
                                            <asp:Label ID="lblSystemDate" runat="server" Font-Italic="False" ForeColor="Black"></asp:Label>
                                        </div>
                                          </td>
                                      </tr>

                                      <tr>
                                          <td class="auto-style3"><strong>Action Rights</strong></td>
                                          <td class="auto-style8" colspan="3">

                                              <asp:Panel ID="Panel1" runat="server" >
                                                  
                                                      <asp:CheckBox ID="chk_R_Add" runat="server" Text="ADD" />
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;<asp:TextBox ID="txtadddays" runat="server" Height="21px" Width="24px"></asp:TextBox>
                                                      &nbsp;Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;

                                                  
                                                      <asp:CheckBox ID="chk_R_Edit" runat="server" Text="EDIT" />
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;<asp:TextBox ID="txtEditDays" runat="server" Height="21px" Width="24px"></asp:TextBox>
                                                      &nbsp;&nbsp;Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;&nbsp;
                                                  

                                                  
                                                      <asp:CheckBox ID="chk_R_Delete" runat="server" Text="DELETE" />
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                  <asp:TextBox ID="txtDelDays" runat="server" Height="21px" Width="24px"></asp:TextBox>
                                                      &nbsp;Days&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                                 

                                                 
                                                      <asp:CheckBox ID="chk_R_Find" runat="server" Text="FIND" />
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; &nbsp;&nbsp;&nbsp;
                                               

                                                 
                                                      <asp:CheckBox ID="chk_R_View" runat="server" Text="VIEW" />
                                                      &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                 


                                              </asp:Panel>
                                          </td>
                                          
                                      </tr>
                             

                            </table>
                                       
                                </div>


                             <%--  ENd Modals --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>
                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                          <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                                <asp:BoundField DataField="FormTitle" HeaderText="Form Title" SortExpression="FormTitle" />
                                                 <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName"></asp:BoundField>
                                                 <asp:BoundField DataField="Menu_Title" HeaderText="Menu Title" SortExpression="Menu_Title" />
                                                <asp:BoundField DataField="DmlAllowed" HeaderText="DML Allowed" SortExpression="DmlAllowed" />
                                                <asp:BoundField DataField="DateFrom" HeaderText="Date From" SortExpression="DateFrom" />
                                                <asp:BoundField DataField="Add" HeaderText="Add" SortExpression="Add" />
                                                <asp:BoundField DataField="AddDays" HeaderText="Add Days" SortExpression="AddDays"></asp:BoundField>
                                                <asp:BoundField DataField="Edit" HeaderText="Edit" SortExpression="Edit" />
                                                <asp:BoundField DataField="EditDays" HeaderText="Edit Days" SortExpression="EditDays" />
                                                <asp:BoundField DataField="Delete" HeaderText="Delete" SortExpression="Delete" />
                                                <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
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

                             <%--  ENd Modals --%>

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--  ENd Modals --%>
                                        <div style="width: 100%; height: 400px; overflow: scroll">
                                 <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" OnRowDataBound="GridView2_RowDataBound" >
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                                <asp:BoundField DataField="FormTitle" HeaderText="Form Title" SortExpression="FormTitle" />
                                                 <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName"></asp:BoundField>
                                               
                                                <asp:BoundField DataField="Menu_Title" HeaderText="Menu Title" SortExpression="Menu_Title" />
                                                <asp:BoundField DataField="DmlAllowed" HeaderText="DML Allowed" SortExpression="DmlAllowed" />
                                                <asp:BoundField DataField="DateFrom" HeaderText="Date From" SortExpression="DateFrom" />
                                                <asp:BoundField DataField="Add" HeaderText="Add" SortExpression="Add" />
                                                <asp:BoundField DataField="AddDays" HeaderText="Add Days" SortExpression="AddDays"></asp:BoundField>
                                                <asp:BoundField DataField="Edit" HeaderText="Edit" SortExpression="Edit" />
                                                <asp:BoundField DataField="EditDays" HeaderText="Edit Days" SortExpression="EditDays" />
                                                <asp:BoundField DataField="Delete" HeaderText="Delete" SortExpression="Delete" />
                                                  <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
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
                                        <%--  ENd Modals --%>
                                        <div style="width: 100%; height: 400px; overflow: scroll">
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False"  OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                         <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="Sno" HeaderText="ID" SortExpression="Sno" />
                                              <asp:BoundField DataField="FormTitle" HeaderText="Form Title" SortExpression="FormTitle" />  
                                              <asp:BoundField DataField="UserGrpName" HeaderText="User Group Name" SortExpression="UserGrpName"></asp:BoundField>
                                               
                                                <asp:BoundField DataField="Menu_Title" HeaderText="Menu Title" SortExpression="Menu_Title" />
                                                <asp:BoundField DataField="DmlAllowed" HeaderText="DML Allowed" SortExpression="DmlAllowed" />
                                                <asp:BoundField DataField="DateFrom" HeaderText="Date From" SortExpression="DateFrom" />
                                                <asp:BoundField DataField="Add" HeaderText="Add" SortExpression="Add" />
                                                <asp:BoundField DataField="AddDays" HeaderText="Add Days" SortExpression="AddDays"></asp:BoundField>
                                                <asp:BoundField DataField="Edit" HeaderText="Edit" SortExpression="Edit" />
                                                <asp:BoundField DataField="EditDays" HeaderText="Edit Days" SortExpression="EditDays" />
                                                <asp:BoundField DataField="Delete" HeaderText="Delete" SortExpression="Delete" />
                                               <asp:BoundField DataField="EntryDate" HeaderText="Entry Date" SortExpression="EntryDate" DataFormatString = "{0:dd-MMM-yyyy}" >
                                                    <HeaderStyle Width="200px" />
                                                    </asp:BoundField>
                                                <asp:CheckBoxField DataField="IsActive" HeaderText="Active" SortExpression="IsActive" />
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

               
                             <div id="Div1" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                       <%-- CssClass="table table-bordered table-hover"--%>
                                      
                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView4" runat="server"  CssClass="table table-bordered table-hover tableFixHead"  AutoGenerateColumns="False" OnSelectedIndexChanged="GridView4_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="Sno"  OnRowDataBound="GridView4_RowDataBound"  >
                                              
                                                 <Columns>
                                                    <asp:CommandField SelectImageUrl="~/dist/img/iconeditdata.png" ShowSelectButton="True" ButtonType="Image" />
                                                    <asp:TemplateField HeaderText="ID" SortExpression="Sno">
                                                        
                                                        <EditItemTemplate>
                                                            <asp:Label ID="lblNatureID" runat="server" Text='<%# Eval("Sno") %>'></asp:Label>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblNatureID" runat="server" Text='<%# Bind("Sno") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle  Height="10px" />
                                                        <ItemStyle Height="10px" />
                                                    </asp:TemplateField>
                                                    
                                                     <asp:TemplateField HeaderText="Form Title" SortExpression="FormTitle">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("FormTitle") %>'></asp:TextBox>
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lbldoc" runat="server" Text='<%# Bind("FormTitle") %>'></asp:Label>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="300px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Menu" SortExpression="MenuId">
                                                         <EditItemTemplate>
                                                             <asp:TextBox ID="TextBox2" runat="server" Text='<%# Bind("MenuId") %>'></asp:TextBox>
                                                             
                                                         </EditItemTemplate>
                                                         <ItemTemplate>
                                                             <asp:Label ID="lbldoc1" runat="server" Text='<%# Bind("MenuId") %>' Visible="false"></asp:Label>
                                                              <asp:DropDownList ID="ddlGridView4Menuid" CssClass="form-control select2"  width="220px" runat="server"></asp:DropDownList>
                                                         </ItemTemplate>
                                                         <HeaderStyle Width="300px" />
                                                          <ItemStyle Height="10px" />
                                                    </asp:TemplateField>
                                                    <%--<asp:TemplateField HeaderText="Base Document " SortExpression="DocName">
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="TextBox3" runat="server" Text='<%# Bind("DocName") %>'></asp:TextBox>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:Label ID="lbldocname" runat="server" Text='<%# Bind("DocName") %>'></asp:Label>
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="200px" />
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>--%>
                                                    <asp:TemplateField HeaderText="Hide?">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_HYes" runat="server" Text ="Y" GroupName="rdbcheck" Font-Bold="False" Height="0px"/>
                                                            &nbsp;&nbsp;
                                                            <asp:RadioButton ID="rdb_NYes" runat="server" Text ="N" Checked ="true" GroupName="rdbcheck" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_HYes" runat="server" Text ="Y" GroupName="rdbcheck" />
                                                            &nbsp;<asp:RadioButton ID="rdb_NYes" runat="server" Text ="N"  Checked ="true"  GroupName="rdbcheck"/>
                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                    <asp:TemplateField HeaderText="Add">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_ADDY" runat="server" Text =" YES"  Checked ="true"  GroupName="rdbcheckMain"/>
                                                            <asp:RadioButton ID="rdb_ADDN" runat="server" Text =" NO" GroupName="rdbcheckMain" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_ADDY" runat="server" Text ="Y"  Checked ="true"  GroupName="rdbcheckMain" />
                                                            <asp:RadioButton ID="rdb_ADDN" runat="server" Text ="N" GroupName="rdbcheckMain"/>
                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                     <asp:TemplateField HeaderText="Edit">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_EditY" runat="server" Text =" YES"  Checked ="true"  GroupName="rdbcheckEdit"/>
                                                            <asp:RadioButton ID="rdb_EditN" runat="server" Text =" NO" GroupName="rdbcheckEdit" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_EditY" runat="server" Text ="Y"  Checked ="true"  GroupName="rdbcheckEdit" />
                                                            <asp:RadioButton ID="rdb_EditN" runat="server" Text ="N" GroupName="rdbcheckEdit"/>
                                                            <%--<asp:Label ID="Label1" runat="server" Text='<%# Bind("IsHide") %>'></asp:Label>--%>
                                                        </ItemTemplate>
                                                         <ItemStyle Height="10px" />
                                                    </asp:TemplateField>

                                                      <asp:TemplateField HeaderText="Delete">
                                                        <EditItemTemplate>
                                                            <asp:RadioButton ID="rdb_DeleteY" runat="server" Text =" YES"  Checked ="true"  GroupName="rdbcheckdelte"/>
                                                            <asp:RadioButton ID="rdb_DeleteN" runat="server" Text =" NO" GroupName="rdbcheckdelte" />
                                                           <%-- <asp:TextBox ID="TextBox1" runat="server" Text='<%# Bind("IsHide") %>'></asp:TextBox>--%>
                                                        </EditItemTemplate>
                                                        <ItemTemplate>
                                                            <asp:RadioButton ID="rdb_DeleteY" runat="server" Text ="Y"  Checked ="true"  GroupName="rdbcheckdelte" />
                                                            <asp:RadioButton ID="rdb_DeleteN" runat="server" Text ="N" GroupName="rdbcheckdelte"/>
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
                                                            <asp:TextBox ID="txtAppleDate123" CssClass="form-control" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
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
                                                            <asp:TextBox ID="txtAppleDate123" CssClass="form-control" Width="220px"  runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup1" TargetControlID="txtAppleDate123" Format="dd-MMM-yyyy" runat="server" />
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
                                                <HeaderStyle BackColor="#000080" Font-Bold="True" ForeColor="White"/>
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

            </div>
        </div>

    </section>


     <%--  ENd Modals --%>
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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_UserGrpName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_UserGrpName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Form Title</strong> </td>
                            <td>
                               <%-- <asp:TextBox ID="txtEdit_FormTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_FormTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_MenuTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtEdit_MenuTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         
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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_UserGrpName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_UserGrpName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Form Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_FormTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_FormTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_MenuTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtFind_MenuTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
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
                            <td><strong>User Group Name</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDel_UsergrpName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDel_UsergrpName" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        <tr>
                            <td><strong>Form Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDel_FormTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDel_FormTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Menu Title</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDel_MenuTitle" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                <asp:DropDownList ID="txtDel_MenuTitle" CssClass="form-control select2" width="100%"  runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                        
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="chkDel_Active" runat="server" Checked="true" Text="Active" />
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


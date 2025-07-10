<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SET_COA_Master.aspx.cs" Inherits="frm_SET_COA_Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" Runat="Server">
     <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_COA_Master.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SET_COA_Master.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SET_COA_Master.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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

        .auto-style1 {
            height: 20px;
            padding-bottom: 7px;
        }

        .auto-style3 {
            width: 173px;
            padding-bottom: 7px;
        }

        .auto-style4 {
            height: 20px;
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style5 {
            width: 239px;
            padding-bottom: 7px;
        }

        .auto-style6 {
            width: 173px;
        }

        .auto-style7 {
            height: 20px;
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style8 {
            padding-bottom: 7px;
            width: 296px;
        }

        .auto-style9 {
            font-weight: bold;
        }

        .auto-style10 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
            height: 39px;
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
            height: 40px;
        }

        .auto-style16 {
            padding-bottom: 7px;
            width: 296px;
            height: 40px;
        }

        .auto-style17 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
            height: 40px;
        }

        .auto-style18 {
            padding-bottom: 7px;
            height: 40px;
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
        .auto-style19 {
            width: 173px;
            padding-bottom: 7px;
            height: 27px;
        }
        .auto-style20 {
            padding-bottom: 7px;
            width: 296px;
            height: 27px;
        }
        .auto-style21 {
            height: 39px;
        }
        .auto-style22 {
            padding-bottom: 0px;
            height: 39px;
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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>COA MASTER</strong></h3>
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
                                
                                   <table id="tablecontent1" runat="server" style="width: 100%;" >
                                <tr>
                                    <td class="auto-style6"><strong>COA Master Serial </strong></td>
                                    <td class="auto-style7">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblCoaMAsterSerial" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>

                                        </div>
                                    </td>
                                    <td class="auto-style4"><span class="auto-style9">GOC/Company COA</span></td>
                                    <td class="auto-style1 ">
                                        <%-- Table View --%>
                                        <asp:RadioButton ID="rdbGoc" runat="server" Text="GOC" GroupName="GOC_COMP" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:RadioButton ID="rdbCompanny" runat="server" Text="Company" GroupName="GOC_COMP" />
                                    </td>
                                </tr>
                               
                                <tr>
                                    <td class="auto-style17">COA Template</td>
                                    <td class="auto-style18">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblCOA_Template" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>

                                        </div>
                                    </td>
                                    
                                    <td class="auto-style15"><span class="auto-style9">No of Segments</span></td>
                                    <td class="auto-style16">

                                        <asp:TextBox ID="txtNoOFSEG" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    

                                    
                                     <td class="auto-style19"><strong>Fiscal Start Date for COA </strong></td>
                                    <td class="auto-style20">
                                        
                                        <%--<asp:TextBox ID="txtFS_DateCOA" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID ="txtFS_DateCOA" runat="server" Format="dd-MMM-yyyy" />--%>



                                         <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtFS_DateCOA" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="imgPopup" TargetControlID="txtFS_DateCOA" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtFS_DateCOA" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>




                                           
                                    </td>
                                    <td class="auto-style19"><strong>Fiscal End Date for COA </strong></td>
                                    <td class="auto-style20">
                                        
                                        <%--<asp:TextBox ID="txtFEnd_DateCOA" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" TargetControlID="txtFEnd_DateCOA" runat="server" Format="dd-MMM-yyyy" />--%>



                                         <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtFEnd_DateCOA" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton1" TargetControlID="txtFEnd_DateCOA" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFEnd_DateCOA" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
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
                                    <td class="auto-style10">User Name</td>
                                    <td class="auto-style21">
                                         <div class="backcolorlbl">
                                       <asp:Label ID="lblUSerNAme" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>
                                             </div>
                                    </td>
                                    <td class="auto-style10"><strong>Entry Date </strong></td>
                                    <td class="auto-style22">
                                        
                                     <%--   <asp:TextBox ID="txtEntry_Date" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black" ReadOnly="True"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" TargetControlID="txtEntry_Date" runat="server" Format="dd-MMM-yyyy" ClearTime="True"/>--%>



                                        <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaa"></asp:TextBox>
                                                             <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="ImageButton2" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEntry_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>


                                                       
                                                    </div>


                                           
                                    </td>
                                    
                                </tr>
                            <tr>

                                <td class="auto-style11"><span class="auto-style9">Narration</span></td>
                                    <td class="auto-style12">
                                        
                                            <asp:TextBox ID="txtNarration" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>
                                    </td>
                                 <td class="auto-style3"><span class="auto-style9">System Date</span></td>
                                    <td class="auto-style8">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblSystem_Date" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>

                                        </div>
                                    </td>
                                </tr>
                                       <tr>
                                            <td class="auto-style5"><strong>Active</strong></td>
                                    <td>
                                          <asp:Panel ID="Panel1" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                        
                                    </td>
                                       </tr>
                            </table>
                                       
                                </div>


                            <%--CssClass="table table-bordered table-hover"--%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%-- Table View --%>

                                         
                                          <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="COA_M_ID" EmptyDataText="NO DATA FOUND" >
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="COA_M_ID" HeaderText="ID" SortExpression="COA_M_ID" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                                                <asp:BoundField DataField="NoofSegments" HeaderText="No of Segments" SortExpression="NoofSegments" />
                                                <asp:BoundField DataField="Template" HeaderText="Template" SortExpression="Template" />
                                                <asp:CheckBoxField DataField="Goc_Comp_Coa" HeaderText="Goc Comp Coa" SortExpression="Goc_Comp_Coa" />
                                                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <%--CssClass="table table-bordered table-hover"--%>

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                 <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="COA_M_ID"  BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" >
                                             <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="COA_M_ID" HeaderText="ID" SortExpression="COA_M_ID" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                                                <asp:BoundField DataField="NoofSegments" HeaderText="No of Segments" SortExpression="NoofSegments" />
                                                <asp:BoundField DataField="Template" HeaderText="Template" SortExpression="Template" />
                                                <asp:CheckBoxField DataField="Goc_Comp_Coa" HeaderText="Goc Comp Coa" SortExpression="Goc_Comp_Coa" />
                                                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%-- Start MOdals --%>
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="COA_M_ID" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                             <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" />
                                                <asp:BoundField DataField="COA_M_ID" HeaderText="ID" SortExpression="COA_M_ID" InsertVisible="False" ReadOnly="True"></asp:BoundField>
                                                <asp:BoundField DataField="NoofSegments" HeaderText="No of Segments" SortExpression="NoofSegments" />
                                                <asp:BoundField DataField="Template" HeaderText="Template" SortExpression="Template" />
                                                <asp:CheckBoxField DataField="Goc_Comp_Coa" HeaderText="Goc Comp Coa" SortExpression="Goc_Comp_Coa" />
                                                <asp:CheckBoxField DataField="Active" HeaderText="Active" SortExpression="Active" />
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


    <%--<asp:TextBox ID="txtEdit_GOCName" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
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
                            <td><strong>GOC/Company</strong> </td>
                            <td>
                                <asp:DropDownList ID="txtEdit_GOCName" CssClass="form-control" runat="server"></asp:DropDownList>
                                <%--<asp:TextBox ID="txtFind_Goc_Company" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>No of Segments</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_NoofSeg" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Template</strong> </td>
                            <td>
                                <asp:TextBox ID="txtEdit_Template" CssClass="form-control gap" runat="server"></asp:TextBox>
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
                            <td><strong>Goc/Company COA</strong> </td>
                            <td>
                                <asp:DropDownList ID="txtFind_Goc_Company" CssClass="form-control" runat="server"></asp:DropDownList>

                                <%--<asp:TextBox ID="" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>No of Segments</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_NoofSegments" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Template</strong> </td>
                            <td>
                                <asp:TextBox ID="txtFind_Template" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Active</strong> </td>
                            <td>
                                  <asp:CheckBox ID="ChkFInd_Active" runat="server" Checked="true" Text="Active" />
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
                            <td><strong>GOC Name</strong> </td>
                            <td>
                                <asp:DropDownList ID="txtDelete_GOCName" CssClass="form-control" runat="server"></asp:DropDownList>

                                <%--  ENd Modals --%>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Company Name</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_CompName" CssClass="form-control gap" runat="server"></asp:TextBox>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Template</strong> </td>
                            <td>
                                <asp:TextBox ID="txtDelete_Template" CssClass="form-control gap" runat="server"></asp:TextBox>
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


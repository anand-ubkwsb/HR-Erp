<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetFiscal.aspx.cs" Inherits="frm_SetFiscal" MaintainScrollPositionOnPostback="true"  %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
     <script>
        function alertme() {
            swal("Insert Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetFiscal.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
            });
           
        }

        function Editalert() {
            swal("Update Successfuly!", "record has been saved!!!", "success")
                .then(function () {
                    window.location = "frm_SetFiscal.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
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
                    window.location = "frm_SetFiscal.aspx?UserID=" + userid + "&UsergrpID=" + UserGrpID + "&fiscaly=" + fiscal + "&FormID=" + FormID + "";
                });
        }
    </script>
    <style type="text/css">
/*flex con*/
.flex-container {
  display: flex;
  flex-wrap: nowrap;
 
}

.flex-container > div {
 
  width: 100px;

  text-align: center;
  
  font-size: 30px;
}

        /*flex con*/
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

        .auto-style13 {
            width: 239px;
            padding-bottom: 7px;
            font-weight: bold;
            height: 36px;
        }

        .auto-style14 {
            padding-bottom: 7px;
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
    </style>


</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="Server">
    <%--<asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>--%>
  

     <section class="content">
        <div class="row">
            <div class="col-lg-12">

                <div class="box box-solid shadowbox " style="border-radius: 15px;">
                    <div class="box-header with-border" style="height:75px;">

                        <div style=" padding-top:0px; width:120px;float:left;">
                            <div id="tabmenu">
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>Fiscal Year</strong></h3>
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
                                    <td class="auto-style6"><strong>Fiscal Year Id:</strong></td>
                                    <td class="auto-style7">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblFiscal_Year_Id" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"></asp:Label>

                                        </div>
                                    </td>
                                    <td class="auto-style4"><span class="auto-style9">Fiscal Year Description</span></td>
                                    <td class="auto-style1 ">
                                        <asp:TextBox ID="txtFiscal_Year_Description" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" ForeColor="Black" AutoPostBack="True" OnTextChanged="txtFiscal_Year_Description_TextChanged"></asp:TextBox>

                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtFiscal_Year_Description" Display="None" ErrorMessage="<b> Missing Field</b><br />A Fiscal Year Description is required." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style3"><span class="auto-style9">Fiscal Year Start Date</span></td>
                                    <td class="auto-style8">
                                       <%-- <asp:TextBox ID="txtFiscal_Year_Start_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender2" runat="server" TargetControlID="txtFiscal_Year_Start_Date" Format="dd-MMM-yyyy"/>--%>
                                        <div style="display: inline-block">

                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txtFiscal_Year_Start_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender2" PopupButtonID="imgPopup" TargetControlID="txtFiscal_Year_Start_Date" Format="dd-MMM-yyyy" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ID="Req2" ControlToValidate="txtFiscal_Year_Start_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="Req2" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                <span class="input-group-btn">
                                                    <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                    <asp:ImageButton ID="imgPopup" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                </span>
                                            </div>



                                        </div>










                                    </td>
                                    <td class="auto-style5"><strong>Fiscal Year End Date</strong></td>
                                    <td>

                                       <%-- <asp:TextBox ID="txtFiscal_Year_End_Date" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender3" runat="server" TargetControlID="txtFiscal_Year_End_Date" Format="dd-MMM-yyyy" />--%>


                                        <div style="display: inline-block">

                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txtFiscal_Year_End_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender3" PopupButtonID="ImageButton1" TargetControlID="txtFiscal_Year_End_Date" Format="dd-MMM-yyyy" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtFiscal_Year_End_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                <span class="input-group-btn">
                                                    <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                    <asp:ImageButton ID="ImageButton1" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                </span>
                                            </div>



                                        </div>



                                    </td>
                                </tr>
                                <tr>
                                    
                                    <td class="auto-style5"><strong>Period Description</strong></td>
                                    <td class="padspace">
                                        
                                        <asp:DropDownList ID="ddlPeriod_Description" CssClass="textheight" Width="250px" runat="server" OnSelectedIndexChanged="ddlPeriod_Description_SelectedIndexChanged" AutoPostBack="True">
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" InitialValue="Please select..." ControlToValidate="ddlPeriod_Description" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select period." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator4"  runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                           
                                    </td>
                                    <td class="auto-style15"><span class="auto-style9">GOC Name</span></td>
                                    <td class="auto-style16">

                                        <asp:DropDownList ID="ddlGOC_Name" CssClass="texthieht" Width="250px" runat="server"></asp:DropDownList>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style17">Fiscal Year Status</td>
                                    <td class="auto-style18">
                                        <asp:DropDownList ID="ddlFiscal_Year_Status_Name" CssClass="texthieht" Width="250px" runat="server"></asp:DropDownList>
                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator5" InitialValue="Please select..." ControlToValidate="ddlFiscal_Year_Status_Name" Display="None" ErrorMessage="<b> Missing Field</b><br />Please select Fiscal Year Status." ValidationGroup="aaa" />
                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender6" TargetControlID="RequiredFieldValidator5"  runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                    </td>
                                    <td class="auto-style3"><span class="auto-style9">User Sorting Order</span></td>
                                    <td class="auto-style8">
                                        <asp:TextBox ID="txtUser_Sorting_Order" CssClass="form-control texthieht" Width="250px" runat="server" Font-Bold="False" ForeColor="Black"></asp:TextBox>

                                    </td>
                                </tr>
                                <tr>
                                    <td class="auto-style10">Database Name</td>
                                    <td class="padspace">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblDatabase_Name" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                    <td class="auto-style11"><span class="auto-style9">User Name</span></td>
                                    <td class="auto-style12">
                                        <div class="backcolorlbl">
                                            <asp:Label ID="lblUser_Name" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </div>
                                    </td>
                                </tr>
                                <tr>

                                    <td class="auto-style13">Active</td>
                                    <td class="auto-style14">
                                        <asp:Panel ID="GrpActives" runat="server">
                                            <asp:CheckBox ID="chkActive" runat="server" Text="Active" />
                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        </asp:Panel>
                                    </td>
                                    <td class="auto-style11"><span class="auto-style9">Entry Date</span></td>
                                    <td class="auto-style12">
                                       <%-- <asp:TextBox ID="txtEntry_Date" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False"  ForeColor="Black"></asp:TextBox>
                                        <ajaxToolkit:CalendarExtender ID="CalendarExtender1" TargetControlID="txtEntry_Date" runat="server" Format="dd-MMM-yyyy" />--%>


                                        <div style="display: inline-block">

                                            <div class="input-group input-group-sm">
                                                <asp:TextBox ID="txtEntry_Date" CssClass="form-control texthieht" Width="220px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" ValidationGroup="aaa"></asp:TextBox>
                                                <ajaxToolkit:CalendarExtender ID="CalendarExtender1" PopupButtonID="ImageButton2" TargetControlID="txtEntry_Date" Format="dd-MMM-yyyy" runat="server" />
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtEntry_Date" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaa" />
                                                <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="BottomLeft"></ajaxToolkit:ValidatorCalloutExtender>
                                                <span class="input-group-btn">
                                                    <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                    <asp:ImageButton ID="ImageButton2" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                </span>
                                            </div>



                                        </div>


                                    </td>
                                </tr>

                                <tr>

                                    <td class="auto-style13">System Date</td>
                                    <td class="auto-style14">
                                        <%--<asp:TextBox ID="txtCompany_Name" CssClass="form-control" Width="250px" runat="server" Font-Bold="False" Font-Italic="True" ForeColor="Black"></asp:TextBox>--%>
                                        <div class="backcolorlbl texthieht">
                                            <asp:Label ID="lblSystem_Date" runat="server" Font-Bold="False" ForeColor="Black"></asp:Label>
                                        </div>

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


                                        <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="FiscalYearID" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="FiscalYearID" HeaderText="ID" ReadOnly="True" SortExpression="FiscalYearID" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description">
                                                <HeaderStyle Width="500px" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <%-- Table View --%>

                            <div id="Deletebox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <asp:GridView ID="GridView2" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="FiscalYearID" OnSelectedIndexChanged="GridView2_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView2_RowDataBound">
                                            <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="FiscalYearID" HeaderText="ID" ReadOnly="True" SortExpression="FiscalYearID" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description">
                                                <HeaderStyle Width="500px" />
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
                                    <!-- /.box-body -->
                                </div>
                            </div>

                            <div id="Editbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>
                                        <asp:GridView ID="GridView3" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" DataKeyNames="FiscalYearID" OnSelectedIndexChanged="GridView3_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" OnRowDataBound="GridView3_RowDataBound">
                                           <Columns>
                                                <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                <HeaderStyle Width="20px" />
                                                </asp:CommandField>
                                                <asp:BoundField DataField="FiscalYearID" HeaderText="ID" ReadOnly="True" SortExpression="FiscalYearID" InsertVisible="False" >
                                                <HeaderStyle Width="20px" />
                                                </asp:BoundField>
                                                <asp:BoundField DataField="Description" HeaderText="Fiscal Year" SortExpression="Description">
                                                <HeaderStyle Width="500px" />
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
                            <td><strong>Fiscal Year</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_FYID" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtEdit_FYID" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Start Date</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_STartDAte" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtEdit_STartDAte" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender6" PopupButtonID="ImageButton5" TargetControlID="txtEdit_STartDAte" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator7" ControlToValidate="txtEdit_STartDAte" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender8" TargetControlID="RequiredFieldValidator7" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton5" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Fiscal Year Status Name </strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtEdit_FYSN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtEdit_FYSN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td><strong>Fiscal Year ID</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_FYid" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtFind_FYid" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Start Date</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_SD" CssClass="form-control gap" runat="server"></asp:TextBox>--%>

                                <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtFind_SD" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender5" PopupButtonID="ImageButton3" TargetControlID="txtFind_SD" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator6" ControlToValidate="txtFind_SD" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender7" TargetControlID="RequiredFieldValidator6" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton3" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>

                            </td>

                        </tr>
                         <tr>
                            <td><strong>Fiscal Year Status Name </strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtFind_FYsn" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtFind_FYsn" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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
                            <td><strong>Fiscal Year</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_FYID" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                  <asp:DropDownList ID="txtDelete_FYID" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
                            </td>

                        </tr>
                         <tr>
                            <td><strong>Start Date</strong> </td>
                            <td>
                               <%-- <asp:TextBox ID="txtDelete_SD" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                            
                                <div style="display:inline-block">

                                                        <div class="input-group input-group-sm">
                                                            <asp:TextBox ID="txtDelete_SD" CssClass="form-control texthieht" Width="420px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black"  ValidationGroup="aaab"></asp:TextBox>
                                                            <ajaxToolkit:CalendarExtender ID="CalendarExtender4" PopupButtonID="ImageButton4" TargetControlID="txtDelete_SD" Format="dd-MMM-yyyy" runat="server" />
                                                        <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator11" ControlToValidate="txtDelete_SD" Display="None" ErrorMessage="<b> Missing Field</b><br />A Date is required." ValidationGroup="aaab" />
                                                        <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender13" TargetControlID="RequiredFieldValidator11" runat="server" HighlightCssClass="vali"></ajaxToolkit:ValidatorCalloutExtender>
                                                            <span class="input-group-btn">
                                                                <%--<button type="button" class="btn btn-info btn-flat">Go!</button>--%>
                                                                <asp:ImageButton ID="ImageButton4" CssClass="btn btn-flat" ImageUrl="img/calendar.png" ImageAlign="Bottom" runat="server" />
                                                            </span>
                                                       </div>
                                                       
                                                    </div>

                            </td>

                        </tr>
                         <tr>
                            <td><strong>Fiscal Year Status</strong> </td>
                            <td>
                                <%--<asp:TextBox ID="txtDelete_FYSN" CssClass="form-control gap" runat="server"></asp:TextBox>--%>
                                 <asp:DropDownList ID="txtDelete_FYSN" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>
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


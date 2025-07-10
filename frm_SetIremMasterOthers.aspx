<%@ Page Title="" Language="C#" MasterPageFile="~/Site.master" AutoEventWireup="true" CodeFile="frm_SetIremMasterOthers.aspx.cs" Inherits="frm_Period" %>

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
                                <h3 class="box-title" style="color: #fff; padding-left: 10px;"><strong>ITEM MASTER OTHER</strong></h3>
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
                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                    <ContentTemplate>
                                        <table id="tablecontent1" runat="server" style="width: 100%;">
                                            <tr>
                                                <td class="auto-style6"><strong>Item Code</strong></td>
                                                <td class="auto-style11">


                                                    <telerik:RadComboBox RenderMode="Lightweight" ID="RadComboItem_Code" runat="server" Height="200" Width="250"
                                                        DropDownWidth="500" EmptyMessage="Choose a Item Code" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        AutoPostBack="true" 
                                                        OnItemsRequested="RadComboItem_Code_ItemsRequested"
                                                        Label="">
                                                        <HeaderTemplate>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">Item Code
                                                                    </td>
                                                                    <td style="width: 175px;">Description
                                                                    </td>
                                                                    <td style="width: 40px;">Local/Import
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 175px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Description']")%>
                                                                    </td>
                                                                    <td style="width: 60px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Local_Import']")%>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                                    <%--<asp:TextBox ID="txtItemCode" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>--%>
                                                </td>
                                                <td class="auto-style6"><strong>Part No detail</strong></td>
                                                <td class="auto-style11">

                                                    <asp:TextBox ID="txtPartNoDetail" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                                    <%--<asp:DropDownList ID="ddlItemTypes" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddlItemTypes" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Item Type." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>--%>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style6"><strong>Department</strong></td>
                                                <td class="auto-style11">
                                                   <asp:DropDownList ID="ddlDepartment" Width="250px" CssClass=" form-control select2" runat="server" ValidationGroup="aaa"  ></asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ID="Req" ControlToValidate="ddlDepartment" InitialValue="Please select..." Display="None" ErrorMessage="<b> Missing Field</b><br />Please Select Item Type." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender1" TargetControlID="Req" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>

                                            </tr>
                                            <tr>
                                                <td class="auto-style6"><strong>Location 1</strong></td>
                                                <td class="auto-style11">
                                                   <asp:TextBox ID="txtLoctaion1" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                                <td class="auto-style6"><strong>Location 2</strong></td>
                                                <td class="auto-style11">
                                                  <asp:TextBox ID="txtLoctaion2" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="auto-style6"><strong>Location 3</strong></td>
                                                <td class="auto-style11">
                                                   <asp:TextBox ID="txtLoctaion3" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                                <td class="auto-style6"><strong>Location 4</strong></td>
                                                <td class="auto-style11">
                                                 
                                                    <asp:TextBox ID="txtLoctaion4" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                </td>
                                            </tr>
                                             <tr>
                                                <td class="auto-style6"><strong>Minimum Stock Level</strong></td>
                                                <td class="auto-style11">
                                                  <asp:TextBox ID="txtMiniStockLevel" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender1" FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMiniStockLevel" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtMiniStockLevel"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Input Minimum Stock Level." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender2" TargetControlID="RequiredFieldValidator1" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style6"><strong>Maximum Stock Level</strong></td>
                                                <td class="auto-style11">
                                                  <asp:TextBox ID="txtMaxStockLevel" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender2" FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtMaxStockLevel" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtMaxStockLevel"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Input Maximum Stock Level." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender3" TargetControlID="RequiredFieldValidator2" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                            </tr>
                                            
                                            <tr>
                                                <td class="auto-style6"><strong>Re-Order Stock level</strong></td>
                                                <td class="auto-style11">
                                                 <asp:TextBox ID="txtRecordStocklevel" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender3" FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtRecordStocklevel" runat="server" />
                                                     <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtRecordStocklevel"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Input Re-Order Stock level." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender4" TargetControlID="RequiredFieldValidator3" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
                                                </td>
                                                <td class="auto-style6"><strong>Re-Order Quantity</strong></td>
                                                <td class="auto-style11">
                                                 <asp:TextBox ID="txtRecQuantity" CssClass="texthieht" Width="250px" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>
                                                    <ajaxToolkit:FilteredTextBoxExtender ID="FilteredTextBoxExtender4" FilterType="Custom, Numbers" ValidChars="."  TargetControlID="txtRecQuantity" runat="server" />
                                                    <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator4" ControlToValidate="txtRecQuantity"  Display="None" ErrorMessage="<b> Missing Field</b><br />Please Input Re-Order Quantity." ValidationGroup="aaa" />
                                                    <ajaxToolkit:ValidatorCalloutExtender ID="ValidatorCalloutExtender5" TargetControlID="RequiredFieldValidator4" runat="server" HighlightCssClass="vali" PopupPosition="Left"></ajaxToolkit:ValidatorCalloutExtender>
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
                                             <tr>
                                                <td class="auto-style6"><strong>Active</strong></td>
                                                <td class="auto-style11">
                                                    <asp:Panel ID="Panel1" runat="server">
                                                        <asp:CheckBox ID="chkActive" runat="server" Text="Active" TabIndex="3" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </asp:Panel>


                                                </td>
                                            </tr>
                                                                                          

                                        </table>
                                        
                                        
                                    </ContentTemplate>
                                    
                                </asp:UpdatePanel>

                            </div>


                            <%-- Table View --%>
                            <div id="Findbox" runat="server">
                                <div class="box">

                                    <!-- /.box-header -->
                                    <div class="box-body" style="width: 100%; overflow: auto; height: 50%;">
                                        <%--CssClass="table table-bordered table-hover"--%>

                                        <div style="width: 100%; height: 400px; overflow: scroll">

                                            <asp:GridView ID="GridView1" runat="server" CssClass="table table-bordered table-hover tableFixHead" AutoGenerateColumns="False" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" BackColor="White" BorderColor="#CCCCCC" BorderStyle="None" BorderWidth="1px" CellPadding="4" ForeColor="Black" GridLines="Horizontal" DataKeyNames="ItemMasterOthersID" >
                                                <Columns>
                                                    <asp:CommandField ShowSelectButton="True" ButtonType="Image" SelectImageUrl="~/dist/img/edit.png" >
                                                    <HeaderStyle Width="20px" />
                                                    </asp:CommandField>
                                                    <asp:BoundField DataField="ItemMasterOthersID" HeaderText="ID" SortExpression="ItemMasterOthersID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" >
                                                    <HeaderStyle Width="400px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PartNo_Detail" HeaderText="Part No Detail" SortExpression="PartNo_Detail" >
                                                    <HeaderStyle Width="60px" />
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
                                                    <asp:BoundField DataField="ItemMasterOthersID" HeaderText="ID" SortExpression="ItemMasterOthersID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" >
                                                    <HeaderStyle Width="400px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PartNo_Detail" HeaderText="Part No Detail" SortExpression="PartNo_Detail" >
                                                    <HeaderStyle Width="60px" />
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
                                                    <asp:BoundField DataField="ItemMasterOthersID" HeaderText="ID" SortExpression="ItemMasterOthersID" InsertVisible="False" ReadOnly="True">
                                                    <HeaderStyle Width="20px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="ItemCode" HeaderText="Item Code" SortExpression="ItemCode" >
                                                    <HeaderStyle Width="400px" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PartNo_Detail" HeaderText="Part No Detail" SortExpression="PartNo_Detail" >
                                                    <HeaderStyle Width="60px" />
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
                            <td class="rowpadd"><strong>Item Code</strong> </td>
                            <td class="rowpadd">
                                  <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                    <ContentTemplate>
                                 <telerik:RadComboBox RenderMode="Lightweight" ID="radedit_itemCode" runat="server" Height="200" Width="100%"
                                                        DropDownWidth="500" EmptyMessage="Choose a Item Code" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        AutoPostBack="true" 
                                                        OnItemsRequested="radedit_itemCode_ItemsRequested"
                                                        Label="">
                                                        <HeaderTemplate>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">Item Code
                                                                    </td>
                                                                    <td style="width: 175px;">Description
                                                                    </td>
                                                                    <td style="width: 40px;">Local/Import
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 175px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Description']")%>
                                                                    </td>
                                                                    <td style="width: 60px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Local_Import']")%>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                <%--<asp:DropDownList ID="ddlEdit_ItemCode" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                           </ContentTemplate>
                                      </asp:UpdatePanel>
                                        
                                         </td>
                        </tr>

                        <tr>

                            <td class="rowpadd"><strong>Part No Detail</strong> </td>
                            <td class="rowpadd">

                                <%--<asp:DropDownList ID="ddlEdit_ItemType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtEdit_PartNoDetail" CssClass="texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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
                            <td class="rowpadd"><strong>Item Code</strong> </td>
                            <td class="rowpadd">
                                  <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                    <ContentTemplate>
                               <telerik:RadComboBox RenderMode="Lightweight" ID="radFindItem_Code" runat="server" Height="200" Width="100%"
                                                        DropDownWidth="500" EmptyMessage="Choose a Item Code" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        AutoPostBack="true" 
                                                        OnItemsRequested="radFindItem_Code_ItemsRequested"
                                                        Label="">
                                                        <HeaderTemplate>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">Item Code
                                                                    </td>
                                                                    <td style="width: 175px;">Description
                                                                    </td>
                                                                    <td style="width: 40px;">Local/Import
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 175px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Description']")%>
                                                                    </td>
                                                                    <td style="width: 60px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Local_Import']")%>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                </ContentTemplate>
                                      </asp:UpdatePanel>
                                <%-- <asp:DropDownList ID="ddlFind_ItemCode" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                            </td>
                        </tr>

                        <tr>

                            <td class="rowpadd"><strong>Part No Detail</strong> </td>
                            <td class="rowpadd">

                                <%--<asp:DropDownList ID="ddlEdit_ItemType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtFind_PartNoDetail" CssClass="texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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
                            <td class="rowpadd"><strong>Item Code</strong> </td>
                            <td class="rowpadd">
                                 <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                    <ContentTemplate>
                                   <telerik:RadComboBox RenderMode="Lightweight" ID="radDel_ItemCode" runat="server" Height="200" Width="100%"
                                                        DropDownWidth="500" EmptyMessage="Choose a Item Code" HighlightTemplatedItems="true"
                                                        EnableLoadOnDemand="true" Filter="StartsWith"
                                                        AutoPostBack="true" 
                                                        OnItemsRequested="radDel_ItemCode_ItemsRequested"
                                                        Label="">
                                                        <HeaderTemplate>
                                                            <table style="width: 100%" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">Item Code
                                                                    </td>
                                                                    <td style="width: 175px;">Description
                                                                    </td>
                                                                    <td style="width: 40px;">Local/Import
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </HeaderTemplate>
                                                        <ItemTemplate>
                                                            <table style="width: 500px" cellspacing="0" cellpadding="0">
                                                                <tr>
                                                                    <td style="width: 100px;">
                                                                        <%# DataBinder.Eval(Container, "Text")%>
                                                                    </td>
                                                                    <td style="width: 175px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Description']")%>
                                                                    </td>
                                                                    <td style="width: 60px;">
                                                                        <%# DataBinder.Eval(Container, "Attributes['Local_Import']")%>
                                                                    </td>
                                                                   
                                                                </tr>
                                                            </table>
                                                        </ItemTemplate>
                                                    </telerik:RadComboBox>
                                <%--<asp:DropDownList ID="ddlDel_ItemCode" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                            </ContentTemplate>
                                        </asp:UpdatePanel>
                                        </td>
                        </tr>

                        <tr>

                            <td class="rowpadd"><strong>Part No Detail</strong> </td>
                            <td class="rowpadd">

                                <%--<asp:DropDownList ID="ddlEdit_ItemType" CssClass="form-control select2" Width="100%" runat="server"></asp:DropDownList>--%>
                                <asp:TextBox ID="txtDel_PartNoDetail" CssClass="texthieht" Width="100%" runat="server" Font-Bold="False" Font-Italic="False" ForeColor="Black" Height="25px"></asp:TextBox>

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


</asp:Content>


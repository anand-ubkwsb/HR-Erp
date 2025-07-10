using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class frm_UserGrp_Menu : System.Web.UI.Page
{
    DmlOperation dml = new DmlOperation();
    protected void Page_Load(object sender, EventArgs e)
    {
        dml.dropdownsql(ddlUsrGrpN, "SET_UserGrp", "UserGrpName", "UserGrpId");
        dml.dropdownsql(ddlModule, "SET_Module", "ModuleDescription", "ModuleId");
        dml.dropdownsql(ddlMenu, "SET_Menu", "Menu_title", "menuId");
        
        
    }

    protected void btnInsert_Click(object sender, EventArgs e)
    {

    }

    protected void btnSave_Click(object sender, EventArgs e)
    {

    }

    protected void btnUpdate_Click(object sender, EventArgs e)
    {

    }

    protected void btnCancel_Click(object sender, EventArgs e)
    {

    }

    protected void btnSearchEdit_Click(object sender, EventArgs e)
    {

    }

    protected void btn_Search_Click(object sender, EventArgs e)
    {

    }

    protected void btn_delete_Click(object sender, EventArgs e)
    {

    }

    protected void GridView1_SelectedIndexChanged(object sender, EventArgs e)
    {

    }
}
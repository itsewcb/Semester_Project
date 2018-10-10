using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class _Default : System.Web.UI.Page
{
    //setting acount/login stuff at the top of page
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Session["pk_User_ID"] == null)
        {
            Is_Signed_In.Visible = false;
        }
        else
        {
            Not_Signed_In.Visible = false;
        }
    }

    protected void LinkButton1_Click(object sender, EventArgs e)
    {
        
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }
}
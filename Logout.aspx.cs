using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class Logout : System.Web.UI.Page
{
    //Loads page and sets sessions to null
    protected void Page_Load(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["Username"] = null;
        Session["pk_USer_ID"] = null;
        Session["Date_User"] = null;
        Response.Redirect("Default.aspx");
    }
}
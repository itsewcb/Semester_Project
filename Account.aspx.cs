using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Account : System.Web.UI.Page
{
    //Jump back to main page if session is null
    protected void Page_Load(object sender, EventArgs e)
    {
        if(Session["pk_User_ID"] == null)
        {
            Response.Redirect("Login.aspx");
        }
        //make the "Modify Account Page" not visible
        divChange.Visible = false;

        //fills labels
        LabelEmail.Text = Session["Email"].ToString();
        LabelUsername.Text = Session["Username"].ToString();
        LabelCreation.Text = Session["Date_User"].ToString();
    }

    //Open the "Modify Account Page" by making the main account page invisible
    protected void ButtonModify_Click(object sender, EventArgs e)
    {
        divChange.Visible = true;
        divCurrent.Visible = false;

        tboxEmail.Text = Session["Email"].ToString();
        tboxUsername.Text = Session["Username"].ToString();
    }

    //Goes back to normal account page and makes textboxes and error label blank
    protected void ButtonCancel_Click(object sender, EventArgs e)
    {
        divChange.Visible = false;
        divCurrent.Visible = true;

        LabelError.Text = "";
        tboxCurrentPW.Text = "";
        tboxEmail.Text = "";
        tboxPassword.Text = "";
        tboxUsername.Text = "";
    }

    //change info button
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Checks if any missing any info
        if (tboxUsername.Text == "" || tboxEmail.Text == "" || tboxCurrentPW.Text =="")
        {
            LabelError.Text = "You need to enter your current password plus a email address and username";
            divChange.Visible = true;
            divCurrent.Visible = false;
            return;
        }

        //first check if if user entered the right password
        if (password_validaton())
        {
            //data source
            SqlDataSource objDS = new SqlDataSource();

            //connection strings
            objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
            objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

            //updating data base
            objDS.UpdateCommand = "Update Forum_User " +
                                    "SET [Password]=?, [Username]=?, [Email]=?" +
                                    "WHERE pk_User_ID=?";

            objDS.UpdateParameters.Add("[Password]", tboxPassword.Text);
            objDS.UpdateParameters.Add("[Username]", tboxUsername.Text);
            objDS.UpdateParameters.Add("[Email]", tboxEmail.Text);
            objDS.UpdateParameters.Add("[pk_User_ID]", Session["pk_User_ID"].ToString());
            objDS.Update();

            //update current session
            Session["Username"] = tboxUsername.Text;
            Session["Password"] = tboxPassword.Text;
            Session["Email"] = tboxEmail.Text;     

            //Resetting password and labels
            tboxPassword.Text = "";
            tboxCurrentPW.Text = "";
            LabelError.Text = "Success";
            Response.Redirect("Account.aspx");

        }
        else
        {
            //failures
            LabelError.Text = "Unsuccessful";
            divChange.Visible = true;
            divCurrent.Visible = false;
        }
    }

    protected void ButtonLogout_Click(object sender, EventArgs e)
    {
        Session["Email"] = null;
        Session["Username"] = null;
        Session["pk_User_ID"] = null;
        Response.Redirect("Default.aspx");
    }

    //if user decides to delete account
    protected void ButtonDelete_Click(object sender, EventArgs e)
    {
        //check is password has been entered
        if (tboxCurrentPW.Text == "")
        {
            LabelError.Text = "You need to enter your password in order to delete your account";
            divChange.Visible = true;
            divCurrent.Visible = false;
            return;
        }

        //check if password is correct
        if(password_validaton())
        {
            SqlDataSource objDS = new SqlDataSource();

            objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
            objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

            //deleted the account and goes to logout, this will null the session values
            objDS.DeleteCommand = "DELETE FROM [Forum_User]" +
                                     "WHERE [pk_User_ID]=[?]";
            objDS.DeleteParameters.Add("pk_Thread_ID", Session["pk_User_ID"].ToString());
            objDS.Delete();

            Response.Redirect("Logout.aspx");
        }
        else
        {
            LabelError.Text = "You entered your password incorrectly";
            divChange.Visible = true;
            divCurrent.Visible = false;
        }


    }

    //Password Validation Function
    public bool password_validaton()
    {
        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        objDS.SelectCommand = "select *" +
                                "from [Forum_User] " +
                                "where [pk_User_ID]=? and [Password]=?";

        objDS.SelectParameters.Add("[pk_User_ID]", Session["pk_User_ID"].ToString());
        objDS.SelectParameters.Add("[Password]", tboxCurrentPW.Text);


        objDS.DataSourceMode = SqlDataSourceMode.DataReader;

        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);

        tboxPassword.Text = tboxCurrentPW.Text;

        if (myData.Read())
            return true;
        else
            return false;
    }
}
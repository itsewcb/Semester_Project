using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class SignUp : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    //Goes to login if user has account
    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        Response.Redirect("Login.aspx");
    }

    //Signs a person up
    protected void ButtonSignUp_Click(object sender, EventArgs e)
    {
        //First check if passwords match
        if(tboxConfirmation.Text != tboxPassword.Text)
        {
            LabelError.Text = "Password does not match";
            return;
        }

        //Create Current Date
        DateTime myDateTime = DateTime.Now;
        string sqlDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        SqlDataSource objDS = new SqlDataSource
        {
            ProviderName = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName,
            ConnectionString = System.Configuration.ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString,

            InsertCommand = "INSERT INTO [Forum_User] ([Email], [Username], [Password], [Date_User]) VALUES (?,?,?,?)"

        };

        objDS.InsertParameters.Add("Email", tboxEmail.Text);
        objDS.InsertParameters.Add("Username", tboxUsername.Text);
        objDS.InsertParameters.Add("Password", tboxPassword.Text);
        objDS.InsertParameters.Add("Date_User", sqlDate);

        //load user in, checks if the user user entered all necesary info
        try
        {
            objDS.Insert();
            Response.Redirect("Login.aspx");
        }

        catch
        {
            LabelError.Text = "Missing Email, Username, and/or password";
        }

    }

}
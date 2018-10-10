using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    
    protected void ButtonLogin_Click(object sender, EventArgs e)
    {
        //Check if user name or password is missing
        if (tboxPassword.Text == "" || tboxUsername.Text == "")
        {
            LabelError.Text = "Missing a Username or Password";
            return;
        }

        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        objDS.SelectCommand = "SELECT[pk_User_ID], [Email], [Username], [Date_User]" +
                                "FROM [Forum_User]" +
                                "WHERE[Username]=[Usr] and[Password]=[Psw]";


        objDS.SelectParameters.Add("[Username]", tboxUsername.Text);
        objDS.SelectParameters.Add("[Password]", tboxPassword.Text);

        objDS.DataSourceMode = SqlDataSourceMode.DataReader;
        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);

        //Check to see if you entered your user name or password incorrectly, if you didnt then it logs you in
        if (myData.Read())
        {
            Session["pk_User_ID"] = myData[0];
            Session["Email"] = myData[1];
            Session["Username"] = myData[2];
            Session["Date_User"] = myData[3]; 

            Response.Redirect("Default.aspx");
        }
        else
        {
            LabelError.Text = "Wrong username or password";
        }
    }

    //Goes to signup page if user doesnt have account
    protected void Button2_Click(object sender, EventArgs e)
    {
        Response.Redirect("Signup.aspx");
    }
}
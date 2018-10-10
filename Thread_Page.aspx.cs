using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Thread_Page : System.Web.UI.Page
{
    string qryStr;

    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["pk_Thread_ID"] != null || Request.QueryString["pk_Board_ID"] != null)
            qryStr = Request.QueryString["pk_Thread_ID"].ToString();
        else
            Response.Redirect("Default.aspx");

        //checking if viewer is signed in/has account
        if (Session["pk_User_ID"] == null)
        {        
            //make login and signup visible
            Is_Signed_In.Visible = false;
            myButton.Visible = false;
        }
        else
        {
            //Make account and logout visible
            Not_Signed_In.Visible = false;

            ////Check to see if the person viewing is the owner of the thread
            SqlDataSource objDS1 = new SqlDataSource();

            objDS1.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
            objDS1.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

            objDS1.SelectCommand = "SELECT[Thread_Title]" +
                                    "FROM [Thread]" +
                                    "WHERE[pk_Thread_ID] =[Trd] and[fk_User_ID] =[Usr]";

            objDS1.SelectParameters.Add("[pk_Thread_ID]", qryStr);
            objDS1.SelectParameters.Add("[fk_User_ID]", Session["pk_User_ID"].ToString());
            
            objDS1.DataSourceMode = SqlDataSourceMode.DataReader;
            IDataReader myData = (IDataReader)objDS1.Select(DataSourceSelectArguments.Empty);

            //if they are make the delete button visible
            if (myData.Read() == true)
                myButton.Visible = true;
            else
                myButton.Visible = false;     
        }
        //runs a half working function
        thread_counter();
    }

    //Reply Button
    protected void LinkButton1_Click1(object sender, EventArgs e)
    {
        //Checks if user is logged in, if logged in then show the reply div
        if (Session["pk_User_ID"] == null)
            Response.Redirect("Login.aspx");

        Reply_Box.Visible = true;
    }

    //Cancel Button
    protected void Button2_Click(object sender, EventArgs e)
    {
        //removes all the text and closes reply div
        Reply_Box.Visible = false;

        TextArea1.InnerText = "";
        LabelError.Text = "";
    }

    //Post a reply Button
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Create Current Date
        DateTime myDateTime = DateTime.Now;
        string sqlDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        SqlDataSource objDS = new SqlDataSource
        {
            ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName,
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString,

            InsertCommand = "INSERT INTO [Thread_Comment] ([Comment], [fk_User_ID], [fk_Thread_ID], [Date_Comment]) VALUES (?,?,?,?)"
        };

        //Inserts a comment into the thread
        objDS.InsertParameters.Add("Comment", TextArea1.InnerText);
        objDS.InsertParameters.Add("fk_User_ID", Session["pk_User_ID"].ToString());
        objDS.InsertParameters.Add("fk_Thread_ID", qryStr);
        objDS.InsertParameters.Add("Date_Comment", sqlDate);

        try
        {
            //check any errors, reloads page if worked
            objDS.Insert();
            Reply_Box.Visible = false;
            TextArea1.InnerText = "";
            LabelError.Text = "";
            Response.Redirect($"Thread_Page.aspx?pk_Thread_ID={qryStr}");
        }

        catch
        {
            LabelError.Text = "No Empty Comments";
        }
    }

    //Button for deleting thread, is visible/active if they own the thread, otherwise invisible
    protected void DeleteButton_Click(object sender, EventArgs e)
    {
        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        //Delete the thread itself
        objDS.DeleteCommand = "DELETE FROM [Thread]" +
                                 "WHERE [pk_Thread_ID]=[?]";
        objDS.DeleteParameters.Add ("pk_Thread_ID", qryStr);
        objDS.Delete();

        //Delete all comments in the thread
        objDS.DeleteCommand = "DELETE FROM  [Thread_Comment]" +
                                 "WHERE [fk_Thread_ID]=[?]";
        objDS.DeleteParameters.Add ("fk_Thread_ID", qryStr);
        objDS.Delete(); 

        Response.Redirect("Default.aspx");  
    }

    protected void thread_counter()
    {
        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;
        objDS.DataSourceMode = SqlDataSourceMode.DataReader;

        //Number of comments in thread
        objDS.SelectCommand = "SELECT COUNT(*)" +
                                "FROM [Thread_Comment]" +
                                "WHERE [fk_Thread_ID]=[Trd]";
        objDS.SelectParameters.Add("[fk_Thread_ID]", qryStr);
        
        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);
        myData.Read();
        LabelComments.Text = myData[0].ToString();

        ////IDK Why this wont work, I checked the sql in access and its fine. 
        ////Number of unique posters
        //objDS.SelectCommand = "SELECT COUNT(*) AS NameCount" +
        //                        "FROM(SELECT DISTINCT fk_User_ID" +
        //                                "FROM Thread_Comment" +
        //                                "WHERE fk_Thread_ID =[Trd])  AS DistinctName";
        //objDS.SelectParameters.Add("[fk_Thread_ID]", qryStr);

        //objDS.DataSourceMode = SqlDataSourceMode.DataReader;
        //myData.Read();
        //LabelUnique.Text = myData[0].ToString();

    }


    //Go back to board
    protected void LinkButton2_Click(object sender, EventArgs e)
    {
        SqlDataSource objDS = new SqlDataSource();
        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        //looks at current thread and grabs board ID to go back a page
        objDS.SelectCommand = "SELECT Thread.[fk_Board_ID], Thread.[pk_Thread_ID]" +
                                "FROM [Thread]" +
                                "WHERE pk_Thread_ID =[?]";

        objDS.SelectParameters.Add("[pk_Thread_ID]", qryStr);

        objDS.DataSourceMode = SqlDataSourceMode.DataReader;
        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);
        myData.Read();
        
        Response.Redirect($"Board.aspx?pk_Board_ID={myData[0]}");
    }
}
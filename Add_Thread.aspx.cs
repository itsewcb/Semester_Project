using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Configuration;
using System.Data;

public partial class Add_Thread : System.Web.UI.Page
{
    //saves query string for later use
    private string qryStr;

    protected void Page_Load(object sender, EventArgs e)
    {
        //checks if query string has a value passed in, if it does check if session is null
        if (Request.QueryString["pk_Board_ID"] != null)
            qryStr = Request.QueryString["pk_Board_ID"].ToString();
        else
            Response.Redirect("Default.aspx");

        if (Session["pk_User_ID"] == null)
            Response.Redirect("Login.aspx");

        //Population Label at top of page
        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        objDS.SelectCommand = "SELECT[Board_Name]" +
                                "FROM [Board]" +
                                "WHERE[pk_Board_ID]=[Brd]";
        objDS.SelectParameters.Add("[Username]", qryStr);

        objDS.DataSourceMode = SqlDataSourceMode.DataReader;
        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);

        myData.Read();
        LabelTitle.Text = myData[0].ToString();


    }

    //Add a new thread to the board
    protected void Button1_Click(object sender, EventArgs e)
    {
        //Create Current Date
        DateTime myDateTime = DateTime.Now;
        string sqlDate = myDateTime.ToString("yyyy-MM-dd HH:mm:ss");

        SqlDataSource objDS = new SqlDataSource
        {
            ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName,
            ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString,

            InsertCommand = "INSERT INTO [Thread] ([fk_Board_ID], [fk_User_ID], [Thread_Title], [First_Comment], [Date_Thread]) VALUES (?,?,?,?,?)"

        };

        objDS.InsertParameters.Add("fk_Board_ID", qryStr);
        objDS.InsertParameters.Add("Email", Session["pk_User_ID"].ToString());
        objDS.InsertParameters.Add("Username", tboxTitle.Text);
        objDS.InsertParameters.Add("Password", TextArea1.InnerText);
        objDS.InsertParameters.Add("Date_Thread", sqlDate);

        //Check if all value are filled
        try
        {
            objDS.Insert();
            //Goes back a page with board ID
            Response.Redirect($"Board.aspx?pk_Board_ID={qryStr}");

        }

        catch
        {
            LabelError.Text = "You need a title and opening comment";
        }
    }
}
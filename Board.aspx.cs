using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;

public partial class Board : System.Web.UI.Page
{
    //Board ID for later use
    string qryStr;

    protected void Page_Load(object sender, EventArgs e)
    {
        //Checks if received board id if it did then load it into aryStr otherwise go back to homepage
        if (Request.QueryString["pk_Board_ID"] != null)
            qryStr = Request.QueryString["pk_Board_ID"].ToString();
        else
            Response.Redirect("Default.aspx");

        //Checks if user is logged in to make account/login stuff show
        if (Session["pk_User_ID"] == null)
            Is_Signed_In.Visible = false;
        else
            Not_Signed_In.Visible = false;

        SqlDataSource objDS = new SqlDataSource();

        objDS.ProviderName = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ProviderName;
        objDS.ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString_myDatabase"].ConnectionString;

        //Loads threads with board ID
        objDS.SelectCommand = "SELECT[Board_Name]" +
                                "FROM [Board]" +
                                "WHERE[pk_Board_ID]=[Usr]";
        objDS.SelectParameters.Add("[Username]", qryStr);

        objDS.DataSourceMode = SqlDataSourceMode.DataReader;
        IDataReader myData = (IDataReader)objDS.Select(DataSourceSelectArguments.Empty);
        myData.Read();

        Label_Title_Board.Text = myData[0].ToString();
    }

    //Goes to add a thread page and brings board id
    protected void LinkButton_Start_Thread_Click(object sender, EventArgs e)
    {
        Response.Redirect($"Add_Thread.aspx?pk_Board_ID={qryStr}");
    }

    protected void SqlDataSource1_Selecting(object sender, SqlDataSourceSelectingEventArgs e)
    {

    }


}
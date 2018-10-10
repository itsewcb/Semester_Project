<%@ Page Language="C#"  MasterPageFile="~/MasterPage.master"  AutoEventWireup="true" CodeFile="Board.aspx.cs" Inherits="Board" %>

<asp:Content ID="login" ContentPlaceHolderID="head" Runat="Server">
         <div id ="Not_Signed_In" runat="server">
            <asp:HyperLink Href="Login.aspx" ID="HyperLink1" runat="server">Login</asp:HyperLink> | 
            <asp:HyperLink Href="Signup.aspx" ID="HyperLink2" runat="server">Sign Up</asp:HyperLink>
        </div>
        <div id="Is_Signed_In" runat="server">           
            <asp:HyperLink Href="Account.aspx" ID="HyperLink3" runat="server">Account</asp:HyperLink> | 
            <asp:HyperLink Href="Logout.aspx" ID="HyperLink4" runat="server">Logout</asp:HyperLink>
        </div>
</asp:Content> 




<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" Runat="Server">
    <form id="Board_Top" runat="server">
        <p>
            <asp:Label ID="Label_Title_Board" runat="server" Font-Bold="True" Font-Size="X-Large"></asp:Label>
        </p>
        <p>
            [<asp:LinkButton Class="Create_Thread" ID="LinkButton_Start_Thread" runat="server" Font-Bold="True" Font-Size="Larger" OnClick="LinkButton_Start_Thread_Click">Start Thread</asp:LinkButton>]
        </p>
    </form>
    <div id = "myContent1">
        <asp:Repeater ID="Repeater1" runat="server" DataSourceID="SqlDataSource1">
            <ItemTemplate>
                <div class ="Thread">                 
                    <table id ="Table1">
                        <tr><td id = "Thread_Title"><span id="Username"><%#Eval("Username") %></span>  | <%#Eval("Thread_Title") %> | <%#Eval("Date_Thread")%>
                        [<asp:HyperLink class ="ViewLink" NavigateUrl='<%#Eval("pk_Thread_ID", "Thread_Page.aspx?pk_Thread_ID={0}") %>' runat="server">View</asp:HyperLink>]</td></tr>
                        <tr><td id="Thread_First_Comment"><%#Eval("First_Comment") %></td></tr>
                    </table>
                </div>
            </ItemTemplate>
        </asp:Repeater>
    </div>
    <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:ConnectionString_myDatabase %>" ProviderName="<%$ ConnectionStrings:ConnectionString_myDatabase.ProviderName %>" SelectCommand="SELECT Thread.pk_Thread_ID, Board.pk_Board_ID, Thread.Thread_Title, Thread.First_Comment, Thread.Date_Thread, Forum_User.Username FROM ((Thread INNER JOIN Forum_User ON Thread.fk_User_ID = Forum_User.pk_User_ID) INNER JOIN Board ON Thread.fk_Board_ID = Board.pk_Board_ID) WHERE (Thread.fk_Board_ID = ?)" OnSelecting="SqlDataSource1_Selecting">
        <SelectParameters>
            <asp:QueryStringParameter Name="?" QueryStringField="pk_Board_ID" />
        </SelectParameters>
    </asp:SqlDataSource>
</asp:Content> 

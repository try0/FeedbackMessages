<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="FeedbackMessages.Example.WebForms.FirstPage" %>

<%@ Register Assembly="FeedbackMessages" Namespace="FeedbackMessages.Components" TagPrefix="fm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <!-- feedback message area -->
    <fm:FeedbackMessagePanel runat="server" ID="FeedbackMessagePanel"></fm:FeedbackMessagePanel>


    <!-- simple form -->
    <div class="ui form" style="padding-top:3em;">
        <div class="field"  >
            <asp:TextBox runat="server" ID="Message" Rows="4" TextMode="MultiLine"></asp:TextBox>
        </div>

        <div class="field">
            <asp:Button runat="server" ID="BtnResponseRedirect" Text="Response.Redirect" CssClass="ui primary button" />
            <asp:Button runat="server" ID="BtnServerTransfer" Text="Server.Transfer" CssClass="ui primary button" />
            <asp:Button runat="server" ID="BtnSubmit" Text="Submit" CssClass="ui primary button" />
        </div>
    </div>

</asp:Content>

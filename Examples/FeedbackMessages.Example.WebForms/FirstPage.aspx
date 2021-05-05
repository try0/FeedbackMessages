<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="FirstPage.aspx.cs" Inherits="FeedbackMessages.Example.WebForms.FirstPage" %>

<%@ Register Assembly="FeedbackMessages" Namespace="FeedbackMessages.Components" TagPrefix="fm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <div class="ui basic segment ">
        <a style="font-size:1.5em; float:right" title="show sample code on GitHub" href="https://github.com/try0/FeedbackMessages/blob/master/Examples/FeedbackMessages.Example.WebForms/FirstPage.aspx.cs"><i class="github icon"></i></a>
    </div>
    
    <!-- feedback message area -->
    <fm:FeedbackMessagePanel runat="server" ID="FeedbackMessagePanel" ShowValidationErrors="true"></fm:FeedbackMessagePanel>


    <!-- simple form -->
    <div class="ui form" style="padding-top:3em;">
        <div class="field"  >
            <asp:TextBox runat="server" ID="Message" Rows="4" TextMode="MultiLine"></asp:TextBox>
            <asp:RequiredFieldValidator ID="MessageRequiredFieldValidator" runat="server" ControlToValidate="Message" ErrorMessage="必須" Display="None" EnableClientScript="false"></asp:RequiredFieldValidator>
        </div>

        <div class="field">
            <asp:Button runat="server" ID="BtnResponseRedirect" Text="Response.Redirect" CssClass="ui primary button" />
            <asp:Button runat="server" ID="BtnServerTransfer" Text="Server.Transfer" CssClass="ui primary button" />
            <asp:Button runat="server" ID="BtnSubmit" Text="Submit" CssClass="ui primary button" />
        </div>
    </div>

</asp:Content>

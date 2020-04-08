<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="SecondPage.aspx.cs" Inherits="FeedbackMessages.Example.WebForms.SecondPage" %>

<%@ Register Assembly="FeedbackMessages" Namespace="FeedbackMessages.Components" TagPrefix="fm" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">

    <div style="padding-bottom: 3em;">
        <a href="FirstPage.aspx" class="ui button">戻る</a>
    </div>

    <!-- feedback message area -->
    <fm:FeedbackMessagePanel runat="server" ID="FeedbackMessagePanel"></fm:FeedbackMessagePanel>

</asp:Content>

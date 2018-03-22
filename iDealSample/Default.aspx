<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
    CodeBehind="Default.aspx.cs" Inherits="ING.iDealSample.Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div>
        For a successfull iDeal payment the following flow should be followed.<br />
        <br />
        <table cellpadding="4" cellspacing="0" border="1">
            <tr>
                <td><b>Step</b></td>
                <td><b>Description</b></td>
                <td><b>Action</b></td>
            </tr>
            <tr>
                <td align="center">1</td>
                <td>Requests a list* of issuers.</td>
                <td>(Function:
                    <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl="~/PageIssuerList.aspx">getIssuerList</asp:HyperLink>)
                </td>
            </tr>
            <tr>
                <td align="center">2</td>
                <td>Select an issuer.</td>
                <td><i>User action</i></td>
            </tr>
            <tr>
                <td align="center">3</td>
                <td>Requests a new transaction.</td>
                <td>(Function:
                    <asp:HyperLink runat="server" NavigateUrl="~/PageRequestTransaction.aspx">requestTransaction</asp:HyperLink>)
                </td>
            </tr>
            <tr>
                <td align="center">4</td>
                <td>Authenticate transaction.</td>
                <td><i>User/Acceptant action</i></td>
            </tr>
            <tr>
                <td align="center">5</td>
                <td>Request transaction status.</td>
                <td>(Function:
                    <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl="~/PageRequestTransactionStatus.aspx">requestTransactionStatus</asp:HyperLink>)
                </td>
            </tr>
        </table>
        <br />
        This API provides functionality for performing steps <b>1, 3</b>&amp; <b>5</b>.<br />
        <br />
        <i>* For optimal performance the retrieved list could be cached.<br />
        </i>
    </div>
</asp:Content>

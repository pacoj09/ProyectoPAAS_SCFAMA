<%@ Page Title="" Language="C#" MasterPageFile="~/PAAS.Master" AutoEventWireup="true" CodeBehind="Resource.aspx.cs" Inherits="PAAS_SCFAMA.Resource" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPAASHead" runat="server">

    <script type="text/javascript">
        function validarInhabilitar() {
            var messange = confirm("Desea Inhabilitar este Recurso");
            if (messange === true) {
                return true;
            } else {
                return false;
            }
        }
    </script>

</asp:Content>



<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPAAS" runat="server">
    <script type="text/javascript">
        document.getElementById("lRecursos").className = "current_page_item";
    </script>


    <div align="center">
        <h1>Recursos</h1>
        <br />
        <fieldset>
            <legend>Datos de Recurso:</legend>
            <asp:Button ID="btnNuevoRecurso" runat="server" Text="Nuevo Recurso" CssClass="btn btn-default" OnClick="btnNuevoRecurso_Click" />
            <br />
            <br />
            ID Recurso:<br>
            <asp:TextBox ID="txtIdRecurso" runat="server" Enabled="false"></asp:TextBox>
            <br>
            Tipo Recurso:<br>
            <asp:TextBox ID="txtTipo" runat="server"></asp:TextBox>
            <br>
            Descripci&oacute;n:<br>
            <asp:TextBox ID="txtDescripcion" runat="server" TextMode="MultiLine" Height="125px" Width="350px"></asp:TextBox>
            <br>
            Stock:<br>
            <asp:TextBox ID="txtStock" runat="server"></asp:TextBox>
            <br>
            <asp:Button ID="btnActualizarRecurso" runat="server" Text="Actualizar Recurso" OnClick="btnActualizarRecurso_Click" CssClass="btn btn-default" />
            &nbsp;	&nbsp;
            <asp:Button ID="btnInhabilitarRecurso" runat="server" Text="Inhabilitar Recurso" OnClientClick="return validarInhabilitar();" OnClick="btnInhabilitarRecurso_Click" CssClass="btn btn-default" />
        </fieldset>
        <br />
        <br />
        <div style="width: 80%; height: 300px; overflow: scroll">
            <asp:GridView ID="gvRecursos" runat="server" OnSelectedIndexChanged="gvRecursos_SelectedIndexChanged" OnRowDataBound="gvRecursos_RowDataBound">
                <Columns>
                    <asp:CommandField CausesValidation="False" InsertVisible="False" ShowCancelButton="False" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

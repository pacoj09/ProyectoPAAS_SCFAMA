<%@ Page Title="" Language="C#" MasterPageFile="~/PAAS.Master" AutoEventWireup="true" CodeBehind="User.aspx.cs" Inherits="PAAS_SCFAMA.User" MaintainScrollPositionOnPostback="true" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderPAASHead" runat="server">

    <script type="text/javascript">
        function validarInhabilitar() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtIdUsuario").value === '') {
                alert("Debe seleccionar un usuario para inhabilitar");
                return false;
            } else {
                var messange = confirm("Desea Inhabilitar este Usuario");
                if (messange === true) {
                    return true;
                } else {
                    return false;
                }
            }
        }

        function validarCampos() {
            if (document.getElementById("ContentPlaceHolderPAAS_txtNombre").value === '') {
                alert("Debe de llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolderPAAS_txtClave").value === '') {
                alert("Debe de llenar todos los campos");
                return false;
            } else if (document.getElementById("ContentPlaceHolderPAAS_txtConfirmarClave").value === '') {
                alert("Debe de llenar todos los campos");
                return false;
            }
            return true;
        }

    </script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderPAAS" runat="server">
    <script type="text/javascript">
        document.getElementById("lUsuarios").className = "current_page_item";
    </script>



    <div align="center">
        <h1>Usuarios</h1>
        <br />
        <fieldset>
            <legend>Datos de Usuario:</legend>
            <br />
            <br />
            ID Usuario:<br>
            <asp:TextBox ID="txtIdUsuario" runat="server" Enabled="false"></asp:TextBox>
            <br>
            Nombre:<br>
            <asp:TextBox ID="txtNombre" runat="server"></asp:TextBox>
            <br>
            Clave:<br>
            <asp:TextBox ID="txtClave" runat="server" TextMode="Password"></asp:TextBox>
            <br>
            Confirmar Clave:<br>
            <asp:TextBox ID="txtConfirmarClave" runat="server" TextMode="Password"></asp:TextBox>
            <br>
            Tipo de Usuario:<br>
            <asp:DropDownList ID="ddlTipoUsuario" runat="server">
                <asp:ListItem Text="Administrador" Value="Administrador" Selected="True"></asp:ListItem>
                <asp:ListItem Text="Gerente" Value="Gerente"></asp:ListItem>
            </asp:DropDownList>
            <br>
            <asp:Button ID="btnGuardarUsuario" runat="server" Text="Guardar Usuario" OnClientClick="return validarCampos();" OnClick="btnGuardarUsuario_Click" CssClass="btn btn-default" />
            &nbsp;	&nbsp;
            <asp:Button ID="btnInhabilitarUsuario" runat="server" Text="Inhabilitar Usuario" OnClientClick="return validarInhabilitar();" OnClick="btnInhabilitarUsuario_Click" CssClass="btn btn-default" />
        </fieldset>
        <br />
        <br />
        <div style="width: 80%; height: 300px; overflow: scroll">
            <asp:GridView ID="gvUsuarios" runat="server" OnSelectedIndexChanged="gvUsuarios_SelectedIndexChanged" OnRowDataBound="gvUsuarios_RowDataBound">
                <Columns>
                    <asp:CommandField CausesValidation="False" InsertVisible="False" ShowCancelButton="False" ShowSelectButton="True" />
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>

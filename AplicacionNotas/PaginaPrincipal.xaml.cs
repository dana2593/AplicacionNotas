using AplicacionNotas.Data;
using AplicacionNotas.Models;

namespace AplicacionNotas
{
    public partial class PaginaPrincipal : ContentPage
    {
        private readonly BaseDatosNotas _baseDatos;
        private int _notaSeleccionadaId = 0;
        private bool _notasVisibles = false;

        public PaginaPrincipal(BaseDatosNotas baseDatos)
        {
            InitializeComponent();
            _baseDatos = baseDatos;

            // Ocultar la lista y botones de edición al inicio
            listViewNotas.IsVisible = false;
            botonModificar.IsVisible = false;
        }

        void OnAgregarNotaClicked(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(entryTexto.Text))
            {
                labelStatus.Text = "Por favor ingresa una nota";
                return;
            }

            _baseDatos.AgregarNuevaNota(entryTexto.Text);
            labelStatus.Text = _baseDatos.StatusMessage;
            entryTexto.Text = string.Empty;
            _notaSeleccionadaId = 0;

            // NO actualizar la lista automáticamente
        }

        void OnModificarNotaClicked(object sender, EventArgs e)
        {
            if (!_notasVisibles)
            {
                labelStatus.Text = "Primero debes ver todas las notas para poder modificar";
                return;
            }

            if (_notaSeleccionadaId == 0)
            {
                labelStatus.Text = "Selecciona una nota de la lista para modificar";
                return;
            }

            if (string.IsNullOrWhiteSpace(entryTexto.Text))
            {
                labelStatus.Text = "Por favor ingresa el nuevo texto";
                return;
            }

            _baseDatos.ModificarNota(_notaSeleccionadaId, entryTexto.Text);
            labelStatus.Text = _baseDatos.StatusMessage;
            entryTexto.Text = string.Empty;
            _notaSeleccionadaId = 0;

            // Actualizar la lista solo si ya está visible
            OnObtenerNotasClicked(sender, e);
        }

        void OnEliminarNotaClicked(object sender, EventArgs e)
        {
            if (!_notasVisibles)
            {
                labelStatus.Text = "Primero debes ver todas las notas para poder eliminar";
                return;
            }

            if (sender is Button button && button.CommandParameter is int notaId)
            {
                _baseDatos.EliminarNota(notaId);
                labelStatus.Text = _baseDatos.StatusMessage;

                // Actualizar la lista solo si ya está visible
                OnObtenerNotasClicked(sender, e);
            }
        }

        void OnNotaSeleccionada(object sender, SelectedItemChangedEventArgs e)
        {
            if (e.SelectedItem is Nota notaSeleccionada)
            {
                _notaSeleccionadaId = notaSeleccionada.Id;
                entryTexto.Text = notaSeleccionada.Texto;
                labelStatus.Text = $"Nota seleccionada: {notaSeleccionada.Id}. Modifica el texto y presiona 'Modificar'";
            }
        }

        void OnObtenerNotasClicked(object sender, EventArgs e)
        {
            var notas = _baseDatos.ObtenerTodasLasNotas();
            listViewNotas.ItemsSource = notas;

            // Mostrar la lista y habilitar funciones de edición
            listViewNotas.IsVisible = true;
            botonModificar.IsVisible = true;
            _notasVisibles = true;

            if (notas.Count == 0)
                labelStatus.Text = "No hay notas guardadas";
            else
                labelStatus.Text = $"Se encontraron {notas.Count} nota(s)";
        }
    }
}
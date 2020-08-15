using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using System.IO;
using Xamarin.Essentials;

namespace S13Reto3
{
    // Learn more about making custom code visible in the Xamarin.Forms previewer
    // by visiting https://aka.ms/xamarinforms-previewer
    [DesignTimeVisible(false)]
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            btnGuardar.Clicked += BtnGuardar_Clicked;
            btnEnviar.Clicked += BtnEnviar_Clicked;
            String nombreArchivo = "prueba.txt";
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String rutaCompleta = Path.Combine(ruta, nombreArchivo);
            if (File.Exists(rutaCompleta))
            {
                File.Delete(rutaCompleta);
            }
        }

        private async void BtnEnviar_Clicked(object sender, EventArgs e)
        {
            String nombreArchivo = "prueba.txt";
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String rutaCompleta = Path.Combine(ruta, nombreArchivo);
            if (File.Exists(rutaCompleta))
            {
                using (var lector = new StreamReader(rutaCompleta, true))
                {
                    String TextoLeido;
                    while ((TextoLeido = lector.ReadLine()) != null)
                    {
                        temp.Text = TextoLeido;
                        var message = new EmailMessage("Reto3", TextoLeido);
                        message.Body = TextoLeido;
                        message.To.Add("cursosunilat@gmail.com");
                        await Email.ComposeAsync(message);
                    }
                }
            }

        }

        private async void BtnGuardar_Clicked(object sender, EventArgs e)
        {
            String nombreArchivo = "prueba.txt";
            String ruta = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            String rutaCompleta = Path.Combine(ruta, nombreArchivo);

            if (File.Exists(rutaCompleta))
            {
                using (var escritor = File.AppendText(rutaCompleta))
                {
                    escritor.Write(entrFrase.Text);
                }
            }
            else
            {
                using (var escritor = File.CreateText(rutaCompleta))
                {
                    escritor.Write(entrFrase.Text);
                }
            }
            await TextToSpeech.SpeakAsync("Guardado");
        }
    }
}

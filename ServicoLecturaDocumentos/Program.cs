namespace ServicoLecturaDocumentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Documento servicio = new Documento();

            servicio.MenuOpcionesDocumento();

        }
    }


    public class Documento
    {
        string rutaArchivoLog = @"./Log.txt";
        string rutaDocumento = "";
        public void MenuOpcionesDocumento()
        {
            CrearArchivoLog();

            int opcionSeleccionada;

            do
            {
                Console.Clear();
                Console.Write($"BIENVENIDOS A NUESTRO SERVICIO DE LECTURA DE DOCUMENTOS (.CSV / XLSX)\n" +
                              $"==========SELECIONE UNA OPCION 1-5 ========== \n\n" +
                              $"1. Validar si existe un documento\n" +
                              $"2. Validar si el documento contiene datos\"" +
                              $"3. Mostrar la informacion de un documento\n" +
                              $"4. Mostra los registros o eventos de este programa\n" +
                              $"5. SALIR\n" +
                              $"DIGITE UNA OPCION -----> ");

                if (!int.TryParse(Console.ReadLine(), out opcionSeleccionada))
                {
                    RegistroEventos(DateTime.Now, "Se ingreso una opcion incorrecta.");
                    Console.Write("\nOpcion no valida, presione enter para continuar....");
                    Console.ReadKey();
                    continue;
                }

                switch (opcionSeleccionada)
                {
                    case 1:
                        ValidarExistenciaDocumento();
                        break;

                    case 2:
                        ValidarDatosDocumento();
                        break;

                    case 3:
                        MostrarDocumento();
                        break;

                    case 4:
                        MostrarLog();
                        Console.WriteLine("SALIENDO DEL PROGRAMA....");
                        break;

                    case 5:
                        SalirServicio();
                        break;

                    default:
                        RegistroEventos(DateTime.Now, "Se ingreso una opcion incorrecta.");
                        Console.WriteLine("OPCION NO VALIDA, INTENTE DE NUEVO....");

                        break;
                }

            } while (opcionSeleccionada != 5);

        }

        public void CrearArchivoLog()
        {
            if (!File.Exists(rutaArchivoLog))
            {
                File.WriteAllText(rutaArchivoLog, "=============== REGISTROS DEL PROGRAMA ===============\n");
                RegistroEventos(DateTime.Now, "Creacion del archivo \"Log.txt\"");
            }

            else if (new FileInfo(rutaArchivoLog).Length == 0)
            {
                File.WriteAllText(rutaArchivoLog, "========== REGISTROS DEL PROGRAMA ==========\n");
                RegistroEventos(new FileInfo(rutaArchivoLog).CreationTime, "Creacion del archivo\"Log.txt\"");
            }

            RegistroEventos(DateTime.Now, "Programa Iniciado.");
        }

        public void RegistroEventos(DateTime fecha, string evento)
        {
            File.AppendAllText(rutaArchivoLog, $"{fecha} : {evento}" + Environment.NewLine);
        }


        public void ValidarExistenciaDocumento()
        {
            Console.Clear();

            Console.Write("Ingrese la ruta del archivo del cual quiere validar su exitencia: ");
            rutaDocumento = Console.ReadLine() ?? "";


            if (File.Exists(rutaDocumento) && rutaDocumento != "")
            {
                RegistroEventos(DateTime.Now, $"Consultando exitencia del documento en la ruta: {rutaDocumento}");
                Console.WriteLine($"El archivo en la ruta: {rutaDocumento} EXISTE.");
                RegistroEventos(DateTime.Now, $"El archivo en la ruta: {rutaDocumento} fue encontrado");
            }
            else if (!File.Exists(rutaDocumento) && rutaDocumento != "")
            {
                RegistroEventos(DateTime.Now, $"Consultando exitencia del documento en la ruta: {rutaDocumento}");
                Console.WriteLine($"El archivo en la ruta: {rutaDocumento} NO EXISTE.");
                RegistroEventos(DateTime.Now, $"El archivo en la ruta: {rutaDocumento} no fue encontrado");

            }
            else
            {
                Console.WriteLine($"La ruta ingresada esta vacia.");
                RegistroEventos(DateTime.Now, "Se ingreso una ruta vacia");
                Console.WriteLine();
            }

        }

        public void ValidarDatosDocumento()
        {
            RegistroEventos(DateTime.Now, $"Consultando datos del documento en la ruta: {rutaDocumento}");
        }

        public void MostrarDocumento()
        {

        }

        public void MostrarLog()
        {

        }

        void VolverMenu()
        {
            Console.Write("Desea volver al menu s/n");
            string opcion = Console.ReadLine() ?? "s";

            if (opcion == "s" || opcion != "n") { MenuOpcionesDocumento(); RegistroEventos(DateTime.Now, "Regreso al menu principal."); }
            else { SalirServicio(); }
        }

        void SalirServicio()
        {
            Console.Clear();
            RegistroEventos(DateTime.Now, "Programa Finalizado");
            Console.Write($"=================================" +
                          $"Gracias por usar nuestros servicios" +
                          $"=================================");

            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
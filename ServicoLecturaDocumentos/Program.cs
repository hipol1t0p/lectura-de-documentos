using OfficeOpenXml;

namespace ServicoLecturaDocumentos
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Documento servicio = new Documento();

            servicio.MenuOpcionesDocumento();

        }
    }


    public class Documento
    {
        string rutaArchivoLog = @"./Log.txt";
        protected string rutaDocumento { get; set; }

        public Documento()
        {
            rutaDocumento = "";
        }
        public void MenuOpcionesDocumento()
        {
            CrearArchivoLog();

            int opSeleccionadaMenu;

            do
            {
                Console.Clear();
                Console.Write($"\n\nBIENVENIDOS A NUESTRO SERVICIO DE LECTURA DE DOCUMENTOS (.CSV / XLSX)\n\n" +
                              $"\t==========SELECIONE UNA OPCION 1-5 ========== \n\n" +
                              $"1. Validar si existe un documento\n" +
                              $"2. Validar si el documento contiene datos\n" +
                              $"3. Mostrar la informacion de un documento\n" +
                              $"4. Mostra los registros o eventos de este programa\n" +
                              $"5. SALIR\n" +
                              $"DIGITE UNA OPCION -----> ");

                if (!int.TryParse(Console.ReadLine(), out opSeleccionadaMenu))
                {
                    RegistroEventos(DateTime.Now, "Se ingreso una opcion incorrecta.");
                    Console.Write("\nOpcion no valida, presione enter para continuar....");
                    Console.ReadKey();
                    continue;
                }

                switch (opSeleccionadaMenu)
                {
                    case 1:

                        Console.Clear();
                        Console.Write("Ingrese la ruta del documento: ");
                        rutaDocumento = Console.ReadLine() ?? "";
                        ValidarExistenciaDocumento(rutaDocumento);
                        break;

                    case 2:
                        SubMenuOpcionesDocumentos();
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

            } while (opSeleccionadaMenu != 5);

        }

        void SubMenuOpcionesDocumentos()
        {

            do
            {
                int opSeleccionadaSubMenu;

                Console.Clear();
                Console.Write($"==========SELECIONE UNA OPCION 1-3 ========== \n\n" +
                              $"1. Seleccionar el documento desde una ventana emergente\n" +
                              $"2. Ingresar la ruta del documento manualmente\n" +
                              $"3. Volver al menu principal\n" +
                              $"DIGITE UNA OPCION -----> ");

                if (!int.TryParse(Console.ReadLine(), out opSeleccionadaSubMenu))
                {
                    Console.Write("\nOpcion no valida, presione enter para continuar....");
                    Console.ReadKey();
                    RegistroEventos(DateTime.Now, "Se ingreso una opcion incorrecta en el sub menu.");
                    continue;
                }

                switch (opSeleccionadaSubMenu)
                {
                    case 1:

                        ValidarDatosDocumento(SelecionarDocumento());
                        MenuOpcionesDocumento();
                        break;

                    case 2:

                        Console.Write("Ingrese la ruta del documento: ");
                        rutaDocumento = Console.ReadLine() ?? ""; // posible mejora como la opciones de los menus

                        ValidarExistenciaDocumento(rutaDocumento);

                        RegistroEventos(DateTime.Now, $"Documento ingresado manualmente en la ruta: {rutaDocumento}");
                        break;

                    case 3:
                        MenuOpcionesDocumento();
                        break;

                    default:
                        Console.WriteLine("OPCION NO VALIDA, INTENTE DE NUEVO....");
                        RegistroEventos(DateTime.Now, "Se ingreso una opcion incorrecta en el sub menu.");
                        break;
                }


            } while (true);
        }


        public string SelecionarDocumento()
        {
            OpenFileDialog seleccionDocumento = new OpenFileDialog();

            do
            {
                if (seleccionDocumento.ShowDialog() == DialogResult.OK)
                {
                    Console.Clear();
                    rutaDocumento = seleccionDocumento.FileName;

                    if (Path.GetExtension(rutaDocumento) == ".csv" || Path.GetExtension(rutaDocumento) == ".xlsx")
                    {
                        RegistroEventos(DateTime.Now, $"Documento seleccionado en la ruta: {rutaDocumento}");

                    }
                    else
                    {
                        Console.WriteLine("\n\nEl formato del documento seleccionado no es valido, por favor seleccione un documento con extension .CSV o .XLSX");
                        Console.Write("\nPresione enter para continuar.... ");
                        Console.ReadKey();
                        RegistroEventos(DateTime.Now, $"El formato del documento seleccionado no es valido: {rutaDocumento}");

                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo se selecciono ningun documento.  ");
                    Console.Write("\nPresione enter para continuar.... ");
                    Console.ReadKey();
                    RegistroEventos(DateTime.Now, "Se cancelo la seleccion del documento.");
                    SubMenuOpcionesDocumentos();
                    break;
                }

            } while ((Path.GetExtension(rutaDocumento) != ".csv") && (Path.GetExtension(rutaDocumento) != ".xlsx"));

            return rutaDocumento;
        } //Tereminado - Posible retoques

        public void CrearArchivoLog()
        {
            if (!File.Exists(rutaArchivoLog))
            {
                File.WriteAllText(rutaArchivoLog, "=============== REGISTROS DEL PROGRAMA ===============\n\n");
                RegistroEventos(DateTime.Now, "Creacion del archivo \"Log.txt\"");
            }

            else if (new FileInfo(rutaArchivoLog).Length == 0)
            {
                File.WriteAllText(rutaArchivoLog, "========== REGISTROS DEL PROGRAMA ==========\n");
                RegistroEventos(new FileInfo(rutaArchivoLog).CreationTime, "Creacion del archivo\"Log.txt\"");
            }

            RegistroEventos(DateTime.Now, "Programa Iniciado.");
        } // Terminado - Posible mejora para la validacion de los eventos

        public void RegistroEventos(DateTime fecha, string evento)
        {
            File.AppendAllText(rutaArchivoLog, $"{fecha} : {evento}" + Environment.NewLine);
        } // Culminado 

        public bool ValidarExistenciaDocumento(string ruta)
        {
            Console.Clear();

            RegistroEventos(DateTime.Now, $"Iniciando validacion de existencia del documento en la ruta {ruta}.");

            if ((File.Exists(ruta) && ruta != "") && (Path.GetExtension(ruta) == ".csv" || Path.GetExtension(ruta) == ".xlsx"))
            {
                RegistroEventos(DateTime.Now, $"Consultando exitencia del documento en la ruta: {ruta}");
                Console.WriteLine($"El archivo en la ruta: {ruta}  \"EXISTE.\"\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventos(DateTime.Now, $"El archivo en la ruta: {ruta} fue encontrado");
                Console.ReadKey();

                return true;
            }

            else if ((File.Exists(ruta) && ruta != "") && (Path.GetExtension(ruta) != ".csv" && Path.GetExtension(ruta) != ".xlsx"))
            {
                RegistroEventos(DateTime.Now, $"Consultando exitencia del documento en la ruta: {ruta}");
                Console.WriteLine($"El archivo en la ruta: {ruta} no es un archivo con extencion valida .csv / .xlsx\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventos(DateTime.Now, $"El archivo en la ruta: {ruta} no es un archivo con extencion valida .csv / .xlsx");
                Console.ReadKey();

                return false;
            }

            else if (!File.Exists(ruta) && ruta != "")
            {
                RegistroEventos(DateTime.Now, $"Consultando exitencia del documento en la ruta: {ruta}\n\n");
                Console.WriteLine($"El archivo en la ruta: {ruta} NO EXISTE.\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventos(DateTime.Now, $"El archivo en la ruta: {ruta} no fue encontrado");
                Console.ReadKey();

                return false;
            }

            else
            {
                Console.WriteLine($"La ruta ingresada esta vacia.\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventos(DateTime.Now, "Se ingreso una ruta vacia");
                Console.ReadKey();

                return false;
            }

        } // Terminado se podria simplificar tal vez

        void VolverMenu()
        {
            Console.Write("Desea volver al menu s/n");
            string opcion = Console.ReadLine() ?? "s";

            if (opcion == "s" || opcion != "n") { MenuOpcionesDocumento(); RegistroEventos(DateTime.Now, "Regreso al menu principal."); }
            else { SalirServicio(); }
        } // Terminados - Pendientes de usar

        void SalirServicio()
        {
            Console.Clear();
            RegistroEventos(DateTime.Now, "Programa Finalizado");
            Console.WriteLine($"=================================" +
                          $"Gracias por usar nuestros servicios" +
                          $"=================================");

            Console.ReadKey();
            Environment.Exit(0);
        } // Terminado - Pendientes de usar

        public bool ValidarDatosDocumento(string ruta)
        {
            RegistroEventos(DateTime.Now, $"Consultando existencia de datos en el documento de la ruta: {rutaDocumento}");

            if (ConocerExtencionDocumento())
            {

                if (File.ReadAllLines(ruta).Any(linea => !string.IsNullOrWhiteSpace(linea)))
                {
                    Console.Clear();
                    Console.Write($"\n\nEl documento en la ruta: {ruta} contiene datos.");
                    RegistroEventos(DateTime.Now, $"El documento en la ruta: {ruta} contiene datos.");
                    Console.ReadKey();

                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.Write($"\n\nEl documento en la ruta: {rutaDocumento} NO contiene datos.");
                    RegistroEventos(DateTime.Now, $"El documento en la ruta: {rutaDocumento} NO contiene datos.");
                    Console.ReadKey();

                    return false;
                }
            }

            else
            {
                ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization"); //This will also set the Company property to the organization name provided in the argument.

                //Linea necesaria para EPPlus version 5 en adelante para uso no comercial

                using var documentoExcel = new ExcelPackage(new FileInfo(rutaDocumento));

                var hoja = documentoExcel.Workbook.Worksheets[0];

                if (hoja.Dimension != null && hoja.Dimension.Rows > 0)
                {
                    Console.Clear();
                    Console.Write($"\n\nEl documento en la ruta: {ruta} contiene datos.");
                    RegistroEventos(DateTime.Now, $"El documento en la ruta: {ruta} contiene datos.");
                    Console.ReadKey();
                    return true;
                }
                else
                {
                    Console.Clear();
                    Console.Write($"\n\nEl documento en la ruta: {rutaDocumento} NO contiene datos.");
                    RegistroEventos(DateTime.Now, $"El documento en la ruta: {rutaDocumento} NO contiene datos.");
                    Console.ReadKey();
                    return false;
                }
            }


        } // Terminado - Completo


        bool ConocerExtencionDocumento()
        {
            RegistroEventos(DateTime.Now, $"Consultando la extencion del documento de la ruta: {rutaDocumento}");

            if (Path.GetExtension(rutaDocumento) == ".csv")
            {
                RegistroEventos(DateTime.Now, $"El documento en la ruta: {rutaDocumento} es un archivo .CSV");

                return true;
            }
            else
            {
                RegistroEventos(DateTime.Now, $"El documento en la ruta: {rutaDocumento} es un archivo .XLSX");

                return false;
            }
        } // Terminado - Pendientes de usar


        // Metodos pendientes de terminar;
        public void MostrarDocumento()
        {

        }

        public void MostrarLog()
        {

        }

    }
}
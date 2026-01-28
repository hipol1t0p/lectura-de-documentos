using Microsoft.VisualBasic.Logging;
using OfficeOpenXml;
using System.Diagnostics;
using System.Reflection.Metadata;

namespace ServicoLecturaDocumentos
{
    internal class Program
    {
        [STAThread]
        static void Main(string[] args)
        {
            Menus.MenuOpcionesDocumento();
        }
    }


    public class Menus 
    {
        public static void MenuOpcionesDocumento()
        {
            Documento.CrearArchivoLog();

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
                    // RegistroEventosLog(DateTime.Now, "Se ingreso una opcion incorrecta.");
                    Console.Write("\nOpcion no valida, presione enter para continuar....");
                    Console.ReadKey();
                    continue;
                }

                switch (opSeleccionadaMenu)
                {
                    case 1:

                        Console.Clear();
                        Console.Write("Ingrese la ruta del documento: ");
                        new Documento(Console.ReadLine() ?? "");
                        Documento.ValidarExistenciaDocumento();
                        break;

                    case 2:
                        SubMenuOpcionesDocumentos(opSeleccionadaMenu);
                        break;

                    case 3:
                        SubMenuOpcionesDocumentos(opSeleccionadaMenu);
                        //Documento.MostrarDocumento();
                        //if (Documento.ValidarExistenciaDocumento()) { Documento.MostrarDocumento(); }
                        break;

                    case 4:
                        Documento.MostrarLog();
                        break;

                    case 5:
                        SalirServicio();
                        break;

                    default:
                        Documento.RegistroEventosLog(DateTime.Now, "Se ingreso una opcion incorrecta.");
                        Console.WriteLine("\nFuera del rango de OPCION, INTENTE DE NUEVO....");
                        Console.ReadKey();
                        break;
                }

            } while (opSeleccionadaMenu != 5);

        }

        public static void SubMenuOpcionesDocumentos(int M_P)
        {
            int opSeleccionadaSubMenu;

            do
            {
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
                    Documento.RegistroEventosLog(DateTime.Now, "Se ingreso una opcion incorrecta en el sub menu.");
                    continue;
                }

                switch (opSeleccionadaSubMenu)
                {
                    case 1:

                        if (M_P == 2) 
                        { 
                            new Documento(Documento.SelecionarDocumento()).ValidarDatosDocumento(1); 
                        }
                        else if (M_P == 3) 
                        {
                            new Documento(Documento.SelecionarDocumento()).MostrarDocumento(1);
                        }

                        break;

                    case 2:

                        if (M_P == 2) 
                        {
                            Console.Clear();
                            Console.Write("Ingrese la ruta del documento: ");
                            new Documento(Console.ReadLine() ?? "").ValidarDatosDocumento(2);
                        }
                        else if (M_P == 3) 
                        {
                            Console.Clear();
                            Console.Write("Ingrese la ruta del documento: ");
                            new Documento(Console.ReadLine() ?? "").MostrarDocumento(2);
                        }

                        break;

                    case 3:
                        break;

                    default:
                        Console.WriteLine("\nFuera del rango de OPCION, INTENTE DE NUEVO....");
                        Documento.RegistroEventosLog(DateTime.Now, "Se ingreso una opcion incorrecta en el sub menu.");
                        Console.ReadKey();
                        break;
                }

            } while (opSeleccionadaSubMenu !=3);
        }

        /*protected void VolverMenu()
        {
            Console.Write("Desea volver al menu s/n");
            string opcion = Console.ReadLine() ?? "s";

            if (opcion == "s" || opcion != "n") 
            { 
                MenuOpcionesDocumento(); // RegistroEventosLog(DateTime.Now, "Regreso al menu principal.");
    }
            else { SalirServicio(); }
        } */  

        private static void SalirServicio()
        {
            Console.Clear();
            Documento.RegistroEventosLog(DateTime.Now, "Programa Finalizado");
            Console.WriteLine($"==================" +
                          $"Gracias por usar nuestros servicios" +
                          $"=====================");
            return;
        }
    }

    public class Documento
    {
        public static string RutaDocumento { get; set; }
        protected static string RutaArchivoLog { get { return @"./Log.txt"; } }

        static Documento()
        {
            RutaDocumento = String.Empty;
        }

        public Documento(string ruta)
        {
            RutaDocumento = ruta;
        }
        public static string SelecionarDocumento()
        {
            OpenFileDialog seleccionDocumento = new OpenFileDialog();

            do
            {
                if (seleccionDocumento.ShowDialog() == DialogResult.OK)
                {
                    Console.Clear();
                    RutaDocumento = seleccionDocumento.FileName;

                    if (Path.GetExtension(RutaDocumento) == ".csv" || Path.GetExtension(RutaDocumento) == ".xlsx")
                    {
                        RegistroEventosLog(DateTime.Now, $"Documento seleccionado en la ruta: {RutaDocumento} valido.");

                    }
                    else
                    {
                        Console.WriteLine("\n\nEl formato del documento seleccionado no es valido, por favor seleccione un documento con extension .CSV o .XLSX");
                        Console.Write("\nPresione enter para volver a selecionar otro. ");
                        Console.ReadKey();
                        RegistroEventosLog(DateTime.Now, $"El formato del documento seleccionadoen la ruta {RutaDocumento} no es valido");

                        continue;
                    }
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("\n\nNo fue selecciono ningun documento.");
                    Console.Write("\nPresione enter para continuar. ");
                    Console.ReadKey();
                    RegistroEventosLog(DateTime.Now, "Se cancelo la seleccion del documento.");
                    Menus.SubMenuOpcionesDocumentos(0);
                    break;
                    //continue;
                }

            } while ((Path.GetExtension(RutaDocumento) != ".csv") && (Path.GetExtension(RutaDocumento) != ".xlsx"));

            return RutaDocumento;
        }

        public static void CrearArchivoLog()
        {            
            string tituloLog = "=============== REGISTROS DEL PROGRAMA ===============";

            if (!File.Exists(RutaArchivoLog))
            {
                File.WriteAllText(RutaArchivoLog, $"{tituloLog}\n\n");
                RegistroEventosLog(DateTime.Now, "Creacion del archivo \"Log.txt\"");
            }

            else 
            {
                string[] lineasArchivoLog = File.ReadAllLines(RutaArchivoLog);

                if ((lineasArchivoLog[0] != tituloLog) || (lineasArchivoLog.Length == 0))
                {
                    File.WriteAllText(RutaArchivoLog, $"{tituloLog}\n\n");
                    RegistroEventosLog(new FileInfo(RutaArchivoLog).CreationTime, "Creacion del archivo\"Log.txt\"");
                    RegistroEventosLog(DateTime.Now, "Reescritura del archivo");
                }
            }
            
            RegistroEventosLog(DateTime.Now, "Programa Iniciado.");
        } // Terminado

        public static void RegistroEventosLog(DateTime fecha, string evento)
        {
            File.AppendAllText(RutaArchivoLog, $"{fecha} : {evento}" + Environment.NewLine);
        } // Culminado 

        public static bool ValidarExistenciaDocumento()
        {
            Console.Clear();

            RegistroEventosLog(DateTime.Now, $"Iniciando validacion de existencia del documento en la ruta \"{RutaDocumento} \"");

            if (File.Exists(RutaDocumento))
            {
                if (Path.GetExtension(RutaDocumento) == ".csv" || Path.GetExtension(RutaDocumento) == ".xlsx")
                {
                    //RegistroEventosLog(DateTime.Now, $"Consultando exitencia del documento en la ruta: {RutaDocumento}");
                    Console.WriteLine($"El archivo en la ruta ingresada\"EXISTE.\"\n\n");
                    Console.Write($"Presione enter para continuar ------> ");
                    RegistroEventosLog(DateTime.Now, $"El archivo en la ruta: {RutaDocumento} fue encontrado");
                    Console.ReadKey();

                    return true;
                }
                else
                {
                    Console.WriteLine($"El archivo en la ruta ingresada no es un archivo con extencion valida .csv / .xlsx\n\n");
                    Console.Write($"Presione enter para continuar ------> ");
                    RegistroEventosLog(DateTime.Now, $"El archivo en la ruta: {RutaDocumento} no es un archivo con extencion valida .csv / .xlsx");
                    Console.ReadKey();

                    return false;
                }
            }
            else
            {
                Console.WriteLine($"No existe el archivo en la ruta ingresada\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventosLog(DateTime.Now, $"No existe el archivo en la ruta ingresada");
                Console.ReadKey();

                return false;
                /*Console.WriteLine($"La ruta ingresada esta vacia.\n\n");
                Console.Write($"Presione enter para continuar ------> ");
                RegistroEventosLog(DateTime.Now, "Se ingreso una ruta vacia");
                Console.ReadKey();

                return false;*/
            }

        } // Terminado

        public void ValidarDatosDocumento(int opcion)
        {
            RegistroEventosLog(DateTime.Now, $"Iniciando validacion de existencia de datos en el documento de la ruta: {RutaDocumento}");

            if (opcion == 1)
            {
                if (RutaDocumento != String.Empty)
                {
                    if (Path.GetExtension(RutaDocumento) == ".csv")
                    {
                        if (File.ReadAllLines(RutaDocumento).Any(linea => !string.IsNullOrWhiteSpace(linea)))
                        {
                            //Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} contiene datos.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta no contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} NO contiene datos.");
                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization"); //This will also set the Company property to the organization name provided in the argument.

                        //Linea necesaria para EPPlus version 5 en adelante para uso no comercial

                        //using var documentoExcel = new ExcelPackage(new FileInfo(RutaDocumento));
                        var documentoExcel = new ExcelPackage(new FileInfo(RutaDocumento));

                        var hoja = documentoExcel.Workbook.Worksheets[0];

                        if (hoja.Dimension != null && hoja.Dimension.Rows > 0)
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} contiene datos.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta no contiene datos. Presione enter para continuar.");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} NO contiene datos.");
                            Console.ReadKey();
                        }
                    }

                }
            }

            else 
            {
                if (ValidarExistenciaDocumento())
                {
                    if (Path.GetExtension(RutaDocumento) == ".csv")
                    {
                        if (File.ReadAllLines(RutaDocumento).Any(linea => !string.IsNullOrWhiteSpace(linea)))
                        {
                            //Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} contiene datos.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta no contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} NO contiene datos.");
                            Console.ReadKey();
                        }
                    }

                    else
                    {
                        ExcelPackage.License.SetNonCommercialOrganization("My Noncommercial organization"); 

                        var documentoExcel = new ExcelPackage(new FileInfo(RutaDocumento));

                        var hoja = documentoExcel.Workbook.Worksheets[0];

                        if (hoja.Dimension != null && hoja.Dimension.Rows > 0)
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta contiene datos. Presione enter para continuar. ");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} contiene datos.");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.Clear();
                            Console.Write($"\n\nEl documento en la ruta no contiene datos. Presione enter para continuar.");
                            RegistroEventosLog(DateTime.Now, $"El documento en la ruta: {RutaDocumento} NO contiene datos.");
                            Console.ReadKey();
                        }
                    }

                }
            }

        } // Terminado - Completo

        public static void MostrarLog()
        {
            RegistroEventosLog(DateTime.Now, "Apertura de archivo de registro de sistema \"(Log.txt)\"");
            string rutaCompletaLog = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, RutaArchivoLog);

            Process.Start(new ProcessStartInfo { FileName = rutaCompletaLog, UseShellExecute = true });

        }

        public void MostrarDocumento(int opcion)
        {
            if (opcion == 1) 
            {
                if (RutaDocumento != String.Empty)
                {
                    RegistroEventosLog(DateTime.Now, $"Apertura de documento en la ruta \"{RutaDocumento}\"");
                    Process.Start(new ProcessStartInfo { FileName = RutaDocumento, UseShellExecute = true });
                    Console.WriteLine("Docuemnto mostrado correctamente");
                    Console.ReadKey();
                }
            }

            else 
            { 
                if (ValidarExistenciaDocumento()) 
                {
                    Console.Clear();
                    RegistroEventosLog(DateTime.Now, $"Apertura de documento en la ruta \"{RutaDocumento}\"");
                    Process.Start(new ProcessStartInfo { FileName = RutaDocumento, UseShellExecute = true });
                    Console.WriteLine("Docuemnto mostrado correctamente");
                    Console.ReadKey();
                }
            }

        }

    }

}
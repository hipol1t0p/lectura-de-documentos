namespace ServicoLecturaDocumentos
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Documento ruta = Documento.CrearUnicaInstanciaDocumento;

            Console.WriteLine("Hello, World!");
        }
    }


    public class Documento 
    {
        private static Documento unicaInstanciaDocumento;
        private string rutaDocumento;
        private Documento() { this.rutaDocumento = @"./Datos.csv"; }

        public static Documento CrearUnicaInstanciaDocumento 
        {
            get 
            {
                if (unicaInstanciaDocumento == null) { unicaInstanciaDocumento = new Documento(); }

                return unicaInstanciaDocumento;
            }
        }

        public void MenuDocumento() 
        {
            string opcionSeleccionada;

            do {

                Console.Clear();
                Console.Write($"BIENVENIDOS A NUESTRO SERVICIO DE LECTURA DE DOCUMENTOS\n" +
                              $"==========SELECIONE UNA OPCION 1/4 ========== \n\n" +
                              $"1. LEER DOCUMENTO\n" +
                              $"2. ESCRIBIR EN DOCUMENTO\"" +
                              $"3. SOBRESCRIBIR DOCUMENTO\n" +
                              $"4. SALIR\n" +
                              $"DIGITE UNA OPCION -----> ");

                opcionSeleccionada = Console.ReadLine() ?? "0";

                switch (opcionSeleccionada) 
                {
                    case "1":
                        LeerDocumento();
                        break;
                    case "2":
                        EscribirDocumento();
                        break;
                    case "3":
                        SobrescribirDocumento2();
                        break;
                    case "4":
                        Console.WriteLine("SALIENDO DEL PROGRAMA....");
                        break;
                    default:
                        Console.WriteLine("OPCION NO VALIDA, INTENTE DE NUEVO....");
                        
                        break;
                }

                Console.WriteLine("\nPRESIONE CUALQUIER TECLA PARA CONTINUAR..... ");
                Console.ReadKey();

            } while (opcionSeleccionada != "4");
            
        }


        public void LeerDocumento() 
        { 
            if (!File.Exists(rutaDocumento)) 
            {
                Console.WriteLine("EL ARCHIVO NO EXISTE.");

            }

        }

        public void EscribirDocumento() 
        {

        }

        public void SobrescribirDocumento2() 
        { 
        
        }
    }
}

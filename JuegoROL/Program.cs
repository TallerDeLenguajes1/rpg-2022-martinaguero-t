using System.Text.Json;
namespace JuegoROL; 
class Program {

    static int Main(string[] args){

        MenuPrincipal();
        return 0;

    }
    
    public static void MenuPrincipal(){

        int opcionesJuego = 0;

        do
        {
            Console.WriteLine("====== JUEGO DE ROL ======");    

            do
            {  
                Console.WriteLine("--> Elija alguna de las siguientes opciones: \n1) Jugar \n2) Mostrar historial de partidas \n3) Salir del juego");
                int.TryParse(Console.ReadLine(), out opcionesJuego);
                
            } while (opcionesJuego < 1 || opcionesJuego > 3);

            Console.WriteLine("==============================================");

            switch (opcionesJuego)
            {
                case 1:
                    // Función que elige la opción de carga
                    List<Personaje> listaPersonajes = ConsultarSobreCargaPersonajes();
                    ProcesosJuego.DesarrollarJuegoPorRondas(listaPersonajes);
                    break;
                case 2:
                    ManejoDeArchivos.MostrarHistorial();
                    break;
            }

            Console.WriteLine("==============================================\n");

        } while (opcionesJuego != 3);

    }

    // CONSULTA : consultar sobre los métodos implementados y el uso de ref. ¿Por qué no hizo falta en las funciones que modifican los personajes? 

    public static List<Personaje> ConsultarSobreCargaPersonajes(){
        
        var listaPersonajesJSON = new List<Personaje>();
        // Lista donde se cargarán los personajes desde un archivo JSON; si falló la carga, quedará vacía.

        Console.WriteLine("¿Desea cargar personajes (desde archivo JSON) o generarlos aleatoriamente?");
        int consulta = ConsultarAUsuario();
        // Modularización para usar la función luego en IntentarCargarDesdeJSON()

        if(consulta == 1){
            IntentarCargarDesdeJSON(consulta, ref listaPersonajesJSON);
        }

        return listaPersonajesJSON;

    }

    private static int ConsultarAUsuario(){
        
        int consulta;

        do
        {
            Console.WriteLine("1) Cargar desde archivo JSON");
            Console.WriteLine("2) Generar aleatoriamente");
            int.TryParse(Console.ReadLine(), out consulta);

        } while (consulta < 1 || consulta > 2);

        return consulta;

    }
    
    private static void IntentarCargarDesdeJSON(int consulta, ref List<Personaje> listaPersonajesJSON){

        bool existeArchivo = false;

        do
        {
            Console.WriteLine("Indique la ruta del archivo JSON:");
            string? rutaArchivoJSON = Console.ReadLine();
            existeArchivo = File.Exists(rutaArchivoJSON);

            if(existeArchivo){

                IntentarLeerJSON(rutaArchivoJSON, ref listaPersonajesJSON);

            } else {

                Console.WriteLine("No se encontró el archivo buscado. ¿Desea ingresar otra ruta o cargar personajes aleatoriamente?");

                consulta = ConsultarAUsuario();

            }

        } while (!existeArchivo && consulta != 2);
        // Si el archivo existe, se terminará el proceso iterativo condicional habiendo obtenido o una lista vacia o una lista con al menos dos personajes.

    }

    private static void IntentarLeerJSON(string rutaArchivoJSON, ref List<Personaje> listaPersonajesJSON){

        StreamReader reader = new StreamReader(rutaArchivoJSON);
        // NOTA: la ruta de archivo no será vacía, pues se controló que el archivo exista previamente y esta función es llamada si el archivo existe.
        
        string? datosPersonajes = reader.ReadLine();

        if(!string.IsNullOrEmpty(datosPersonajes)){

            try
            {
                
                listaPersonajesJSON = JsonSerializer.Deserialize<List<Personaje>>(datosPersonajes);

                int cantidadPersonajes = listaPersonajesJSON.Count;

                if(cantidadPersonajes < 2){

                    Console.WriteLine("ERROR: Hubo un error al leer los personajes. Se generarán aleatoriamente...");
                    
                    listaPersonajesJSON.Clear();
                    // Se liberan los datos cargados a la lista.
                }
            }
            catch 
            {
                Console.WriteLine("ERROR: El archivo JSON contiene información inválida. Se generarán los personajes aleatoriamente... ");

            }

        } else {
            Console.WriteLine("ERROR: No se encontró información de personajes. Se generarán aleatoriamente... ");
        }

        if(!listaPersonajesJSON.Any()){
            ProcesosJuego.CargarPersonajes(listaPersonajesJSON);
        }
        // Si la lista no tiene elementos (no fue cargada desde un JSON), se carga aleatoriamente.

        reader.Close();
        // Se cierra el stream de lectura
            

    }
   

}
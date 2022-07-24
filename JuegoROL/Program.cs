using System.Text.Json;
namespace JuegoROL; 
class Program {

    static int Main(string[] args){

        int opcionesJuego = 0;

        do 
        {
            opcionesJuego = MenuDeOpcionesDeJuego();
            
            switch (opcionesJuego)
            {
                case 1:
                    // Carga de personajes (de manera aleatoria o desde JSON)
                    List<Personaje> listaPersonajes = CargarPersonajes();
                    ProcesosJuego.Pausa();

                    // Guardado de informacion de personajes a un archivo JSON
                    ManejoDeArchivos.GuardarJugadoresJSON(listaPersonajes);
                    
                    // Desarrollo del juego y obtencion del ganador
                    Personaje ganador = ProcesosJuego.DesarrollarJuegoPorRondas(listaPersonajes);

                    // Carga del personaje ganador a un archivo CSV
                    ManejoDeArchivos.GuardarInformacionGanador(ganador);

                    break;

                case 2:
                    // Mostrar historial de partidas
                    ManejoDeArchivos.MostrarHistorial();
                    break;
            }

        } while (opcionesJuego != 3);

        return 0;

    }
    
    public static int MenuDeOpcionesDeJuego(){

        // Muestra opciones del juego y retorna la opción elegida por el usuario
        int opcionesJuego = 0;

        Console.WriteLine("====== JUEGO DE ROL ======"); 
           
        do
        {  
            Console.WriteLine("--> Elija alguna de las siguientes opciones: \n1) Jugar \n2) Mostrar historial de partidas \n3) Salir del juego");
            int.TryParse(Console.ReadLine(), out opcionesJuego);
            
        } while (opcionesJuego < 1 || opcionesJuego > 3);

        Console.WriteLine("==============================================");

        return opcionesJuego;

    }

    public static List<Personaje> CargarPersonajes(){
        
        List<Personaje> listaPersonajes = new List<Personaje>();

        // Consulta sobre modalidad de carga de personajes
        Console.WriteLine("¿Desea cargar personajes (desde archivo JSON) o generarlos aleatoriamente?");

        int consulta = ConsultarAUsuario();

        if(consulta == 1) CargarPersonajesDesdeJson(consulta, ref listaPersonajes);
        // Se carga personajes desde JSON. Si hay un error en la carga, la lista de personajes se mantiene vacía. 

        if(consulta == 2 || !listaPersonajes.Any()) listaPersonajes = ProcesosJuego.CrearPersonajesAleatoriamente();
        // Si originalmente el usuario deseaba cargar personajes de manera aleatoria o si no se pudo cargar desde JSON, entonces se generan los personajes aleatoriamente

        return listaPersonajes;

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

    private static void CargarPersonajesDesdeJson(int consulta, ref List<Personaje> listaPersonajes){

        bool existeArchivo = false;

        do
        {
            Console.WriteLine("Indique la ruta del archivo JSON:");
            string? rutaArchivoJSON = Console.ReadLine();
            existeArchivo = File.Exists(rutaArchivoJSON);

            if(existeArchivo){

                LeerPersonajesDesdeJson(rutaArchivoJSON, ref listaPersonajes);

            } else {

                Console.WriteLine("No se encontró el archivo buscado. ¿Desea ingresar otra ruta o cargar personajes aleatoriamente?");

                consulta = ConsultarAUsuario();

            }

        } while (!existeArchivo && consulta != 2);
        // Si el archivo existe, se terminará el proceso iterativo condicional habiendo obtenido o una lista vacia o con al menos un personaje

    }

    private static void LeerPersonajesDesdeJson(string rutaArchivoJSON, ref List<Personaje> listaPersonajesJSON){

        StreamReader reader = new StreamReader(rutaArchivoJSON);
        // NOTA: la ruta de archivo no será vacía, pues se controló que el archivo exista previamente y esta función es llamada si el archivo existe.
        
        string? datosPersonajes = reader.ReadLine();

        if(!string.IsNullOrEmpty(datosPersonajes)){

            try
            {
                listaPersonajesJSON = JsonSerializer.Deserialize<List<Personaje>>(datosPersonajes);
            }
            catch 
            {
                Console.WriteLine("ERROR: El archivo JSON contiene información inválida o no pudo ser leído correctamente. Se generarán los personajes aleatoriamente... ");

            }

        } else {
            Console.WriteLine("ERROR: No se encontró información de personajes. Se generarán aleatoriamente... ");
        }

        reader.Close();
        // Se cierra el stream de lectura

    }
   

}
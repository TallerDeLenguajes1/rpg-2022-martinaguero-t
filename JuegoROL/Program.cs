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
                    
                    // Aplicación de efectos del clima (API)
                    UtilidadClima.AplicarEfectosClima(listaPersonajes);
                    
                    ProcesosJuego.Pausa();

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


        Console.WriteLine("================================================================================= \n===> JUEGO DE ROL \n=================================================================================");
           
        do
        {
            MenuDeOpcionesView();
            int.TryParse(Console.ReadLine(), out opcionesJuego);

        } while (opcionesJuego < 1 || opcionesJuego > 3);

        Console.WriteLine("=================================================================================");

        return opcionesJuego;

    }

    private static void MenuDeOpcionesView()
    {
        Console.WriteLine("--> Elija alguna de las siguientes opciones: \n1) Jugar \n2) Mostrar historial de partidas \n3) Salir del juego");
    }

    public static List<Personaje> CargarPersonajes(){
        
        List<Personaje> listaPersonajes;

        Console.WriteLine("¿Desea cargar personajes (desde archivo JSON) o generarlos aleatoriamente?");

        do {

            int modalidad = SeleccionModalidadCargaPersonajesView();

            if(modalidad == 1){ 
                listaPersonajes = CargarPersonajesDesdeJson(modalidad);
            } else {
                listaPersonajes = ProcesosJuego.CrearPersonajesAleatoriamente();
            } 
            
            // Si esta vacia la lista (lo cual solo puede ocurrir si falla la carga desde JSON), entonces se muestra el siguiente mensaje:

            if(!listaPersonajes.Any()){
                Console.WriteLine("\nNo se pudo cargar personajes desde JSON. \n¿Desea intentar de nuevo o cargar los personajes de manera aleatoria?");
            }

            if(modalidad == 1 && listaPersonajes.Any()){
                Console.WriteLine("\nCarga exitosa desde JSON.");
            }
            
        } while(!listaPersonajes.Any());

        Console.WriteLine("=================================================================================");

        return listaPersonajes;

    }

    private static int SeleccionModalidadCargaPersonajesView(){
        
        int modalidad; 

        do
        {
            Console.WriteLine("1) Cargar desde archivo JSON");
            Console.WriteLine("2) Generar aleatoriamente");
            int.TryParse(Console.ReadLine(), out modalidad);

        } while (modalidad < 1 || modalidad > 2);

        return modalidad;

    }

    /// <summary>
    /// Se carga personajes desde JSON. Si hay un error en la carga, se devuelve una lista de personajes vacía.
    /// </summary>
    private static List<Personaje> CargarPersonajesDesdeJson(int modalidad){

        bool existeArchivo = false;

        Console.WriteLine("\nIndique la ruta del archivo JSON:");

        string? rutaArchivoJSON = Console.ReadLine();
        existeArchivo = File.Exists(rutaArchivoJSON);

        if(existeArchivo) return LeerPersonajesDesdeJson(rutaArchivoJSON);

        return new List<Personaje>();

    }

    private static List<Personaje> LeerPersonajesDesdeJson(string rutaArchivoJSON){

        List<Personaje> personajes;
        StreamReader reader = new StreamReader(rutaArchivoJSON);
        
        string? datosPersonajes = reader.ReadLine();
        reader.Close();

        try
        {
            personajes = JsonSerializer.Deserialize<List<Personaje>>(datosPersonajes);
        }
        catch 
        {
            personajes = new List<Personaje>();
        } 

        return personajes;

    }
   

}
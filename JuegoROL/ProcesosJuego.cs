namespace JuegoROL;
public class ProcesosJuego
{
    // Clase con métodos usados para el desarrollo del juego
    public static void DesarrollarJuegoPorRondas(List<Personaje> listaPersonajes)
    {
        if(listaPersonajes.Count() > 2){

            Console.WriteLine("=====> NUEVO TORNEO por RONDAS <=====");

            MostrarListaPersonajes(listaPersonajes);

            ManejoDeArchivos.GuardarJugadoresJSON(listaPersonajes);
            // Guardo la información de los jugadores generados en un archivo json.

            int numRonda = 1;

            // Combate por rondas entre dos personajes
            while (listaPersonajes.Count > 1)
            {

                Console.WriteLine($"====> RONDA {numRonda}:");
                Personaje jugador1 = listaPersonajes[0];
                Personaje jugador2 = listaPersonajes[1];

                Console.WriteLine($"~ Los jugadores de esta ronda son: {jugador1.DatosPersonaje.Nombre}, {jugador1.DatosPersonaje.Apodo} y {jugador2.DatosPersonaje.Nombre}, {jugador2.DatosPersonaje.Apodo}");

                // Se desarrolla el combate por turnos entre dos jugadores.
                CombatePorTurnos(jugador1, jugador2);

                // Se muestra y bonifica la salud del ganador de la ronda
                MostrarYBonificarGanadorRonda(listaPersonajes, jugador1, jugador2);

                numRonda++;
                Console.ReadLine();
                
            }

            Console.WriteLine("============================================================================");
            Console.WriteLine($"GANADOR/A DE ESTE TORNEO: ¡{listaPersonajes[0].DatosPersonaje.Nombre}, {listaPersonajes[0].DatosPersonaje.Apodo}!");
            Console.WriteLine("============================================================================\n");
            
            ManejoDeArchivos.GuardarInformacionGanador(listaPersonajes[0]);

        } else {

            Console.WriteLine("Hubo un error en la carga de los personajes. Intente nuevamente.");
            
        }

    }

    private static void CombatePorTurnos(Personaje jugador1, Personaje jugador2)
    {
        // Variables auxiliares
        int cantidadTurnos = 3 * 2;
        bool turnoJugador1 = true;
        int numTurno = 1;

        // Desarrollo de turnos de la ronda
        while (numTurno <= cantidadTurnos && jugador1.DatosPersonaje.Salud > 0 && jugador2.DatosPersonaje.Salud > 0)
        {
            // Este bucle se repetirá hasta que se completen todos los turnos o hasta que algún personaje quede con salud 0.
            Console.WriteLine($"==> TURNO {numTurno}:");

            if (turnoJugador1){
                Atacar(jugador1, jugador2);
                turnoJugador1 = false;
                // En el próximo turno será el turno del jugador 2
            } else {
                Atacar(jugador2, jugador1);
                turnoJugador1 = true;
                // En el próximo turno será el turno del jugador 1
            }

            numTurno++;

        }
    }

    private static void MostrarYBonificarGanadorRonda(List<Personaje> listaPersonajes, Personaje jugador1, Personaje jugador2)
    {
        if (jugador1.DatosPersonaje.Salud == jugador2.DatosPersonaje.Salud)
        {
            Console.WriteLine("¡Hubo un empate!");

        } else{

            if (jugador1.DatosPersonaje.Salud > jugador2.DatosPersonaje.Salud){

                Console.WriteLine($"¡El ganador de esta ronda es el jugador: {jugador1.DatosPersonaje.Nombre}, {jugador1.DatosPersonaje.Apodo}!\n");

                listaPersonajes.Remove(jugador2);
                BonificarSaludGanador(jugador1);

            } else {

                Console.WriteLine($"¡El ganador de esta ronda es el jugador: {jugador2.DatosPersonaje.Nombre}, {jugador2.DatosPersonaje.Apodo}!\n");

                listaPersonajes.Remove(jugador1);
                BonificarSaludGanador(jugador2);

            }
        }
    }
    private static void BonificarSaludGanador(Personaje jugadorGanador)
    {
        float bonus = jugadorGanador.DatosPersonaje.Salud * (float)0.2;

        if (jugadorGanador.DatosPersonaje.Salud + bonus > 100){
            jugadorGanador.DatosPersonaje.Salud = 100;
        } else {
            jugadorGanador.DatosPersonaje.Salud += bonus;
        }

    }

    private static void Atacar(Personaje jugadorAtacante, Personaje jugadorDefensor)
    {
        Random rand = new Random(Guid.NewGuid().GetHashCode());

        float maximoDanoProvocable = 500;

        float poderDisparo = jugadorAtacante.CaracteristicasPersonaje.Destreza * jugadorAtacante.CaracteristicasPersonaje.Fuerza * jugadorAtacante.CaracteristicasPersonaje.Nivel;

        float efecDisparo = (float)rand.Next(1, 101) / 100;
        // Se considera como porcentual

        float valorAtaque = poderDisparo * efecDisparo * (float)1.7 * ((float)rand.Next(100, 201) / 100);

        float poderDefensa = jugadorDefensor.CaracteristicasPersonaje.Armadura * jugadorDefensor.CaracteristicasPersonaje.Velocidad + ((float)rand.Next(200, 301) / 100);

        float danoProvocado = Math.Abs(((((valorAtaque * efecDisparo) - poderDefensa)) / maximoDanoProvocable) * 100);

        if (jugadorDefensor.DatosPersonaje.Salud - danoProvocado > 0)
        {
            jugadorDefensor.DatosPersonaje.Salud -= danoProvocado;
        }
        else
        {
            jugadorDefensor.DatosPersonaje.Salud = 0;
        }
        // Control de salud de personaje.

        MostrarResultadosTurno(jugadorAtacante, jugadorDefensor, danoProvocado);

    }

    private static void MostrarResultadosTurno(Personaje jugadorAtacante, Personaje jugadorDefensor, float danoProvocado)
    {
        if (danoProvocado > 30) {
            Console.WriteLine($"¡El personaje {jugadorAtacante.DatosPersonaje.Nombre} ({jugadorAtacante.DatosPersonaje.Apodo}) atacó a {jugadorDefensor.DatosPersonaje.Nombre} ({jugadorDefensor.DatosPersonaje.Apodo}), causando un daño CRÍTICO de {danoProvocado}!");
        } else {
            Console.WriteLine($"El personaje {jugadorAtacante.DatosPersonaje.Nombre} ({jugadorAtacante.DatosPersonaje.Apodo}) atacó a {jugadorDefensor.DatosPersonaje.Nombre} ({jugadorDefensor.DatosPersonaje.Apodo}), causando un daño de {danoProvocado}.");
        }
        // Indicador de daño critico!

        Console.WriteLine($"Salud de {jugadorAtacante.DatosPersonaje.Nombre}: {jugadorAtacante.DatosPersonaje.Salud} HP (atacante) || Salud de {jugadorDefensor.DatosPersonaje.Nombre}: {jugadorDefensor.DatosPersonaje.Salud} HP (defensor)");

        Console.Write("\n");
    }

    private static void MostrarListaPersonajes(List<Personaje> listaPersonajes)
    {
        Console.WriteLine("-> Se cargaron los siguientes personajes: ");

        short i = 1;

        foreach (var personaje in listaPersonajes)
        {
            Console.WriteLine($"PERSONAJE {i}");
            MetodosPersonajes.MostrarDatosPersonaje(personaje);
            i++;
            // Solo se muestran los datos
        }

        Console.WriteLine("======================>\n");
    }

    public static void CargarPersonajes(List<Personaje> listaPersonajes)
    {

        Random rand = new Random();
        int cantidadPersonajes = rand.Next(2, 6);
        // Se crean aleatoriamente entre 2 y 5 personajes.

        for (int i = 0; i < cantidadPersonajes; i++)
        {
            listaPersonajes.Add(MetodosPersonajes.CrearPersonaje());
        }

    }

    /*
    public static void ConsultarSobreCargaPersonajes(){

        int consulta = 1;
        var listaPersonajes = new List<Personaje>();

        do
        {
            Console.WriteLine("¿Desea cargar personajes (desde archivo JSON) o generarlos aleatoriamente?");
            Console.WriteLine("1) Cargar desde archivo JSON");
            Console.WriteLine("2) Generar aleatoriamente");
            int.TryParse(Console.ReadLine(), out consulta);

        } while (consulta < 1 || consulta > 2);

        if(consulta == 1){
            
            bool existeArchivo = false;

            do
            {
                Console.WriteLine("Indique la ruta del archivo JSON: ");
                string rutaArchivo = Console.ReadLine();

                existeArchivo = File.Exists(rutaArchivo);

                if(existeArchivo){

                    StreamReader reader = new StreamReader(rutaArchivo);
                    // CONSULTA: ¿está bien cerrar el stream en todos los casos?
                    
                    string datosPersonajes = reader.ReadLine();

                    if(!string.IsNullOrEmpty(datosPersonajes)){
                        
                        try
                        {
                            listaPersonajes = JsonSerializer.Deserialize<List<Personaje>>(reader.ReadLine());

                            if(listaPersonajes.Count < 2){
                                
                                reader.Close();
                                
                                Console.WriteLine("ERROR: Hubo un error al leer los personajes. Se generarán aleatoriamente...");
                                CargarPersonajes(listaPersonajes);
                                // Si la lista está vacía o tiene un solo personaje, no se considera válida para el combate pues se necesitan al menos dos personajes. Entonces, se generan aleatoriamente los personajes.

                            } 
                        }
                        catch
                        {
                            reader.Close();

                            Console.WriteLine("ERROR: El archivo JSON contiene información inválida. Se generarán los personajes aleatoriamente... ");
                            CargarPersonajes(listaPersonajes);
                        }
                        
                        // Consulta: está bien trabajar de esta manera?
                        

                    } else {

                        reader.Close();

                        Console.WriteLine("ERROR: No se encontró información de personajes. Se generarán aleatoriamente... ");
                        CargarPersonajes(listaPersonajes);

                        // Si el archivo está vacío, se generarán aleatoriamente los personajes.

                    }

                    reader.Close();

                } else {

                    Console.WriteLine("No se encontró el archivo buscado. ¿Desea ingresar otra ruta o cargar personajes aleatoriamente?");
                    do
                    {
                        Console.WriteLine("1) Ingresar nueva ruta");
                        Console.WriteLine("2) Cargar aleatoriamente");
                        int.TryParse(Console.ReadLine(),out consulta);

                    } while (consulta < 1 || consulta > 2);

                }

            } while (!existeArchivo && consulta != 2);
            
            CargarPersonajes(listaPersonajes);
            // En el caso en que se haya decidido cargar personajes aleatoriamente.

        } else {

            CargarPersonajes(listaPersonajes);

        }

        Console.Write("\n");

        DesarrollarJuegoPorRondas(listaPersonajes);

    }
    */
}

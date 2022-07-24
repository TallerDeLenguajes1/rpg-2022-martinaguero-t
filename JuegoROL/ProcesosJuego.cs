namespace JuegoROL;
public class ProcesosJuego
{
    // Clase con métodos usados para el desarrollo del juego
    public static Personaje DesarrollarJuegoPorRondas(List<Personaje> listaPersonajes)
    {
        // Método que desarrolla el juego por rondas y retorna el personaje ganador

        Console.WriteLine("=====> NUEVO TORNEO por RONDAS <=====");

        MostrarPersonajes(listaPersonajes);

        int numRonda = 1;

        // Combate por rondas entre dos personajes
        while (listaPersonajes.Count > 1)
        {
            Personaje jugador1 = listaPersonajes[0];
            Personaje jugador2 = listaPersonajes[1];

            // Se muestran los jugadores de cada ronda
            MostrarContrincantesRonda(numRonda,jugador1,jugador2);

            // Se desarrolla el combate por turnos entre dos jugadores.
            CombatePorTurnos(jugador1, jugador2);

            // Procesamiento de los resultados
            Personaje? ganador = ObtenerGanadorRonda(jugador1, jugador2);

            // En caso de no haber empate, se elimina el perdedor y se bonifica al ganador (si es que no es la última ronda)
            if(ganador != null){

                if(ganador == jugador1){
                    EliminarPerdedor(listaPersonajes,jugador2);
                } else {
                    EliminarPerdedor(listaPersonajes,jugador1);
                }

                // No se bonifica al ganador del torneo (cuando queda un solo personaje, ese es el ganador)
                if(listaPersonajes.Count > 1) BonificarSaludGanador(ganador);
            }

            MostrarResultadoRonda(ganador);
            Pausa();

            numRonda++;
            
        }

        MostrarResultadoTorneo(listaPersonajes[0]);

        return listaPersonajes[0];

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

    private static Personaje? ObtenerGanadorRonda( Personaje jugador1, Personaje jugador2)
    {   
        if (jugador1.DatosPersonaje.Salud == jugador2.DatosPersonaje.Salud)
        {
            return null;
        } else{
            if (jugador1.DatosPersonaje.Salud > jugador2.DatosPersonaje.Salud){
                return jugador1;
            } else {
                return jugador2;
            }
        }
    }

    private static void EliminarPerdedor(List<Personaje> listaPersonajes,Personaje jugadorPerdedor){
        listaPersonajes.Remove(jugadorPerdedor);
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

    private static void MostrarResultadoRonda(Personaje jugadorGanador){

        if(jugadorGanador == null){
            Console.WriteLine("¡Hubo un empate!");
        } else {
            Console.WriteLine($"¡El/la ganador/a de la ronda es: {jugadorGanador.DatosPersonaje.Nombre}, {jugadorGanador.DatosPersonaje.Apodo}!");
        }

    }

    private static void MostrarContrincantesRonda(int numRonda, Personaje jugador1, Personaje jugador2){
        Console.WriteLine($"====> RONDA {numRonda}:");
        Console.WriteLine($"~ Los jugadores de esta ronda son: {jugador1.DatosPersonaje.Nombre}, {jugador1.DatosPersonaje.Apodo} y {jugador2.DatosPersonaje.Nombre}, {jugador2.DatosPersonaje.Apodo}");
    }
    public static void Pausa(){
        Console.WriteLine("Presione cualquier tecla para continuar...");
        Console.ReadLine();
    }

    public static void MostrarResultadoTorneo(Personaje ganador){
        Console.WriteLine("============================================================================");
        Console.WriteLine($"GANADOR/A DE ESTE TORNEO: ¡{ganador.DatosPersonaje.Nombre}, {ganador.DatosPersonaje.Apodo}!");
        Console.WriteLine("============================================================================\n");
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

    private static void MostrarPersonajes(List<Personaje> listaPersonajes)
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

    public static List<Personaje> CrearPersonajesAleatoriamente()
    {

        var listaPersonajes = new List<Personaje>();

        Random rand = new Random();

        int cantidadPersonajes = rand.Next(2,6);
        // Se crean aleatoriamente entre 2 y 5 personajes. 

        for (int i = 0; i < cantidadPersonajes; i++)
        {
            listaPersonajes.Add(MetodosPersonajes.CrearPersonaje());
        }

        return listaPersonajes;

    }

}

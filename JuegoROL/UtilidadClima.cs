using System.Net;
using System.Text.Json;
namespace JuegoROL;

public class UtilidadClima {

    public static List<Localidad> ObtenerClimaLocalidadesArgentina(){

        // Método que obtiene la lista de localidades con sus respectivos climas. Si hubo un error en la comunicación a la API, devuelve una lista vacía.

        List<Localidad> listaClimaLocalidadesArgentina;

        var url = $"https://ws.smn.gob.ar/map_items/weather";

        var request = (HttpWebRequest) WebRequest.Create(url);

        request.Method = "GET";
        request.ContentType = "application/json";
        request.Accept = "application/json";

        try
        {
            using (WebResponse response = request.GetResponse())
            {
                using (Stream strReader = response.GetResponseStream())
                {
                    using (StreamReader objReader = new StreamReader(strReader))
                    {
                        string lecturaJson = objReader.ReadToEnd();
                        
                        listaClimaLocalidadesArgentina = JsonSerializer.Deserialize<List<Localidad>>(lecturaJson);

                    }
                }
            }
        }
        catch (WebException ex)
        {
            listaClimaLocalidadesArgentina = new List<Localidad>();
        }

        return listaClimaLocalidadesArgentina;

    }

    private static string[] _localidadesPosibles = {"San Miguel de Tucumán","Córdoba","Mendoza","San Salvador de Jujuy","La Plata","Capital Federal","Neuquén","Ushuaia"};

    public static Localidad SeleccionarLocalidadAleatoriamente(List<Localidad> listaClimasLocalidades){

        // Se espera recibir una lista no vacia.

        Random rand = new Random();

        string nombreLocalidadSeleccionada = _localidadesPosibles[rand.Next(_localidadesPosibles.Count())];

        return listaClimasLocalidades.Find(localidad => localidad.Name == nombreLocalidadSeleccionada);


    }   

    public enum EstadoClima {
        Despejado,
        Neblina,
        Nublado,
        Lluvioso
    }

    public static void AplicarEfectosClima(List<Personaje> listaPersonajes){

        Console.WriteLine("=================================================================================");

        List<Localidad> listaClimaLocalidadesArgentina = ObtenerClimaLocalidadesArgentina();

        if(listaClimaLocalidadesArgentina.Any()){

            Localidad localidadElegida = SeleccionarLocalidadAleatoriamente(listaClimaLocalidadesArgentina);

            Console.WriteLine($"¡El escenario de combate será {localidadElegida.Name}!");

            EstadoClima estadoClima = ObtenerEstadoClimaLocalidad(localidadElegida);

            MostrarEfectosClima(estadoClima);
            AplicarEfectosClima(estadoClima,listaPersonajes);
            
        } else {

            Console.WriteLine("No se pudo obtener información del clima, así que el escenario de combate va a ser algún espacio de la memoria de su computadora(¡se puede usar la imaginación!).");

        }

        Console.WriteLine("=================================================================================");

    }

    public static EstadoClima ObtenerEstadoClimaLocalidad(Localidad localidad){

        EstadoClima estadoClima = EstadoClima.Despejado;
        // Por defecto el cielo está despejado.

        if(localidad.Weather.Description.ToLower().Contains("neblina")) estadoClima = EstadoClima.Neblina;

        if(localidad.Weather.Description.ToLower().Contains("nublado")) estadoClima = EstadoClima.Nublado;

        if(localidad.Weather.Description.ToLower().Contains("lluvia") || localidad.Weather.Description.ToLower().Contains("llovizna")) estadoClima = EstadoClima.Lluvioso;

        return estadoClima;

    }

    public static void MostrarEfectosClima(EstadoClima clima){
        
        switch(clima){
            case EstadoClima.Despejado:
                Console.WriteLine("¡El cielo está despejado! Los caballos tendrán menor dificultad para moverse por el escenario de combate. ¡Todos los caballeros ven incrementada su velocidad! Además, la gran cantidad de luz no les gusta a los magos, que pierden parte de su destreza.");
                break;
            case EstadoClima.Neblina:
                Console.WriteLine("La neblina desorienta a todas las unidades, reduciendo su velocidad. Además, a los arqueros les cuesta más apuntar, y a los soldados ver por su pequeño visor, por lo que decae su destreza.");
                break;
            case EstadoClima.Nublado:
                Console.WriteLine("Las nubes cubren el cielo. Los magos y los sacerdotes ganan más fuerza ya que la falta del sol les permite leer sus libros sin tener que entrecerrar los ojos, y la armadura a los soldados les resulta más liviana, por lo que ganan velocidad.");
                break;
            case EstadoClima.Lluvioso:
                Console.WriteLine("¡Está lloviendo! El suelo mojado hace que los caballos y los soldados se resbalen más, por lo que tienen que andar con cuidado (menos velocidad de movimiento). Los magos y sacerdotes tienen que resguardar sus libros, por lo que pierden destreza, y los arqueros aprovechan la situación para atacar a los demás (¡mayor fuerza!).");
                break;
            
        }

        
    }

    public static void AplicarEfectosClima(EstadoClima clima, List<Personaje> listaPersonajes){
        
        // En todos los casos, se decrementa o incrementa un 5% del valor a cada característica que corresponda.

        switch(clima){
            case EstadoClima.Despejado:
                foreach (var personaje in listaPersonajes)
                {   

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Knight) personaje.CaracteristicasPersonaje.Velocidad *= 1.05F;

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Mage) personaje.CaracteristicasPersonaje.Destreza *= 0.95F;
                }
                break;
            case EstadoClima.Neblina:
                foreach (var personaje in listaPersonajes)
                {   
                    personaje.CaracteristicasPersonaje.Velocidad *= 0.95F;

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Archer || personaje.DatosPersonaje.Tipo == TipoPersonaje.Soldier) personaje.CaracteristicasPersonaje.Destreza *= 0.95F;

                }
                break;
            case EstadoClima.Nublado:
                foreach (var personaje in listaPersonajes)
                {   

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Mage || personaje.DatosPersonaje.Tipo == TipoPersonaje.Priest) personaje.CaracteristicasPersonaje.Fuerza *= 1.05F;

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Soldier) personaje.CaracteristicasPersonaje.Velocidad *= 1.05F;

                }
                break;
            case EstadoClima.Lluvioso:
                foreach (var personaje in listaPersonajes)
                {   

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Knight || personaje.DatosPersonaje.Tipo == TipoPersonaje.Soldier) personaje.CaracteristicasPersonaje.Velocidad *= 0.95F;

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Mage || personaje.DatosPersonaje.Tipo == TipoPersonaje.Priest) personaje.CaracteristicasPersonaje.Destreza *= 0.95F;

                    if(personaje.DatosPersonaje.Tipo == TipoPersonaje.Archer) personaje.CaracteristicasPersonaje.Fuerza *= 1.05F;

                }
                break;
            
        }
        
    }
}
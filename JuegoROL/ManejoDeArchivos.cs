using System.Text.Json;
namespace JuegoROL;

public class ManejoDeArchivos{
    
    public static void GuardarInformacionGanador(Personaje ganador)
    {
        string registroGanadores = @"D:\Facultad\2do\Taller_de_Lenguajes_I\Repositorios\TPS\rpg-2022-martinaguero-t\JuegoROL\registroGanadores.csv";

        if(!File.Exists(registroGanadores)){
            StreamWriter swTituloTabla = new StreamWriter(registroGanadores);
            string tituloTabla = "Nombre;Apodo;Salud;Fecha de victoria";
            swTituloTabla.WriteLine(tituloTabla);
            swTituloTabla.Close();
        }
        // Si no existe el archivo, se debe crear uno nuevo con los titulares de tabla

        using (StreamWriter sw = new StreamWriter(registroGanadores, true))
        {
            string datosGanador = ganador.DatosPersonaje.Nombre + ";" + ganador.DatosPersonaje.Apodo + ";" + ganador.DatosPersonaje.Salud + " HP;" + DateTime.Now.ToString("dd/MM/yyyy");
            sw.WriteLine(datosGanador);
        }

    }

    public static void MostrarHistorial(){

        string registroGanadores = @"D:\Facultad\2do\Taller_de_Lenguajes_I\Repositorios\TPS\rpg-2022-martinaguero-t\JuegoROL\registroGanadores.csv";

        if(!File.Exists(registroGanadores)){
            Console.WriteLine("No se encontraron registros de combates.");
        } else {
            // Si el archivo existe, entonces tiene la cabecera y la información de al menos un combate.
            Console.WriteLine("REGISTRO DE COMBATES: ");

            using(StreamReader sr = new StreamReader(registroGanadores)){

                var linea = sr.ReadLine();

                while(linea != null){

                    var datosColumnas = linea.Split(";");

                    Console.WriteLine(" ===============================================================================");
                    Console.WriteLine($"| {datosColumnas[0].PadRight(17)} | {datosColumnas[1].PadRight(17)} | {datosColumnas[2].PadRight(17)} | {datosColumnas[3].PadRight(17)} |");
                    Console.WriteLine(" ===============================================================================");
                    
                    linea = sr.ReadLine();

                }

                sr.Close();
                
            }
        }
    }

    public static void GuardarJugadoresJSON(List<Personaje> listaPersonajes){

        string rutaArchivoJson = @"D:\Facultad\2do\Taller_de_Lenguajes_I\Repositorios\TPS\rpg-2022-martinaguero-t\JuegoROL\jsonJuegoROL\registroJugadores.json";

        string jugadoresGenerados = JsonSerializer.Serialize(listaPersonajes);

        using (StreamWriter writer = new StreamWriter(rutaArchivoJson,true))
        {
            writer.WriteLine(jugadoresGenerados);
        }

    }
}
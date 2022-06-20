namespace JuegoROL;

public class MetodosPersonajes{

    public const int maxDestreza = 5;
    public const int maxOtrasCaracteristicas = 10;

    public static Personaje CrearPersonaje(){

        Datos datosAleatorios = GenerarDatosAleatorios();
    
        Caracteristicas caracteristicasAleatorias = GenerarCaracteristicasAleatorias();

        Personaje nuevoPersonaje = new Personaje(caracteristicasAleatorias,datosAleatorios);

        BonusPersonaje(nuevoPersonaje);

        return nuevoPersonaje;

    }

    private static string[] _nombresPosibles = {"Marugui","Bashushi","Patuchi","Ñaruchi","Mishushi","Sapuchi","Molugui","Awuti"};
    private static string[] _apodosPosibles = {"Luna azul","Pato verde","Montaña violeta","Gato rojo","Lobo amarillo","Sol blanco"};
    
    public static Datos GenerarDatosAleatorios(){

        Random rand = new Random();

        var tipos = Enum.GetValues<TipoPersonaje>();

        TipoPersonaje tipoAleatorio = tipos[rand.Next(tipos.Length)];

        string nombreAleatorio = _nombresPosibles[rand.Next(_nombresPosibles.Length)];

        string apodoAleatorio = _apodosPosibles[rand.Next(_apodosPosibles.Length)];

        DateTime fechaNacimientoAleatoria = new DateTime(rand.Next(1000,2023),rand.Next(1,12),rand.Next(1,28));

        int edadAleatoria = DateTime.Now.Year - fechaNacimientoAleatoria.Year;

        if((DateTime.Now.Month == fechaNacimientoAleatoria.Month && DateTime.Now.Day < fechaNacimientoAleatoria.Day) || DateTime.Now.Month < fechaNacimientoAleatoria.Month) edadAleatoria--;
      
        return new Datos(tipoAleatorio,nombreAleatorio,apodoAleatorio,fechaNacimientoAleatoria,edadAleatoria,100);

    }
  
    public static Caracteristicas GenerarCaracteristicasAleatorias(){

        Random rand = new Random();

        return new Caracteristicas(rand.Next(1,maxOtrasCaracteristicas),rand.Next(1,maxDestreza),rand.Next(1,maxOtrasCaracteristicas),rand.Next(1,maxOtrasCaracteristicas),rand.Next(1,maxOtrasCaracteristicas));


    }

    public static void MostrarInformacionPersonaje(Personaje personajeAMostrar){

        Console.WriteLine("=== INFORMACIÓN DEL PERSONAJE: ");

        MostrarDatosPersonaje(personajeAMostrar);
        MostrarCaracteristicasPersonaje(personajeAMostrar);

    }

    public static void MostrarDatosPersonaje(Personaje personajeAMostrar){

        Console.WriteLine("  --> Datos:");
        Console.WriteLine($"   - Nombre: {personajeAMostrar.DatosPersonaje.Nombre}");
        Console.WriteLine($"   - Apodo: {personajeAMostrar.DatosPersonaje.Apodo}");
        Console.WriteLine($"   - Fecha de nacimiento: {personajeAMostrar.DatosPersonaje.FechaNacimiento.ToString("dd/MM/yyyy")}");
        
        Console.WriteLine($"   - Edad: {personajeAMostrar.DatosPersonaje.Edad} años");
        Console.WriteLine($"   - Tipo: {personajeAMostrar.DatosPersonaje.Tipo}");
        Console.WriteLine($"   - Salud: {personajeAMostrar.DatosPersonaje.Salud} HP");

    }   

    public static void MostrarCaracteristicasPersonaje(Personaje personajeAMostrar){

        Console.WriteLine("  --> Características:");
        Console.WriteLine($"   - Velocidad: {personajeAMostrar.CaracteristicasPersonaje.Velocidad}");
        Console.WriteLine($"   - Destreza: {personajeAMostrar.CaracteristicasPersonaje.Destreza}");
        Console.WriteLine($"   - Fuerza: {personajeAMostrar.CaracteristicasPersonaje.Fuerza}");
        Console.WriteLine($"   - Nivel: {personajeAMostrar.CaracteristicasPersonaje.Nivel}");
        Console.WriteLine($"   - Armadura: {personajeAMostrar.CaracteristicasPersonaje.Armadura}");

    }

    private static void BonusPersonaje(Personaje personaje){

        float bonus = 0;
        float incremento = 1.1F;
        // En cada caso el bonus solo se aplicará si el personaje no tiene el mayor valor posible en la correspondiente característica

        switch (personaje.DatosPersonaje.Tipo)
        {
            case TipoPersonaje.Mage:

                bonus =  personaje.CaracteristicasPersonaje.Destreza*incremento;

                if(bonus <= maxDestreza)
                personaje.CaracteristicasPersonaje.Destreza = (float) Math.Round(bonus, 2);

                break;

            case TipoPersonaje.Warrior:

                bonus = personaje.CaracteristicasPersonaje.Fuerza*incremento;

                if(bonus <= maxOtrasCaracteristicas)
                personaje.CaracteristicasPersonaje.Fuerza = (float) Math.Round(bonus, 2);
                
                break;

            case TipoPersonaje.Priest:

                bonus = personaje.CaracteristicasPersonaje.Destreza*incremento;

                if(bonus <= maxDestreza)
                personaje.CaracteristicasPersonaje.Destreza = (float) Math.Round(bonus, 2);
                
                break;

            case TipoPersonaje.Archer:
                
                bonus = personaje.CaracteristicasPersonaje.Velocidad*incremento;

                if(bonus <= maxOtrasCaracteristicas)
                personaje.CaracteristicasPersonaje.Velocidad = (float) Math.Round(bonus, 2);
                
                break;

            case TipoPersonaje.Knight:

                bonus = personaje.CaracteristicasPersonaje.Armadura*incremento;

                if(bonus <= maxOtrasCaracteristicas)
                personaje.CaracteristicasPersonaje.Armadura = (float) Math.Round(bonus, 2);

                break;

        }
    }

}
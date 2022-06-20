namespace JuegoROL;

public class Personaje
{
    public Caracteristicas CaracteristicasPersonaje {get; set;}
    public Datos DatosPersonaje {get; set;}

    public Personaje(Caracteristicas caracteristicasPersonaje, Datos datosPersonaje){

        CaracteristicasPersonaje = caracteristicasPersonaje;

        DatosPersonaje = datosPersonaje;

    }

    public Personaje(){
        CaracteristicasPersonaje = MetodosPersonajes.GenerarCaracteristicasAleatorias();
        DatosPersonaje = MetodosPersonajes.GenerarDatosAleatorios();
    }
    // Constructor sin parámetros para soporte de deserialización JSON

    
}
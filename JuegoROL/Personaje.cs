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
    // CONSULTA: ¿está bien este constructor sin parámetros usado solo para deserialización JSON?
    
}
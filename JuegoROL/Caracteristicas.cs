namespace JuegoROL;
public class Caracteristicas
{
    public float Velocidad {get; set;}
    public float Destreza {get; set;}
    public float Fuerza {get; set;}
    public float Nivel {get; set;}
    public float Armadura {get; set;}

    public Caracteristicas(float velocidad, float destreza, float fuerza, float nivel, float armadura){
        Velocidad = velocidad;
        Destreza = destreza;
        Fuerza = fuerza;
        Nivel = nivel;
        Armadura = armadura;
    }

    public Caracteristicas(){}
    // Constructor sin parámetros para soporte de deserialización JSON

}
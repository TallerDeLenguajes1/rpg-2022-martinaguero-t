namespace JuegoROL;

public enum TipoPersonaje{
    Mage,
    Soldier,
    Priest,
    Archer,
    Knight
}

public class Datos
{
    public TipoPersonaje Tipo {get; set;}
    public string? Nombre {get; set;}
    public string? Apodo {get; set;}
    public DateTime FechaNacimiento {get; set;}
    public int Edad {get; set;}
    public float Salud {get; set;}

    public Datos(TipoPersonaje tipo, string nombre, string apodo, DateTime fechaNacimiento, int edad, int salud){
        Tipo = tipo;
        Nombre = nombre;
        Apodo = apodo;
        FechaNacimiento = fechaNacimiento;
        Edad = edad;
        Salud = salud;
    }
    public Datos(){}
    // Constructor sin parámetros para soporte de deserialización JSON

}

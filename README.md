# Juego de rol - Registro de avances

## Datos adicionales

1) **Convención de nombres**

    | Case usada |                                                                             |
    |-----------------------|-----------------------------------------------------------------------------|
    | camelCase             | parámetros de constructores y métodos - nombres de variables usadas en métodos y en Main                                       |
    | _camelCase            | campos privados de clases                                                   |
    | PascalCase            | nombres de métodos, clases, struct y namespaces - campos públicos de clases - nombres de enums |

    _Referencia: <https://docs.microsoft.com/en-us/dotnet/csharp/fundamentals/coding-style/coding-conventions>_

---

## Historial de cambios

### Versión 6

- **Trabajo con JSON (correcciones)**

La función implementada para la consulta al usuario sobre la carga de personajes de manera aleatoria o desde un archivo JSON fue movida a la clase Program ya que se considera como una función de la interfaz de interacción con el usuario.

---

### Versión 5

- **Trabajo con JSON**

Se implementaron dos funciones de utilidad para el trabajo con archivos JSON: una que guarda la información de todos los jugadores generados en una partida en un archivo JSON y otra usada para poder desarrollar una partida con datos de jugadores obtenidos de un archivo JSON.

---

### Versión 4

- **Carga de datos de los ganadores a archivo .csv**

Se implementó la carga de datos de los ganadores de cada partida a un archivo .csv., para eso se creó una nueva clase **ManejoDeArchivos** con métodos para escribir el archivo .csv y leer datos del archivo.

---

### Versión 3

- **Nueva clase MetodosPersonajes**

Los métodos correspondientes a la generación de personajes, datos y características aleatorios fueron asignados a otra nueva clase MetodosPersonajes con la finalidad de usar la clase ProcesosJuego para todo lo correspondiente al desarrollo del juego, como el combate.

Para el combate, se creó un nuevo método **DesarrollarJuego** que se encarga de generar un nuevo torneo cargando una lista de personajes y desarrollando el combate entre los mismos.

- **Modificaciones sobre el cálculo de las variables usadas para el combate**

Con el fin de generar valores aleatorios de mayor magnitud, se redujo el valor de _maximoDanoProvocable_ de 50000 a 500. Además, como el resto de las variables (excepto _efecDisparo_) dependen completamente de características que para un mismo combate entre dos personajes no varían, se obtienen siempre los mismos valores en los turnos correspondientes a cada personaje. Por eso, se implementó en el cálculo de _valorAtaque_ y _poderDefensa_ un producto adicional de un porcentual de un valor aleatorio y una constante elegida tras realizar varias pruebas para generar mayor aleatoriedad.

- **Bonificación para el personaje ganador de cada ronda**

Para evitar desbalance entre los personajes, decidí solamente incrementar la salud del personaje ganador de cada ronda y no cambiar otras características.

---

### Versión 2

- **Métodos para el desarrollo del juego**
Se implementaron tres nuevos métodos que permiten mostrar por pantalla información de un personaje.

Se implementó un método, **BonusPersonaje** que se aplica al crear un personaje y otorga un bonus adicional del 10% sobre alguna de las características del personaje en base a su tipo. Los bonus son los siguientes: Mage y Priest - Destreza, Warrior - Fuerza, Archer - Velocidad, Knight - Armadura. NOTA: el bonus solo es aplicable si el personaje no tiene el mayor valor posible en la caracteristica a bonificar.

---

### Versión 1

- **Implementación de los personajes**
Se crearon nuevas clases que permiten el manejo de personajes. La clase principal es **Personaje**, con dos campos que a su vez corresponden a otras dos clases: **Caracteristicas** y **Datos** del personaje.

- **Métodos para el desarrollo del juego**
Se creó una nueva clase llamada **ProcesosJuego** con métodos que serán usados para el desarrollo del juego (creación de personajes, generación aleatoria de datos de un personaje, desarrollo del combate, entre otros).

Para crear un personaje (instancia de la clase Personaje), se usa un método llamado **CrearPersonaje** que usa otros dos métodos (**GenerarDatosAleatorios** y **GenerarCaracteristicasAleatorias**) para instanciar un objeto de la clase Datos y otro de la clase Caracteristicas a fin de pasarlos como parámetro al constructor de la clase Personaje.

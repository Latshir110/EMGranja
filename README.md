# EMGranja

Juego en Unity 6 donde hay que alimentar a los animales de la granja lanzándoles comida. Se puede jugar con teclado o con un sensor EMG: al contraer el músculo del brazo, el granjero dispara.

![Gameplay](docs/img/gameplay.gif)

## Cómo se juega

- **Moverse:** izquierda / derecha
- **Disparar comida:** barra espaciadora o contraer el músculo (EMG)
- Cada animal alimentado suma 1 punto.
- Algunos animales intentan esquivar la comida.

## Control con EMG

![Demo EMG](docs/img/demo-emg.gif)

Los electrodos captan la señal del músculo y el módulo EMG la amplifica y filtra. El Arduino Leonardo la lee por el pin A0 y, si supera el umbral (`brecha`), presiona la barra espaciadora como si fuera un teclado USB. Unity la recibe igual que una tecla normal, así que el juego funciona con o sin el sensor.

![Circuito EMG](docs/img/circuito-emg.jpg)

**Materiales**
- Arduino Leonardo (necesita USB nativo para funcionar como teclado; un Arduino Uno no sirve)
- Módulo sensor EMG con entrada jack 3.5 mm
- 3 electrodos adhesivos: rojo sobre el músculo, verde como segundo electrodo y amarillo como tierra (sobre el hueso)
- Protoboard, batería y cables Dupont

**Conexión principal**
- `SIG` del módulo → pin `A0` del Arduino
- `GND` del módulo → `GND` del Arduino
- Batería → alimentación del módulo (`+Vs`, `-Vs`, `GND`)

El valor de `brecha` se ajusta para cada persona mirando la señal en el Serial Plotter del IDE de Arduino.

## Código

Los scripts de Unity están en `Assets/Scripts` y el código de Arduino en `Arduino/EMG_Teclado`. Haz clic en cada imagen para abrir el archivo.

### PlayerController

Mueve al granjero de izquierda a derecha sin salir de `XRange`. Al iniciar crea una pool de 20 proyectiles apagados, y al disparar (Espacio o EMG) reutiliza uno libre en vez de crear uno nuevo.

[![PlayerController](docs/img/codigo/PlayerController.png)](Assets/Scripts/PlayerController.cs)

### SpawnManager

Crea varios animales de cada tipo al inicio y los deja apagados. Cada 1,5 segundos busca uno libre, lo ubica en una X aleatoria y lo activa.

[![SpawnManager](docs/img/codigo/SpawnManager.png)](Assets/Scripts/SpawnManager.cs)

### MoveForward

Mueve la comida y los animales hacia adelante a velocidad constante.

[![MoveForward](docs/img/codigo/MoveForward.png)](Assets/Scripts/MoveForward.cs)

### DestroyOutOfBounds

Cuando un objeto sale de los límites en Z, lo desactiva para que vuelva a estar disponible en la pool.

[![DestroyOutOfBounds](docs/img/codigo/DestroyOutOfBounds.png)](Assets/Scripts/DestroyOutOfBounds.cs)

### DetectCollisions

Cuando la comida (tag `Comida`) toca a un animal, suma un punto, lo muestra en el texto de TextMeshPro y desactiva a ambos.

[![DetectCollisions](docs/img/codigo/DetectCollisions.png)](Assets/Scripts/DetectCollisions.cs)

### EsquivarComida

IA del animal con dos estados: avanzar y esquivar. Detecta la comida cercana con `OverlapSphere` y, según una probabilidad, se mueve hacia un lado por un momento.

[![EsquivarComida](docs/img/codigo/EsquivarComida.png)](Assets/Scripts/EsquivarComida.cs)

### Arduino: EMG_Teclado

Lee la señal del sensor EMG en el pin A0. Si supera la `brecha`, presiona la barra espaciadora con `Keyboard.h`. También muestra en el Serial Monitor los valores que pasan el umbral y el valor máximo registrado, lo que sirve para calibrar.

[![EMG_Teclado](docs/img/codigo/EMG_Teclado.png)](Arduino/EMG_Teclado/EMG_Teclado.ino)

## Decisiones técnicas

- **Object Pool:** la comida y los animales se crean una sola vez al inicio y después se reutilizan con `SetActive`, en vez de usar `Instantiate` y `Destroy` todo el tiempo. Esto importa porque con el EMG se puede disparar muchas veces seguidas.
- **Input System:** el disparo no depende de una tecla fija, por eso el EMG se pudo integrar sin cambiar el código del juego.
- **Máquina de estados simple:** los animales alternan entre avanzar y esquivar. Con dos comportamientos no hacía falta algo más complejo.

## Videos

- [Gameplay](docs/video/gameplay.mp4)
- [Demo con EMG](docs/video/demo-emg.mp4)

## Autora

Kristal Espinoza — Ingeniería en Realidad Virtual y Diseño de Juegos Digitales, Universidad Bernardo O'Higgins.


Proyecto en Behance: https://www.behance.net/gallery/259146535/EMGranja-Kristal-Espinoza

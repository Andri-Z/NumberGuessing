# 🎯 Number Guessing Game

Este es un proyecto basico de CLI que se basa en adivinar numeros.

Esta idea del proyecto esta basada aqui: https://roadmap.sh/projects/number-guessing-game 


## ✨ Características

- Juego de adivinar un número aleatorio entre 1 y 100.
- Niveles de dificultad (fácil, medio, difícil) que definen la cantidad máxima de intentos.
- Temporizador para medir el tiempo de cada partida.
- Validación de entrada robusta en menús y suposiciones.
- Estadísticas de rendimiento: intentos máximos y tiempo mínimo al final del juego.


## 🧩 Estructura del proyecto

- `Program`: Controla el flujo principal del juego (`Main`, bucle de repetición, respuesta de salida).
- `Game`: Lógica central del juego (generación de número, bucle de intento, evaluación).
- `ShowMessages`: Encapsula todos los mensajes al usuario (bienvenida, pérdidas, mejores resultados).
- `TimerControl`: Utiliza `Stopwatch` para medir controlar de juego.

## 🚀 Uso

1. Compila el proyecto en .NET (C#).
2. Ejecuta `Program.exe` (o el ejecutable generado).
3. Sigue las instrucciones en pantalla:
   - Selecciona tu nivel de dificultad (1–3).
   - Ingresa tus suposiciones.
   - Decide al final si quieres jugar otra partida.
4. Al salir, verás:
   - El menor número de intentos entre todas las partidas.
   - El menor tiempo transcurrido.


## 📁 Cómo clonar y ejecutar (desde consola)

```bash
git clone <URL_DEL_REPOSITORIO>
cd NumberGuessingGame
dotnet build
dotnet run


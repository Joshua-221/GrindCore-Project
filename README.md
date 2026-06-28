# ADR-04: Implementación de Patrones GoF

| Campo  | Valor |
|--------|-------|
| Autor  | Joshua |
| Fecha  | 28/06/2026 |
| Estado | `Aceptado` |

---

## Contexto

El sistema requiere calcular el 1RM basándose en diferentes fórmulas (Epley, etc.) y niveles de RPE. Originalmente, esta lógica estaba acoplada dentro del controlador mediante estructuras condicionales, lo que dificultaba el mantenimiento y la extensibilidad ante la adición de nuevas fórmulas o métodos de cálculo.

---

## Decisión

Se ha implementado el Patrón Strategy para encapsular las fórmulas de cálculo en clases independientes y el Patrón Factory Method para delegar la creación de la estrategia correcta basándose en el tipo de fórmula solicitada por el cliente.

### ¿Por qué?

- **Strategy:** Permite que el `CalculatorApiController` sea independiente de la lógica matemática. Cumple con el principio de Abierto/Cerrado (SOLID), permitiendo agregar nuevas fórmulas sin modificar el controlador.
- **Factory Method:** Centraliza la lógica de instanciación. El controlador no necesita conocer las clases concretas (`EpleyRpeStrategy`), únicamente depende de una interfaz (`ICalculationStrategy`), simplificando la gestión de dependencias.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Lógica estática/Hardcoded | Difícil de testear y propenso a errores al escalar. |
| Herencia simple | Crea una jerarquía rígida difícil de extender en tiempo de ejecución. |
| Service Locator | Oculta dependencias y hace el código más difícil de mantener (anti-patrón). |

---

## Consecuencias

**Lo que gano:**

- **Técnica:** Desacoplamiento total entre la capa de presentación (API) y la lógica de dominio. Escalabilidad inmediata para nuevas fórmulas.
- **Proceso:** Código más limpio, modular y fácil de someter a pruebas unitarias (unit testing).

**Lo que sacrifico o asumo:**

- **Limitación técnica:** Se introduce una ligera complejidad inicial al tener que gestionar más archivos (interfaces y clases concretas).
- **Deuda o riesgo:** Es necesario mantener la consistencia en el registro de los servicios en `Program.cs`. Si el proyecto crece demasiado, la `Factory` podría necesitar una refactorización hacia un registro por reflexión o mediante contenedores de inyección más avanzados.

## Declaración de IA

Para la elaboración de este ADR se utilizó Gemini (Google) como herramienta de asistencia en la redacción y estructuración del documento. Todas las decisiones de diseño, el análisis de alternativas y la justificación técnica aplicada al contexto de GrindCore son propias del autor. La IA fue utilizada exclusivamente como apoyo para expresar y documentar de forma clara las decisiones previamente razonadas.


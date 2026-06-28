# ADR-01: Adopción de Arquitectura MVC Inicial

| Campo  | Valor |
|--------|-------|
| Autor  | Joshua Isaí Cruz Mosqueda |
| Fecha  | 26/06/2026 |
| Estado | `Aceptado` |

---

## Contexto

Desarrollo de la primera versión de Grind Core, una aplicación web dedicada a la gestión de entrenamientos de powerlifting. Se requiere una estructura inicial en .NET que permita construir una base funcional y estable, sirviendo como punto de partida estructurado antes de realizar la transición hacia patrones arquitectónicos más desacoplados.

---

## Decisión

Implementar el patrón arquitectónico Modelo-Vista-Controlador (MVC) estructurado mediante las herramientas nativas de ASP.NET Core.

### ¿Por qué?

ASP.NET Core MVC utiliza el principio de convención sobre configuración. Esto resuelve el enrutamiento, la gestión de peticiones y el renderizado de la interfaz en un único proyecto unificado, eliminando la necesidad de configurar middlewares complejos, comunicación entre servicios o infraestructura adicional en las fases iniciales.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Arquitectura Hexagonal | Requiere una definición exhaustiva de puertos y adaptadores que eleva la complejidad innecesariamente para el alcance actual del repositorio. |
| Clean Architecture | La separación estricta en múltiples capas y proyectos independientes genera un exceso de abstracción difícil de justificar en este punto del desarrollo. |
| Minimal APIs + Frontend Independiente | Implica gestionar la configuración de CORS y mantener entornos de ejecución separados, lo que ralentiza el flujo de trabajo inicial. |

---

## Consecuencias

**Lo que gano:**

- Consecuencia técnica: Organización inmediata de los componentes web y acceso a datos guiado por las convenciones estándar del framework, simplificando el mantenimiento del código inicial.
- Consecuencia sobre el proceso o el equipo: Flujo de trabajo directo y centralizado en un solo proyecto, optimizando el tiempo al evitar configuraciones de infraestructura ajenas al dominio.

**Lo que sacrifico o asumo:**

- Limitación técnica: Fuerte acoplamiento entre la lógica de presentación y las reglas de negocio, lo que impide realizar pruebas unitarias completamente aisladas del contexto HTTP.
- Deuda o riesgo: La lógica del cálculo de los levantamientos quedará ligada a los controladores, asumiendo el compromiso de realizar una refactorización profunda para extraer el dominio en la siguiente rama.

## Declaración de IA 

- Para la elaboración de este ADR se utilizó Gemini (Google) como herramienta de asistencia en la redacción y estructuración del documento. Todas las decisiones de diseño, el análisis de alternativas y la justificación técnica aplicada al contexto de GrindCore son propias del autor. La IA fue utilizada exclusivamente como apoyo para expresar y documentar de forma clara las decisiones previamente razonadas.

## Diagrama

```mermaid
graph TD
    Client((Usuario / Navegador)) -->|HTTP Request| C[Controladores]
    C -->|Instancia / Modifica| M[Modelos / Lógica de Entrenamiento]
    C -->|Pasa datos| V[Vistas / Razor Pages]
    V -->|HTML / UI| Client
    M <--> DB[(PostgreSQL / SQLite)] 





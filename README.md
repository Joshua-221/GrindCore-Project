# ADR-02: Migración a Arquitectura Hexagonal

| Campo  | Valor |
|--------|-------|
| Autor  | Joshua Isaí Cruz Mosqueda |
| Fecha  | 28/06/2026 |
| Estado | `Aceptado` |

---

## Contexto

Estoy desarrollando GrindCore, una aplicación técnica de gestión para atletas de powerlifting. El sistema debe manejar rutinas, registrar levantamientos y calcular métricas de fuerza. El problema principal es que la estructura inicial no separaba la lógica de negocio de la infraestructura, lo que impide escalar el proyecto hacia una API REST profesional y realizar pruebas unitarias del motor de cálculo de 1RM.

---

## Decisión

Se decidió migrar a una Arquitectura Hexagonal, dividiendo el proyecto en tres capas: `GrindCore.Domain` (núcleo), `GrindCore.Infrastructure` (persistencia) y `GrindCore.Web` (interfaz y API).

### ¿Por qué?

Esta arquitectura garantiza que las reglas de negocio, como la fórmula de Epley para el 1RM, sean independientes de cualquier base de datos o framework web. Esto permite que el núcleo sea testable y que el proyecto sea mantenible a largo plazo.

### Alternativas consideradas

| Alternativa | Por qué la descarté |
|-------------|---------------------|
| Monolito acoplado | Dificulta la evolución de la API y las pruebas unitarias. |
| Arquitectura en capas tradicional | Genera acoplamiento directo entre la capa web y datos. |
| Microservicios | Excesiva complejidad operativa para un proyecto individual. |

---

## Consecuencias

**Lo que gano:**

- **Técnica**: Desacoplamiento total, permitiendo cambiar el almacenamiento o la interfaz sin modificar el núcleo de negocio.
- **Proceso**: Permite desarrollar la API REST de forma aislada mientras se refina la lógica del dominio.

**Lo que sacrifico o asumo:**

- **Limitación técnica**: Mayor esfuerzo inicial en la configuración de proyectos y referencias en Rider.
- **Deuda o riesgo**: Necesidad de ser estrictos con la visibilidad de los namespaces para no romper las capas.

## Declaración de IA

Para la elaboración de este ADR se utilizó Gemini (Google) como herramienta de asistencia en la redacción y estructuración del documento. Todas las decisiones de diseño, el análisis de alternativas y la justificación técnica aplicada al contexto de GrindCore son propias del autor. La IA fue utilizada exclusivamente como apoyo para expresar y documentar de forma clara las decisiones previamente razonadas.

## Diagrama

```mermaid
graph TD
    subgraph Web
    A[AthleteController / API]
    end
    subgraph Domain
    B[Domain Models / Interfaces]
    C[OneRepMaxCalculator]
    end
    subgraph Infrastructure
    D[InMemoryRepository]
    end
    
    A --> B
    D -.-> B

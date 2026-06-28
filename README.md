# ADR-03: Grind Core API

| Campo | Valor |
|-------|-------|
| Proyecto | Grind Core |
| Versión | 1.0.0 |
| Estatus | En desarrollo |

---

## Contexto

**Grind Core** es una plataforma de gestión para powerlifting diseñada para atletas y entrenadores que buscan precisión en su programación[cite: 1]. El sistema resuelve la necesidad de estimar la fuerza máxima (1RM) con base en cargas submáximas, considerando factores de fatiga reales del atleta para evitar proyecciones erróneas propias de fórmulas estándar[cite: 1].

---

## Características Principales

### Calculadora de 1RM con RPE
El motor principal de la API es un servicio de dominio que combina la fórmula de Epley con una tabla de factores de ajuste según el RPE (Rate of Perceived Exertion)[cite: 1].

### Arquitectura Hexagonal
El proyecto está estructurado para separar la lógica de negocio (`Domain`) de la infraestructura y la capa de presentación (`Web`), asegurando mantenibilidad y facilidad para realizar pruebas unitarias[cite: 1].

---

## Especificaciones de la API

El endpoint principal para obtener estimaciones es: `GET /api/calculator/1rm`

| Parámetro | Tipo | Requerido | Descripción |
|-----------|------|-----------|-------------|
| `weight`  | double | Sí | Peso levantado en kg[cite: 1] |
| `reps`    | int    | Sí | Repeticiones realizadas[cite: 1] |
| `rpe`     | int    | Sí | Esfuerzo percibido (1-10)[cite: 1] |

---

## Guía de Instalación y Uso

1. **Requisitos:** .NET 10 y un entorno configurado para ejecutar servicios web[cite: 1].
2. **Ejecución:** Utiliza tu IDE preferido (se recomienda el uso de herramientas JetBrains como Rider) para compilar y ejecutar el proyecto en modo `Development`[cite: 1].
3. **Pruebas:** Accede a la interfaz de Swagger local (`/swagger/index.html`) para interactuar con los endpoints y verificar las respuestas en formato JSON[cite: 1].

---

## Autor
* **Joshua Isaí Cruz Mosqueda**

## Declaración de IA

Para la elaboración de este ADR se utilizó Gemini (Google) como herramienta de asistencia en la redacción y estructuración del documento. Todas las decisiones de diseño, el análisis de alternativas y la justificación técnica aplicada al contexto de GrindCore son propias del autor. La IA fue utilizada exclusivamente como apoyo para expresar y documentar de forma clara las decisiones previamente razonadas.

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


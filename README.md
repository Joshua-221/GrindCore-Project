# ADR-01: Adopción de Arquitectura MVC Inicial[cite: 1]

| Campo  | Valor |[cite: 1]
|--------|-------|[cite: 1]
| Autor  | Joshua Isaí Cruz Mosqueda |[cite: 1]
| Fecha  | 26/06/2026 |[cite: 1]
| Estado | `Aceptado` |[cite: 1]

---

## Contexto[cite: 1]

Estamos construyendo la primera iteración de Grind Core, una aplicación web enfocada en gestionar los entrenamientos de atletas y coaches de powerlifting. La restricción principal de esta rama (`main`) es el tiempo crítico, ya que debe entregarse antes de las 12:00 PM de hoy. Se está desarrollando en .NET y se requiere un punto de partida rápido que sirva como base funcional antes de realizar refactorizaciones hacia patrones más complejos.[cite: 1]

---

## Decisión[cite: 1]

Se decidió implementar la arquitectura **Modelo-Vista-Controlador (MVC)** nativa del framework ASP.NET Core.[cite: 1]

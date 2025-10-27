# TecWebFest — Examen (90 minutos)

**Objetivo:** Evaluar dominio de relaciones 1:N, N:M y 1:1 en EF Core y el manejo de arquitectura por capas (Controller ➜ Service ➜ Repository ➜ DbContext) implementando endpoints clave para un mini‑sistema de festival de música.

## Contexto del dominio
- Un **Festival** tiene muchas **Stages** (1:N).
- Un **Artist** puede tocar en muchas **Stages**, y una **Stage** recibe muchos **Artists** a través de **Performance**, con horario (N:M con payload).
- Un **Attendee** puede comprar muchos **Tickets** (1:N). Cada ticket pertenece a un **Festival**.
- Un **Attendee** puede tener un **AttendeeProfile** (1:1 opcional, con PK compartida).

## Lo que ya tienes listo
Este starter incluye la solución con:
- Capas: `Controllers`, `Services`, `Repositories`, `Data (DbContext)`, `Entities`, `DTOs`.
- DI configurado en `Program.cs` y proveedor **InMemory** para rapidez en aula.
- Repos genéricos y específicos con métodos base y consultas incluídas (`Include`).

## Tareas (implementa y/o completa)
1. **Configurar relaciones en `AppDbContext` (OnModelCreating):**
   - 1:N **Festival ➜ Stages** con FK requerida y eliminación en cascada de stages al borrar un festival.
   - N:M **Artist ↔ Stage** mediante **Performance** con **clave compuesta** `(ArtistId, StageId, StartTime)`.
   - 1:N **Attendee ➜ Tickets** con FK requerida.
   - 1:1 **Attendee ↔ AttendeeProfile** con **PK compartida** en `AttendeeProfile` (opcional en Attendee).
2. **Validación de solapamiento de horarios (bonus +5):**
   - Al crear un `Performance`, impedir que en **la misma Stage** existan horarios superpuestos.
3. **Endpoints que deben funcionar:**
   - `POST /api/v1/festivals` crea festival con lista de stages.
   - `GET  /api/v1/festivals/{id}/lineup` devuelve programación por stage (ordenada por hora).
   - `POST /api/v1/artists` crea artista.
   - `POST /api/v1/artists/performances` vincula artista con stage y horario.
   - `GET  /api/v1/artists/{id}/schedule` devuelve agenda del artista.
   - `POST /api/v1/attendees/register` registra asistente (si vienen `DocumentId` y `BirthDate`, crea su `AttendeeProfile` 1:1).
   - `POST /api/v1/attendees/tickets` registra compra de ticket.
   - `GET  /api/v1/attendees/{id}/tickets` lista tickets del asistente.
4. **Arquitectura por capas:**
   - No acceder al `DbContext` desde los controllers: usa Services y Repositories (ya están registrados).
   - Mantén DTOs en la capa DTOs, mapea en Services.
5. **(Opcional) Manejo de errores / validaciones / paginación / filtro** (+5 extra).

## Criterios de evaluación (100 pts)
- **Relaciones y mapeos EF Core (40 pts):**
  - 1:N Festival‑Stage (10)
  - N:M con payload Performance (15)
  - 1:N Attendee‑Ticket (10)
  - 1:1 Attendee‑AttendeeProfile (5)
- **Arquitectura por capas (30 pts):**
  - Controllers delgados y limpios (10)
  - Services realizando la lógica de negocio y mapeos (10)
  - Repositories encapsulando acceso a datos (10)
- **Endpoints funcionales (20 pts):**
  - Crear y consultar datos según requerimientos (10)
  - Consultas con `Include` y orden correcto (10)
- **Buenas prácticas (5 pts):**
  - DI correcta, DTOs/validaciones, nombres y organización (5)
- **Bonus (5 pts):**
  - Evitar solapamiento de horarios en Stage (5)
  - o Manejo de errores/paginación/filtros (5)

## Cómo ejecutar
```bash
dotnet run --project TecWebFest.Api
# Swagger: http://localhost:5000/swagger (puede variar el puerto)
```

## Notas
- Puedes cambiar InMemory por SQLite si deseas, pero no es obligatorio para el examen.
- El **foco** es demostrar dominio de **relaciones** y **capas** en 90 minutos.

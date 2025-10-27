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

## Cómo evaluar y probar
✅ 1. Crear un Festival con Stages (1:N)

POST api/v1/festivals

{
  "name": "TecWebFest 2025",
  "city": "Cochabamba",
  "startDate": "2025-07-01",
  "endDate": "2025-07-03",
  "stages": [
    { "name": "Main Stage" },
    { "name": "Electro Dome" }
  ]
}


🎤 2. Crear un Artista

POST api/v1/artists

{
  "stageName": "DJ Infinity",
  "genre": "Electronic"
}

🎸 3. Crear otro Artista
{
  "stageName": "Rock Masters",
  "genre": "Rock"
}

🎭 4. Asignar Performance (N:M con payload)

POST api/v1/artists/performances

{
  "artistId": 1,
  "stageId": 1,
  "startTime": "2025-07-01T20:00:00",
  "endTime": "2025-07-01T21:30:00"
}

📅 5. Consultar Lineup del Festival

GET api/v1/festivals/1/lineup

Respuesta esperada (estructura):

{
  "festival": "TecWebFest 2025",
  "city": "Cochabamba",
  "stages": [
    {
      "stage": "Main Stage",
      "performances": [
        {
          "artistId": 1,
          "artist": "DJ Infinity",
          "startTime": "2025-07-01T20:00:00",
          "endTime": "2025-07-01T21:30:00"
        }
      ]
    }
  ]
}

🧍 6. Registrar Asistente (1:1 opcional con Profile)

POST api/v1/attendees/register

{
  "fullName": "Juan Pérez",
  "email": "juan@example.com",
  "documentId": "12345678",
  "birthDate": "1998-05-21"
}

🎫 7. Comprar Ticket (1:N)

POST api/v1/attendees/tickets

{
  "attendeeId": 1,
  "festivalId": 1,
  "price": 120.50,
  "category": "VIP"
}

🎟 8. Listar Tickets del Asistente

GET api/v1/attendees/1/tickets

Respuesta esperada:

[
  {
    "id": 1,
    "festival": "TecWebFest 2025",
    "category": "VIP",
    "price": 120.50,
    "purchasedAt": "2025-02-27T15:21:00.000Z"
  }
]

🎼 9. Ver Agenda de un Artista

GET api/v1/artists/1/schedule


🧪 10. BONUS – Intentar solapamiento (Debe fallar si validan)

POST api/v1/artists/performances

{
  "artistId": 2,
  "stageId": 1,
  "startTime": "2025-07-01T20:30:00",
  "endTime": "2025-07-01T21:00:00"
}


Respuesta esperada si está implementado el BONUS:

{
  "error": "The stage already has a performance in this time range."
}


## Notas
- El **foco** es demostrar dominio de **relaciones** y **capas** en 90 minutos.

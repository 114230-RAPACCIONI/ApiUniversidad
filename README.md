# API Universidad - Sistema de Gestión Escolar

API REST desarrollada en ASP.NET Core 9.0 para la gestión de una universidad, incluyendo administración de alumnos, docentes, cursos y autenticación con JWT.

## 📋 Requisitos Previos

- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0)
- [PostgreSQL](https://www.postgresql.org/download/) (versión 12 o superior)
- IDE recomendado: Visual Studio, Visual Studio Code o JetBrains Rider

## 🚀 Instalación y Configuración

### 1. Clonar el Repositorio

```bash
git clone <url-del-repositorio>
cd ApiUniversidad
```

### 2. Configurar Base de Datos PostgreSQL

#### Crear Base de Datos

1. Abre PostgreSQL (pgAdmin o línea de comandos)
2. Crea una nueva base de datos llamada `Universidad`:

```sql
CREATE DATABASE Universidad;
```

#### Configurar Cadena de Conexión

Edita el archivo `Backend/ApiUniversidad/ApiUniversidad/appsettings.json` o `appsettings.Development.json`:

```json
{
  "ConnectionStrings": {
    "ConexionDB": "Host=localhost;Database=Universidad;Port=5432;User Id=postgres;Password=TU_PASSWORD;"
  }
}
```

**Importante:** Reemplaza `TU_PASSWORD` con la contraseña de tu usuario de PostgreSQL.

### 3. Configuración Code First con Entity Framework

El proyecto utiliza **Code First** para crear las tablas automáticamente desde los modelos.

#### Opción A: Usar Migraciones (Recomendado)

1. Abre una terminal en la carpeta del proyecto:
```bash
cd Backend/ApiUniversidad/ApiUniversidad
```

2. Instala las herramientas de Entity Framework (si no están instaladas):
```bash
dotnet tool install --global dotnet-ef
```

3. Crea la primera migración:
```bash
dotnet ef migrations add InitialCreate
```

4. Aplica las migraciones a la base de datos:
```bash
dotnet ef database update
```

#### Opción B: Crear Base de Datos al Iniciar (Solo Desarrollo)

Si prefieres que se cree automáticamente, puedes agregar al `Program.cs`:

```csharp
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<UniversidadContext>();
    db.Database.EnsureCreated();
}
```

**Nota:** Esto solo es recomendable para desarrollo. En producción, usa migraciones.

### 4. Configurar JWT

El proyecto ya incluye una clave secreta JWT en `appsettings.json`. Para producción, genera una nueva clave segura:

```bash
# Generar una clave secreta aleatoria (en Linux/Mac)
openssl rand -base64 64
```

Actualiza el valor en `appsettings.json`:

```json
{
  "JwtSettings": {
    "SecretKey": "TU_CLAVE_SECRETA_AQUI"
  }
}
```

### 5. Restaurar Dependencias y Compilar

```bash
cd Backend/ApiUniversidad/ApiUniversidad
dotnet restore
dotnet build
```

### 6. Ejecutar la Aplicación

```bash
dotnet run
```

La API estará disponible en:
- **HTTP:** `http://localhost:5000`
- **HTTPS:** `https://localhost:5001`
- **Swagger UI:** `http://localhost:5000` (en modo desarrollo)

## 📁 Estructura del Proyecto

```
Backend/ApiUniversidad/ApiUniversidad/
├── Controllers/          # Controladores de la API
│   ├── AlumnoController.cs
│   ├── DocenteController.cs
│   ├── CursoController.cs
│   ├── LoginController.cs
│   └── RoleController.cs
├── Dtos/                 # Data Transfer Objects
├── Interfaces/           # Interfaces de repositorios y servicios
├── Mappings/             # Configuración de AutoMapper
├── Models/               # Modelos de Entity Framework
├── Query/                # Objetos para queries/comandos
├── Repositories/         # Implementación de repositorios
├── Response/             # Clases de respuesta API
├── Services/             # Lógica de negocio
├── Program.cs            # Punto de entrada de la aplicación
└── appsettings.json      # Configuración
```

## 🔐 Autenticación

La API utiliza JWT (JSON Web Tokens) para autenticación. El endpoint de login está **público** (sin autenticación requerida).

### Endpoint de Login

```http
POST /login
Content-Type: application/json

{
  "nombreUsuario": "usuario@email.com",
  "email": "usuario@email.com"
}
```

**Respuesta:**
```json
{
  "success": true,
  "data": {
    "nombreUsuario": "usuario@email.com",
    "email": "usuario@email.com",
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9..."
  }
}
```

### Uso del Token

Incluye el token en el header `Authorization` de las peticiones:

```http
Authorization: Bearer eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...
```

El JWT incluye los siguientes claims:
- `Id` - ID del usuario
- `Email` - Email del usuario
- `IdRol` - ID del rol
- `NombreRol` - Nombre del rol (admin, docente, alumno)
- `DescripcionRol` - Descripción del rol
- `FechaAlta` - Fecha de alta del usuario

## 📡 Endpoints Disponibles

### Autenticación

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| POST | `/login` | ❌ | - |

### Alumnos

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| GET | `/alumnos/getAllAlumnos` | ✅ | admin |
| GET | `/alumnos/getAlumnosById/{id}` | ✅ | admin |
| POST | `/alumno/crearAlumno` | ✅ | admin |
| PUT | `/alumno/updateAlumno/{id}` | ✅ | admin, alumno |
| DELETE | `/alumno/deleteAlumno/{id}` | ✅ | admin |

### Docentes

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| GET | `/docentes/getAllDocentes` | ✅ | admin |
| GET | `/docentes/getDocenteById/{id}` | ✅ | admin |
| POST | `/docente/crearDocente` | ✅ | admin |
| PUT | `/docente/updateDocente/{id}` | ✅ | admin, docente |
| DELETE | `/docente/deleteDocente/{id}` | ✅ | admin |

### Cursos

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| GET | `/cursos/getAllCursos` | ✅ | admin |
| GET | `/cursos/getCursoById/{id}` | ✅ | admin |
| POST | `/curso/crearCurso` | ✅ | admin |
| PUT | `/curso/updateCurso/{id}` | ✅ | admin |
| DELETE | `/curso/deleteCurso/{id}` | ✅ | admin |
| GET | `/cursos/getAlumnosByCurso/{idCurso}` | ✅ | admin |

### Asignaciones

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| POST | `/curso/asignarAlumno` | ✅ | admin |
| DELETE | `/curso/quitarAlumno/{idCurso}/{idAlumno}` | ✅ | admin |
| POST | `/curso/asignarDocente` | ✅ | admin |
| DELETE | `/curso/quitarDocente/{idCurso}/{idDocente}` | ✅ | admin |

### Roles

| Método | Endpoint | Auth | Rol |
|--------|----------|------|-----|
| GET | `/getAllRoles` | ✅ | - |

## 🗄️ Modelos de Base de Datos

### Usuarios
- `Id` - Guid
- `Email` - string
- `Contraseña` - string
- `IdRol` - Guid (FK a Roles)
- `FechaAlta` - DateTime

### Roles
- `Id` - Guid
- `Nombre` - string (admin, docente, alumno)
- `Descripcion` - string

### Alumnos
- `Id` - Guid
- `Nombre` - string
- `Apellido` - string
- `Legajo` - string
- `IdRol` - Guid (FK a Roles)
- `FechaAlta` - DateTime

### Docentes
- `Id` - Guid
- `Nombre` - string
- `Apellido` - string
- `Legajo` - string
- `IdRol` - Guid (FK a Roles)
- `FechaAlta` - DateTime

### Cursos
- `Id` - Guid
- `Nombre` - string
- `FechaCreacion` - DateTime
- `Horarios` - string
- `IdCarrera` - Guid (FK a CarrerasUniversidad)

### CarrerasUniversidad
- `Id` - Guid
- `Nombre` - string

### DocentesPorCurso
- `Id` - Guid
- `IdCurso` - Guid (FK a Cursos)
- `IdDocente` - Guid (FK a Docentes)
- `FechaAlta` - DateTime

### AlumnosPorCurso
- `Id` - Guid
- `IdCurso` - Guid (FK a Cursos)
- `IdAlumno` - Guid (FK a Alumnos)
- `FechaAlta` - DateTime

## 🛠️ Tecnologías Utilizadas

- **ASP.NET Core 9.0** - Framework web
- **Entity Framework Core 9.0** - ORM para acceso a datos
- **PostgreSQL** - Base de datos
- **JWT Bearer** - Autenticación
- **AutoMapper** - Mapeo de objetos
- **Swagger/OpenAPI** - Documentación de API

## 📝 Aspectos Técnicos Implementados

✅ Autenticación con JWT (incluye todos los datos del usuario y rol como claims)  
✅ AutoMapper para mapeo de DTOs  
✅ Arquitectura en capas (Controllers → Services → Repositories)  
✅ DTOs para transferencia de datos  
✅ Repositorios con interfaces  
✅ Servicios con interfaces  
✅ Code First con Entity Framework  
✅ Autorización basada en roles  
✅ Validaciones y manejo de errores  

## 🔧 Comandos Útiles

### Crear una nueva migración
```bash
dotnet ef migrations add NombreMigracion
```

### Aplicar migraciones pendientes
```bash
dotnet ef database update
```

### Revertir última migración
```bash
dotnet ef database update NombreMigracionAnterior
```

### Eliminar última migración (sin aplicarla)
```bash
dotnet ef migrations remove
```

### Ver el script SQL de una migración
```bash
dotnet ef migrations script
```

## 📚 Documentación API

Cuando la aplicación está en ejecución, puedes acceder a la documentación interactiva de Swagger en:
- **Desarrollo:** `http://localhost:5000` o `https://localhost:5001`

Desde Swagger puedes:
- Ver todos los endpoints disponibles
- Probar los endpoints directamente
- Ver los modelos de datos
- Autenticarte y probar endpoints protegidos

## ⚠️ Notas Importantes

1. **Base de Datos:** Asegúrate de que PostgreSQL esté corriendo antes de ejecutar la aplicación.
2. **JWT Secret Key:** Cambia la clave secreta JWT en producción por una más segura.
3. **Conexión DB:** La cadena de conexión en `appsettings.json` contiene credenciales por defecto. Cámbialas según tu configuración.
4. **Migraciones:** En producción, usa migraciones en lugar de `EnsureCreated()`.
5. **CORS:** La configuración actual permite cualquier origen. Restringe esto en producción.

## 🐛 Troubleshooting

### Error: "No connection could be made because the target machine actively refused it"
- Verifica que PostgreSQL esté corriendo
- Revisa la cadena de conexión en `appsettings.json`

### Error: "JWT Secret Key no está configurada"
- Verifica que `JwtSettings:SecretKey` esté en `appsettings.json`

### Error: "relation does not exist"
- Aplica las migraciones: `dotnet ef database update`

### Error: "Authentication failed"
- Verifica que el usuario y contraseña de PostgreSQL sean correctos
- Verifica que la base de datos exista

## 📄 Licencia

Este proyecto es de código abierto y está disponible bajo la licencia MIT.

## 👥 Autor

Desarrollado para la gestión de universidad.

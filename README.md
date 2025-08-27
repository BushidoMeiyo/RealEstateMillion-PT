# 🏡 RealEstateMillion

**RealEstateMillion** es una solución integral para la gestión de propiedades inmobiliarias, construida con tecnologías modernas tanto en el frontend como en el backend.

Incluye:

- 🔧 **Backend** en .NET 8 (ASP.NET Core)
- 💻 **Frontend** en React + Vite
- ⚙️ Arquitectura limpia con capas bien definidas
- 🐳 Docker para contenerización y despliegue local
- ✅ Pruebas unitarias e integración

---

## 📁 Estructura del Proyecto

```
RealEstateMillion/
│
├── RealEstate.Api/                             # API principal
├── RealEstate.Application/                    # Lógica de aplicación (casos de uso)
├── RealEstate.Domain/                         # Entidades y lógica de dominio
├── RealEstate.Infrastructure/                 # Acceso a datos, servicios externos
├── RealEstate.Application.UnitTests/          # Pruebas unitarias
├── RealEstate.Infrastructure.Tests.Integration/  # Pruebas de integración
│
├── real-estate-client/                        # Frontend en React + Vite
├── docker-compose.yml                         # Orquestación de servicios
└── RealEstateMillion.sln                      # Solución de Visual Studio
```

---

## 🚀 Cómo Ejecutar el Proyecto

### 🔁 Requisitos Previos

- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download)
- [Node.js y npm](https://nodejs.org/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop)

---

### 🔧 Levantar el Backend (.NET API)

```bash
cd RealEstate.Api
dotnet run
```

- API disponible en: `https://localhost:7186`
- Usa CORS habilitado para desarrollo local con el frontend

---

### 💻 Levantar el Frontend (React + Vite)

```bash
cd real-estate-client
npm install
npm run dev
```

- Abre [http://localhost:5173](http://localhost:5173) en tu navegador

---

### 🐳 Levantar con Docker

```bash
docker-compose up -d
```

> Puedes modificar el archivo `docker-compose.yml` para incluir una base de datos como MongoDB o PostgreSQL según la arquitectura del proyecto.

---

## 🧪 Ejecutar Pruebas

### 🔹 Pruebas Unitarias

```bash
cd RealEstate.Application.UnitTests
dotnet test
```

### 🔸 Pruebas de Integración

```bash
cd RealEstate.Infrastructure.Tests.Integration
dotnet test
```

---

## 📌 Tecnologías y Herramientas Usadas

- **.NET 8** con arquitectura limpia (Clean Architecture)
- **React + Vite** con componentes modernos
- **Docker Compose** para entornos locales
- * NUnit** (en backend)

---

## 📄 Licencia

Este proyecto se distribuye bajo la licencia **MIT**.

---

## ✍️ Autor

**Martin Mauricio Martínez Romero**  
👨‍💻 Fullstack Developer | Arquitecto de Software | Entusiasta del Ajedrez y la Tecnología  
📫 martin.martinez.dev@outlook.com

---

¡Gracias por visitar este repositorio! ⭐ Si este proyecto te parece útil, considera dejar una estrella.

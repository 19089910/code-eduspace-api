
# 🎓 EduSpace – Sistema de Acompanhamento de Matrículas

**EduSpace** é uma aplicação full stack com **CRUD completo** para gerenciamento de **Cursos, Alunos e Matrículas**, desenvolvida como parte de uma avaliação técnica.

---

## 📌 Funcionalidades

- ✅ CRUD de **Cursos** (criar, listar, editar, excluir)
- ✅ CRUD de **Alunos** (nome, e-mail, data de nascimento — com validação para maioridade)
- ✅ CRUD de **Matrículas** (matricular e desmatricular alunos em cursos)
- ✅ Listagens com filtros:
  - Alunos por curso
  - Todos os alunos matriculados
  - Todos os cursos disponíveis

---

## 🛠 Tecnologias Utilizadas

### Backend
- .NET 9
- Entity Framework Core (com Migrations)
- SQL Server

### Frontend
- React
- TypeScript ✅

---

## 📁 Estrutura do Projeto

/eduspace
├── backend/ # API em .NET 9 com EF Core e SQL Server
├── frontend/ # Aplicação React com TypeScript
└── README.md

yaml
Copy
Edit

---

## 🚀 Como rodar o projeto localmente

### ✅ Pré-requisitos

- [.NET 9 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/9.0)
- [Node.js](https://nodejs.org/)
- [SQL Server](https://www.microsoft.com/pt-br/sql-server/sql-server-downloads)
- [Git](https://git-scm.com/)

---

### 🖥️ Backend (.NET 9 + EF Core + SQL Server)

1. Acesse a pasta do backend:

cd backend
Restaure os pacotes:

bash
Copy
Edit
dotnet restore
Configure a conexão com o banco de dados no appsettings.json:

json
Copy
Edit
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=EduSpaceDB;Trusted_Connection=True;TrustServerCertificate=True;"
}
Aplique as migrations:

bash
Copy
Edit
dotnet ef database update
Inicie a aplicação:

bash
Copy
Edit
dotnet run
A API estará disponível em https://localhost:5001 ou http://localhost:5000
Deploy:
Elastic Beanstalk para hospedar sua API 
Amazon RDS para hospedar seu banco SQL Server.

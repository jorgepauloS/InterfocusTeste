# Teste Técnico - Interfocus

Bibliotecas utilizadas no Backend:
- Microsoft.EntityFrameworkCore - ORM;
- Microsoft.EntityFrameworkCore.Tools - Para gerar as migrations;
- Microsoft.EntityFrameworkCore.Design - Obrigatório para o funcionamento do Tools;
- Microsoft.EntityFrameworkCore.Relational - Para fazer as configurações das entidades;
- Microsoft.EntityFrameworkCore.SqlServer - Para conectar com banco SQL Server e fazer as configurações relacionadas ao banco;
- AutoMapper - Fazer o mapeamento para conversão entre os DTOs e as entidades.

Instruções:
- Colocar string de conexão no appsettings.json: ConnectionStrings.SqlConnection;
- Executar comando "Update-Database" no Package Manager Console.

Bibliotecas utilizadas no Frontend:
- mui - Componentes do material UI;
- mui/icons-material - Ícones do material UI;
- react-router-dom - trabalhar de forma simplificada com roteamento;
- axios - trabalhar de forma simplificada com requisições.

Instruções:
- Antes de executar, colocar endereço da API no arquivo /vendinha-frontend/src/Services/ApiClient.js.

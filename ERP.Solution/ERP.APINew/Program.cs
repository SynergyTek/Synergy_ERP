<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
  </PropertyGroup>

  <ItemGroup>
    <PackageReference Include="QuestPDF" Version="2025.5.1" />
    <PackageReference Include="Swashbuckle.AspNetCore" Version="6.6.2" />
    <PackageReference Include="Microsoft.EntityFrameworkCore" Version="9.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Relational" Version="9.0.5" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="9.0.5" />
    <PackageReference Include="Npgsql.EntityFrameworkCore.PostgreSQL" Version="9.0.4" />
  </ItemGroup>

  <ItemGroup>
    <ProjectReference Include="..\\ERP.PayrollService\\ERP.PayrollService.csproj" />
  </ItemGroup>

</Project> 
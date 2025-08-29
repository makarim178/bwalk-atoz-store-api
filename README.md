# AtoZ.Store

AtoZ.Store is a modern e-commerce API built with .NET, designed for scalability and ease of integration. The project includes RESTful endpoints for managing products, carts, and orders, and leverages Supabase for database migrations and seeding.

## Project Structure

- **AtoZ.Store.Api/**  
  Main ASP.NET Core Web API source code, including controllers, DTOs, models, repositories, and services.
- **productsDetailsFromApi/**  
  Scripts for generating SQL seed data from product lists.
- **supabaseMigration/**  
  Supabase migration files and configuration.

## Getting Started

### Prerequisites

- [.NET 9 SDK](https://dotnet.microsoft.com/download)
- [Supabase CLI](https://supabase.com/docs/guides/cli)
- [libpq (PostgreSQL client)](https://www.postgresql.org/docs/current/libpq.html)

### Setup

1. **Clone the repository**
   ```sh
   git clone <repo-url>
   cd bwalk-atoz-store-api
   ```

2. **Restore dependencies**
   ```sh
   dotnet restore AtoZ.Store.Api/AtoZ.Store.Api.csproj
   ```

3. **Build the project**
   ```sh
   dotnet build AtoZ.Store.Api/AtoZ.Store.Api.csproj
   ```

### Supabase Migration

1. **Install Supabase CLI**  
   [Supabase CLI Installation Guide](https://supabase.com/docs/guides/cli/installation)

2. **Navigate to migration folder**
   ```sh
   cd supabaseMigration
   ```

3. **Login to Supabase**
   ```sh
   supabase login
   ```

4. **Link your Supabase project**
   ```sh
   supabase link --project-ref <your-project-ref>
   ```

5. **Push migrations**
   ```sh
   supabase db push
   ```

### Seeding the Database

#### Option 1: Using Supabase CLI

```sh
supabase db execute ./supabase/seed.sql --db-url "postgresql://postgres:3lrPu7TY5RA0Mvxh@db.hwjxnxnhnzqwqhcbsjzm.supabase.co:5432/postgres"
```

#### Option 2: Using psql

1. **Install libpq**  
   [libpq Installation Guide](https://www.postgresql.org/docs/current/libpq.html)

2. **Run the seed command**
   ```sh
   psql "postgresql://postgres:3lrPu7TY5RA0Mvxh@db.hwjxnxnhnzqwqhcbsjzm.supabase.co:5432/postgres" -f supabase/seed.sql
   ```

### API Endpoints

- **Products**: `/api/products`
- **Cart**: `/api/cart`
- **Orders**: `/api/orders`

Refer to the controllers in [AtoZ.Store.Api/Controllers](AtoZ.Store.Api/Controllers) for details.

## Notes

- Seed data is generated via [productsDetailsFromApi/products.js](productsDetailsFromApi/products.js).
- DTOs are defined in [AtoZ.Store.Api/DTOs](AtoZ.Store.Api/DTOs).
- Ensure your Supabase project is properly linked before running migrations or seeding.

## License

This project is licensed under the MIT License.

---

*For questions or contributions, please open an issue or submit a pull request.*
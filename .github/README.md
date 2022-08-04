# My Travel Journal Api

Simple API to store experiences from travelling. This API is backend
for [Client-side application](https://github.com/filip0s/my-travel-journal-web) made in React.

## Project  quickstart

### Prerequisites

- For building the project you will need [.NET 6 runtime](https://dotnet.microsoft.com/en-us/download).
- [Docker](https://docs.docker.com/desktop/install/linux-install/) is needed for running the Postgres database image

### Preparation
Just copy commands below into your terminal

```shell
# Clone the repo
git clone git@github.com:filip0s/my-travel-journal-api.git

# Change into repo root
cd my-travel-journal-api

# Spin up database image in docker
docker compose up -d

# Install Entity Framework tool
dotnet tool install --global dotnet-ef

# Change into API project directory
cd MyTravelJournal.Api

# Database migration
dotnet ef migrations add InitialMigration
dotnet ef database update
```

### Running
After everything form the section above is done, you can just run the project.

```shell
dotnet run
```

## API Endpoints

- If running the project in the Development mode, you can use Swagger UI to overview API endpoints

### Trips

#### [GET] All Trips

| Method | `GET`       |
|--------|-------------|
| URL    | `/api/trip` |

Responds with array of all trip logs in the database

<details>
<summary>Example request</summary>

```shell
curl -X 'GET' \
  'https://localhost:7258/api/trip' \
  -H 'accept: text/plain'

```

</details>

<details>
<summary>Example response (<code>200 OK</code>) </summary>

```json
[
  {
    "id": 1,
    "title": "Mount Fuji",
    "description": "Our trip to Japan",
    "location": "Japan",
    "start": "2022-05-24T00:00:00",
    "end": "2022-05-27T00:00:00"
  },
  {
    "id": 2,
    "title": "Whole day on the beach",
    "description": "I've finally been to LA, so it was a MUST to go to Venice",
    "location": "Venice Beach, CA",
    "start": "2022-08-04T13:07:57.827Z",
    "end": "2022-08-04T13:07:57.827Z"
  }
]
```

</details>

---

#### [GET] Get Trip By ID

| Method | `GET`            |
|--------|------------------|
| URL    | `/api/trip/{id}` |

Fetches single trip log, which is selected by its ID

<details>
<summary>Example cURL request</summary>

```shell
curl -X 'GET' \
  'https://localhost:7258/api/trip/2' \
  -H 'accept: text/plain'
```

</details>

<details>
<summary>Example Response (<code>200 OK</code>)</summary>

```shell

{
  "id": 2,
  "title": "Whole day on the beach",
  "description": "I've finally been to LA, so it was a MUST to go to Venice",
  "location": "Venice Beach, CA",
  "start": "2022-08-04T13:07:57.827Z",
  "end": "2022-08-04T13:07:57.827Z"
}
```

</details>

---

#### [POST] Create New Trip Log

| Method | `POST`      |
|--------|-------------|
| URL    | `/api/trip` |

Adds new travel log with the data specified inside of the request body

<details>
<summary>Example request body</summary>

```json
{
  "id": 0,
  "title": "string",
  "description": "string",
  "location": "string",
  "start": "2022-07-28T18:33:13.348Z",
  "end": "2022-07-28T18:33:13.348Z"
}
```

> value of the `id` field does not matter because database assigns its own id.

> Note that the `start` and `end` attribute which are type of `DateTime` should be
> in [ISO 8601 format](https://en.wikipedia.org/wiki/ISO_8601)

</details>
<details>
<summary>Example cURL request</summary>

```shell
curl -X 'POST' \
  'https://localhost:7258/api/trip' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
  "title": "Whole day on the beach",
  "description": "I'\''ve finally been to LA, so it was a MUST to go to Venice",
  "location": "Venice Beach, CA",
  "start": "2022-08-04T13:07:57.827Z",
  "end": "2022-08-04T13:07:57.827Z"
}'
```

</details>

<details>
<summary>Example response (<code>200 OK</code>)</summary>

```text
Journal successfully added
```

</details>

---

#### [PATCH] Update Trip By ID

| Method | `PATCH`          |
|--------|------------------|
| URL    | `/api/trip/{id}` |

Updates information about trip, which is specified by ID

<details>
<summary>Example cURL request</summary>

```shell
curl -X 'PATCH' \
  'https://localhost:7258/api/trip/2' \
  -H 'accept: */*' \
  -H 'Content-Type: application/json' \
  -d '{
    "id": 2,
    "title": "Whole day on the beach",
    "description": "I'\''ve finally been to LA, so it was a MUST to go to Santa Monica",
    "location": "Santa Monica, CA",
    "start": "2022-08-04T13:07:57.827Z",
    "end": "2022-08-04T13:07:57.827Z"
  }'
  
```

> Please be wary of the fact that the value of the `id` field in the request
> JSON does not matter. `id` is only specified by value passed in the URL

</details>

<details>
<summary>Example Response (<code>200 OK</code>)</summary>

```text
Trip data (id 2) successfully updated.
```

</details>

---

#### [DELETE] Delete Trip By ID

| Method | `DELETE`         |
|--------|------------------|
| URL    | `/api/trip/{id}` |

Deletes trip log, which is specified by its ID

<details>
<summary>Example cURL request</summary>

```shell
curl -X 'DELETE' \
  'https://localhost:7258/api/trip/1' \
  -H 'accept: */*'
```

</details>

<details>
<summary>Example Response (<code>200 OK</code>)</summary>

```text
Trip with id 1 sucessfully deleted
```

</details>

## Used technologies

- **API framework:** ASP.NET 6
- **ORM:** Entity Framework
- **Database:** PostgreSQL
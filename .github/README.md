# My Travel Journal Api

Simple API to store experiences from travelling. This API is backend
for [Client-side application](https://github.com/filip0s/my-travel-journal-web) made in React.

## Project  quickstart

### Prerequisites

For running the project you will need .NET 6 Runtime, which is available to
download [here](https://dotnet.microsoft.com/en-us/download).

- On Fedora you can run `sudo dnf install dotnet` to install the officially maintained package.


### Running
`dotnet run`

## API Endpoints

- If running the project in the Development mode, you can use Swagger UI to overview API endpoints

### Journals

#### [GET] All Journals

| Method | `GET`           |
|--------|-----------------|
| URL    | `/api/journal`  |

Responds with array of all logs in the in-memory "database" (real database is TBA)

<details>
<summary>Example request</summary>

```bash
curl -X 'GET' \
  'https://localhost:7258/api/journal' \
  -H 'accept: text/plain'

```

</details>

<details>
<summary>Example response (<code>200 OK</code>) </summary>

```json
[
  {
    "id": 0,
    "title": "Mount Fuji",
    "description": "Our trip to Japan",
    "location": "Japan",
    "start": "2022-05-24T00:00:00",
    "end": "2022-05-27T00:00:00"
  }
]
```

</details>

#### [POST] Create new journal log

| Method | `POST`         |
|--------|----------------|
| URL    | `/api/journal` |

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

Note that the `start` and `end` attribute which are type of `DateTime` should be
in [ISO 8601 format](https://en.wikipedia.org/wiki/ISO_8601)

</details>
<details>
<summary>Example cURL request</summary>

```bash
curl -X 'POST' \
  'https://localhost:7258/api/journal' \
  -H 'accept: text/plain' \
  -H 'Content-Type: application/json' \
  -d '{
  "id": 0,
  "title": "string",
  "description": "string",
  "location": "string",
  "start": "2022-07-28T18:37:48.572Z",
  "end": "2022-07-28T18:37:48.572Z"
}'

```

</details>

<details>
<summary>Example response (<code>200 OK</code>)</summary>

```text
Journal successfully added
```
</details>



## Used technologies

- **Framework:** ASP.NET Web Api
- **Database:** PostgreSQL
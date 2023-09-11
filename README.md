# FlightBookingAPI

The FlightBookingAPI is a RESTful API that provides access to airline-related information. This project serves as a case study and includes three main endpoints: GET /airports, GET /flights, and GET /config. To ensure the proper functioning of the application, a Redis server running at localhost:6379 is required. Users can set up Redis using the following Docker command:

```
docker run -d -p 6379:6379 redis
```

## Requirements
To run the project, you need the following dependencies:

.NET 6 SDK: https://dotnet.microsoft.com/download/dotnet/6.0

Redis: A local Redis server is required to start the project.

## Installation
To run the project on your local machine, follow these steps:

Download and install the .NET 6 SDK.

Run Redis on your local machine using Docker:

bash
```
docker run -d -p 6379:6379 redis
```

Clone the project:

```
git clone https://github.com/your-username/flightbookingapi.git
cd flightbookingapi
```

Start the application:

```
dotnet run
```

Once the application is running, the REST API will be available at the following endpoints:

```GET /airports:``` To retrieve airport data.

```GET /flights:``` To retrieve flight data.

```GET /config:``` To retrieve configuration information.

Usage
In this section, explain how to use the project. Provide examples of making API requests, such as:

License
This project is licensed under the Apache License, Version 2.0 - see the LICENSE file for details.

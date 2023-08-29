# Demo .NET Application

This is a demo .NET application for connecting to the Sportsbook real-time data feed.

## Prerequisites

- .NET Core SDK installed

## Installation

1. Clone the repository
2. Run `dotnet restore` to install dependencies

## Usage

1. Run `dotnet run` to start the application

The application will connect to the RabbitMQ server, create a queue, bind it to the exchange, and start consuming fixture data.
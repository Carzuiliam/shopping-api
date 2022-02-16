# Shopping API

This project shows an example of an API that simulates an online shopping cart.

## Introduction

If you (like me), loves to buy things from the Internet, you may wonder how the concept of a shopping cart works online. Generally, all interactions with a user's shopping cart can be saved directly in an online database -- but this may be too risky because of a set of factors, like the lack of security, or less integration with other applications (e.g., access by smartphone). Because of this, online shopping tends to prefer the use of an API. 

An **API** is an interface that acts as a "bridge" between an application and a server with the requested data. The application does a request to the API, which processes it, calls the server, gets the requested data from it, and returns the data to the application. This data, then, can be manipulated by the application the way it desires. 

## About This Project

This project is made on [**Visual Studio 2022**](https://visualstudio.microsoft.com/vs/). Structurally speaking, there are two main objects inside this project:

- The **source code** (obviously!);
- The **data source**, inside the _Database_ folder, which contains the SQL database, and the scripts needed to feed the database before running the application (*DB_TABLE_CREATION* and *DB_TABLE_FEEDING*). 

### The Data Source

Currently, the data source of this project is a [**SQLite**](https://www.sqlite.org/) database. I preferred SQLite because it's easy to configure and access the content in it since it's just a commom file -- but I don't recommend SQLite for large applications, because of the lack of access speed, scalability, and security.

### The Entity Concept

I created a concept, which I called **Entity**, in order to produce the necessary SQL queries to perform operations in the database. An Entity is defined, in this context, as a representation of an object in the database, including its operations -- e.g., a SQL table. More details of how this concept works can be found in the source code itself.

## Additional Info

Since this is a work in progress, probably I will add more features in the future (e.g. treatement of corner cases and bug fixes). Stay tuned!

## License

The available source codes here are under the Apache License, version 2.0 (see the attached `LICENSE` file for more details). Any questions can be submitted to my email: carloswilldecarvalho@outlook.com.
# Sales Taxes
This API will help you to calculate some simple taxes when calculating the items price and also to sum up the totals for the sale.

For imported items will be added a tax of 5% and for products which doesn't have a tax exception will be added a 10%. Both taxes will be applied on the shelf price of the item.

For the grouping purpose of the items in the sale result, an item will be considered the same product when it has the same description, category and also its origin.

To be considered valid all the fields are required, the item must have a quantity greater than 0, also its unit price must be greater than 0.

## Technologies and design
The technologies used were C# .Net 5 API using packages as FluentValidation, Serilog, Automapper among others. The application was also containerized in docker and published in DockerHub.

The application is a multilayered app using SOLID principles and clean code best practices. Each layer has its own responsability making easier to code reusage and maintanence. Also some unit and integration tests were created to cover the most important rules of the application to assure its quality.

## How to run
You can run the API in Visual Studio 2019 or executing the commands below to use the docker compose or you can get the image directly from DockerHub

Docker-compose on the root of this repo
``` 
docker-compose -f docker-compose.yml -f docker-compose.override.yml up -d
```

Image in DockerHub
``` 
docker run -d -p 8000:80 joaobaracat/salestaxesapi
```

When running on Visual Studio the port used will be the 5000 and on Docker the port 8000. You can access the swagger using the links below:

http://localhost:5000/swagger/index.html
or
http://localhost:8000/swagger/index.html

**Another option to run it, you can unzip the publish.zip in the root of this repo and execute the file SalesTaxes.API.exe**

**After that, you can access the link http://localhost:5000/swagger/index.html**

## Payloads
Here you have a suggested example to test the calculation functionality request and its response:

Request
``` 
[
  {
    "description": "Imported box of chocolates",
    "quantity": 1,
    "unitPrice": 10.00,
    "origin": "Imported",
    "category": "Food"
  },
  {
    "description": "Imported bottle of perfume",
    "quantity": 1,
    "unitPrice": 47.50,
    "origin": "Imported",
    "category": "Cosmetics"
  }
]
```

Response
```
{
  "success": true,
  "data": {
    "totalSalesTaxes": 7.65,
    "totalPrice": 65.15,
    "saleItems": [
      {
        "description": "Imported box of chocolates",
        "quantity": 1,
        "salePrice": 10.5,
        "totalPrice": 10.5
      },
      {
        "description": "Imported bottle of perfume",
        "quantity": 1,
        "salePrice": 54.65,
        "totalPrice": 54.65
      }
    ]
  }
}
```

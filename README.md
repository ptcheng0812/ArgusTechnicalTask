# Argus Technical Task
You are testing a checkout system for a restaurant. There is a new endpoint that will calculate the total of the order, and add a 10% service charge on food.

## Assumptions of the api service: 
1. order/book:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 201
- the backend api service create a new Order object stored in memory / db

2. order/add/{_orderId}:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 200
- the backend api service query that Order object using orderId and add count of people, starters, mains, drinks, drinks 

3. order/delete/{_orderId}:
- receive request body {
     people (int),
     starters (int),
     mains (int),
     drinks (int),
     hour (string) 
} as JSON Body then return {orderId: <some id>, people } with status code 200
- the backend api service query that Order object using orderId and deduct count of people, starters, mains, drinks, drinks 

4. checkout/bill/{_orderId}:
- query the order using orderId 
- calculate the bill in backend: (count of starters*4 + count of mains*7) * 1.1 + count of drinks * 2.5 + count of drinks with discount *2.5 * 0.7
- return { "totalAmount": totalAmount, "people": people remaining }

5. If an invalid orderId is provided, the API returns 404 Not Found.


## Project Structure
![image](https://github.com/user-attachments/assets/b0b6145f-7030-4fda-9958-72ad47d03362)

## How to Run
```sh
cd ArgusAPICheckoutSystem
dotnet restore
dotnet build
dotnet run 
```
to start the api running on http://localhost:5283 if needed

```sh
cd ArgusTestCheckoutSystem
dotnet restore
dotnet build
dotnet test
```
to start running the specflow tests

## Dependencies 

| Dependencies |  
| ------ | 
| Specflow / ReqnRoll| 
| NUnit |
| RestSharp |
| Newtonsoft Json | 


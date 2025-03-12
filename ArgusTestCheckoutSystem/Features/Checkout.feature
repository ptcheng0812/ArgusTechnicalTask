Feature: Checkout

Assumptions of the api service:

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
- calculate the bill in backend: (count of starters*4 + count of mains*7) * 1.1 + count of drinks*2.5 + count of drinks with discount*2.5*0.7
- return { "totalAmount": totalAmount, "people": people remaining }

5. If an invalid orderId is provided, the API returns 404 Not Found.


Scenario: 0000 Order with Drink Discount
Group of order with 4 people, 4 starters, 4 mains, and 4 drinks before 19:00
	Given a group places an order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 4      | 4        | 4     | 4      | 18:59 |
	When the bill is requested
	Then the total amount should be "£55.40" and 4 people remain

Scenario: 0001 Order without Drink Discount
Group of order with 4 people, 4 starters, 4 mains, and 4 drinks on 19:00
	Given a group places an order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 4      | 4        | 4     | 4      | 19:00 |
	When the bill is requested
	Then the total amount should be "£58.40" and 4 people remain

Scenario: 0100 Order with Drink Discount then Add Order Without Drink Discount
Group of order with 2 people, 1 starters, 2 mains, and 2 drinks before 19:00 
then another group of order with 2 people, 0 starters, 2 mains, and 2 drinks order at 20:00
	Given a group places an order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 2      | 1        | 2     | 2      | 18:00 |
	When the bill is requested
	Then the total amount should be "£23.30" and 2 people remain
	Given a group add an order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 2      | 0        | 2     | 2      | 20:00 |
	When the bill is requested
	Then the total amount should be "£43.70" and 4 people remain

Scenario: 0200 Each Order before 19:00 then One Person Left before 19:00
4 people each order with 1 starters, 1 mains, and 1 drinks before 19:00 
1 people cancel order before 19:00 
	Given a group each order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 4      | 1        | 1     | 1      | 18:00 |
	When the bill is requested
	Then the total amount should be "£55.40" and 4 people remain
	Given order is cancelled as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 1      | 1        | 1     | 1      | 18:59 |
	When the bill is requested
	Then the total amount should be "£41.55" and 3 people remain

Scenario: 0201 Each Order after 19:00 then One Person Left after 19:00
4 people each order with 1 starters, 1 mains, and 1 drinks after 19:00 
1 people cancel order after 19:00 
	Given a group each order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 4      | 1        | 1     | 1      | 19:01 |
	When the bill is requested
	Then the total amount should be "£58.40" and 4 people remain
	Given order is cancelled as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 1      | 1        | 1     | 1      | 20:00 |
	When the bill is requested
	Then the total amount should be "£43.80" and 3 people remain

Scenario: 0202 Each Order before 19:00 then One Person Left after 19:00
4 people each order with 1 starters, 1 mains, and 1 drinks before 19:00 
1 people cancel order after 19:00 
	Given a group each order as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 4      | 1        | 1     | 1      | 18:59 |
	When the bill is requested
	Then the total amount should be "£55.40" and 4 people remain
	Given order is cancelled as follow
		| People | Starters | Mains | Drinks | Hour  |
		| 1      | 1        | 1     | 1      | 20:00 |
	When the bill is requested
	Then the total amount should be "£43.30" and 3 people remain



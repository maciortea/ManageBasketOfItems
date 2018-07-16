# ManageBasketOfItems
This API will allow users to set up and manage an order of items

Usage: <br />
POST /api/account/register - creates new user
POST /api/account/token - user login (will return bearer token which will be passed as authorization header in subsecvent requests)

GET /api/basket - get basket for logged user
POST /api/basket/items - add item to basket
DELETE /api/basket/items/{id} - remove item from basket
DELETE /api/basket/items - clear all items
PUT /api/basket/items/{id} - change quantity

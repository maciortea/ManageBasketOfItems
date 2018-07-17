# ManageBasketOfItems
This API will allow users to set up and manage an order of items.

Usage: <br />
<strong>POST /api/account/register</strong> - creates new user <br />
<strong>POST /api/account/token</strong> - user login (will return bearer token which will be passed as authorization header in subsecvent requests) <br />
<strong>GET /api/basket</strong> - get basket for logged user <br />
<strong>GET /api/basket/items/{id}</strong> - gets the basket item by id
<strong>POST /api/basket/items</strong> - add item to basket <br />
<strong>DELETE /api/basket/items/{id}</strong> - remove item from basket <br />
<strong>DELETE /api/basket/items</strong> - clear all items <br />
<strong>PUT /api/basket/items/{id}</strong> - change quantity <br />

# ManageBasketOfItems
This API will allow users to set up and manage an order of items.

API | Description | Request headers | Request body | Response body
--- | ----------- | --------------- | ------------ | -------------
POST /api/account/register | Create a new user | None | { "email": "value", "password": "pwdValue", "confirmPassword": "pwdValue" } | None
POST /api/account/token | User login | None | { "email": "marian_test@yahoo.com", "password": "Pass@word1" } | Bearer token
GET /api/basket | Get basket for logged user | Authorization: Bearer token | None | Basket
GET /api/basket/items/{id} | Get a basket item by id | Authorization: Bearer token | None | Basket item
POST /api/basket/items | Add a new basket item | Authorization: Bearer token | { "productId": 5, "quantity": 4, "priceInPounds": 1.2 } | Basket item
DELETE /api/basket/items/{id} | Remove a basket item by id | Authorization: Bearer token | None | None
DELETE /api/basket/items | Clear all items from basket | Authorization: Bearer token | None | None
PUT /api/basket/items/{id} | Change basket item's quantity | Authorization: Bearer token | None | None

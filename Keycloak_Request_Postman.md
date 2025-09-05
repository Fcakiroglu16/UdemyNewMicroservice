


---Client Credential flow by Endpoint-----------------------------------------
curl --location 'http://localhost:8080/realms/udemyTenant/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id=web' \
--data-urlencode 'client_secret=vkljFT3n5vz6EDrqVJD9DJUi9qp8RBrs' \
--data-urlencode 'grant_type=client_credentials'

---Client Credential flow by Admin Endpoint-----------------------------------------
curl --location 'http://localhost:8080/realms/udemyTenant/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id=admin-cli' \
--data-urlencode 'client_secret=hJ3ze3hwXSlnKFqxBxzQCsgkrZiJlwan' \
--data-urlencode 'grant_type=client_credentials'

---Resource Owner Password Flow Endpoint-----------------------------------------
curl --location 'http://localhost:8080/realms/udemyTenant/protocol/openid-connect/token' \
--header 'Content-Type: application/x-www-form-urlencoded' \
--data-urlencode 'client_id=web' \
--data-urlencode 'client_secret=vkljFT3n5vz6EDrqVJD9DJUi9qp8RBrs' \
--data-urlencode 'grant_type=password' \
--data-urlencode 'username=fcakiroglu16' \
--data-urlencode 'password=Password12*'

----User Create Endpoint---------------------------------------------------------
curl --location 'http://localhost:8080/admin/realms/udemyTenant/users' \
--header 'Content-Type: application/json' \
--header 'Authorization: ••••••' \
--data-raw '{
  "username": "fcakiroglu17",
  "enabled": true,
  "firstName": "John",
  "lastName": "Doe",
  "email": "f-cakiroglu17@outlook.com",
  "credentials": [
    {
      "type": "password",
      "value": "Password12*",
      "temporary": true
    }
  ]
}'

---- Sigin In Endpoint---------------------------------------------------------
curl --location 'http://localhost:5280/api/v1/courses' \
--form 'name="course 3"' \
--form 'picture=@"/C:/Users/f-cak/OneDrive/Masaüstü/images.jpg"' \
--form 'description="course 1 Açıklama"' \
--form 'price="200"' \
--form 'categoryId="08ddeac9-294f-b52b-1b62-9e59b8fd0000"'
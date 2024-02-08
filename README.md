# COCREATEAPI
COCREATEAPI


Issue with update media file name.

Fix strings for container names.

Sort out error messages (invalid email)

Change the path of register top use "auth"
 

docker tag cocreateapi cocreateapiregistry.azurecr.io/cocreateapi:v0.5

docker push cocreateapiregistry.azurecr.io/cocreateapi:v0.4

az acr login --name cocreateapiregistry

az login

docker build -t cocreateapi .
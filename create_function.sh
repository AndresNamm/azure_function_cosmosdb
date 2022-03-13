az login
az account set --subscription IC3-PSTNDYNRTG-DEV
az group create --name dev-andres-test --location northeurope
az storage account create --name andresdevstorage --location northeurope --resource-group dev-andres-test --sku Standard_LRS
az functionapp create --resource-group dev-andres-test --consumption-plan-location northeurope --runtime dotnet-isolated --functions-version 4 --name dev-andres-test --storage-account andresdevstorage
func azure functionapp publish dev-andres-test
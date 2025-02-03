$backendLocation = $PSScriptRoot

cd "..\shoppinglist-frontend\"
npm run build

cp -r .\out\* ..\shoppinglist-backend\wwwroot\

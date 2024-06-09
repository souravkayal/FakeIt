
![FakeIt_logo](https://github.com/souravkayal/FakeIt/assets/6651731/14c1ce8d-1c6e-457d-848c-575754293e37) 

<h2> FakeIt </h2> A simple application to host your fake API simulate HTTP response. The application is build to keep the usecase simple but efficient. 
</br> </br>
<b> Teach Stack : </b> </br> </br> 
ASP.NET Core 8 to run the backend service. </br> </br>
CosmosDB is to store the API setup.
</br></br>

<h4> Setup guideline </h4> </br>

1. Host the application whereever you want ;) . Preferably in azure ! best choice is App Service. Sinple.
2. Configure CosmosDB NoSQL container.

   I.   Create CosmosDB NoSQL Instance </br></br>
   II.  Create database "fakeit-store" </br></br>
   III. Create container "api-master" </br>
      &nbsp;&nbsp; III.A  Set partition key as "/project_name". </br>
      &nbsp;&nbsp; III.B  Set unique key by combining "/url" and "/http_methode".  
   
   ![FakeIt-easy_Cosmos](https://github.com/souravkayal/FakeIt/assets/6651731/bf0f65d2-f3ce-4e0e-8595-f2bd172bc3aa)
   <br/><br/>
3. 
   Set the Cosmos connection string in appsettings. Keep the DatabaseId as it is. 
   
   ````
   "CosmosDb": {
    "ConnectionString": "",
    "DatabaseId": "fakeit-store"
   }
   ````

4. Browser www.yourhost.com/swagger and have fun !   
   
![FakeIteasy_swagar](https://github.com/souravkayal/FakeIt/assets/6651731/ab8b30d3-386c-46be-bccf-1e883125b3e0)

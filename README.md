# Twitter Trending in Athens

A simple repository for a twitter scrapper that displays the results in an ASP.NET+Angular app.


## Web Application

To build the web app, it is necessary to build the frontend and then the backend.
After that, just deploy it wherever it pleases you.

### Front end

In the **Trending.Athens.Client** directory execute the following:

`npm run build --prod`

By default, the product of the build is saved in the wwwroot/ of the backend project.
Done.

### Backend

In the **Trending.Athens.Api** directory execute the following: 

`dotnet publish --self-cotained -c release -r [chosen runtime] -o bin/Publish`

Done.

---

All that's left is to deploy the webapplication to a VM/VPS of your choosing and setup a service running the following:

`dotnet Trending.Athens.Api.dll`

Done.

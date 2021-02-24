# PowerPlantChallenge
> From the [powerplant-coding-challenge](https://github.com/gem-spaas/powerplant-coding-challenge) Calculate how much power each of a multitude of different powerplants need to produce (a.k.a. the production-plan) when the load is given and taking into account the cost of the underlying energy sources (gas, kerosine) and the Pmin and Pmax of each powerplant.

<img src="https://www.next-kraftwerke.be/en/technology/next-pool/" title="Power Plant Distribution" alt="Power Plant Distribution Image">

[Image source](https://www.next-kraftwerke.be/wp-content/uploads/2016/03/virtuele-energiecentrale.png)

## Required
[.Net Core 3.1](https://dotnet.microsoft.com/download/dotnet/3.1)

## Build
Run the following command line in the project directory
```shell
$ dotnet build
```

## Run
Run the following command line in the project directory
```shell
$ dotnet run
```

## Test
You can now use your favorite testing tool on the next url.
Send a POST request with one of the JSON files available on the [challenge page](https://github.com/gem-spaas/powerplant-coding-challenge/tree/master/example_payloads) 
 I personaly use [POSTMAN](https://www.postman.com/) for API testing.
- http://localhost:8888/productionplan

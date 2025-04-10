# mongoBenchmark
Benchmarking different Mongo DB packages

# Run Mongo
docker run --name mongo -p 27017:27017 -d mongo

# Run App to generate 10 dummy users
cd mongobenchmark/
dotnet run
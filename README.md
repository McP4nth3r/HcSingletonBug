# Demo for Singleton Bug in HotChocolate
This is a demo for a bug in HotChocolate. The bug is that the singleton pattern is not working as expected. The singleton doesn't created once.

## How to reproduce
1. Clone the repository
2. Run the project
3. Open the GraphQL Playground
4. Run the following query:
```graphql
query {
  test
}
```
it should prints the following at response:
```json
{
  "data": {
    "test": [
      "2c016eae-1dcb-4604-ad9e-4ae59386d471",
      "2c016eae-1dcb-4604-ad9e-4ae59386d472",
      "2c016eae-1dcb-4604-ad9e-4ae59386d473",
      "2c016eae-1dcb-4604-ad9e-4ae59386d474"
    ]
  }
}
```
but it prints:
```json
{
  "data": {
    "test": []
  }
}
```

5. Press key on Console

it writes the following to the console:
```
2c016eae-1dcb-4604-ad9e-4ae59386d471
2c016eae-1dcb-4604-ad9e-4ae59386d472
2c016eae-1dcb-4604-ad9e-4ae59386d473
2c016eae-1dcb-4604-ad9e-4ae59386d474
```
6. Run query again
7. The Dictionary is empty in Query but its not empty in Tasks (see Console)

# RepeatFixture
.NET framework that creates mocked classes with repeatable results.


#How To Use

To create a single instance of a class using default rules:
'''RepeatFixture.Create<ClassName>();'''

To create a multiple instances of a class using default rules:
'''RepeatFixture.CreateMany<ClassName>();


Rules available to pass in:
 - seed: This lets you vary the starting point for randomly generating values.
 - subClassFillLevel: This is the number of nested classes to populate.
 - count: For the CreateMany<T>() method this identifies how many instances to create and return in a List.
